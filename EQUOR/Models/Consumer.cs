using System.ComponentModel.DataAnnotations;

namespace EQUOR.Models
{
	public class Consumer
	{
        [Key]
        public int IdConsumer { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
