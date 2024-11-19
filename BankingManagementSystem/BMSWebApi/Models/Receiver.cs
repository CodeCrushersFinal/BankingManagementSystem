using BMSWebApi.Model;
using System.Text.Json.Serialization;


namespace BMSWebApi.Models
{
    public class Receiver
    {
        public int ReceiverId { get; set; }
        public int TransactionId {  get; set; }
        public int AccountId {  get; set; }

        [JsonIgnore]
        public virtual Transaction Transaction { get; set; }
        public virtual Account Account { get; set; }
    }
}
