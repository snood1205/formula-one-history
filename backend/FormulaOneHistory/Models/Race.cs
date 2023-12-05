namespace FormulaOneHistory.Models;

public class Race
{
    public int RaceId { get; set; }
    public int ExternalRaceId { get; set; }
    public string Name { get; set; }
    public string? Circuit { get; set; }
    public int Year { get; set; }
}