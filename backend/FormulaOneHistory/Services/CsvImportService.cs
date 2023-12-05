using FormulaOneHistory.Data;
using Microsoft.VisualBasic.FileIO;

namespace FormulaOneHistory.Services;

public abstract class CsvImportService<T> where T : class
{
    protected readonly FormulaOneHistoryDbContext _context;
    protected string FilePath;

    protected CsvImportService(FormulaOneHistoryDbContext context, string filePath)
    {
        _context = context;
        FilePath = filePath;
    }

    public async Task<List<T>> Import()
    {
        var file = File.Open(FilePath, FileMode.Open);
        var entities = new List<T>();
        using var parser = new TextFieldParser(file);
        parser.TextFieldType = FieldType.Delimited;
        parser.SetDelimiters(",");
        var headers = parser.ReadFields();
        if (headers is null) return entities;
        while (!parser.EndOfData)
        {
            var row = parser.ReadFields();
            if (row is null) continue;
            var rowDict = headers.Zip(row, (header, value) => new { header, value })
                .ToDictionary(x => x.header, x => x.value);
            entities.Add(CreateEntityFromRow(rowDict));
        }

        var dbSet = _context.Set<T>();
        await dbSet.AddRangeAsync(entities);
        var rowsSaved = await _context.SaveChangesAsync();
        if (rowsSaved < entities.Count)
        {
            throw new Exception("Some rows were not saved.");
        }

        return entities;
    }

    protected abstract T CreateEntityFromRow(IReadOnlyDictionary<string, string> rowDict);
}