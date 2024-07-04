using BusinessObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;
using System.Security.Claims;

namespace fues_prn221_chiennguyensrdn.Pages.Moderator
{
    [Authorize(Roles = "Moderator")]
    public class ConfirmBanModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IBanRepository _banRepo;
        private readonly IReportRepository _reportRepo;

        public ConfirmBanModel(IProductRepository productRepo, IBanRepository banRepo, IReportRepository reportRepository)
        {
            _productRepo = productRepo;
            _banRepo = banRepo;
            _reportRepo = reportRepository;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public int ProductId { get; set; }

        [BindProperty]
        public string Reason { get; set; }

        public async Task<IActionResult> OnGetAsync(int productId)
        {
            Product = await _productRepo.GetProductById(productId);

            if (Product == null)
            {
                return NotFound();
            }

            ProductId = productId;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var state in ModelState)
                {
                    foreach (var error in state.Value.Errors)
                    {
                        Console.WriteLine($"Property: {state.Key} Error: {error.ErrorMessage}");
                    }
                }
                return Page();
            }

            // Ensure Product is retrieved again in OnPostAsync
            Product = await _productRepo.GetProductById(ProductId);

            if (Product == null)
            {
                return NotFound();
            }

            var userIdClaim = User.FindFirst("UserId");
            int moderatorId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            var ban = new Ban
            {
                ProductId = ProductId,
                ModeratorId = moderatorId,
                Reason = Reason,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(10),
            };

            await _banRepo.AddBan(ban);

            Product.Status = "Banned";
            await _productRepo.UpdateProduct(Product);

            var reports = await _reportRepo.GetReportByProductId(ProductId);
            foreach (var report in reports)
            {
                report.Status = "Actioned";
                await _reportRepo.UpdateReport(report);
            }

            return RedirectToPage("/Moderator/ViewReport");
        }
    }
}
