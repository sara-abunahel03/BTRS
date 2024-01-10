using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
    public class Bus
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CaptinName { get; set; }
        [Required]
        public int Num_of_S { get; set; }


        
        [ForeignKey("FK_Bus_Trip")]
        public Trip trip { get; set; }

        [ForeignKey("FK_Bus_Admin")]
        public Admin admin { get; set; }








    }
}
