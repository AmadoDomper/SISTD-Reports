

$(document).ready(function () {

    $('#frm #ddlplan').dropdowlist2({
        dataShow: 'CText',
        dataValue: 'CValue',
        dataselect: 'CValue',
        datalist: Model.lsPOIs
    });

    $('#frm #ddlplan').change(function () {

        var url = $("#listarActividadEstrategica").val();
        var nPlanOpeId = $('#frm #ddlplan').val();

        if (nPlanOpeId != "") {
            $.fn.Conexion({
                direccion: url,
                bloqueo: false,
                datos: { "PlanOperativoId": nPlanOpeId},
                terminado: function (data) {
                    $('#btbuscar').prop('disabled', '');
                    data = JSON.parse(data);
                        $('#ddlAcEst').dropdowlist2({
                            dataShow: 'CText',
                            dataValue: 'CValue',
                            dataselect: 'CValue',
                            datalist: data.lsAcEst
                        });

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
            $('#frm #btbuscar').click();
        }, 500)

        
    });

    $('#frm #ddlplan').change();

    //$('#frm #ddlplan').change(function () {
    //    $('#frm #btbuscar').click();
    //});

    $('#frm #ddlAcEst').change(function () {
        $('#frm #btbuscar').click();
    });

    $('#frm #ddlPeriodo').change(function () {
        $('#frm #btbuscar').click();
    });

    $('#frm #btbuscar').click(function () {

        var nAEId = -1;
        var nPeriodo = 1;

        if ($('#ddlAcEst').val() != null) {
            nAEId = $('#ddlAcEst').val();
            nPeriodo = $("#ddlPeriodo").val();
        }
        Listar(nAEId, nPeriodo);

    });

    function Listar(nAEId, nPeriodo) {

        var url = $("#listaRepActOpe").val();
        var nplanOpeId = $("#ddlplan").val();

        $.fn.Conexion({
            direccion: url,
            bloqueo: true,
            datos: { "nAEId": nAEId, "nPeriodo": nPeriodo, "nPlanOperativoId": nplanOpeId },
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

