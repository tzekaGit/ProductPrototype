using System.Text.Json;

namespace ProductPrototype.DbAccess.ProductExtractor;

public class GetJSonFile
{
    public JsonDocument ReadJsonFile()
    {

        string filePath = @"XPathConfig.json";   // note the @ for the backslashes

        // 1. Read the whole file into a string
        string jsonString = File.ReadAllText(filePath);

        // 2. Parse into a document
        return JsonDocument.Parse(jsonString);
    }

}
