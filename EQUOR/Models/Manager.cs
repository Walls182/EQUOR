using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EQUOR.Models
{
	public class Manager
	{
        [Key]
        public int IdManager { get; set; }
        public string Name { get; set; }
		public string Email { get; set; }
        public string Password { get; set; }
        public int IdCompany { get; set; }
        public int IdRole { get; set; }

        [ForeignKey("IdCompany")]
        public Company Company { get; set; }
        [ForeignKey("IdRole")]
        public Role Role { get; set; }
    }
}
