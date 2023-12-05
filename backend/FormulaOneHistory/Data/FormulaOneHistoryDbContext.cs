using FormulaOneHistory.Models;
using Microsoft.EntityFrameworkCore;

namespace FormulaOneHistory.Data;

public class FormulaOneHistoryDbContext : DbContext
{
    public FormulaOneHistoryDbContext(DbContextOptions<FormulaOneHistoryDbContext> options) : base(options)
    {
    }
    
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Race> Races { get; set; }
    public DbSet<RaceResult> RaceResults { get; set; }
    public DbSet<Team> Teams { get; set; }
}