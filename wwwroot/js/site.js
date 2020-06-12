// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

//TODO: Write your JavaScript code.

jQuery(function () {

    jQuery('#main_navbar').bootnavbar({
        animation: false
    });

    jQuery("label.btn").not(".disabled").click(function () {
        var jqBtn = jQuery(this);

        jqBtn.parent().children().each(function () {
            var jqItem = jQuery(this);
            var itemSelectedState = jqItem.data('selected-state');
            jqItem.removeClass(itemSelectedState).addClass("btn-secondary");
        });

        var btnSelectedState = jqBtn.data('selected-state');
        jqBtn.removeClass("btn-secondary").addClass(btnSelectedState);
    });

    var flatpickrConfig = {
        locale: { firstDayOfWeek: 1 },
        altInput: true,
        altFormat: "dHi\\ZMy",//DTG format eg. DDHHMMZMONYY => 312358ZJAN20
        dateFormat: "Y-m-d H:i",
        time_24hr: true,
        enableTime: true,
        allowInput: true,
        clickOpens: false,
        wrap: true,
        onOpen: function (selectedDates, dateStr, instance) {
            if (dateStr === "")
                instance.setDate("today");
        }
    };

    jQuery(".datetimepickr").flatpickr(flatpickrConfig);

    jQuery('[data-toggle="tooltip"]').tooltip();

    jQuery(".customSelect .dropdown-item").click(function () {
        var jqItem = jQuery(this);
        var navopdef = jqItem.closest(".customSelect");
        var dropdownItems = jQuery(".dropdown-item", navopdef);
        dropdownItems.removeClass("active");
        jqItem.addClass("active");
        jQuery("input:text", navopdef).val(jqItem.data("value"));
    });
});