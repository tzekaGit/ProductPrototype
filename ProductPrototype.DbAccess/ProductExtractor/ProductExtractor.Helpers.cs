using System.Text.Json;
using System.Text.RegularExpressions;

namespace ProductPrototype.DbAccess;

public partial class HtmlProductExtractor
{
    // Helper to parse weight or price string to decimal
    private decimal ParseDecimal(string text)
    {
        if (string.IsNullOrEmpty(text))
            return 0m;

        // Remove non-numeric chars, e.g., $, lbs, spaces
        string cleaned = Regex.Replace(text, @"[^\d\.]", "");
        return decimal.TryParse(cleaned, out decimal result) ? result : 0m;
    }

    // Helper to parse weight like "(94 lbs)"
    private decimal ParseWeight(string text)
    {
        if (string.IsNullOrEmpty(text)) return 0m;
        var match = Regex.Match(text, @"(\d+(\.\d+)?)");
        return match.Success ? decimal.Parse(match.Value) : 0m;
    }

    public string SerializeToJson(object data)
    {
        if (data is IEnumerable<object> collection)
        {
            var options = new JsonSerializerOptions { WriteIndented = false };
            var items = collection.Select(item => JsonSerializer.Serialize(item, options));

            // ✅ This gives you commas with line breaks and proper indentation
            return "[\n  " + string.Join(",\n  ", items) + "\n]";
        }

        // For single objects, use standard formatting
        return JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
    }

    private string Cleantext(string text)
    {
        var cleantext = text.ReplaceLineEndings().Trim();
        return Regex.Replace(cleantext, @"\s+", " ");

    }
}
