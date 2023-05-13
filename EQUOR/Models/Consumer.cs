using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EQUOR.Models
{
	public class Consumer
	{
        [Key]
        public int IdConsumer { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        public string Name { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "El campo Nombre es requerido")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public int IdRole { get; set; }
        [ForeignKey("IdRole")]
        public Role Role { get; set; }
    }
}
