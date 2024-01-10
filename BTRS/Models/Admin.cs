using System.ComponentModel.DataAnnotations;

namespace BTRS.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string password { get; set; }



        
        public ICollection<Trip> Trip { get; set; }

        public ICollection<Bus> bus { get; set; }
    }
}
