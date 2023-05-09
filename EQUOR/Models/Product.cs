using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EQUOR.Models
{
	public class Product
	{
        [Key]
        public int IdProduct { get; set; }

        public int IdManager { get; set; }
        public string Name { get; set; }
        public string DesProduct { get; set; }
        public string TipeTransport { get; set; }
        public int QWaterUsed { get; set; }
        public int QEnergy { get; set; }
        public int QWaste { get; set; }
        public string CodigoQR { get; set; }
        public int CarbonFootprint { get; set; }
        public int TimeSearch { get; set; }

        [ForeignKey("IdManager")]
        public Manager Manager { get; set; }
    }
}
