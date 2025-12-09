/*
	delete from product where Id>3
	delete from RelatedProduct
	delete from picture where Id>80
	delete from [ProductAttribute] where Id>9
	delete from specificationAttributeGroup where Id>1
*/

select * from ProductAttribute
select ParentGroupedProductId, ProductTypeId, DisplayOrder, price, 
* from product where Sku = 'E-Series-Steel-Picnic-Table-Rectangular'

select  * from RelatedProduct where productid1 = 8 //26

select  * from  CrossSellProduct
select * from picture where SeoFilename like '%/Images/238-EV6-001-M.jpg%'


select * from [dbo].[Product_Picture_Mapping]

select * from PredefinedProductAttributeValue
select * from Product_SpecificationAttribute_Mapping where productid in 
(select Id from product where Id = 42 or ParentGroupedProductId = 42)  

select * from [Product_ProductAttribute_Mapping] where productid in 
(select Id from product where Id = 42 or ParentGroupedProductId = 42) 


select * from [dbo].[ProductAttribute]


select * from specificationAttributeGroup
select * from specificationAttributeOption where name = 'ADA - Double Overhang E-Series Rectangular Steel Picnic Table | Diamond Pattern'
select * from SpecificationAttribute 

select * from [Product_ProductAttribute_Mapping]
select * from [Product_SpecificationAttribute_Mapping] order by productId, SpecificationAttributeOptionId


select p.Id, p.Sku,attr.Name, m.*
from [dbo].[Product_ProductAttribute_Mapping]  m
Inner join Product p on p.id = m.ProductId
Inner join ProductAttribute attr on attr.id = m.ProductAttributeId
--left outer join ProductAttributeValue attrValue on m.id = attrValue.ProductAttributeMappingId
where p.Sku = '942S-V6'  --p.Id >=4 
order by p.Id

select * from [Product_ProductAttribute_Mapping] where  AttributeControlTypeId =1

--============================
select * from  SpecificationAttributeOption
select * from ProductAttributeValue




--================================
select * from [dbo].[Product_SpecificationAttribute_Mapping]
select p.Id, p.Sku,attr.Name, op.* from [dbo].[Product_SpecificationAttribute_Mapping]  m
left outer join Product p on p.id = m.ProductId
left outer join SpecificationAttributeOption op on op.id = m.SpecificationAttributeOptionId
left outer join SpecificationAttribute attr on attr.id = op.SpecificationAttributeId
where productId >= 4 order by sku
--delete from SpecificationAttributeOption where Id>16

select * from [dbo].[Product_ProductAttribute_Mapping]

select * from [dbo].[ProductAttributeValue]






