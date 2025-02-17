using TransactionManagement.Core.Models.Transactions;

namespace TransactionManagement.Core.Services.Transactions
{
    public interface ITransactionService
    {
        (bool,List<string>) ProcessTransaction(Transaction transaction);
        decimal GetBalance();
        List<Transaction> GetTransactions(); 
    }
}
