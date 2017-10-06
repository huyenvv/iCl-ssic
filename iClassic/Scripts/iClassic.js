function formatMoney(n, unit) {
    if (unit == undefined) unit = "₫";
    return n.toFixed(0).replace(/./g, function (c, i, a) {
        return i > 0 && c !== "." && (a.length - i) % 3 === 0 ? "," + c : c;
    }) + " " + unit;
}
function formatPrices() {
    $(".price").each(function () {
        var $this = $(this);
        var value = parseInt($this.val().replace(/,/g, '')) | 0;
        $this.val(formatMoney(value, ''));
    });
}

jQuery(document).ready(function () {

    "use strict";
    function validateMenuLink($this) {
        var itemUrl = $this.attr('href') + '/' + $this.attr('data-href');
        var isActive = itemUrl.indexOf(iclassic.currentLink) != -1;
        return isActive;
    }

    function activeMenu() {
        var found = false;
        jQuery("#LeftMenu > li > a").each(function () {
            var $this = jQuery(this);
            var isActive = validateMenuLink($this);
            if (!isActive) return;

            found = true;
            $this.parent().addClass("active");
        });

        if (found) return;

        jQuery("#LeftMenu .children li a").each(function () {
            var $this = jQuery(this);
            var isActive = validateMenuLink($this);
            if (!isActive) return;

            $this.parent().addClass("active");
            if (!$("body").hasClass("leftpanel-collapsed")) {
                $this.parent().parent().show();
            }
            $this.parents(".nav-parent").addClass("active nav-active");
        });
    }
    activeMenu();
});