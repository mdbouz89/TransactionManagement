namespace TransactionManagement.Core.Models.Transactions
{
    public class CreditTransaction : Transaction
    {
        public CreditTransaction(DateTime date, decimal amount, string description)
            : base(date, amount, description)
        {
        }
        public override TransactionType Type => TransactionType.Credit;
    }
}
