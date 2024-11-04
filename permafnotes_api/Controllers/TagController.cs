using Microsoft.AspNetCore.Mvc;
using PermafnotesApi.DbContexts;

namespace PermafnotesApi.Controllers
{
    [ApiController]
    [Route("Tag")]
    public class TagController : ControllerBase
    {
        private readonly ILogger<TagController> _logger;
        private readonly PermafnotesDbContext _context;

        public TagController(ILogger<TagController> logger, PermafnotesDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<Tag> Get()
        {
            _logger.LogInformation("Getting notes");
            return _context.FetchTags();
        }
    }
}
