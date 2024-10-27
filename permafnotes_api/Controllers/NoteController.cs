using Microsoft.AspNetCore.Mvc;
using PermafnotesApi.DbContexts;

namespace PermafnotesApi.Controllers
{
    [ApiController]
    [Route("Note")]
    public class NoteController : ControllerBase
    {
        private readonly ILogger<NoteController> _logger;
        private readonly PermafnotesDbContext _context;

        public NoteController(ILogger<NoteController> logger, PermafnotesDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public IEnumerable<NoteWithTag> Get()
        {
            _logger.LogInformation("Getting notes");
            return _context.FetchNoteWithTag();
        }

        [HttpGet("{id}")]
        public ActionResult<NoteWithTag> Get(long id)
        {
            _logger.LogInformation("Getting note");
            var note = _context.SelectNoteWithTagByNoteId(id);
            if (note == null)
            {
                return NotFound();
            }
            return note;
        }

        [HttpPost]
        public async Task<ActionResult<NoteWithTag>> Post(NoteWithTag note)
        {
            _logger.LogInformation("Creating note");
            return _context.AddNoteAndTags(note);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NoteWithTag>> Put(long id, NoteWithTag note)
        {
            _logger.LogInformation("Updating note");
            if (id != note.Id)
            {
                return BadRequest();
            }
            return _context.UpdateNoteAndTags(note);
        }
    }
}
