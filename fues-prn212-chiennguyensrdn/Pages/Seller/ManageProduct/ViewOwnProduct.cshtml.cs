using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepo;


namespace fues_prn221_chiennguyensrdn.Pages.Seller.ManageProduct
{
    public class ViewOwnProductModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        public ViewOwnProductModel(IProductRepository productRepository)
        {
            _productRepo = productRepository;
        }

        public List<Product> Products { get; set; }

        public async Task OnGetAsync()
        {
            var userIdClaim = User.FindFirst("UserId");
            int userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
            Products = await _productRepo.GetProductsBySellerId(userId);
        }
    }
}
