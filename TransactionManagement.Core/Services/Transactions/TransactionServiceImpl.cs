using TransactionManagement.Core.Models.Rules;
using TransactionManagement.Core.Models.Transactions;


namespace TransactionManagement.Core.Services.Transactions
{
    public class TransactionServiceImpl:ITransactionService
    {
        private decimal _currentBalance;
        private readonly List<Transaction> _transactions;
        private readonly List<IValidationRule> _validationRules;

        public TransactionServiceImpl()
        {
            _currentBalance = 0;
            _transactions = new List<Transaction>();
            _validationRules = new List<IValidationRule>()
            {
                new MaximumCreditAmountRule(),
                new MinimumBalanceRule(),
                new MinimumTransactionRule()
            };
        }
        public (bool,List<string>) ProcessTransaction(Transaction transaction)
        {
            var (isValid, errors) = ValidateTransaction(transaction);

            if (isValid)
            {
                _transactions.Add(transaction);
                _currentBalance += transaction.Type == TransactionType.Credit ?
                    transaction.Amount : -transaction.Amount;
                Console.WriteLine($"Transaction traitée. Nouveau solde : {_currentBalance}");
                return (true,errors);
            }else
            {
                Console.WriteLine("Transaction invalide :");
                errors.ForEach(error => Console.WriteLine($"\t- {error}"));
                return (false,errors);
            }
        }
        public decimal GetBalance() => _currentBalance;

        public List<Transaction> GetTransactions() => _transactions;

        private (bool isValid,List<string> errors) ValidateTransaction(Transaction transaction)
        {
     
                var errors = new List<string>();

                foreach (var rule in _validationRules)
                {
                    if (!rule.Validate(transaction, _currentBalance))
                    {
                        errors.Add(rule.ErrorMessage);
                    }

                }
                return (errors.Count == 0, errors);
       
        }
    }
}
