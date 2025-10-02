using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Serilog;

using System.Net.Http;
using System.Net.Http.Json;

namespace School.Controllers
{
    [ApiController]
    [Route("[controller]")]
    // "/students
    public class RequestController : ControllerBase
    {
        // Fields
        private readonly ILogger<RequestController> _logger;
        private readonly IMapper _mapper;

        private HttpClient client = new HttpClient();

        string url = "https://jsonplaceholder.typicode.com/posts/";

        // Constructor
        public RequestController(ILogger<RequestController> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }

        // Methods

        // Get All Posts
        [HttpGet(Name = "GetAllPosts")]
        public async Task<IActionResult> GetAllPostsAsync()
        {
            _logger.LogInformation("Getting all posts");

            var results = await client.GetAsync(url);


            // Console.WriteLine(results.Content.ReadAsStringAsync().Result);

            var PostList = await results.Content.ReadFromJsonAsync<List<PostDTO>>();
            return Ok("Getting all posts");
        }
    }
}