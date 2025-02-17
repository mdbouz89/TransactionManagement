using TransactionManagement.Core.Models.Transactions;

namespace TransactionManagement.Core.Models.Rules
{
    public interface IValidationRule
    {
        bool Validate(Transaction transaction, decimal currentBalance);
        string ErrorMessage { get; }
    }
}
