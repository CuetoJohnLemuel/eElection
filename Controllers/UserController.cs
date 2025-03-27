using eElection.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eElection.Data;
using eElection.Services;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;  // Add this namespace

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
        public IActionResult Result(int electionId)
        {
            // Step 1: Retrieve the election and its ElectionType(s)
            var election = _context.Elections
                .FirstOrDefault(e => e.ElectionId == electionId);
            if (election == null)
            {
                return NotFound("Election not found.");
            }

            // Step 2: Split ElectionTypes to handle multiple types
            var electionTypeNames = election.ElectionTypes
                .Split(',')
                .Select(et => et.Trim()) // Trim spaces
                .ToList();

            // Step 3: Get all PositionId values from ElectionTypePositions table that match any ElectionType
            var positionIds = _context.ElectionTypePositions
                .Where(etp => electionTypeNames.Contains(etp.ElectionTypeName))
                .Select(etp => etp.PositionId)
                .ToList();

            // Step 4: Retrieve positions from Positions table
            var positions = _context.Positions
                .Where(p => positionIds.Contains(p.PositionId))
                .ToList();

            // Step 5: Get candidates with their vote counts
            var candidateVotes = _context.Candidates
                .Where(c => c.ElectionId == electionId && positionIds.Contains(c.PositionId))
                .Select(c => new
                {
                    PositionId = c.PositionId,
                    CandidateName = _context.Voters
                        .Where(v => v.VoterId == c.VoterId)
                        .Select(v => $"{v.FirstName} {v.LastName}")
                        .FirstOrDefault(),
                   VoteCount = _context.Votes
                .Count(v => v.CandidateId == c.CandidateId && v.ElectionId == electionId) // ✅ Filter by ElectionId
        })
                .ToList();

            // Step 6: Pass data to the view
            var viewModel = new ElectionResultViewModel
            {
                Election = election,
                Positions = positions,
                Candidates = candidateVotes
                    .GroupBy(c => c.PositionId)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(c => $"{c.CandidateName} ({c.VoteCount} Votes)").ToList()
                    )
            };

            return View(viewModel);
        }




        [HttpGet]
        public IActionResult GetPositions()
        {
            var positions = _context.Positions
                .Where(p => p.PositionName != null) // Ensure no null PositionNames
                .Select(p => new { p.PositionId, PositionName = p.PositionName.Trim() }) // Trim spaces
                .ToList();

            if (positions.Count == 0)
            {
                return Json(new { success = false, message = "No positions found." });
            }

            // Log the positions for debugging
            Console.WriteLine("Positions returned from database:");
            foreach (var position in positions)
            {
                Console.WriteLine($"PositionId: {position.PositionId}, PositionName: {position.PositionName}");
            }

            return Json(new { success = true, data = positions });
        }

        [Authorize(Roles = "Voter")]
        public IActionResult Ballot(int? electionId)
        {
            if (electionId == null)
            {
                return RedirectToAction("Index"); // Redirect if electionId is missing
            }
            var voterIdClaim = User.Claims.FirstOrDefault(c => c.Type == "VoterId");
            if (voterIdClaim == null)
            {
                return Unauthorized("Voter ID not found.");
            }
            int voterId = int.Parse(voterIdClaim.Value);
            // Check if the voter already voted in this election
            bool hasVoted = _context.Votes
                .Any(v => v.VoterId == voterId && v.ElectionId == electionId);

            if (hasVoted)
            {
                TempData["ErrorMessage"] = "You have already voted in this election.";
                return RedirectToAction("Elections"); // Redirect them back to the listing page
            }
            var electionData = _context.Elections
                .Where(e => e.ElectionId == electionId)
                .Select(e => new
                {
                    e.ElectionId,
                    e.ElectionTypes
                })
                .FirstOrDefault();

            if (electionData == null)
            {
                return NotFound("Election not found.");
            }

            var electionTypeNames = electionData.ElectionTypes.Split(',')
                .Select(et => et.Trim())
                .ToList();

            var positions = _context.ElectionTypePositions
                .Where(etp => electionTypeNames.Contains(etp.ElectionTypeName))
                .Join(_context.Positions,
                      etp => etp.PositionId,
                      p => p.PositionId,
                      (etp, p) => new
                      {
                          p.PositionId,
                          p.PositionName,
                          p.MaxCandidates // Assuming MaxCandidates column exists
                      })
                .ToList();

            ViewBag.ElectionId = electionData.ElectionId;
            ViewBag.ElectionTypes = electionData.ElectionTypes ?? "";
            ViewBag.Positions = positions;

            return View();
        }



        [HttpGet]
        public JsonResult GetCandidates(string positionName)
        {
            // Retrieve the PositionId based on the provided PositionName
            var positionId = _context.Positions
                .Where(p => p.PositionName == positionName)
                .Select(p => p.PositionId)
                .FirstOrDefault();

            // If PositionId is not found, return an empty list
            if (positionId == 0)
            {
                return Json(new List<object>());
            }

            // Retrieve candidates and join with Voters table to get full names
            var candidates = _context.Candidates
                .Where(c => c.PositionId == positionId)
                .Join(_context.Voters,
                    candidate => candidate.VoterId,  // Foreign key in Candidates
                    voter => voter.VoterId,          // Primary key in Voters
                    (candidate, voter) => new
                    {
                        CandidateId = candidate.CandidateId,
                        FullName = voter.FirstName + " " + voter.LastName // Concatenate First and Last Name
                    })
                .ToList();

            return Json(candidates);
        }



        [HttpGet]
        public IActionResult GetCandidatesByPosition(string positionName, int electionId)
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
                    .Where(c => c.Position != null
                        && c.Position.PositionName.ToLower() == positionName.ToLower()
                        && c.ElectionId == electionId) // ✅ Filter by ElectionId
                    .Select(c => new
                    {
                        c.CandidateId,
                        FullName = c.Voter != null ? $"{c.Voter.FirstName} {c.Voter.LastName}" : "Unknown",
                        PositionId = c.PositionId
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
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }


        [HttpPost]
        public IActionResult SubmitVote([FromBody] List<VoteRequest> votes)
        {
            try
            {
                var voterIdClaim = User.Claims.FirstOrDefault(c => c.Type == "VoterId");
                if (voterIdClaim == null)
                {
                    return Json(new { success = false, message = "Unauthorized: Voter ID not found." });
                }

                int voterId = int.Parse(voterIdClaim.Value);

                if (votes == null || votes.Count == 0)
                {
                    return Json(new { success = false, message = "No votes received." });
                }

                var groupedVotes = votes.GroupBy(v => v.PositionId)
                                        .ToDictionary(g => g.Key, g => g.Count());

                var positionLimits = _context.Positions
                    .Where(p => groupedVotes.Keys.Contains(p.PositionId))
                    .ToDictionary(p => p.PositionId, p => p.MaxCandidates);

                foreach (var kvp in groupedVotes)
                {
                    int positionId = kvp.Key;
                    int selectedCount = kvp.Value;

                    if (positionLimits.TryGetValue(positionId, out int maxAllowed) && selectedCount > maxAllowed)
                    {
                        return Json(new { success = false, message = $"You can only select up to {maxAllowed} candidates for this position." });
                    }
                }

                foreach (var vote in votes)
                {
                    var newVote = new Vote
                    {
                        VoterId = voterId,
                        ElectionId = vote.ElectionId,
                        PositionId = vote.PositionId,
                        CandidateId = vote.CandidateId
                    };

                    _context.Votes.Add(newVote);
                }

                _context.SaveChanges();
                return Json(new { success = true, message = "Votes submitted successfully!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error: {ex.Message}" });
            }
        }



        [Authorize(Roles = "Voter")]
        public IActionResult Elections()
        {
            var elections = _context.Elections
                .Select(e => new Election
                {
                    ElectionId = e.ElectionId,
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