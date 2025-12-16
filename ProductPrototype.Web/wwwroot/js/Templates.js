const productGroupHeader_Template = `
    <tr class="{cssClass}">
       <td class="item" colspan="6" class = "ptExpanded2"><h2 class="px13"> {name1} | <span class="cffff00">{name2}</span></h2></td>
       <td class="qty">Qty</td>
    </tr>
`;

const productGroupHeaderWithDiscount_Template = `
        <tr class="{cssClass}">
            <td class="itemSpecial440" colspan="4"><h2 class="px13">{name1} <span class="cffff00"> |{name2}</span></h2></td>
            <td class="was50">Was</td>
            <td class="special75">Special</td>
            <td class="qty">Qty</td>
        </tr>
`;

const benchProductColumns = `
<thead>
    <tr>
        <th>Sku</th>
        <th>MountType</th>
        <th style="width: 120px;">Seat Color</th>
        <th style="width: 120px;">Frame Color</th>
        <th>Weight</th>
        <th>Price</th>
        <th>QTY</th>
    </tr>
</thead>
`;
const six_Column_Template = `
      <tr  style="{style}">
        <td class="model85"><a href="javascript:gallery('{sku}')">{sku}</a></td>
        <td class="detail110">{specification}</td>
        <td class="detail120" id="seatColor"><{select1} size="1" name="AN{i}" style="width: 120px;">{seatColors}</{select1}></td>
        <td class="lbs55">({weight} lbs)</td>
        <td class="price75">{price}</td>
        <td class="entry"><input name="QTY{i}" size="2" maxlength="6" style="text-align: center;" class="qty_box" /></td>
      </tr>
`;

const seven_Column_Template = ` 
      <tr  style="{style}">
        <td class="model85"><a href="javascript:gallery('{sku}')">{sku}</a></td>
        <td class="detail110">{specification}</td>
        <td class="detail120" id="seatColor"><{select1} size="1" name="AN{i}" style="width: 120px;">{seatColors}</{select1}></td>
        <td class="detail120" id="frameColor"><{select2} size="1" name="BN{i}" style="width: 120px;">{frameColors}</{select2}></td>
        <td class="lbs55">({weight} lbs)</td>
        <td class="price75">{price}</td>
        <td class="entry"><input name="QTY{i}" size="2" maxlength="6" style="text-align: center;" class="qty_box" /></td>
      </tr>
`;

seven_Column_WithOldPrice_Template = `
    <tr>
		    <td class="model125">{sku}</td>
		    <td class="detail110">{specification}</td>
		    <td class="detail120"><{select1} size="1" name="AN{i}" style="width: 120px;">{seatColors}</{select1}></td>
		    <td class="lbs65">({weight} lbs)</td>
		    <td class="priceWas70">{oldPrice}</td> 
		    <td class="price65">{price}</td>
		    <td class="pick"><input name="QTY{i}" size="2" maxlength="6" style="text-align: center;" class="qty_box" /></td>
		</tr>
`;



const eight_Column_Template = `
      <tr  style="{style}">
        <td class="alpha"><img border="0" src="./Park Bench with Diamond Pattern Steel _ Belson Outdoors®_files/UA-Glyph2-0.gif" width="20" height="20" alt="UA Logo"></td>
        <td class="model85"><a href="javascript:gallery('{sku}')">{sku}</a></td>
        <td class="detail110">{specification}</td>
        <td class="detail120" id="seatColor"><{select1} size="1" name="AN{i}" style="width: 120px;">{seatColors}</{select1}></td>
        <td class="detail120" id="frameColor"><{select2} size="1" name="BN{i}" style="width: 120px;">{frameColors}</{select2}></td>
        <td class="lbs55">({weight} lbs)</td>
        <td class="price75">{price}</td>
        <td class="entry"><input name="QTY{i}" size="2" maxlength="6" style="text-align: center;" class="qty_box" /></td>
      </tr>
`;

const related_Product_Template =
    `
    <tr>
		<td class="ocModel">{sku}</td>
		<td class="ocDetail" style="text-indent: .42em;"  colspan="4"><b>Optional {name}</b> {description} - {price}/{unit}</td>
		<td class="ocYes"><input type="radio" name="GROUP{i}" value="No" /><span class="rtx">No</span></td>
		<td class="ocNo"><input type="radio" name="GROUP{i}" value="Yes" /><span class="rtx">Yes</span><input type="hidden" name="QTY{i}" value="0" /><input type="hidden" name="AN{i}" value="" /></td>
      </tr>

    `;

const dimensions_template = '<div class="dimD"> <b><span style="color: #616c7b;">{specificationOptionName}</span> - {length}&quot;L x {width} &quot;W x {height}&quot;H</b></div>'
