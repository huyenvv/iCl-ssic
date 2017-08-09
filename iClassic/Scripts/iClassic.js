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

            $this.parent().addClass("active").parent().show();
            $this.parents(".nav-parent").addClass("active nav-active");
        });
    }
    activeMenu();
});