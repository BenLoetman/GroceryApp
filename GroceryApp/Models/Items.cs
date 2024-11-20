namespace GroceryApp.Models
{

    // Item table in SQL database has 4 fields.
    public class Items
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int CategoryId {  get; set; } 
    }
}
