using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace fues_prn221_chiennguyensrdn.Pages.Buyer
{
    [Authorize]
    public class ConfirmReportModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IReportRepository _reportRepo;

        public ConfirmReportModel(IProductRepository productRepo, IReportRepository reportRepo)
        {
            _productRepo = productRepo;
            _reportRepo = reportRepo;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public int ProductId { get; set; }

        [BindProperty]
        public string? ReportReason { get; set; }
        public bool ShowPopup { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Product = await _productRepo.GetProductById(id);

            if (Product == null)
            {
                return NotFound();
            }
            ProductId = id;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userIdClaim = User.FindFirst("UserId");
            int userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            bool hasAlreadyReported = await _reportRepo.HasBuyerAlreadyReportedProduct(userId, id);
            if (hasAlreadyReported)
            {
                ShowPopup = true;
                ModelState.AddModelError(string.Empty, "You have already reported this item");
                return Page();
            }

            Product = await _productRepo.GetProductById(ProductId);
            if (Product == null)
            {
                return NotFound();
            }

            var report = new Report
            {
                ProductId = Product.Id,
                ReporterId = userId,
                Reason = ReportReason,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _reportRepo.AddReport(report);

            return RedirectToPage("/General/ViewProduct");
        }
    }
}
