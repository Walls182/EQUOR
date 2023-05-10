using System.ComponentModel.DataAnnotations;

namespace EQUOR.Models
{
    public class Role
    {
        [Key]
        public int IdRole { get; set; }
        public string Nombre { get; set; }
    }

}
