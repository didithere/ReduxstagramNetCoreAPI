using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReduxstagramAPI.Interfaces;
using ReduxstagramAPI.Models;

namespace ReduxstagramAPI.Data
{
    public class PostRepository : BaseRepository<Post>
    {
        private readonly DataContext _context = null;

        public PostRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<bool> DeleteById(string id)
        {
            var result = await _context.Posts.DeleteOneAsync(
                          Builders<Post>.Filter.Eq("_id", id));

            return result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Post>> GetAll()
        {
            var posts = await _context.Posts.Find(_ => true).ToListAsync();
            return posts;
        }

        public async Task<Post> GetById(string id)
        {
            var filter = Builders<Post>.Filter.Eq("_id", id);
            var post = await _context.Posts.Find(filter).FirstOrDefaultAsync();
            return post;
        }

        public async void Insert(Post entity)
        {
            await _context.Posts.InsertOneAsync(entity);
        }

        public async void UpdateById(string id, Post entity)
        {
            var filter = Builders<Post>.Filter.Eq("_id", id);

            if (entity == null)
            {
                entity = _context.Posts.Find(filter).FirstOrDefault(); 
            }

            var update = Builders<Post>.Update
                                .Set(s => s.likes, entity.likes + 1);
            var result = await _context.Posts.UpdateOneAsync(filter, update);
        }
    }
}
