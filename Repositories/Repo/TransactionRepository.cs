using BusinessObjects;
using DataAccessObjects.DAO;
using Repositories.IRepo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repositories.Repo
{
    public class TransactionRepository : ITransactionRepository
    {
        public Task<List<Transaction>> GetTransactions()
            => TransactionDAO.Instance.GetTransactions();

        public Task<Transaction> GetTransactionById(int id)
            => TransactionDAO.Instance.GetTransactionById(id);

        public Task AddTransaction(Transaction transaction)
            => TransactionDAO.Instance.AddTransaction(transaction);

        public Task UpdateTransaction(Transaction transaction)
            => TransactionDAO.Instance.UpdateTransaction(transaction);

        public Task RemoveTransaction(int id)
            => TransactionDAO.Instance.RemoveTransaction(id);

        public Task<List<Transaction>> GetTransactionsByBuyerId(int buyerId)
            => TransactionDAO.Instance.GetTransactionsByBuyerId(buyerId);

        public Task UpdateTransactionStatus(int transactionId, string status)
            => TransactionDAO.Instance.UpdateTransactionStatus(transactionId, status);
    }
}
