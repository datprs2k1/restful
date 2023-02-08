using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class APIEntities : DbContext
    {
        public APIEntities(DbContextOptions options) : base(options) { }

        #region DbSet
        public DbSet<Person> Persons { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(e =>
            {
                e.ToTable("Persons");
                e.HasData(
                    new { Id = 1, Name = "Đạt", Age = 20 },
                    new { Id = 2, Name = "Đạt", Age = 21 }
                    );
            });
        }
    }
}
