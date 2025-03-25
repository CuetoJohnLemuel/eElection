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
using System.Text.Json;

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
            var elections = _context.Elections.ToList(); // Retrieve all elections
            var electionTypes = _context.ElectionTypePositions
                                        .Select(e => e.ElectionTypeName) // Select only the names
                                        .Distinct() // Remove duplicates
                                        .ToList();

            ViewBag.ElectionTypes = electionTypes; // Pass unique election types to the view
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
        public async Task<IActionResult> ElectionType()
        {
            var groupedElectionTypePositions = await _context.ElectionTypePositions
                .Include(etp => etp.Position) // ✅ Include Position only
                .GroupBy(etp => etp.ElectionTypeName) // ✅ Group by ElectionTypeName (since it's now a string)
                .Select(group => new ElectionTypePositionsViewModel
                {
                    ElectionTypeName = group.Key, // ✅ Grouped by ElectionTypeName
                    Positions = string.Join(", ", group.Select(etp => etp.Position.PositionName)) // ✅ Get all positions under this ElectionType
                })
                .ToListAsync();

            ViewBag.Positions = await _context.Positions.ToListAsync();

            return View(groupedElectionTypePositions); // ✅ Return ViewModel
        }

        [HttpGet]
        public IActionResult GetPositionsByElectionType(string electionTypeName)
        {
            var positions = _context.ElectionTypePositions
                .Where(e => e.ElectionTypeName == electionTypeName)
                .Select(e => e.PositionId)
                .ToList();

            return Json(new { electionTypeName, positionIds = positions });
        }



        [HttpGet]
        public async Task<IActionResult> CheckElectionTypeExists(string name)
        {
            bool exists = await _context.ElectionTypePositions
                .AnyAsync(etp => etp.ElectionTypeName == name);

            return Json(new { exists });
        }

        [HttpPost]
        public async Task<IActionResult> AddElectionTypePosition([FromBody] JsonElement requestData)
        {
            try
            {
                // Parse ElectionTypeName safely
                if (!requestData.TryGetProperty("ElectionTypeName", out var electionTypeNameElement) ||
                    string.IsNullOrWhiteSpace(electionTypeNameElement.GetString()))
                {
                    return Json(new { success = false, message = "Election Type is required." });
                }

                string electionTypeName = electionTypeNameElement.GetString();

                // ✅ Check if ElectionTypeName already exists
                bool exists = await _context.ElectionTypePositions
                    .AnyAsync(etp => etp.ElectionTypeName == electionTypeName);

                if (exists)
                {
                    return Json(new { success = false, message = "This Election Type already exists." });
                }

                // Parse PositionIds safely
                if (!requestData.TryGetProperty("PositionIds", out var positionIdsElement) ||
                    positionIdsElement.ValueKind != JsonValueKind.Array)
                {
                    return Json(new { success = false, message = "Invalid PositionIds format. Expected an array." });
                }

                var positionIds = positionIdsElement.EnumerateArray()
                                  .Select(p => p.GetInt32())  // Extract int values from JSON array
                                  .ToList();

                if (!positionIds.Any())
                {
                    return Json(new { success = false, message = "At least one Position must be selected." });
                }

                Console.WriteLine($"📌 Received: ElectionTypeName={electionTypeName}, Positions={string.Join(",", positionIds)}");

                // ✅ Check if all Position IDs exist
                var existingPositions = _context.Positions
                    .Where(p => positionIds.Contains(p.PositionId))
                    .Select(p => p.PositionId)
                    .ToList();

                if (existingPositions.Count != positionIds.Count)
                {
                    return Json(new { success = false, message = "Some Position IDs are invalid or do not exist." });
                }

                // ✅ Insert new election type positions
                var newPositions = positionIds.Select(positionId => new ElectionTypePositions
                {
                    ElectionTypeName = electionTypeName,
                    PositionId = positionId
                }).ToList();

                _context.ElectionTypePositions.AddRange(newPositions);
                await _context.SaveChangesAsync();

                Console.WriteLine("✅ Insert successful!");
                return Json(new { success = true, message = "Positions added successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ Database Insert Error: " + ex.Message);
                Console.WriteLine("❌ Stack Trace: " + ex.StackTrace);

                return Json(new { success = false, message = "Error inserting into database.", error = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> EditElectionTypePosition([FromBody] JsonElement requestData)
        {
            try
            {
                if (!requestData.TryGetProperty("ElectionTypePositionId", out var electionTypePositionIdElement) ||
                    !requestData.TryGetProperty("ElectionTypeName", out var electionTypeNameElement) ||
                    !requestData.TryGetProperty("PositionIds", out var positionIdsElement))
                {
                    return Json(new { success = false, message = "Invalid input data." });
                }

                int electionTypePositionId = electionTypePositionIdElement.GetInt32();
                string electionTypeName = electionTypeNameElement.GetString();
                var positionIds = positionIdsElement.EnumerateArray().Select(p => p.GetInt32()).ToList();

                var existingPositions = await _context.ElectionTypePositions
                    .Where(etp => etp.ElectionTypePositionId == electionTypePositionId)
                    .ToListAsync();

                if (!existingPositions.Any())
                {
                    return Json(new { success = false, message = "Election Type Position not found." });
                }

                // Delete old positions
                _context.ElectionTypePositions.RemoveRange(existingPositions);

                // Add updated positions
                var newPositions = positionIds.Select(positionId => new ElectionTypePositions
                {
                    ElectionTypePositionId = electionTypePositionId,
                    ElectionTypeName = electionTypeName,
                    PositionId = positionId
                }).ToList();

                _context.ElectionTypePositions.AddRange(newPositions);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Election Type Position updated successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error updating database.", error = ex.Message });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteElectionTypePosition(int id)
        {
            try
            {
                var electionTypePositions = _context.ElectionTypePositions
                    .Where(etp => etp.ElectionTypePositionId == id)
                    .ToList();

                if (!electionTypePositions.Any())
                {
                    return Json(new { success = false, message = "Election Type Position not found." });
                }

                _context.ElectionTypePositions.RemoveRange(electionTypePositions);
                await _context.SaveChangesAsync();

                return Json(new { success = true, message = "Election Type Position deleted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting from database.", error = ex.Message });
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
                // Store election types as a single comma-separated string
                model.ElectionTypes = string.Join(",", model.ElectionTypes.Split(',').Distinct());

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
        public IActionResult DeleteElectionType(string electionTypeName)
        {
            var positions = _context.ElectionTypePositions
                .Where(e => e.ElectionTypeName == electionTypeName)
                .ToList();

            if (positions.Any())
            {
                _context.ElectionTypePositions.RemoveRange(positions);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false, message = "Election Type not found" });
        }


        [HttpPost]
        public IActionResult UpdateElectionType(string electionTypeName, List<int> selectedPositions)
        {
            var existingPositions = _context.ElectionTypePositions
                .Where(e => e.ElectionTypeName == electionTypeName)
                .ToList();

            _context.ElectionTypePositions.RemoveRange(existingPositions);
            _context.SaveChanges();

            foreach (var positionId in selectedPositions)
            {
                _context.ElectionTypePositions.Add(new ElectionTypePositions
                {
                    ElectionTypeName = electionTypeName,
                    PositionId = positionId
                });
            }

            _context.SaveChanges();
            return Json(new { success = true });
        }

        //[HttpPost]
        //public IActionResult EditElectionType([FromBody] ElectionType model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var existingElectionType = _context.ElectionTypes.Find(model.ElectionTypePositionId);
        //    if (existingElectionType == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        existingElectionType.ElectionTypeName = model.ElectionTypeName;
        //        _context.SaveChanges();
        //        return Json(new { success = true, message = "Election type updated successfully!" });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { success = false, message = "An error occurred while updating the election type." });
        //    }
        //}

        //[HttpPost]
        //public IActionResult DeleteElectionType(int id)
        //{
        //    var electionType = _context.ElectionTypes.Find(id);
        //    if (electionType == null)
        //    {
        //        return NotFound();
        //    }

        //    try
        //    {
        //        _context.ElectionTypes.Remove(electionType);
        //        _context.SaveChanges();
        //        return Json(new { success = true, message = "Election type deleted successfully!" });
        //    }
        //    catch (Exception)
        //    {
        //        return Json(new { success = false, message = "An error occurred while deleting the election type." });
        //    }
        //}

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
                existingPosition.MaxCandidates = model.MaxCandidates; // ✅ Add this line
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
