using System;
using System.Collections.Generic;
using System.Text;

namespace ProductPrototype.DbAccess.Data.ModelDto;

public class ClassificationResult
{
    public string AttributeOption { get; set; } = string.Empty;
    public string Attribute { get; set; } = string.Empty;
    public string ProductToken { get; set; } 
    public bool RequiresManualReview { get; set; } = false;
}
