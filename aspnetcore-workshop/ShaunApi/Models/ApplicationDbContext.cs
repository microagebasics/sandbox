using Microsoft.EntityFrameworkCore;

namespace ShaunApi.Models
{
  public class ApplicationDbContext : DbContext
    {

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options)
    {

    }

    public DbSet<Speaker> Speakers { get; set; }

  }
}
