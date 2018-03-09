

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
        var myUrl = $("#myUrl").val();

        $.fn.Conexion({
            direccion: myUrl,
            datos: { "nPeriodo": nPeriodo, "nPlanOpeId": nPlanOpeId },
            bloqueo: true,
            terminado: function (data) {
                data = JSON.parse(data);
                console.log(data);
                cargarDatos(data);
                $($("table")[1]).attr('id', 'tblDatos');
                groupTable($('#tblDatos tr:has(td)'), 0, 1); groupTable($('#tblDatos tr:has(td)'), 3, 1); $('#tblDatos .deleted').remove();
            }
        });
    }

    function cargarDatos(data) {

    var fields = null;

        fields = [
            //{ name: "CatePresu", align: "", headercss: "jsgrid-align-center", title: "CATEGORÍA PRESUPUESTAL", type: "text", width: 100, editing: false },
            {
                name: "CatePresu", align: "", headercss: "jsgrid-align-center", title: "CATEGORÍA PRESUPUESTAL", type: "text", width: 100,
                itemTemplate: function (value, item) {
                    return '<input type="hidden" value="' + item.CodigoAgrupacionProgramatica + '">' + value;
                },
                editing: false
            },
            { name: "ActProgra", align: "", headercss: "jsgrid-align-center", title: "ACTIVIDAD PROGRAMATICA", type: "text", width: 100, editing: false },

            {
                name: "APEficacia", align: "center", title: "EFICACIA PROMEDIO EN LA ACTIVIDAD PROGRAMATICA", type: "text", width: 50,
                itemTemplate: function (value, item) {
                    return value + " %";
                },
                editing: false
            },
            {
                name: "APCateEficacia", align: "center", title: "EFICACIA PROMEDIO DE LA CATEGORIA PRESUPUESTAL", type: "text", width: 50,
                itemTemplate: function (value, item) {
                    return '<input type="hidden" value="' + item.CodigoAgrupacionProgramatica + '">' + value + " %";
                },
                editing: false
            }

            //{ name: "APEficacia", align: "center", title: "EFICACIA PROMEDIO EN LA ACTIVIDAD PROGRAMATICA", type: "text", width: 50, editing: false },
            //{ name: "APCateEficacia", align: "center", title: "EFICACIA PROMEDIO DE LA CATEGORIA PRESUPUESTAL", type: "text", width: 50, editing: false }
        ];


        $("#jsGrid").jsGrid({
            height: "auto",
            width: "100%",
            autoload: true,
            editing: false,
            data: data,
            controller: {
                loadData: function () {
                    return data;
                }
            },
            fields: fields
        });
    }

    $('#frm #ddlPeriodo').change(function () {
        CargarGrilla();
    });
});

function groupTable($rows, startIndex, total) {
    if (total === 0) {
        return;
    }
    var i, currentIndex = startIndex, count = 1, lst = [];
    var tds = $rows.find('td:eq(' + currentIndex + ')');
    var ctrl = $(tds[0]);
    lst.push($rows[0]);
    for (i = 1; i <= tds.length; i++) {
        if ($(ctrl).find('input').val() == $(tds[i]).find('input').val()) {
            count++;
            $(tds[i]).addClass('deleted');
            lst.push($rows[i]);
        }
        else {
            if (count > 1) {
                ctrl.attr('rowspan', count);
                groupTable($(lst), startIndex + 1, total - 1)
            }
            count = 1;
            lst = [];
            ctrl = $(tds[i]);
            lst.push($rows[i]);
        }
    }
}