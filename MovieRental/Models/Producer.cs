using System.ComponentModel.DataAnnotations;

namespace MovieRental.Models
{
    public class Producer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
