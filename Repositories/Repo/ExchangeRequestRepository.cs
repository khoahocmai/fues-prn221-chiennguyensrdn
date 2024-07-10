using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repo
{
    public class ExchangeRequestRepository : IExchangeRequestRepository
    {
        public Task<List<ExchangeRequest>> GetExchangeRequests()
            => ExchangeRequestDAO.Instance.GetExchangeRequests();

        public Task<List<ExchangeRequest>> GetExchangeRequestsBySellerId(int sellerId, string status)
            => ExchangeRequestDAO.Instance.GetExchangeRequestsBySellerId(sellerId, status);

        public Task<List<ExchangeRequest>> GetExchangeRequestsByBuyerId(int buyerId)
            => ExchangeRequestDAO.Instance.GetExchangeRequestsByBuyerId(buyerId);

        public Task<List<ExchangeRequest>> GetExchangeRequestsByProductId(int productId)
            => ExchangeRequestDAO.Instance.GetExchangeRequestsByProductId(productId);

        public Task<ExchangeRequest> GetExchangeRequestsByBuyerIdAndProductId(int buyerId, int productId)
            => ExchangeRequestDAO.Instance.GetExchangeRequestsByBuyerIdAndProductId(buyerId, productId);

        public Task<ExchangeRequest> GetExchangeRequestById(int id)
            => ExchangeRequestDAO.Instance.GetExchangeRequestById(id);

        public Task AddExchangeRequest(ExchangeRequest exchangeRequest)
            => ExchangeRequestDAO.Instance.AddExchangeRequest(exchangeRequest);

        public Task UpdateExchangeRequest(ExchangeRequest exchangeRequest)
            => ExchangeRequestDAO.Instance.UpdateExchangeRequest(exchangeRequest);

        public Task RemoveExchangeRequest(int id)
            => ExchangeRequestDAO.Instance.RemoveExchangeRequest(id);

        public Task<bool> HasBuyerAlreadyTradedProduct(int buyerId, int productId)
            => ExchangeRequestDAO.Instance.HasBuyerAlreadyTradedProduct( buyerId, productId);
    }
}
