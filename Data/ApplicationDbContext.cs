using eElection.Models;
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
