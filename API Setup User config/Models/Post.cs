using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Setup_User_config.Models
{
    public partial class Post
    {
        public Post(int id, string match)
        {
            this.id = id;
            this.match = match;
        }

        public int id { get; set; }
        public string match { get; set; }
    }
}
