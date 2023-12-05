using FormulaOneHistory.Data;
using FormulaOneHistory.Models;

namespace FormulaOneHistory.Services;

public class DriverImportService(FormulaOneHistoryDbContext context) : CsvImportService<Driver>(context, "KaggleData/drivers.csv")
{
    protected override Driver CreateEntityFromRow(IReadOnlyDictionary<string, string> rowDict)
    {
        return new Driver
        {
            ExternalDriverId = int.Parse(rowDict["driverId"]),
            Name = $"{rowDict["forename"]} {rowDict["surname"]}",
            Nationality = rowDict["nationality"]
        };
    }
}