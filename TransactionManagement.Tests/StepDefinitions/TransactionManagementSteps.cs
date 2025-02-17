using FluentAssertions;
using TechTalk.SpecFlow;
using TransactionManagement.Core.Factories;
using TransactionManagement.Core.Models;
using TransactionManagement.Core.Models.Transactions;
using TransactionManagement.Core.Services;
using TransactionManagement.Core.Services.Transactions;

namespace TransactionManagement.Tests.StepDefinitions
{
    [Binding]
    public class TransactionManagementSteps
    {
        private readonly ITransactionService _transactionService;
        private decimal _initialBalance;
        private bool _transactionResult;
        private List<Transaction> _transactions;
        private List<string> _errors;

        public TransactionManagementSteps()
        {
            _transactionService = new TransactionServiceImpl();
            _transactions = new List<Transaction>();
        }

        [Given(@"un solde initial de (.*)")]
        public void GivenUnSoldeInitialDe(decimal initialBalance)
        {
            _initialBalance = initialBalance;

            // Ajouter une transaction initiale pour définir le solde
            if (_initialBalance > 0)
            {
                var initialTransaction = TransactionFactory.CreateTransaction(
                    TransactionType.Credit,
                    DateTime.Now,
                    _initialBalance,
                    "Initial balance");
                  (_,_errors)=_transactionService.ProcessTransaction(initialTransaction);
              
            }
        }

        [When(@"j'ajoute une transaction de type ""(.*)"" avec un montant de (.*) et une description ""(.*)""")]
        public void WhenJAjouteUneTransactionDeTypeAvecUnMontantDeEtUneDescription(
            string type,
            decimal amount,
            string description)
        {
            var transactionType = type == "Crédit" ? TransactionType.Credit : TransactionType.Debit;

            var transaction = TransactionFactory.CreateTransaction(
                transactionType,
                DateTime.Now,
                amount,
                description);

            (_transactionResult, _errors) =  _transactionService.ProcessTransaction(transaction);
            _transactions.Add(transaction);
        }

        [When(@"j'ajoute les transactions suivantes :")]
        public void WhenIAddTheFollowingTransactions(Table table)
        {
            foreach (var row in table.Rows)
            {
                var type = row["Type"];
                var amount = decimal.Parse(row["Amount"]);
                var description = row["Description"];

                var transactionType = type == "Credit" ? TransactionType.Credit : TransactionType.Debit;

                var transaction = TransactionFactory.CreateTransaction(
                    transactionType,
                    DateTime.Now,
                    amount,
                    description);

                _transactions.Add(transaction);
                (_,_errors)=_transactionService.ProcessTransaction(transaction);
            }
        }

        [Then(@"le solde doit être de (.*)")]
        public void ThenLeSoldeDoitEtreDe(decimal expectedBalance)
        {
            var balance =  _transactionService.GetBalance();
            balance.Should().Be(expectedBalance);
        }

        [Then(@"l'historique doit contenir une transaction avec la description ""(.*)""")]
        public void ThenLHistoriqueDoitContenirUneTransactionAvecLaDescription(string description)
        {
            var transactions = _transactionService.GetTransactions();
            transactions.Should().Contain(t => t.Description == description);
        }
        [Then(@"la transaction doit échouer avec le message d'erreur :")]
        public void Then(Table table)
        {
            foreach (var row in table.Rows)
            {
                var error = row["Error"];
                _errors.Should().Contain(error);
            }
               
        }

        [Then(@"la transaction doit échouer")]
        public void ThenLaTransactionDoitEchouer()
        {
            _transactionResult.Should().BeFalse();
        }

        [Then(@"l'historique des transactions devrait contenir :")]
        public void ThenTheTransactionHistoryShouldContain(Table table)
        {
            var history =  _transactionService.GetTransactions();

            foreach (var row in table.Rows)
            {
                var type = row["Type"];
                var amount = decimal.Parse(row["Amount"]);
                var description = row["Description"];

                var transactionType = type == "Credit" ? TransactionType.Credit : TransactionType.Debit;

                history.Should().ContainSingle(t =>
                    t.Type == transactionType &&
                    t.Amount == amount &&
                    t.Description == description);
            }
        }

        [Then(@"le solde doit rester à (.*)")]
        public void ThenLeSoldeDoitResterA(decimal expectedBalance)
        {
            var balance =  _transactionService.GetBalance();
            balance.Should().Be(expectedBalance);
        }
    }
}