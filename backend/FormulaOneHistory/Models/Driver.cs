namespace FormulaOneHistory.Models;

public class Driver
{
    public int DriverId { get; set; }
    public int ExternalDriverId { get; set; }
    public string Name { get; set; } 
    public string? Nationality { get; set; }
}