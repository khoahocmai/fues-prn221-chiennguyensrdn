using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;

namespace fues_prn221_chiennguyensrdn.Pages.Seller.ManageER
{
    public class HIstoryExchangeRequestModel : PageModel
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepo;

        public HIstoryExchangeRequestModel(IExchangeRequestRepository exchangeRequestRepository)
        {
            _exchangeRequestRepo = exchangeRequestRepository;
        }
        public List<ExchangeRequest> ExchangeRequests { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst("UserId");
            int userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
            ExchangeRequests = await _exchangeRequestRepo.GetExchangeRequestsBySellerId(userId, "Accepted");

            return Page();
        }
    }
}
