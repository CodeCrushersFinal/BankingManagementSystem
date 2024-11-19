using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BMSWebApi.DTO
{
    public class TransactionDTO
    {
        [Required]
        public int TransactionId { get; set; }

        [Required]
        public int SenderAccountId { get; set; }

        [Required]
        public int ReceiverAccountId { get; set; }

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public string TransactionType { get; set; }

        [Required]
        public DateTime TransactionDate { get; set; }


    }
}
