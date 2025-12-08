namespace ProductPrototype.DbAccess.Data.StructDictEnums;

using System;
using System.Collections.Generic;

public static class CommercialParkProduct
{
    // Dictionary where:
    // Key = Product Name/Token (what you find in the table)
    // Value = Standardized Product Category
    public static readonly Dictionary<string, string> ProductClassification = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
    {
        // 1. SEATING & TABLES (Outdoor Furniture)
        // Seating
        { "bench", "OutdoorSeating" },
        { "steel bench", "OutdoorSeating" },
        { "concrete bench", "OutdoorSeating" },
        { "picnic bench", "OutdoorSeating" },
        { "glider", "OutdoorSeating" },
        { "swing seat", "OutdoorSeating" },

        { "frame", "Component/Structure" },
        { "seat", "Component/Seating" },
        { "base", "Component/Structure" },
        { "post", "Infrastructure" },
    

        // Tables
        { "table", "OutdoorTables" },
        { "picnic table", "OutdoorTables" },
        { "concrete table", "OutdoorTables" },
        { "aluminum table", "OutdoorTables" },
        { "patio table", "OutdoorTables" },
        
        // 2. COOKING & WASTE MANAGEMENT
        // Grills and Fire Pits
        { "grill", "CookingEquipment" },
        { "charcoal grill", "CookingEquipment" },
        { "pedestal grill", "CookingEquipment" },
        { "fire pit", "CookingEquipment" },
        { "barbecue", "CookingEquipment" },

        // Waste Receptacles
        { "trash can", "WasteManagement" },
        { "litter receptacle", "WasteManagement" },
        { "recycling bin", "WasteManagement" },
        { "ash urn", "WasteManagement" },
        { "can", "WasteManagement" },

        // 3. RECREATION & FITNESS EQUIPMENT
        
        // Pool Accessories
        { "pool ladder", "PoolAccessories" },
        { "skimmer net", "PoolAccessories" },
        { "diving board", "PoolAccessories" },
        { "pool cover", "PoolAccessories" }, // Can overlap with Covers category

        // Sports & Fitness
        { "goal", "SportsEquipment" },
        { "net", "SportsEquipment" },
        { "hoop", "SportsEquipment" },
        { "fitness station", "SportsEquipment" },
        { "tennis net", "SportsEquipment" },
        
        // 4. ACCESSORIES & COVERS
        
        // Bike Accessories
        { "bike rack", "BikeAccessories" },
        { "bicycle stand", "BikeAccessories" },
        { "bike security", "BikeAccessories" },

        // Protective Covers
        { "cover", "ProtectiveCovers" },
        { "table cover", "ProtectiveCovers" },
        { "bench cover", "ProtectiveCovers" },
        { "grill cover", "ProtectiveCovers" },
        { "item cover", "ProtectiveCovers" },
        { "furniture cover", "ProtectiveCovers" },
        
        // 5. INFRASTRUCTURE & SAFETY
        { "bollard", "Infrastructure" },
        { "sign", "Infrastructure" },
        { "pole", "Infrastructure" },
        { "light fixture", "Infrastructure" },
        { "drinking fountain", "Infrastructure" }
    };
}
