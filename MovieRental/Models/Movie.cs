using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MovieRental.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Movie Name")]
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }

        [Display(Name = "Producer Name")]
        [Required]
        public int ProducerId { get; set; }
        [ForeignKey("ProducerId")]
        public Producer Producer { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        [ForeignKey("GenreId")]
        public Genre Genre { get; set; }

        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
    }
}
