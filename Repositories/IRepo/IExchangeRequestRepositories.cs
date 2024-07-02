using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

public interface IExchangeRequestRepository
{
    Task<List<ExchangeRequest>> GetExchangeRequests();
    Task<ExchangeRequest> GetExchangeRequestById(int id);
    Task AddExchangeRequest(ExchangeRequest exchangeRequest);
    Task UpdateExchangeRequest(ExchangeRequest exchangeRequest);
    Task RemoveExchangeRequest(int id);
}
