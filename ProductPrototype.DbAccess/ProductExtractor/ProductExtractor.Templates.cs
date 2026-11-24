using HtmlAgilityPack;
using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.DbAccess;

public partial class HtmlProductExtractor
{
    private (List<ColumnStyle> rowColumnStyles, string trClass, string cssName) ExtractProductColumnStyle(HtmlNode table)
    {
        var rowColmStyles = new List<ColumnStyle>();

        if (table == null) return (rowColmStyles, string.Empty, string.Empty);

        var row = table.SelectSingleNode(".//tr");
        if (row == null) return (rowColmStyles, string.Empty, string.Empty);

        var trClass = row.ParentNode
                         .GetAttributeValue("class", "") ?? "";

        var trStyle = row.ParentNode
                         .GetAttributeValue("style", "") ?? "";

        AddNewTemplate(rowColmStyles, 0, trClass, trStyle);

        var tds = row.Elements("td").ToList();
        string bgImg = string.Empty;
        for (int i = 0; i < tds.Count; i++)
        {
            var td = tds[i];

            // Collect only <th>-related metadata (visuals & structure)
            string css = td.GetAttributeValue("class", "");

            //
            if (td.SelectSingleNode(".//img") != null)
            {
                bgImg = td.SelectSingleNode(".//img")
                          ?.GetAttributeValue("src", "")
                          ?.Replace("/Images/", "")
                          ?.Split(".")[0] ?? string.Empty;
                bgImg = "bgImg_" + bgImg;

                css = $"{css} {bgImg}";
            }
            string style = td.GetAttributeValue("style", "");

            // If no style, fallback to first child element with style
            if (string.IsNullOrWhiteSpace(style))
            {
                var childWithStyle = td.Descendants().FirstOrDefault(x => x.Attributes["style"] != null);
                if (childWithStyle != null)
                    style = childWithStyle.GetAttributeValue("style", "");
            }

            AddNewTemplate(rowColmStyles, i, css, style);
        }
        return (rowColmStyles, trClass, bgImg);

        static void AddNewTemplate(List<ColumnStyle> rowColmStyles, int i, string css, string style)
        {
            rowColmStyles.Add(new ColumnStyle
            {
                DisplayOrder = i,
                CssClass = css,
                Style = style,
            });
        }
    }

    private int GetPageTemplate(HtmlNode node, Product product)
    {
        var titleNode = node.SelectSingleNode("//div[@class ='productHeaderBar']");

        string titleBgImg = titleNode.SelectSingleNode(".//td//img")?
                                     .GetAttributeValue("src", "") ?? "";


        var headerTbl = node.SelectSingleNode("..//div[@id='pricing']/table");
        var headerCells = headerTbl.SelectNodes(".//td")?.Count;
        var cssClass = headerTbl.GetAttributeValue("class", "");
        var prodtbl = node.SelectSingleNode("//table[contains(@class, 'description')]");
        var tblColNumb = prodtbl.SelectNodes("./tr/td").Count();
        var headerTmp = $"{tblColNumb}TCell.{headerCells}HCell.{cssClass}";

        PageTmp template = new PageTmp { TitleBgImg = titleBgImg, CssClass = cssClass, HeaderTmp = headerTmp };
        var templateStyle = SerializeToJson(template)?.ReplaceLineEndings("");

        return GetProductTmp(headerTmp, templateStyle, viewPath: "ProductTemplate.Grouped");

    }

    private int GetProductTmp(string name, string templateStyle, string viewPath)
    {
        var producttemplate = productTemplates.FirstOrDefault(tml => tml.Name == name && tml.TemplateStyle == templateStyle);
        producttemplate ??= _context.ProductTemplates.FirstOrDefault(tml => tml.Name == name && tml.TemplateStyle == templateStyle);

        if (producttemplate == null)
        {
            lastProductTemplateId = lastProductTemplateId == 0 ? ++lastDbProductTemplateId : ++lastProductTemplateId;
            producttemplate = new ProductTemplate
            {
                Id = lastProductTemplateId,
                Name = name,
                DisplayOrder = 0,
                ViewPath = viewPath,
                TemplateStyle = templateStyle
            };
            productTemplates.Add(producttemplate);
        }
        return producttemplate.Id;
    }
}
