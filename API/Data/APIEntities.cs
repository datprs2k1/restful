using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class APIEntities : IdentityDbContext<User>
    {
        public APIEntities(DbContextOptions options) : base(options) { }

        #region DbSet
        public DbSet<Person> Persons { get; set; }
        public DbSet<Token> Tokens { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Person>(e =>
            {
                e.ToTable("Persons");
                e.HasData(
                    new { Id = 1, Name = "Đạt", Age = 20 },
                    new { Id = 2, Name = "Đạt", Age = 21 }
                    );
            });

            modelBuilder.Entity<Token>(e =>
            {
                e.ToTable("Tokens");
                e.Property(x => x.IsUsed).HasDefaultValue(false);
                e.Property(x => x.IsRevoked).HasDefaultValue(false);
                e.HasOne(x => x.User)
                .WithMany(x => x.Tokens)
                .HasForeignKey(x => x.UserID);
            });
        }
    }
}
