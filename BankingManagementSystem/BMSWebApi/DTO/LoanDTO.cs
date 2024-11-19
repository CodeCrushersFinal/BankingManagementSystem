using System.ComponentModel.DataAnnotations;

namespace BMSWebApi.DTO
{
    public class LoanDTO
    {
        [Required]
        public int LoanId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public decimal LoanAmount { get; set; }

        [Required]
        public decimal InterestRate { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        public DateTime ApplicationDate { get; set; }
    }
}
