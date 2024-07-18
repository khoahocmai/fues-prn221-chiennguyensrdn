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
            using var db = new FUESManagementContext();
            return await db.ExchangeRequests.ToListAsync();
        }

        public async Task<List<ExchangeRequest>> GetExchangeRequestsBySellerId(int sellerId, string status)
        {
            using var db = new FUESManagementContext();
            return await db.ExchangeRequests
                .Where(er => er.Product.SellerId == sellerId && er.Status == status)
                .Include(er => er.Product)
                .Include(er => er.Requester)
                .ToListAsync();
        }

        public async Task<List<ExchangeRequest>> GetExchangeRequestsByBuyerId(int buyerId)
        {
            using var db = new FUESManagementContext();
            return await db.ExchangeRequests
                .Where(er => er.RequesterId == buyerId)
                .Include(er => er.Product)
                .Include(er => er.Requester)
                .ToListAsync();
        }

        public async Task<List<ExchangeRequest>> GetExchangeRequestsByProductId(int productId)
        {
            using var db = new FUESManagementContext();
            return await db.ExchangeRequests
                .Where(er => er.ProductId == productId)
                .Include(er => er.Product)
                .Include(er => er.Requester)
                .ToListAsync();
        }

        public async Task<ExchangeRequest> GetExchangeRequestsByBuyerIdAndProductId(int buyerId, int productId)
        {
            using var db = new FUESManagementContext();
            return await db.ExchangeRequests
                .Where(er => er.RequesterId == buyerId && er.ProductId == productId)
                .Include(er => er.Product)
                .Include(er => er.Requester)
                .FirstOrDefaultAsync();
        }

        public async Task<ExchangeRequest> GetExchangeRequestById(int id)
        {
            using var db = new FUESManagementContext();
            return await db.ExchangeRequests
                .Where(er => er.Id == id)
                .Include(er => er.Product)
                .Include(er => er.Requester)
                .FirstOrDefaultAsync();
        }

        public async Task AddExchangeRequest(ExchangeRequest exchangeRequest)
        {
            using var db = new FUESManagementContext();
            await db.ExchangeRequests.AddAsync(exchangeRequest);
            await db.SaveChangesAsync();
        }

        public async Task UpdateExchangeRequest(ExchangeRequest exchangeRequest)
        {
            // Validation: Ensure the provided exchangeRequest is not null
            if (exchangeRequest == null)
            {
                throw new ArgumentNullException(nameof(exchangeRequest), "Exchange request cannot be null");
            }

            using var db = new FUESManagementContext();

            // Retrieve the existing request from the database
            var existingRequest = await db.ExchangeRequests.FindAsync(exchangeRequest.Id);
            if (existingRequest == null)
            {
                throw new ArgumentException("Exchange Request not found", nameof(exchangeRequest.Id));
            }

            // Update the properties
            existingRequest.ProductId = exchangeRequest.ProductId;
            existingRequest.RequesterId = exchangeRequest.RequesterId;
            existingRequest.Message = exchangeRequest.Message;
            existingRequest.Status = exchangeRequest.Status;
            existingRequest.UpdatedAt = DateTime.Now;

            // Save changes to the database
            db.ExchangeRequests.Update(existingRequest);
            await db.SaveChangesAsync();
        }

        public async Task RemoveExchangeRequest(int id)
        {
            using var db = new FUESManagementContext();
            var exchangeRequest = await db.ExchangeRequests.FindAsync(id);
            if (exchangeRequest != null)
            {
                db.ExchangeRequests.Remove(exchangeRequest);
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> HasBuyerAlreadyTradedProduct(int buyerId, int productId)
        {
            using var db = new FUESManagementContext();
            bool isAlreadyExchaned = await db.ExchangeRequests
                .AnyAsync(er => er.RequesterId == buyerId && er.ProductId == productId && er.Status == "Accepted");
            bool IsNotCanceledInTracsaction = await db.Transactions
                .AnyAsync(t => t.BuyerId == buyerId && t.ProductId == productId && t.Status != "Canceled");
            return isAlreadyExchaned && IsNotCanceledInTracsaction;
        }
    }
}
