using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace movie_app_graduation_api.Models.Data
{
    public class MoviesDbContext : IdentityDbContext<MoviesUser>
    {

        public MoviesDbContext()
        {
        }
        public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
            : base(options)
        {
        }


        public virtual DbSet<Calculator> Calculators { get; set; } = null!;
        public virtual DbSet<Genre> Genres { get; set; } = null!;
        public virtual DbSet<Movie> Movies { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }
        /*   protected override void OnModelCreating(ModelBuilder builder)
           {
            *//*   builder.Entity<IdentityRoleClaim<String>>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
               builder.Entity<IdentityRole>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
               builder.Entity<IdentityUserClaim<String>>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
               builder.Entity<IdentityUserLogin<String>>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
               builder.Entity<IdentityUserRole<String>>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
               builder.Entity<MoviesUser>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
               builder.Entity<IdentityUserToken<String>>().ToTable(nameof(Users), t => t.ExcludeFromMigrations());
   *//*


               base.OnModelCreating(builder);

           }*/

    }

}
