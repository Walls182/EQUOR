namespace EQUOR.Models
{
    public class ProductViewModel
    {
        public int IdProduct { get; set; }
        public string Name { get; set; }
        public string DesProduct { get; set; }
        public string TipeTransport { get; set; }
        public int QWaterUsed { get; set; }
        public int QEnergy { get; set; }
        public int QWaste { get; set; }
    }
}