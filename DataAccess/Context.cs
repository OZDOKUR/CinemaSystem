using Entities.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete
{
    public class Context : IdentityDbContext<AppUser, AppRole, int>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=12345;database=cinemadb2", new MySqlServerVersion(new Version(8, 0, 11)));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

           

            modelBuilder.Entity<MovieStar>()
                .HasKey(ms => new { ms.MovieId, ms.StarId });

            modelBuilder.Entity<MovieStar>()
                .HasOne(ms => ms.Movie)
                .WithMany(m => m.MovieStars)
                .HasForeignKey(ms => ms.MovieId);

            modelBuilder.Entity<MovieStar>()
                .HasOne(ms => ms.Star)
                .WithMany(s => s.MovieStars)
                .HasForeignKey(ms => ms.StarId);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Category)
                .WithMany(c => c.Movies)
                .HasForeignKey(m => m.CategoryId);


            modelBuilder.Entity<Seance>()
        .HasOne(s => s.MovieTheater)
        .WithMany(mt => mt.Seances) 
        .HasForeignKey(s => s.MovieTheaterId);

            
            modelBuilder.Entity<Seance>()
                .HasOne(s => s.Movie)
                .WithMany(w => w.Seances)
                .HasForeignKey(s => s.MovieId);

      
           

            modelBuilder.Entity<Ticket>()
            .HasOne(t => t.Worker)
            .WithMany(w => w.WorkerTickets)
            .HasForeignKey(t => t.WorkerId)
            .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Movie> Movies { get; set; }
       
        public DbSet<Star> Stars { get; set; }

        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<Seance> Seances { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
     
        public DbSet<MovieStar> MovieStars { get; set; }
        public DbSet<Category> Categories { get; set; }

    
     

    }
}
