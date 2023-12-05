namespace FormulaOneHistory.Models;

public class Team
{
    public int TeamId { get; set; }
    public int ExternalTeamId { get; set; }
    public string Name { get; set; }
    public string? Nationality { get; set; }
}