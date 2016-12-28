$(document).ready(function () {
    $(".confirmDelete").click(function () {
        return confirm("Bạn có chắc?");
    });
    $("ul#Menu li a").each(function () {
        var item = $(this);
        var link = item.attr('href') + "/" + item.attr('data-href') + "/";
        var isActive = link.indexOf(getController() + "/") != -1 && link.indexOf(getAction() + "/") != -1;
        if (isActive) {
            item.parent().addClass('active');
        }
    });

    $("#SoTien, #TienThuaThangTruoc").autoNumeric("init", {
        aSep: ',',
        aDec: '.',
        mDec: 0
    });

    setCheckAll(".checkAll", ".checkitem");
    setCheckAll(".checkAllSang", ".checkitemSang");
});

function beforAddMoney() {
    var newValue = $("#SoTien").val().replace(/,/g, "");
    $("#SoTien").val(newValue);
    return true;
}

function setCheckAll(checkAllCls, checkItemCls) {
    // show hide button Edit & Delete when click checkbox
    $(checkAllCls).click(function () {
        if ($(this).is(':checked')) {
            $(checkItemCls).prop('checked', true);
        } else {
            $(checkItemCls).prop('checked', false);
        }
    });
    $(checkItemCls).click(function () {
        var numberOfChecked = $('input:checkbox:checked').length;
        var numberItem = $('input:checkbox' + checkItemCls).length;
        if ($(this).is(':checked')) {
            if (numberItem === numberOfChecked) {
                $(checkAllCls).prop('checked', true);
            }
        } else {
            $(checkAllCls).prop('checked', false);
        }
    });
}
