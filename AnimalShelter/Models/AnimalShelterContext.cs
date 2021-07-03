using Microsoft.EntityFrameworkCore;

namespace AnimalShelter.Models
{
  public class AnimalShelterContext : DbContext
  {
    public AnimalShelterContext(DbContextOptions<AnimalShelterContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
      builder.Entity<Animal>()
          .HasData(
              new Animal { AnimalId = 1, Name = "Matilda", Species = "Cat", Family = "unknown", Age = 6, Markings = "Spotted/Orange/White" },
              new Animal { AnimalId = 2, Name = "Maeve", Species = "Dog", Family = "Husky", Age = 19, Markings = "Female" },
              new Animal { AnimalId = 3, Name = "Matilda", Species = "Cat", Family = "unknown", Age = 7, Markings = "Black/White/Red" },
              new Animal { AnimalId = 4, Name = "Pip", Species = "Dog", Family = "Pomeranian", Age = 10, Markings = "Spotted" },
              new Animal { AnimalId = 5, Name = "Chance", Species = "Dog", Family = "Lab", Age = 7, Markings = "Black" }
          );
    }

    public DbSet<Animal> Animals { get; set; }
  }
}