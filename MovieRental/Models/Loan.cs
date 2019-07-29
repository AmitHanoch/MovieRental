using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRental.Models
{
    public class Loan
    {
        [Key, Column(Order = 0)]
        public int MovieId { get; set; }

        [Key, Column(Order = 1)]
        public int CustomerId { get; set; }

        [Key, Column(Order = 2)]
        [Display(Name = "Loan Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime LoanDate { get; set; }

        [Required]
        [Display(Name = "Return Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReturnDate { get; set; }

        [ForeignKey("MovieId")]
        [Display(Name = "Movie Name")]
        public virtual Movie Movie { get; set; }

        [Display(Name = "Customer Id")]
        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }
    }
}
