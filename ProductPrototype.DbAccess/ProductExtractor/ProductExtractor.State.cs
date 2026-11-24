using ProductPrototype.DbAccess.Data.Models;

namespace ProductPrototype.DbAccess;

public partial class HtmlProductExtractor
{
    private List<ProdRowTmp> prodRowTmps = new();

    //db entities
    private List<Product> products = new();
    private List<ProductAttribute> productAttributes = new();
    private List<PredefinedProductAttributeValue> predefinedProductAttributeValues = new();
    private List<ProductAttributeValue> productAttributeValues = new();

    private List<SpecificationAttributeGroup> specificationAttributeGroups = new();
    private List<SpecificationAttribute> specificationAttributes = new();

    private List<ProductProductAttributeMapping> productProductAttributeMappings = new();
    private List<ProductSpecificationAttributeMapping> productSpecificationAttributeMappings = new();
    private List<SpecificationAttributeOption> specificationAttributeOptions = new();
    private List<ProductAttributeCombination> productAttributeCombinations = new();
    private List<RelatedProduct> relatedProducts = new();

    private List<Picture> pictures = new();
    private List<ProductPictureMapping> productPictureMappings = new();
    private List<ProductTemplate> productTemplates = new();

    //template
    public PageTmp pageTemplate = new();


    private int lastDbProductAttributeId = 0;
    private int predefinedDbProductAttributeValueId = 0;
    private int lastDbSpecificationAttributeGroupId = 0;
    private int lastDbSpecificationAttributeId = 0;
    private int lastDbSpecificationAttributeOptionId = 0;
    private int lastDbProductId = 0;

    private int lastDbProductProductAttributeMappingId = 0;
    private int lastDbProductAttributeValueId = 0;

    private int lastDbProductSpecificationAttributeMappingId = 0;
    private int lastDbRelatedProductId = 0;


    private int lastProductAttributeId = 0;
    private int predefinedProductAttributeValueId = 0;
    private int lastSpecificationAttributeGroupId = 0;
    private int lastSpecificationAttributeId = 0;
    private int lastSpecificationAttributeOptionId = 0;
    private int lastProductId = 0;
    private int lastDbProductDisplayOrder = 0;
    private int lastDbPictureId = 0;
    private int lastDbPictureMappingId = 0;


    private int lastProductProductAttributeMappingId = 0;
    private int lastProductAttributeValueId = 0;

    private int lastProductSpecificationAttributeMappingId = 0;
    private int lastRelatedProductId = 0;
    private int lastProductTemplatesListIdd = 0;
    // pictures
    private int lastPictureId = 0;
    private int lastPictureMappingId = 0;
    private Product lastParentProduct = default;
    private string ProductSku = string.Empty;
    private readonly RepositoryContext _context;
    private string headerTblCss = string.Empty;
    private string url = string.Empty;
    private int lastProductTemplateId = 0;
    private int lastDbProductTemplateId = 0;

    private int lastProductHeaderId = 0;
}
