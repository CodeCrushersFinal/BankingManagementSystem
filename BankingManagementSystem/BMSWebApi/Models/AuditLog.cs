using BMSWebApi.Model;

namespace BMSWebApi.Models
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }
        public int CustomerId {  get; set; }
        public string Action {  get; set; }
        public string Description {  get; set; }
        public DateTime ActionDate { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
