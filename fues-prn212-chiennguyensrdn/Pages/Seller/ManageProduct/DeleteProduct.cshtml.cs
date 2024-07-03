using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Repositories.IRepo;
using BusinessObjects;

namespace fues_prn221_chiennguyensrdn.Pages.Seller.ManageProduct
{
    public class DeleteProductModel : PageModel
    {
        private readonly IProductRepository _productRepo;

        public DeleteProductModel(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        [BindProperty]
        public Product Product { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _productRepo.GetProductById(id);

            if (Product == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync()
        {
            if (Product == null)
            {
                return NotFound();
            }

            await _productRepo.RemoveProduct(Product.Id);
            return RedirectToPage("/Seller/SuccessPage/DeleteSuccess");
        }
    }
}
