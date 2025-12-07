

function buildVariantRow(variant, hasAssociation) {

	// Seat Color dropdown
	const seatColors = (variant.productAttributes[0] || []).productAttributeValues || [];
	const seatColorOptions = seatColors
		.map(color => `<option value="${color.name}">${color.name}</option>`)
		.join("");

	// Frame Color dropdown
	const frameColors = variant.productAttributes[1]?.productAttributeValues || [] // AttributeId 2 = Frame Color
	const frameColorOptions = frameColors
		.map(color => `<option value="${color.name}">${color.name}</option>`)
		.join("");
	//sécifications
	const specifications = variant.productSpecificationAttributeOptions[0];

	const data = {
		sku: variant.sku || "",
		specification: specifications ? specifications.specificationName : "",
		weight: variant.weight,
		price: formatPrice(variant.price),
		seatColors: seatColorOptions,
		frameColors: frameColorOptions,
		style: "",
		i: index,
		style: hasAssociation == true ? "border-bottom:none" : ""
	};
	let html = "";
	html = seven_Column_Template.replace(/{(\w+)}/g, (match, key) => data[key] ?? match);
	$tbody.append(html);
}



function buildAssociateRow(assoc) {
	
	// Quantity input
	const data = {
		sku: assoc.sku,
		name: "Covers",
		description: "2 Covers Required Per Bench - ",
		price: formatPrice(assoc.price) ,
		unit: "each",
		i: index
	};
	const html = related_Product_Template.replace(/{(\w+)}/g, (match, key) => data[key] ?? match);
	$tbody.append(html);
	}

	// Function to build table body from JS models
function buildTableBody(variant) {
	const hasAssociation = variant.relatedProducts?.length > 0 ?? false;
	$tbody.append(buildVariantRow(variant, hasAssociation));
			
	if (hasAssociation) {
		index++
		assoc = variant.relatedProductDetails[0];
		$tbody.append(buildAssociateRow(assoc, hasAssociation));
	}
	index++
	//};
	return $tbody;
	}

	function formatPrice(price) {
		return new Intl.NumberFormat('en-US', {
						style: 'currency',
						currency: 'USD'
		}).format(price);
	}

