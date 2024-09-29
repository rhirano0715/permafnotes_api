using Microsoft.AspNetCore.Mvc;
using PermafnotesApi.DbContexts;

namespace PermafnotesApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        private readonly PermafnotesDbContext _context;

        public NoteController(ILogger<NoteController> logger, PermafnotesDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet(Name = "Note")]
        public IEnumerable<Note> Get()
        {
            _logger.LogInformation("Getting notes");
            return _context.Notes;
        }
    }
}
