using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReduxstagramAPI.Models;

namespace ReduxstagramAPI.Data
{
    public class DataContext
    {
        private readonly IMongoDatabase _database = null;

        public DataContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Post> Posts
        {
            get
            {
                return _database.GetCollection<Post>("posts");
            }
        }

        public IMongoCollection<Comment> Comments
        {
            get
            {
                return _database.GetCollection<Comment>("comments");
            }
        }
    }
}
