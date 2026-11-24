using HtmlAgilityPack;
using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.DbAccess;

public partial class HtmlProductExtractor
{
    private void GetPropertiesFromHtml(HtmlNode element, ref Product product, string selectBy, string selectVal = "")
    {
        // Name and Length from h2 > span inside the element
        string nameNode;
        HtmlNode lengthNode = default;
        HtmlNode elementNode = default;
        string xpathQuery = string.Empty;


        switch (selectBy)
        {
            case "h1":
                elementNode = element.SelectSingleNode(".//" + selectVal);
                product.Name = elementNode.InnerText.Trim();
                elementNode?.InnerHtml = "{product-header-title}";
                break;

            case "h2":
                lengthNode = element.SelectSingleNode(".//span");
                if (lengthNode != null)
                {
                    // Parse length as decimal if needed, else keep as string
                    string lengthText = lengthNode.InnerText.Trim().Replace("'", "").Replace("’", "");
                    if (decimal.TryParse(lengthText, out decimal len))
                        product.Length = len;
                }
                break;

            case "class/img":
                if (selectVal.IndexOf("leftL") > 0 || selectVal.IndexOf("leftR") > 0)
                {
                    elementNode = element.SelectSingleNode(".//div[@class ='leftL']");
                    elementNode ??= element.SelectSingleNode(".//div[@class ='leftR']");
                    if (elementNode == null) break;
                }

                var imgNode = elementNode.SelectSingleNode("..//img");
                imgNode ??= elementNode.SelectSingleNode("..//img");
                if (imgNode != null)
                {
                    var imgDocTag = new HtmlDocument();
                    imgDocTag.LoadHtml(imgNode.OuterHtml);
                    var productImage = imgDocTag.DocumentNode.Descendants("img").FirstOrDefault();
                    string imgSource = productImage.GetAttributeValue("src", string.Empty).Trim();

                    // Parse length as decimal if needed, else keep as string
                    string imgText = imgNode.InnerText.Trim().Replace("'", "").Replace("’", "");


                    var picExists = pictures.Any(pic => pic.SeoFilename == imgSource) == false ?
                                    _context.Pictures.Any(pic => pic.SeoFilename == imgSource) :
                                    false;

                    if (!picExists)
                    {
                        lastPictureId = (lastPictureId == 0) ? lastDbPictureId + 1 : lastPictureId + 1;
                        var picture = new Picture
                        {
                            Id = lastPictureId,
                            MimeType = "image/jpeg",
                            SeoFilename = imgSource,
                            IsNew = false,
                        };
                        pictures.Add(picture);

                        var productId = product.Id;
                        var ProductPictureMapping = productPictureMappings
                                                                .FirstOrDefault(pic => pic.ProductId == productId && pic.PictureId == picture?.Id);

                        ProductPictureMapping ??= productPictureMappings
                                                              .FirstOrDefault(pic => pic.ProductId == productId && pic.PictureId == picture?.Id);
                        if (ProductPictureMapping == null)
                        {
                            lastPictureMappingId = (lastPictureMappingId == 0 ? lastDbPictureMappingId : lastPictureMappingId) + 1;
                            productPictureMappings.Add(new ProductPictureMapping
                            {
                                Id = lastPictureMappingId,
                                ProductId = product.Id,
                                PictureId = picture.Id,
                                DisplayOrder = 0,
                            });
                        }
                    }
                }
                break;

            case "id":
                elementNode = element.SelectSingleNode("//div[@id ='" + selectVal + "']");
                if (selectVal == "product")
                {
                    product.FullDescription = Cleantext(elementNode?.InnerHtml);
                }

                break;

            case "class":

                elementNode = element.SelectSingleNode("//div[@class ='" + selectVal + "']//div//h2");

                if (elementNode != null)
                {
                    var descriptionNode = elementNode.ParentNode; // elementNode.SelectSingleNode("//div[contains(concat(' ', normalize-space(@class), ' '), ' leftL ') or contains(concat(' ', normalize-space(@class), ' '), ' leftR ')]//h2");
                    var shortDecsNode = descriptionNode.ParentNode.SelectSingleNode("//p");
                    product.ShortDescription = Cleantext(shortDecsNode?.InnerText);
                    shortDecsNode?.InnerHtml = "{short-description}";
                }

                var colorOptions = element.SelectNodes("//div[@class ='" + selectVal + "']//img[contains(@src, '-CC-') or contains(@src, '-BT-') ]");
                if (colorOptions == null || colorOptions.Count == 0)
                    break;
                for (int i = 0; i < colorOptions.Count; i++)
                {
                    if (i == 0)
                    {
                        var colorNode = new HtmlDocument().CreateElement("div");
                        colorNode.SetAttributeValue("class", "colorContainer");
                        colorNode.InnerHtml = "<div id=\"spheresContainer\"></div>";

                        colorOptions[0].ParentNode.ReplaceChild(colorNode, colorOptions[0]);
                    }
                    else
                        colorOptions[i].ParentNode.RemoveChild(colorOptions[i]);
                }
                break;

            case "div/class":
                xpathQuery = "//div[contains(concat(' ', normalize-space(@class), ' '), '" + selectVal + "')]";
                var elementNodes = element.SelectNodes(xpathQuery);
                if (elementNodes == null)
                    break;
                foreach (var el in elementNodes)
                {
                    if (el.HasClass("dimT")) continue;

                    //ExtractWholePartsSimplified(el.InnerText);

                    el.Remove();
                }
                break;

            default:
                break;
        }
    }
}
