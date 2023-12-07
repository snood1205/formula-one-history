namespace FormulaOneHistory.Models;

public class RaceResult
{
    public int RaceResultId { get; set; }
    public int ExternalRaceResultId { get; set; }
    public int? CarNumber { get; set; }
    public int? GridPosition { get; set; }
    public int? Position { get; set; }
    public bool FastestLap { get; set; }
    public double? FastestLapSpeed { get; set; }
    
    public int ExternalRaceId { get; set; }

    public int RaceId { get; set; }
    public virtual Race Race { get; set; }

    public int DriverId { get; set; }
    public virtual Driver Driver { get; set; }

    public int TeamId { get; set; }
    public virtual Team Team { get; set; }
}