namespace FormulaOneHistory.Models;

public class Qualifying
{
    public int QualifyingId { get; set; }
    public int ExternalQualifyingId { get; set; }
    public int CarNumber { get; set; }
    public int DriverId { get; set; }
    public int Position { get; set; }
    public Driver Driver { get; set; }
    public int TeamId { get; set; }
    public Team Team { get; set; }
    public int RaceId { get; set; }
    public Race Race { get; set; }
}