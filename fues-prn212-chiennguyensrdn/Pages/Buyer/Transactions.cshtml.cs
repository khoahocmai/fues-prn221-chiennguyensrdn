using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObjects;
using Repositories.IRepo;
using System.Security.Claims;

namespace fues_prn221_chiennguyensrdn.Pages.Buyer
{
    public class TransactionsModel : PageModel
    {
        private readonly ITransactionRepository _transactionRepo;
        private readonly IProductRepository _productRepo;

        public TransactionsModel(ITransactionRepository transactionRepo, IProductRepository productRepo)
        {
            _transactionRepo = transactionRepo;
            _productRepo = productRepo;
        }

        public List<Transaction> Transactions { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var userIdClaim = User.FindFirst("UserId");
            int buyerId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;

            Transactions = await _transactionRepo.GetTransactionsByBuyerId(buyerId);
            return Page();
        }

        public async Task<IActionResult> OnPostUpdateStatusAsync(int transactionId, string status)
        {
            await _transactionRepo.UpdateTransactionStatus(transactionId, status);

            if (status == "Completed")
            {
                var transaction = await _transactionRepo.GetTransactionById(transactionId);
                if (transaction != null && transaction.ProductId.HasValue)
                {
                    await _productRepo.UpdateProductStatus(transaction.ProductId.Value, "Sold");
                }
            }

            return RedirectToPage();
        }
    }
}
