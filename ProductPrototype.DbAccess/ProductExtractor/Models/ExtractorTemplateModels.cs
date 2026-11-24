namespace ProductPrototype.DbAccess;

public class PageTmp
{
    public string TitleBgImg { get; set; }
    public string CssClass { get; set; }
    public string HeaderTmp { get; set; }

}

public class ProdRowTmp
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public string TmpName { get; set; }        // td/th class
    public List<ColumnStyle> columnStyles { get; set; }
}
public class ColumnStyle
{
    public int DisplayOrder { get; set; }
    public string Style { get; set; }
    public string CssClass { get; set; }
    public int Colspan { get; set; }
}
