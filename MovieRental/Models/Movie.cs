using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MovieRental.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required]
        public int ProducerId { get; set; }

        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }

        [Required]
        public int GenreId { get; set; }

        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
