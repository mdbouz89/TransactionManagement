using Microsoft.AspNetCore.Mvc;
using TransactionManagement.Core.Services.Transactions;
using TransactionManagement.Core.Factories;
using TransactionManagement.Api.DTOs;

namespace TransactionManagement.Api.Controllers
{
    /// <summary>  
    /// Gère les transactions financières.  
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        /// <summary>  
        /// Crée une nouvelle transaction.  
        /// </summary>  
        /// <param name="transactionDto">Détails de la transaction.</param>  
        /// <returns>Résultat de la création de la transaction.</returns>
        [HttpPost]
        public IActionResult CreateTransaction([FromBody] TransactionDto transactionDto)
        {
            var transaction = TransactionFactory.CreateTransaction(
                transactionDto.Type,
                transactionDto.Date,
                transactionDto.Amount,
                transactionDto.Description);

            var (success,errors) = _transactionService.ProcessTransaction(transaction);
            if (success)
            {
                return Ok(new { Message = "Transaction traitée avec succès" });
            }

            return BadRequest(new { 
                Message = "Erreur lors du traitement de la transaction",
                Errors= errors});
        }

        /// <summary>  
        /// Récupère le solde actuel.  
        /// </summary>  
        /// <returns>Le solde actuel.</returns>
        [HttpGet("balance")]
        public IActionResult GetBalance()
        {
            var balance = _transactionService.GetBalance();
            return Ok(new { Balance = balance });
        }

        /// <summary>  
        /// Récupère l'historique des transactions.  
        /// </summary>  
        /// <returns>Liste des transactions.</returns>
        [HttpGet]
        public IActionResult GetTransactions()
        {
            var transactions = _transactionService.GetTransactions();
            return Ok(transactions);
        }
    }
}
