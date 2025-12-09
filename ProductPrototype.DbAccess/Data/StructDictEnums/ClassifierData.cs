namespace ProductPrototype.DbAccess.Data.StructDictEnums;

public class ClassifierData
{
    public static readonly Dictionary<string, string> MasterAttributeMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
{
    // Red Tones
    { "red", "Color" },
    { "scarlet", "Color" },
    { "crimson", "Color" },
    { "cherry", "Color" },
    { "ruby", "Color" },
    { "maroon", "Color" },

    // Blue/Cyan Tones
    { "blue", "Color" },
    { "navy", "Color" },
    { "sapphire", "Color" },
    { "teal", "Color" },
    { "cyan", "Color" },

    // Green Tones
    { "green", "Color" },
    { "emerald", "Color" },
    { "lime", "Color" },
    { "olive", "Color" },
    { "forest", "Color" },
    { "jade", "Color" },

    // Yellow/Amber Tones
    { "yellow", "Color" },
    { "gold", "Color" },
    { "lemon", "Color" },
    { "mustard", "Color" },
    { "amber", "Color" },

    // Neutrals & Grays
    { "black", "Color" },
    { "white", "Color" },
    { "gray", "Color" },
    { "grey", "Color" },
    { "charcoal", "Color" },
    { "slate", "Color" },

    // Brown Tones
    { "brown", "Color" },
    { "tan", "Color" },
    { "mocha", "Color" },
    { "khaki", "Color" },
    { "bronze", "Color" },

    // Metals, Pastels, and Earth Tones
    { "silver", "Color" },
    { "copper", "Color" },
    { "metallic", "Color" },
    { "blush", "Color" },
    { "mint", "Color" },
    { "rose", "Color" },
    { "sand", "Color" },
    { "moss", "Color" },
    { "stone", "Color"},
    { "ash", "Color"  },
    { "dusty", "Color" },

    // quantity and special
    {"unit",  "Quantity"},
    {"units", "Quantity"},
    {"special", "Special"},
    {"specials", "Specials"},
    {"lbs", "Weight"}
};


 
    public static readonly Dictionary<string, string> ColumnHeaderMapping = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        { "frame color", "Frame Color" },
        { "seat color", "Seat Color" },
        { "base color", "Base Color" },
        { "post color", "Post Color" },
        { "table top color", "Table Top Color"},
        { "units", "Number of Units"},
    };
   
    

}
