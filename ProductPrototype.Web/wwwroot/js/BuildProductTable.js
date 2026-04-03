
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
	const variantTemplate = variant.productTemplate;
	const templateStyle = JSON.parse(variantTemplate.templateStyle);
	//console.log(templateStyle);
	const data = {
		sku: variant.sku || "",
		specification: specifications ? specifications.specificationName : "",
		weight: variant.weight,
		itemPrice: formatPrice(variant.price),
		oldPrice: variant.oldPrice !=0? formatPrice(variant.oldPrice):0,
		seatColors: seatColorOptions,	
		frameColors: frameColorOptions,
		borderRow: "",
		select1: seatColorOptions.split('</option>').length == 2 ? "span" : "select",
		select2: frameColorOptions.split('</option').length == 2 ? "span" : "select",
		i: index,
		style: templateStyle[0]?.Style ?? "",
	};

	const counts = {};
	
	templateStyle.forEach(css => {
		let strCss = css.CssClass.replace(/\d+/g, ''); 
		if (!counts.hasOwnProperty(strCss)) {
			counts[strCss] = 0;
		} else {
			counts[strCss]++;
		}
		let uniqueKey = "";
		if (strCss.indexOf("alpha") >= 0) {
			uniqueKey = "alpha";
		} else {
			uniqueKey = counts[strCss] === 0 ? strCss : strCss + counts[strCss];
		}
		uniqueKey = (uniqueKey.indexOf("model") >= 0) ? "model" : uniqueKey
		uniqueKey = (uniqueKey.indexOf("pick") >= 0 || uniqueKey.indexOf("qty") >= 0 || uniqueKey.indexOf("entry") >= 0)  ? "qty" : uniqueKey
		data[uniqueKey] = css.CssClass;   // assign the class name as value, or any other value you want
	});
	data["tblCss"] = templateStyle[0].CssClass; 


	let html = "";
	const numNames = {
		5: "five",
		6: "six",
		7: "seven",
		8: "eight"
	};

	
	const templId = variantTemplate.name[0];

	let row_Template = productTmps["six_Column_Template"];
	
	//if (data.style?.length > 0)
	//	data.borderRow = `<tr style = "line-height:15px"><td colspan = "${templId}" style="${data.style}">&nbsp;</td>`;

	html = row_Template.replace(/{(\w+)}/g, (match, key) => data[key] ?? match); //six_Column_Template , seven_Column_WithOldPrice_Template /seven_Column_Template
	let $row = $(html);

	if (data.frameColors?.length > 0 && templId != 8)
	{
		$row.find('td').eq(2).after(`<td class="${data.detail2} id="frameColor"><${data.select2} size="1" name="AN${index}" style="width: 120px;">${data.frameColors}</${data.select2}></td>`);
	}

	else if (templId == 8 && data.frameColors.length > 0)
	{
		$row.find('td').eq(0).before(`<td class="${data.alpha}"> </td>`);
		$row.find('td').eq(3).after(`<td class="${data.detail2} id="frameColor"><${data.select2} size="1" name="AN${index}" style="width: 120px;">${data.frameColors}</${data.select2}></td>`);
	}

	if (data["oldPrice"] != null && data["oldPrice"] != 0 )
		$row.find('td').eq(templId-3).before(`<td class="${data.priceWas}">${data.oldPrice}</td>`);


	$tbody.append($row[0].outerHTML);	
}



function buildAssociateRow(assoc) {
	const variantTemplate = assoc.productTemplate;
	const templateStyle = JSON.parse(variantTemplate.templateStyle);
	// Quantity input
	const data = {
		sku: assoc.sku,
		tblCss: templateStyle[0].CssClass,
		name: "Covers",
		description: "2 Covers Required Per Bench - ",
		price: formatPrice(assoc.price),
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

