using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApplication1.Views.ViewModels
{
    public class EditProductModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public decimal price { get; set; }
        public string CategoryName { get; set; }
    }
}
