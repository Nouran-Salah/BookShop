namespace WebApplication1.Views.ViewModels
{
    public class CategoryDetailsViewModel
    {
        public int Id { get; set; }
        public string catName { get; set; }


        public int catOrder { get; set; }

   
        public DateTime createdDate { get; set; } = DateTime.Now;

       
        public bool markedAsDeleted { get; set; }
    }
}
