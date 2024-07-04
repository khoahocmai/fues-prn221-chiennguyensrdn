using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;
using BusinessObjects;

namespace fues_prn221_chiennguyensrdn.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IUserRepository _userRepo;
        private readonly IBanRepository _banRepo;

        public DashboardModel(IProductRepository productRepo, IUserRepository userRepo, IBanRepository banRepo)
        {
            _productRepo = productRepo;
            _userRepo = userRepo;
            _banRepo = banRepo;
        }

        public int TotalProducts { get; set; }
        public int TotalBannedProducts { get; set; }
        public int TotalBuyers { get; set; }
        public int TotalSellers { get; set; }
        public int TotalModerators { get; set; }

        public List<Product> ProductDetails { get; set; }
        public List<Product> BannedProductDetails { get; set; }
        public List<User> BuyerDetails { get; set; }
        public List<User> SellerDetails { get; set; }
        public List<User> ModeratorDetails { get; set; }

        public async Task OnGetAsync()
        {
            TotalProducts = await _productRepo.GetTotalProducts();
            TotalBannedProducts = await _banRepo.GetBannedProducts();
            TotalBuyers = await _userRepo.GetTotalUsersByRole("Buyer");
            TotalSellers = await _userRepo.GetTotalUsersByRole("Seller");
            TotalModerators = await _userRepo.GetTotalUsersByRole("Moderator");

            ProductDetails = await _productRepo.GetProducts();
            BannedProductDetails = await _productRepo.GetProductBanned();
            BuyerDetails = await _userRepo.GetUsersByRole("Buyer");
            SellerDetails = await _userRepo.GetUsersByRole("Seller");
            ModeratorDetails = await _userRepo.GetUsersByRole("Moderator");
        }
    }
}
