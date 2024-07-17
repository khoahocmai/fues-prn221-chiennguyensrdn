using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;
using System.Linq;

namespace fues_prn221_chiennguyensrdn.Pages.General
{
    public class ViewProductsModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public ViewProductsModel(IProductRepository productRepo, ICategoryRepository categoryRepo)
        {
            _productRepo = productRepo;
            _categoryRepo = categoryRepo;
        }

        public List<Product> Products { get; set; }
        public List<Category> Categories { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SelectedCategoryId { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MinPrice { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxPrice { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _categoryRepo.GetCategories();

            Products = await _productRepo.GetProducts();

            if (SelectedCategoryId.HasValue)
            {
                Products = Products.Where(p => p.CategoryId == SelectedCategoryId.Value).ToList();
            }

            if (MinPrice.HasValue)
            {
                Products = Products.Where(p => p.Price >= MinPrice.Value).ToList();
            }

            if (MaxPrice.HasValue)
            {
                Products = Products.Where(p => p.Price <= MaxPrice.Value).ToList();
            }

            return Page();
        }
    }
}
