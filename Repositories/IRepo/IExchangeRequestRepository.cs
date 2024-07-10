using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories.IRepo
{
    public interface IExchangeRequestRepository
    {
        Task<List<ExchangeRequest>> GetExchangeRequests();
        Task<List<ExchangeRequest>> GetExchangeRequestsBySellerId(int sellerId, string status);
        Task<List<ExchangeRequest>> GetExchangeRequestsByBuyerId(int buyerId);
        Task<List<ExchangeRequest>> GetExchangeRequestsByProductId(int productId);
        Task<ExchangeRequest> GetExchangeRequestsByBuyerIdAndProductId(int buyerId, int productId);
        Task<ExchangeRequest> GetExchangeRequestById(int id);
        Task AddExchangeRequest(ExchangeRequest exchangeRequest);
        Task UpdateExchangeRequest(ExchangeRequest exchangeRequest);
        Task RemoveExchangeRequest(int id);
        Task<bool> HasBuyerAlreadyTradedProduct(int buyerId, int productId);
    }
}
