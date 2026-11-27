using ProductPrototype.DbAccess.Interfaces;

namespace ProductPrototype.DbAccess.ProductExtractor;

public sealed class OrderWriter(IJsonConfigStore configStore)
{
    public void WriteOrder()
    {
        string dbSectionJson = configStore.SerializeSection("Database");
        var writeRules = configStore.DeserializeSection<WriteRulesConfig>("WriteRules");

        // Use dbSectionJson and writeRules to write to the database...
        Console.WriteLine($"BatchSize: {writeRules.BatchSize} | DB Section: {dbSectionJson}");
    }
}
