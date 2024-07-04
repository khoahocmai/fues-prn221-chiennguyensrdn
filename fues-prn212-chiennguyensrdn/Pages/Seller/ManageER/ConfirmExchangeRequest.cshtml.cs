using BusinessObjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Repositories.IRepo;

namespace fues_prn221_chiennguyensrdn.Pages.Seller.ManageER
{
    public class ConfirmExchangeRequestModel : PageModel
    {
        private readonly IExchangeRequestRepository _exchangeRequestRepo;
        private readonly IProductRepository _productRepo;

        public ConfirmExchangeRequestModel(IExchangeRequestRepository exchangeRequestRepo, IProductRepository productRepository)
        {
            _exchangeRequestRepo = exchangeRequestRepo;
            _productRepo = productRepository;
        }

        [BindProperty]
        public ExchangeRequest ExchangeRequest { get; set; }

        [BindProperty]
        public int ProductId { get; set; }

        public async Task<IActionResult> OnGetAsync(int exchangeRequestId, int productId)
        {
            ExchangeRequest = await _exchangeRequestRepo.GetExchangeRequestById(exchangeRequestId);
            ProductId = productId;

            if (ExchangeRequest == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int exchangeRequestId, int productId)
        {
            // Lấy tất cả các yêu cầu trao đổi liên quan đến productId
            var exchangeRequests = await _exchangeRequestRepo.GetExchangeRequestsByProductId(productId);

            if (exchangeRequests == null || !exchangeRequests.Any())
            {
                return NotFound("No exchange requests found for the given product.");
            }

            // Cập nhật trạng thái của tất cả các yêu cầu trao đổi về "Rejected"
            foreach (var request in exchangeRequests)
            {
                if (request.Id != exchangeRequestId)
                {
                    request.Status = "Rejected";
                    request.UpdatedAt = DateTime.UtcNow;
                    await _exchangeRequestRepo.UpdateExchangeRequest(request);
                }
            }

            // Lấy yêu cầu trao đổi cụ thể bằng exchangeRequestId
            var exchangeRequest = await _exchangeRequestRepo.GetExchangeRequestById(exchangeRequestId);

            if (exchangeRequest == null)
            {
                return NotFound("Specific exchange request not found.");
            }

            // Cập nhật trạng thái của yêu cầu trao đổi cụ thể về "Accepted"
            exchangeRequest.Status = "Accepted";
            exchangeRequest.UpdatedAt = DateTime.UtcNow;
            await _exchangeRequestRepo.UpdateExchangeRequest(exchangeRequest);

            var product = await _productRepo.GetProductById(productId);
            product.Status = "Sold";
            await _productRepo.UpdateProduct(product);

            return RedirectToPage("/Seller/ManageER/ExchangeRequest");
        }

    }
}
