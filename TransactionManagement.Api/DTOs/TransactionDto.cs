using TransactionManagement.Core.Models.Transactions;

namespace TransactionManagement.Api.DTOs
{
    /// <summary>  
    /// Représente une transaction financière.  
    /// </summary>
    public class TransactionDto
    {
        /// <summary>  
        /// Date de la transaction.  
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>  
        /// Montant de la transaction.  
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>  
        /// Description de la transaction.  
        /// </summary>
        public string Description { get; set; }
        /// <summary>  
        /// Type de la transaction (Crédit ou Débit).  
        /// </summary>
        public TransactionType Type { get; set; }
    }
}
