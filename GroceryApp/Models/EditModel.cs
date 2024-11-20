namespace GroceryApp.Models
{
    public class EditModel
    {
        public required IEnumerable<Categories> Categories { get; set; }
        public required Items Item { get; set; }
    }
}
