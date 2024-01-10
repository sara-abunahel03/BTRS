using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BTRS.Models
{
    public class Trip_Passenger
    {
        [Key]
        public int ID {  get; set; }   
        

        [ForeignKey("FK_Passenger")]
        public Passenger Passenger { get; set; }
   



        [ForeignKey("FK_Trip")]
        public Trip trip { get; set; }
      
    }
}
