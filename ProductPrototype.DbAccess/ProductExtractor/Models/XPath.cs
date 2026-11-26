namespace ProductPrototype.DbAccess.ProductExtractor.Models;

public class XPath
{
    public string PathName { get; set; }
    public string PathValue { get; set; }
    public string DBDbField { get; set; }
}

public enum TabAttributeIndex
{
    Tab0,
    Tab1,
    Tab2,
    Tab3,
    Tab4,
    Tab5,
    Tab6,
    Tab7,
    Tab8,
    Tab9,
}

public class PunctuationPattern
{
    public string PatternName { get; set; }
    public string PatternValue { get; set; }
}

