SELECT distinct TOP (1000)  spec.[Id]
    ,[CustomValue]
    ,[ProductId]
    ,[SpecificationAttributeOptionId]
    ,[AttributeTypeId]
    ,[AllowFiltering]
    ,[ShowOnProductPage]
    ,prod.id as productid
    ,spcOpt.DisplayOrder as spcOptOrder
    ,spcOpt.Name
    ,specMap.ProductId
    ,prod.DisplayOrder
    ,prod.Sku
    ,specGr.Id as BenchLengthGroupId
    ,specGr.Name as specGrName
    ,prod.length


  FROM [Belson4.91].[dbo].[Product_SpecificationAttribute_Mapping] specMap
      inner join Product prod on prod.Id = specMap.ProductId
      inner join SpecificationAttributeOption spcOpt on spcOpt.Id =  specMap.SpecificationAttributeOptionId 
      inner join SpecificationAttribute spec on spec.Id = spcOpt.SpecificationAttributeId
      inner join SpecificationAttributeGroup specGr on specGr.Id = spec.SpecificationAttributeGroupId
       
    where (prod.Id = 48  Or  prod.ParentGroupedProductId = 48)
    and spcOpt.Id  in (select SpecificationAttributeOptionId  from  [Product_SpecificationAttribute_Mapping] where ProductId= 48)



  order by spcOpt.DisplayOrder asc

