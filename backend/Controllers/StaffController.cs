using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bogus;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BonelessPharmacyBackend.Controllers
{
    [Route("api/[controller]")]

    public class StaffController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<Staff>> Get() => await Task.Run<IEnumerable<Staff>>(() =>
        {
            using (var db = new Db())
            {
                // Ensure to call toList so that the DB doesn't dispose itself
                return db.Staff.Include(s => s.Role).Select(s => s.Safe).ToList();
            }
        });

        //Get api/Staff/5
        [HttpGet("{id}")]
        public async Task<Staff> Get(int id) => await Task.Run<Staff>(() =>
        {
            using (var db = new Db())
            {
                return db.Staff.Include(s => s.Role).Select(s => s.Safe).FirstOrDefault(s => s.Id == id);
            }
        });

        //  Post api/Staff/5
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody]Dictionary<string, string> value) => await Task.Run<IActionResult>(async () =>
        {
            if (!value.ContainsKey("name") || !value.ContainsKey("phone") || !value.ContainsKey("roleid"))
                return BadRequest("Staff creation requires name and phone number");
            string secret = $"{new Faker().Lorem.Word()}{new Random().Next(999)}";
            using (var db = new Db())
            {
                await db.Staff.AddAsync(new Staff(){
                    Name = value["name"],
                    Password = secret,
                    RoleId = Int32.Parse(value["roleid"]),
                    PhoneNumber = value["phone"]
                });
                await db.SaveChangesAsync();
                return Created("api/Staff", secret);
            }
        });

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Staff value) => await Task.Run<IActionResult>(async () =>
        {
            if (ModelState.IsValid)
            {
                value.Id = id;
                using (var db = new Db())
                {
                    db.Staff.Update(value);
                    await db.SaveChangesAsync();
                    return Accepted("api/Staff", value);
                }
            }
            else
                return BadRequest();
        });

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) => await Task.Run<IActionResult>(async () =>
        {
            using (var db = new Db())
            {
                db.Staff.Remove(db.Staff.FirstOrDefault(s => s.Id == id));
                await db.SaveChangesAsync();
                return Accepted();
            }
        });

    }
}