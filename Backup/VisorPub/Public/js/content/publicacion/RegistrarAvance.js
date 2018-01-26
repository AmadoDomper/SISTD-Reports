

$(document).ready(function () {



    $('#ddlplan').dropdowlist({
        dataShow: 'CText',
        dataValue: 'CValue',
        dataselect: 'CValue',
        datalist: Model.lsPOIsVigentes
    });

    $('#ddlplan').change(function () {
        $.fn.Conexion({
            direccion: '../Inicio/ListarMetasDeResponsableMeta',
            bloqueo: true,
            datos: { "PlanOperativoId": $(this).val(), "ResponsableMeta": Model.idUser },
            terminado: function (data) {
                $('#btbuscar').prop('disabled', '');
                data = JSON.parse(data);
                if (data.length > 0) {
                    $('#ddlMeta').dropdowlist({
                        dataShow: 'CText',
                        dataValue: 'CValue',
                        dataselect: 'CValue',
                        datalist: data
                    });
                }
                else {
                    alert('Usted no es responsable de ninguna meta');
                    $('#btbuscar').prop('disabled', 'false');
                }
                
            }
        });
    });

    $('#btbuscar').click(function () {
        $.fn.Conexion({
            direccion: '../Inicio/ListarMatrizPogramacion',
            bloqueo: true,
            datos: { "InstanciaId": $('#ddlMeta').val(), "PlanOperativoId": $('#ddlplan').val(), "nPeriodo": $('#ddlPeriodo').val() },
            terminado: function (data) {
                data = JSON.parse(data);
                console.log(data);
                cargarDatos(data);
            }
        });
    });

    function cargarDatos(data)
    {

        var nPeriodo = $('#ddlPeriodo').val();
        var fields = null;

        if (nPeriodo == 1) {
            fields = [
                {
                    name: "Nombre", title: "Item", type: "text", width: 80,
                    itemTemplate: function (value, item) {
                        if (item.Nivel == 1) {
                            var $text = $("<p>").text(item.Nombre);
                            return $("<div class='matrizTituloPadre'>").append($text);
                        }
                        return value;
                    },
                    editing: false
                },
                { name: "UnidadDeMedida", title: "Unidad de Medida", type: "text", width: 50, editing: false },
                { name: "Ene", title: "Ene", type: "text", width: 20, editing: false },
                { name: "Feb", title: "Feb", type: "text", width: 20, editing: false },
                { name: "Mar", title: "Mar", type: "text", width: 20, editing: false },
                { name: "Abr", title: "Abr", type: "text", width: 20, editing: false },
                { name: "May", title: "May", type: "text", width: 20, editing: false },
                { name: "Jun", title: "Jun", type: "text", width: 20, editing: false },
                { name: "nAvance1", title: "Avance I Sem.", type: "number", width: 40, editing: true },
                { name: "nMotivoRestraso1", title: "Motivo del retraso", type: "select", items: Model.lsMotivoRetraso, valueField: "CValue", textField: "CText", width: 50, editing: true },
                { name: "cLogro1", title: "Logros más importantes", type: "textarea", width: 80, editing: true },
                { type: "control", modeSwitchButton: false, deleteButton: false, width: 10 }
            ];
        } else if (nPeriodo == 2) {
            fields = [
                {
                    name: "Nombre", title: "Item", type: "text", width: 80,
                    itemTemplate: function (value, item) {
                        if (item.Nivel == 1) {
                            var $text = $("<p>").text(item.Nombre);
                            return $("<div class='matrizTituloPadre'>").append($text);
                        }
                        return value;
                    },
                    editing: false
                },
                { name: "UnidadDeMedida", title: "Unidad de Medida", type: "text", width: 50, editing: false },
                { name: "Jul", title: "Jul", type: "text", width: 20, editing: false },
                { name: "Ago", title: "Feb", type: "text", width: 20, editing: false },
                { name: "Sep", title: "Sep", type: "text", width: 20, editing: false },
                { name: "nAvance2", title: "Avance II Trim.", type: "number", width: 40, editing: true },
                { name: "nMotivoRestraso2", title: "Motivo del retraso", type: "select", items: Model.lsMotivoRetraso, valueField: "CValue", textField: "CText", width: 50, editing: true },
                { name: "cLogro2", title: "Logros más importantes", type: "textarea", width: 80, editing: true },
                { type: "control", modeSwitchButton: false, deleteButton: false, width: 10 }
            ];
        } else if (nPeriodo == 3) {
            fields = [
                {
                    name: "Nombre", title: "Item", type: "text", width: 80,
                    itemTemplate: function (value, item) {
                        if (item.Nivel == 1) {
                            var $text = $("<p>").text(item.Nombre);
                            return $("<div class='matrizTituloPadre'>").append($text);
                        }
                        return value;
                    },
                    editing: false
                },
                { name: "UnidadDeMedida", title: "Unidad de Medida", type: "text", width: 50, editing: false },
                { name: "Oct", title: "Oct", type: "text", width: 20, editing: false },
                { name: "Nov", title: "Nov", type: "text", width: 20, editing: false },
                { name: "Dic", title: "Dic", type: "text", width: 20, editing: false },
                { name: "nAvance3", title: "Avance III Trim.", type: "number", width: 40, editing: true },
                { name: "nMotivoRestraso3", title: "Motivo del retraso", type: "select", items: Model.lsMotivoRetraso, valueField: "CValue", textField: "CText", width: 50, editing: true },
                { name: "cLogro3", title: "Logros más importantes", type: "textarea", width: 80, editing: true },
                { type: "control", modeSwitchButton: false, deleteButton: false, width: 10 }
            ];
        }


        $("#jsGrid").jsGrid({
            height: "auto",
            width: "100%",
            autoload: true,
            editing: true,
            controller: {
                loadData: function () {
                    return data;
                }
            },
            fields: fields,
            onItemUpdated: function (args) {
                console.log(args);
                if (args.item.ID != 0) {
                    args.cancel = true;
                }

                
                var InstanciaId = args.item.ID;

                var nAvance1 = args.item.nAvance1?args.item.nAvance1:0;
                var nMotivoRestraso1 = args.item.nMotivoRestraso1 ? args.item.nMotivoRestraso1 : 0;
                var cLogro1 = args.item.cLogro1 ? args.item.cLogro1 : '';

                var nAvance2 = args.item.nAvance2?args.item.nAvance2:0;
                var nMotivoRestraso2 = args.item.nMotivoRestraso2?args.item.nMotivoRestraso2:0;
                var cLogro2 = args.item.cLogro2?args.item.cLogro2:'';
                
                var nAvance3 = args.item.nAvance3?args.item.nAvance3:0;
                var nMotivoRestraso3 = args.item.nMotivoRestraso3?args.item.nMotivoRestraso3:0;
                var cLogro3 = args.item.cLogro3?args.item.cLogro3:'';

                $.fn.Conexion({
                    direccion: '../Inicio/RegistrarAvancePOI',
                    bloqueo: true,
                    datos: {
                        "InstanciaId":InstanciaId,
                        "nAvance1": nAvance1,
                        "nMotivoRestraso1": nMotivoRestraso1,
                        "cLogro1": cLogro1,
                        "nAvance2": nAvance2,
                        "nMotivoRestraso2": nMotivoRestraso2,
                        "cLogro2": cLogro2,
                        "nAvance3": nAvance3,
                        "nMotivoRestraso3": nMotivoRestraso3,
                        "cLogro3": cLogro3
                    },
                    terminado: function (data) {
                        data = JSON.parse(data);
                        alert(data.Respuesta);
                    }
                });
                
                
            }
        });

        

    }

    

});