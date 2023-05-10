using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using QRCoder;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;

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

        public double CalculateCarbonFootprint(string tipeTransport, int qWaterUsed, int qEnergy, int qWaste)
        {
            double huellaCarbono = 0;

            // Cálculo de la huella de carbono según el tipo de transporte utilizado
            switch (tipeTransport)
            {
                case "Avión":
                    huellaCarbono += 200; // Supongamos que un vuelo de 1000 km emite 200 kg de CO2 por pasajero
                    break;
                case "Automóvil":
                    huellaCarbono += 120; // Supongamos que un coche emite 120 g de CO2 por km recorrido
                    break;
                case "Tren":
                    huellaCarbono += 40; // Supongamos que un tren emite 40 g de CO2 por km recorrido
                    break;
                case "Bicicleta":
                    huellaCarbono += 0; // La huella de carbono de la bicicleta se considera nula
                    break;
                default:
                    huellaCarbono += 0; // Si el tipo de transporte no está especificado, se considera una huella de carbono nula
                    break;
            }

            // Cálculo de la huella de carbono según el consumo de agua y energía y la generación de residuos
            huellaCarbono += qWaterUsed * 5; // Supongamos que se emiten 5 kg de CO2 por cada metro cúbico de agua utilizada
            huellaCarbono += qEnergy * 0.5; // Supongamos que se emiten 0.5 kg de CO2 por cada kW/h de energía consumido
            huellaCarbono += qWaste * 50; // Supongamos que se emiten 50 kg de CO2 por cada tonelada de residuos generada

            return huellaCarbono;
        }


        public byte[] GenerateQrCode(string data)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(data, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            var qrCodeImage = qrCode.GetGraphic(20);

            using (var stream = new MemoryStream())
            {
                qrCodeImage.Save(stream, ImageFormat.Png);
                return stream.ToArray();
            }
        }

        public int UpdateSearchCount(int idProduct)
        {
            //var product = _context.Products.Find(idProduct);
           // product.TimeSearch++;
            return TimeSearch;
        }




    }
}
