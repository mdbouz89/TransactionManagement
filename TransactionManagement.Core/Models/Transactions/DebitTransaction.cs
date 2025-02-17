namespace TransactionManagement.Core.Models.Transactions
{
    public class DebitTransaction : Transaction
    {
        public DebitTransaction(DateTime date, decimal amount, string description)
            : base(date, amount, description)
        {
        }
        public override TransactionType Type => TransactionType.Debit;
    }
}
