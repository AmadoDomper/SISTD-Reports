$(document).ready(function () {

    nJefeId = 0;
    nUsuario = 0;
    nPeriodo = 0;

    if (window.location.hash !== '') {
        var hash = window.location.hash.replace('#eva=', '');
        var parts = hash.split('/');
        if (parts.length === 3) {
            nJefeId = parseInt(atob(atob(parts[0])), 10);
            nUsuario = parseInt(atob(atob(parts[1])), 10);
            nPeriodo = parseInt(atob(atob(parts[2])), 10);
        }
    }

    //var shouldUpdate = true;
    //var ActualizarLink = function () {
    //    if (!shouldUpdate) {
    //        // do not update the URL when the view was changed in the 'popstate' handler
    //        shouldUpdate = true;
    //        return;
    //    }

    //    var _nJefeId = $('#frm #ddlJefe').val();
    //    var _nUsuarioId = $("#ddlEquipo").val();
    //    var _nPeriodo = $("#ddlPeriodo").val();

    //    var hash = '#eva=' +
    //                _nJefeId + '/' +
    //                _nUsuarioId + '/' +
    //                _nPeriodo;
    //    var state = {
    //        nJefeId: nJefeId,
    //        nUsuarioId: nUsuario,
    //        nPeriodo: nPeriodo
    //    };
    //    window.history.pushState(state, 'eva', hash);



    //};



    function ListarJefe(_nJefeId) {
        //$('#frm #ddlJefe').change(function () {
        //var nJefeId = $('#frm #ddlJefe').val();

        var url = $("#listaJefe").val();

        //if (nJefeId != 0) {
        $.fn.Conexion({
            //direccion: '/Reporte/ListarJefe',
            //direccion: '/repsistd/Reporte/ListarJefe',
            direccion: url,
            bloqueo: false,
            datos: { "nJefeId": _nJefeId },
            terminado: function (data) {
                data = JSON.parse(data);
                if (data.length > 0) {
                    $('#frm #ddlJefe').dropdowlist2({
                        dataShow: 'CText',
                        dataValue: 'CValue',
                        dataselect: 'CValue',
                        datalist: data
                    });


                    var result = null;
                    result = $.grep(data, function (e) { return e.CValue == nJefeId })

                    if (nJefeId != 0) {
                        if (result.length != 0) {
                            $("#ddlJefe").val(nJefeId);
                        }
                    }

                    nJefeId = $("#ddlJefe").val();

                    ListarColaboradores();
                }

            }
        });
        //}
        //});
    }


    function ListarColaboradores() {

        var _nJefeId = nJefeId;
        var _nUsuId = nUsuario;

        var url = $("#listaColabora").val();

        $.fn.Conexion({
            //direccion: '/Reporte/ListarColaboradores',
            //direccion: '/repsistd/Reporte/ListarColaboradores',
            direccion: url,
            bloqueo: false,
            datos: { "nJefeId": _nJefeId, "nUsuarioId": _nUsuId },
            terminado: function (data) {
                $('#btbuscar').prop('disabled', '');
                data = JSON.parse(data);
                if (data.length > 0) {
                    $('#frm #ddlEquipo').dropdowlist2({
                        dataShow: 'CText',
                        dataValue: 'CValue',
                        dataselect: 'CValue',
                        datalist: data
                    });

                    $('#frm #ddlEquipo').dropdowlist2({
                        dataShow: 'CText',
                        dataValue: 'CValue',
                        dataselect: 'CValue',
                        datalist: data
                    });

                    var result = null;
                    result = $.grep(data, function (e) { return e.CValue == nUsuario })

                    if (nUsuario != 0) {
                        if (result.length != 0) {
                            $("#ddlEquipo").val(nUsuario);
                        } 
                    }

                    nUsuario = $("#ddlEquipo").val();

                    CargarGrilla();
                }
                else {
                    $('#btbuscar').prop('disabled', 'false');
                }

            }
        });
    }

    function CargarGrilla() {
        var nUsuarioId, nPeriodo;
        _nUsuarioId = $("#ddlEquipo").val();
        _nPeriodo = $("#ddlPeriodo").val();

        var url = $("#listaDeseInd").val();

        $.fn.Conexion({
            //direccion: '/repsistd/Reporte/ReporteDesempIndividual',
            //direccion: '/Reporte/ReporteDesempIndividual',
            direccion : url,
            bloqueo: true,
            datos: { "nUsuarioId": _nUsuarioId, "nPeriodo": _nPeriodo },
            terminado: function (data) {
                data = JSON.parse(data);
                console.log(data);
                cargarDatos(data);
                CargarDatosDesem();
                ActualizarLink();
            }
        });


    }


    function CargarDatosDesem() {
        var _nJefeId, _nColaboradorId, _nPeriodoId;
        
        _nJefeId = $("#ddlJefe").val();
        _nColaboradorId = $("#ddlEquipo").val();
        _nPeriodoId = $("#ddlPeriodo").val();

        var url = $("#datosInd").val();

        $.fn.Conexion({
            //direccion: '/repsistd/Reporte/OptenerDesemIndividualValores',
            //direccion: '/Reporte/OptenerDesemIndividualValores',
            direccion: url,
            bloqueo: false,
            datos: { "nJefeId": _nJefeId, "nColaboradorId": _nColaboradorId, "nPeriodoId": _nPeriodo },
            terminado: function (data) {
                data = JSON.parse(data);
                $("#txtEficInd").val(data.Efic_Ind + "%");
                $("#txtEficInd").removeClass("azul verde amarillo rojo").addClass(ColorEfi(data.Efic_Ind));


                $("#txtEficUO").val(data.Efic_UO + "%");
                $("#txtEficUO").removeClass("azul verde amarillo rojo").addClass(ColorEfi(data.Efic_UO));

                $("#txtEfiInst").val(data.Efic_Inst + "%");
                $("#txtEfiInst").removeClass("azul verde amarillo rojo").addClass(ColorEfi(data.Efic_Inst));

                $("#txtAvanInd").val(data.Avan_Ind + "%");
                $("#txtAvanInd").removeClass("azul verde amarillo rojo").addClass(ColorAva(data.Avan_Ind));

                $("#txtAvanUO").val(data.Avan_UO + "%");
                $("#txtAvanUO").removeClass("azul verde amarillo rojo").addClass(ColorAva(data.Avan_UO));

                $("#txtAvanInst").val(data.Avan_Inst + "%");
                $("#txtAvanInst").removeClass("azul verde amarillo rojo").addClass(ColorAva(data.Avan_Inst));
            }
        });
    }

    function ColorEfi(valor) {
        switch (true) {
            case valor >= 90:
                return 'azul';
                break;
            case valor >= 75:
                return 'verde';
                break;
            case valor >= 60:
                return 'amarillo';
                break;
            default:
                return 'rojo';
                break;
        }
    }

    function ColorAva(valor) {
        switch (true) {
            case valor >= 50:
                return 'azul';
                break;
            case valor >= 35:
                return 'verde';
                break;
            case valor >= 16:
                return 'amarillo';
                break;
            default:
                return 'rojo';
                break;
        }
    }
    function cargarDatos(data) {

        var fields = null;

        fields = [

            { name: "NumeroDeMeta", align: "center", title: "N° Meta", type: "text", width: 20, editing: false },
            { name: "Nombre", align: "", headercss: "jsgrid-align-center", title: "Meta Presupuestal", type: "text", width: 100, editing: false },
            { name: "ActiviOpe", align: "", headercss: "jsgrid-align-center", title: "Actividad Operativa/Tarea", type: "text", width: 100, editing: false },
            { name: "UniMed", align: "center", title: "Unidad de Medida", type: "text", width: 20, editing: false },
            { name: "nEsperado", align: "center", title: "Esperado", type: "text", width: 20, editing: false },
            { name: "nLogrado", align: "center", title: "Logrado", type: "text", width: 20, editing: false }
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


    $('#frm #ddlJefe').change(function () {
        nJefeId = $(this).val();
        ListarColaboradores();
    });


    $('#frm #ddlEquipo,#frm #ddlPeriodo').change(function () {
        CargarGrilla();

    });



    ListarJefe(nJefeId);



    var shouldUpdate = true;
    var ActualizarLink = function () {
        if (!shouldUpdate) {
            // do not update the URL when the view was changed in the 'popstate' handler
            shouldUpdate = true;
            return;
        }

        var _nJefeId = $('#frm #ddlJefe').val();
        var _nUsuarioId = $("#ddlEquipo").val();
        var _nPeriodo = $("#ddlPeriodo").val();

        var hash = '#eva=' +
                    btoa(btoa(_nJefeId)) + '/' +
                    btoa(btoa(_nUsuarioId)) + '/' +
                    btoa(btoa(_nPeriodo));
        var state = {
            nJefeId: btoa(btoa(nJefeId)),
            nUsuarioId: btoa(btoa(nUsuario)),
            nPeriodo: btoa(btoa(nPeriodo))
        };
        window.history.pushState(state, 'eva', hash);
    };

    window.addEventListener('popstate', function (event) {
        if (event.state === null) {
            return;
        }
        $("#ddlJefe").val(atob(atob(event.state.nJefeId)));
        $("#ddlEquipo").val(atob(atob(event.state.nUsuarioId)));
        $("#ddlPeriodo").val(atob(atob(event.state.nPeriodo)));
        CargarGrilla();

        //map.getView().setCenter(event.state.center);
        //map.getView().setZoom(event.state.zoom);
        //map.getView().setRotation(event.state.rotation);
        shouldUpdate = false;
    });



    //$('#frm #ddlJefe').change();

    $("#btnCopyUrl").click(function () {

        var aux = document.createElement("input");
        aux.setAttribute("value", window.location.href);
        document.body.appendChild(aux);
        aux.select();
        document.execCommand("copy");
        document.body.removeChild(aux);

        alert("La Url fue copiada en el portapapeles. Utilice Ctrl + V para pegarlo.");

    });



});