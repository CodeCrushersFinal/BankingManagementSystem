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
        public async Task<ActionResult<Customer>> PostCustomer(CustomerDTO customerDTO)
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
