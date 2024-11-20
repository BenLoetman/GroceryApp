
namespace GroceryApp.Models
{

    // Model; used for the create item form.
    public class CreateModel
    {
        public required Items Item { get; set; }
        public required IEnumerable<Categories> Categories { get; set; }
        public int CategoryId { get; set; }
    }


}
