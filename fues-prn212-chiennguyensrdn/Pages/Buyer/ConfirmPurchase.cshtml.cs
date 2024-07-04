using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Repositories.IRepo;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace fues_prn221_chiennguyensrdn.Pages.Buyer
{
    [Authorize]
    public class ConfirmPurchaseModel : PageModel
    {
        private readonly IProductRepository _productRepo;
        private readonly IExchangeRequestRepository _exchangeRequestRepo;

        public ConfirmPurchaseModel(IProductRepository productRepository, IExchangeRequestRepository exchangeRequestRepository)
        {
            _productRepo = productRepository;
            _exchangeRequestRepo = exchangeRequestRepository;
        }

        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public string Message { get; set; }
        public bool ShowPopup { get; set; }

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
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var userIdClaim = User.FindFirst("UserId");
            int userId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            bool hasAlreadyTraded = await _exchangeRequestRepo.HasBuyerAlreadyTradedProduct(userId, id);
            if (hasAlreadyTraded)
            {
                ShowPopup = true;
                ModelState.AddModelError(string.Empty, "You have already submitted a request to exchange this item");
                return Page();
            }

            var exchangeRequest = new ExchangeRequest
            {
                ProductId = id,
                RequesterId = userId,
                Message = Message,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            await _exchangeRequestRepo.AddExchangeRequest(exchangeRequest);

            return RedirectToPage("/General/ViewProduct");
        }
    }
}
