using BMSWebApi.DTO;
using BMSWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly BMSDbContext _context;
        private readonly ILogger<LoanController> _logger;
        public LoanController(BMSDbContext context, ILogger<LoanController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// This action is to get all the loans list
        /// </summary>
        /// <returns>list of loans</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Loan>>> GetLoans()
        {
            _logger.LogInformation("Fetched all the loans data");
            return await _context.Loans.ToListAsync();
        }


        /// <summary>
        /// This action will get the loan by id
        /// </summary>
        /// <param name="id">Particular Id of the loan for which you want the loan details.</param>
        /// <returns>Loan</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<Loan>> GetLoanById(int id)
        {
            var loan = await _context.Loans.FindAsync(id);
            _logger.LogInformation($"Fetched the loan of {id}");
            if (loan == null)
            {
                return NotFound();
            }
            return loan;
        }


        /// <summary>
        /// This action helps us to edit the loan details
        /// </summary>
        /// <param name="id">LoanId and Modified Loan Details</param>
        /// <param name="loanDTO"></param>
        /// <returns>Nothing</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLoan(int id, LoanDTO loanDTO)
        {
            Loan loan = new Loan();
            loan.LoanId = loanDTO.LoanId;
            loan.CustomerId = loanDTO.CustomerId;
            loan.LoanAmount = loanDTO.LoanAmount;
            loan.InterestRate = loanDTO.InterestRate;
            loan.Status = loanDTO.Status;
            loan.ApplicationDate = loanDTO.ApplicationDate;


            //Write your logic here

            if (id != loan.LoanId)
            {
                return BadRequest();
            }

            _context.Entry(loan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated the loan of {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            //By Deepthi

            return NoContent();
        }

        /// <summary>
        /// This action is to create a loan and update in database
        /// </summary>
        /// <param name="loanDTO">New Loan details</param>
        /// <returns>The updated new record details</returns>

        [HttpPost]
        public async Task<ActionResult<Loan>> PostLoan(LoanDTO loanDTO)
        {
            Loan loan = new Loan();
            loan.LoanId = loanDTO.LoanId;
            loan.CustomerId = loanDTO.CustomerId;
            loan.LoanAmount = loanDTO.LoanAmount;
            loan.InterestRate = loanDTO.InterestRate;
            loan.Status = loanDTO.Status;
            loan.ApplicationDate = loanDTO.ApplicationDate;

            try
            {
                _context.Loans.Add(loan);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Created the loan of {loan.LoanId}");
            }
            catch (DbUpdateException)
            {
                if (LoanExists(loan.LoanId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during loan amount sending processing: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while processing the transfer of loan amount.", Details = ex.Message });
            }

            return CreatedAtAction("GetLoanById", new { id = loan.LoanId }, loan);
        }

        [HttpGet("Pending")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetPendingLoans()
        {
            var pendingLoans = await _context.Loans
                .Where(loan => loan.Status == "Waiting for approval") // Only include pending loans
                .ToListAsync();

            if (!pendingLoans.Any())
            {
                return Ok(new { Message = "No pending loans found.", Loans = new List<Loan>() });
            }

            return Ok(pendingLoans);
        }

        [HttpPut("Approve/{id}")]
        public async Task<IActionResult> ApproveLoan(int id)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
            {
                return NotFound(new { Message = "Loan not found." });
            }

            if (loan.Status == "Approved")
            {
                return BadRequest(new { Message = "Loan is already approved." });
            }

            loan.Status = "Approved";

            var resultParam = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 500
            };

            try
            {
                // Update the loan status to "Approved"
                _context.Entry(loan).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                // Call the LoanAmountSent stored procedure
                await _context.Database.ExecuteSqlInterpolatedAsync(
                    $"EXEC LoanAmountSent @LoanId = {loan.LoanId}, @CustomerId = {loan.CustomerId}, @LoanAmount = {loan.LoanAmount}, @Result = {resultParam} OUTPUT");

                // Check the result message from the stored procedure
                string resultMessage = resultParam.Value.ToString();
                if (resultMessage != "Loan amount sent successfully!")
                {
                    return BadRequest(new { Message = resultMessage });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error approving loan {id}: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while approving the loan.", Details = ex.Message });
            }

            return NoContent();
        }


        [HttpGet("Approved")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetApprovedLoans()
        {
            var approvedLoans = await _context.Loans
                .Where(loan => loan.Status == "LoanAmountSent")
                .ToListAsync();

            if (!approvedLoans.Any())
            {
                return NotFound(new { Message = "No approved loans found." });
            }

            return Ok(approvedLoans);
        }

        // PUT: api/Loans/Reject/5
        [HttpPut("Reject/{id}")]
        public async Task<IActionResult> RejectLoan(int id)
        {
            var loan = await _context.Loans.FindAsync(id);

            if (loan == null)
            {
                return NotFound(new { Message = "Loan not found." });
            }

            if (loan.Status == "Rejected")
            {
                return BadRequest(new { Message = "Loan is already rejected." });
            }

            loan.Status = "Rejected";

            try
            {
                _context.Entry(loan).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while rejecting the loan.", Details = ex.Message });
            }

            return NoContent();
        }

        // GET: api/Loans/Rejected
        [HttpGet("Rejected")]
        public async Task<ActionResult<IEnumerable<Loan>>> GetRejectedLoans()
        {
            var rejectedLoans = await _context.Loans
                .Where(loan => loan.Status == "Rejected")
                .ToListAsync();

            // If no rejected loans are found, return a message with empty list
            if (!rejectedLoans.Any())
            {
                return Ok(new { Message = "No loans rejected", Loans = new List<Loan>() });
            }

            return Ok(rejectedLoans); // Return the list of rejected loans
        }

        /// <summary>
        /// This method is to know whether the loan is present.
        /// </summary>
        /// <param name="id">Loan Id</param>
        /// <returns>True or False</returns>

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.LoanId == id);
        }
    }
}
