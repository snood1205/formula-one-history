using FormulaOneHistory.Data;
using FormulaOneHistory.Models;

namespace FormulaOneHistory.Services;

public class RaceResultImportService(FormulaOneHistoryDbContext context)
    : CsvImportService<RaceResultRow>(context, "KaggleData/results.csv")
{
    protected override List<RaceResultRow> ParseData()
    {
        var resultsWithoutRaceAssociations = base.ParseData();
        var associatedResults =
            (from result in resultsWithoutRaceAssociations
                join race in _context.Races on result.ExternalRaceId equals race.ExternalRaceId
                join driver in _context.Drivers on result.ExternalDriverId equals driver.ExternalDriverId
                join team in _context.Teams on result.ExternalTeamId equals team.ExternalTeamId
                select new RaceResultRow
                {
                    ExternalRaceResultId = result.ExternalRaceResultId,
                    CarNumber = result.CarNumber,
                    GridPosition = result.GridPosition,
                    Position = result.Position,
                    ExternalRaceId = result.ExternalRaceResultId,
                    RaceId = race.RaceId,
                    DriverId = driver.DriverId,
                    FastestLapSpeed = result.FastestLapSpeed,
                    TeamId = team.TeamId
                }).ToList();
        var fastestLapByRace =
            (from result in associatedResults
                where result.FastestLapSpeed != null && result.Position <= 10
                group result by result.RaceId
                into resultsByRace
                select new
                {
                    RaceId = resultsByRace.Key,
                    FastestLapSpeed = resultsByRace.Max(r => r.FastestLapSpeed)
                }).ToList();

        foreach (var result in associatedResults)
        {
            var fastestLap = fastestLapByRace.FirstOrDefault(race => race.RaceId == result.RaceId);
            if (fastestLap?.FastestLapSpeed != null && result.FastestLapSpeed != null)
            {
                result.FastestLap = Math.Abs(result.FastestLapSpeed.Value - fastestLap.FastestLapSpeed.Value) < 0.001;
            }
        }

        return associatedResults;
    }

    protected override RaceResultRow CreateEntityFromRow(IReadOnlyDictionary<string, string> rowDict)
    {
        return new RaceResultRow
        {
            ExternalRaceResultId = int.Parse(rowDict["resultId"]),
            CarNumber = ParseInt(rowDict["number"]),
            GridPosition = ParseInt(rowDict["grid"]),
            Position = ParseInt(rowDict["position"]),
            ExternalRaceId = int.Parse(rowDict["raceId"]),
            DriverId = int.Parse(rowDict["driverId"]),
            FastestLapSpeed = rowDict["fastestLapSpeed"] == "\\N" ? 0 : double.Parse(rowDict["fastestLapSpeed"]),
            TeamId = int.Parse(rowDict["constructorId"]),
            ExternalDriverId = int.Parse(rowDict["driverId"]),
            ExternalTeamId = int.Parse(rowDict["constructorId"])
        };
    }

    public override async Task<List<RaceResultRow>> Import(bool insertData = true)
    {
        var entities = ParseData();

        if (!insertData) return entities;

        var dbSet = _context.Set<RaceResult>();
        await dbSet.AddRangeAsync(entities);
        var rowsSaved = await _context.SaveChangesAsync();
        if (rowsSaved < entities.Count)
        {
            throw new Exception("Some rows were not saved.");
        }

        return entities;
    }

    private static int? ParseInt(string value)
    {
        if (value == "\\N") return null;
        return int.Parse(value);
    }
}

public class RaceResultRow : RaceResult
{
    public int ExternalDriverId { get; set; }
    public int ExternalTeamId { get; set; }
}