
const $tbody = $("#productTable tbody");
function buildVariantRow(variant, index) {

	// Seat Color dropdown

	const seatColors = (variant.productAttributes[0] || []).productAttributeValues || [];
	const seatColorOptions = seatColors
		.map(color => `<option value="${color.name}">${color.name}</option>`)
		.join("");

	// Frame Color dropdown
	const frameColors = variant.productAttributes[1].productAttributeValues || [] // AttributeId 2 = Frame Color
	const frameColorOptions = frameColors
		.map(color => `<option value="${color.name}">${color.name}</option>`)
		.join("");
	//sécifications
	const specifications = variant.productSpecificationAttributeOptions[0];

	// let rowStyle = hasAssociation == true ? "border-bottom:none" : "";

	const data = {
		sku: variant.sku || "",
		specification: specifications ? specifications.specificationName : "",
		weight: variant.weight,
		price: formatPrice(variant.price),
		seatColors: seatColorOptions,
		frameColors: frameColorOptions,
		style: "",
		i: index
	};

	const html = seven_Column_Template.replace(/{(\w+)}/g, (match, key) => data[key] ?? match);
	$tbody.append(html);
}



function buildAssociateRow(assoc, index) {
		var xmlString = $("<textarea />").html(assoc.AttributesXml || "").text();
	var xmlDoc = $.parseXML(xmlString);
	var $xml = $(xmlDoc);

	var name = $xml.find("ProductAttribute").text() || assoc.Name;
	var desc = $xml.find("Description").text();
	var priceValue = $xml.find("Price > Value").text() || assoc.Price;
	var priceUnit = $xml.find("Price > PriceUnit").text();

	// Quantity input
	const data = {
		sku: assoc.Sku,
		name: "Covers",
		description: desc,
		price: formatPrice(priceValue) ,
		unit: priceUnit,
		i: index
	};
	const html = related_Product_Template.replace(/{(\w+)}/g, (match, key) => data[key] ?? match);
	$tbody.append(html);
	}

	// Function to build table body from JS models
	function buildTableBody(parent) {

		const headerData =
	{
		name: parent.Name,
		length: parent.Length
	};
	// Parent row
	const productHeaderHtml = benchHeader_Template.replace(/{(\w+)}/g, (match, key) => headerData[key] ?? match);
	$tbody.append(productHeaderHtml);

	// Variant rows
	let index = 0;
	const variants = products.filter(p => p.ParentGroupedProductId === parent.Id);
		for (let i = 0; i < variants.length; i++) {
			let v = variants[i];
			const hasAssociation = relatedProducts.some(rp => rp.ProductId1 === v.Id);
			$tbody.append(buildVariantRow(v, hasAssociation,  ++index));
			
			if (hasAssociation) {
				var relatedProduct = relatedProducts.find(rp => rp.ProductId1 === v.Id);
				assoc = productAttributeCombinations.find(rp => rp.ProductId === relatedProduct.ProductId2);
				$tbody.append(buildAssociateRow(assoc, hasAssociation, ++index));
				++i;
			}
	
		};
	return $tbody;
	}




	function formatPrice(price) {
		return new Intl.NumberFormat('en-US', {
						style: 'currency',
					currency: 'USD'
		}).format(price);
	}

