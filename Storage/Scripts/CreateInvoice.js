var rowTemplate = "<tr><td style='text-align: center'><span class='rowNumber'>{0}</span></td><td><input id='productCode' tabindex='{1}' class='productCode' type='text'/><input class='hiddenProductID' id='InvoiceViewModel_InvoiceModel_Products[{3}]_ProductID' name='InvoiceViewModel.InvoiceModel.Products[{3}].ProductID' type='hidden' value='' /></td><td style='text-align: left'><span id='productName' class='productName'></span></td><td><input id='InvoiceViewModel_InvoiceModel_Products[{3}]_Quantity' name='InvoiceViewModel.InvoiceModel.Products[{3}].Quantity' tabindex='{2}' class='productQuantity' type='text' style='text-align: right'/></td><td style='text-align: left'><span id='productUnit' class='productUnit'></span></td><td style='text-align: right'><span id='productPrice' class='productPrice'></span><input class='hiddenPrice' id='InvoiceViewModel_InvoiceModel_Products[{3}]_Price' name='InvoiceViewModel.InvoiceModel.Products[{3}].Price' type='hidden' value='' /></td><td style='text-align: right'><span id='total' class='total'></span></td><td><a class='deleteline' href='#'>Удалить</a></td></tr>";

function GetProductByCode(code, parentRow, getProductByCodeURL) {
    $.ajax({
        url: getProductByCodeURL,
        dataType: 'json',
        type: 'POST',
        data: { code: code, priceType: $("#PriceType").val() },
        success: function (json) {
            if (json.success != null) {
                $('.productName', parentRow).html(json.Name);
                $('.productUnit', parentRow).html(json.Unit);
                $('.productPrice', parentRow).html(json.Price);
                $('.hiddenPrice', parentRow).val(json.Price);
                $('.hiddenProductID', parentRow).val(json.ID);
            }
            else {
                alert(json.error);
            }
        }
    });
}

function SetProductCodeEvent(getProductByCodeURL) {
    $('.productCode').live('blur', function () {
        if (CheckExistingCodeRow($(this))) {
            return;
        }

        var code = $(this).val();

        if (code) {
            GetProductByCode(code, $(this).parent().parent(), getProductByCodeURL);
        }
    });
}

function CheckExistingCodeRow(currentProductCodeElement) {
    var currentProductCode = $(currentProductCodeElement).val();

    $('.productCode').not(currentProductCodeElement).each(function () {
        var productCode = $(this).val();

        if (currentProductCode == productCode) {
            $('.productQuantity', $(this).parent().parent()).focus().select();

            DeleteRow(currentProductCodeElement.parent().parent());

            return true;
        }
    });

    return false;
}

function SetProductQuantityEvent() {
    $('.productQuantity').live('blur', function () {
        var quantity = $(this).val();

        if (quantity) {
            var parentRow = $(this).parent().parent();

            var price = $('.productPrice', parentRow).asNumber({ region: currentRegion });
            var number = $('.rowNumber', parentRow).html();
            var tabIndex = $('.productQuantity', parentRow).attr("tabindex");

            $('.total', parentRow).html(quantity * price).formatCurrency({ region: currentRegion });

            var parentTable = $(this).parent().parent().parent();

            $("tr:last", parentTable).before(rowTemplate.format(parseInt(number) + 1, parseInt(tabIndex) + 1, parseInt(tabIndex) + 2, parseInt(number)));

            var newRow = $("tr:last", parentTable).prev();
            $('.productCode', newRow).focus();

            UpdateTable();
            UpdateMasterTotal();
        }
    });
}

function DeleteRow(rowElement) {
    rowElement.remove();

    if ($("tr", $('#invoiceProducts')).size() == 2) {
        InsertFirstRow();
    }

    UpdateTable();
    UpdateMasterTotal();
}

function SetDeleteLineEvent() {
    $('.deleteline').live('click', function () {
        DeleteRow($(this).parent().parent());
    });
}

function UpdateMasterTotal() {
    var masterTotal = 0;

    $('tr', $('#invoiceProducts')).each(function () {
        var element = $('.total', $(this));

        if (element.length > 0) {
            var totalHtml = element.html();

            if (totalHtml) {
                var total = element.asNumber({ region: currentRegion });
                masterTotal += total;
            }
        }
    });

    $('.mastertotal').html(masterTotal).formatCurrency({ region: currentRegion });
}

function InsertFirstRow() {
    $("tr:last", $('#invoiceProducts')).before(rowTemplate.format(1, 1, 2, 0));
}

function UpdateTable() {
    var rowNumberPosition = 1;
    var tabIndexPosition = 1;

    $('tr', $('#invoiceProducts')).each(function () {

        // updates rowNumber
        var rowNumberElement = $('.rowNumber', $(this));

        if (rowNumberElement.length > 0) {
            rowNumberElement.html(rowNumberPosition);
            rowNumberPosition += 1;
        }

        // updates tabIndex
        var productCodeElement = $('.productCode', $(this));
        var productQuantityElement = $('.productQuantity', $(this));

        if (productCodeElement.length > 0 && productQuantityElement.length > 0) {

            productCodeElement.attr("tabindex", tabIndexPosition);
            tabIndexPosition += 1;

            productQuantityElement.attr("tabindex", tabIndexPosition);
            tabIndexPosition += 1;
        }
    });
}