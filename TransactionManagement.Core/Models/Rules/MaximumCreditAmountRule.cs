using TransactionManagement.Core.Models.Transactions;

namespace TransactionManagement.Core.Models.Rules
{
    public class MaximumCreditAmountRule : IValidationRule
    {
        private const decimal MAX_CREDIT_AMOUNT= 10000;
        private const string ERROR_MESSAGE = "Le montant du crédit ne peut pas dépasser 10 000";
        public string ErrorMessage => ERROR_MESSAGE;

        public bool Validate(Transaction transaction, decimal currentBalance)
        {
            if (transaction.Type != TransactionType.Credit) return true;
            return transaction.Amount <= MAX_CREDIT_AMOUNT;
        }
    }
}
