using BMSWebApi.DTO;
using BMSWebApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BMSWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly BMSDbContext _context;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(BMSDbContext context, ILogger<CustomerController> logger)
        {
            _context = context;
            _logger = logger;
        }


        /// <summary>
        /// This action is to get all the customers list
        /// </summary>
        /// <returns>list of customers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
        {
            _logger.LogInformation($"Fetched all the customers");
            return await _context.Customers.ToListAsync();

        }


        /// <summary>
        /// This action will get the customer by id
        /// </summary>
        /// <param name="id">Particular Id of the customer for which you want the customer details.</param>
        /// <returns>Customer</returns>

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _logger.LogInformation($"Fetched the customer by {id}");
            if (customer == null)
            {
                return NotFound();
            }
            return customer;
        }

        [HttpGet("{customerId}/Accounts")]
        public IActionResult GetAccountsByCustomerId(int customerId)
        {
            // Fetch accounts where CustomerId matches
            var accounts = _context.Accounts
                .Where(a => a.CustomerId == customerId)
                .Select(a => new
                {
                    a.AccountId,
                    a.AccountType,
                    a.Balance,
                    a.CreatedDate,
                    a.IsActive
                })
                .ToList();

            if (accounts == null || !accounts.Any())
            {
                return NotFound($"No accounts found for Customer ID {customerId}.");
            }

            return Ok(accounts);
        }

        [HttpGet("{customerId}/AccountID")]
        public IActionResult GetAccountIDByCustomerId(int customerId)
        {
            // Fetch accounts where CustomerId matches
            var accounts = _context.Accounts
                .Where(a => a.CustomerId == customerId)
                .Select(a => a.AccountId)
                .ToList();

            if (accounts == null || !accounts.Any())
            {
                return NotFound($"No accounts found for Customer ID {customerId}.");
            }

            return Ok(accounts);
        }

        [HttpGet("{email}/AccountsByEmail")]
        public IActionResult GetAccountsByCustomerEmail(string email)
        {
            // Fetch accounts where CustomerId matches
            var customers = _context.Customers;
            int customerId=0;
            foreach (var customer in customers) 
            { 
                if(customer.Email == email)
                {
                    customerId = customer.CustomerId;
                }
            }
            if (customerId != 0)
            {
                var accounts = _context.Accounts
                .Where(a => a.CustomerId == customerId)
                .Select(a => a.AccountId )
                .ToList();
                return Ok(accounts);
            }

           else
            {
                return NotFound($"No accounts found for this email {email}");
            }
        }

        /// <summary>
        /// This action helps us to edit the customer details
        /// </summary>
        /// <param name="id">CustomerId and Modified Customer Details</param>
        /// <param name="customerDTO"></param>
        /// <returns>Nothing</returns>

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDTO customerDTO)
        {
            Customer customer = new Customer();
            customer.CustomerId = customerDTO.CustomerId;
            customer.FirstName = customerDTO.FirstName;
            customer.LastName = customerDTO.LastName;
            customer.Email = customerDTO.Email;
            customer.PhoneNumber = customerDTO.PhoneNumber;
            customer.Address = customerDTO.Address;
            customer.PasswordHash = customerDTO.PasswordHash;
            customer.RoleId = customerDTO.RoleId;

            //Write your logic here
            if (id != customer.CustomerId)
            {
                return BadRequest();
            }

            _context.Entry(customer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Updated the customer of {customer.CustomerId}");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomerExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// This action is to create a customer and update in database
        /// </summary>
        /// <param name="customerDTO">New Customer details</param>
        /// <returns>The updated new record details</returns>

        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer([FromBody] CustomerDTO customerDTO)
        {
            Customer customer = new Customer();
            customer.CustomerId = customerDTO.CustomerId;
            customer.FirstName = customerDTO.FirstName;
            customer.LastName = customerDTO.LastName;
            customer.Email = customerDTO.Email;
            customer.PhoneNumber = customerDTO.PhoneNumber;
            customer.Address = customerDTO.Address;
            customer.PasswordHash = customerDTO.PasswordHash;
            customer.RoleId = customerDTO.RoleId;

            //Write your logic here

            _context.Customers.Add(customer);
            try
            {
                await _context.SaveChangesAsync();
                _logger.LogInformation($"Created the customer with {customer.CustomerId}");
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }

            }


            return CreatedAtAction("GetCustomerById", new { id = customer.CustomerId }, customer );
        }

        [HttpGet("GetEmail/{email}")]
        public async Task<ActionResult<Customer>> GetCustomerByEmail(string email)
        {
            return Ok(await _context.Customers.FirstAsync(customers => customers.Email.Equals(email)));
        }

        [HttpGet("GetID/{email}")]
        public async Task<ActionResult> GetCustomerIDByEmail(string email)
        {
            int id = 0;
            var customers = _context.Customers;
            foreach (var customer in customers) 
            {
                if (customer.Email.Equals(email))
                {
                    id = customer.CustomerId;
                }
            }
            if(id != 0)
            {
                return Ok(id);
            }
            else
            {
                return BadRequest("Customer does not exists");
            }
        }


        /// <summary>
        /// This method is to know whether the account is present.
        /// </summary>
        /// <param name="id">Account Id</param>
        /// <returns>True or False</returns>

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.CustomerId == id);
        }
    }
}
