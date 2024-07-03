using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

namespace Repositories.IRepo
{
    public interface IExchangeRequestRepository
    {
        Task<List<ExchangeRequest>> GetExchangeRequests();
        Task<List<ExchangeRequest>> GetExchangeRequestsBySellerId(int sellerId);
        Task<List<ExchangeRequest>> GetExchangeRequestsByProductId(int productId);
        Task<ExchangeRequest> GetExchangeRequestById(int id);
        Task AddExchangeRequest(ExchangeRequest exchangeRequest);
        Task UpdateExchangeRequest(ExchangeRequest exchangeRequest);
        Task RemoveExchangeRequest(int id);
    }
}
