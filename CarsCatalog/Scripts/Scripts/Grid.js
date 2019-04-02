$(function () {
    $('#brandsBtnLink').removeClass('hidden');
    $('#catalogBtnLink').removeClass('hidden');

    $("#jqg").jqGrid({
        url: $("table").attr('href'),
        datatype: "json",
        colNames: ["Car_Id", "Brand_Id", "Model_Id", "Color", "Volume engine", "Description", "Price"],
        colModel: [
            {
                name: "Id", width: 50, stype: 'text', sortable: true, key: true, search: false, hidden: true
            },
            {
                name: "BrandId", editable: true, edittype: 'text', hidden: true
            },
            {
                name: "ModelId", editable: true, edittype: 'text', hidden: true
            },
            {
                name: "Color",
                width: 80,
                align: 'center',
                editable: true,
                edittype: 'text',
                sortable: true,
                sorttype: 'text',
                editrules: { required: true }
            }
            ,
            {
                name: "VolumeEngine",
                width: 100,
                align: 'center',
                editable: true,
                edittype: 'text',
                sortable: true,
                editrules: { required: true }
            },
            {
                name: "Description",
                width: 450,
                editable: true,
                edittype: 'text',
                search: false,
                editoptions: {
                    dataInit: function (elem) {
                        $(elem).width(300);
                    }
                }
            },
            {
                name: "Price",
                index: 'Price',
                width: 80,
                align: 'center',
                editable: true,
                sortable: true,
                formatter: 'number',
                editrules: { required: true },
                search: false
            }
        ],
        jsonReader: { repeatitems: false },
        rownumbers: true,
        rownumWidth: 50,
        rowNum: 10,
        rowList: [5, 10, 20, 50],
        height: 230,
        pager: '#jpager',
        emptyrecords: "Nothing to display",
        loadonce: false,
        sortname: 'Id',
        sortorder: "asc",
        viewrecords: true,
        caption: "Cars",
        search: {
            caption: "Search...",
            Find: "Find",
            Reset: "Reset",
            odata: ['equal', 'not equal', 'less', 'less or equal', 'greater', 'greater or equal', 'begins with', 'does not begin with', 'is in', 'is not in', 'ends with', 'does not end with', 'contains', 'does not contain'],
            groupOps: [{ op: "AND", text: "all" }, { op: "OR", text: "any" }],
            matchText: " match",
            rulesText: " rules"
        }
    });

    $("#jqg").jqGrid('navGrid', '#jpager', {
        refresh: true,
        add: true,
        del: true,
        edit: true,
        view: true,
        search: true,
        searchtext: "Search",
        addtext: 'Add',
        deltext: 'Delete',
        edittext: 'Edit',
        viewtext: 'View'
    },
        update("edit"),
        update("add"),
        update("del")
    );
    function update(act) {
        return {
            closeAfterAdd: true,
            height: 250,
            width: 600,
            closeAfterEdit: true,
            reloadAfterSubmit: true,
            drag: true,
            onclickSubmit: function (params) {
                var list = $("#jqg");
                var selectedRow = list.getGridParam("selrow");
                rowData = list.getRowData(selectedRow);
                switch (act) {
                    case "add":
                        params.url = $("#jpg-href").attr('href-create');
                        break;
                    case "del":
                        params.url = $("#jpg-href").attr('href-delete');
                        break;
                    case "edit":
                        params.url = $("#jpg-href").attr('href-edit');
                        break;
                }
            },
            afterSubmit: function (response, postdata) {
                $(this).jqGrid('setGridParam', { datatype: 'json' }).trigger('reloadGrid');
                return [true, "", 0];
            }
        };
    };
});