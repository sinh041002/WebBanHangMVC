
$(document).ready(function () {
    ShowCount()
    $('body').on('click', '.btn-addtoCard', function (e) {
        e.preventDefault();
        var id = $(this).data('id')
        var quantity = 1;
        var tquantity = $('#quantity_value').text();
        if (tquantity != '') {
            quantity = parseInt(tquantity);
        }

        $.ajax({
            url: '/shoppingcard/AddToCart',
            type: 'POST',
            data: { id: id, quantity: quantity },
            success: function (rs) {
                $('#checkout_items').html(rs.Count);
                alert(rs.msg);
            }
        });
    });
    $('body').on('click', '.btndeleteAll', function (e) {
        e.preventDefault();
        var conf = confirm('Bạn có chắc muốn xóa hết sản phẩm trong giỏ hàng?');
        //debugger;
        if (conf == true) {
            DeleteAll();
        }

    });
    $('body').on('click', '.btnUpdate', function (e) {
        e.preventDefault();
        var id = $(this).data("id");
        var quantity = $('#Quantity_' + id).val();
        debugger
        Update(id, quantity);

    });
    $('body').on('click', '.btndelete', function (e) {
        e.preventDefault();
        var id = $(this).data('id')



        $.ajax({
            url: '/shoppingcard/Delete',
            type: 'POST',
            data: { id: id },
            success: function (rs) {

                $('#trow_' + id).remove();
                ShowCount();
                LoadCart();
            }
        });
    });

});
function ShowCount() {
    $.ajax({
        url: '/shoppingcard/ShowCount',
        type: 'GET',
        success: function (rs) {
            $('#checkout_items').html(rs.Count);
        }
    });
}
function DeleteAll() {
    $.ajax({
        url: '/shoppingcard/DeleteAll',
        type: 'POST',
        success: function (rs) {
            if (rs.Success) {
               
                ShowCount();
                LoadCart();
            }
        }
    });
}
function Update(id, quantity) {
    $.ajax({
        url: '/shoppingcard/Update',
        type: 'POST',
        data: { id: id, quantity: quantity },
        success: function (rs) {
            if (rs.Success) {
                LoadCart();
            }
        }
    });
}
function LoadCart() {
    $.ajax({
        url: '/shoppingcard/Partial_Item_Cart',
        type: 'GET',
        success: function (rs) {
            $('#load_data').html(rs);
        }
    });
}