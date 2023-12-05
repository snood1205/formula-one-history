namespace FormulaOneHistory.Services;

using FormulaOneHistory.Data;
using FormulaOneHistory.Models;

public class RaceImportService(FormulaOneHistoryDbContext context) : CsvImportService<Race>(context, "KaggleData/races.csv")
{
    private Dictionary<int, string>? _circuits = CircuitService.ImportCircuits();

    protected override Race CreateEntityFromRow(IReadOnlyDictionary<string, string> rowDict)
    {
        return new Race
        {
            ExternalRaceId = int.Parse(rowDict["raceId"]),
            Name = rowDict["name"],
            Year = int.Parse(rowDict["year"]),
            Circuit = _circuits?[int.Parse(rowDict["circuitId"])]
        };
    }
}