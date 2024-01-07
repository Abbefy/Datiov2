namespace Datiov2.Models
{
    public class CategoryModel
    {
        public int CategoryID { get; set; }
        public string CategoryName { get; set; }
        public int CategoryRank { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
