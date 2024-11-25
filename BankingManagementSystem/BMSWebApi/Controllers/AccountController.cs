using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BMSWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BMSWebApi.DTO;

namespace BMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly BMSDbContext _context;
        private readonly ILogger<AccountController> _logger;
        public AccountController(BMSDbContext context, ILogger<AccountController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// This action is to get all the accounts list
        /// </summary>
        /// <returns>list of accounts</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts()
        {
            _logger.LogInformation("Fetched all the accounts data");
            return await _context.Accounts.ToListAsync();
        }


        /// <summary>
        /// This action will get the account by id
        /// </summary>
        /// <param name="id">Particular Id of the account for which you want the account details.</param>
        /// <returns>Account</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<Account>> GetAccountById(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _logger.LogInformation($"Fetched the account of {id}");
            if (account == null)
            {
                return NotFound();
            }
            return account;
        }


        /// <summary>
        /// This action helps us to edit the account details
        /// </summary>
        /// <param name="id">AccountId and Modified Account Details</param>
        /// <param name="accountDTO"></param>
        /// <returns>Nothing</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAccount(int id, AccountDTO accountDTO)
        {
            Account account = new Account();
            account.AccountId = accountDTO.AccountId;
            account.CustomerId = accountDTO.CustomerId;
            account.AccountType = accountDTO.AccountType;
            account.Balance = accountDTO.Balance;
            account.CreatedDate = accountDTO.CreatedDate;
            account.IsActive = accountDTO.IsActive;

            //Write your logic here

            if (id != account.AccountId)
            {
                return BadRequest();
            }

            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated the account of {id}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(id))
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
        /// This action is to create a account and update in database
        /// </summary>
        /// <param name="accountDTO">New Account details</param>
        /// <returns>The updated new record details</returns>

        [HttpPost]
        public async Task<ActionResult<Account>> PostAccount(AccountDTO accountDTO)
        {
            Account account = new Account();
            account.AccountId = accountDTO.AccountId;
            account.CustomerId = accountDTO.CustomerId;
            account.AccountType = accountDTO.AccountType;
            account.Balance = accountDTO.Balance;
            account.CreatedDate = accountDTO.CreatedDate;
            account.IsActive = accountDTO.IsActive;

            //Write your logic here
            _context.Accounts.Add(account);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Created the account of {account.AccountId}");
            }
            catch (DbUpdateException)
            {
                if (AccountExists(account.AccountId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
                
            }

            //By Surabhi

            return CreatedAtAction("GetAccountById", new { id = account.AccountId }, account );
        }

        [HttpGet("{accountId}/Transactions")]
        public IActionResult GetTransactionsByAccountId(int accountId)
        {
            var transactions = _context.Transactions
                .Where(t => t.AccountId == accountId)
                .ToList();

            if (transactions == null || transactions.Count == 0)
            {
                return NotFound("No transactions found for the account.");
            }

            return Ok(transactions);
        }

        /// <summary>
        /// Toggles the IsActive status of an account.
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns>Updated account details</returns>
        [HttpPut("{id}/ToggleActiveStatus")]
        public async Task<IActionResult> ToggleActiveStatus(int id)
        {
            // Find the account by ID
            var account = await _context.Accounts.FindAsync(id);

            if (account == null)
            {
                _logger.LogWarning($"Account with ID {id} not found.");
                return NotFound($"Account with ID {id} not found.");
            }

            // Toggle the IsActive status
            account.IsActive = !account.IsActive;

            // Update the database
            _context.Entry(account).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Toggled IsActive status for account {id} to {account.IsActive}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while toggling IsActive status for account {id}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the account.");
            }

            return Ok(account);
        }


        /// <summary>
        /// This action is to deposit a certain amount into the account.
        /// </summary>
        /// <param name="accountId">Account ID to deposit into</param>
        /// <param name="amount">Amount to deposit</param>
        /// <returns>Updated Account balance</returns>
        [HttpPost("{accountId}/Deposit")]
        public async Task<ActionResult<Account>> Deposit(int accountId, [FromBody] decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            if (amount <= 0)
            {
                return BadRequest("Deposit amount must be greater than zero.");
            }

            account.Balance += amount;
            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Deposited {amount} into account {accountId}. New Balance: {account.Balance}");
                return Ok(new { Message = "Deposit successful", NewBalance = account.Balance });
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        /// <summary>
        /// This action is to withdraw a certain amount from the account.
        /// </summary>
        /// <param name="accountId">Account ID to withdraw from</param>
        /// <param name="amount">Amount to withdraw</param>
        /// <returns>Updated Account balance</returns>
        [HttpPost("{accountId}/Withdraw")]
        public async Task<ActionResult<Account>> Withdraw(int accountId, [FromBody] decimal amount)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return NotFound("Account not found.");
            }

            if (amount <= 0)
            {
                return BadRequest("Withdrawal amount must be greater than zero.");
            }

            if (account.Balance < amount)
            {
                return BadRequest("Insufficient funds.");
            }

            account.Balance -= amount;
            _context.Entry(account).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Withdrawn {amount} from account {accountId}. New Balance: {account.Balance}");
                return Ok(new { Message = "Withdrawal successful", NewBalance = account.Balance });
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


        /// <summary>
        /// This method is to know whether the account is present.
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns>True or False</returns>

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }

    }

}
