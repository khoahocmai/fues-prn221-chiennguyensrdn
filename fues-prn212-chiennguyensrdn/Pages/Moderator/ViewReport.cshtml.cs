using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepo;

namespace fues_prn221_chiennguyensrdn.Pages.Moderator
{
    public class ViewReportModel : PageModel
    {
        private readonly IReportRepository _reportRepo;
        public ViewReportModel(IReportRepository reportRepository)
        {
            _reportRepo = reportRepository;
        }
        public List<Report> Reports { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Reports = await _reportRepo.GetReports();

            return Page();
        }

        public async Task<IActionResult> OnPostRejectAsync(int productId)
        {
            var reports = await _reportRepo.GetReportByProductId(productId);
            foreach (var report in reports)
            {
                report.Status = "Rejected";
                await _reportRepo.UpdateReport(report);
            }
            return RedirectToPage();
        }

    }
}
