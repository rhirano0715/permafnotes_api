using Microsoft.EntityFrameworkCore;
using PermafnotesApi.Controllers;

namespace PermafnotesApi.DbContexts
{
    public class PermafnotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; } = null!;
        public DbSet<Tag> Tags { get; set; } = null!;
        public DbSet<Note_Tag> Notes_Tags { get; set; } = null!;

        private readonly ILogger<PermafnotesDbContext> _logger;

        public PermafnotesDbContext(DbContextOptions<PermafnotesDbContext> options, ILogger<PermafnotesDbContext> logger)
            : base(options)
        {
            _logger = logger;
            _logger.LogInformation("PermafnotesDbContext created");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note_Tag>()
                .HasKey(nt => new { nt.Note_Id, nt.Tag_Id });
        }

        public IEnumerable<NoteWithTag> FetchNoteWithTag()
        {
            var notes = Notes.ToList();
            var note_tags = Notes_Tags.ToList();
            var tags = Tags.ToList();
            var noteWithTags = notes.Select(note =>
            {
                var tagsForNote = note_tags
                    .Where(nt => nt.Note_Id == note.Id)
                    .Select(nt => tags.First(t => t.Id == nt.Tag_Id))
                    .ToList();
                return new NoteWithTag
                {
                    Id = note.Id,
                    Title = note.Title,
                    Reference = note.Reference,
                    Source = note.Source,
                    Memo = note.Memo,
                    Created_At = note.Created_At,
                    Updated_At = note.Updated_At,
                    Tags = tagsForNote
                };
            });
            return noteWithTags;
        }

    }

    public class NoteWithTag
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Memo { get; set; } = string.Empty;
        public DateTimeOffset Created_At { get; set; }
        public DateTimeOffset? Updated_At { get; set; }
        public List<Tag> Tags { get; set; } = new List<Tag>();
    }

    public class Note
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Reference { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public string Memo { get; set; } = string.Empty;
        public DateTimeOffset Created_At { get; set; }
        public DateTimeOffset? Updated_At { get; set; }
    }

    public class Tag
    {
        public long Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
    public class Note_Tag
    {
        public long Note_Id { get; set; }
        public long Tag_Id { get; set; }
    }

}
