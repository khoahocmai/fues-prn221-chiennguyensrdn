﻿using BusinessObjects;
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

        public async Task<List<ExchangeRequest>> GetExchangeRequestsBySellerId(int sellerId)
        {
            using var db = new FUESManagementContext();
            return await db.ExchangeRequests
                .Where(er => er.Product.SellerId == sellerId && er.Status == "Pending")
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
            using var db = new FUESManagementContext();
            var existingRequest = await db.ExchangeRequests.FindAsync(exchangeRequest.Id);
            if (existingRequest == null)
            {
                throw new ArgumentException("Exchange Request not found");
            }
                existingRequest.ProductId = exchangeRequest.ProductId;
                existingRequest.RequesterId = exchangeRequest.RequesterId;
                existingRequest.Message = exchangeRequest.Message;
                existingRequest.Status = exchangeRequest.Status;
                existingRequest.UpdatedAt = DateTime.Now;

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
    }
}