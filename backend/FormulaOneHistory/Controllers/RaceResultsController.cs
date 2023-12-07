using FormulaOneHistory.Models;
using FormulaOneHistory.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOneHistory.Controllers;

[ApiController]
[Route("api/race-results")]
public class RaceResultsController : ControllerBase
{
    [HttpGet("import")]
    public async Task<ActionResult> Import([FromServices] RaceResultImportService raceResultImportService)
    {
        var results = await raceResultImportService.Import();
        return Ok(results);
    }
}