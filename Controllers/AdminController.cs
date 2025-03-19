using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eElection.Models;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Crypto.Generators;
using eElection.Data;
using eElection.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eElection.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly PasswordHasher<Account> _passwordHasher;
        private readonly EmailService _emailService;

        public AdminController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _passwordHasher = new PasswordHasher<Account>();
            _emailService = emailService;
        }

        public IActionResult SomeProtectedPage()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Elections()
        {
            var elections = _context.Elections.ToList(); // Retrieve all elections from the database
            return View(elections);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Parties()
        {
            var parties = _context.Parties.ToList(); // Retrieve all parties from the database
            return View(parties);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Positions()
        {
            var positions = _context.Positions.ToList(); // Retrieve all positions from the database
            return View(positions);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ElectionTypes()
        {
            var electionTypes = _context.ElectionTypes.ToList(); 
            return View(electionTypes);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Candidates()
        {
            // Retrieve candidates with related table data
            var candidates = _context.Candidates
                .Include(c => c.Election)
                .Include(c => c.Voter)
                .Include(c => c.Party)
                .Include(c => c.Position)
                .Select(c => new CandidateExtension
                {
                    CandidateId = c.CandidateId,
                    ElectionName = c.Election != null ? c.Election.ElectionName : "N/A",
                    FullName = c.Voter != null ? c.Voter.FirstName + " " + c.Voter.LastName : "N/A",
                    PartyName = c.Party != null ? c.Party.PartyName : "N/A",
                    PositionName = c.Position != null ? c.Position.PositionName : "N/A",
                    Biography = c.Biography,
                    CreatedAt = c.CreatedAt
                })
                .ToList();

            // Populate dropdown lists
            ViewBag.Elections = new SelectList(_context.Elections, "ElectionId", "ElectionName");
            ViewBag.Voters = new SelectList(_context.Voters
                .Select(v => new { VoterId = v.VoterId, FullName = v.FirstName + " " + v.LastName })
                .ToList(), "VoterId", "FullName");
            ViewBag.Parties = new SelectList(_context.Parties, "PartyId", "PartyName");
            ViewBag.Positions = new SelectList(_context.Positions, "PositionId", "PositionName");

            // ✅ Pass the candidates list as the model
            return View(candidates);
        }

        // POST: Add Candidate
        [HttpPost]
        public IActionResult AddCandidate([FromBody] Candidate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Candidates.Add(model);
                _context.SaveChanges();

                return Json(new { success = true, message = "Candidate added successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while adding the candidate." });
            }
        }

        [HttpGet]
        public IActionResult GetCandidate(int id)
        {
            var candidate = _context.Candidates
                .Where(c => c.CandidateId == id)
                .Select(c => new
                {
                    c.CandidateId,
                    c.ElectionId,
                    c.VoterId,
                    c.PartyId,
                    c.PositionId,
                    c.Biography
                })
                .FirstOrDefault();

            if (candidate == null)
            {
                return NotFound(new { success = false, message = "Candidate not found." });
            }

            return Json(candidate);
        }

        [HttpPost]
        public IActionResult EditCandidate([FromBody] Candidate model)
        {
            var candidate = _context.Candidates.Find(model.CandidateId);
            if (candidate == null)
            {
                return Json(new { success = false, message = "Candidate not found." });
            }

            candidate.ElectionId = model.ElectionId;
            candidate.VoterId = model.VoterId;
            candidate.PartyId = model.PartyId;
            candidate.PositionId = model.PositionId;
            candidate.Biography = model.Biography;

            _context.SaveChanges();

            return Json(new { success = true, message = "Candidate updated successfully!" });
        }

        [HttpPost]
        public IActionResult DeleteCandidate(int id)
        {
            var candidate = _context.Candidates.Find(id);
            if (candidate == null)
            {
                return Json(new { success = false, message = "Candidate not found." });
            }

            _context.Candidates.Remove(candidate);
            _context.SaveChanges();

            return Json(new { success = true, message = "Candidate deleted successfully!" });
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Voters()
        {
            var voters = _context.Voters.ToList(); // Retrieve all voters from the database
            return View(voters);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Announcements()
        {
            var announcements = _context.Announcements.ToList(); // Retrieve all announcements from the database
            return View(announcements);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Home()
        {
            // Get the count of voters
            int voterCount = _context.Voters.Count();

            // Get the count of candidates
            int candidateCount = _context.Candidates.Count();
            // Pass the counts and candidate list to the view
            ViewBag.VoterCount = voterCount;
            ViewBag.CandidateCount = candidateCount;
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Election()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AddVoter()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Position()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Try()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Homes()
        {
            // Get the count of voters
            int voterCount = _context.Voters.Count();

            // Get the count of candidates
            int candidateCount = _context.Candidates.Count();
            // Pass the counts and candidate list to the view
            ViewBag.VoterCount = voterCount;
            ViewBag.CandidateCount = candidateCount;
            return View();
        }
        public async Task<IActionResult> ElectionTypePositions()
        {
            var electionTypePositions = await _context.ElectionTypePositions
                .Include(etp => etp.ElectionType)
                .Include(etp => etp.Position)
                .ToListAsync();

            // Pass ElectionTypes and Positions to the view for dropdowns
            ViewBag.ElectionTypes = await _context.ElectionTypes.ToListAsync();
            ViewBag.Positions = await _context.Positions.ToListAsync();

            return View(electionTypePositions);
        }

        [HttpPost]
        public async Task<IActionResult> AddElectionTypePosition([FromBody] ElectionTypePositions model)
        {
            if (!ModelState.IsValid)
            {
                // Log validation errors
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                Console.WriteLine("Validation Errors: " + string.Join(", ", errors));
                return Json(new { success = false, message = "Invalid data.", errors = errors });
            }

            try
            {
                // Log the received data
                Console.WriteLine("Received ElectionTypePosition: ElectionTypeId=" + model.ElectionTypeId + ", PositionId=" + model.PositionId);

                // Save to database
                _context.ElectionTypePositions.Add(model);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Position added to Election Type successfully!" });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("Error: " + ex.Message);
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }

        // POST: Admin/DeleteElectionTypePosition
        [HttpPost]
        public async Task<IActionResult> DeleteElectionTypePosition(int id)
        {
            try
            {
                var electionTypePosition = await _context.ElectionTypePositions.FindAsync(id);
                if (electionTypePosition == null)
                {
                    return Json(new { success = false, message = "Record not found." });
                }

                _context.ElectionTypePositions.Remove(electionTypePosition);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Position removed from Election Type successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Candidate()
        {
            ViewBag.Elections = new SelectList(_context.Elections, "ElectionId", "ElectionName");
            // Concatenate FirstName and LastName for Full Name
            ViewBag.Voters = new SelectList(_context.Voters
                .Select(v => new
                {
                    VoterId = v.VoterId,
                    FullName = v.FirstName + " " + v.LastName
                }).ToList(), "VoterId", "FullName");
            //ViewBag.Voters = new SelectList(_context.Voters, "VoterId", "FirstName");
            ViewBag.Parties = new SelectList(_context.Parties, "PartyId", "PartyName");
            ViewBag.Positions = new SelectList(_context.Positions, "PositionId", "PositionName");
            
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddCandidate(Candidate candidate)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Candidates.Add(candidate);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Home");
        //    }

        //    return View();
        //}
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Logins", "Account"); // Redirect to Account login page
        }

        [HttpPost]
        public IActionResult AddElectionist([FromBody] Election model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                // Save to database
                _context.Elections.Add(model);
                _context.SaveChanges();

                return Json(new { success = true, message = "Election added successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving the election." });
            }
        }

        [HttpPost]
        public IActionResult AddElection([FromBody] Election model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Elections.Add(model);
                _context.SaveChanges();

                return Json(new { success = true, message = "Election added successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while adding the election.", error = ex.Message });
            }
        }



        [HttpPost]
        public IActionResult AddAnnouncement([FromBody] Announcement model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                // Save to database
                _context.Announcements.Add(model);
                _context.SaveChanges();

                return Json(new { success = true, message = "Announcement added successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving the announcement." });
            }
        }
        [HttpGet]
        public IActionResult GetAnnouncement(int id)
        {
            var announcement = _context.Announcements.Find(id);
            if (announcement == null)
            {
                return NotFound();
            }
            return Json(announcement);
        }
        [HttpPost]
        public IActionResult EditAnnouncement([FromBody] Announcement model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingAnnouncement = _context.Announcements.Find(model.AnnouncementId);
            if (existingAnnouncement == null)
            {
                return NotFound();
            }

            try
            {
                existingAnnouncement.Title = model.Title;
                existingAnnouncement.Description = model.Description;
                existingAnnouncement.Status = model.Status;

                _context.SaveChanges();
                return Json(new { success = true, message = "Announcement updated successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while updating the announcement." });
            }
        }

        [HttpPost]
        public IActionResult DeleteAnnouncement(int id)
        {
            var announcement = _context.Announcements.Find(id);
            if (announcement == null)
            {
                return NotFound();
            }

            try
            {
                _context.Announcements.Remove(announcement);
                _context.SaveChanges();
                return Json(new { success = true, message = "Announcement deleted successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while deleting the announcement." });
            }
        }

        [HttpPost]
        public IActionResult AddElectionType([FromBody] ElectionType model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                // Save to database
                _context.ElectionTypes.Add(model);
                _context.SaveChanges();

                return Json(new { success = true, message = "Election type added successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving the election type." });
            }
        }

        [HttpGet]
        public IActionResult GetElectionType(int id)
        {
            var electionType = _context.ElectionTypes.Find(id);
            if (electionType == null)
            {
                return NotFound();
            }
            return Json(electionType);
        }

        [HttpPost]
        public IActionResult EditElectionType([FromBody] ElectionType model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingElectionType = _context.ElectionTypes.Find(model.ElectionTypeId);
            if (existingElectionType == null)
            {
                return NotFound();
            }

            try
            {
                existingElectionType.ElectionTypeName = model.ElectionTypeName;
                _context.SaveChanges();
                return Json(new { success = true, message = "Election type updated successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while updating the election type." });
            }
        }

        [HttpPost]
        public IActionResult DeleteElectionType(int id)
        {
            var electionType = _context.ElectionTypes.Find(id);
            if (electionType == null)
            {
                return NotFound();
            }

            try
            {
                _context.ElectionTypes.Remove(electionType);
                _context.SaveChanges();
                return Json(new { success = true, message = "Election type deleted successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while deleting the election type." });
            }
        }

        [HttpPost]
        public IActionResult AddPosition([FromBody] Position model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                // Save to database
                _context.Positions.Add(model);
                _context.SaveChanges();

                return Json(new { success = true, message = "Position added successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving the position." });
            }
        }
        [HttpGet]
        public IActionResult GetPosition(int id)
        {
            var position = _context.Positions.Find(id);
            if (position == null)
            {
                return NotFound();
            }
            return Json(position);
        }
        [HttpPost]
        public IActionResult EditPosition([FromBody] Position model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingPosition = _context.Positions.Find(model.PositionId);
            if (existingPosition == null)
            {
                return NotFound();
            }

            try
            {
                existingPosition.PositionName = model.PositionName;
                _context.SaveChanges();
                return Json(new { success = true, message = "Position updated successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while updating the position." });
            }
        }

        [HttpPost]
        public IActionResult DeletePosition(int id)
        {
            var position = _context.Positions.Find(id);
            if (position == null)
            {
                return NotFound();
            }

            try
            {
                _context.Positions.Remove(position);
                _context.SaveChanges();
                return Json(new { success = true, message = "Position deleted successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while deleting the position." });
            }
        }

        [HttpPost]
        public IActionResult AddParty([FromBody] Party model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors
            }

            try
            {
                // Save to database
                _context.Parties.Add(model);
                _context.SaveChanges();

                return Json(new { success = true, message = "Party added successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred while saving the party." });
            }
        }
        [HttpGet]
        public IActionResult GetParty(int id)
        {
            var party = _context.Parties.Find(id);
            if (party == null)
            {
                return NotFound();
            }
            return Json(party);
        }
        [HttpPost]
        public IActionResult EditParty([FromBody] Party model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingParty = _context.Parties.Find(model.PartyId);
            if (existingParty == null)
            {
                return NotFound();
            }

            try
            {
                existingParty.PartyName = model.PartyName;
                existingParty.Leader = model.Leader;
                existingParty.FoundedYear = model.FoundedYear;
                _context.SaveChanges();
                return Json(new { success = true, message = "Party updated successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while updating the party." });
            }
        }

        [HttpPost]
        public IActionResult DeleteParty(int id)
        {
            var party = _context.Parties.Find(id);
            if (party == null)
            {
                return NotFound();
            }

            try
            {
                _context.Parties.Remove(party);
                _context.SaveChanges();
                return Json(new { success = true, message = "Party deleted successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while deleting the party." });
            }
        }

        [HttpGet]
        public IActionResult GetElection(int id)
        {
            var election = _context.Elections.Find(id);
            if (election == null)
            {
                return NotFound();
            }
            return Json(election);
        }

        [HttpPost]
        public IActionResult EditElection([FromBody] Election model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingElection = _context.Elections.Find(model.ElectionId);
            if (existingElection == null)
            {
                return NotFound();
            }

            try
            {
                // Update properties
                existingElection.ElectionName = model.ElectionName;
                existingElection.StartDate = model.StartDate;
                existingElection.EndDate = model.EndDate;
                existingElection.Status = model.Status;
                existingElection.ElectionTypes = model.ElectionTypes; // Add this line

                _context.SaveChanges();
                return Json(new { success = true, message = "Election updated successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while updating the election." });
            }
        }

        [HttpPost]
        public IActionResult DeleteElection(int id)
        {
            var election = _context.Elections.Find(id);
            if (election == null)
            {
                return NotFound();
            }

            try
            {
                _context.Elections.Remove(election);
                _context.SaveChanges();
                return Json(new { success = true, message = "Election deleted successfully!" });
            }
            catch (Exception)
            {
                return Json(new { success = false, message = "An error occurred while deleting the election." });
            }
        }

        //[HttpPost]
        //public IActionResult AddParty(Party model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    try
        //    {
        //        // Save to database
        //        _context.Parties.Add(model);
        //        _context.SaveChanges();

        //        TempData["SuccessMessage"] = "Election added successfully!";
        //        return RedirectToAction("Home"); // Redirect to list page
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "An error occurred while saving the election.");
        //        return View(model);
        //    }
        //}
        //[HttpPost]
        //public IActionResult AddPosition(Position model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    try
        //    {
        //        // Save to database
        //        _context.Positions.Add(model);
        //        _context.SaveChanges();

        //        TempData["SuccessMessage"] = "Election added successfully!";
        //        return RedirectToAction("Home"); // Redirect to list page
        //    }
        //    catch (Exception ex)
        //    {
        //        ModelState.AddModelError("", "An error occurred while saving the election.");
        //        return View(model);
        //    }
        //}
        [HttpPost]
        public IActionResult AddVoter(Voter model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                // Save to database
                _context.Voters.Add(model);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Election added successfully!";
                return RedirectToAction("Home"); // Redirect to list page
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving the election.");
                return View(model);
            }
        }
    }
}
