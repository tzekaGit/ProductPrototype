const T2CellProductGroupHeader_Template = `
<table class="{cssClass}">
    <tr>
       <td class="item" class = "ptExpanded2"><h2 class="px13"> {name1} | <span class="cffff00">{name2}</span></h2></td>
       <td class="qty">Qty</td>
    </tr>
 </table>
`;

const T4CellproductGroupHeaderWithDiscount_Template = `
        <table class="{cssClass}">
        <tr >
            <td class="itemSpecial440"><h2 class="px13">{name1} <span class="cffff00"> |{name2}</span></h2></td>
            <td class="was50">Was</td>
            <td class="special75">Special</td>
            <td class="qty">Qty</td>
        </tr>
   </table>
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

const five_Column_Template = "";

const six_Column_Template = ` 
<table  class="{tblCss}" style="{style}">
      <tr>
        <td class="{model}"><a href="javascript:gallery('{sku}')">{sku}</a></td>
        <td class="{detail}">{specification}</td>
        <td class="{detail1}" id="seatColor"><{select1} size="1" name="AN{i}" style="width: 120px;">{seatColors}</{select1}></td>
        <td class="{lbs}">({weight} lbs)</td>
        <td class="{price}">{itemPrice}</td>
        <td class="{qty}"><input name="QTY{i}" size="2" maxlength="6" style="text-align: center;" class="qty_box" /></td>
      </tr>
      {borderRow}
   </table>
`;


const related_Product_Template =
    `
    <table class="{tblCss}" style="{style}">
    <tr>
		<td class="ocModel">{sku}</td>
		<td class="ocDetail" style="text-indent: .42em;"><b>Optional {name}</b> {description} - {price}/{unit}</td>
		<td class="ocYes"><input type="radio" name="GROUP{i}" value="No" /><span class="rtx">No</span></td>
		<td class="ocNo"><input type="radio" name="GROUP{i}" value="Yes" /><span class="rtx">Yes</span><input type="hidden" name="QTY{i}" value="0" /><input type="hidden" name="AN{i}" value="" /></td>
      </tr>
    </table>
    `;
const nav_template = '<li><a href="/id={productIdid}">{productName}</a></li>'

const productTmps =
{
    //"five_Column_Template": five_Column_Template,
    "six_Column_Template": six_Column_Template,
    //"seven_Column_Template": seven_Column_Template,
    //"eight_Column_Template": eight_Column_Template
}