namespace BMSWebApi.Model
{
    public class Customer
    {
        public int CustomerId {  get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email {  get; set; }
        public string PhoneNumber {  get; set; }
        public string Address { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId {  get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }
}
