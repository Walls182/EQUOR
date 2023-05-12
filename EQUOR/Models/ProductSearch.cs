using System.ComponentModel.DataAnnotations.Schema;

namespace EQUOR.Models
{
    public class ProductSearch
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public DateTime SearchDate { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
