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
        private readonly FUESManagementContext _context;

        public TransactionDAO(FUESManagementContext context)
        {
            _context = context;
        }

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
            return await _context.Transactions.ToListAsync();
        }

        public async Task<Transaction> GetTransactionById(int id)
        {
            return await _context.Transactions.SingleOrDefaultAsync(t => t.Id == id);
        }

        public async Task AddTransaction(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTransaction(Transaction transaction)
        {
            var existingTransaction = await _context.Transactions.FindAsync(transaction.Id);
            if (existingTransaction != null)
            {
                existingTransaction.BuyerId = transaction.BuyerId;
                existingTransaction.ProductId = transaction.ProductId;
                existingTransaction.Status = transaction.Status;
                existingTransaction.UpdatedAt = DateTime.Now;

                _context.Transactions.Update(existingTransaction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveTransaction(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
        }
    }
}
