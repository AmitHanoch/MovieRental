using System.ComponentModel.DataAnnotations;

namespace MovieRental.Models
{
    public class Genre
    {
        [Key]
        public int GenreId { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
