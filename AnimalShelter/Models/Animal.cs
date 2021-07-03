using System.ComponentModel.DataAnnotations;

namespace AnimalShelter.Models
{
  public class Animal
  {
    public int AnimalId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Species { get; set; }
    [Required]
    public string Family { get; set; }
    public int Age { get; set; }
    [Required]
    public string Markings { get; set; }

  }
}