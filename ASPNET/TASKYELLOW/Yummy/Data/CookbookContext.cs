using Microsoft.EntityFrameworkCore;
using Yummy.Models;

namespace Yummy.Data
{
    public class CookbookContext : DbContext
    {
        public CookbookContext(DbContextOptions<CookbookContext> options)
            : base(options)
        {
        }

        public DbSet<Recipe> Recipe { get; set; } = default!;
    }
}