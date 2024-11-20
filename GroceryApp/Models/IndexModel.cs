namespace GroceryApp.Models
{
    public class IndexModel
    {
        public required IEnumerable<Categories> Categories { get; set; }

        public required IEnumerable<Items> Item { get; set; }
    }
}
