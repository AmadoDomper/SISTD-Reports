﻿var lsFeatures = Array();
var lsVectorLayers = Array();
var indexFeature = 0;

function quitarElemento(index) {
    for (var i = 0; i < lsFeatures.length; i++) {
        if (lsFeatures[i].n == index) {
            lsFeatures.splice(i, 1);
            map.removeLayer(lsVectorLayers[i])
            lsVectorLayers.splice(i, 1);
            break;
        }
    }
}



/*Control del mapa*/
var wmsSource = new ol.source.TileWMS({
    url: 'http://10.10.10.14:8082/geoserver/visor/wms',
    params: { 'LAYERS': 'visor:prueba' },
    serverType: 'geoserver'
});

var vectorSource = new ol.source.Vector({});

var vista = new ol.View({
    projection: "EPSG:4326",
    center: [-74, -4],
    zoom: 6,
});

var map = new ol.Map({
    target: 'map',
    view: vista,

    controls: ol.control.defaults().extend([
        new ol.control.ScaleLine(),
        new ol.control.ZoomSlider(),
    ])

});

var wmsLayer1 = new ol.layer.Tile({
    type: 'base',
    title: 'Mapa Base',
    source: new ol.source.TileWMS({
        url: 'http://10.10.10.14:8082/geoserver/visor/wms',
        params: {
            LAYERS: 'visor:departamentos',
            FORMAT: 'image/png'
        },
    })
});
map.addLayer(wmsLayer1);

function map_AgregarPoligono(puntos) {

    var ring=puntos;
    var polygon = new ol.geom.Polygon([ring]);
    var feature = new ol.Feature(polygon);
    var vectorSource = new ol.source.Vector();
    vectorSource.addFeature(feature);
    var vectorLayer = new ol.layer.Vector({
        source: vectorSource
    });
    map.addLayer(vectorLayer);
    lsVectorLayers.push(vectorLayer);
}

function map_AgregarPunto(punto) {

    var point_feature = new ol.Feature({});
    var point_geom = new ol.geom.Point(punto);
    point_feature.setGeometry(point_geom);
    var vectorLayer = new ol.layer.Vector({
        source: new ol.source.Vector({
            features: [point_feature]
        })
    })
    map.addLayer(vectorLayer);
    lsVectorLayers.push(vectorLayer);
}

/*Fin control del mapa*/

$(document).ready(function () {
    

    $('#ddlTipo').dropdowlist({
        dataShow: 'tip_descripcion',
        dataValue: 'tip_idtipo',
        dataselect: 'tip_idtipo',
        datalist: Model.lsTipos
    });

    /*$('#ddlTema').dropdowlist({
        dataShow: 'tem_descripcion',
        dataValue: 'tem_idtipo',
        dataselect: 'tem_idtipo',
        datalist: Model.lsTemas
    });*/

    /*Grilla*/
    $("#jsGrid").jsGrid({
        width: "100%",
        height: "400px",
        sorting: true,
        paging: true,
        deleteConfirm: "¿Estás seguro de eliminar?",
        noDataContent: "Sin datos",
        //data: datos,

        fields: [
            {
                name: "Tipo", type: "text", width: 20, align: "center", itemTemplate: function (value) {
                    if (value == 1) {
                        return $("<div>").addClass("dv_icpunto");
                    }
                    else if (value == 2) {
                        return $("<div>").addClass("dv_iclinea");
                    }
                    else if (value == 3) {
                        return $("<div>").addClass("dv_icpoligono");
                    }
                    else {
                        return $("<div>");
                    }
                }
            },
            { name: "Info", type: "text", width: 200 },
            { type: "control", editButton: false }
        ],
        onItemDeleted: function (args) {
            quitarElemento(args.item.n);
            console.log(lsFeatures);
        }
    });
    /*Fin de la grilla*/

    /*multiselet tema*/

    function formatDatosTemas(lsTemas) {
        var datos = new Array();
        for (var i = 0; i < lsTemas.length; i++) {
            var fila = { label: lsTemas[i].tem_descripcion, title: lsTemas[i].tem_descripcion, value: lsTemas[i].tem_idtema };
            datos.push(fila);
        }
        return datos;
    }
    $('#ddlTema').multiselect({
        nonSelectedText: 'Elige un tema',
    });
    $('#ddlTema').multiselect('dataprovider', formatDatosTemas(Model.lsTemas));

    /*fin multiselect tema*/ 
    


    $('#btAddPunto').click(function () {
        $().addPunto({
            alAceptar: function (lat, lng) {
                indexFeature++;
                var info = lat + ' | ' + lng;
                var item = { "n": indexFeature, "Tipo": 1, "Info": info };
                $("#jsGrid").jsGrid("insertItem", item);
                
                lsFeatures.push(item);
                map_AgregarPunto([lat,lng]);
            }
        });
    });

   /* $('#btAddLinea').click(function () {
        $().addLinea({
            alAceptar: function (lat1, lng1, lat2, lng2) {

            }
        });
    });*/

    $('#btAddPoli').click(function () {
        $().addPoligono({
            alAceptar: function (lsLat, lsLng) {
                indexFeature++;
                var info = '';
                var puntos = [];
                for (var i = 0; i < lsLat.length; i++) {
                    info += lsLat[i] + ' | ' + lsLng[i];
                    if (i != lsLat.length - 1) {
                        info += ',';
                    }
                    puntos.push([lsLat[i], lsLng[i]]);
                }
                //el poligono debe cerrarse con su punto inicial (como un lazo)
                if (lsLat.length > 0) {
                    puntos.push([lsLat[0], lsLng[0]]);
                }
                var item = { "n": indexFeature, "Tipo": 3, "Info": info };
                $("#jsGrid").jsGrid("insertItem", item);
                lsFeatures.push(item);
                map_AgregarPoligono(puntos);
            }
        });
    });

});