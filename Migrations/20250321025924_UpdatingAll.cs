using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace eElection.Migrations
{
    /// <inheritdoc />
    public partial class UpdatingAll : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    AnnouncementId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.AnnouncementId);
                });

            migrationBuilder.CreateTable(
                name: "Elections",
                columns: table => new
                {
                    ElectionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ElectionTypes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elections", x => x.ElectionId);
                });

            migrationBuilder.CreateTable(
                name: "ElectionTypes",
                columns: table => new
                {
                    ElectionTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionTypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionTypes", x => x.ElectionTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Parties",
                columns: table => new
                {
                    PartyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Leader = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FoundedYear = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parties", x => x.PartyId);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    PositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PositionName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.PositionId);
                });

            migrationBuilder.CreateTable(
                name: "Voters",
                columns: table => new
                {
                    VoterId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Birthdate = table.Column<DateOnly>(type: "date", nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ProfilePhoto = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RejectionReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Voters", x => x.VoterId);
                });

            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    VoteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Votes", x => x.VoteId);
                });

            migrationBuilder.CreateTable(
                name: "ElectionTypePositions",
                columns: table => new
                {
                    ElectionTypePositionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionTypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectionTypePositions", x => x.ElectionTypePositionId);
                    table.ForeignKey(
                        name: "FK_ElectionTypePositions_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    AccountId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmationToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Account_Voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Voters",
                        principalColumn: "VoterId");
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                columns: table => new
                {
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ElectionId = table.Column<int>(type: "int", nullable: false),
                    VoterId = table.Column<int>(type: "int", nullable: false),
                    PartyId = table.Column<int>(type: "int", nullable: false),
                    PositionId = table.Column<int>(type: "int", nullable: false),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.CandidateId);
                    table.ForeignKey(
                        name: "FK_Candidates_Elections_ElectionId",
                        column: x => x.ElectionId,
                        principalTable: "Elections",
                        principalColumn: "ElectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidates_Parties_PartyId",
                        column: x => x.PartyId,
                        principalTable: "Parties",
                        principalColumn: "PartyId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidates_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "PositionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Candidates_Voters_VoterId",
                        column: x => x.VoterId,
                        principalTable: "Voters",
                        principalColumn: "VoterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "VoteDetails",
                columns: table => new
                {
                    VoteDetailId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VoteId = table.Column<int>(type: "int", nullable: false),
                    CandidateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteDetails", x => x.VoteDetailId);
                    table.ForeignKey(
                        name: "FK_VoteDetails_Votes_VoteId",
                        column: x => x.VoteId,
                        principalTable: "Votes",
                        principalColumn: "VoteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Announcements",
                columns: new[] { "AnnouncementId", "CreatedAt", "Description", "Status", "Title" },
                values: new object[,]
                {
                    { 2, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Don't forget to vote on the upcoming election day!", "Published", "Election Day Reminder" },
                    { 3, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Voter registration has been extended until next Friday.", "Published", "Registration Deadline Extended" },
                    { 4, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Official election results will be announced this weekend.", "Scheduled", "Election Results Announcement" }
                });

            migrationBuilder.InsertData(
                table: "Elections",
                columns: new[] { "ElectionId", "CreatedAt", "ElectionName", "ElectionTypes", "EndDate", "StartDate", "Status" },
                values: new object[,]
                {
                    { 10, new DateTime(2025, 3, 14, 0, 40, 44, 0, DateTimeKind.Unspecified), "2025 National Election", "National Elections", new DateTime(2025, 3, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { 11, new DateTime(2025, 3, 14, 0, 41, 13, 0, DateTimeKind.Unspecified), "2025 Midterm Election", "Midterm Elections", new DateTime(2025, 3, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" },
                    { 13, new DateTime(2025, 3, 14, 7, 59, 5, 0, DateTimeKind.Unspecified), "dsad", "Midterm Elections,Regional Elections", new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 3, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Active" }
                });

            migrationBuilder.InsertData(
                table: "Parties",
                columns: new[] { "PartyId", "CreatedAt", "FoundedYear", "Leader", "PartyName" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 6, 2, 1, 29, 0, DateTimeKind.Unspecified), 2001, "Manny Villar", "NP" },
                    { 2, new DateTime(2025, 3, 6, 2, 1, 54, 0, DateTimeKind.Unspecified), 2018, "Bongbong Marcos", "PFP" },
                    { 4, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 1982, "Aquilino \"Koko\" Pimentel III", "PDP–Laban" },
                    { 5, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 1907, "Manny Villar", "Nacionalista Party" },
                    { 6, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 1946, "Francis Pangilinan", "Liberal Party" },
                    { 7, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 1992, "Lito Lapid", "Nationalist People’s Coalition" },
                    { 8, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 1991, "Gloria Macapagal Arroyo", "Lakas–CMD" },
                    { 9, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 2012, "Jejomar Binay", "United Nationalist Alliance (UNA)" },
                    { 10, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 1997, "Ernest Ramel", "Aksyon Demokratiko" },
                    { 11, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 2009, "Richard Gordon", "Bagumbayan-VNP" },
                    { 12, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 2018, "Ferdinand Marcos Jr.", "Partido Federal ng Pilipinas" },
                    { 13, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 1991, "Miriam Defensor Santiago (deceased)", "People’s Reform Party" },
                    { 14, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 2009, "Neri Colmenares", "Makabayan Bloc" },
                    { 15, new DateTime(2025, 3, 12, 13, 32, 48, 0, DateTimeKind.Unspecified), 1991, "Joseph Estrada", "Partido ng Masang Pilipino (PMP)" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "PositionId", "CreatedAt", "PositionName" },
                values: new object[,]
                {
                    { 6, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "President" },
                    { 7, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Vice President" },
                    { 8, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Senators" },
                    { 9, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "District Representatives" },
                    { 10, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Party-List Representatives" },
                    { 11, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Regional Governor" },
                    { 12, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Regional Vice Governor" },
                    { 13, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Regional Assembly Members" },
                    { 14, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Governor" },
                    { 15, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Vice Governor" },
                    { 16, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Sangguniang Panlalawigan Members" },
                    { 17, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "City/Municipal Mayor" },
                    { 18, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "City/Municipal Vice Mayor" },
                    { 19, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Sangguniang Panlungsod/Bayan Members" },
                    { 20, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Barangay Captain (Punong Barangay)" },
                    { 21, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Sangguniang Barangay Members" },
                    { 22, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Sangguniang Kabataan (SK) Chairman" },
                    { 23, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Sangguniang Kabataan (SK) Members" },
                    { 24, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Councilors for Indigenous Peoples (IPs)" },
                    { 25, new DateTime(2025, 3, 15, 2, 38, 39, 0, DateTimeKind.Unspecified), "Councilors for Sectoral Representatives" }
                });

            migrationBuilder.InsertData(
                table: "Voters",
                columns: new[] { "VoterId", "Address", "Birthdate", "CreatedAt", "Email", "FirstName", "Gender", "LastName", "Phone", "ProfilePhoto", "RejectionReason", "Status" },
                values: new object[,]
                {
                    { 4, "", new DateOnly(1, 1, 1), new DateTime(2025, 3, 8, 3, 49, 49, 0, DateTimeKind.Unspecified), "admin@gmail.com", "", "", "", "", "", null, "" },
                    { 6, "Parang", new DateOnly(1, 1, 1), new DateTime(2025, 3, 10, 7, 33, 19, 0, DateTimeKind.Unspecified), "lemuelcueto21@gmail.com", "John Lemuel", "", "Cueto", "09123456789", "", null, "" },
                    { 22, "Tibag", new DateOnly(2025, 3, 21), new DateTime(2025, 3, 14, 0, 58, 0, 0, DateTimeKind.Unspecified), "cueto.johnlemuel.j@gmail.com", "Jandel", "Male", "Escaleraa", "09123456789", "", null, "Pending" },
                    { 23, "Lazareto", new DateOnly(2025, 3, 14), new DateTime(2025, 3, 14, 0, 59, 44, 0, DateTimeKind.Unspecified), "johnlemuelcueto.olloka@gmail.com", "King", "Male", "Pacheco", "09123456789", "", null, "Pending" },
                    { 24, "Victoria", new DateOnly(2025, 3, 15), new DateTime(2025, 3, 15, 2, 53, 19, 0, DateTimeKind.Unspecified), "deployhaha@gmail.com", "Quinee", "Female", "Deguzman", "09123456789", "", null, "Pending" },
                    { 25, "Xevera", new DateOnly(2025, 3, 15), new DateTime(2025, 3, 15, 2, 54, 58, 0, DateTimeKind.Unspecified), "jandeleido@gmail.com", "Jean Claude", "Male", "Manalo", "09123456789", "", null, "Pending" }
                });

            migrationBuilder.InsertData(
                table: "Account",
                columns: new[] { "AccountId", "CreatedAt", "Email", "EmailConfirmationToken", "IsEmailConfirmed", "Password", "Username", "VoterId" },
                values: new object[,]
                {
                    { 5, new DateTime(2025, 3, 8, 3, 49, 50, 0, DateTimeKind.Unspecified), "admin@gmail.com", "9f88c35b-f85f-4add-9023-05fdf5411e24", false, "AQAAAAIAAYagAAAAEBsg6iR+zJKFGlZtsAI2bTE68Ji5i2iemqzz1YktMbkp5EgvTbOTSELClp3e8Gq3kg==", "admin", 4 },
                    { 7, new DateTime(2025, 3, 10, 7, 33, 20, 0, DateTimeKind.Unspecified), "lemuelcueto21@gmail.com", "c8919e1a-2110-4757-926f-daa3035cacb5", true, "AQAAAAIAAYagAAAAEB64volyPMvo1GSwisFZZsYtvWpZngvA7soffvSSMuKuiobkLOwAY6Zgk9cldGj3aA==", "lemuel", 6 },
                    { 9, new DateTime(2025, 3, 14, 0, 58, 1, 0, DateTimeKind.Unspecified), "cueto.johnlemuel.j@gmail.com", "", true, "AQAAAAIAAYagAAAAEAkp4GQjt/Ey18RGW696gSkotd8CHsjy6Zv5VfuMSFDI435GMvEczn3zf9zadJP0Jw==", "Jandel Escalera", 22 },
                    { 10, new DateTime(2025, 3, 14, 0, 59, 45, 0, DateTimeKind.Unspecified), "johnlemuelcueto.olloka@gmail.com", "", true, "AQAAAAIAAYagAAAAEP5sSrBrZV03W+AbSn1NyMjVKQfqEY7xkUT9Rz/TRraD/x5CXa3TYlVToCKkFqqmwg==", "King Pacheco", 23 },
                    { 11, new DateTime(2025, 3, 15, 2, 53, 21, 0, DateTimeKind.Unspecified), "deployhaha@gmail.com", "827c9562-71ba-4e14-b557-0d773b4e2a28", true, "AQAAAAIAAYagAAAAEPJM1uM6eC/RJz77uZFFNUoQ5f/1GuYY+g5wYi63JluogLt27u0UWEHWxmvXKC/9tw==", "Quinee", 24 },
                    { 12, new DateTime(2025, 3, 15, 2, 54, 58, 0, DateTimeKind.Unspecified), "jandeleido@gmail.com", "22aee146-620f-416c-b814-884d8e29a4ef", true, "AQAAAAIAAYagAAAAELRRPNDnSUCikTQDkjqQ6gHq9OTEAIoxYz0/AH4M01n6+JFyKU/bgrDVM7aLjYKuIw==", "Claude", 25 }
                });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "CandidateId", "Biography", "CreatedAt", "ElectionId", "PartyId", "PositionId", "VoterId" },
                values: new object[,]
                {
                    { 9, "John Lemuel Cueto is a dedicated leader and public servant with a strong vision for a better and more progressive nation. Born and raised in [Location], he has been deeply committed to serving the people and advocating for meaningful change.\n\nJohn holds a degree in [Field of Study] from [University Name] and has extensive experience in [related profession or public service]. Over the years, he has led numerous initiatives focused on [key achievements such as economic development, social welfare programs, education reforms, or infrastructure projects].\n\nAs a candidate for President, John Lemuel Cueto aims to prioritize good governance, economic growth, quality education, accessible healthcare, and national security. He believes in transparency, accountability, and inclusive leadership, ensuring that every citizen has a voice in shaping the nation's future.\n\nWith a strong track record of leadership, integrity, and dedication, John Lemuel Cueto is ready to lead the country toward progress and prosperity.\n\nCampaign Slogan: \"Together, We Build a Stronger Nation!\"", new DateTime(2025, 3, 14, 0, 57, 5, 0, DateTimeKind.Unspecified), 10, 2, 6, 6 },
                    { 10, "Jandel Escalera is a dedicated leader and public servant with a strong vision for a better and more progressive nation. Born and raised in [Location], he has been deeply committed to serving the people and advocating for meaningful change.\n\nJandel holds a degree in [Field of Study] from [University Name] and has extensive experience in [related profession or public service]. Over the years, he has led numerous initiatives focused on [key achievements such as economic development, social welfare programs, education reforms, or infrastructure projects].\n\nAs a candidate for President, Jandel Escalera aims to prioritize good governance, economic growth, quality education, accessible healthcare, and national security. He believes in transparency, accountability, and inclusive leadership, ensuring that every citizen has a voice in shaping the nation's future.\n\nWith a strong track record of leadership, integrity, and dedication, Jandel Escalera is ready to lead the country toward progress and prosperity.\n\nCampaign Slogan: \"Together, We Build a Stronger Nation!\"", new DateTime(2025, 3, 14, 2, 25, 54, 0, DateTimeKind.Unspecified), 10, 6, 7, 22 },
                    { 11, "King Pacheco is a dedicated leader and public servant with a strong vision for a better and more progressive nation. Born and raised in [Location], he has been deeply committed to serving the people and advocating for meaningful change.\n\nKing holds a degree in [Field of Study] from [University Name] and has extensive experience in [related profession or public service]. Over the years, he has led numerous initiatives focused on [key achievements such as economic development, social welfare programs, education reforms, or infrastructure projects].\n\nAs a candidate for President, King Pacheco aims to prioritize good governance, economic growth, quality education, accessible healthcare, and national security. He believes in transparency, accountability, and inclusive leadership, ensuring that every citizen has a voice in shaping the nation's future.\n\nWith a strong track record of leadership, integrity, and dedication, King Pacheco is ready to lead the country toward progress and prosperity.\n\nCampaign Slogan: \"Together, We Build a Stronger Nation!\"", new DateTime(2025, 3, 14, 6, 46, 58, 0, DateTimeKind.Unspecified), 10, 13, 8, 23 },
                    { 12, "q", new DateTime(2025, 3, 15, 3, 0, 59, 0, DateTimeKind.Unspecified), 10, 5, 9, 24 },
                    { 13, "w", new DateTime(2025, 3, 15, 3, 1, 20, 0, DateTimeKind.Unspecified), 10, 5, 10, 25 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Account_VoterId",
                table: "Account",
                column: "VoterId",
                unique: true,
                filter: "[VoterId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_ElectionId",
                table: "Candidates",
                column: "ElectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PartyId",
                table: "Candidates",
                column: "PartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PositionId",
                table: "Candidates",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_VoterId",
                table: "Candidates",
                column: "VoterId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectionTypePositions_PositionId",
                table: "ElectionTypePositions",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteDetails_VoteId",
                table: "VoteDetails",
                column: "VoteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "Candidates");

            migrationBuilder.DropTable(
                name: "ElectionTypePositions");

            migrationBuilder.DropTable(
                name: "ElectionTypes");

            migrationBuilder.DropTable(
                name: "VoteDetails");

            migrationBuilder.DropTable(
                name: "Elections");

            migrationBuilder.DropTable(
                name: "Parties");

            migrationBuilder.DropTable(
                name: "Voters");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Votes");
        }
    }
}
