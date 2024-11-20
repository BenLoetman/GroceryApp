namespace GroceryApp.Models
{
    public class Items
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int CategoryId {  get; set; } 
    }
}
