using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using API_Setup_User_config.Models;
using MongoDB.Driver.Linq;

namespace API_Setup_User_config.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public List<List<UserClass>> Database = new List<List<UserClass>>();
        public IMongoDatabase client = new MongoClient("mongodb://GateKeeper:silvereye@localhost:27017").GetDatabase("Virksomhed");

        void dbSetup()
        {
            var userQuery = from c in client.GetCollection<UserClass>(UserClass.Name).AsQueryable()
                            select c;
            foreach (UserClass output in userQuery)
            {
                List<UserClass> userClasses = new List<UserClass>{ output };
                Database.Add(userClasses);
            }
        }
        // GET: api/User
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                dbSetup();
                return Ok(Database);
            }
            catch (Exception e)
            {
                return Ok($"Error! \n\n{e.Message}");
            }
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public ActionResult Get(int id)
        {

            return Ok (from c in client.GetCollection<UserClass>(UserClass.Name).AsQueryable()
                   where c._id == id
                   select c);
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
