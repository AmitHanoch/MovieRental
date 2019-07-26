using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieRental.Models
{
    public class User
    {
        [Key]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        // RoleId: 1-manager, 2-StoreOwner 
        [Required]
        public int RoleId { get; set; }
    }
}
