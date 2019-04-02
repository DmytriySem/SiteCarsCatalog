$(function () {
    $.ajaxSetup({ cache: false });

    $("#dialogContent").dialog({
        autoOpen: false,
        resizable: false,
        minWidth: 550,
        show: { effect: "fade", duration: 1000 },
        hide: { effect: "fade", duration: 1000 },
        modal: true,
        draggable: true,
        open: function (event, ui) {
            $(".ui-dialog-titlebar-close", ui.dialog | ui).hide();
        }
    });

    $(".view-dialog").on("click", function (e) {
        let href = $(this).attr("href");
        let title = $(this).attr("data-dialog-title");
        let action = $(this).attr("data-action"); 

        $("#dialogContent")
            .attr({ "data-href": href, "data-action": action })
            .load(href)
            .dialog("option", "title", title)
            .dialog("option", "buttons", getButtons(action))
            .dialog("open");

        $('.ui-dialog .ui-dialog-buttonpane button')
            .addClass("ui-priority-secondary ui-button ui-widget ui-corner-all");

        return false;
    });

    function getButtons(action) {
        let buttons = {};

        switch (action) {
            case "ADD":
                buttons["SAVE"] = save;

                break;
            case "EDIT":
                buttons["UPDATE"] = update;

                break;
            case "DELETE":
                buttons["DELETE"] = remove;
                break;
        }
        buttons["CANCEL"] = cancel;

        return buttons;
    }
    function save() {
        let url = $("#dialogContent").attr("data-href");
        let model = new FormData($('form')[0]);
        $.ajax({
            url: url,
            type: "POST",
            data: model,
            dataType: "html",
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                let response = $('<html />').html(data);
                let isValid = response.find("input").hasClass("input-validation-error");
                let isValidSum = response.find("div").hasClass("validation-summary-errors");

                if (isValid || isValidSum) {
                    $("#dialogContent").empty().html(data);
                }
                else {
                    $("#dialogContent").dialog("close");
                    window.location.reload();
                }
            }
        });
    }
    function update() {
        let url = $("#dialogContent").attr("data-href");
        let model = new FormData($('form')[0]);
        $.ajax({
            url: url,
            type: "POST",
            data: model,
            dataType: "html",
            cache: false,
            contentType: false,
            processData: false,
            success: function (data) {
                let response = $('<html />').html(data);
                let isValid = response.find("input").hasClass("input-validation-error");
                
                if (isValid) {
                    $("#dialogContent").empty().html(data);
                }
                else {
                    $("#dialogContent").dialog("close");
                    window.location.reload();
                }
            }
        });
    }
    function cancel() {
        $(this).dialog("close");
    }
    function remove() {
        $($('form')[0]).submit();
    }
});