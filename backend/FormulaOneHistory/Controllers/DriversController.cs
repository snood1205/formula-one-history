using FormulaOneHistory.Data;
using FormulaOneHistory.Models;
using FormulaOneHistory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaOneHistory.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DriversController : ControllerBase
{
    private readonly FormulaOneHistoryDbContext _context;
    private readonly DriverImportService _driverImportService;

    public DriversController(FormulaOneHistoryDbContext context, DriverImportService driverImportService)
    {
        _context = context;
        _driverImportService = driverImportService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Driver>>> GetDrivers()
    {
        var drivers = await _context.Drivers.ToListAsync();
        return Ok(drivers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Driver>> GetDriver(int id)
    {
        var driver = await _context.Drivers.FindAsync(id);
        if (HttpContext.Request.Query["skip_seasons"] == "true")
        {
            return Ok(driver);
        }

        var seasonBySeason =
            from result in _context.RaceResults
            where result.DriverId == id
            join race in _context.Races on result.RaceId equals race.RaceId
            select new { Race = race, Result = result }
            into racesWithResults
            group racesWithResults by racesWithResults.Race.Year
            into racesByYear
            select new
            {
                Year = racesByYear.Key,
                Races = racesByYear.Select(race => new
                {
                    race.Race.Circuit,
                    race.Race.Name,
                    race.Result.Position,
                    race.Result.FastestLap
                })
            };
        if (driver == null)
        {
            return NotFound();
        }

        return Ok(new
        {
            Driver = driver,
            SeasonBySeason = seasonBySeason
        });
    }
    
    [HttpGet("import")]
    public async Task<ActionResult<List<Driver>>> ImportDrivers()
    {
        return Ok(await _driverImportService.Import());
    }
}