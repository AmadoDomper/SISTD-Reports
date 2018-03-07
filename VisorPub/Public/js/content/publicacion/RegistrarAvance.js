

$(document).ready(function () {
    $('#frm #ddlplan').dropdowlist({
        dataShow: 'CText',
        dataValue: 'CValue',
        dataselect: 'CValue',
        datalist: Model.lsPOIsVigentes
    });

    $('#frm #ddlplan').change(function () {

        var url = $("#listarCombosxPlanOperativoId").val();
        var nPlanOpeId = $('#frm #ddlplan').val();

        if (nPlanOpeId != "") {
            $.fn.Conexion({
                direccion: url,
                //direccion: '/Inicio/ListarMetasDeResponsableMeta',
                bloqueo: true,
                datos: { "PlanOperativoId": nPlanOpeId, "ResponsableMeta": Model.idUser },
                terminado: function (data) {
                    $('#btbuscar').prop('disabled', '');
                    data = JSON.parse(data);
                    if (data.lsMetas.length > 0) {
                        $('#ddlMeta').dropdowlist({
                            dataShow: 'CText',
                            dataValue: 'CValue',
                            dataselect: 'CValue',
                            datalist: data.lsMetas
                        });
                    }else {
                        alert('Usted no es responsable de ninguna meta');
                        $('#btbuscar').prop('disabled', 'false');
                    }

                    $('#ddlPeriodo').dropdowlist2({
                        dataShow: 'CText',
                        dataValue: 'CValue',
                        dataselect: 'CValue',
                        datalist: data.lsPeriodoCale
                    });

                }
            });
        }

        if ($(".jsgrid-header-cell").length > 1) {
            $("#jsGrid").jsGrid('destroy');
            $("#nEficacia").html("0");
            $("#nEfiFisica").html("0");
        }
    });

    $('#frm #btbuscar').click(function () {
        var nPlanOpeId = $('#ddlplan').val();
        var nInsMetaId = $('#ddlMeta').val();
        var url = $("#listarMatrizPogramacion").val();

        if (nPlanOpeId != "" && (nInsMetaId != "" && nInsMetaId != null)) {
            $.fn.Conexion({
                direccion: url,
                //direccion: '/Inicio/ListarMatrizPogramacion',
                bloqueo: true,
                datos: { "InstanciaId": nInsMetaId, "PlanOperativoId": nPlanOpeId, "nPeriodo": $('#ddlPeriodo').val(), "nRespId": Model.idUser },
                terminado: function (data) {
                    data = JSON.parse(data);
                    console.log(data);
                    cargarDatos(data);
                }
            });

            OptenerEficaciaMeta();
        }
    });


    $("#btnExportar").bind("click", function () {
        var cUrl,nInstancia, nPlanId, nPeriodo, cPlanText, cPeriodo;

        cUrl = $("#GetExcelMatrizProgramacion").val();
        nInstancia = encodeURIComponent($('#ddlMeta').val());
        nPlanId = encodeURIComponent($("#ddlplan").val());
        nPeriodo = encodeURIComponent($("#ddlPeriodo").val());
        cPlanText = $("#ddlMeta option:selected").html();
        cPeriodo = $("#ddlPeriodo option:selected").html();

        cPlanText = cPlanText.substring(1, cPlanText.indexOf("]"));
        cPlanText = encodeURIComponent(cPlanText);
        cPeriodo = encodeURIComponent(cPeriodo);

        window.location.href = cUrl + "?InstanciaId=" + nInstancia + "&PlanOperativoId=" + nPlanId + "&nPeriodo=" + nPeriodo + "&cPlanText=" + cPlanText + "&cPeriodo=" + cPeriodo + "&nRespId=" + Model.idUser;
    });




    function OptenerEficaciaMeta() {
        var url = $("#optenerIndicadores").val();

        $.fn.Conexion({
            direccion: url,
            //direccion: '/Inicio/OptenerEficacia',
            bloqueo: true,
            datos: { "InstanciaId": $('#ddlMeta').val(), "nPeriodo": $('#ddlPeriodo').val(), "nPlanOpeId": $('#ddlplan').val() },
            terminado: function (data) {
                data = JSON.parse(data);
                console.log(data);
                $("#nEficacia").html(data.Eficacia);
                $("#nEfiFisica").html(data.Avance);
            }
        });
    }

    function cargarDatos(data) {

        var nPeriodo = $('#ddlPeriodo').val();
        var fields = null;

        if (nPeriodo == 1) {
            fields = [
                {
                    name: "Nombre", title: "Actividad Operativa/Tarea", type: "text", width: 80,
                    itemTemplate: function (value, item) {
                        if (item.Nivel == 1) {
                            var $text = $("<p>").text(item.Nombre);
                            return $("<div class='matrizTituloPadre'>").append($text);
                        }
                        return value;
                    },
                    editing: false
                },
                { name: "UnidadDeMedida", align: "center", title: "Unidad de Medida", type: "text", width: 50, editing: false },
                { name: "Ene", align: "center", title: "Ene", type: "text", width: 20, editing: false },
                { name: "Feb", align: "center", title: "Feb", type: "text", width: 20, editing: false },
                { name: "Mar", align: "center", title: "Mar", type: "text", width: 20, editing: false }
            ];

            if ($("#ddlplan").val() == "14") {
                fields.push(
                            { name: "Abr", align: "center", title: "Abr", type: "text", width: 20, editing: false },
                            { name: "May", align: "center", title: "May", type: "text", width: 20, editing: false },
                            { name: "Jun", align: "center", title: "Jun", type: "text", width: 20, editing: false },
                            { name: "nTotal_I_S", align: "center", title: "Avance Programado",css:"negrita", type: "text", width: 40, editing: false },
                            { name: "nAvance1", align: "center", title: "Avance I Sem.", css: "blueColumn", type: "number", width: 40, editing: true }
                );
            }else{
                fields.push({ name: "nTotal_I_T", align: "center", title: "Avance Programado",css:"negrita", type: "text", width: 40, editing: false },
                            { name: "nAvance1", align: "center", title: "Avance I Trim.", css: "blueColumn", type: "number", width: 40, editing: true }
                );
            }

            fields.push(
                        { 
                            name: "nMotivoRestraso1",
                            title: "Motivo del retraso", 
                            type: "select", 
                            items: Model.lsMotivoRetraso,
                            valueField: "CValue",
                            textField: "CText",           
                            insertcss: "CText-insert",
                            editcss: "CText-edit",
                            width: 50,
                            align: "center",
                            cellRenderer: function (value, item) {
                                return $("<td>").append(item.cMotivoRestraso1);
                            },
                            editing: true 
                        },
                        {
                            name: "cLogro1", align: "",headercss: "jsgrid-align-center", title: "Logros más importantes", type: "textarea", width: 80,
                            cellRenderer: function (value, item) {
                                if (item.Nivel == 2) {
                                    return $("<td>").addClass("custom-cell");
                                }
                                return $("<td>").append(item.cLogro1);
                            },
                            editing: true
                        },
                        { type: "control", modeSwitchButton: false, deleteButton: false, width: 10 }
                    );

        } else if (nPeriodo == 2) {
            fields = [
                {
                    name: "Nombre", title: "Actividad Operativa/Tarea", type: "text", width: 80,
                    itemTemplate: function (value, item) {
                        if (item.Nivel == 1) {
                            var $text = $("<p>").text(item.Nombre);
                            return $("<div class='matrizTituloPadre'>").append($text);
                        }
                        return value;
                    },
                    editing: false
                },
                { name: "UnidadDeMedida", align: "center", title: "Unidad de Medida", type: "text", width: 50, editing: false }
            ];

            if ($("#ddlplan").val() == "14") {
                fields.push(
                        { name: "Jul", align: "center", title: "Jul", type: "text", width: 20, editing: false },
                        { name: "Ago", align: "center", title: "Ago", type: "text", width: 20, editing: false },
                        { name: "Sep", align: "center", title: "Sep", type: "text", width: 20, editing: false },
                        { name: "nTotal_III_T", align: "center", title: "Avance Programado", css: "negrita", type: "text", width: 40, editing: false },
                        { name: "nAvance2", align: "center", title: "Avance III Trim.", css: "blueColumn", type: "number", width: 40, editing: true }
                );
            }else {
                fields.push(
                        { name: "Abr", align: "center", title: "Abr", type: "text", width: 20, editing: false },
                        { name: "May", align: "center", title: "May", type: "text", width: 20, editing: false },
                        { name: "Jun", align: "center", title: "Jun", type: "text", width: 20, editing: false },
                        { name: "nTotal_II_T", align: "center", title: "Avance Programado",css:"negrita", type: "text", width: 40, editing: false },
                        { name: "nAvance2", align: "center", title: "Avance II Trim.", css: "blueColumn", type: "number", width: 40, editing: true }
                );
            }

            fields.push(
                        {
                            name: "nMotivoRestraso2", align: "center", title: "Motivo del retraso", type: "select", items: Model.lsMotivoRetraso, valueField: "CValue", textField: "CText", width: 50,
                            cellRenderer: function (value, item) {
                                return $("<td>").append(item.cMotivoRestraso2);
                            },
                            editing: true
                        },
                        {
                            name: "cLogro2", align: "",headercss: "jsgrid-align-center", title: "Logros más importantes", type: "textarea", width: 80,
                            cellRenderer: function (value, item) {
                                if (item.Nivel == 2) {
                                    return $("<td>").addClass("custom-cell");
                                }
                                return $("<td>").append(item.cLogro2);
                            },
                            editing: true
                        },
                        { type: "control", modeSwitchButton: false, deleteButton: false, width: 10 }
            );
        } else if (nPeriodo == 3) {
            fields = [
                {
                    name: "Nombre", title: "Actividad Operativa/Tarea", type: "text", width: 80,
                    itemTemplate: function (value, item) {
                        if (item.Nivel == 1) {
                            var $text = $("<p>").text(item.Nombre);
                            return $("<div class='matrizTituloPadre'>").append($text);
                        }
                        return value;
                    },
                    editing: false
                },
                { name: "UnidadDeMedida", align: "center", title: "Unidad de Medida", type: "text", width: 50, editing: false }
            ];

            if ($("#ddlplan").val() == "14") {
                fields.push(
                            { name: "Oct", align: "center", title: "Oct", type: "text", width: 20, editing: false },
                            { name: "Nov", align: "center", title: "Nov", type: "text", width: 20, editing: false },
                            { name: "Dic", align: "center", title: "Dic", type: "text", width: 20, editing: false },
                            { name: "nTotal_IV_T", align: "center", title: "Avance Programado", css: "negrita", type: "text", width: 40, editing: false },
                            { name: "nAvance3", align: "center", title: "Avance IV Trim.", css: "blueColumn", type: "number", width: 40, editing: true }
                );
            }else{
                fields.push(
                            { name: "Jul", align: "center", title: "Jul", type: "text", width: 20, editing: false },
                            { name: "Ago", align: "center", title: "Ago", type: "text", width: 20, editing: false },
                            { name: "Sep", align: "center", title: "Sep", type: "text", width: 20, editing: false },
                            { name: "nTotal_III_T", align: "center", title: "Avance Programado", css: "negrita", type: "text", width: 40, editing: false },
                            { name: "nAvance3", align: "center", title: "Avance III Trim.", css: "blueColumn", type: "number", width: 40, editing: true }
                );

            }

            fields.push(
                        {
                            name: "nMotivoRestraso3", align: "center", title: "Motivo del retraso", type: "select", items: Model.lsMotivoRetraso, valueField: "CValue", textField: "CText", width: 50,
                            cellRenderer: function (value, item) {
                                return $("<td>").append(item.cMotivoRestraso3);
                            },
                            editing: true
                        },
                        {
                            name: "cLogro3", align: "",headercss: "jsgrid-align-center", title: "Logros más importantes", type: "textarea", width: 80,
                            cellRenderer: function (value, item) {
                                if (item.Nivel == 2) {
                                    return $("<td>").addClass("custom-cell");
                                }
                                return $("<td>").append(item.cLogro3);
                            },
                            editing: true
                        },

                        { type: "control", modeSwitchButton: false, deleteButton: false, width: 10 }
                    );
        } else if (nPeriodo == 4) {

               fields = [
                {
                    name: "Nombre", title: "Actividad Operativa/Tarea", type: "text", width: 80,
                    itemTemplate: function (value, item) {
                        if (item.Nivel == 1) {
                            var $text = $("<p>").text(item.Nombre);
                            return $("<div class='matrizTituloPadre'>").append($text);
                        }
                        return value;
                    },
                    editing: false
                },
                { name: "UnidadDeMedida", align: "center", title: "Unidad de Medida", type: "text", width: 50, editing: false },
                { name: "Oct", align: "center", title: "Oct", type: "text", width: 20, editing: false },
                { name: "Nov", align: "center", title: "Nov", type: "text", width: 20, editing: false },
                { name: "Dic", align: "center", title: "Dic", type: "text", width: 20, editing: false },
                { name: "nTotal_IV_T", align: "center", title: "Avance Programado", css: "negrita", type: "text", width: 40, editing: false },
                { name: "nAvance3", align: "center", title: "Avance IV Trim.", css: "blueColumn", type: "number", width: 40, editing: true },
                {
                    name: "nMotivoRestraso4", align: "center", title: "Motivo del retraso", type: "select", items: Model.lsMotivoRetraso, valueField: "CValue", textField: "CText", width: 50,
                    cellRenderer: function (value, item) {
                        return $("<td>").append(item.cMotivoRestraso4);
                    },
                    editing: true
                },
                {
                    name: "cLogro4", align: "",headercss: "jsgrid-align-center", title: "Logros más importantes", type: "textarea", width: 80,
                    cellRenderer: function (value, item) {
                        if (item.Nivel == 2) {
                            return $("<td>").addClass("custom-cell");
                        }
                        return $("<td>").append(item.cLogro4);
                    },
                    editing: true
                },

                { type: "control", modeSwitchButton: false, deleteButton: false, width: 10 }
            ];
        }

        $("#jsGrid").jsGrid({
            height: "auto",
            width: "100%",
            autoload: true,
            editing: true,
            data: data,
            rowClass: function (item, itemIndex) {
                if (item.Nivel == 1) {
                    return "Padre";
                }
                return "";
            },
            //editRowRenderer: function (item, itemIndex) {
            //    if (args.item.Nivel == 2) {
            //        $("textarea").hide()
            //    }
            //    return true;
            //},
            editRowRenderer: function (item, itemIndex) {
                var $result = $("<tr>").addClass(this.editRowClass);

                var valor = null;
                valor = $.grep(fields, function (e) { return e.name == "cLogro1" })

                debugger;

                //if ($("#ddlplan").val() == "14") {
                //    alert("El periodo de evaluación de este Plan Operativo ha terminado. No es posible realizar más modificaciones.");

                //    cPeriodo = "1";

                //    return 0;
                //}

                //if (valor.length != 0) {
                    //alert("El periodo de evaluación del I Semestre ha terminado. No es posible realizar más modificaciones.");

                    //cPeriodo = "1";

                    //return 0;
                //}

                cPeriodo = "";

                this._eachField(function (field) {
                    var fieldValue = this._getItemFieldValue(item, field);
                    if ((field.name == "cLogro1" || field.name == "cLogro2" || field.name == "cLogro3" || field.name == "cLogro4")) {
                        if (item.Nivel == 1) {
                            field.editing = true;
                        } else {
                            field.editing = false;
                        }
                    }

                    this._prepareCell("<td>", field, "editcss")
                        .append(this.renderTemplate(field.editTemplate || "", field, { value: fieldValue, item: item }))
                        .appendTo($result);
                });


                return $result;

                if (item.Nivel == 2) {
                    $("textarea").hide();
                }
            },
            rowClick: function myfunction(args) {
                this.editItem($(args.event.target).closest("tr"));
                if (args.item.Nivel == 2) {
                    $("textarea").hide();
                }
            },
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

                var nAvance1 = args.item.nAvance1 ? args.item.nAvance1 : 0;
                var nMotivoRestraso1 = args.item.nMotivoRestraso1 ? args.item.nMotivoRestraso1 : 0;
                var cLogro1 = args.item.cLogro1 ? args.item.cLogro1 : '';

                var nAvance2 = args.item.nAvance2 ? args.item.nAvance2 : 0;
                var nMotivoRestraso2 = args.item.nMotivoRestraso2 ? args.item.nMotivoRestraso2 : 0;
                var cLogro2 = args.item.cLogro2 ? args.item.cLogro2 : '';

                var nAvance3 = args.item.nAvance3 ? args.item.nAvance3 : 0;
                var nMotivoRestraso3 = args.item.nMotivoRestraso3 ? args.item.nMotivoRestraso3 : 0;
                var cLogro3 = args.item.cLogro3 ? args.item.cLogro3 : '';

                var nAvance4 = args.item.nAvance4 ? args.item.nAvance4 : 0;
                var nMotivoRestraso4 = args.item.nMotivoRestraso4 ? args.item.nMotivoRestraso4 : 0;
                var cLogro4 = args.item.cLogro4 ? args.item.cLogro4 : '';

                var url = $("#registrarAvancePOI").val();

                $.fn.Conexion({
                    direccion: url,
                    bloqueo: false,
                    datos: {
                        "InstanciaId": InstanciaId,
                        "nAvance1": nAvance1,
                        "nMotivoRestraso1": nMotivoRestraso1,
                        "cLogro1": cLogro1,
                        "nAvance2": nAvance2,
                        "nMotivoRestraso2": nMotivoRestraso2,
                        "cLogro2": cLogro2,
                        "nAvance3": nAvance3,
                        "nMotivoRestraso3": nMotivoRestraso3,
                        "cLogro3": cLogro3,
                        "nAvance4": nAvance4,
                        "nMotivoRestraso4": nMotivoRestraso4,
                        "cLogro4": cLogro4
                    },
                    terminado: function (data) {
                        data = JSON.parse(data);
                        //alert(data.Respuesta);
                    }
                });

                $("#btbuscar").click();
            }
        });



    }



});

