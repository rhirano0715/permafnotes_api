using Microsoft.EntityFrameworkCore;

namespace PermafnotesApi.DbContexts
{
    public class PermafnotesDbContext : DbContext
    {
        public PermafnotesDbContext(DbContextOptions<PermafnotesDbContext> options)
            : base(options)
        {
        }
        public DbSet<Note> Notes { get; set; } = null!;
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
}
