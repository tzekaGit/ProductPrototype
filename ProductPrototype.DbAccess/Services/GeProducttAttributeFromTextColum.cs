using ProductPrototype.DbAccess.Data.ModelDto;
using ProductPrototype.DbAccess.Data.StructDictEnums;
using RTools_NTS.Util;

namespace ProductPrototype.DbAccess.Services;



public static class GeProducttAttribute
{

    // High-speed lookup for colors

    public static readonly HashSet<string> StopWords = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "dark", "light", "mismatched", "the", "a", "an", "for"
    };
    public static string GetAttributeName(List<string> columnLabels)
    {
        // 1. Storage for results
        var productTokens = new List<string>();
        var attributeTypes = new Dictionary<string, int>();

        foreach (var label in columnLabels)
        {
            // Use your existing method that returns ClassificationResult
            var singleResult = MatchAttributeName(label);

            // 2. Aggregate Product Tokens (e.g., "frame", "bench")
            productTokens.Add(singleResult.ProductToken); // Assuming you added this property

            // 3. Track Attribute Types
            if (!string.IsNullOrEmpty(singleResult.AttributeOption))
                attributeTypes[singleResult.AttributeOption] = attributeTypes.GetValueOrDefault(singleResult.AttributeOption) + 1;

        }

        // 4. Determine Dominant Product and Attribute Type
        string dominantProduct = productTokens.GroupBy(t => t).OrderByDescending(g => g.Count()).First().Key;
        string dominantAttribute = attributeTypes.OrderByDescending(kv => kv.Value).First().Key;

        // 5. Final Mapping (e.g., using a lookup: "frame" + "AttributeOption" => "Frame AttributeOption")
        string columnKey = $"{dominantProduct} {dominantAttribute}";

        // Assume you have a static dictionary for final mapping
        if (ClassifierData.ColumnHeaderMapping.TryGetValue(columnKey, out string finalHeader))
        {
            return finalHeader;
        }

        return $"Unknown Column: {columnKey}";
    }

    public static ClassificationResult MatchAttributeName(string label)
    {
         var tokens = label.ToLowerInvariant().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        var result = new ClassificationResult();

        // 1. Entity Recognition: Search and tag ALL known attributes
        tokens.RemoveAll(t =>
        {
            if (ClassifierData.MasterAttributeMapping.TryGetValue(t, out string attributeType))
            {
                result.AttributeOption = attributeType.ToLower(); 
                return true;
            }
            return false;
        });
        

        // 2. Classification: Find the product category from the remaining text
        string remainingText = string.Join(" ", tokens);

        if (CommercialParkProduct.ProductClassification.TryGetValue(remainingText, out string category))
        {
            result.ProductToken = remainingText;
        }
        else
        {
            // Fallback rule
            result.RequiresManualReview = true;
        }

        return result;
    }

}
