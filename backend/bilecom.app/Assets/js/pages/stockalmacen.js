var sedealmacenLista = [];

const pageStockAlmacen = {
    Init: function () {
        common.ConfiguracionDataTable();
        //  this.Validar();
        this.CargarCombo(this.InitEvents());
        
        
        //$("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        //pageStockAlmacen.CargarCombo(pageMantenimientoCliente.ObtenerDatos());
        $("input[name=rd-filtro][value=1]").prop("checked", true);
        $("#btn-buscar").click(pageStockAlmacen.BtnBuscarClick);
    },
    BtnBuscarClick: function (e) {
        //e.preventDefault();

        pageStockAlmacen.CreateDataTable("#tbl-lista")
    },
    CargarCombo: async function (fn = null) {
        let user = common.ObtenerUsuario();

        let promises = [
            fetch(`${urlRoot}api/sede/listar-sedealmacen?empresaId=${user.Empresa.EmpresaId}`)]

        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([SedealmacenLista]) => {
                sedealmacenLista = SedealmacenLista;
                
                pageStockAlmacen.ResponseSedeAlmacen(sedealmacenLista);

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
                url: `${urlRoot}api/stockalmacen/buscar-stockalmacen`,
                data: {
                    empresaId: empresaId,
                    almacenId: () => $("#cmb-almacen").val(),
                    filtro: () => $('input:radio[name=rd-filtro]:checked').val()
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