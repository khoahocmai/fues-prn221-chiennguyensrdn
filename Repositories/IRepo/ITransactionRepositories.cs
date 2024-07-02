using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessObjects;

public interface ITransactionRepository
{
    Task<List<Transaction>> GetTransactions();
    Task<Transaction> GetTransactionById(int id);
    Task AddTransaction(Transaction transaction);
    Task UpdateTransaction(Transaction transaction);
    Task RemoveTransaction(int id);
}
