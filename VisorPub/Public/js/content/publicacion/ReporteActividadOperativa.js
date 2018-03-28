

$(document).ready(function () {
    $('#frm #ddlplan').dropdowlist2({
        dataShow: 'CText',
        dataValue: 'CValue',
        dataselect: 'CValue',
        datalist: Model.lsPOIs
    });



    $('#frm #ddlplan').change(function () {

        var url = $("#listaCombos").val();
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

                    $('#frm #ddlMeta').dropdowlist2({
                        dataShow: 'CText',
                        dataValue: 'CValue',
                        dataselect: 'CValue',
                        datalist: data.lsMetas
                    });

                }
            });
        }

        setTimeout(function () {
            ListarActvidadOperativa();
        }, 1000)

        setTimeout(function () {
            $('#frm #btbuscar').click();
        }, 1000)


    });

    
    function ListarActvidadOperativa(){
        var nMetaId = $('#frm #ddlMeta').val();
        var nPlanOpeId = $('#frm #ddlplan').val();
        var url = $("#actOpe").val();

        if (nMetaId != "") {
            $.fn.Conexion({
                direccion: url,
                bloqueo: false,
                datos: { "nMetaId": nMetaId, "nPlanOpeId": nPlanOpeId },
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


        if ($('#ddlActOpe').val() != null) {
            setTimeout(function () {
                $('#frm #btbuscar').click();
            }, 1000)
        }
    }



    $('#frm #ddlMeta').change(function () {
        ListarActvidadOperativa();
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
        var nPlanOpeId = $("#ddlplan").val();

        $.fn.Conexion({
            direccion: url,
            bloqueo: true,
            datos: { "nNumMeta": nMetaId, "nInstanciaId": nActOpeId, "cLogro": cLogro, "nPeriodo": nPeriodo, "nPlanOpeId": nPlanOpeId },
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

    //ListarActvidadOperativa();
    //Listar(-1, -1, "");
    $('#frm #ddlplan').change();


    $("#btnExportar").bind("click", function () {
        var cUrl, nPlanId, cPlan, nPeriodo, cPeriodo, nMetaId = -1, nActOpeId = -1, cLogro = "";

        cUrl = $("#GetExcelReporteLogrosActividadOperativa").val();
        nPlanId = encodeURIComponent($("#ddlplan").val());
        nPeriodo = encodeURIComponent($("#ddlPeriodo").val());
        cPeriodo = $("#ddlPeriodo option:selected").html();
        cPlan = $("#ddlplan option:selected").html();

        cPlan = cPlan.substring(cPlan.indexOf("[") + 1, cPlan.indexOf("]"));
        cPlan = encodeURIComponent(cPlan);
        cPeriodo = encodeURIComponent(cPeriodo);


        if ($('#ddlActOpe').val() != null) {
            nMetaId = encodeURIComponent($('#ddlMeta').val());
            nActOpeId = encodeURIComponent($('#ddlActOpe').val());
            cLogro = encodeURIComponent($("#txtLogros").val());
        }

        window.location.href = cUrl + "?nNumMeta=" + nMetaId + "&nInstanciaId=" + nActOpeId + "&cLogro=" + cLogro + "&nPeriodo=" + nPeriodo + "&nPlanOpeId=" + nPlanId + "&cPlan=" + cPlan + "&cPeriodo=" + cPeriodo;
    });


});

