namespace Datiov2.Models
{
    public class OrderModel
    {
        public int OrderID { get; set; }
        public int OrderCartID { get; set; }
        public int OrderUserID { get; set; }
        public int OrderPrice { get; set; }
        public string OrderAddress { get; set; }
        public string OrderFirstName { get; set; }
        public string OrderLastName { get; set; }
        public int OrderPostalCode { get; set; }
        public string OrderCity { get; set; }
        public string OrderDate { get; set; }

    }
}
