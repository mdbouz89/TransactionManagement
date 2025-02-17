using System;
using TransactionManagement.Core.Models.Transactions;

namespace TransactionManagement.Core.Models.Rules
{
    public class MinimumTransactionRule : IValidationRule
    {
         private const string ERROR_MESSAGE = "Les valeurs saisies ne sont pas valides.";
        public string ErrorMessage => ERROR_MESSAGE;
        public bool Validate(Transaction transaction, decimal currentBalance)
        {
    
            if (transaction.Amount <= 0 || string.IsNullOrWhiteSpace(transaction.Description))
            {
                return false;
            }
            return true;
        }
    }
}