using TransactionManagement.Core.Factories;
using TransactionManagement.Core.Models.Transactions;
using TransactionManagement.Core.Services.Transactions;
namespace TransactionManagement.ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            var transactionService = new TransactionServiceImpl();

            while (true)
            {
                string choice = DisplayMenu();

                switch (choice)
                {
                    case "1":
                        AddTransaction(transactionService);
                        break;
                    case "2":
                        GetBalance(transactionService);
                        break;
                    case "3":
                        ShowTransactionHistory(transactionService);
                        break;
                    case "4":
                        return;
                }
            }
        }

        static void AddTransaction(ITransactionService transactionService)
        {
            Console.WriteLine("Type (1: Crédit, 2: Débit):");
            var type = Console.ReadLine() == "1" ? TransactionType.Credit : TransactionType.Debit;

            Console.WriteLine("Description:");
            var description = Console.ReadLine();

            Console.WriteLine("Montant:");
            decimal.TryParse(Console.ReadLine(), out decimal amount);

            var transaction = TransactionFactory.CreateTransaction(
                type,
                DateTime.Now,
                amount,
                description);

            transactionService.ProcessTransaction(transaction);
        }

        private static void GetBalance(TransactionServiceImpl transactionService)
        {
            var balance = transactionService.GetBalance();
            Console.WriteLine($"Solde actuel : {balance}");
        }

        private static string DisplayMenu()
        {
            Console.WriteLine("===============================");
            Console.WriteLine("\n1. Ajouter une transaction");
            Console.WriteLine("2. Voir le solde");
            Console.WriteLine("3. Voir l'historique");
            Console.WriteLine("4. Quitter");
            Console.WriteLine("===============================\n");

            var choice = Console.ReadLine();
            return choice;
        }
        static void ShowTransactionHistory(ITransactionService transactionService)
        {
            var transactions = transactionService.GetTransactions();
        
            foreach (var transaction in transactions)
            {
                Console.WriteLine($"{transaction.Date:g} - {transaction.Type} - {transaction.Amount} - {transaction.Description}");
            }
            var balance = transactionService.GetBalance();
            Console.WriteLine($"Solde actuel : {balance}");
        }
    }
}
