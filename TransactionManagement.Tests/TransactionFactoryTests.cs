using Xunit;
using TransactionManagement.Core.Factories;
using TransactionManagement.Core.Models.Transactions;

namespace TransactionManagement.Tests
{
    public class TransactionFactoryTests
    {
        [Fact]
        public void CreateTransaction_CreditTransaction_ShouldCreateCorrectly()
        {
            // Arrange
            decimal amount = 100.50m;

            // Act
            var transaction = TransactionFactory.CreateTransaction(TransactionType.Credit, DateTime.Now, amount, "credit");

            // Assert
            Assert.IsType<CreditTransaction>(transaction);
            Assert.Equal(amount, transaction.Amount);
            Assert.Equal(TransactionType.Credit, transaction.Type);
        }

        [Fact]
        public void CreateTransaction_DebitTransaction_ShouldCreateCorrectly()
        {
            // Arrange
            decimal amount = 50.25m;

            // Act
            var transaction = TransactionFactory.CreateTransaction(TransactionType.Debit, DateTime.Now, amount, "debit");

            // Assert
            Assert.IsType<DebitTransaction>(transaction);
            Assert.Equal(amount, transaction.Amount);
            Assert.Equal(TransactionType.Debit, transaction.Type);
        }
    }
}