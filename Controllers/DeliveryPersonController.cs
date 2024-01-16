using APICODEBASE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICODEBASE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeliveryPersonController : ControllerBase
    {
        private readonly MyDBContext _context;

        public DeliveryPersonController(MyDBContext context)
        {

            _context=context;
        }

        [HttpGet]

        public IActionResult Get()
        {

            try
            {
                var DeliveryPersonData = _context.DeliveryPersonInfo.ToList();

                if (DeliveryPersonData.Count==0)
                {

                    return NotFound("DeliveryPerson Data not found");
                }

                return Ok(DeliveryPersonData);

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
                var DeliveryPersonIdData = _context.DeliveryPersonInfo.Find(id);

                if (DeliveryPersonIdData==null)
                {

                    return NotFound($"DeliveryPerson Details not found with id {id}");
                }

                return Ok(DeliveryPersonIdData);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public IActionResult Post(DeliveryPersonInfo delivery)
        {

            try
            {
                _context.Add(delivery);
                _context.SaveChanges();

                return Ok("DeliveryPerson data Created");

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public IActionResult Put(DeliveryPersonInfo delivery)
        {


            if (delivery == null || delivery.DeliveryPersonId == 0)

            {

                if (delivery == null)
                {
                    return BadRequest("DeliveryPerson data is Invalid");
                }

                if (delivery.DeliveryPersonId == 0)
                {
                    return BadRequest($"DeliveryPerson id {delivery.DeliveryPersonId} is Invalid");
                }
            }

            try
            {
                var deliveryDataInfo = _context.Admin.Find(delivery.DeliveryPersonId);
                if (deliveryDataInfo==null)
                {
                    return NotFound($"DeliveryPerson id not found {delivery.DeliveryPersonId}");
                }

                deliveryDataInfo.firstName = delivery.firstName;
                deliveryDataInfo.lastName = delivery.lastName;
                deliveryDataInfo.email = delivery.email;
                deliveryDataInfo.password = delivery.password;
                deliveryDataInfo.phoneNumber = delivery.phoneNumber;
                deliveryDataInfo.street = delivery.street;
                deliveryDataInfo.city = delivery.city;
                deliveryDataInfo.pincode = delivery.pincode;
                deliveryDataInfo.state = delivery.state;
                deliveryDataInfo.country = delivery.country;
                _context.SaveChanges();
                return Ok("DeliveryPerson Details Updated");


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
                var DeliveryDataDelete = _context.DeliveryPersonInfo.Find(id);
                if (DeliveryDataDelete==null)
                {
                    return NotFound($"DeliveryPerson id not found with {id}");
                }

                _context.DeliveryPersonInfo.Remove(DeliveryDataDelete);
                _context.SaveChanges();
                return Ok("DeliveryPerson Details Deleted");


            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
