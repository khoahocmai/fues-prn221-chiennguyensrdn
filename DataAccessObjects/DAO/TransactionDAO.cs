using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccessObjects.DAO
{
    public class TransactionDAO
    {
        private static TransactionDAO instance = null;
        public static readonly object Lock = new object();
        private TransactionDAO() { }
        public static TransactionDAO Instance
        {
            get
            {
                lock (Lock)
                {
                    if (instance == null)
                    {
                        instance = new TransactionDAO();
                    }
                    return instance;
                }
            }
        }

        public async Task<List<Transaction>> GetTransactions()
        {
            using var db = new FUESManagementContext();
            return await db.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            using var db = new FUESManagementContext();
            return await db.Transactions.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            using var db = new FUESManagementContext();
            await db.Transactions.AddAsync(transaction);
            await db.SaveChangesAsync();
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            using var db = new FUESManagementContext();
            var existingTransaction = await db.Transactions.FindAsync(transaction.Id);
            if (existingTransaction != null)
            {
                existingTransaction.BuyerId = transaction.BuyerId;
                existingTransaction.ProductId = transaction.ProductId;
                existingTransaction.Status = transaction.Status;
                existingTransaction.UpdatedAt = DateTime.Now;

                db.Transactions.Update(existingTransaction);
                await db.SaveChangesAsync();
            }
        }

        public async Task RemoveTransaction(int id)
        {
            using var db = new FUESManagementContext();
            var transaction = await db.Transactions.FindAsync(id);
            if (transaction != null)
            {
                db.Transactions.Remove(transaction);
                await db.SaveChangesAsync();
            }
        }

        public async Task<List<Transaction>> GetTransactionsByBuyerId(int buyerId)
        {
            using var db = new FUESManagementContext();
            return await db.Transactions
                          .Where(t => t.BuyerId == buyerId)
                          .Include(t => t.Product)
                          .ToListAsync();
        }

        public async Task UpdateTransactionStatus(int transactionId, string status)
        {
            using var db = new FUESManagementContext();
            var transaction = await db.Transactions.FindAsync(transactionId);
            if (transaction != null)
            {
                transaction.Status = status;
                transaction.UpdatedAt = DateTime.Now;
                await db.SaveChangesAsync();
            }
        }

    }
}
