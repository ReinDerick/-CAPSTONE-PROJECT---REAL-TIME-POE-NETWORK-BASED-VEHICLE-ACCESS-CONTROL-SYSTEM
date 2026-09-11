using BsuAdmin.Data;
using BsuAdmin.Models;
using BsuAdmin.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BsuAdmin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {

        private readonly AppDbContext dbContext;

        public AdminController(AppDbContext dbContext)
        {
            this.dbContext = dbContext;

        }

        [HttpGet]

        public IActionResult GetAllAdmins()
        {
            var allAdmins = dbContext.Admins.ToList();

            return Ok(allAdmins);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public IActionResult UpdateAdmin(Guid id, AddAdminDto addAdminDto)
        {
            var existingAdmin = dbContext.Admins.Find(id);

            if (existingAdmin is null)
            {
                return NotFound("Admin User not found");
            }

            existingAdmin.UserName = addAdminDto.UserName;
            existingAdmin.Password = addAdminDto.Password;

            dbContext.SaveChanges();

            return Ok(existingAdmin);
        }

        [HttpPost("Login")]
        public IActionResult LogInAdmin(AddAdminDto addAdminDto) 
        { 
            try
            {
                var admin = dbContext.Admins.FirstOrDefault(a => a.UserName == addAdminDto.UserName && a.Password == addAdminDto.Password);
                if (admin is null)
                {
                    return NotFound(new { message = "Admin account not found" });
                }

                return Ok(new { message = "Login successful!" });
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
