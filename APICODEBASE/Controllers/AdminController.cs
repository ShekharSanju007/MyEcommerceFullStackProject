using APICODEBASE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using BCrypt.Net;
using System.IdentityModel.Tokens.Jwt;
using System;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

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

     
        [Authorize]
        [HttpGet]

        public async Task<ActionResult<Admin>>  Get()
        {

            try {



                var AdminData = await _context.Admin.ToListAsync();

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
        [Route("Register")]
        public async Task<ActionResult> Register(Admin admin)
        {
            try
            {
                if (admin == null)
                {
                    return BadRequest("Invalid Admin data");
                }

                if (_context.Admin.Any(a => a.email == admin.email))
                {
                    return Conflict(new { message = "Email already exists" });
                }

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(admin.password);
                admin.password = hashedPassword;

               _context.Admin.Add(admin);
                await _context.SaveChangesAsync();

                return Ok(new { message = "User registered successfully" });
            }

            catch (DbUpdateException ex)
            {
               
                return Conflict(new { message = "User registration failed due to a database conflict: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(Admin admin)
        {
            try
            {
                var loginData = await _context.Admin.FirstOrDefaultAsync(a => a.email == admin.email);


                if (loginData == null || !BCrypt.Net.BCrypt.Verify(admin.password, loginData.password))
                {
                    return Unauthorized(new { message = "Invalid email or password" });
                }

               
                loginData.JwtToken = CreateJwtToken(loginData);
                await _context.SaveChangesAsync();

                return Ok(new     
               {

                    Token = loginData.JwtToken,
                    message = "Login details are successful" });
                
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



        private string CreateJwtToken(Admin admin)
       {

            var key = Encoding.ASCII.GetBytes("veryverysecretkeyjwtbdfsgthsfgdfgsfdgfdfddgdsfgdfgfdfgdgddffgdfdgffgdgfdgfd");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, admin.Role ?? ""),
                 new Claim(ClaimTypes.Email, admin.email ?? "")
        
                };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                NotBefore = DateTime.UtcNow,  
                Expires = DateTime.UtcNow.AddSeconds(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }



    }
}
