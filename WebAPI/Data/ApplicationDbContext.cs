using kDg.FileBaseContext.Extensions;
using Microsoft.EntityFrameworkCore;
using WebAPI.Models;

namespace WebAPI.Data;

public class ApplicationDbContext : DbContext
{
    public DbSet<Person> Persons { get; set; }

    /// <inheritdoc />
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    /// <inheritdoc />
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Interest>(interest =>
        {
            interest
                .HasOne<Person>(i => i.Person)
                .WithMany(p => p.Interests)
                .HasForeignKey(i => i.PersonId)
                .IsRequired();
        });

        modelBuilder.Entity<Link>(link =>
        {
            link.HasOne<Interest>(l => l.Interest)
                .WithMany(i => i.Links)
                .HasForeignKey(l => l.InterestId)
                .IsRequired();
            link.HasOne<Person>(l => l.Person)
                .WithMany(p => p.Links)
                .HasForeignKey(l => l.PersonId)
                .IsRequired();
        });

        modelBuilder
            .Entity<Person>()
            .HasData(
                new Person
                {
                    Id = 1,
                    Name = "Björn Agnemo",
                    PhoneNumber = "0703038338"
                }
            );

        modelBuilder
            .Entity<Interest>()
            .HasData(
                new Interest
                {
                    Id = 1,
                    Title = "Play VGM on Piano",
                    Description = "Play Video Game Music from Piano Notes.",
                    PersonId = 1
                }
            );

        modelBuilder
            .Entity<Link>()
            .HasData(
                new Link
                {
                    Id = 1,
                    Title = "Smart Game Piano",
                    LinkURL = new UriBuilder("www.smartgamepiano.com").Uri,
                    InterestId = 1,
                    PersonId = 1
                }
            );
    }
}
