var sedealmacenLista = [], productoLista = [];
const fechaActual = new Date();
const pageKardex = {
    Init: function () {
        common.ConfiguracionDataTable();
        //  this.Validar();
        this.CargarCombo(this.InitEvents());

        $("#cmb-producto").select2({
            allowClear: true,
            placeholder: '[Seleccione...]',
            minimumInputLength: 3,
            ajax: {
                url: `${urlRoot}api/producto/buscar-producto-por-nombre`,
                data: function (params) {
                    let user = common.ObtenerUsuario();
                    let nombre = params.term;
                    let tipoProductoId = 1;
                    return { empresaId: user.Empresa.EmpresaId, nombre, tipoProductoId };
                },
                processResults: function (data) {
                    let results = (data || []).map(x => Object.assign(x, { id: x.ProductoId, text: x.Nombre }));
                    return { results };
                }
            }
        });

        //$("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        
        $("#txt-fecha-emision-desde").datepicker({ format: "dd/mm/yyyy", autoclose: true });
        $("#txt-fecha-emision-desde").datepicker("setDate", new Date(fechaActual.getFullYear(), fechaActual.getMonth()));
        $("#txt-fecha-emision-hasta").datepicker({ format: "dd/mm/yyyy", autoclose: true });
        $("#txt-fecha-emision-hasta").datepicker("setDate", fechaActual);

        $("#btn-buscar").click(pageKardex.BtnBuscarClick);
    },
    BtnBuscarClick: function (e) {
        //e.preventDefault();

        pageKardex.CreateDataTable("#tbl-lista")
    },
    CargarCombo: async function (fn = null) {
        let user = common.ObtenerUsuario();

        let promises = [
            fetch(`${urlRoot}api/sede/listar-sedealmacen?empresaId=${user.Empresa.EmpresaId}`)]

        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([SedealmacenLista]) => {
                sedealmacenLista = SedealmacenLista;

                pageKardex.ResponseSedeAlmacen(sedealmacenLista);

                if (typeof fn == 'function') fn();
            })
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
                //url: `${urlRoot}api/personal/buscar-personal?empresaId=${empresaId}&nroDocumentoIdentidad=${pagePersonal.ObtenerNroDocumentoIdentidad()}&nombresCompletos=${pagePersonal.ObtenerNombreCompletos()}`,
                url: `${urlRoot}api/kardex/buscar-kardex`,
                data: {
                    empresaId: empresaId,
                    almacenId: () => $("#cmb-almacen").val(),
                    productoId: () => $("#cmb-producto").val(),
                    fechaInicio: () => $("#txt-fecha-emision-desde").val(),
                    fechaFinal: () => $("#txt-fecha-emision-hasta").val(),
                }
            },
            columns: [
                { data: "Fila" },
                { data: "Codigo" },
                { data: "Descripcion" },
                { data: "UnidadMedidaDescripcion" },
                { data: "StockMinimo" },
                { data: "StockActual" },
                { data: "TipoCalculo" },
                { data: "Monto" },
            ]
        })
    },
    ResponseSedeAlmacen: function (data) {
        let dataAlmacen = data.map(x => Object.assign(x, { id: x.SedeId, text: x.Nombre + ' - ' + x.Direccion }));
        $("#cmb-almacen").select2({ data: dataAlmacen, width: '100%', placeholder: '[SELECCIONE...]' });
    },
}