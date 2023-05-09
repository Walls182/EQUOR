using System.ComponentModel.DataAnnotations;

namespace EQUOR.Models
{
	public class Company
	{
        [Key]
        public int IdCompany { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
