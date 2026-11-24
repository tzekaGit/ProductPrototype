using HtmlAgilityPack;
using Microsoft.EntityFrameworkCore;
using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.DbAccess;

public partial class HtmlProductExtractor
{
    private void ExtractSpecifications(HtmlDocument doc)
    {

        string specName = string.Empty;
        string groupName = string.Empty;

        specName = SpecificationTypes.ProductGrouping1.ToString();
        groupName = SpecificationTypes.ProductGrouping1.ToString();

        CreateGroupingSpecifications(groupName, specName, doc);

        foreach (var row in doc.DocumentNode.SelectNodes($"//table[contains(@class, 'description') and not(contains(@class, '{headerTblCss}')) ]//tr") ?? Enumerable.Empty<HtmlNode>())
        {
            if (row.SelectNodes("./td")?.Count > 0 && row.SelectNodes("./td")?.Count <= 4)
            {
                continue;
            }
            var cells = row.SelectNodes("td");
            // Detect Specification 1 (specificationAttributes)
            int specIndex = 1;
            if (row.SelectSingleNode(".//td[contains(@class,'alpha')]") != null)
            {
                specIndex = 2;
            }
            string specText = HtmlEntity.DeEntitize(cells[specIndex].InnerText.Trim());

            specName = SpecificationTypes.TableColumn1.ToString();
            groupName = SpecificationTypes.TableColumn1.ToString();

            CreateSpecifications(specText, groupName, specName);
        }
    }

    private void CreateGroupingSpecifications(string groupName, string specName, HtmlDocument doc)
    {
        var allTables = doc.DocumentNode.SelectNodes("id('pricing')//table");
        if (allTables == null)
        {
            throw new Exception("No specification tables found.");
        }

        headerTblCss = allTables[0].Attributes["class"]?.Value;

        //var allTables = node.DocumentNode
        //    .SelectNodes("//table[contains(@class,'" + headerTblCss + "')]")
        //    ?.ToList();

        if (allTables == null)
        {
            return;
        }

        foreach (var table in allTables)
        {
            if (table.HasClass(headerTblCss))
            {
                var specOption = table.SelectSingleNode(".//td");

                if (specOption != null)
                {
                    var specGroup = specificationAttributeGroups
                                                            .FirstOrDefault(grp => grp.Name == groupName);

                    specGroup ??= _context.SpecificationAttributeGroups
                                                            .FirstOrDefault(grp => grp.Name == groupName);
                    if (specGroup == null)
                    {
                        lastSpecificationAttributeGroupId = (lastSpecificationAttributeGroupId == 0 ? lastDbSpecificationAttributeGroupId : lastSpecificationAttributeGroupId) + 1;
                        specGroup = new SpecificationAttributeGroup { Id = lastSpecificationAttributeGroupId, Name = groupName, DisplayOrder = 0 };
                        specificationAttributeGroups.Add(specGroup);
                    }
                    else
                    {
                        lastSpecificationAttributeGroupId = specGroup.Id;
                    }

                    var spec = specificationAttributes
                                  .FirstOrDefault(s => s.Name.Equals(specName, StringComparison.OrdinalIgnoreCase));

                    spec ??= _context.SpecificationAttributes.FirstOrDefault(s => s.Name == specName);



                    if (spec == null)
                    {
                        lastSpecificationAttributeId = (lastSpecificationAttributeId == 0 ? lastDbSpecificationAttributeId : lastSpecificationAttributeId) + 1;
                        spec = new SpecificationAttribute { Id = lastSpecificationAttributeId, Name = specName, SpecificationAttributeGroupId = specGroup.Id };

                        specificationAttributes.Add(spec);
                    }
                    else
                    {
                        lastSpecificationAttributeId = spec.Id;
                    }


                    var specificationAttributeOption = specificationAttributeOptions
                                                               .FirstOrDefault(attr => attr.Name == specOption.InnerText);

                    specificationAttributeOption ??= _context.SpecificationAttributeOptions
                                                               .FirstOrDefault(attr => attr.Name == specOption.InnerText);

                    if (specificationAttributeOption == null)
                    {

                        int displayOrder = specificationAttributeOptions
                                          .Where(attr => attr.SpecificationAttributeId == lastSpecificationAttributeId)
                                          .OrderByDescending(itm => itm.DisplayOrder)
                                          .FirstOrDefault()?
                                          .DisplayOrder ?? 0;

                        //pull from the db
                        if (displayOrder == 0)
                        {
                            displayOrder = _context.SpecificationAttributeOptions
                                                      .Where(attr => attr.SpecificationAttributeId == lastSpecificationAttributeId)
                                                      .MaxAsync(e => (int?)e.DisplayOrder).Result ?? 0;

                        }

                        lastSpecificationAttributeOptionId = (lastSpecificationAttributeOptionId == 0 ? lastDbSpecificationAttributeOptionId : lastSpecificationAttributeOptionId) + 1;

                        specificationAttributeOptions.Add(new SpecificationAttributeOption
                        {
                            Id = lastSpecificationAttributeOptionId,
                            Name = specOption.InnerText,
                            SpecificationAttributeId = lastSpecificationAttributeId,
                            DisplayOrder = displayOrder + 1
                        });
                    }
                }
            }

        }
    }

    private void CreateSpecifications(string specText, string groupName, string specName)
    {
        var specGroup = specificationAttributeGroups
                              .FirstOrDefault(s => s.Name.Equals(groupName, StringComparison.OrdinalIgnoreCase));

        specGroup ??= _context.SpecificationAttributeGroups.FirstOrDefault(s => s.Name == groupName);


        if (specGroup == null)
        {
            lastSpecificationAttributeGroupId = (lastSpecificationAttributeGroupId == 0 ? lastDbSpecificationAttributeGroupId : lastSpecificationAttributeGroupId) + 1;
            specGroup = new SpecificationAttributeGroup { Id = lastSpecificationAttributeGroupId, Name = groupName, DisplayOrder = 0 };
            specificationAttributeGroups.Add(specGroup);
        }


        var spec = specificationAttributes
                              .FirstOrDefault(s => s.Name.Equals(specName, StringComparison.OrdinalIgnoreCase));
        spec ??= _context.SpecificationAttributes.FirstOrDefault(s => s.Name == specName);


        if (spec == null)
        {
            lastSpecificationAttributeId = (lastSpecificationAttributeId == 0 ? lastDbSpecificationAttributeId : lastSpecificationAttributeId) + 1;
            spec = new SpecificationAttribute { Id = lastSpecificationAttributeId, Name = specName, SpecificationAttributeGroupId = specGroup.Id };
            specificationAttributes.Add(spec);
        }

        var specOption = specificationAttributeOptions.FirstOrDefault(s => s.Name.Equals(specText, StringComparison.OrdinalIgnoreCase));
        specOption ??= _context.SpecificationAttributeOptions.FirstOrDefault(s => s.Name.ToLower() == specText.ToLower());



        if (specOption == null)
        {
            lastSpecificationAttributeOptionId = (lastSpecificationAttributeOptionId == 0 ? lastDbSpecificationAttributeOptionId : lastSpecificationAttributeOptionId) + 1;
            var displayOrder = _context.SpecificationAttributeOptions.MaxAsync(e => (int?)e.DisplayOrder).Result ?? 0;
            displayOrder++;
            specificationAttributeOptions.Add(new SpecificationAttributeOption
            {
                Id = lastSpecificationAttributeOptionId,
                SpecificationAttributeId = spec.Id,
                Name = specText,
                DisplayOrder = displayOrder,
            });
        }
    }

    private List<ProductSpecificationAttributeMapping> ExtractProductSpecificationMappings(string specName, string specOption, Product product)
    {
        //Console.WriteLine("products:" + products.Count);
        // HtmlEntity.DeEntitize(cells[1].InnerText.Trim());
        var spec = specificationAttributes
                                .FirstOrDefault(s => s.Name == specName);

        spec ??= _context.SpecificationAttributes
                                .FirstOrDefault(s => s.Name == specName);

        if (spec == null)
        {
            throw new Exception();
            // return productSpecificationAttributeMappings;
        }
        var option = specificationAttributeOptions
                                .FirstOrDefault(o => o.Name.Equals(specOption, StringComparison.OrdinalIgnoreCase)
                                                  && o.SpecificationAttributeId == spec.Id);

        option ??= _context.SpecificationAttributeOptions
                           .FirstOrDefault(o => o.Name.ToLower() == specOption.ToLower()
                                             && o.SpecificationAttributeId == spec.Id);

        var lastSpecificationAttributeMappingDisplayOrder = 0;
        if (option == null)
        {
            return productSpecificationAttributeMappings;
        }
        else
        {
            ProductSpecificationAttributeMapping specMapp = default;

            bool specMappExists = !productSpecificationAttributeMappings.Any(opt => opt.ProductId == product.Id && opt.SpecificationAttributeOptionId == option.Id) ?
                                  _context.ProductSpecificationAttributeMappings.Any(opt => opt.ProductId == product.Id && opt.SpecificationAttributeOptionId == option.Id) :
                                  false;

            if (!specMappExists)
            {
                lastProductSpecificationAttributeMappingId = (lastProductSpecificationAttributeMappingId == 0 ? lastDbProductSpecificationAttributeMappingId : lastProductSpecificationAttributeMappingId) + 1;
                lastSpecificationAttributeMappingDisplayOrder++;

                productSpecificationAttributeMappings.Add(new ProductSpecificationAttributeMapping
                {
                    Id = lastProductSpecificationAttributeMappingId,
                    ProductId = product.Id,
                    SpecificationAttributeOptionId = option.Id,
                    DisplayOrder = lastSpecificationAttributeMappingDisplayOrder
                });

            }
        }

        return productSpecificationAttributeMappings;
    }
}
