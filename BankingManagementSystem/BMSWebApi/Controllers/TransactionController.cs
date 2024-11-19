using BMSWebApi.DTO;
using BMSWebApi.Model;
using BMSWebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace BMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly BMSDbContext _context;
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(BMSDbContext context, ILogger<TransactionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// This action is to get all the transactions list
        /// </summary>
        /// <returns>list of transactions</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
        {
            _logger.LogInformation("Fetched all the transactions data");
            return await _context.Transactions.ToListAsync();
        }


        /// <summary>
        /// This action will get the transaction by id
        /// </summary>
        /// <param name="id">Particular Id of the transaction for which you want the transaction details.</param>
        /// <returns>Transaction</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransactionById(int id)
        {
            var transaction = await _context.Transactions.FindAsync(id);
            _logger.LogInformation($"Fetched the transaction of {id}");
            if (transaction == null)
            {
                return NotFound();
            }
            return transaction;
        }

        /// <summary>
        /// This action is to create a transaction and update in database
        /// </summary>
        /// <param name="transactionDTO">New Transaction details</param>
        /// <returns>The updated new record details</returns>

        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(TransactionDTO transactionDTO)
        {
            Transaction transaction = new Transaction();
            transaction.TransactionId = transactionDTO.TransactionId;
            transaction.AccountId = transactionDTO.SenderAccountId;
            transaction.Amount = transactionDTO.Amount;
            transaction.TransactionType = transactionDTO.TransactionType;
            transaction.TransactionDate = transactionDTO.TransactionDate;
            
            Receiver receiver = new Receiver();
            receiver.TransactionId = transactionDTO.TransactionId;
            receiver.AccountId = transactionDTO.ReceiverAccountId;
            

            var resultParam = new SqlParameter
            {
                ParameterName = "@Result",
                SqlDbType = SqlDbType.VarChar,
                Direction = ParameterDirection.Output,
                Size = 500
            };


            try
            {
                await _context.Database.ExecuteSqlInterpolatedAsync(
                $"EXEC DoTransaction @SenderId = {transactionDTO.SenderAccountId}, @ReceiverId = {transactionDTO.ReceiverAccountId}, @Amount = {transactionDTO.Amount}, @Result = {resultParam} OUTPUT");

                string resultMessage = resultParam.Value.ToString();

                if (resultMessage != "Transaction Successfully done ")
                {
                    return BadRequest(new { Message = resultMessage });
                }


                _context.Receivers.Add(receiver);

                //Write your logic here
                _context.Transactions.Add(transaction);

                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();

                _logger.LogInformation($"Created the transaction with ID: {transaction.TransactionId}");

                // Return success response
                return CreatedAtAction("GetTransactionById", new { id = transaction.TransactionId }, transaction);
            }
            catch (DbUpdateException)
            {
                if (TransactionExists(transaction.TransactionId))
                {
                    return Conflict(new { Message = "Transaction already exists." });
                }
                else
                {
                    throw;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Error during transaction processing: {ex.Message}");
                return StatusCode(500, new { Message = "An error occurred while processing the transaction.", Details = ex.Message });
            }

        }

        /// <summary>
        /// This method is to know whether the transaction is present.
        /// </summary>
        /// <param name="id">Transaction Id</param>
        /// <returns>True or False</returns>

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.TransactionId == id);
        }

    }
}
