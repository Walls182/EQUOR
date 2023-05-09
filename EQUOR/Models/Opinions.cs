using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EQUOR.Models
{
	public class Opinions
	{
        [Key]
        public int IdOpinion { get; set; }
        public DateTime Date { get; set; }
        public int IdConsumer { get; set; }
        public int IdProduct { get; set; }
        public String Favorite { get; set; }


        [ForeignKey("IdConsumer")]
        public Consumer Consumer { get; set; }

        [ForeignKey("IdProduct")]
        public Product Product { get; set; }


    }
}
