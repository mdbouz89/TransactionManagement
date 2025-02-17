using FluentAssertions;
using TransactionManagement.Core.Factories;
using TransactionManagement.Core.Models.Transactions;
using TransactionManagement.Core.Services.Transactions;
using Xunit;

namespace TransactionManagement.Tests
{
    public class TransactionIntegrationTests : IDisposable
    {
        private readonly TransactionServiceImpl _service;
        private readonly List<Transaction> _testTransactions;

        public TransactionIntegrationTests()
        {
            _service = new TransactionServiceImpl();
            _testTransactions = new List<Transaction>();
        }
        public void Dispose()
        {
            // Nettoyage après chaque test
            _testTransactions.Clear();
        }
        [Fact]
        public void CompleteTransactionWorkflow_ShouldSucceed()
        {
            // Arrange  
            var creditTransaction = TransactionFactory.CreateTransaction(
                TransactionType.Credit,
                DateTime.Now,
                1000m,
                "Salaire");

            var debitTransaction = TransactionFactory.CreateTransaction(
                TransactionType.Debit,
                DateTime.Now,
                500m,
                "Loyer");

            // Étape 1 : Traitement des transactions  
            var (creditResult,_) =  _service.ProcessTransaction(creditTransaction);
            creditResult.Should().BeTrue();
         
            var (debitResult,_) = _service.ProcessTransaction(debitTransaction);
            debitResult.Should().BeTrue();

            // Étape 2 : Vérification du solde  
            var balance =  _service.GetBalance();
            balance.Should().Be(500m);

            // Étape 3 : Vérification de l'historique  
            var transactions =  _service.GetTransactions();
            transactions.Should().HaveCount(2);
            transactions.Should().Contain(creditTransaction);
            transactions.Should().Contain(debitTransaction);
        }
    }
}
