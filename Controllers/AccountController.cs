using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eElection.Models;
using eElection.Data;
using Microsoft.AspNetCore.Identity;
using eElection.Services;
using System.Security.Claims;

namespace eElection.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Account> _passwordHasher;
        private readonly EmailService _emailService;

        public AccountController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Account>();
            _emailService = emailService;
        }
        public IActionResult Logins()
        {
            return View();
        }
        public IActionResult Registers()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Register(Account model, string confirmPassword)
        //{
        //    // Ensure Password is provided
        //    if (string.IsNullOrWhiteSpace(model.Password))
        //    {
        //        ModelState.AddModelError("Password", "Password is required.");
        //        return View(model);
        //    }

        //    // Validate if password matches confirm password
        //    if (model.Password != confirmPassword)
        //    {
        //        ModelState.AddModelError("ConfirmPassword", "Passwords do not match.");
        //        return View(model);
        //    }

        //    // Hash the password before saving
        //    model.Password = _passwordHasher.HashPassword(model, model.Password);

        //    if (!ModelState.IsValid)
        //    {
        //        var errors = ModelState.Values.SelectMany(v => v.Errors)
        //                                      .Select(e => e.ErrorMessage)
        //                                      .ToList();
        //        ViewBag.Errors = errors;
        //        return View(model);
        //    }

        //    //model.Role = model.Role ?? "User";
        //    model.EmailConfirmationToken = Guid.NewGuid().ToString();
        //    model.IsEmailConfirmed = false;
        //    model.CreatedAt = DateTime.UtcNow;

        //    _context.Account.Add(model);
        //    await _context.SaveChangesAsync();

        //    // Send confirmation email
        //    var confirmationLink = Url.Action("ConfirmEmail", "Account",
        //        new { email = model.Email, token = model.EmailConfirmationToken }, Request.Scheme);

        //    string emailBody = $@"
        //    <h2>Welcome, {model.Username}!</h2>
        //    <p>Please confirm your email to activate your account.</p>
        //    <a href='{confirmationLink}' style='padding:10px 20px; background-color:blue; color:white; text-decoration:none;'>Confirm Email</a>";

        //    await _emailService.SendEmailAsync(model.Email, "Confirm Your Email", emailBody);

        //    // Store success message in TempData
        //    TempData["SuccessMessage"] = "Registration successful! Please check your email and verify your account.";

        //    return RedirectToAction("Login");
        //}
        [HttpPost]
        public async Task<IActionResult> Register(Account model, string confirmPassword)
        {
            // Ensure Password is provided
            if (string.IsNullOrWhiteSpace(model.Password))
            {
                ModelState.AddModelError("Password", "Password is required.");
            }

            // Validate if password matches confirm password
            if (model.Password != confirmPassword)
            {
                ViewData["ConfirmPasswordError"] = "Passwords do not match.";
            }

            // Hash the password before saving
            model.Password = _passwordHasher.HashPassword(model, model.Password);

            if (!ModelState.IsValid)
            {
                // Return the view with validation errors
                return View("Registers", model);
            }

            // Create a new Voter linked to the Account
            var voter = new Voter
            {
                FirstName = "", // Use username as first name (or add a separate field)
                LastName = "", // Add last name if needed
                Email = model.Email,
                Phone = "", // Add phone if needed
                Address = "", // Add address if needed
                CreatedAt = DateTime.UtcNow
            };

            // Add the Voter to the database
            _context.Voters.Add(voter);
            await _context.SaveChangesAsync();

            // Link the Account to the Voter
            model.VoterId = voter.VoterId;
            model.EmailConfirmationToken = Guid.NewGuid().ToString();
            model.IsEmailConfirmed = false;
            model.CreatedAt = DateTime.UtcNow;

            _context.Account.Add(model);
            await _context.SaveChangesAsync();

            // Send confirmation email
            var confirmationLink = Url.Action("ConfirmEmail", "Account",
                new { email = model.Email, token = model.EmailConfirmationToken }, Request.Scheme);

            string emailBody = $@"
<h2>Welcome, {model.Username}!</h2>
<p>Please confirm your email to activate your account.</p>
<a href='{confirmationLink}' style='padding:10px 20px; background-color:blue; color:white; text-decoration:none;'>Confirm Email</a>";

            await _emailService.SendEmailAsync(model.Email, "Confirm Your Email", emailBody);

            // Store success message in TempData
            TempData["SuccessMessage"] = "Registration successful! Please check your email and verify your account.";

            return RedirectToAction("Logins");
        }

        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var user = _context.Account.FirstOrDefault(u => u.Email == email && u.EmailConfirmationToken == token);
            if (user != null)
            {
                user.IsEmailConfirmed = true;
                user.EmailConfirmationToken = string.Empty; // Use empty string instead of null
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Verify successful! You can now login.";
                return View("Logins");
            }
            return View("EmailConfirmationFailed");
        }
        public IActionResult Login()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<IActionResult> Login(string email, string password)
        //{
        //    // 🔹 Check if the user is the Static Admin
        //    if (email == "admin@gmail.com" && password == "admin123")
        //    {
        //        var adminClaims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, "Admin"),
        //        new Claim(ClaimTypes.Email, email),
        //        new Claim(ClaimTypes.Role, "Admin")  // Admin role
        //    };

        //        var adminIdentity = new ClaimsIdentity(adminClaims, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var adminPrincipal = new ClaimsPrincipal(adminIdentity);

        //        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, adminPrincipal);

        //        return RedirectToAction("Home", "Admin");  // Redirect to Admin Dashboard
        //    }

        //    // 🔹 Check Voter Login in the Database
        //    var userAccount = _context.Account.FirstOrDefault(u => u.Email == email);

        //    if (userAccount != null)
        //    {
        //        if (!userAccount.IsEmailConfirmed)
        //        {
        //            ViewBag.ErrorMessage = "You need to confirm your email before logging in.";
        //            return View();
        //        }

        //        var result = _passwordHasher.VerifyHashedPassword(userAccount, userAccount.Password, password);
        //        if (result == PasswordVerificationResult.Success)
        //        {
        //            var claims = new List<Claim>
        //        {
        //            new Claim(ClaimTypes.Name, userAccount.Username),
        //            new Claim(ClaimTypes.Email, userAccount.Email),
        //            new Claim(ClaimTypes.Role, "Voter")  // Default role for voters
        //        };

        //            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //            var principal = new ClaimsPrincipal(identity);

        //            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

        //            return RedirectToAction("Home", "User");  // Redirect voters to Home page
        //        }
        //    }

        //    ViewBag.ErrorMessage = "Invalid email or password.";
        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            // 🔹 Check if the user is the Static Admin
            if (email == "admin@gmail.com")
            {
                if (password == "1234") // Correct admin password
                {
                    var adminClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Admin"),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, "Admin")  // Admin role
            };

                    var adminIdentity = new ClaimsIdentity(adminClaims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var adminPrincipal = new ClaimsPrincipal(adminIdentity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, adminPrincipal);

                    return RedirectToAction("Home", "Admin");  // Redirect to Admin Dashboard
                }
                else
                {
                    // Specific error for incorrect admin password
                    ViewBag.ErrorMessage = "Invalid email or password.";
                    return View("Logins");
                }
            }

            // 🔹 Check Voter Login in the Database
            var userAccount = _context.Account
                .Include(a => a.Voter) // Include the linked Voter
                .FirstOrDefault(u => u.Email == email);

            if (userAccount != null)
            {
                if (!userAccount.IsEmailConfirmed)
                {
                    ViewBag.ErrorMessage = "You need to confirm your email before logging in.";
                    return View("Logins");
                }

                try
                {
                    // Verify the hashed password
                    var result = _passwordHasher.VerifyHashedPassword(userAccount, userAccount.Password, password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userAccount.Username),
                    new Claim(ClaimTypes.Email, userAccount.Email),
                    new Claim(ClaimTypes.Role, "Voter"),  // Default role for voters
                    new Claim("VoterId", userAccount.VoterId.ToString()) // Add VoterId to claims
                };

                        var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        var principal = new ClaimsPrincipal(identity);

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                        return RedirectToAction("Homes", "User");  // Redirect voters to Profile page
                    }
                    else
                    {
                        // Incorrect password for voter account
                        ViewBag.ErrorMessage = "Invalid email or password.";
                        return View("Logins");
                    }
                }
                catch (FormatException)
                {
                    // Handle invalid password hash
                    ViewBag.ErrorMessage = "Invalid password format. Please contact support.";
                    return View("Logins");
                }
            }

            // If email is not found
            ViewBag.ErrorMessage = "Invalid email or password.";
            return View("Logins");
        }

        // 🔹 Logout for General Users (Voter/Admin)
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Logins", "Account"); // Redirect to login page
        }
    }
}
