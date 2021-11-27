var sedealmacenLista = [], productoLista = [],table;
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
        $('#tbl-lista tbody').on('click', 'td.details-control', function () {
            var tr = $(this).closest('tr');
            var row = table.api().row(tr);
            if (row.child.isShown()) {
                // This row is already open - close it
                row.child.hide();
                //tr.removeClass('shown');
                
            }
            else {
                // Open this row pageKardex
                //row.child(format(row.data())).show();
                //let NombreTabla = "#tbl-"+row.data().ProductoId;
                row.child(pageKardex.DevuelveDetalle(row.data())).show();
                pageKardex.CreateDataTableDetalle("#tbl-detalle", row.data());
                //tr.addClass('shown');
                
            }
        });
    },
    InitEvents: function () {
        $("#cmb-almacen, #cmb-producto").change(pageKardex.BtnBuscarClick);

        $("#txt-fecha-emision-desde, #txt-fecha-emision-hasta").datepicker({ format: "dd/mm/yyyy", autoclose: true }).on("changeDate", pageKardex.BtnBuscarClick);
        $("#txt-fecha-emision-desde").datepicker("update", new Date(fechaActual.getFullYear(), fechaActual.getMonth()));
        $("#txt-fecha-emision-hasta").datepicker("update", fechaActual);

        $("#btn-buscar").click(pageKardex.BtnBuscarClick);
    },
    BtnBuscarClick: function (e) {
        if (!(e.type == "change" && e.currentTarget.tagName.toLowerCase() == "select")) e.preventDefault();
        if (["keyup"].includes(e.type)) {
            if (e.keyCode != 13) return;
        }
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
        let desde = $("#txt-fecha-emision-desde").val();
        let hasta = $("#txt-fecha-emision-hasta").val();
        let almacenId = $("#cmb-almacen").val();
        let productoId = $("#cmb-producto").val();

        table = $(id).dataTable({
            processing: true,
            serverSide: true,
            ajax: {
                //url: `${urlRoot}api/personal/buscar-personal?empresaId=${empresaId}&nroDocumentoIdentidad=${pagePersonal.ObtenerNroDocumentoIdentidad()}&nombresCompletos=${pagePersonal.ObtenerNombreCompletos()}`,
                url: `${urlRoot}api/kardex/buscarnivel1-kardex`,
                data: {
                    empresaId: () => empresaId,
                    almacenId: () => $("#cmb-almacen").val(),
                    productoId: () => $("#cmb-producto").val(),
                    fechaInicio: pageKardex.ObtenerFechaDesde,
                    fechaFinal: pageKardex.ObtenerFechaHasta
                }
            },
            columns: [
                {
                    "className": 'details-control',
                    "orderable": false,
                    "data": null,
                    "defaultContent": ''
                },
                { data: "Fila" },
                { data: "Codigo" },
                { data: "CodigoSunat" },
                { data: "Nombre" },
                { data: "UnidadMedidaDescripcion" },
                { data: "StockActual" },
            ]
        })
    },
    CreateDataTableDetalle: function (id, item) {
        let estaInicializado = $.fn.DataTable.isDataTable(id);
        if (estaInicializado == true) {
            $(id).DataTable().ajax.reload();
            return;
        }
        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;

        table = $(id).dataTable({
            processing: true,
            serverSide: true,
            ajax: {
                //url: `${urlRoot}api/personal/buscar-personal?empresaId=${empresaId}&nroDocumentoIdentidad=${pagePersonal.ObtenerNroDocumentoIdentidad()}&nombresCompletos=${pagePersonal.ObtenerNombreCompletos()}`,
                url: `${urlRoot}api/kardex/buscarnivel2-kardex`,
                data: {
                    empresaId: empresaId,
                    almacenId: () => $("#cmb-almacen").val(),
                    productoId: item.ProductoId,
                    fechaInicio: pageKardex.ObtenerFechaDesde,
                    fechaFinal: pageKardex.ObtenerFechaHasta
                }
            },
            columns: [
                { data: "Fila" },
                { data: "TipoMovimientoDescripcion" },
                { data: "FechaHoraEmision" },
                { data: "Cantidad" },
                { data: "PrecioUnitario" },
                { data: "TotalImporte" },
            ]
        })
    },
    ResponseSedeAlmacen: function (data) {
        let dataAlmacen = data.map(x => Object.assign(x, { id: x.SedeId, text: x.Nombre + ' - ' + x.Direccion }));
        let todos = {
            id: 0,
            text: "TODOS LOS ALMACENES"
        }
        dataAlmacen.push(todos);
        dataAlmacen.sort((a, b) => a.id - b.id);
        
        $("#cmb-almacen").select2({ data: dataAlmacen, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    DevuelveDetalle: function (item) {
        return `<table id="#tbl-detalle" class="table table-striped table-bordered small" cellspacing="0" width="100%">
                    <thead>
                        <tr style="background-color:#FFFA93">
                            <th rowspan="1" colspan="1" style="width:25px">N°</th>
                            <th class="min-tablet text-center">Tipo de Movimiento</th>
                            <th class="min-tablet text-center">Fecha y Hora de Emisión</th>
                            <th class="min-tablet text-center">Cantidad</th>
                            <th class="min-tablet text-center">Precio</th>
                            <th class="min-tablet text-center">Importe Total</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>`;
    },
    ObtenerFechaHasta: function () {
        let fechaHasta = $("#txt-fecha-emision-hasta").datepicker("getDate");
        return fechaHasta.toLocaleDateString("en-US", { year: "numeric", month: "2-digit", day: "2-digit" });
    },
    ObtenerFechaDesde: function () {
        let fechaDesde = $("#txt-fecha-emision-desde").datepicker("getDate");
        return fechaDesde.toLocaleDateString("en-US", { year: "numeric", month: "2-digit", day: "2-digit" });
    },
}