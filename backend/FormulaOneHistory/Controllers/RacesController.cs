using FormulaOneHistory.Data;
using FormulaOneHistory.Models;
using FormulaOneHistory.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOneHistory.Controllers;

[ApiController]
[Route("api/races")]
public class RacesController(FormulaOneHistoryDbContext context, RaceImportService raceImportService)
    : ControllerBase
{
    [HttpGet("import")]
    public async Task<ActionResult<List<Race>>> ImportRaces()
    {
        return Ok(await raceImportService.Import());
    }
}