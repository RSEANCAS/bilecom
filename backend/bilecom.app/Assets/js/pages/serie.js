const pageSerie = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.CargarCombo();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        $("#btn-buscar").click(pageSerie.BtnBuscarClick);
    },
    BtnBuscarClick: function (e) {
        e.preventDefault();
        pageSerie.CreateDataTable("#tbl-lista")
    },
    CargarCombo: async function () {
        let promises = [
            fetch(`${urlRoot}api/tipocomprobante/listar-tipocomprobante`)]
        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([TipoComprobanteLista]) => {

                pageSerie.ResponseTipoComprobanteListar(TipoComprobanteLista || []);

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
        $("#cmb-tipo-comprobante").select2({ data: datatipocomprobante, width: '100%', placeholder: '[TODOS...]' });
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
                    tipoComprobanteId: pageSerie.ObtenerTipoComprobante,
                    serial: pageSerie.ObtenerSerial
                }
            },
            columns: [
                { data: "SerieId" },
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

}