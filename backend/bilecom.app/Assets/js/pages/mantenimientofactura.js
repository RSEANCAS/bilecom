var serieLista = [], monedaLista = [], tipoOperacionVentaLista = [], tipoDocumentoIdentidadLista = [], tipoProductoLista = [], unidadMedidaLista = [], tipoAfectacionIgvLista = [], tipoTributoLista = [], formaPagoLista = [], formatoLista = [], detalleLista = [], detalleListaEliminados = [];
var departamentoLista = [], provinciaLista = [], distritoLista = [];

var datosclienteNuevo = {
    ClienteId: 0,
    EmpresaId: 0,
    TipoDocumentoIdentidadId: 0,
    NroDocumentoIdentidad: "",
    RazonSocial: "",
    NombreComercial: "",
    DistritoId: 0,
    Direccion: "",
    Correo: "",
    FlagActivo: 1,
    Usuario: ""
}

const columnsDetalle = [
    { data: "Fila" },
    { data: "Descripcion" },
    { data: "Cantidad", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
    { data: "PrecioUnitario", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
    { data: "ImporteTotal", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
    {
        data: "FacturaDetalleId", render: function (data, type, row) {
            return `<button type="button" class="btn btn-xs btn-default btn-hover-dark ion-edit add-tooltip btnEditar" onclick='pageMantenimientoFactura.BtnAgregarDetalleClick(event, ${data})' data-original-title="Editar" data-container="body"></button>
                    <button type="button" class="btn btn-xs btn-danger btn-hover-danger ion-trash-a add-tooltip btnEliminar" onclick='pageMantenimientoFactura.BtnEliminarDetalleClick(${data})' data-original-title="Eliminar" data-container="body"></button>`
        }
    },
];

const fechaActual = new Date();

const pageMantenimientoFactura = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.CargarCombo(() => {
            this.InitEvents();
            this.Validar();
            this.ListarDetalle();
        });
    },

    InitEvents: function () {
        $("#btn-buscar-cliente").click(pageMantenimientoFactura.BtnBuscarClienteClick);
        $("#btn-agregar-detalle").click(pageMantenimientoFactura.BtnAgregarDetalleClick);

        $("#txt-fecha-vencimiento").datepicker({ format: "dd/mm/yyyy", autoclose: true, startDate: fechaActual });

        pageMantenimientoFactura.ObtenerDatos();

        $("#btn-nuevo-cliente").click(pageMantenimientoFactura.BtnNuevoClienteClick);
    },

    BtnBuscarClienteClick: function () {
        bootbox.dialog({
            message:
                `<p class='text-semibold text-main'>Buscar Cliente</p>
                    <hr>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">N° Doc. Identidad</label>
                                <input class="form-control" name="txt-filtro-cliente-nro-documento-identidad" id="txt-filtro-cliente-nro-documento-identidad">
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Nombres y/o Razón Social</label>
                                <input class="form-control" name="txt-filtro-cliente-nombres-razonsocial" id="txt-filtro-cliente-nombres-razonsocial">
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">&nbsp;</label>
                                <button id="btn-buscar-cliente-dialog" class="btn btn-dark" type="button"><i class="ion-search"></i> Buscar</button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <table id="tbl-busqueda-cliente" class="table table-striped table-bordered small" cellspacing="0" width="100%">
                                <thead>
                                    <tr class="bg-dark">
                                        <th>N°</th>
                                        <th>Tipo Doc. Identidad</th>
                                        <th>N° Doc. Identidad</th>
                                        <th>Nombres/Razón Social</th>
                                        <th>Opción</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>`,
            onShow: function (e) {
                $(e.currentTarget).attr("id", "modal-busqueda-cliente");

                $("#modal-busqueda-cliente").on("hide.bs.modal", function () {
                    $("#frm-factura-mantenimiento").data('bootstrapValidator').revalidateField("hdn-cliente-id");
                })

                let user = common.ObtenerUsuario();
                let empresaId = user.Empresa.EmpresaId;

                let ajax = {
                    url: `${urlRoot}api/cliente/buscar-cliente`,
                    data: {
                        empresaId: empresaId,
                        nroDocumentoIdentidad: pageMantenimientoFactura.ObtenerFiltroClienteNroDocumentoIdentidad,
                        razonSocial: pageMantenimientoFactura.ObtenerFiltroClienteNombres
                    }
                };

                let columns = [
                    { data: "ClienteId" },
                    { data: "TipoDocumentoIdentidad.Descripcion" },
                    { data: "NroDocumentoIdentidad" },
                    { data: "RazonSocial" },
                    {
                        data: "ClienteId", render: function (data, type, row) {
                            return `<button class="btn btn-xs btn-default btn-hover-dark ion-checkmark-circled add-tooltip" onclick='pageMantenimientoFactura.BtnSeleccionarClienteClick(${JSON.stringify(row)})' data-original-title="Seleccionar" data-container="body"></button>`
                        }
                    },
                ];

                $("#btn-buscar-cliente-dialog").click(() => common.CreateDataTableFromAjax("#tbl-busqueda-cliente", ajax, columns));
                $("#btn-buscar-cliente-dialog").trigger("click");
            },
            animateIn: 'zoomInDown',
            animateOut: 'zoomOutUp'
        });
    },
    BtnNuevoClienteClick: function () {
        bootbox.dialog({
            title: "Nuevo Cliente",
            message:
                `<form id="frm-factura-cliente-nuevo" autocomplete="off">
                    <div class="row">
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">Tipo Doc. Identidad</label>
                                <select class="form-control val-exc select2-show-accessible" name="cmb-cliente-tipo-documento-identidad-nuevo" id="cmb-cliente-tipo-documento-identidad-nuevo" data-bv-field="cmb-cliente-tipo-documento-identidad-nuevo" tabindex="-1" aria-hidden="false" ></select>
                            </div>
                        </div>
                        <div class="col-sm-3">
                            <div class="form-group">
                                <label class="control-label">N° Doc. Identidad</label>
                                <input class="form-control" name="txt-cliente-nro-documento-identidad-nuevo" id="txt-cliente-nro-documento-identidad-nuevo">
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Nombres y/o Razón Social</label>
                                <input class="form-control" name="txt-cliente-nombres-razonsocial-nuevo" id="txt-cliente-nombres-razonsocial-nuevo">
                            </div>
                        </div>          
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">Departamento</label>
                                <select class="form-control val-exc select2-show-accessible" name="cmb-cliente-departamento-nuevo" id="cmb-cliente-departamento-nuevo" data-bv-field="cmb-cliente-departamento-nuevo" tabindex="-1" aria-hidden="false" onchange="pageMantenimientoFactura.CmbDepartamentoChange()"></select>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">Provincia</label>
                                <select class="form-control val-exc select2-show-accessible" name="cmb-cliente-provincia-nuevo" id="cmb-cliente-provincia-nuevo" data-bv-field="cmb-cliente-provincia-nuevo" tabindex="-1" aria-hidden="false" onchange="pageMantenimientoFactura.CmbProvinciaChange()"></select>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">Distrito</label>
                                <select class="form-control val-exc select2-show-accessible" name="cmb-cliente-distrito-nuevo" id="cmb-cliente-distrito-nuevo" data-bv-field="cmb-cliente-distrito-nuevo" tabindex="-1" aria-hidden="false" ></select>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <label class="control-label">Dirección</label>
                                <input class="form-control" name="txt-cliente-direccion-nuevo" id="txt-cliente-direccion-nuevo">
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">Correo</label>
                                <input class="form-control" name="txt-cliente-correo-nuevo" id="txt-cliente-correo-nuevo">
                            </div>
                        </div>
                    </div>
                    <!--<div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <button id="btn-cliente-guardar-nuevo" class="btn btn-success" type="button" onclick="pageMantenimientoFactura.BtnGuardarySeleccionar()"><i class="ion-save"></i> Guardar y Seleccionar</button>
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <button id="btn-cliente-cancelar-nuevo" class="btn btn-danger" type="button" onClick="pageMantenimientoFactura.BtnCierraNuevoCliente()"><i class="ion-save"></i> Cancelar</button>
                            </div>
                        </div>
                    </div>-->
                </form>`,
            closeButton: true,
            buttons: {
                guardar: {
                    label: 'Guardar y Seleccionar',
                    className: 'btn-success',
                    callback: function () {
                        $("#frm-factura-cliente-nuevo").trigger("submit");
                        return false;
                    },

                },
                cancelar: {
                    label: 'Cancelar',
                    classname: 'btn-danger',
                    callback: function () {
                        pageMantenimientoFactura.BtnCierraNuevoCliente();
                    }
                }
            },
            onShow: function (e) {
                $(e.currentTarget).attr("id", "modal-nuevo-cliente");

                $("#modal-nuevo-cliente").on("hide.bs.modal", function () {
                    $("#frm-factura-mantenimiento").data('bootstrapValidator').revalidateField("hdn-cliente-id");
                })

                pageMantenimientoFactura.ResponseTipoDocumentoIdentidadNuevoListar(tipoDocumentoIdentidadLista, dropdownParent = $("#modal-nuevo-cliente"));
                pageMantenimientoFactura.ResponseDepartamentoListar(departamentoLista, dropdownParent = $("#modal-nuevo-cliente"));

                let user = common.ObtenerUsuario();
                let empresaId = user.Empresa.EmpresaId;

                pageMantenimientoFactura.CmbDepartamentoChange();
                pageMantenimientoFactura.ValidarClienteNuevo();
                //$("#btn-buscar-cliente-dialog").click(() => common.CreateDataTableFromAjax("#tbl-busqueda-cliente", ajax, columns));
                //$("#btn-buscar-cliente-dialog").trigger("click");
            },
            animateIn: 'zoomInDown',
            animateOut: 'zoomOutUp'
        });
    },
    BtnSeleccionarClienteClick: function (data, modal = "#modal-busqueda-cliente") {
        pageMantenimientoFactura.LlenarDatosCliente(data);
        $(modal).modal('hide');
    },


    CmbDepartamentoChange: function () {
        let departamentoId = $("#cmb-cliente-departamento-nuevo").val();
        let ProvinciaFiltro = provinciaLista.filter(x => x.DepartamentoId == departamentoId);
        pageMantenimientoFactura.ResponseProvinciaListar(ProvinciaFiltro, dropdownParent = $("#modal-nuevo-cliente"));

        pageMantenimientoFactura.CmbProvinciaChange();
    },

    CmbProvinciaChange: function () {
        let provinciaId = $("#cmb-cliente-provincia-nuevo").val();
        let DistritoFiltro = distritoLista.filter(x => x.ProvinciaId == provinciaId);
        pageMantenimientoFactura.ResponseDistritoListar(DistritoFiltro, dropdownParent = $("#modal-nuevo-cliente"));

    },
    LlenarDatosCliente(data) {
        pageMantenimientoFactura.LimpiarDatosCliente();
        $("#hdn-cliente-id").val(data.ClienteId);
        $("#cmb-tipo-documento-identidad-cliente").val(data.TipoDocumentoIdentidadId).trigger('change');
        $("#txt-numero-documento-identidad-cliente").val(data.NroDocumentoIdentidad);
        $("#txt-nombres-completos-cliente").val(data.RazonSocial);
        $("#txt-direccion-cliente").val(data.Direccion);
    },

    LimpiarDatosCliente() {
        $("#hdn-cliente-id").val("");
        $("#cmb-tipo-documento-identidad-cliente").val("").trigger('change');
        $("#txt-numero-documento-identidad-cliente").val("");
        $("#txt-nombres-completos-cliente").val("");
        $("#txt-direccion-cliente").val("");
    },

    BtnAgregarDetalleClick: function (e, id = null) {
        bootbox.dialog({
            title: (id == null ? 'Agregar Detalle' : 'Editar Detalle') + `<div class="panel-control" style="height: fit-content">
                                <label class="small" style="display: flex; align-items: center">
                                    <input type="checkbox" id="chk-detalle-avanzado" name="chk-detalle-avanzado" style="margin-top: 0" />
                                    &nbsp;
                                    Avanzado
                                </label>
                            </div>`,
            message:
                `<form id="frm-factura-detalle" autocomplete="off">
                    ${(id == null ? "" : `<input type="hidden" id="hdn-detalle-id" value="${id}" />`)}
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Tipo Producto</span>
                                <div id="div-detalle-tipo-producto"></div>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Código</span>
                                <select class="form-control val-exc select2-show-accessible" name="cmb-detalle-codigo" id="cmb-detalle-codigo" aria-hidden="false" style="width: 100%"></select>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Código SUNAT</span>
                                <select class="form-control val-exc select2-show-accessible" name="cmb-detalle-codigo-sunat" id="cmb-detalle-codigo-sunat" aria-hidden="false" style="width: 100%" disabled></select>
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Descripción</span>
                                <select class="form-control val-exc select2-show-accessible" name="cmb-detalle-descripcion" id="cmb-detalle-descripcion" aria-hidden="false" style="width: 100%"></select>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Unidad Medida</span>
                                <select class="form-control val-exc select2-show-accessible" name="cmb-detalle-unidad-medida" id="cmb-detalle-unidad-medida" aria-hidden="false" style="width: 100%"></select>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Tipo Afectación al IGV</span>
                                <select class="form-control val-exc select2-show-accessible" name="cmb-detalle-tipo-afectacion-igv" id="cmb-detalle-tipo-afectacion-igv" aria-hidden="false" style="width: 100%"></select>
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Cantidad</span>
                                <input class="form-control" name="txt-detalle-cantidad" id="txt-detalle-cantidad" placeholder="Cantidad">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Precio Unitario</span>
                                <input class="form-control" name="txt-detalle-precio-unitario" id="txt-detalle-precio-unitario" placeholder="Precio Unitario">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Descuento</span>
                                <input class="form-control" name="txt-detalle-descuento" id="txt-detalle-descuento" placeholder="Descuento">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">IGV %</span>
                                <input class="form-control" name="txt-detalle-porcentaje-igv" id="txt-detalle-porcentaje-igv" placeholder="IGV %" readonly value="18.00">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">IGV</span>
                                <input class="form-control" name="txt-detalle-igv" id="txt-detalle-igv" placeholder="IGV" readonly>
                            </div>
                        </div>
                        <div class="col-sm-4 hidden" id="col-txt-detalle-porcentaje-isc">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">ISC %</span>
                                <input class="form-control" name="txt-detalle-porcentaje-isc" id="txt-detalle-porcentaje-isc" placeholder="ISC %">
                            </div>
                        </div>
                        <div class="col-sm-4 hidden" id="col-txt-detalle-isc">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">ISC</span>
                                <input class="form-control" name="txt-detalle-isc" id="txt-detalle-isc" placeholder="ISC">
                            </div>
                        </div>
                        <div class="col-sm-4 hidden" id="col-txt-detalle-otros-cargos">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Otros Cargos</span>
                                <input class="form-control" name="txt-detalle-otros-cargos" id="txt-detalle-otros-cargos" placeholder="Otros Cargos">
                            </div>
                        </div>
                        <div class="col-sm-4 hidden" id="col-txt-detalle-otros-tributos">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Otros Tributos</span>
                                <input class="form-control" name="txt-detalle-otros-tributos" id="txt-detalle-otros-tributos" placeholder="Otros Tributos">
                            </div>
                        </div>
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Importe Total</span>
                                <input class="form-control" name="txt-detalle-importe-total" id="txt-detalle-importe-total" placeholder="Importe Total" readonly>
                            </div>
                        </div>
                    </div>
                </form>`,
            closeButton: true,
            buttons: {
                guardar: {
                    label: 'Guardar',
                    className: 'btn-primary',
                    callback: function () {
                        $("#frm-factura-detalle").trigger("submit");
                        return false;
                    },

                }
            },
            onShow: function (e) {
                $(e.currentTarget).attr("id", "modal-detalle-agregar");

                pageMantenimientoFactura.ResponseTipoProductoListar(tipoProductoLista);
                $("input[name='rbt-detalle-tipo-producto']").change(pageMantenimientoFactura.RbtDetalleTipoProductoChange);

                $("#chk-detalle-avanzado").change(pageMantenimientoFactura.ChkDetalleAvanzado);

                $("#modal-detalle-agregar").on("hide.bs.modal", function () {
                    $("#frm-factura-mantenimiento").data('bootstrapValidator').revalidateField("hdn-detalle");
                })

                $("#txt-detalle-cantidad, #txt-detalle-precio-unitario, #txt-detalle-descuento").change(pageMantenimientoFactura.CalcularImporteTotal);

                $("#cmb-detalle-codigo").select2({
                    allowClear: true,
                    dropdownParent: $("#modal-detalle-agregar"),
                    placeholder: '[Seleccione...]',
                    minimumInputLength: 3,
                    ajax: {
                        url: `${urlRoot}api/producto/buscar-producto-por-codigo`,
                        data: function (params) {
                            let user = common.ObtenerUsuario();
                            let codigo = params.term;
                            let tipoProductoId = $("input[name='rbt-detalle-tipo-producto']:checked").val();
                            return { empresaId: user.Empresa.EmpresaId, codigo, tipoProductoId };
                        },
                        processResults: function (data) {
                            let results = (data || []).map(x => Object.assign(x, { id: x.ProductoId, text: x.Codigo }));
                            return { results };
                        }
                    }
                });

                $("#cmb-detalle-codigo").change(pageMantenimientoFactura.CmbDetalleCodigoChange);

                $("#cmb-detalle-codigo-sunat").select2({ tags: true, placeholder: 'Ingrese Código Sunat', dropdownParent: $("#modal-detalle-agregar") });

                $("#cmb-detalle-descripcion").select2({
                    allowClear: true,
                    dropdownParent: $("#modal-detalle-agregar"),
                    placeholder: '[Seleccione...]',
                    minimumInputLength: 3,
                    ajax: {
                        url: `${urlRoot}api/producto/buscar-producto-por-nombre`,
                        data: function (params) {
                            let user = common.ObtenerUsuario();
                            let nombre = params.term;
                            let tipoProductoId = $("input[name='rbt-detalle-tipo-producto']:checked").val();
                            return { empresaId: user.Empresa.EmpresaId, nombre, tipoProductoId };
                        },
                        processResults: function (data) {
                            let results = (data || []).map(x => Object.assign(x, { id: x.ProductoId, text: x.Nombre }));
                            return { results };
                        }
                    }
                });

                $("#cmb-detalle-descripcion").change(pageMantenimientoFactura.CmbDetalleDescripcionChange);

                pageMantenimientoFactura.ResponseUnidadMedidaListar(unidadMedidaLista, dropdownParent = $("#modal-detalle-agregar"));
                //$("input[name='rbt-detalle-tipo-producto']").trigger("change");

                pageMantenimientoFactura.ResponseTipoAfectacionIgvListar(tipoAfectacionIgvLista, dropdownParent = $("#modal-detalle-agregar"));

                let registroExiste = id != null;

                if (registroExiste == true) {
                    let index = detalleLista.findIndex(x => x.FacturaDetalleId == id);
                    let data = detalleLista[index];

                    let optionDefault = new Option(data.Descripcion, data.ProductoId, true, true);

                    $("#cmb-detalle-descripcion").append(optionDefault).trigger("change");
                    $("#txt-detalle-cantidad").val(data.Cantidad.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                    $("#txt-detalle-precio-unitario").val(data.PrecioUnitario.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                    $("#txt-detalle-importe-total").val(data.ImporteTotal.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                }
                pageMantenimientoFactura.ValidarDetalle();
            },
            animateIn: 'zoomInDown',
            animateOut: 'zoomOutUp'
        });
    },

    BtnEliminarDetalleClick: function (id) {
        bootbox.confirm({
            title: 'Eliminar Registro',
            message: '¿Está seguro de eliminar el registro?',
            closeButton: true,
            buttons: {
                confirm: {
                    label: 'Sí',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'No',
                    className: 'btn-default'
                }
            },
            callback: function (result) {
                if (result == true) {
                    //debugger;
                    let index = detalleLista.findIndex(x => x.FacturaDetalleId == id);
                    if (detalleLista[index].FacturaDetalleId > 0) detalleListaEliminados.push(detalleLista[index].FacturaDetalleId);
                    detalleLista.splice(index, 1);
                    pageMantenimientoFactura.ListarDetalle();
                    $("#frm-factura-mantenimiento").data('bootstrapValidator').revalidateField("hdn-detalle");
                }
            }
        })
    },

    ChkDetalleAvanzado: function () {
        let flagAvanzado = $("#chk-detalle-avanzado").prop("checked");

        if (flagAvanzado) {
            $("#col-txt-detalle-porcentaje-isc").removeClass("hidden");
            $("#col-txt-detalle-isc").removeClass("hidden");
            $("#col-txt-detalle-otros-cargos").removeClass("hidden");
            $("#col-txt-detalle-otros-tributos").removeClass("hidden");
        } else {
            $("#col-txt-detalle-porcentaje-isc").addClass("hidden");
            $("#col-txt-detalle-isc").addClass("hidden");
            $("#col-txt-detalle-otros-cargos").addClass("hidden");
            $("#col-txt-detalle-otros-tributos").addClass("hidden");
        }
    },

    CmbDetalleCodigoChange: function () {
        let data = $("#cmb-detalle-codigo").select2('data');
        if (data.length == 0) {
            $("#cmb-detalle-codigo")
            return;
        }
        data = data[0];

        //$("#cmb-detalle-descripcion").select2('data', data);
        let optionDefaultDescripcion = new Option(data.Nombre, data.ProductoId, true, true);
        $("#cmb-detalle-descripcion").append(optionDefaultDescripcion);

        pageMantenimientoFactura.CargarOtrosCamposProducto(data);
    },

    CmbDetalleDescripcionChange: function () {
        let data = $("#cmb-detalle-descripcion").select2('data');
        if (data.length == 0) {
            $("#cmb-detalle-descripcion")
            return;
        }
        data = data[0];

        //$("#cmb-detalle-codigo").select2('data', data);
        let optionDefaultCodigo = new Option(data.Codigo, data.ProductoId, true, true);
        $("#cmb-detalle-codigo").append(optionDefaultCodigo);

        pageMantenimientoFactura.CargarOtrosCamposProducto(data);
    },

    CargarOtrosCamposProducto(data) {
        let existeCodigoSunat = data.CodigoSunat != null;
        $("#cmb-detalle-codigo-sunat").prop("disabled", existeCodigoSunat);
        if (existeCodigoSunat) {
            let optionDefaultCodigoSunat = new Option(data.CodigoSunat, data.CodigoSunat, true, true);
            $("#cmb-detalle-codigo-sunat").append(optionDefaultCodigoSunat);
        }

        //pageMantenimientoFactura.ResponseUnidadMedidaListar(unidadMedidaLista, dropdownParent = $("#modal-detalle-agregar"));
        $("#cmb-detalle-unidad-medida").val(data.UnidadMedidaId).trigger("change");
        $("#cmb-detalle-tipo-afectacion-igv").val(data.TipoAfectacionIgvId).trigger("change");
    },

    RbtDetalleTipoProductoChange: function () {
        pageMantenimientoFactura.ResponseUnidadMedidaListar(unidadMedidaLista, dropdownParent = $("#modal-detalle-agregar"));
    },

    ListarDetalle: function () {
        detalleLista = detalleLista.map((x, i) => Object.assign(x, { Fila: (i + 1) }));
        common.CreateDataTableFromData("#tbl-lista-detalle", detalleLista, columnsDetalle);
        $("#hdn-detalle").val(detalleLista.length);
        pageMantenimientoFactura.MostrarTotales();
    },

    MostrarTotales: function () {
        let totalImporte = detalleLista.map(x => x.ImporteTotal).reduce((a, b) => a + b, 0);
        $("#lbl-total-importe").text(totalImporte.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
    },

    Validar: function () {
        $("#frm-factura-mantenimiento")
            .bootstrapValidator({
                excluded: [],
                fields: {
                    "hdn-cliente-id": {
                        validators: {
                            notEmpty: {
                                message: "Debe seleccionar un cliente.",
                            }
                        }
                    },
                    "hdn-personal-id": {
                        validators: {
                            notEmpty: {
                                message: "Debe seleccionar un personal.",
                            }
                        }
                    },
                    "hdn-detalle": {
                        validators: {
                            greaterThan: { value: 0, inclusive: false, message: "Debe ingresar mínimmo un detalle." }
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoFactura.EnviarFormulario();
            });
    },
    ValidarClienteNuevo: function () {
        $("#frm-factura-cliente-nuevo")
            .bootstrapValidator({
                fields: {
                    "cmb-cliente-tipo-documento-identidad-nuevo": {
                        validators: {
                            notEmpty: {message:"Debe seleccionar tipo de documento de identidad."}
                        }
                    },
                    "txt-cliente-nro-documento-identidad-nuevo": {
                        validators: {
                            notEmpty: { message: "Debe ingresar número de documento de identidad." },
                            callback: {
                                message: 'El número de documento de identidad no es válido',
                                callback: function (value, validator, $field) {
                                    let tipoDocumentoIdentidad = $("#cmb-cliente-tipo-documento-identidad-nuevo").select2('data')[0];
                                    if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiRUC) {
                                        if (value === "") {
                                            return true;
                                        }
                                        if (value.length != 11) {
                                            return {
                                                valid: false,
                                                message: "RUC inválido"
                                            }
                                        }
                                        else {
                                            if (!(/^[0-9]*$/).test(value)) {
                                                return {
                                                    valid: false,
                                                    message: "RUC inválido"
                                                }
                                            }
                                        }
                                    }
                                    else if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiCE) {
                                        if (value === "") {
                                            return true;
                                        }
                                        if (value.length != 11) {
                                            return {
                                                valid: false,
                                                message: "Carnet de Extranjería inválido"
                                            }
                                        }
                                        else {
                                            if (!(/^[0-9]*$/).test(value)) {
                                                return {
                                                    valid: false,
                                                    message: "Carnet de Extranjería inválido"
                                                }
                                            }
                                        }
                                    }
                                    else if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiPasaporte) {
                                        if (value === "") {
                                            return true;
                                        }
                                        if (value.length != 7) {
                                            return {
                                                valid: false,
                                                message: "Pasaporte inválido"
                                            }
                                        }
                                        else {
                                            if (!(/^[0-9]*$/).test(value)) {
                                                return {
                                                    valid: false,
                                                    message: "Pasaporte inválido"
                                                }
                                            }
                                        }
                                    }
                                    return true;
                                }
                            }
                        }
                    },
                    "txt-cliente-nombres-razonsocial-nuevo": {
                        validators: {
                            notEmpty: { message: "Debe de ingresar Nombres o Razón Social." },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-ñÑá-úÁ-Ú ]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
                            }
                        }
                    },
                    "cmb-cliente-departamento-nuevo": {
                        validators: {
                            notEmpty: {
                                message: "Debe seleccionar departamento",
                            }
                        }
                    },
                    "cmb-cliente-provincia-nuevo": {
                        validators: {
                            notEmpty: {
                                message: "Debe seleccionar provincia",
                            }
                        }
                    },
                    "cmb-cliente-distrito-nuevo": {
                        validators: {
                            notEmpty: {
                                message: "Debe seleccionar distrito",
                            }
                        }
                    },
                    "txt-cliente-direccion-nuevo": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar direccion",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-_ñÑ .]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
                            },
                            stringLength: {
                                min: 5,
                                message: 'Dirección no válida'
                            }
                        }
                    },
                    "txt-cliente-correo-nuevo": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar correo válido",
                            },
                            emailAddress: {
                                message: 'La dirección de correo no es válido'
                            }
                        }
                    },
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                
                pageMantenimientoFactura.BtnGuardarySeleccionar();
            });
    },
    ValidarDetalle: function () {
        $("#frm-factura-detalle")
            .bootstrapValidator({
                fields: {
                    "cmb-detalle-descripcion": {
                        validators: {
                            notEmpty: { message: "Debe seleccionar descripción." },
                        }
                    },
                    "txt-detalle-cantidad": {
                        validators: {
                            notEmpty: { message: "Debe ingresar cantidad." },
                            numeric: { message: "Debe ingresar valor numérico." },
                            greaterThan: { value: 0, inclusive: false, message: "Debe ingresar valor mayor a cero (0)." }
                        }
                    },
                    "txt-detalle-precio-unitario": {
                        validators: {
                            notEmpty: { message: "Debe ingresar precio unitario." },
                            numeric: { message: "Debe ingresar valor numérico." },
                        }
                    },
                    "txt-detalle-importe-total": {
                        validators: {
                            notEmpty: { message: "Debe ingresar importe total." },
                            numeric: { message: "Debe ingresar valor numérico." },
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoFactura.GuardarDetalle();
                $("#modal-detalle-agregar").modal('hide');
            });
    },

    GuardarDetalle: function () {
        let facturaDetalleId = pageMantenimientoFactura.ObtenerFacturaDetalleId();
        let detalleExiste = detalleLista.some(x => x.FacturaDetalleId == facturaDetalleId);
        let tipoProductoId = $("input[name='rbt-detalle-tipo-producto']:checked").val();
        let cantidad = Number($("#txt-detalle-cantidad").val().replace(/,/g, ""));
        let unidadMedidaId = $("#cmb-detalle-unidad-medida").val();
        let unidadMedida = $("#cmb-detalle-unidad-medida").select2("data")[0];
        unidadMedida = { UnidadMedidaId: unidadMedida.UnidadMedidaId, Id: unidadMedida.Id, Descripcion: unidadMedida.Descripcion };
        let productoId = parseInt($("#cmb-detalle-descripcion").val());
        let codigo = $("#cmb-detalle-codigo option:selected").text();
        let codigoSunat = $("#cmb-detalle-codigo-sunat").val();
        let descripcion = $("#cmb-detalle-descripcion option:selected").text();
        let tipoAfectacionIgvId = $("#cmb-detalle-tipo-afectacion-igv").val();
        let tipoAfectacionIgv = $("#cmb-detalle-tipo-afectacion-igv").select2("data")[0];
        tipoAfectacionIgv = { TipoTributoId: tipoAfectacionIgv.TipoTributoId, Id: tipoAfectacionIgv.Id, FlagGravado: tipoAfectacionIgv.FlagGravado, FlagExonerado: tipoAfectacionIgv.FlagExonerado, FlagInafecto: tipoAfectacionIgv.FlagInafecto, FlagExportacion: tipoAfectacionIgv.FlagExportacion, FlagGratuito: tipoAfectacionIgv.FlagGratuito, FlagVentaArrozPilado: tipoAfectacionIgv.FlagVentaArrozPilado, TipoTributo: tipoAfectacionIgv.TipoTributo };
        let tipoTributoIgvId = tipoAfectacionIgv.TipoTributoId;
        let tipoTributoIgv = { TipoTributoId: tipoAfectacionIgv.TipoTributoId, Codigo: tipoAfectacionIgv.TipoTributo.Codigo, Nombre: tipoAfectacionIgv.TipoTributo.Nombre, CodigoNombre: tipoAfectacionIgv.TipoTributo.CodigoNombre };
        let descuento = Number($("#txt-detalle-descuento").val().replace(/,/g, ""));
        let isc = Number($("#txt-detalle-isc").val().replace(/,/g, ""));
        let porcentajeIgv = tipoAfectacionIgv.FlagExonerado || tipoAfectacionIgv.FlagInafecto || (tipoAfectacionIgv.FlagGratuito && !tipoAfectacionIgv.FlagGravado) ? 0 : Number($("#txt-detalle-porcentaje-igv").val().replace(/,/g, ""));
        let igv = tipoAfectacionIgv.FlagExonerado || tipoAfectacionIgv.FlagInafecto || (tipoAfectacionIgv.FlagGratuito && !tipoAfectacionIgv.FlagGravado) ? 0 : Number($("#txt-detalle-igv").val().replace(/,/g, ""));
        let precioUnitario = Number($("#txt-detalle-precio-unitario").val().replace(/,/g, ""));
        let valorUnitario = Math.round((precioUnitario / (1 + (porcentajeIgv / 100))) * 100) / 100;
        let valorVenta = Math.round((valorUnitario * cantidad) * 100) / 100;
        let totalImporte = tipoAfectacionIgv.FlagGratuito ? 0 : Number($("#txt-detalle-importe-total").val().replace(/,/g, ""))

        let data = {
            FacturaDetalleId: facturaDetalleId,
            TipoProductoId: tipoProductoId,
            Cantidad: cantidad,
            UnidadMedidaId: unidadMedidaId,
            UnidadMedida: unidadMedida,
            ProductoId: productoId,
            CodigoSunat: codigoSunat,
            Codigo: codigo,
            Descripcion: descripcion,
            FlagAplicaICPBER: false,
            TipoAfectacionIgvId: tipoAfectacionIgvId,
            TipoAfectacionIgv: tipoAfectacionIgv,
            TipoTributoIdIGV: tipoTributoIgvId,
            TipoTributoIGV: tipoTributoIgv,
            Descuento: descuento,
            ISC: isc,
            PorcentajeIGV: porcentajeIgv,
            IGV: igv,
            ICPBER: 0,
            PorcentajeICPBER: 0,
            ValorUnitario: valorUnitario,
            PrecioUnitario: precioUnitario,
            ValorVenta: valorVenta,
            PrecioVenta: totalImporte,
            ImporteTotal: totalImporte
        }

        if (detalleExiste == true) {
            let index = detalleLista.findIndex(x => x.FacturaDetalleId == facturaDetalleId);
            detalleLista[index] = Object.assign(detalleLista[index], data);
        }
        else detalleLista.push(data);

        pageMantenimientoFactura.ListarDetalle();
    },

    ObtenerFacturaDetalleId: function () {
        let facturaDetalleId = $("#hdn-detalle-id").val();
        if (facturaDetalleId != null) return Number(facturaDetalleId);

        let listaIdNegativos = detalleLista.filter(x => x.FacturaDetalleId < 0);
        facturaDetalleId = listaIdNegativos.length == 0 ? 0 : listaIdNegativos.map(x => x.FacturaDetalleId).sort((a, b) => a - b)[0];
        facturaDetalleId--;
        return facturaDetalleId;
    },

    CalcularImporteTotal: function () {
        let cantidadString = $("#txt-detalle-cantidad").val().replace(/,/g, '');
        let precioUnitarioString = $("#txt-detalle-precio-unitario").val().replace(/,/g, '');
        let descuentoString = $("#txt-detalle-descuento").val().replace(/,/g, '');

        let tipoAfectacionIgv = $("#cmb-detalle-tipo-afectacion-igv").select2("data")[0];
        let porcentajeIgvString = tipoAfectacionIgv.FlagExonerado || tipoAfectacionIgv.FlagInafecto || (tipoAfectacionIgv.FlagGratuito && !tipoAfectacionIgv.FlagGravado) ? "0" : $("#txt-detalle-porcentaje-igv").val().replace(/,/g, "");
        let porcentajeIgv = Number(porcentajeIgvString);

        let cantidad = isNaN(Number(cantidadString)) ? 0 : Number(cantidadString);
        let precioUnitario = isNaN(Number(precioUnitarioString)) ? 0 : Number(precioUnitarioString);
        let valorUnitario = precioUnitario / (1 + (porcentajeIgv / 100));
        let valorVenta = valorUnitario * cantidad;
        let descuento = isNaN(Number(descuentoString)) ? 0 : Number(descuentoString);
        let importeTotal = cantidad * (precioUnitario - descuento);
        let igv = tipoAfectacionIgv.FlagGratuito && !tipoAfectacionIgv.FlagGravado ? 0 : importeTotal - valorVenta;
        importeTotal = tipoAfectacionIgv.FlagGratuito ? 0 : importeTotal;


        $("#txt-detalle-cantidad").val(cantidad.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $("#txt-detalle-precio-unitario").val(precioUnitario.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $("#txt-detalle-descuento").val(descuento.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $("#txt-detalle-porcentaje-igv").val(porcentajeIgv.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $("#txt-detalle-igv").val(igv.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $("#txt-detalle-importe-total").val(importeTotal.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
    },

    CargarCombo: async function (fnNext) {
        let user = common.ObtenerUsuario();
        let promises = [
            fetch(`${urlRoot}api/serie/listar-serie-por-tipocomprobante?empresaId=${user.Empresa.EmpresaId}&ambienteSunatId=${ambienteSunatId}&tipoComprobanteId=${tipoComprobanteIdFactura}`),
            fetch(`${urlRoot}api/moneda/listar-moneda-por-empresa?empresaId=${user.Empresa.EmpresaId}`),
            fetch(`${urlRoot}api/tipooperacionventa/listar-tipooperacionventa-por-empresa-tipocomprobante?empresaId=${user.Empresa.EmpresaId}&tipoComprobanteId=${tipoComprobanteIdFactura}`),
            fetch(`${urlRoot}api/tipodocumentoidentidad/listar-tipodocumentoidentidad`),
            fetch(`${urlRoot}api/tipoproducto/listar-tipoproducto-por-empresa?empresaId=${user.Empresa.EmpresaId}`),
            fetch(`${urlRoot}api/unidadmedida/listar-unidadmedida-por-empresa?empresaId=${user.Empresa.EmpresaId}`),
            fetch(`${urlRoot}api/tipoafectacionigv/listar-tipoafectacionigv-por-empresa?empresaId=${user.Empresa.EmpresaId}&withTipoTributo=true`),
            fetch(`${urlRoot}api/tipoTributo/listar-tipoTributo`),
            fetch(`${urlRoot}api/formapago/listar-formapago`),
            fetch(`${urlRoot}api/formato/listar-formato-por-tipocomprobante?tipoComprobanteId=${tipoComprobanteIdFactura}`),
            fetch(`${urlRoot}api/departamento/listar-departamento`),
            fetch(`${urlRoot}api/provincia/listar-provincia`),
            fetch(`${urlRoot}api/distrito/listar-distrito`)
        ]
        Promise.all(promises)
            .then(r => Promise.all(r.map(common.ResponseToJson)))
            .then(([SerieLista, MonedaLista, TipoOperacionVentaLista, TipoDocumentoIdentidadLista, TipoProductoLista, UnidadMedidaLista, TipoAfectacionIgvLista, TipoTributoLista, FormaPagoLista, FormatoLista, DepartamentoLista, ProvinciaLista, DistritoLista]) => {
                tipoProductoLista = TipoProductoLista || [];
                serieLista = SerieLista || [];
                monedaLista = MonedaLista || [];
                tipoOperacionVentaLista = TipoOperacionVentaLista || [];
                tipoDocumentoIdentidadLista = TipoDocumentoIdentidadLista || [];
                unidadMedidaLista = UnidadMedidaLista || [];
                tipoAfectacionIgvLista = TipoAfectacionIgvLista || [];
                tipoTributoLista = TipoTributoLista || [];
                formaPagoLista = FormaPagoLista || [];
                formatoLista = FormatoLista || [];
                departamentoLista = DepartamentoLista || [];
                provinciaLista = ProvinciaLista || [];
                distritoLista = DistritoLista || [];

                pageMantenimientoFactura.ResponseSerieListar(serieLista);
                pageMantenimientoFactura.ResponseMonedaListar(monedaLista);
                pageMantenimientoFactura.ResponseTipoOperacionVentaListar(tipoOperacionVentaLista);
                pageMantenimientoFactura.ResponseTipoDocumentoIdentidadListar(tipoDocumentoIdentidadLista);
                pageMantenimientoFactura.ResponseFormaPagoListar(formaPagoLista);
                pageMantenimientoFactura.ResponseFormatoListar(formatoLista);
                if (typeof fnNext == "function") fnNext();
            })
    },

    ObtenerDatos: function () {
        if (facturaId != null) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/factura/obtener-factura?empresaId=${empresaId}&facturaId=${facturaId}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(common.ResponseToJson)
                .then(pageMantenimientoFactura.ResponseObtenerDatos);
        }
    },

    ObtenerTipoAfectacionIgv: function (tipoAfectacionIgvId) {
        let obj = tipoAfectacionIgvLista.find(x => x.TipoAfectacionIgvId == tipoAfectacionIgvId);
        return obj;
    },

    EnviarFormulario: function () {
        let serieId = $("#cmb-serie").val();
        let serie = $("#cmb-serie").select2("data")[0];
        serie = { Serial: serie.Serial };
        let fechaVencimiento = $("#txt-fecha-vencimiento").datepicker('getDate').toISOString();
        let moneda = $("#cmb-moneda").select2("data")[0];
        moneda = { MonedaId: moneda.MonedaId, Nombre: moneda.Nombre, Simbolo: moneda.Simbolo, Codigo: moneda.Codigo };
        let monedaId = $("#cmb-moneda").val();
        let tipoOperacionVentaId = $("#cmb-tipo-operacion-venta").val();
        let tipoOperacionVenta = $("#cmb-tipo-operacion-venta").select2("data")[0];
        tipoOperacionVenta = { CodigoSunat: tipoOperacionVenta.CodigoSunat };
        let formaPagoId = $("#cmb-forma-pago").val();
        let formaPago = $("#cmb-forma-pago").select2("data")[0];
        formaPago = { FormaPagoId: formaPagoId, Descripcion: formaPago.Descripcion };
        let formatoId = $("#cmb-formato").val();
        let observacion = $("#txt-observacion").val();
        let clienteId = $("#hdn-cliente-id").val();
        let tipoDocumentoIdentidadCliente = $("#cmb-tipo-documento-identidad-cliente").select2("data")[0];
        let cliente = {
            TipoDocumentoIdentidad: { TipoDocumentoIdentidadId: tipoDocumentoIdentidadCliente.TipoDocumentoIdentidadId, Descripcion: tipoDocumentoIdentidadCliente.Descripcion, Codigo: tipoDocumentoIdentidadCliente.Codigo },
            NroDocumentoIdentidad: $("#txt-numero-documento-identidad-cliente").val(),
            RazonSocial: $("#txt-nombres-completos-cliente").val(),
            Direccion: $("#txt-direccion-cliente").val()
        };
        let totalGravado = detalleLista.filter(x => pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagGravado && !pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagGratuito).map(x => x.ValorVenta).reduce((a, b) => a + b, 0);
        let totalExonerado = detalleLista.filter(x => pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagExonerado && !pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagGratuito).map(x => x.ValorVenta).reduce((a, b) => a + b, 0);
        let totalInafecto = detalleLista.filter(x => pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagInafecto && !pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagGratuito).map(x => x.ValorVenta).reduce((a, b) => a + b, 0);
        let totalExportacion = detalleLista.filter(x => pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagExportacion && !pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagGratuito).map(x => x.ValorVenta).reduce((a, b) => a + b, 0);
        let totalGratuito = detalleLista.filter(x => pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagGratuito).map(x => x.ValorVenta).reduce((a, b) => a + b, 0);
        let totalVentaArrozPilado = detalleLista.filter(x => pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagVentaArrozPilado && !pageMantenimientoFactura.ObtenerTipoAfectacionIgv(x.TipoAfectacionIgvId).FlagGratuito).map(x => x.ValorVenta).reduce((a, b) => a + b, 0);
        let totalIGV = detalleLista.map(x => x.IGV).reduce((a, b) => a + b, 0);
        let totalISC = detalleLista.map(x => x.ISC).reduce((a, b) => a + b, 0);
        let totalOtrosCargos = 0;
        let totalOtrosTributos = 0;
        let totalBaseImponible = detalleLista.map(x => x.ValorVenta).reduce((a, b) => a + b, 0);
        let totalDescuentoDetalle = detalleLista.map(x => x.Descuento).reduce((a, b) => a + b, 0);
        let importeTotal = detalleLista.map(x => x.ImporteTotal).reduce((a, b) => a + b, 0);

        let tipoTributoExonerado = totalExonerado > 0 ? tipoTributoLista.find(x => x.TipoTributoId == tipoTributoIdExonerado) : null;
        let tipoTributoInafecto = totalInafecto > 0 ? tipoTributoLista.find(x => x.TipoTributoId == tipoTributoIdInafecto) : null;
        let tipoTributoExportacion = totalExportacion > 0 ? tipoTributoLista.find(x => x.TipoTributoId == tipoTributoIdExportacion) : null;
        let tipoTributoGratuito = totalGratuito > 0 ? tipoTributoLista.find(x => x.TipoTributoId == tipoTributoIdGratuito) : null;
        let tipoTributoIgv = totalIGV > 0 ? tipoTributoLista.find(x => x.TipoTributoId == tipoTributoIdIgv) : null;
        let tipoTributoIsc = totalISC > 0 ? tipoTributoLista.find(x => x.TipoTributoId == tipoTributoIdIsc) : null;
        let tipoTributoOth = totalOtrosTributos > 0 ? tipoTributoLista.find(x => x.TipoTributoId == tipoTributoIdOtrosConceptosPago) : null;

        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;

        let ObjectoJson = {
            EmpresaId: empresaId,
            FacturaId: facturaId,
            SerieId: serieId,
            Serie: serie,
            FechaVencimiento: fechaVencimiento,
            MonedaId: monedaId,
            Moneda: moneda,
            TipoOperacionVentaId: tipoOperacionVentaId,
            TipoOperacionVenta: tipoOperacionVenta,
            FormaPagoId: formaPagoId,
            FormaPago: formaPago,
            FormatoId: formatoId,
            Observacion: observacion,
            FlagExportacion: false,
            FlagGratuito: false,
            FlagEmisorItinerante: false,
            FlagAnticipo: false,
            FlagISC: false,
            FlagOtrosCargos: false,
            FlagOtrosTributos: false,
            ClienteId: clienteId,
            Cliente: cliente,
            TotalGravado: totalGravado,
            TotalExonerado: totalExonerado,
            TotalInafecto: totalInafecto,
            TotalExportacion: totalExportacion,
            TotalGratuito: totalGratuito,
            TotalVentaArrozPilado: totalVentaArrozPilado,
            TotalIgv: totalIGV,
            TotalIsc: totalISC,
            TotalOtrosCargos: totalOtrosCargos,
            TotalOtrosTributos: totalOtrosTributos,
            TotalBaseImponible: totalBaseImponible,
            TotaDescuentos: totalDescuentoDetalle,
            ImporteTotal: detalleLista.map(x => x.ImporteTotal).reduce((a, b) => a + b, 0),
            Observacion: observacion,
            TipoTributoIdExonerado: totalExonerado > 0 ? tipoTributoIdExonerado : null,
            TipoTributoExonerado: tipoTributoExonerado,
            TipoTributoIdInafecto: totalInafecto > 0 ? tipoTributoIdInafecto : null,
            TipoTributoInafecto: tipoTributoInafecto,
            TipoTributoIdExportacion: totalExportacion > 0 ? tipoTributoIdExportacion : null,
            TipoTributoExportacion: tipoTributoExportacion,
            TipoTributoIdGratuito: totalGratuito > 0 ? tipoTributoIdGratuito : null,
            TipoTributoGratuito: tipoTributoGratuito,
            TipoTributoIdIgv: totalIGV > 0 ? tipoTributoIdIgv : null,
            TipoTributoIgv: tipoTributoIgv,
            TipoTributoIdIsc: totalISC > 0 ? tipoTributoIdIsc : null,
            TipoTributoIsc: tipoTributoIsc,
            TipoTributoIdOtrosTributos: totalOtrosTributos > 0 ? tipoTributoIdOtrosConceptosPago: null,
            TipoTributoOtrosTributos: tipoTributoOth,
            AmbienteSunatId: ambienteSunatId,
            Usuario: user.Nombre,
            ListaFacturaDetalle: detalleLista,
            ListaFacturaDetalleEliminados: detalleListaEliminados
        }

        let url = `${urlRoot}api/factura/guardar-factura`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoFactura.ResponseEnviarFormulario);
    },

    ResponseEnviarFormulario: function (data) {
        let tipo = "", mensaje = "";
        if (data == true) {
            tipo = "success";
            mensaje = "¡Se ha guardado con éxito!";
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
                    location.href = `${urlRoot}Facturas`
                }
            }
        });
    },

    ResponseObtenerDatos: function (data) {
        $("#txt-fecha-hora-emision").val((new Date(data.FechaHoraEmision)).toLocaleDateString("es-PE", { year: "numeric", month: "2-digit", day: "2-digit" }));
        $("#cmb-serie").val(data.SerieId).trigger("change");
        $("#cmb-moneda").val(data.MonedaId).trigger("change");
        $("#txt-nro-comprobante").val(data.NroComprobante.toLocaleString("es-PE", { minimumIntegerDigits: 8 }).replace(/,/g, ''));
        pageMantenimientoFactura.LlenarDatosCliente(data.Cliente);
        pageMantenimientoFactura.LlenarDatosPersonal(data.Personal);
        detalleLista = data.ListaFacturaDetalle;
        pageMantenimientoFactura.ListarDetalle();
    },

    ResponseSerieListar: function (data) {
        let dataSerie = data.map(x => Object.assign(x, { id: x.SerieId, text: x.Serial }));
        $("#cmb-serie").select2({ data: dataSerie, width: '100%', placeholder: '[Seleccione...]' });
    },

    ResponseMonedaListar: function (data) {
        let dataMoneda = data.map(x => Object.assign(x, { id: x.MonedaId, text: x.Nombre }));
        $("#cmb-moneda").select2({ data: dataMoneda, width: '100%', placeholder: '[Seleccione...]' });
    },

    ResponseTipoOperacionVentaListar: function (data) {
        let dataTipoOperacionVenta = data.map(x => Object.assign(x, { id: x.TipoOperacionVentaId, text: x.Nombre }));
        $("#cmb-tipo-operacion-venta").select2({ data: dataTipoOperacionVenta, width: '100%', placeholder: '[Seleccione...]' });
    },

    ResponseTipoDocumentoIdentidadListar: function (data) {
        let dataTipoDocumentoIdentidad = data.map(x => Object.assign(x, { id: x.TipoDocumentoIdentidadId, text: x.Descripcion }));
        $("#cmb-tipo-documento-identidad-cliente").select2({ data: dataTipoDocumentoIdentidad, width: '100%', placeholder: '[Seleccione...]' });
        $("#cmb-tipo-documento-identidad-cliente").val("").trigger("change");
    },
    
    ResponseTipoProductoListar: function (data) {
        let dataTipoProducto = data.map((x, i) => `<label class="radio-inline"><input type="radio" ${(i == 0 ? "checked" : "")} id="rbt-detalle-tipo-producto-${x.TipoProductoId}" name="rbt-detalle-tipo-producto" value="${x.TipoProductoId}" /> ${x.Nombre}</label>`).join('');
        $("#div-detalle-tipo-producto").html(dataTipoProducto);
    },

    ResponseUnidadMedidaListar: function (data, dropdownParent = null) {
        let tipoProductoId = $("input[name='rbt-detalle-tipo-producto']:checked").val();

        let dataUnidadMedida = data.filter(x => x.TipoProductoId == tipoProductoId).map(x => Object.assign(x, { id: x.Id, text: x.Descripcion }));
        $("#cmb-detalle-unidad-medida").empty();
        $("#cmb-detalle-unidad-medida").select2({ data: dataUnidadMedida, width: '100%', placeholder: '[Seleccione...]', dropdownParent });
        $("#cmb-detalle-unidad-medida").val("").trigger("change");
    },

    ResponseTipoAfectacionIgvListar: function (data, dropdownParent = null) {
        let dataTipoAfectacionIgv = data.map(x => Object.assign(x, { id: x.TipoAfectacionIgvId, text: x.Descripcion }));
        $("#cmb-detalle-tipo-afectacion-igv").empty();
        $("#cmb-detalle-tipo-afectacion-igv").select2({ data: dataTipoAfectacionIgv, width: '100%', placeholder: '[Seleccione...]', dropdownParent });
        $("#cmb-detalle-tipo-afectacion-igv").val("").trigger("change");
    },

    ResponseFormaPagoListar: function (data) {
        let dataFormaPago = data.map(x => Object.assign(x, { id: x.FormaPagoId, text: x.Descripcion }));
        $("#cmb-forma-pago").select2({ data: dataFormaPago, width: '100%', placeholder: '[Seleccione...]' });
    },

    ResponseFormatoListar: function (data) {
        let dataFormato = data.map(x => Object.assign(x, { id: x.FormatoId, text: x.Nombre }));
        $("#cmb-formato").select2({ data: dataFormato, width: '100%', placeholder: '[Seleccione...]' });
    },

    ObtenerFiltroClienteNroDocumentoIdentidad: function () {
        return $("#txt-filtro-cliente-nro-documento-identidad").val();
    },

    ObtenerFiltroClienteNombres: function () {
        return $("#txt-filtro-cliente-nombres-razonsocial").val();

    },

    ObtenerFiltroPersonalNroDocumentoIdentidad: function () {
        return $("#txt-filtro-personal-nro-documento-identidad").val();
    },

    ObtenerFiltroPersonalNombresCompletos: function () {
        return $("#txt-filtro-personal-nombres-completos").val();

    },
    ResponseDepartamentoListar: function (data, dropdownParent = null) {
        $("#cmb-cliente-departamento-nuevo").empty();
        let datadepartamento = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.DepartamentoId, text: item.Nombre }); });
        $("#cmb-cliente-departamento-nuevo").select2({ data: datadepartamento, width: '100%', placeholder: '[SELECCIONE...]',dropdownParent});
    },

    ResponseProvinciaListar: function (data, dropdownParent = null) {
        $("#cmb-cliente-provincia-nuevo").empty();
        let dataprovincia = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.ProvinciaId, text: item.Nombre }); });
        $("#cmb-cliente-provincia-nuevo").select2({ data: dataprovincia, width: '100%', placeholder: '[SELECCIONE...]', dropdownParent});
    },

    ResponseDistritoListar: function (data, dropdownParent = null) {
        $("#cmb-cliente-distrito-nuevo").empty();
        let datadistrito = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.DistritoId, text: item.Nombre }); });
        $("#cmb-cliente-distrito-nuevo").select2({ data: datadistrito, width: '100%', placeholder: '[SELECCIONE...]', dropdownParent});
    },
    ResponseTipoDocumentoIdentidadNuevoListar: function (data, dropdownParent = null) {
        data = data.filter(x => x.TipoDocumentoIdentidadId == 2);
        let dataTipoDocumentoIdentidad = data.map(x => Object.assign(x, { id: x.TipoDocumentoIdentidadId, text: x.Descripcion }));
        $("#cmb-cliente-tipo-documento-identidad-nuevo").select2({ data: dataTipoDocumentoIdentidad, width: '100%', placeholder: '[Seleccione...]', dropdownParent });
    },
    BtnCierraNuevoCliente: function () {
        $("#modal-nuevo-cliente").modal('hide');
    },
    BtnGuardarySeleccionar: function () {
        let clienteId = 0;
        let nombres = $("#txt-cliente-nombres-razonsocial-nuevo").val();
        let nrodocumento = $("#txt-cliente-nro-documento-identidad-nuevo").val();
        let nombrecomercial = "";
        let direcion = $("#txt-cliente-direccion-nuevo").val();
        let correo = $("#txt-cliente-correo-nuevo").val();
        let tipodocumentoidentidadId = $("#cmb-cliente-tipo-documento-identidad-nuevo").val();
        let distritoId = $("#cmb-cliente-distrito-nuevo").val();

        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        pageMantenimientoFactura.datosclienteNuevo = {
            ClienteId: clienteId,
            EmpresaId: empresaId,
            TipoDocumentoIdentidadId: tipodocumentoidentidadId,
            NroDocumentoIdentidad: nrodocumento,
            RazonSocial: nombres,
            NombreComercial: nombrecomercial,
            DistritoId: distritoId,
            Direccion: direcion,
            Correo: correo,
            FlagActivo: 1,
            Usuario: user
        }
        let url = `${urlRoot}api/cliente/guardar-cliente`;
        let params = JSON.stringify(pageMantenimientoFactura.datosclienteNuevo);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoFactura.ResponseClienteNuevoGuardar);
        
    },
    ResponseClienteNuevoGuardar: function (data) {
        
        let tipo = "", mensaje = "";
        if (data > 0) {
            //$("#hdn-cliente-id").val(data);
            tipo = "success";
            mensaje = "¡Se ha guardado y seleccionado con éxito!";
            pageMantenimientoFactura.datosclienteNuevo.ClienteId = data;
            pageMantenimientoFactura.BtnSeleccionarClienteClick(pageMantenimientoFactura.datosclienteNuevo,"#modal-nuevo-cliente");
            pageMantenimientoFactura.BtnCierraNuevoCliente();
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
                //if (data == true) {
                //    location.href = `${urlRoot}Facturas`
                //}
            }
        });
    },
    LimpiarDatosClienteNuevo: function () {
        pageMantenimientoFactura.datosclienteNuevo = {
            ClienteId: 0,
            EmpresaId: 0,
            TipoDocumentoIdentidadId: 0,
            NroDocumentoIdentidad: "",
            RazonSocial: "",
            NombreComercial: "",
            DistritoId: 0,
            Direccion: "",
            Correo: "",
            FlagActivo: 1,
            Usuario: ""
        }
    }
}