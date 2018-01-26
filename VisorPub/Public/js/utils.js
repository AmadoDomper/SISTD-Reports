function esDecimal(n) {
    return n.match(/^-?[0-9]+([.][0-9]*)?$/);
}


(function ($) {
    $.fn.dropdowlist = function (m) {

        m.dataShow = m.dataShow || "";
        m.dataValue = m.dataValue || "";
        m.dataselect = m.dataselect || "";
        m.datalist = m.datalist || null;
        m.contenedor = m.contenedor || this;
        m.placeholder = m.placeholder || 'Elige una opción'

        $(m.contenedor).html('');

        m.contenedor.append('<option value="">' + m.placeholder + '</option>');

        for (var i = 0; i < m.datalist.length; i++) {
            if (m.dataselect != "") {
                if (m.dataselect == eval("m.datalist[i]." + m.dataValue)) {
                    m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '" selected="true" datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                }
                else {
                    m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '"  datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                }
            }
            else {

                m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '"  datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
            }
        }
    }
})(jQuery);


(function ($) {
    $.fn.dropdowlist2 = function (m) {

        m.dataShow = m.dataShow || "";
        m.dataValue = m.dataValue || "";
        m.dataselect = m.dataselect || "";
        m.datalist = m.datalist || null;
        m.contenedor = m.contenedor || this;

        $(m.contenedor).html('');

        for (var i = 0; i < m.datalist.length; i++) {
            if (m.dataselect != "") {
                if (m.dataselect == eval("m.datalist[i]." + m.dataValue)) {
                    m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '" selected="true" datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                }
                else {
                    m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '"  datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
                }
            }
            else {

                m.contenedor.append('<option data-index="' + i + '" value="' + eval("m.datalist[i]." + m.dataValue) + '"  datashow="' + eval("m.datalist[i]." + m.dataShow) + '">' + eval("m.datalist[i]." + m.dataShow) + '</option>');
            }
        }
    }
})(jQuery);