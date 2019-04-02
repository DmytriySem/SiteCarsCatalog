$(function () {
    //----------------------------------------------------------------------------------
    //---------------------------JSTREE-------------------------------------------------
    //----------------------------------------------------------------------------------

    $('#jstree').jstree({
        'core': {
            'check_callback': true,
            'dblclick_toggle': false,
            'data': {
                'url': '/Catalog/GetTree',
                'dataType': 'json'
            },
            'themes': {
                'theme': 'default-dark',
                'dots': false,
                'icons': false
            },
            'plugins': ['themes']
        }

    });
    $('#jstree').attr("selected-tree-node", "ALL");

    $('#jstree').on("select_node.jstree", function (e, data) {
        $('#jstree').jstree("toggle_node", data.node);

        let nodeName = $(this).jstree().get_selected(true)[0].text;

        if (nodeName !== $('#jstree').attr("selected-tree-node")) {
            $('#jstree').attr("selected-tree-node", nodeName);
            getListOfCars();
        }
    });

    //----------------------------------------------------------------------------------
    //---------------------------SLIDER----------------------------------------------
    //----------------------------------------------------------------------------------

    let minPrice = $("#price").data("min-price") - 0;
    let maxPrice = $("#price").data("max-price");// + 10;

    $("#slider").slider({
        range: true,
        min: minPrice,
        max: maxPrice,
        step: 0.1,
        values: [minPrice, maxPrice],
        create: function () {
            $("#price").text(minPrice + "$ - " + maxPrice + "$");
        },
        slide: function (event, ui) {
            $("#price").text(ui.values[0] + "$ - " + ui.values[1] + "$");
        },
        stop: function (event, ui) {
            getListOfCars();
        }
    });

    //----------------------------------------------------------------------------------
    //---------------------------DATEPICKER----------------------------------------------
    //----------------------------------------------------------------------------------
    $("#datepicker").datepicker().on('change Date', function () { getListOfCars(); });

    //----------------------------------------------------------------------------------
    //---------------------------COLORS----------------------------------------------
    //----------------------------------------------------------------------------------
    $("#colorsSelect").change(function () {
        $("#colorsSelect > option[selected]").removeAttr('selected');
        $("#colorsSelect option:selected").attr('selected', 'selected');

        getListOfCars();
    });

    //----------------------------------------------------------------------------------
    //---------------------------Engines----------------------------------------------
    //----------------------------------------------------------------------------------
    $("#volEngSelect").change(function () {
        $("#volEngSelect > option[selected]").removeAttr('selected');
        $("#volEngSelect option:selected").attr('selected', 'selected');

        getListOfCars();
    });

    //----------------------------------------------------------------------------------
    //---------------------------ItemsInRow----------------------------------------------
    //----------------------------------------------------------------------------------
    $("#tileCount").change(function () {
        $("#tileCount > option[selected]").removeAttr('selected');
        $("#tileCount option:selected").attr('selected', 'selected');

        rebuildCarsList();
    });

    getListOfCars();

    //----------------------------------------------------------------------------------
    //---------------------------LoadData-----------------------------------------------
    //----------------------------------------------------------------------------------
    function getListOfCars() {
        $.ajax({
            url: "/Catalog/GetCars",
            type: "POST",
            data: {
                treeNodeName: $('#jstree').attr("selected-tree-node"),
                color: $("#colorsSelect option:selected").val(),
                volEngine: $("#volEngSelect option:selected").val(),
                colsNum: $("#tileCount option:selected").val(),
                minPrice: $("#slider").slider("values", 0),
                maxPrice: $("#slider").slider("values", 1),
                date: $('#datepicker').datepicker('getDate').toDateString()
            },
            success: function (data) {
                var carsDiv = $("#findCars");
                carsDiv.empty();
                carsDiv.append(data);

                var foundCars = $('#findCars > div').data('found-cars');
                $('#countCars').empty();
                $('#countCars').append('Number of vehicles found: ' + '<strong>' + foundCars + '</strong>');    
            },
            error: function (data) {
                alert("Get cars fault");
            }
        });

        //при загрузке установить ALL красниым цветом --!!!!!!!!!!!!!!!!!!!!!!!!!
        //$('#ALL_anchor').addClass('jstree-clicked');
    }

    //----------------------------------------------------------------------------------
    //---------------------------RebuildCarsList----------------------------------------
    //----------------------------------------------------------------------------------
    function rebuildCarsList() {
        var sel = $("#tileCount option:selected").val();
        var width = Math.floor(100 / sel) + "%";

        $('#findCars article').css('width', width);

        //$('#findCars article').each(function () {
        //    $(this).css('width', width);
        //});
    }

     //----------------------------------------------------------------------------------
    //---------------------------Reset----------------------------------------
    //----------------------------------------------------------------------------------
    $('#resetBtn').click(function () {
        $('#jstree').jstree("select_node", '#ALL_anchor', true);
        $('#jstree').attr("selected-tree-node", "ALL");

        $("#colorsSelect option:selected").val("ALL");
        $("#colorsSelect :first").attr('selected', 'selected');

        $("#volEngSelect option:selected").val("ALL");
        $("#volEngSelect :first").attr('selected', 'selected');
        
        $("#tileCount option:selected").removeAttr('selected');
        $("#tileCount :contains('5')").attr('selected', 'selected');

        $("#slider").slider("values", 0, minPrice);
        $("#slider").slider("values", 1, maxPrice);
        $("#price").text("$" + minPrice + " - $" + maxPrice);

        $("#datepicker").datepicker('setDate', 'today');

        getListOfCars();
    });


});