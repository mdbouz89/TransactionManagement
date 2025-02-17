using TransactionManagement.Core.Models.Transactions;

namespace TransactionManagement.Core.Models.Rules
{
    public class MinimumBalanceRule : IValidationRule
    {
        private const decimal MIN_BALANCE_AMOUNT = -5000;
        private const string ERROR_MESSAGE = "Le solde ne peut pas être inférieur à -5 000";
        public string ErrorMessage => ERROR_MESSAGE;

        public bool Validate(Transaction transaction, decimal currentBalance)
        {
            if (transaction.Type != TransactionType.Debit) return true;

            return (currentBalance - transaction.Amount) >= MIN_BALANCE_AMOUNT;
        }
    }
}
