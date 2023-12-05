using FormulaOneHistory.Data;
using FormulaOneHistory.Models;

namespace FormulaOneHistory.Services;

public class TeamImportService(FormulaOneHistoryDbContext context) : CsvImportService<Team>(context,
    "KaggleData/constructors.csv")
{
    protected override Team CreateEntityFromRow(IReadOnlyDictionary<string, string> rowDict)
    {
        return new Team
        {
            ExternalTeamId = int.Parse(rowDict["constructorId"]),
            Name = rowDict["name"],
            Nationality = rowDict["nationality"]
        };
    }
}