using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class ExchangeRequestDAO
    {
        private readonly FUESManagementContext _context;

        public ExchangeRequestDAO(FUESManagementContext context)
        {
            _context = context;
        }

        private static ExchangeRequestDAO instance = null;
        public static readonly object Lock = new object();
        private ExchangeRequestDAO() { }
        public static ExchangeRequestDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new ExchangeRequestDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<ExchangeRequest>> GetExchangeRequests()
        {
            return await _context.ExchangeRequests.ToListAsync();
        }

        public async Task<ExchangeRequest> GetExchangeRequestById(int id)
        {
            return await _context.ExchangeRequests.SingleOrDefaultAsync(er => er.Id == id);
        }

        public async Task AddExchangeRequest(ExchangeRequest exchangeRequest)
        {
            await _context.ExchangeRequests.AddAsync(exchangeRequest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateExchangeRequest(ExchangeRequest exchangeRequest)
        {
            var existingRequest = await _context.ExchangeRequests.FindAsync(exchangeRequest.Id);
            if (existingRequest != null)
            {
                existingRequest.ProductId = exchangeRequest.ProductId;
                existingRequest.RequesterId = exchangeRequest.RequesterId;
                existingRequest.Message = exchangeRequest.Message;
                existingRequest.Status = exchangeRequest.Status;
                existingRequest.UpdatedAt = DateTime.Now;

                _context.ExchangeRequests.Update(existingRequest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveExchangeRequest(int id)
        {
            var exchangeRequest = await _context.ExchangeRequests.FindAsync(id);
            if (exchangeRequest != null)
            {
                _context.ExchangeRequests.Remove(exchangeRequest);
                await _context.SaveChangesAsync();
            }
        }
    }
}
