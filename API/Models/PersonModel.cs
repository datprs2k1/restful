using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class PersonModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        [Range(0, 150)]
        public int Age { get; set; }
    }
}
