using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
    public class Trip
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Must be insert")]
        public int BusNum { get; set; }
        [Required(ErrorMessage = "Must be insert")]
        public string dest { get; set; }
        [Required(ErrorMessage = "Must be insert")]
        public DateTime  StartD { get; set; }
        [Required(ErrorMessage = "Must be insert")]
        public DateTime EndD{ get; set; }


        public ICollection<Bus> bus { get; set; }



        [ForeignKey("FK_Admin_Trip")]
        public Admin admin { get; set; }
     
    }
}
 