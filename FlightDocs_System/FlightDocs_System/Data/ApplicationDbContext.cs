using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlightDocs_System.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<TypeDocument> TypeDocument { get; set; }
        public DbSet<FlightDocument> FlightDocuments { get; set; }
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            builder.Entity<Flight>()
                .HasIndex(f => f.FlightNo)
                .IsUnique();
            builder.Entity<TypeDocument>()
               .HasIndex(f => f.Name)
               .IsUnique();
            // relationship
            builder.Entity<FlightDocument>()
               .HasOne(fd => fd.Flight)
               .WithMany(f => f.FlightDocuments)
               .HasForeignKey(fd => fd.FlightId);

            builder.Entity<FlightDocument>()
                .HasOne(fd => fd.TypeDocument)
                .WithMany(t => t.FlightDocuments)
                .HasForeignKey(fd => fd.TypeId);

            builder.Entity<FlightDocument>()
                .HasOne(d => d.Document)
                .WithOne(fd => fd.FlightDocument)
                .HasForeignKey<FlightDocument>(fd => fd.DocumentId);


            //SeedRoles(builder);
        }

        private void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole()
                {
                    Name = "Pilot",
                    ConcurrencyStamp = "1",
                    NormalizedName = "Pilot"
                },
                new IdentityRole()
                {
                    Name = "Craw",
                    ConcurrencyStamp = "2",
                    NormalizedName = "Craw"
                }

                );
        }
    }
}
