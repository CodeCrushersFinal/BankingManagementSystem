using BMSWebApi.Models;

namespace BMSWebApi.Model
{
    public class Account
    {
        public int AccountId { get; set; }
        public int CustomerId {  get; set; }
        public string AccountType {  get; set; }
        public decimal Balance {  get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsActive {  get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }  
        public virtual ICollection<Receiver> Recievers { get; set; }
    }
}
