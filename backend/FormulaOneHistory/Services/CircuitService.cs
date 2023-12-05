using Microsoft.VisualBasic.FileIO;

namespace FormulaOneHistory.Services;

public static class CircuitService
{
    public static Dictionary<int, string> ImportCircuits()
    {
        var file = File.Open("KaggleData/circuits.csv", FileMode.Open);
        var circuits = new Dictionary<int, string>();
        using var parser = new TextFieldParser(file);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var headers = parser.ReadFields();
        if (headers is null) return circuits;
        while (!parser.EndOfData)
        {
            var row = parser.ReadFields();
            if (row is null) continue;
            var rowDict = headers.Zip(row, (header, value) => new { header, value })
                .ToDictionary(x => x.header, x => x.value);
            circuits.Add(int.Parse(rowDict["circuitId"]), rowDict["name"]);
        }

        return circuits;
    }
}