using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftDeleteSketch.Entities;

namespace SoftDeleteSketch.Controllers {

    [ApiController]
    [Route("/api/tests")]
    public class TestController : ControllerBase {

        private readonly AppDbContext _appDbContext;

        public TestController(
            AppDbContext appDbContext
        ) {
            _appDbContext = appDbContext;
        }

        [HttpGet("posts")]
        public IActionResult GetPosts() {
            var posts = _appDbContext.Posts
                .Include(p => p.Author)
                .Include(p => p.Blog)
                .ToList();
            return Ok(posts);
        }

        [HttpGet("blogs")]
        public IActionResult GetBlogs() {
            var blogs = _appDbContext.Blogs.IgnoreQueryFilters().ToList();
            return Ok(blogs);
        }

        [HttpGet("persons")]
        public IActionResult GetPersons() {
            var peoples = _appDbContext.People
                .Include(p => p.Blogs)
                .Include(p => p.Posts)
                .IgnoreQueryFilters().ToList();
            return Ok(peoples);
        }

        [HttpDelete("delete/posts/{id::guid}")]
        public async Task<IActionResult> DeletePosts(Guid id) {
            var post = await _appDbContext.Posts.FirstOrDefaultAsync(p => p.Id == id);
            if (post == null)
                return NotFound();

            // _appDbContext.Posts.Remove(post);
            _appDbContext.Remove(post);
            await _appDbContext.SaveChangesAsync();
            return Ok("Deleted!");
        }

        [HttpDelete("delete/person/{id::guid}")]
        public async Task<IActionResult> DeletePersons(Guid id) {
            var person = await _appDbContext.People
                .FirstOrDefaultAsync(p => p.Id == id);
            if (person == null)
                return NotFound();

            _appDbContext.Remove(person);
            await _appDbContext.SaveChangesAsync();
            return Ok("Deleted!");
        }

    }
}