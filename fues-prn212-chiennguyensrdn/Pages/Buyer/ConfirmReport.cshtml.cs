using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;
using System.Security.Claims;

namespace fues_prn221_chiennguyensrdn.Pages.Buyer
{
    public class ConfirmReportModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IReportRepository _reportRepo;

        public ConfirmReportModel(IProductRepository productRepo, IReportRepository reportRepo)
        {
            _productRepo = productRepo;
            _reportRepo = reportRepo;
        }

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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var userIdClaim = User.FindFirst("UserId");
            int userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            var report = new Report
            {
                ProductId = id,
                ReporterId = userId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _reportRepo.AddReport(report);

            return RedirectToPage("/Buyer/ViewProduct");
        }
    }
}
