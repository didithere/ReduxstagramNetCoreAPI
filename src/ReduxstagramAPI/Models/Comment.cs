using MongoDB.Bson.Serialization.Attributes;

namespace ReduxstagramAPI.Models
{
    public class Comment
    {
        [BsonId]
        public string _id { get; set; }
        public string code { get; set; }
        public string text { get; set; }
        public string user { get; set; }
    }
}
