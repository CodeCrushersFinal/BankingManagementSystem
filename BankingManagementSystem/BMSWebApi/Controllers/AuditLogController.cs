using BMSWebApi.Model;
using BMSWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditLogController : ControllerBase
    {
        private readonly BMSDbContext _context;

        public AuditLogController(BMSDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuditLog>>> GetAuditLogs()
        {
            var auditLogs = await _context.AuditLogs
                .Include(a => a.Customer)  // Include the Supplier data
                .Select(auditLog => new
                {
                    auditLog.AuditLogId,
                    auditLog.Action,
                    auditLog.Description,
                    auditLog.ActionDate,
                    SupplierId = auditLog.Customer.CustomerId,  // Select only SupplierId
                    SupplierName = auditLog.Customer.FirstName      // Select only SupplierName
                })
                .ToListAsync();

            return Ok(auditLogs);
        }


        // POST: api/AuditLog/LogAction
        [HttpPost("LogAction")]
        public async Task<IActionResult> LogAction(int customerId, string action, string description)
        {
            // Validate input
            if (string.IsNullOrEmpty(action) || string.IsNullOrEmpty(description))
            {
                return BadRequest("Action and description are required.");
            }

            // Create a new audit log entry
            var auditLog = new AuditLog
            {
                CustomerId = customerId,   // Associate with a specific Supplier
                Action = action,
                Description = description,
                ActionDate = DateTime.UtcNow   // Set the current time for ActionDate
            };

            // Add the new audit log to the context and save changes
            await _context.AuditLogs.AddAsync(auditLog);
            await _context.SaveChangesAsync();

            return Ok("Action logged successfully.");
        }
    }
}
