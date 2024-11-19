namespace BMSWebApi.Model
{
    public class Loan
    {
        public int LoanId { get; set; }
        public int CustomerId {  get; set; }
        public decimal LoanAmount {  get; set; }
        public decimal InterestRate {  get; set; }
        public string Status {  get; set; }
        public DateTime ApplicationDate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
