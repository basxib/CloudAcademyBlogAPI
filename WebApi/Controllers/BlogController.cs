using System.Collections.Generic;
using WebApi.Models;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using WebApi.Authorization;
namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BlogController: ControllerBase{
        private readonly List<Post> _posts;
        public BlogController(IList<Post> posts)
        {
             _posts = (List<Post>)posts;
        }

        [HttpGet]
        public ActionResult<List<Post>> GetPosts()
        {
            return _posts;   
        }

        [HttpPost("AddPost")]
        public ActionResult AddPost(Post p)
        {
            //make sure the post has all required fields filled and the content doesn't exceed 1024 chars and return 400 http error otherwise.
            if(!p.IsValid())
                return new BadRequestResult();
            p.Id = Guid.NewGuid();
            _posts.Add(p);
            return new OkResult();
            
        }

        [HttpGet("{id}")]
        public ActionResult<Post> GetPostById(Guid id)
        {
            var post = _posts.SingleOrDefault(p=>p.Id == id);
            if(post == null)
                return new NotFoundObjectResult(id);
            else return post;
        }

        [HttpPut("UpdatePost")]
        public ActionResult UpdatePost(Post UpdatedPost)
        {
            if(!UpdatedPost.IsValid())
                return new BadRequestResult();
            else if(!_posts.Any(x=>x.Id.ToString() == UpdatedPost.Id.ToString()))
                return new NotFoundResult();
            else
            {
                var oldPostIndex = _posts.FindIndex(x=>x.Id.ToString() == UpdatedPost.Id.ToString());
                _posts[oldPostIndex] = UpdatedPost;
                return new OkResult();
            }
        }
        [Authorize]
        [HttpDelete("{id}")]
        public ActionResult DeletePost(String id)
        {
            var post = _posts.SingleOrDefault(x=>x.Id.ToString() == id);
            if(post == null)
                return new NotFoundResult();
            _posts.Remove(post);
            return new OkResult();
        }

        //Assign category
        [HttpPost("AssignCategory")]
        public ActionResult AssignCategory(string id, string Category)
        {
            if(!_posts.Any(x=>x.Id.ToString() == id))
                return new NotFoundResult();
            else
            {
                var oldPostIndex = _posts.FindIndex(x=>x.Id.ToString() == id);
                _posts[oldPostIndex].Category = Category;
                return new OkResult();
            }
        }

        //Assign Tags

        //Search


    }

}