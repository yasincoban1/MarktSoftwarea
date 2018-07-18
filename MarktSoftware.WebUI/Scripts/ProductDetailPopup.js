$(function () {

    $('#modal_productdetail').on('show.bs.modal',
        function(e) {

            var btn = $(e.relatedTarget);
            productid = btn.data("product-id");

            $("#modal_productdetail_body").load("/Product/GetProductText/" + productid);
        });

});