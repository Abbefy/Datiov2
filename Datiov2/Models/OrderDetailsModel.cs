namespace Datiov2.Models
{
    public class OrderDetailsModel
    {
        public int OrderDetailsID { get; set; }
        public int OrderDetailsOrderID { get; set; }
        public int OrderDetailsProductID { get; set; }
        public int OrderDetailsQuantity { get; set; }
        public int OrderDetailsPrice { get; set; }
        public ProductModel OrderDetailsProduct { get; set; }


    }
}

