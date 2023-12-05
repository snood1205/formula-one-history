using FormulaOneHistory.Data;
using FormulaOneHistory.Models;

namespace FormulaOneHistory.Services;

public class RaceResultImportService(FormulaOneHistoryDbContext context) : CsvImportService<RaceResult>(context,
    "KaggleData/results.csv")
{
    public async Task<List<RaceResult>> Import()
    {
        var resultsWithoutRaceAssociations = await base.Import();
        var associatedResults =
            from result in resultsWithoutRaceAssociations
            join race in context.Races on result.ExternalRaceId equals race.ExternalRaceId
            select new RaceResult
            {
                ExternalRaceResultId = result.ExternalRaceResultId,
                CarNumber = result.CarNumber,
                GridPosition = result.GridPosition,
                Position = result.Position,
                ExternalRaceId = result.ExternalRaceResultId,
                RaceId = race.RaceId,
                DriverId = result.DriverId,
                TeamId = result.TeamId
            };
        var fastestLapByRace =
            from result in associatedResults
            group result by result.RaceId
            into resultsByRace
            select new
            {
                RaceId = resultsByRace.Key,
                FastestLap = resultsByRace.Max(results => results.FastestLapSpeed)
            };
        foreach (var result in associatedResults)
        {
            var fastestLap = fastestLapByRace.FirstOrDefault(race => race.RaceId == result.RaceId);
            if (fastestLap != null)
            {
                result.FastestLap = Math.Abs(result.FastestLapSpeed - fastestLap.FastestLap) < 0.001;
            }
        }
        return associatedResults.ToList();
    }

    protected override RaceResult CreateEntityFromRow(IReadOnlyDictionary<string, string> rowDict)
    {
        return new RaceResult
        {
            ExternalRaceResultId = int.Parse(rowDict["resultId"]),
            CarNumber = int.Parse(rowDict["number"]),
            GridPosition = int.Parse(rowDict["grid"]),
            Position = int.Parse(rowDict["position"]),
            ExternalRaceId = int.Parse("raceId"),
            DriverId = int.Parse(rowDict["driverId"]),
            FastestLapSpeed = double.Parse(rowDict["fastestLapSpeed"]),
            TeamId = int.Parse(rowDict["constructorId"])
        };
    }
}