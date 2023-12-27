using APICODEBASE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICODEBASE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {

        private readonly MyDBContext _context;
        public CustomerController(MyDBContext context)
        {

            _context=context;
        }

        [HttpGet]

        public IActionResult Get()
        {

            try
            {
                var CustData = _context.Customer.ToList();

                if (CustData.Count==0)
                {

                    return NotFound("Customer Data not found");
                }

                return Ok(CustData);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {

            try
            {
                var CustIdData = _context.Customer.Find(id);

                if (CustIdData==null)
                {

                    return NotFound($"Customer Details not found with id {id}");
                }

                return Ok(CustIdData);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public IActionResult Post(Customer customer)
        {

            try
            {
                _context.Add(customer);
                _context.SaveChanges();

                return Ok("Customer Created");

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public IActionResult Put(Customer customer)
        {


            if (customer == null || customer.CustomerId == 0)

            {

                if (customer == null)
                {
                    return BadRequest("Customer data is Invalid");
                }

                if (customer.CustomerId == 0)
                {
                    return BadRequest($"Customer id {customer.CustomerId} is Invalid");
                }
            }

            try
            {
                var CustDataInfo = _context.Admin.Find(customer.CustomerId);
                if (CustDataInfo==null)
                {
                    return NotFound($"Customer id not found {customer.CustomerId}");
                }

                CustDataInfo.firstName = customer.firstName;
                CustDataInfo.lastName = customer.lastName;
                CustDataInfo.email = customer.email;
                CustDataInfo.password = customer.password;
                CustDataInfo.phoneNumber = customer.phoneNumber;
                CustDataInfo.street = customer.street;
                CustDataInfo.city = customer.city;
                CustDataInfo.pincode = customer.pincode;
                CustDataInfo.state = customer.state;
                CustDataInfo.country = customer.country;
                _context.SaveChanges();
                return Ok("Customer Details Updated");


            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpDelete]
        public IActionResult Delete(int id)
        {


            try
            {
                var CustDataDelete = _context.Customer.Find(id);
                if (CustDataDelete==null)
                {
                    return NotFound($"Customer id not found with {id}");
                }

                _context.Customer.Remove(CustDataDelete);
                _context.SaveChanges();
                return Ok("Customer Details Deleted");


            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }










    }
}
