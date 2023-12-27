using APICODEBASE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace APICODEBASE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly MyDBContext _context;

        public AdminController(MyDBContext context)
        {

            _context=context;
        }

        [HttpGet]

        public IActionResult Get()
        {

            try {
                var AdminData = _context.Admin.ToList();

                if (AdminData.Count==0)
                {

                    return NotFound("Admin Data not found");
                }

                return Ok(AdminData);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpGet  ("{id}")]
        public IActionResult Get(int id)
        {

            try
            {
                var AdminIdData = _context.Admin.Find(id);

                if (AdminIdData==null)
                {

                    return NotFound($"Admin Details not found with id {id}");
                }

                return Ok(AdminIdData);

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


        [HttpPost]
        public IActionResult Post(Admin admin)
        {

            try
            {
                _context.Add(admin);
                _context.SaveChanges();

                return Ok("Admin Created");

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        public IActionResult Put(Admin admin)
        {


          if(admin == null || admin.AdminId == 0)

            {

                if(admin == null)
                {
                    return BadRequest("Admin data is Invalid");
                }

                if (admin.AdminId == 0)
                {
                    return BadRequest($"Admin id {admin.AdminId} is Invalid");
                }
            }

            try
            {
                var AdminDataInfo = _context.Admin.Find(admin.AdminId);
                if(AdminDataInfo==null)
                {
                    return NotFound($"Admin id not found { admin.AdminId}");
                }

                AdminDataInfo.firstName = admin.firstName;
                AdminDataInfo.lastName = admin.lastName;
                AdminDataInfo.email = admin.email;
                AdminDataInfo.password = admin.password;
                AdminDataInfo.phoneNumber = admin.phoneNumber;
                AdminDataInfo.street = admin.street;
                AdminDataInfo.city = admin.city;
                AdminDataInfo.pincode = admin.pincode;
                AdminDataInfo.state = admin.state;
                AdminDataInfo.country = admin.country;
                _context.SaveChanges();
                return Ok("Admin Details Updated");
               

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
                var AdminDataDelete = _context.Admin.Find(id);
                if (AdminDataDelete==null)
                {
                    return NotFound($"Admin id not found with {id}");
                }

                _context.Admin.Remove(AdminDataDelete);
                _context.SaveChanges();
                return Ok("Admin Details Deleted");


            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }


    }
}
