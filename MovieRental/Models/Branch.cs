using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MovieRental.Models
{
    public class Branch
    {
        [Key]
        public int BranchId { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public double LocationX { get; set; }

        [Required]
        public double LocationY { get; set; }
    }
}
