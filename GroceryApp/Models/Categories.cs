namespace GroceryApp.Models
{

    // This Model contains all the fields for the Category table.
    public class Categories
    {
        public int Id { get; set; }
        public required string CategoryName { get; set; }
        public required string CategoryColor { get; set; }
    }
}
