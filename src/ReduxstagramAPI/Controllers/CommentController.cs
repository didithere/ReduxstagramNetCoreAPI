using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReduxstagramAPI.Interfaces;
using ReduxstagramAPI.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ReduxstagramAPI.Controllers
{
    [Route("api/[controller]")]
    public class CommentController : Controller
    {
        private readonly BaseRepository<Comment> _commentRepository;

        public CommentController(BaseRepository<Comment> commentRepository)
        {
            _commentRepository = commentRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<string> Get()
        {
            var comments = await _commentRepository.GetAll();
            return JsonConvert.SerializeObject(comments);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(string id)
        {
            var comment = await _commentRepository.GetById(id) ?? new Comment();
            return JsonConvert.SerializeObject(comment);
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody]Comment entity)
        {
            if (entity != null)
            {
                var comment = new Comment()
                {
                    _id = Guid.NewGuid().ToString(),
                    code = entity.code,
                    text = entity.text,
                    user = entity.user
                };
                _commentRepository.Insert(comment);
                return JsonConvert.SerializeObject(comment); 
            }

            return string.Empty;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]Comment entity)
        {
            if (entity != null && !string.IsNullOrEmpty(id))
            {
                _commentRepository.UpdateById(id, entity); 
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                _commentRepository.DeleteById(id); 
            }
        }
    }
}
