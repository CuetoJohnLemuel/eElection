using eElection.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eElection.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
        public DbSet<Account> Account { get; set; }
        public DbSet<Voter> Voters { get; set; }
        public DbSet<Election> Elections { get; set; }
        public DbSet<Party> Parties { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<Vote> Votes { get; set; }
        public DbSet<VoteDetail> VoteDetails { get; set; }
        public DbSet<ElectionType> ElectionTypes { get; set; }
        public DbSet<ElectionTypePositions> ElectionTypePositions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure ElectionTypePositions Foreign Key Relationship
            modelBuilder.Entity<ElectionTypePositions>()
                .HasOne(e => e.Position)  // ElectionTypePositions has one Position
                .WithMany(p => p.ElectionTypePositions)  // Position has many ElectionTypePositions
                .HasForeignKey(e => e.PositionId)  // Foreign key in ElectionTypePositions
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete if a Position is deleted

            // Configure Position
            modelBuilder.Entity<Position>()
                .HasMany(p => p.ElectionTypePositions)
                .WithOne(e => e.Position)
                .HasForeignKey(e => e.PositionId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete for consistency

            //modelBuilder.Entity<ElectionTypePositions>()
            //.HasKey(etp => new { etp.ElectionTypeId, etp.PositionId });

            //modelBuilder.Entity<ElectionTypePositions>()
            //   .HasOne(etp => etp.ElectionType)
            //   .WithMany(et => et.ElectionTypePositions)
            //   .HasForeignKey(etp => etp.ElectionTypeId);

            //modelBuilder.Entity<ElectionTypePositions>()
            //    .HasOne(etp => etp.Position)
            //    .WithMany(p => p.ElectionTypePositions)
            //    .HasForeignKey(etp => etp.PositionId);

            modelBuilder.Entity<Candidate>()
            .HasOne(c => c.Election)
            .WithMany(e => e.Candidates)
            .HasForeignKey(c => c.ElectionId);

            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Voter)
                .WithMany(v => v.Candidates)
                .HasForeignKey(c => c.VoterId);

            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Party)
                .WithMany(p => p.Candidates)
                .HasForeignKey(c => c.PartyId);

            modelBuilder.Entity<Candidate>()
                .HasOne(c => c.Position)
                .WithMany(p => p.Candidates)
                .HasForeignKey(c => c.PositionId);

            // Optional: Define relationships explicitly
            modelBuilder.Entity<VoteDetail>()
                .HasOne(vd => vd.Vote)
                .WithMany()
                .HasForeignKey(vd => vd.VoteId)
                .OnDelete(DeleteBehavior.Cascade);


            base.OnModelCreating(modelBuilder);

            //var hasher = new PasswordHasher<Account>();

            modelBuilder.Entity<Account>().HasData(
                 new Account
                 {
                     AccountId = 5,
                     Username = "admin",
                     Email = "admin@gmail.com",
                     Password = "AQAAAAIAAYagAAAAEBsg6iR+zJKFGlZtsAI2bTE68Ji5i2iemqzz1YktMbkp5EgvTbOTSELClp3e8Gq3kg==", // 🔒 Pre-hashed
                     EmailConfirmationToken = "9f88c35b-f85f-4add-9023-05fdf5411e24",
                     IsEmailConfirmed = false,
                     CreatedAt = new DateTime(2025, 3, 8, 3, 49, 50),
                     VoterId = 4
                 },
                 new Account
                 {
                     AccountId = 7,
                     Username = "lemuel",
                     Email = "lemuelcueto21@gmail.com",
                     Password = "AQAAAAIAAYagAAAAEB64volyPMvo1GSwisFZZsYtvWpZngvA7soffvSSMuKuiobkLOwAY6Zgk9cldGj3aA==", // 🔒 Pre-hashed
                     EmailConfirmationToken = "c8919e1a-2110-4757-926f-daa3035cacb5",
                     IsEmailConfirmed = true,
                     CreatedAt = new DateTime(2025, 3, 10, 7, 33, 20),
                     VoterId = 6
                 },
                 new Account
                 {
                     AccountId = 9,
                     Username = "Jandel Escalera",
                     Email = "cueto.johnlemuel.j@gmail.com",
                     Password = "AQAAAAIAAYagAAAAEAkp4GQjt/Ey18RGW696gSkotd8CHsjy6Zv5VfuMSFDI435GMvEczn3zf9zadJP0Jw==", // 🔒 Pre-hashed
                     EmailConfirmationToken = "",
                     IsEmailConfirmed = true,
                     CreatedAt = new DateTime(2025, 3, 14, 0, 58, 1),
                     VoterId = 22
                 },
                 new Account
                 {
                     AccountId = 10,
                     Username = "King Pacheco",
                     Email = "johnlemuelcueto.olloka@gmail.com",
                     Password = "AQAAAAIAAYagAAAAEP5sSrBrZV03W+AbSn1NyMjVKQfqEY7xkUT9Rz/TRraD/x5CXa3TYlVToCKkFqqmwg==", // 🔒 Pre-hashed
                     EmailConfirmationToken = "",
                     IsEmailConfirmed = true,
                     CreatedAt = new DateTime(2025, 3, 14, 0, 59, 45),
                     VoterId = 23
                 },
                 new Account
                 {
                     AccountId = 11,
                     Username = "Quinee",
                     Email = "deployhaha@gmail.com",
                     Password = "AQAAAAIAAYagAAAAEPJM1uM6eC/RJz77uZFFNUoQ5f/1GuYY+g5wYi63JluogLt27u0UWEHWxmvXKC/9tw==", // 🔒 Pre-hashed
                     EmailConfirmationToken = "827c9562-71ba-4e14-b557-0d773b4e2a28",
                     IsEmailConfirmed = true,
                     CreatedAt = new DateTime(2025, 3, 15, 2, 53, 21),
                     VoterId = 24
                 },
                 new Account
                 {
                     AccountId = 12,
                     Username = "Claude",
                     Email = "jandeleido@gmail.com",
                     Password = "AQAAAAIAAYagAAAAELRRPNDnSUCikTQDkjqQ6gHq9OTEAIoxYz0/AH4M01n6+JFyKU/bgrDVM7aLjYKuIw==", // 🔒 Pre-hashed
                     EmailConfirmationToken = "22aee146-620f-416c-b814-884d8e29a4ef",
                     IsEmailConfirmed = true,
                     CreatedAt = new DateTime(2025, 3, 15, 2, 54, 58),
                     VoterId = 25
                 }
             );



            modelBuilder.Entity<Announcement>().HasData(
                new Announcement
                {
                    AnnouncementId = 2,
                    Title = "Election Day Reminder",
                    Description = "Don't forget to vote on the upcoming election day!",
                    Status = "Published",
                    CreatedAt = DateTime.Parse("2025-03-15 02:38:39")
                },
                new Announcement
                {
                    AnnouncementId = 3,
                    Title = "Registration Deadline Extended",
                    Description = "Voter registration has been extended until next Friday.",
                    Status = "Published",
                    CreatedAt = DateTime.Parse("2025-03-15 02:38:39")
                },
                new Announcement
                {
                    AnnouncementId = 4,
                    Title = "Election Results Announcement",
                    Description = "Official election results will be announced this weekend.",
                    Status = "Scheduled",
                    CreatedAt = DateTime.Parse("2025-03-15 02:38:39")
                }
            );


            modelBuilder.Entity<Election>().HasData(
                  new Election
                  {
                      ElectionId = 10,
                      ElectionName = "2025 National Election",
                      ElectionTypes = "National Elections",
                      StartDate = DateTime.Parse("2025-03-15 00:00:00"),
                      EndDate = DateTime.Parse("2025-03-28 00:00:00"),
                      Status = "Active",
                      CreatedAt = DateTime.Parse("2025-03-14 00:40:44")
                  },
                  new Election
                  {
                      ElectionId = 11,
                      ElectionName = "2025 Midterm Election",
                      ElectionTypes = "Midterm Elections",
                      StartDate = DateTime.Parse("2025-03-14 00:00:00"),
                      EndDate = DateTime.Parse("2025-03-19 00:00:00"),
                      Status = "Active",
                      CreatedAt = DateTime.Parse("2025-03-14 00:41:13")
                  },
                  new Election
                  {
                      ElectionId = 13,
                      ElectionName = "dsad",
                      ElectionTypes = "Midterm Elections,Regional Elections",
                      StartDate = DateTime.Parse("2025-03-11 00:00:00"),
                      EndDate = DateTime.Parse("2025-03-20 00:00:00"),
                      Status = "Active",
                      CreatedAt = DateTime.Parse("2025-03-14 07:59:05")
                  }
              );

            modelBuilder.Entity<Party>().HasData(
     new Party
     {
         PartyId = 1,
         PartyName = "NP",
         Leader = "Manny Villar",
         FoundedYear = 2001,
         CreatedAt = DateTime.Parse("2025-03-06 02:01:29")
     },
     new Party
     {
         PartyId = 2,
         PartyName = "PFP",
         Leader = "Bongbong Marcos",
         FoundedYear = 2018,
         CreatedAt = DateTime.Parse("2025-03-06 02:01:54")
     },
     new Party
     {
         PartyId = 4,
         PartyName = "PDP–Laban",
         Leader = "Aquilino \"Koko\" Pimentel III",
         FoundedYear = 1982,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 5,
         PartyName = "Nacionalista Party",
         Leader = "Manny Villar",
         FoundedYear = 1907,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 6,
         PartyName = "Liberal Party",
         Leader = "Francis Pangilinan",
         FoundedYear = 1946,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 7,
         PartyName = "Nationalist People’s Coalition",
         Leader = "Lito Lapid",
         FoundedYear = 1992,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 8,
         PartyName = "Lakas–CMD",
         Leader = "Gloria Macapagal Arroyo",
         FoundedYear = 1991,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 9,
         PartyName = "United Nationalist Alliance (UNA)",
         Leader = "Jejomar Binay",
         FoundedYear = 2012,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 10,
         PartyName = "Aksyon Demokratiko",
         Leader = "Ernest Ramel",
         FoundedYear = 1997,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 11,
         PartyName = "Bagumbayan-VNP",
         Leader = "Richard Gordon",
         FoundedYear = 2009,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 12,
         PartyName = "Partido Federal ng Pilipinas",
         Leader = "Ferdinand Marcos Jr.",
         FoundedYear = 2018,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 13,
         PartyName = "People’s Reform Party",
         Leader = "Miriam Defensor Santiago (deceased)",
         FoundedYear = 1991,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 14,
         PartyName = "Makabayan Bloc",
         Leader = "Neri Colmenares",
         FoundedYear = 2009,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     },
     new Party
     {
         PartyId = 15,
         PartyName = "Partido ng Masang Pilipino (PMP)",
         Leader = "Joseph Estrada",
         FoundedYear = 1991,
         CreatedAt = DateTime.Parse("2025-03-12 13:32:48")
     }
 );


            modelBuilder.Entity<Position>().HasData(
                 new Position { PositionId = 6, PositionName = "President", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 7, PositionName = "Vice President", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 8, PositionName = "Senators", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 9, PositionName = "District Representatives", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 10, PositionName = "Party-List Representatives", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 11, PositionName = "Regional Governor", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 12, PositionName = "Regional Vice Governor", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 //new Position { PositionId = 13, PositionName = "Regional Assembly Members", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 14, PositionName = "Governor", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 15, PositionName = "Vice Governor", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 16, PositionName = "Sangguniang Panlalawigan Members", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 17, PositionName = "City/Municipal Mayor", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 18, PositionName = "City/Municipal Vice Mayor", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 19, PositionName = "Sangguniang Panlungsod/Bayan Members", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 20, PositionName = "Barangay Captain (Punong Barangay)", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 21, PositionName = "Sangguniang Barangay Members", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 22, PositionName = "Sangguniang Kabataan (SK) Chairman", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 new Position { PositionId = 23, PositionName = "Sangguniang Kabataan (SK) Members", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") }
                 //new Position { PositionId = 24, PositionName = "Councilors for Indigenous Peoples (IPs)", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") },
                 //new Position { PositionId = 25, PositionName = "Councilors for Sectoral Representatives", CreatedAt = DateTime.Parse("2025-03-15 02:38:39") }
             );

            modelBuilder.Entity<Voter>().HasData(
            new Voter
            {
                VoterId = 4,
                FirstName = "",
                LastName = "",
                Email = "admin@gmail.com",
                Phone = "",
                Address = "",
                CreatedAt = DateTime.Parse("2025-03-08 03:49:49"),
                Birthdate = DateOnly.Parse("0001-01-01"),
                ProfilePhoto = "",
                RejectionReason = null,
                Status = "",
                Gender = ""
            },
            new Voter
            {
                VoterId = 6,
                FirstName = "John Lemuel",
                LastName = "Cueto",
                Email = "lemuelcueto21@gmail.com",
                Phone = "09123456789",
                Address = "Parang",
                CreatedAt = DateTime.Parse("2025-03-10 07:33:19"),
                Birthdate = DateOnly.Parse("0001-01-01"),
                ProfilePhoto = "",
                RejectionReason = null,
                Status = "",
                Gender = ""
            },
            new Voter
            {
                VoterId = 22,
                FirstName = "Jandel",
                LastName = "Escaleraa",
                Email = "cueto.johnlemuel.j@gmail.com",
                Phone = "09123456789",
                Address = "Tibag",
                CreatedAt = DateTime.Parse("2025-03-14 00:58:00"),
                Birthdate = DateOnly.Parse("2025-03-21"),
                ProfilePhoto = "",
                RejectionReason = null,
                Status = "Pending",
                Gender = "Male"
            },
            new Voter
            {
                VoterId = 23,
                FirstName = "King",
                LastName = "Pacheco",
                Email = "johnlemuelcueto.olloka@gmail.com",
                Phone = "09123456789",
                Address = "Lazareto",
                CreatedAt = DateTime.Parse("2025-03-14 00:59:44"),
                Birthdate = DateOnly.Parse("2025-03-14"),
                ProfilePhoto = "",
                RejectionReason = null,
                Status = "Pending",
                Gender = "Male"
            },
            new Voter
            {
                VoterId = 24,
                FirstName = "Quinee",
                LastName = "Deguzman",
                Email = "deployhaha@gmail.com",
                Phone = "09123456789",
                Address = "Victoria",
                CreatedAt = DateTime.Parse("2025-03-15 02:53:19"),
                Birthdate = DateOnly.Parse("2025-03-15"),
                ProfilePhoto = "",
                RejectionReason = null,
                Status = "Pending",
                Gender = "Female"
            },
            new Voter
            {
                VoterId = 25,
                FirstName = "Jean Claude",
                LastName = "Manalo",
                Email = "jandeleido@gmail.com",
                Phone = "09123456789",
                Address = "Xevera",
                CreatedAt = DateTime.Parse("2025-03-15 02:54:58"),
                Birthdate = DateOnly.Parse("2025-03-15"),
                ProfilePhoto = "",
                RejectionReason = null,
                Status = "Pending",
                Gender = "Male"
            }
        );
            modelBuilder.Entity<Candidate>().HasData(
                  new Candidate
                  {
                      CandidateId = 9,
                      ElectionId = 10,
                      VoterId = 6,
                      PartyId = 2,
                      PositionId = 6,
                      Biography = "John Lemuel Cueto is a dedicated leader and public servant with a strong vision for a better and more progressive nation. Born and raised in [Location], he has been deeply committed to serving the people and advocating for meaningful change.\n\n" +
                                  "John holds a degree in [Field of Study] from [University Name] and has extensive experience in [related profession or public service]. Over the years, he has led numerous initiatives focused on [key achievements such as economic development, social welfare programs, education reforms, or infrastructure projects].\n\n" +
                                  "As a candidate for President, John Lemuel Cueto aims to prioritize good governance, economic growth, quality education, accessible healthcare, and national security. He believes in transparency, accountability, and inclusive leadership, ensuring that every citizen has a voice in shaping the nation's future.\n\n" +
                                  "With a strong track record of leadership, integrity, and dedication, John Lemuel Cueto is ready to lead the country toward progress and prosperity.\n\n" +
                                  "Campaign Slogan: \"Together, We Build a Stronger Nation!\"",
                      CreatedAt = DateTime.Parse("2025-03-14 00:57:05")
                  },
                  new Candidate
                  {
                      CandidateId = 10,
                      ElectionId = 10,
                      VoterId = 22,
                      PartyId = 6,
                      PositionId = 7,
                      Biography = "Jandel Escalera is a dedicated leader and public servant with a strong vision for a better and more progressive nation. Born and raised in [Location], he has been deeply committed to serving the people and advocating for meaningful change.\n\n" +
                                  "Jandel holds a degree in [Field of Study] from [University Name] and has extensive experience in [related profession or public service]. Over the years, he has led numerous initiatives focused on [key achievements such as economic development, social welfare programs, education reforms, or infrastructure projects].\n\n" +
                                  "As a candidate for President, Jandel Escalera aims to prioritize good governance, economic growth, quality education, accessible healthcare, and national security. He believes in transparency, accountability, and inclusive leadership, ensuring that every citizen has a voice in shaping the nation's future.\n\n" +
                                  "With a strong track record of leadership, integrity, and dedication, Jandel Escalera is ready to lead the country toward progress and prosperity.\n\n" +
                                  "Campaign Slogan: \"Together, We Build a Stronger Nation!\"",
                      CreatedAt = DateTime.Parse("2025-03-14 02:25:54")
                  },
                  new Candidate
                  {
                      CandidateId = 11,
                      ElectionId = 10,
                      VoterId = 23,
                      PartyId = 13,
                      PositionId = 8,
                      Biography = "King Pacheco is a dedicated leader and public servant with a strong vision for a better and more progressive nation. Born and raised in [Location], he has been deeply committed to serving the people and advocating for meaningful change.\n\n" +
                                  "King holds a degree in [Field of Study] from [University Name] and has extensive experience in [related profession or public service]. Over the years, he has led numerous initiatives focused on [key achievements such as economic development, social welfare programs, education reforms, or infrastructure projects].\n\n" +
                                  "As a candidate for President, King Pacheco aims to prioritize good governance, economic growth, quality education, accessible healthcare, and national security. He believes in transparency, accountability, and inclusive leadership, ensuring that every citizen has a voice in shaping the nation's future.\n\n" +
                                  "With a strong track record of leadership, integrity, and dedication, King Pacheco is ready to lead the country toward progress and prosperity.\n\n" +
                                  "Campaign Slogan: \"Together, We Build a Stronger Nation!\"",
                      CreatedAt = DateTime.Parse("2025-03-14 06:46:58")
                  },
                  new Candidate
                  {
                      CandidateId = 12,
                      ElectionId = 10,
                      VoterId = 24,
                      PartyId = 5,
                      PositionId = 9,
                      Biography = "q",
                      CreatedAt = DateTime.Parse("2025-03-15 03:00:59")
                  },
                  new Candidate
                  {
                      CandidateId = 13,
                      ElectionId = 10,
                      VoterId = 25,
                      PartyId = 5,
                      PositionId = 10,
                      Biography = "w",
                      CreatedAt = DateTime.Parse("2025-03-15 03:01:20")
                  }
              );

        }
    }
}
