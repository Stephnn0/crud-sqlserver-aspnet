namespace notes_api.Data;

using Microsoft.EntityFrameworkCore;
using notes_api.Models;


public class DataContext : DbContext
{
    public DataContext(DbContextOptions options) : base(options)
    {

    }

    public DbSet<Notes> Note { get; set; }
}
