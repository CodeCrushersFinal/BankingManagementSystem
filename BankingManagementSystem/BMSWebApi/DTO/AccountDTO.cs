using System.ComponentModel.DataAnnotations;

namespace BMSWebApi.DTO
{
    public class AccountDTO
    {
        [Required]
        public int AccountId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        public decimal Balance { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
}
