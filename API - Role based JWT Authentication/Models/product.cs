using System.ComponentModel.DataAnnotations;

namespace GeneratingTokens.Models
{
    public class product
    {
        [Key]
        public int? product_id { get; set; }
        public string? product_name { get; set; }
        public decimal? product_price { get; set; }

    }
}
