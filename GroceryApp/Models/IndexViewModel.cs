
namespace GroceryApp.Models
{
    public class IndexViewModel
    {
        public required Items Item { get; set; }
        public required IEnumerable<Categories> Categories { get; set; }
        public int CategoryId { get; set; }
    }


}
