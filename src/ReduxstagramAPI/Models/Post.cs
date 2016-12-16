using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ReduxstagramAPI.Models
{
    public class Post
    {
        [BsonId]
        public string _id { get; set; }
        public string code { get; set; }
        public string caption { get; set; }
        public int likes { get; set; }
        public string display_src { get; set; }
    }
}
