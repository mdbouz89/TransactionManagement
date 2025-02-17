using TransactionManagement.Core.Models.Transactions;

namespace TransactionManagement.Core.Factories
{
    public static class TransactionFactory
    {
        public static Transaction CreateTransaction(TransactionType type,DateTime date,decimal amount,string description)
        {
            return type switch {
                TransactionType.Credit => new CreditTransaction(date, amount, description),
                TransactionType.Debit => new DebitTransaction(date, amount,description),
                _ => throw new ArgumentException("Type de transaction invalide",nameof(type))
            };

        }
    }
}
