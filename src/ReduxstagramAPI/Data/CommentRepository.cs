using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ReduxstagramAPI.Interfaces;
using ReduxstagramAPI.Models;

namespace ReduxstagramAPI.Data
{
    public class CommentRepository : BaseRepository<Comment>
    {
        private readonly DataContext _context = null;

        public CommentRepository(IOptions<Settings> settings)
        {
            _context = new DataContext(settings);
        }

        public async Task<bool> DeleteById(string id)
        {
            var result = await _context.Comments.DeleteOneAsync(
                          Builders<Comment>.Filter.Eq("_id", id));

            return result.DeletedCount > 0;
        }

        public async Task<IEnumerable<Comment>> GetAll()
        {
            var comments = await _context.Comments.Find(_ => true).ToListAsync();
            return comments;
        }

        public async Task<Comment> GetById(string id)
        {
            var filter = Builders<Comment>.Filter.Eq("_id", id);
            var comment = await _context.Comments.Find(filter).FirstOrDefaultAsync();
            return comment;
        }

        public async void Insert(Comment entity)
        {
            await _context.Comments.InsertOneAsync(entity);
        }

        public async void UpdateById(string id, Comment entity)
        {
            var filter = Builders<Comment>.Filter.Eq("_id", id);
            var update = Builders<Comment>.Update
                                .Set(s => s.text, entity.text);
            var result = await _context.Comments.UpdateOneAsync(filter, update);
        }
    }
}
