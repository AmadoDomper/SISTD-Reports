

$(document).ready(function () {

    $('#frm #ddlMeta').dropdowlist2({
        dataShow: 'CText',
        dataValue: 'CValue',
        dataselect: 'CValue',
        datalist: Model.lsMetas
    });

    $('#frm #ddlMeta').change(function () {
        var nMetaId = $('#frm #ddlMeta').val();
        var url = $("#actOpe").val();

        if (nMetaId != "") {
            $.fn.Conexion({
                direccion: url,
                bloqueo: true,
                datos: { "nMetaId": nMetaId },
                terminado: function (data) {
                    $('#btbuscar').prop('disabled', '');
                    var data = JSON.parse(data);
                    if (data.length > 0) {
                        $('#ddlActOpe').dropdowlist2({
                            dataShow: 'CText',
                            dataValue: 'CValue',
                            dataselect: 'CValue',
                            datalist: data
                        });

                        
                    }
                    else {
                        //alert('Usted no es responsable de ninguna meta');
                        $('#btbuscar').prop('disabled', 'false');
                    }
                    
                }
            });
        }
        var cLogro = $("#txtLogros").val();
        var nMetaId = $('#ddlMeta').val();
        Listar(nMetaId, -1, cLogro);
    });

    $('#frm #ddlActOpe').change(function () {
        $('#frm #btbuscar').click();
    });

    $('#frm #ddlPeriodo').change(function () {
        $('#frm #btbuscar').click();
    });

    $("#txtLogros").keyup(function (e) {
        if (e.which == 13) {
            $('#frm #btbuscar').click();
        }
    });

    $('#frm #btbuscar').click(function () {

        var nMetaId = -1, nActOpeId = -1, cLogro = "";

        if ($('#ddlActOpe').val() != null) {
             nMetaId = $('#ddlMeta').val();
             nActOpeId = $('#ddlActOpe').val();
             cLogro = $("#txtLogros").val();
        }
        Listar(nMetaId, nActOpeId, cLogro);
        
    });

    function Listar(nMetaId, nActOpeId, cLogro) {

        var url = $("#listBusOpe").val();
        var nPeriodo = $("#ddlPeriodo").val();

        $.fn.Conexion({
            direccion: url,
            bloqueo: true,
            datos: { "nNumMeta": nMetaId, "nInstanciaId": nActOpeId, "cLogro": cLogro, "nPeriodo": nPeriodo },
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
            { name: "cMeta", align: "",headercss: "jsgrid-align-center", title: "N° META", type: "text", width: 150, editing: false },
            { name: "Nombre", align: "", headercss: "jsgrid-align-center", title: "ACTIVIDAD OPERATIVA", type: "text", width: 150, editing: false },
            { name: "cLogro1", align: "", headercss: "jsgrid-align-center", title: "LOGRO", type: "text", width: 250, editing: false },
            {
                name: "AvanceFisico", align: "center", title: "AVANCE FÍSICO", type: "text", width: 50,
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
            rowClass: function (item, itemIndex) {
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

    $('#frm #ddlMeta').change();
    //Listar(-1, -1, "");

});

