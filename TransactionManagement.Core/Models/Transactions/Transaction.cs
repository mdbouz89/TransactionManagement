namespace TransactionManagement.Core.Models.Transactions
{
    public abstract class Transaction
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
        protected Transaction(DateTime date, decimal amount, string description)
        {
            Date = date;
            Amount = amount;
            Description = description;
        }
        public abstract TransactionType Type { get; }

    }
}
