using BsuAdmin.Data;
using BsuAdmin.Migrations;
using BsuAdmin.Models;
using BsuAdmin.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;

namespace BsuAdmin.Controllers
{
    // localhost:xxxx/api/AddNewPlate
    [Route("api/[controller]")]
    [ApiController]
    public class PlateNumberController : ControllerBase
    {
        private readonly AppDbContext dbContext;
        private readonly IHubContext<LogHub> hubContext;

        public PlateNumberController(AppDbContext dbContext, IHubContext<LogHub> hubContext)
        {
            this.dbContext = dbContext;
            this.hubContext = hubContext;
        }

        [HttpGet("GetRegisteredPlates")]
        public IActionResult GetAllNewPlates()
        {
            var allNewPlates = dbContext.NewPlates.Where(o => o.isRegistered.Equals(true)).ToList();

            return Ok(allNewPlates);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public IActionResult GetAllPlates(Guid id) 
        {
            var addnewplate = dbContext.NewPlates.Find(id);

            if (addnewplate is null)
            {
                return NotFound("Plate Number not found");
            }

            return Ok(addnewplate);
        }

        [HttpGet("GetRecentPlates")]
        public IActionResult GetRecentPlates()
        {
             var recentPlates = dbContext.NewPlates.OrderByDescending(p => p.Date).Take(5).Select(p => new AddPlateNumberDto
             {
                PlateNumber = p.PlateNumber,
                Name = p.Name,
                ContactNumber = p.ContactNumber,
                 Date = DateTime.Now
             }).ToList();

             return Ok(recentPlates);
         }

        [HttpGet("GetRecentMonitoredPlates")]
        public IActionResult GetRecentMonitoredPlates()
        {
            var recentMonitoredPlates = dbContext.checkPlates.OrderByDescending(p => p.DateLogs).Take(5).Select(p => new CheckPlatesDto
            {
                PlateNumber = p.PlateNumber,
                Name = p.Name,
                ContactNumber = p.ContactNumber,
                status = p.status,
                Entrance = p.Entrance, 
                Exit = p.Exit,
            }).ToList();

            return Ok(recentMonitoredPlates);
        }

        [HttpPost("AddNewPlate")]
        public IActionResult AddNewPlate(AddPlateNumberDto addPlateNumberDto)
        {

            var existingPlate = dbContext.NewPlates.FirstOrDefault(p => p.PlateNumber == addPlateNumberDto.PlateNumber && p.isRegistered == true);
            if (existingPlate is not null)
            {
                return BadRequest(new { message = "Plate Number already exists" });
            }

            var NewPlateEntity = new AddNewPlate
            {
                PlateNumber = addPlateNumberDto.PlateNumber,
                Name = addPlateNumberDto.Name,
                ContactNumber = addPlateNumberDto.ContactNumber,
                Date = DateTime.UtcNow, //PREVIOUS CODE: Date = addPlateNumberDto.Date
                isRegistered = true
            };

            dbContext.NewPlates.Add(NewPlateEntity);
            dbContext.SaveChanges();

            return Ok(new { message = "Added Successfully!" });
        }

        [HttpPut]
        public IActionResult UpdatePlateNumber(UpdatePlateNumberDto updatePlateNumberDto)
        {


            var existingPlate = dbContext.NewPlates.FirstOrDefault(p => p.PlateNumber == updatePlateNumberDto.PlateNumber);
            if (existingPlate is not null)
            {
                return BadRequest(new { message = "Plate Number already exists" });
            }

            var plateEntity = new AddNewPlate
            {
                PlateNumber = updatePlateNumberDto.PlateNumber,
                Name = updatePlateNumberDto.Name,
                ContactNumber = updatePlateNumberDto.ContactNumber,
                Date = DateTime.UtcNow
            };
            
            dbContext.SaveChanges();

            return Ok(new { message = "Update Successfully!" });
        }

        [HttpDelete]
        public IActionResult DeletePlateNumber(string plateNumber)
        {
            var existingPlate = dbContext.NewPlates.Where(p => p.PlateNumber == plateNumber).OrderByDescending(p => p.Date).FirstOrDefault();

            if (existingPlate is null)
            {
                return NotFound("Plate Number not found");
            }

            // ✅ Soft delete: just update the property
            existingPlate.isRegistered = false;

            dbContext.SaveChanges();

            return Ok(new { message = "Plate Number soft deleted successfully!" });
        }

        [HttpGet("get/{plateNumber}/{indicator}")]
        public IActionResult checkPlate([FromRoute] string plateNumber, string indicator)
        {
            var checkPlate = dbContext.NewPlates.Where(p => p.PlateNumber == plateNumber).OrderByDescending(p => p.Date).FirstOrDefault();
            
            var currentTimestamp = DateTime.UtcNow.AddHours(8);
            var formattedTimestamp = currentTimestamp.ToString("yyyy-MM-dd HH:mm:ss");

            
            if (indicator == "ENTRANCE")
            {
                if (checkPlate == null || checkPlate.isRegistered == false) 
                {
                    
                    var unrecognized = new CheckPlates
                    {
                        PlateNumber = plateNumber,
                        Name = "Unknown",
                        ContactNumber = string.Empty,
                        Entrance = formattedTimestamp, 
                        Exit = string.Empty,
                        status = false,
                        
                         
                        
                        DateLogs = formattedTimestamp
                    };

                    dbContext.checkPlates.Add(unrecognized);
                    dbContext.SaveChanges();

                    
                    hubContext.Clients.All.SendAsync("NewLog", unrecognized);

                    return Ok(new { message = "Plate Number not recognized. Access Denied." });
                }

                
                var recognized = new CheckPlates
                {
                    PlateNumber = checkPlate.PlateNumber,
                    Name = checkPlate.Name,
                    ContactNumber = checkPlate.ContactNumber,
                    Entrance = formattedTimestamp, 
                    Exit = string.Empty,
                    status = true,
                    DateLogs = formattedTimestamp 
                };
                dbContext.checkPlates.Add(recognized);
                dbContext.SaveChanges();

                
                hubContext.Clients.All.SendAsync("NewLog", recognized);

                return Ok(new { message = "Welcome to Bsu Campus!" });
            }

            
            if (indicator == "EXIT")
            {
                if (checkPlate == null || checkPlate.isRegistered == false) 
                {
                    
                    var unrecognized = new CheckPlates
                    {
                        PlateNumber = plateNumber,
                        Name = "Unknown",
                        ContactNumber = string.Empty,
                        Entrance = string.Empty,
                        Exit = formattedTimestamp, 
                        status = false,
                        DateLogs = formattedTimestamp 
                    };
                    dbContext.checkPlates.Add(unrecognized);
                    dbContext.SaveChanges();

                    
                    hubContext.Clients.All.SendAsync("NewLog", unrecognized);

                    return Ok(new { message = "Plate Number not recognized. Access Denied." });
                }

                
                var recognized = new CheckPlates
                {
                    PlateNumber = checkPlate.PlateNumber,
                    Name = checkPlate.Name,
                    ContactNumber = checkPlate.ContactNumber,
                    Entrance = string.Empty,
                    Exit = formattedTimestamp, 
                    status = true,
                    DateLogs = formattedTimestamp 
                };
                dbContext.checkPlates.Add(recognized);
                dbContext.SaveChanges();

                
                hubContext.Clients.All.SendAsync("NewLog", recognized);

                return Ok(new { message = "Goodbye!" });
            }

            return BadRequest(new { message = "Invalid indicator. Please use 'ENTRANCE' or 'EXIT'." });
        }

        [HttpGet("GetDashboardStats")]
        public IActionResult GetDashboardStats()
        {
            var totalRegistered = dbContext.NewPlates.Where(o => o.isRegistered == true).Count();
            var totalLogs = dbContext.checkPlates.Count();
            var registeredRecognitions = dbContext.checkPlates.Count(c => c.status == true);
            var unrecognized = dbContext.checkPlates.Count(c => c.status == false);

            return Ok(new
            {
                totalRegistered,
                totalLogs,
                registeredRecognitions,
                unrecognized
            });
        }


        
        [HttpGet("logs")]
        public IActionResult GetLogs()
        {
            var logs = dbContext.checkPlates
                .OrderByDescending(l => l.DateLogs)
                .Take(50)
                .ToList();
            return Ok(logs);
        }
    }
}
