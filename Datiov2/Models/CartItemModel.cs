namespace Datiov2.Models
{
    public class CartItemModel
    {

        public int CartItemID { get; set; }
        public int CartItemCartID { get; set; }
        public int CartItemProductID { get; set; }
        public int CartItemQuantity { get; set; }
        public int CartItemPrice { get; set; }
        public ProductModel CartProduct { get; set; }


    }
}

