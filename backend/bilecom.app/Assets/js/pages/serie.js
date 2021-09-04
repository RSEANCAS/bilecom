const pageSerie = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.CargarCombo(() => {
            this.InitEvents();
            $("#btn-buscar").trigger("click");
        });
    },

    InitEvents: function () {
        $("#cmb-tipo-comprobante").change(pageSerie.BtnBuscarClick);
        $("#txt-serial").keyup(pageSerie.BtnBuscarClick);
        $("#btn-buscar").click(pageSerie.BtnBuscarClick);
    },

    BtnBuscarClick: function (e) {
        if (!(e.type == "change" && e.currentTarget.tagName.toLowerCase() == "select")) e.preventDefault();
        if (["keyup"].includes(e.type)) {
            if (e.keyCode != 13) return;
        }
        pageSerie.CreateDataTable("#tbl-lista")
    },

    CargarCombo: async function (fn = null) {
        let promises = [
            fetch(`${urlRoot}api/tipocomprobante/listar-tipocomprobante`)]
        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([TipoComprobanteLista]) => {

                pageSerie.ResponseTipoComprobanteListar(TipoComprobanteLista || []);
                if (typeof fn == "function") fn();

            })
    },

    ObtenerTipoComprobante: function () {
        let tipoComprobante = $("#cmb-tipo-comprobante").val();
        return tipoComprobante;
    },

    ObtenerSerial: function () {
        let serial = $("#txt-serial").val();
        return serial;
    },

    ResponseTipoComprobanteListar: function (data) {
        $("#cmb-tipo-comprobante").empty();
        let datatipocomprobante = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.TipoComprobanteId, text: item.Nombre }); });
        $("#cmb-tipo-comprobante").select2({ data: datatipocomprobante, width: '100%', placeholder: '[TODOS...]', allowClear: true });
        $("#cmb-tipo-comprobante").val(null).trigger("change");
    },

    CreateDataTable: function (id) {
        let estaInicializado = $.fn.DataTable.isDataTable(id);
        if (estaInicializado == true) {
            $(id).DataTable().ajax.reload();
            return;
        }
        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;
        $(id).dataTable({
            processing: true,
            serverSide: true,
            ajax: {
                //${urlRoot}api/cliente/buscar-cliente?empresaId=${empresaId}&nroDocumentoIdentidad=${pageSerie.ObtenerNroDocumentoIdentidad()}&razonSocial=${pageSerie.ObtenerNombres()}`,
                url: `${urlRoot}api/serie/buscar-serie`,
                data: {
                    empresaId: empresaId,
                    ambienteSunatId: ambienteSunatId,
                    tipoComprobanteId: pageSerie.ObtenerTipoComprobante,
                    serial: pageSerie.ObtenerSerial
                }
            },
            columns: [
                { data: "Fila" },
                { data: "TipoComprobante.Nombre" },
                { data: "Serial" },
                { data: "ValorInicial" },
                { data: "ValorFinal" },
                { data: "ValorActual" },
                {
                    data: "SerieId", render: function (data, row) {
                        return `<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="${urlRoot}Series/Editar?Id=${data}" data-original-title="Edit" data-container="body"></a><button class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" onclick="pageSerie.btnEliminaClick(${data})" data-original-title="Delete" data-container="body" ></button >`
                    }
                },
            ]
        })

    },

    btnEliminaClick: function (id) {
        $.niftyNoty({
            icon: "danger",
            type: "danger",
            container: "floating",
            floating: {
                position: "top-center",
                animationIn: "shake",
                animationOut: "fadeOut",
            },
            closeBtn: false,
            html: `<h4 class="alert-title" align="center">¿Desea eliminar registro?</h4><div class="mar-top" align="center"><button type="button" onclick="pageSerie.EliminaRegistro(${id})" class="btn btn-primary" data-dismiss="noty">Si</button><button type="button" class="btn btn-info" data-dismiss="noty">No</button></div>`

        });
    },

    EliminaRegistro: function (id) {
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let url = `${urlRoot}api/serie/eliminar-serie?empresaId=${empresaId}&serieId=${id}&usuario=${user}`;
        let init = { method: 'POST' };

        fetch(url, init)
            .then(r => r.json())
            .then(pageSerie.ResponseEliminaRegistro);
    },

    ResponseEliminaRegistro: function (data) {
        let tipo = "", mensaje = "";
        if (data == true) {
            tipo = "success";
            mensaje = "¡Se ha eliminado con éxito!";
        } else {
            tipo = "danger";
            mensaje = "¡Se ha producido un error, vuelve a intentarlo!";
        }

        $.niftyNoty({
            type: tipo,
            container: "floating",
            html: mensaje,
            floating: {
                position: "top-center",
                animationIn: "shake",
                animationOut: "fadeOut"
            },
            focus: true,
            timer: 1800,
            onHide: function () {
                if (data == true) {
                    location.href = `${urlRoot}Series`
                }
            }
        });
    },
}