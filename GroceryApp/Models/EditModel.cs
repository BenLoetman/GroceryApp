namespace GroceryApp.Models
{

    // Model used to get all categories and single item.
    public class EditModel
    {
        public required IEnumerable<Categories> Categories { get; set; }
        public required Items Item { get; set; }
    }
}
