using FormulaOneHistory.Data;
using FormulaOneHistory.Models;
using FormulaOneHistory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FormulaOneHistory.Controllers;

[ApiController]
[Route("api/drivers")]
public class DriversController(FormulaOneHistoryDbContext context, DriverImportService driverImportService)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<Driver>>> GetDrivers()
    {
        var drivers = await context.Drivers.ToListAsync();
        return Ok(drivers);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Driver>> GetDriver(int id)
    {
        var driver = await context.Drivers.FindAsync(id);
        if (HttpContext.Request.Query["skip_seasons"] == "true")
        {
            return Ok(driver);
        }

        var seasonBySeason =
            from result in context.RaceResults
            where result.DriverId == id
            join race in context.Races on result.RaceId equals race.RaceId
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
        return Ok(await driverImportService.Import());
    }
}