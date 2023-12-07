using FormulaOneHistory.Services;
using Microsoft.AspNetCore.Mvc;

namespace FormulaOneHistory.Controllers;

[ApiController]
[Route("api/teams")]
public class TeamsController : ControllerBase
{
    [HttpGet("import")]
    public async Task<ActionResult> Import([FromServices] TeamImportService teamImportService)
    {
        var results = await teamImportService.Import();
        return Ok(results);
    }
}