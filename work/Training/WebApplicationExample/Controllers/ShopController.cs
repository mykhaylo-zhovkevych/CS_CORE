using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using WebApplicationExample.Models;
using static WebApplicationExample.Models.Customer;

namespace WebApplicationExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {

        static private List<Customer> _customers = new List<Customer>
        {
            new Customer
            {
                Name = "Alice",
                Id = 1,
                Age = 30,
                Email = "example@email.com",
                PhoneNumber = "123-456-7890",
                Addresses = new List<Customer.Address>
                {
                    new Customer.Address { Id = 1, Street = "St Main", City = "NewYork" },
                    new Customer.Address { Id = 2, Street = "St Quie", City = "Geneve" }
                },
                contacts = new List<Contact>
                {
                    new Contact
                    {
                        Id = 1,
                        Type = "Phone",
                        Detail = "123-456-7890"
                    },
                    new Contact
                    {
                        Id = 2,
                        Type = "Email"
                    },
                }
            },
            new Customer
            {
                Name = "Patrick",
                Id = 2,
                Age = 20,
                Email = "example@email.com",
                PhoneNumber = "123-456-7890",
                Addresses = new List<Customer.Address>
                {
                    new Customer.Address { Id = 1, Street = "St Main", City = "NewYork" },
                    new Customer.Address { Id = 2, Street = "St Quie", City = "Geneve" }
                },
            },
            new Customer
            {
                Name = "Jack",
                Id = 3,
                Age = 40,
                Email = "example@email.com",
                PhoneNumber = "123-456-7890",
                Addresses = new List<Customer.Address>
                {
                    new Customer.Address { Id = 1, Street = "St Main", City = "NewYork" },
                    new Customer.Address { Id = 2, Street = "St Quie", City = "Geneve" }
                },
            },
            new Customer
            {
                Name = "Tom",
                Id = 4,
                Age = 30,
                Email = "example@email.com",
                PhoneNumber = "123-456-7890",
                Addresses = new List<Customer.Address>
                {
                    new Customer.Address { Id = 1, Street = "St Main", City = "NewYork" },
                    new Customer.Address { Id = 2, Street = "St Quie", City = "Geneve" }
                },
            }

        };

        // Get /ape/customers
        [HttpGet]
        public ActionResult<List<Customer>> GetCustomers()
        {
            return Ok(_customers);
        }

        // Get /api/customers/{id}
        [HttpGet("{id}")]
        public ActionResult<Customer> GetCustomerId(int id)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // Post /api/customers/{id}/contacts
        [HttpPost("{id}/contacts")]
        public ActionResult<Contact> AddNewContactWithId(int id, [FromBody] Contact newContact)
        {
            var customer = _customers.FirstOrDefault(customer => customer.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            // Dont allow null properties

            customer.contacts.Add(newContact);

            return CreatedAtAction(nameof(GetCustomerId), new { id = customer.Id }, newContact);
        }

        // Put /api/customers/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateCustomer(int id, [FromBody] Customer updatedCustomer)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            customer.Name = updatedCustomer.Name;
            customer.Age = updatedCustomer.Age;
            customer.Email = updatedCustomer.Email;
            customer.PhoneNumber = updatedCustomer.PhoneNumber;
            customer.Addresses = updatedCustomer.Addresses;

            return NoContent();

        }

        // DELETE /api/customers/{customerId}/addresses/{addressId}
        [HttpDelete("{customerId}/addresses/{addressId}")]
        public ActionResult DeleteCustomerAddress(int customerId, int addressId)
        {
            var customer = _customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null) return NotFound();

            var address = customer.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null) return NotFound();

            customer.Addresses.Remove(address);
            return NoContent();
        }
    }
}
