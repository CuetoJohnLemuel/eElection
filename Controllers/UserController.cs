using eElection.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eElection.Data;
using eElection.Services;
using Microsoft.AspNetCore.Identity;

namespace eElection.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        // Only one constructor
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [Authorize(Roles = "Voter")]
        public IActionResult Home()
        {
            return View();
        }

        [Authorize(Roles = "Voter")]
        public IActionResult Ballot(string electionType)
        {
            if (string.IsNullOrEmpty(electionType))
            {
                return BadRequest("Election type is required.");
            }

            Console.WriteLine($"Election Type received: {electionType}");

            // Pass the election type to the view
            ViewBag.ElectionType = electionType;

            return View();
        }

        [HttpGet]
        public IActionResult GetCandidatesByPosition(string positionName)
        {
            try
            {
                if (string.IsNullOrEmpty(positionName))
                {
                    return Json(new { success = false, message = "Invalid position name." });
                }

                var candidates = _context.Candidates
                    .Include(c => c.Voter)
                    .Include(c => c.Position)
                    .Where(c => c.Position != null && c.Position.PositionName.ToLower() == positionName.ToLower()) // Case insensitive
                    .Select(c => new
                    {
                        c.CandidateId,
                        FullName = c.Voter != null ? $"{c.Voter.FirstName} {c.Voter.LastName}" : "Unknown",
                        Position = c.Position != null ? c.Position.PositionName : "Unknown",
                        c.PartyId,
                    })
                    .ToList();

                if (candidates.Count == 0)
                {
                    return Json(new { success = false, message = "No candidates found for this position." });
                }

                return Json(new { success = true, data = candidates });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching candidates: {ex.Message}");
                return Json(new { success = false, message = "An error occurred while fetching candidates." });
            }
        }

        [HttpPost]
        public IActionResult SubmitVote([FromBody] VoteRequest voteRequest)
        {
            try
            {
                if (voteRequest == null || voteRequest.VoterId == 0)
                {
                    return Json(new { success = false, message = "Invalid vote data." });
                }

                // Convert the list of senators to a comma-separated string
                string senatorsCsv = voteRequest.Senators != null ? string.Join(",", voteRequest.Senators) : null;

                // Insert vote into the Vote table
                var newVote = new Vote
                {
                    VoterId = voteRequest.VoterId,
                    PresidentId = voteRequest.PresidentId,
                    VicePresidentId = voteRequest.VicePresidentId,
                    DistrictRepId = voteRequest.DistrictRepId,
                    PartyListRepId = voteRequest.PartyListRepId,
                    Senators = senatorsCsv
                };

                _context.Votes.Add(newVote);
                _context.SaveChanges();

                return Json(new { success = true, message = "Vote submitted successfully!" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error submitting vote: {ex.Message}");
                return Json(new { success = false, message = "An error occurred while submitting the vote." });
            }
        }


        [Authorize(Roles = "Voter")]
        public IActionResult Elections()
        {
            var elections = _context.Elections
                .Select(e => new Election
                {
                    ElectionName = e.ElectionName,
                    ElectionTypes = e.ElectionTypes
                })
                .ToList(); // Ensure the model is List<Election>

            return View(elections);
        }



        [Authorize(Roles = "Voter")]
        public IActionResult Homes()
        {
            // Get the count of voters
            int voterCount = _context.Voters.Count();

            // Get the count of candidates
            int candidateCount = _context.Candidates.Count();

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

            // Pass the counts and candidate list to the view
            ViewBag.VoterCount = voterCount;
            ViewBag.CandidateCount = candidateCount;
            return View(candidates);
        }

        [Authorize(Roles = "Voter")]
        public IActionResult Candidate()
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

            return View(candidates);
        }
        [Authorize(Roles = "Voter")]
        public IActionResult Profile()
        {
            // Get the VoterId from the claims
            var voterIdClaim = User.FindFirst("VoterId");
            if (voterIdClaim == null)
            {
                return Unauthorized(); // Handle unauthorized access
            }

            int voterId = int.Parse(voterIdClaim.Value);

            // Fetch the voter profile from the database
            var voter = _context.Voters
                .Include(v => v.Account) // Include related Account data
                .FirstOrDefault(v => v.VoterId == voterId);

            if (voter == null)
            {
                return NotFound("Voter profile not found.");
            }

            // Pass the voter data to the view
            return View(voter);
        }
       [HttpPost]
        public IActionResult UpdateProfile([FromBody] Voter updatedVoter)
        {
            if (updatedVoter == null)
            {
                return Json(new { success = false, message = "Invalid data received." });
            }

            var voter = _context.Voters.FirstOrDefault(v => v.VoterId == updatedVoter.VoterId);
            if (voter == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            try
            {
                // Update voter details
                voter.FirstName = updatedVoter.FirstName;
                voter.LastName = updatedVoter.LastName;
                voter.Phone = updatedVoter.Phone;
                voter.Address = updatedVoter.Address;
                voter.Birthdate = updatedVoter.Birthdate; // Ensure birthdate is not null
                voter.Gender = updatedVoter.Gender; // Ensure gender is updated


                // Check if email needs to be updated in the Account table
                // var account = _context.Account.FirstOrDefault(a => a.AccountId == voter.AccountId);
                if (voter.Account != null)
                {
                    voter.Account.Email = updatedVoter.Email;
                }
                _context.SaveChanges();

                return Json(new { success = true, message = "Profile updated successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error updating profile: " + ex.Message });
            }
        }

    }
}