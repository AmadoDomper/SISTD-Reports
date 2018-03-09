

$(document).ready(function () {
    $('#frm #ddlplan').dropdowlist2({
        dataShow: 'CText',
        dataValue: 'CValue',
        dataselect: 'CValue',
        datalist: Model.lsPOIs
    });


    $('#frm #ddlplan').change(function () {

        var url = $("#listaPeriodo").val();
        var nPlanOpeId = $('#frm #ddlplan').val();

        if (nPlanOpeId != "") {
            $.fn.Conexion({
                direccion: url,
                bloqueo: false,
                datos: { "PlanOperativoId": nPlanOpeId },
                terminado: function (data) {
                    data = JSON.parse(data);

                    $('#ddlPeriodo').dropdowlist2({
                        dataShow: 'CText',
                        dataValue: 'CValue',
                        dataselect: 'CValue',
                        datalist: data.lsPeriodoCale
                    });
                }
            });
        }

        setTimeout(function () {
            CargarGrilla();
        }, 500)


    });

    $('#frm #ddlplan').change();


    function CargarGrilla() {
        var nPeriodo = $("#ddlPeriodo").val();
        var nPlanOpeId = $("#ddlplan").val();
        var myUrl=$("#myUrl").val();

        $.fn.Conexion({
            direccion: myUrl,
            datos: { "nPeriodo": nPeriodo, "nPlanOpeId": nPlanOpeId },
            bloqueo: true,
            terminado: function (data) {
                data = JSON.parse(data);
                console.log(data);
                cargarDatos(data);
            }
        });
    }

    function cargarDatos(data) {

    var fields = null;

        fields = [

            { name: "NumeroDeMeta", align: "center", title: "N° META", type: "text", width: 20, editing: false },
            { name: "Nombre", align: "", headercss: "jsgrid-align-center", title: "NOMBRE", type: "text", width: 250, editing: false },
            { name: "Usuario", align: "center", title: "RESPONSABLE", type: "text", width: 40, editing: false },
            {
                name: "Eficacia", align: "center", title: "EFICACIA", type: "text", width: 25,
                itemTemplate: function (value, item) {
                    return value + " %";
                },
                editing: false
            }
        ];


        $("#jsGrid").jsGrid({
            height: "auto",
            width: "100%",
            autoload: true,
            editing: false,
            selecting: false,
            rowClass: function(item, itemIndex) {
                return item.Color;
            },
            data: data,
            controller: {
                loadData: function () {
                    return data;
                }
            },
            fields: fields
        });
    }

    CargarGrilla();

    $('#frm #ddlPeriodo').change(function () {
        CargarGrilla();
    });
});