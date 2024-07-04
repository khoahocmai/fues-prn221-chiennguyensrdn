using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;

namespace fues_prn221_chiennguyensrdn.Pages.Buyer
{
    public class ViewExchangeRequestModel : PageModel
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepo;

        public ViewExchangeRequestModel(IExchangeRequestRepository exchangeRequestRepository)
        {
            _exchangeRequestRepo = exchangeRequestRepository;
        }

        public IList<ExchangeRequest> ExchangeRequests { get; set; } = new List<ExchangeRequest>();

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst("UserId");
            int userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            ExchangeRequests = await _exchangeRequestRepo.GetExchangeRequestsByBuyerId(userId);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var exchangeRequest = await _exchangeRequestRepo.GetExchangeRequestById(id);

            if (exchangeRequest == null)
            {
                return NotFound();
            }

            await _exchangeRequestRepo.RemoveExchangeRequest(id);

            return RedirectToPage("/Buyer/ViewExchangeRequest");
        }
    }
}
