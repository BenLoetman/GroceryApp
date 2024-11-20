namespace GroceryApp.Models
{
    // This Model gets all the data in the Category and Item tables.
    public class IndexModel
    {
        public required IEnumerable<Categories> Categories { get; set; }
        public required IEnumerable<Items> Item { get; set; }
    }
}
