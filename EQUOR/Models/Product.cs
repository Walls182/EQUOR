using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using QRCoder;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;
using EQUOR.DataContext;

namespace EQUOR.Models
{
	public class Product
	{
        [Key]
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string DesProduct { get; set; }
        public string TipeTransport { get; set; }
        public int QWaterUsed { get; set; }
        public int QEnergy { get; set; }
        public int QWaste { get; set; }
        public byte[] CodigoQR { get; set; }
        public double CarbonFootprint { get; set; }
        public int TimeSearch { get; set; }
       
       




    }
}
