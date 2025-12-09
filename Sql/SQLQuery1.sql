select 
product.Id,
product.ParentGroupedProductId,
product.Name,
product.Sku,
product.Price,
product.Weight,
product.Width,
product.Length,
product.Height
--product.FullDescription
--,product.ShortDescription
,specGr.Id
,specGr.Name
,spcOpt.Name
,spcOpt.DisplayOrder
,mapping.ProductId
,mapping.SpecificationAttributeOptionId
from Product product
left outer join  Product_SpecificationAttribute_Mapping  mapping on product.Id = mapping.ProductId
left outer  join  SpecificationAttributeOption spcOpt on mapping.SpecificationAttributeOptionId = spcOpt.Id
left outer  join  SpecificationAttribute spec on spcOpt.SpecificationAttributeId = spec.Id
left outer  join  SpecificationAttributeGroup specGr on spec.SpecificationAttributeGroupId = specGr.Id
 where (product.Id = 4 or product.ParentGroupedProductId = 4) 
 and specGr.Name = 'ProductGrouping1'
 order by 
 spcOpt.DisplayOrder asc,
			product.DisplayOrder asc

