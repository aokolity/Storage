var rowTemplate = "<tr><td style='text-align: center'><span class='rowNumber'>{0}</span></td><td><input id='productCode' tabindex='{1}' class='productCode' type='text'/><input class='hiddenProductID' id='InvoiceModel_Products[{3}]_ProductID' name='InvoiceModel.Products[{3}].ProductID' type='hidden' value='' /></td><td style='text-align: left'><span id='productName' class='productName'></span></td><td><input id='InvoiceModel_Products[{3}]_Quantity' name='InvoiceModel.Products[{3}].Quantity' tabindex='{2}' class='productQuantity' type='text' style='text-align: right'/></td><td style='text-align: left'><span id='productUnit' class='productUnit'></span></td><td style='text-align: right'><span id='productPrice' class='productPrice'></span><input class='hiddenPrice' id='InvoiceModel_Products[{3}]_Price' name='InvoiceModel.Products[{3}].Price' type='hidden' value='' /></td><td style='text-align: right'><span id='total' class='total'></span></td><td style='text-align:center;'><a class='deleteline' href='#'>Удалить</a></td></tr>";

function GetProductByCode(code, parentRow, getProductByCodeURL) {
    $.ajax({
        url: getProductByCodeURL,
        dataType: 'json',
        type: 'POST',
        data: { code: code, priceType: $("#InvoiceModel_PriceType").val() },
        success: function (json) {
            if (json.success != null) {
                $('.productName', parentRow).html(json.Name);
                $('.productUnit', parentRow).html(json.Unit);
                $('.productPrice', parentRow).html(json.Price);
                $('.hiddenPrice', parentRow).val(json.Price);
                $('.hiddenProductID', parentRow).val(json.ID);

                $('.productQuantity', parentRow).focus();
            }
            else {
                $('.productCode', parentRow).focus().select();
                
                alert(json.error);
            }
        }
    });
}

function SetProductCodeEvent(getProductByCodeURL) {
    $('.productCode').live('keypress', function (e) {
        var keyCode = (e.keyCode ? e.keyCode : e.which);

        // Enter key code
        if (keyCode == 13 || keyCode == 9) {
            if (CheckExistingCodeRow($(this))) {
                return false;
            }

            var code = $(this).val();

            if (code) {
                GetProductByCode(code, $(this).parent().parent(), getProductByCodeURL);
            }

            return false;
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
    $('.productQuantity').live('keypress', function (e) {
        var keyCode = (e.keyCode ? e.keyCode : e.which);

        // Enter key code
        if (keyCode == 13 || keyCode == 9) {
            var quantity = $(this).val();

            if (quantity) {
                var parentRow = $(this).parent().parent();

                var price = $('.productPrice', parentRow).asNumber({ region: globalCurrentRegion });
                var number = $('.rowNumber', parentRow).html();
                var tabIndex = $('.productQuantity', parentRow).attr("tabindex");

                $('.total', parentRow).html(quantity * price).formatCurrency({ region: globalCurrentRegion });

                var parentTable = $(this).parent().parent().parent();

                $("tr:last", parentTable).before(rowTemplate.format(parseInt(number) + 1, parseInt(tabIndex) + 1, parseInt(tabIndex) + 2, parseInt(number)));

                var newRow = $("tr:last", parentTable).prev();
                $('.productCode', newRow).focus();

                UpdateTable();
                UpdateMasterTotal();
            }

            return false;
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
                var total = element.asNumber({ region: globalCurrentRegion });
                masterTotal += total;
            }
        }
    });

    $('.mastertotal').html(masterTotal).formatCurrency({ region: globalCurrentRegion });
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