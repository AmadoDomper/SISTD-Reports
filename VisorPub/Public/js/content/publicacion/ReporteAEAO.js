

$(document).ready(function () {

    $('#frm #ddlAcEst').dropdowlist2({
        dataShow: 'CText',
        dataValue: 'CValue',
        dataselect: 'CValue',
        datalist: Model.lsAcEst
    });

    $('#frm #ddlAcEst').change(function () {
        $('#frm #btbuscar').click();
    });

    $('#frm #ddlPeriodo').change(function () {
        $('#frm #btbuscar').click();
    });

    $('#frm #btbuscar').click(function () {

        var nAEId = -1;

        if ($('#ddlAcEst').val() != null) {
            nAEId = $('#ddlAcEst').val();
        }
        Listar(nAEId);

    });

    function Listar(nAEId) {

        var url = $("#listaRepActOpe").val();
        var nPeriodo = $("#ddlPeriodo").val();

        $.fn.Conexion({
            direccion: url,
            bloqueo: true,
            datos: { "nAEId": nAEId, "nPeriodo": nPeriodo },
            terminado: function (data) {
                var data = JSON.parse(data);
                //console.log(data);
                cargarDatos(data);
            }
        });
    }

    function cargarDatos(data) {
        var fields = null;

        fields = [
            { name: "cAENombre", align: "", headercss: "jsgrid-align-center", title: "ACCIÓN ESTRATÉGICA", type: "text", width: 150, editing: false },
            { name: "cActividadOpe", align: "", headercss: "jsgrid-align-center", title: "ACTIVIDAD OPERATIVA", type: "text", width: 150, editing: false },
            { name: "cLogro1", align: "", headercss: "jsgrid-align-center", title: "LOGRO", type: "text", width: 250, editing: false }
        ];

        $("#jsGrid").jsGrid({
            height: "auto",
            width: "100%",
            autoload: true,
            editing: false,
            selecting: false,
            data: data,
            controller: {
                loadData: function () {
                    return data;
                }
            },
            fields: fields
        });
    }

    $('#frm #ddlAcEst').change();
    //Listar(-1, -1, "");

});

