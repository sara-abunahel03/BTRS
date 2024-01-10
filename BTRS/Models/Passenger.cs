using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    [Index(nameof(Passenger.email),IsUnique =true)]
    public class Passenger
    {
        [Key]
        public int PassengerId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string gender { get; set; }
        [Required]
        public string phoneNumber { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
