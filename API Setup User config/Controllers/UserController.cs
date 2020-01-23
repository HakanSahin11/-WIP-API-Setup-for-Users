using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using API_Setup_User_config.Models;
using MongoDB.Driver.Linq;
using System.Text.Json;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authentication;

namespace API_Setup_User_config.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        public List<List<UserClass>> DatabaseGet = new List<List<UserClass>>();
        public UserClass DatabaseGetOne { get;set; }
        public SaltClass DatabasePost { get; set; }

        bool dbSetup(string user, string pass, string usage, int? value, string match)
        {
            IMongoDatabase client = new MongoClient($"mongodb://{user}:{pass}@localhost:27017").GetDatabase("Virksomhed");
            var result = true;

            if (usage == "get")
            {

                var userQuery = from c in client.GetCollection<UserClass>(UserClass.Name).AsQueryable()
                                select c;

                foreach (UserClass output in userQuery)
                {
                    List<UserClass> userClasses = new List<UserClass> { output };
                    DatabaseGet.Add(userClasses);
                }
            }
            else if (usage == "getOne")
            {
             var usageQuery =
                from c in client.GetCollection<UserClass>(UserClass.Name).AsQueryable()
                where c._id == value
                select c;

                foreach (UserClass output in usageQuery)
                {
                    DatabaseGetOne = output;
                }
            }
            else if (usage == "post")
            {
                var usageQuery = from c in client.GetCollection<SaltClass>(SaltClass.Name).AsQueryable()
                                 where c._id == value
                                 select c;

                foreach (SaltClass output in usageQuery)
                {
                    DatabasePost = output;
                }
                dbSetup("GateKeeper", "silvereye", "getOne", value, null);
                if (
                Sha256.Sha256Hash(match, DatabasePost.Salt) != DatabaseGetOne.Password)
                {
                    result = false;
                }
            }
            return result;
        }
        // GET: api/User
        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                dbSetup("GateKeeper","silvereye", "get", null, null);
                return Ok(DatabaseGet);
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
            dbSetup("GateKeeper", "silvereye", "getOne", id, null);
            return Ok(DatabaseGetOne);
        }

        // POST: api/User
        [HttpPost]
        public ActionResult Post([FromBody] JsonElement json)
        {
            string match = json.GetString("match").ToString();

            int id = Convert.ToInt32( json.GetString("id"));

            Post post = new Post(id, match); 
            //  var test = JsonConvert.DeserializeObject<Post>(json);

            return Ok(dbSetup("System", "silvereye", "post", post.id, post.match));
           // return Ok();
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
