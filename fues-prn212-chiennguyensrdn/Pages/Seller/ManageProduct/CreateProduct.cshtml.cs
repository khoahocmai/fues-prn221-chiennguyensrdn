using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Repositories.IRepo;
using BusinessObjects;

namespace fues_prn221_chiennguyensrdn.Pages.Seller.ManageProduct
{
    public class CreateProductModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly ICategoryRepository _categoryRepo;

        public CreateProductModel(
            IProductRepository productRepository,
            ICategoryRepository categoryRepo)
        {
            _productRepo = productRepository;
            _categoryRepo = categoryRepo;
        }

        [BindProperty]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Price is required")]
        public decimal Price { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Category is required")]
        public int CategoryId { get; set; }

        public List<Category> Categories { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Categories = await _categoryRepo.GetCategories();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userIdClaim = User.FindFirst("UserId");
            int userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            var product = new Product
            {
                Title = Title,
                Description = Description,
                Price = Price,
                SellerId = userId,
                CategoryId = CategoryId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _productRepo.AddProduct(product);
            return RedirectToPage("/Seller/SuccessPage/CreateSuccess");
        }
    }
}
