using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ReduxstagramAPI.Helper;
using ReduxstagramAPI.Interfaces;
using ReduxstagramAPI.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ReduxstagramAPI.Controllers
{
    [Route("api/[controller]")]
    public class PostController : Controller
    {
        private readonly BaseRepository<Post> _postRepository;

        public PostController(BaseRepository<Post> postRepository)
        {
            _postRepository = postRepository;
        }

        // GET: api/values
        [HttpGet]
        public async Task<string> Get()
        {
            var posts = await _postRepository.GetAll();
            return JsonConvert.SerializeObject(posts);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<string> Get(string id)
        {
            var post = await _postRepository.GetById(id) ?? new Post();
            return JsonConvert.SerializeObject(post);
        }

        // POST api/values
        [HttpPost]
        public string Post([FromBody]Post entity)
        {
            var post = new Post()
            {
                _id = Guid.NewGuid().ToString(),
                code = GeneratorHelper.RandomStringGenerator(6),
                caption = entity.caption,
                display_src = entity.display_src,
                likes = 0
            };
            _postRepository.Insert(post);
            return JsonConvert.SerializeObject(post);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id)
        {
            _postRepository.UpdateById(id, null);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            _postRepository.DeleteById(id);
        }
    }
}
