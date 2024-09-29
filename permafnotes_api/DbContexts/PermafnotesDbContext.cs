using Microsoft.EntityFrameworkCore;
using PermafnotesApi.Controllers;

namespace PermafnotesApi.DbContexts
{
    public class PermafnotesDbContext : DbContext
    {
        private readonly ILogger<PermafnotesDbContext> _logger;

        public PermafnotesDbContext(DbContextOptions<PermafnotesDbContext> options, ILogger<PermafnotesDbContext> logger)
            : base(options)
        {
            _logger = logger;
            _logger.LogInformation("PermafnotesDbContext created");
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
