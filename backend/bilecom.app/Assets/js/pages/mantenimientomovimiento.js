var serieLista = [], sedealmacenLista = [], monedaLista = [], tipoDocumentoIdentidadLista = [], detalleLista = [],
    detalleListaEliminados = [], tipoMovimientoLista = [], tipoOperacionAlmacen = [], tipoComprobanteLista = [], tipoProductoLista = [];

const columnsDetalle = [
    { data: "Fila" },
    { data: "Descripcion" },
    { data: "Cantidad", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
    { data: "PrecioUnitario", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
    { data: "TotalImporte", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
    {
        data: "MovimientoDetalleId", render: function (data, type, row) {
            let nromov = parseInt($("#txt-nro-movimiento").val());
            if (nromov == 0) {
                return `<button type="button" class="btn btn-xs btn-default btn-hover-dark ion-edit add-tooltip btnEditar" onclick='pageMantenimientoMovimiento.BtnAgregarDetalleClick(event, ${data})' data-original-title="Editar" data-container="body"></button>
                    <button type="button" class="btn btn-xs btn-danger btn-hover-danger ion-trash-a add-tooltip btnEliminar" onclick='pageMantenimientoMovimiento.BtnEliminarDetalleClick(${data})' data-original-title="Eliminar" data-container="body"></button>`
            } else { return "" }
        }
    },
];
const pageMantenimientoMovimiento = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.CargarCombo(() => {
            this.Validar();
            this.InitEvents();
            $("#cmb-tipo-movimiento").change();
            this.ListarDetalle();
        });

        $("#cmb-tipo-movimiento").change(function () {
            let tipomov = $("#cmb-tipo-movimiento").val();
            if (tipomov == 1) {
                $("#panel-cliente").css("display", "none");
                $("#panel-proveedor").css("display", "block");
                $("#hdn-cliente-id").val("").trigger("change");
                pageMantenimientoMovimiento.LimpiarDatosCliente();

            } else if (tipomov == 2) {
                $("#panel-cliente").css("display", "block");
                $("#panel-proveedor").css("display", "none");
                $("#hdn-proveedor-id").val("").trigger("change");
                pageMantenimientoMovimiento.LimpiarDatosProveedor();
            }
        });
        //this.InitEvents();
    },

    InitEvents: function () {
        $("#btn-buscar-cliente").click(pageMantenimientoMovimiento.BtnBuscarClienteClick);
        $("#btn-buscar-proveedor").click(pageMantenimientoMovimiento.BtnBuscarProveedorClick);
        $("#btn-buscar-personal").click(pageMantenimientoMovimiento.BtnBuscarPersonalClick);
        $("#chk-obtener-datos-personal-usuario").change(pageMantenimientoMovimiento.ChkObtenerDatosPersonalUsuarioChange);
        $("#chk-obtener-datos-personal-usuario").trigger("click");
        $("#btn-agregar-detalle").click(pageMantenimientoMovimiento.BtnAgregarDetalleClick);

        pageMantenimientoMovimiento.ObtenerDatos();
        pageMantenimientoMovimiento.CmbReferenciaTipoChange();
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
                    $("#frm-movimiento-mantenimiento").data('bootstrapValidator').revalidateField("hdn-cliente-id");
                })

                let user = common.ObtenerUsuario();
                let empresaId = user.Empresa.EmpresaId;

                let ajax = {
                    url: `${urlRoot}api/cliente/buscar-cliente`,
                    data: {
                        empresaId: empresaId,
                        nroDocumentoIdentidad: pageMantenimientoMovimiento.ObtenerFiltroClienteNroDocumentoIdentidad,
                        razonSocial: pageMantenimientoMovimiento.ObtenerFiltroClienteNombres
                    }
                };

                let columns = [
                    { data: "ClienteId" },
                    { data: "TipoDocumentoIdentidad.Descripcion" },
                    { data: "NroDocumentoIdentidad" },
                    { data: "RazonSocial" },
                    {
                        data: "ClienteId", render: function (data, type, row) {
                            return `<button class="btn btn-xs btn-default btn-hover-dark ion-checkmark-circled add-tooltip" onclick='pageMantenimientoMovimiento.BtnSeleccionarClienteClick(${JSON.stringify(row)})' data-original-title="Seleccionar" data-container="body"></button>`
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


    BtnSeleccionarClienteClick: function (data) {
        pageMantenimientoMovimiento.LlenarDatosCliente(data);
        $("#modal-busqueda-cliente").modal('hide');
    },

    LlenarDatosCliente(data) {
        pageMantenimientoMovimiento.LimpiarDatosCliente();
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

    BtnBuscarProveedorClick: function () {
        bootbox.dialog({
            message:
                `<p class='text-semibold text-main'>Buscar Proveedor</p>
                    <hr>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">N° Doc. Identidad</label>
                                <input class="form-control" name="txt-filtro-proveedor-nro-documento-identidad" id="txt-filtro-proveedor-nro-documento-identidad">
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Nombres y/o Razón Social</label>
                                <input class="form-control" name="txt-filtro-proveedor-nombres-razonsocial" id="txt-filtro-proveedor-nombres-razonsocial">
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">&nbsp;</label>
                                <button id="btn-buscar-proveedor-dialog" class="btn btn-dark" type="button"><i class="ion-search"></i> Buscar</button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <table id="tbl-busqueda-proveedor" class="table table-striped table-bordered small" cellspacing="0" width="100%">
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
                $(e.currentTarget).attr("id", "modal-busqueda-proveedor");

                $("#modal-busqueda-proveedor").on("hide.bs.modal", function () {
                    $("#frm-movimiento-mantenimiento").data('bootstrapValidator').revalidateField("hdn-proveedor-id");
                })

                let user = common.ObtenerUsuario();
                let empresaId = user.Empresa.EmpresaId;

                let ajax = {
                    url: `${urlRoot}api/proveedor/buscar-proveedor`,
                    data: {
                        empresaId: empresaId,
                        nroDocumentoIdentidad: pageMantenimientoMovimiento.ObtenerFiltroProveedorNroDocumentoIdentidad,
                        razonSocial: pageMantenimientoMovimiento.ObtenerFiltroProveedorNombres
                    }
                };

                let columns = [
                    { data: "ProveedorId" },
                    { data: "TipoDocumentoIdentidad.Descripcion" },
                    { data: "NroDocumentoIdentidad" },
                    { data: "RazonSocial" },
                    {
                        data: "ProveedorId", render: function (data, type, row) {
                            return `<button class="btn btn-xs btn-default btn-hover-dark ion-checkmark-circled add-tooltip" onclick='pageMantenimientoMovimiento.BtnSeleccionarProveedorClick(${JSON.stringify(row)})' data-original-title="Seleccionar" data-container="body"></button>`
                        }
                    },
                ];

                $("#btn-buscar-proveedor-dialog").click(() => common.CreateDataTableFromAjax("#tbl-busqueda-proveedor", ajax, columns));
                $("#btn-buscar-proveedor-dialog").trigger("click");
            },
            animateIn: 'zoomInDown',
            animateOut: 'zoomOutUp'
        });
    },
    BtnSeleccionarProveedorClick: function (data) {
        pageMantenimientoMovimiento.LlenarDatosProveedor(data);
        $("#modal-busqueda-proveedor").modal('hide');
    },
    LlenarDatosProveedor(data) {
        pageMantenimientoMovimiento.LimpiarDatosProveedor();
        $("#hdn-proveedor-id").val(data.ProveedorId);
        $("#cmb-tipo-documento-identidad-proveedor").val(data.TipoDocumentoIdentidadId).trigger('change');
        $("#txt-numero-documento-identidad-proveedor").val(data.NroDocumentoIdentidad);
        $("#txt-nombres-completos-proveedor").val(data.RazonSocial);
        $("#txt-direccion-proveedor").val(data.Direccion);
    },
    LimpiarDatosProveedor() {
        $("#hdn-proveedor-id").val("");
        $("#cmb-tipo-documento-identidad-proveedor").val("").trigger('change');
        $("#txt-numero-documento-identidad-proveedor").val("");
        $("#txt-nombres-completos-proveedor").val("");
        $("#txt-direccion-proveedor").val("");
    },

    BtnBuscarPersonalClick: function () {
        bootbox.dialog({
            message:
                `<p class='text-semibold text-main'>Buscar Personal</p>
                    <hr>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <label class="control-label">N° Doc. Identidad</label>
                                <input class="form-control" name="txt-filtro-personal-nro-documento-identidad" id="txt-filtro-personal-nro-documento-identidad">
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label class="control-label">Nombres Completos</label>
                                <input class="form-control" name="txt-filtro-personal-nombres-completos" id="txt-filtro-personal-nombres-completos">
                            </div>
                        </div>
                        <div class="col-sm-2">
                            <div class="form-group">
                                <label class="control-label">&nbsp;</label>
                                <button id="btn-buscar-personal-dialog" class="btn btn-dark" type="button"><i class="ion-search"></i> Buscar</button>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <table id="tbl-busqueda-personal" class="table table-striped table-bordered small" cellspacing="0" width="100%">
                                <thead>
                                    <tr class="bg-dark">
                                        <th>N°</th>
                                        <th>Tipo Doc. Identidad</th>
                                        <th>N° Doc. Identidad</th>
                                        <th>Nombres Completos</th>
                                        <th>Opción</th>
                                    </tr>
                                </thead>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                    </div>`,
            onShow: function (e) {
                $(e.currentTarget).attr("id", "modal-busqueda-personal");
                $("#modal-busqueda-personal").on("hide.bs.modal", function () {
                    $("#frm-movimiento-mantenimiento").data('bootstrapValidator').revalidateField("hdn-personal-id");
                })
                let user = common.ObtenerUsuario();
                let empresaId = user.Empresa.EmpresaId;

                let ajax = {
                    url: `${urlRoot}api/personal/buscar-personal`,
                    data: {
                        empresaId: empresaId,
                        nroDocumentoIdentidad: pageMantenimientoMovimiento.ObtenerFiltroPersonalNroDocumentoIdentidad,
                        nombresCompletos: pageMantenimientoMovimiento.ObtenerFiltroPersonalNombresCompletos
                    }
                };

                let columns = [
                    { data: "PersonalId" },
                    { data: "TipoDocumentoIdentidad.Descripcion" },
                    { data: "NroDocumentoIdentidad" },
                    { data: "NombresCompletos" },
                    {
                        data: "PersonalId", render: function (data, type, row) {
                            return `<button class="btn btn-xs btn-default btn-hover-dark ion-checkmark-circled add-tooltip" onclick='pageMantenimientoMovimiento.BtnSeleccionarPersonalClick(${JSON.stringify(row)})' data-original-title="Seleccionar" data-container="body"></button>`
                        }
                    },
                ];

                $("#btn-buscar-personal-dialog").click(() => common.CreateDataTableFromAjax("#tbl-busqueda-personal", ajax, columns));
                $("#btn-buscar-personal-dialog").trigger("click");
            },
            animateIn: 'zoomInDown',
            animateOut: 'zoomOutUp'
        });
    },

    BtnSeleccionarPersonalClick: function (data) {
        pageMantenimientoMovimiento.LlenarDatosPersonal(data);
        $("#modal-busqueda-personal").modal('hide');
    },

    LlenarDatosPersonal(data) {
        pageMantenimientoMovimiento.LimpiarDatosPersonal();
        let user = common.ObtenerUsuario();
        let flagObtenerDatosPersonalUsuario = user.Personal.PersonalId == data.PersonalId;
        $("#chk-obtener-datos-personal-usuario").prop("checked", flagObtenerDatosPersonalUsuario);
        $("#hdn-personal-id").val(data.PersonalId);
        $("#cmb-tipo-documento-identidad-personal").val(data.TipoDocumentoIdentidadId).trigger('change');
        $("#txt-numero-documento-identidad-personal").val(data.NroDocumentoIdentidad);
        $("#txt-nombres-completos-personal").val(data.NombresCompletos);
        $("#txt-direccion-personal").val(data.Direccion);
    },

    LimpiarDatosPersonal() {
        $("#hdn-personal-id").val("");
        $("#cmb-tipo-documento-identidad-personal").val("").trigger('change');
        $("#txt-numero-documento-identidad-personal").val("");
        $("#txt-nombres-completos-personal").val("");
        $("#txt-direccion-personal").val("");
    },

    ChkObtenerDatosPersonalUsuarioChange: function () {
        let flagObtenerDatosPersonalUsuario = $("#chk-obtener-datos-personal-usuario").prop("checked");

        if (flagObtenerDatosPersonalUsuario == true) {
            let user = common.ObtenerUsuario();
            if (user.Personal == null) return;
            pageMantenimientoMovimiento.LlenarDatosPersonal(user.Personal);
        } else {
            pageMantenimientoMovimiento.LimpiarDatosPersonal();
        }

        $("#frm-movimiento-mantenimiento").data('bootstrapValidator').revalidateField("hdn-personal-id");
    },

    BtnAgregarDetalleClick: function (e, id = null) {
        
        bootbox.dialog({
            title: id == null ? 'Agregar Detalle' : 'Editar Detalle',
            message:
                `<form id="frm-Movimiento-detalle" autocomplete="off">
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
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Descripción</span>
                                <select onchange="pageMantenimientoMovimiento.changeProducto()" class="form-control val-exc select2-show-accessible" name="cmb-detalle-descripcion" id="cmb-detalle-descripcion" aria-hidden="false" style="width: 100%"></select>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-4">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Cantidad Actual</span>
                                <input class="form-control" name="txt-detalle-cantidad-actual" id="txt-detalle-cantidad-actual" placeholder="Cantidad Actual" readonly>
                            </div>
                        </div>
                        <div class="col-sm-8">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Almacen</span>
                                <input class="form-control" name="txt-detalle-almacen" id="txt-detalle-almacen" placeholder="Almacen" readonly>
                            </div>
                        </div>
                    </div>

                    <div class="row">
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
                        $("#frm-Movimiento-detalle").trigger("submit");
                        return false;
                    },

                }
            },
            onShow: function (e) {
                $("#txt-detalle-almacen").val($("#cmb-almacen").text());

                $(e.currentTarget).attr("id", "modal-detalle-agregar");

                $("#modal-detalle-agregar").on("hide.bs.modal", function () {
                    $("#frm-movimiento-mantenimiento").data('bootstrapValidator').revalidateField("hdn-detalle");
                })

                $("#txt-detalle-cantidad, #txt-detalle-precio-unitario").change(pageMantenimientoMovimiento.CalcularImporteTotal);
                pageMantenimientoMovimiento.ResponseTipoProductoListar(tipoProductoLista);
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
                            let sedeAlmacenId = $("#cmb-almacen").val();
                            return { empresaId: user.Empresa.EmpresaId, codigo, tipoProductoId, sedeAlmacenId: sedeAlmacenId };
                        },
                        processResults: function (data) {
                            let results = (data || []).map(x => Object.assign(x, { id: x.ProductoId, text: x.Codigo }));
                            return { results };
                        }
                    }
                });

                $("#cmb-detalle-codigo").change(pageMantenimientoMovimiento.CmbDetalleCodigoChange);

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
                            let sedeAlmacenId = $("#cmb-almacen").val();
                            return { empresaId: user.Empresa.EmpresaId, nombre, tipoProductoId, sedeAlmacenId: sedeAlmacenId };
                        },
                        processResults: function (data) {
                            let results = (data || []).map(x => Object.assign(x, { id: x.ProductoId, text: x.Nombre , stock:x.Stock}));
                            return { results };
                        }
                    }
                    
                });

                let registroExiste = id != null;

                if (registroExiste == true) {
                    let index = detalleLista.findIndex(x => x.MovimientoDetalleId == id);
                    let data = detalleLista[index];

                    let optionDefault = new Option(data.Descripcion, data.ProductoId, true, true);

                    $("#cmb-detalle-descripcion").append(optionDefault).trigger("change");
                    $("#txt-detalle-cantidad").val(data.Cantidad.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                    $("#txt-detalle-precio-unitario").val(data.PrecioUnitario.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                    $("#txt-detalle-importe-total").val(data.TotalImporte.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
                }
                pageMantenimientoMovimiento.ValidarDetalle();
            },
            animateIn: 'zoomInDown',
            animateOut: 'zoomOutUp'
        });
    },
    CmbTipoOperacionAlmacenChange: function () {
        pageMantenimientoMovimiento.CargaTipoOperacionAlmacen();
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
                    let index = detalleLista.findIndex(x => x.MovimientoDetalleId == id);
                    if (detalleLista[index].MovimientoDetalleId > 0) detalleListaEliminados.push(detalleLista[index].MovimientoDetalleId);
                    detalleLista.splice(index, 1);
                    pageMantenimientoMovimiento.ListarDetalle();
                    $("#frm-movimiento-mantenimiento").data('bootstrapValidator').revalidateField("hdn-detalle");
                }
            }
        })
    },

    ListarDetalle: function () {
        detalleLista = detalleLista.map((x, i) => Object.assign(x, { Fila: (i + 1) }));
        common.CreateDataTableFromData("#tbl-lista-detalle", detalleLista, columnsDetalle);
        $("#hdn-detalle").val(detalleLista.length);
        pageMantenimientoMovimiento.MostrarTotales();
    },

    MostrarTotales: function () {
        let totalImporte = detalleLista.map(x => x.TotalImporte).reduce((a, b) => a + b, 0);
        $("#lbl-total-importe").text(totalImporte.toLocaleString("en-US", { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
    },

    Validar: function () {
        $("#frm-movimiento-mantenimiento")
            .bootstrapValidator({
                excluded: [],
                fields: {
                    "hdn-cliente-id": {
                        validators: {
                            callback: {
                                message: "Debe seleccionar un cliente.",
                                callback: function (value, validator, $field) {
                                    let l = $("#cmb-tipo-movimiento").val();
                                    if (l == 2) {
                                        if ($("#hdn-cliente-id").val() == "") {
                                            return false;
                                        }
                                        else { return true; }
                                    }
                                    else { return true; }
                                    //return pageMantenimientoMovimiento.CondicionCantidad();
                                }
                            }
                        }
                    },
                    "hdn-proveedor-id": {
                        validators: {
                            callback: {
                                message: "Debe seleccionar un proveedor.",
                                callback: function (value, validator, $field) {
                                    let l = $("#cmb-tipo-movimiento").val();
                                    if (l == 1)
                                    {
                                        if ($("#hdn-proveedor-id").val() == "") {
                                            return false;
                                        }
                                        else { return true; }
                                    }
                                    else
                                    { return true; }
                                    //return pageMantenimientoMovimiento.CondicionCantidad();
                                }
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
                    },
                    "txt-referencia-serie": {
                        validators: {
                            stringLength: {
                                min: 4,
                                max: 4,
                                message:"La serie consta de 4 dígitos."
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9_]+$/,
                                message: "Solo se acepta caracteres alfanumericos."
                            },
                            callback: {
                                message: "Debe ingresar serie.",
                                callback: function (value, validator, $field) {
                                    let l = $("#cmb-referencia-tipo").val();
                                    if (l != 0) {
                                        if ($("#txt-referencia-serie").val().trim() == "") {
                                            return false;
                                        }
                                        else { return true; }
                                    }
                                    else { return true; }
                                    //return pageMantenimientoMovimiento.CondicionCantidad();
                                }
                            }
                        }
                    },
                    "txt-referencia-numero": {
                        validators: {
                            regexp: {
                                regexp: /^[0-9_]+$/,
                                message: "Solo se acepta números."
                            },
                            callback: {
                                message: "Debe de ingresar numero de referencia.",
                                callback: function (value, validator, $field) {
                                    let l = $("#cmb-referencia-tipo").val();
                                    if (l != 0) {
                                        if ($("#txt-referencia-numero").val() <= 0) {
                                            return false;
                                        }
                                        else { return true; }
                                    }
                                    else { return true; }
                                    //return pageMantenimientoMovimiento.CondicionCantidad();
                                }
                            }
                        }
                    }

                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoMovimiento.EnviarFormulario();
            });
    },

    ValidarDetalle: function () {
        $("#frm-Movimiento-detalle")
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
                            greaterThan: { value: 0, inclusive: false, message: "Debe ingresar valor mayor a cero (0)." },
                            callback: {
                                message: "La cantidad de salida no puede ser mayor a la cantidad actual.",
                                callback: function (value, validator, $field) {
                                    return pageMantenimientoMovimiento.CondicionCantidad();
                                }
                            }
                        }
                    },
                    "txt-detalle-precio-unitario": {
                        validators: {
                            notEmpty: { message: "Debe ingresar precio unitario." },
                            numeric: { message: "Debe ingresar valor numérico." },
                            greaterThan: { value: 0, inclusive: false, message: "Debe ingresar valor mayor a cero (0)." }
                        }
                    },
                    "txt-detalle-importe-total": {
                        validators: {
                            numeric: { message: "Debe ingresar valor numérico." },
                            greaterThan: { value: 0, inclusive: false, message: "Debe ingresar valor mayor a cero (0)." }
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoMovimiento.GuardarDetalle();
                $("#modal-detalle-agregar").modal('hide');
            });
    },

    GuardarDetalle: function () {
        let MovimientoDetalleId = pageMantenimientoMovimiento.ObtenerMovimientoDetalleId();
        let detalleExiste = detalleLista.some(x => x.MovimientoDetalleId == MovimientoDetalleId);
        let productoId = parseInt($("#cmb-detalle-descripcion").val());
        let descripcion = $("#cmb-detalle-descripcion option:selected").text();
        let cantidad = Number($("#txt-detalle-cantidad").val().replace(/,/g, ""));
        let precioUnitario = Number($("#txt-detalle-precio-unitario").val().replace(/,/g, ""));
        let totalImporte = Number($("#txt-detalle-importe-total").val().replace(/,/g, ""))

        let data = {
            MovimientoDetalleId: MovimientoDetalleId,
            ProductoId: productoId,
            Descripcion: descripcion,
            Cantidad: cantidad,
            PrecioUnitario: precioUnitario,
            TotalImporte: totalImporte
        }

        if (detalleExiste == true) {
            let index = detalleLista.findIndex(x => x.MovimientoDetalleId == MovimientoDetalleId);
            detalleLista[index] = Object.assign(detalleLista[index], data);
        }
        else detalleLista.push(data);

        pageMantenimientoMovimiento.ListarDetalle();
    },

    ObtenerMovimientoDetalleId: function () {
        let MovimientoDetalleId = $("#hdn-detalle-id").val();
        if (MovimientoDetalleId != null) return Number(MovimientoDetalleId);

        let listaIdNegativos = detalleLista.filter(x => x.MovimientoDetalleId < 0);
        MovimientoDetalleId = listaIdNegativos.length == 0 ? 0 : listaIdNegativos.map(x => x.MovimientoDetalleId).sort((a, b) => a - b)[0];
        MovimientoDetalleId--;
        return MovimientoDetalleId;
    },

    CalcularImporteTotal: function () {
        let cantidadString = $("#txt-detalle-cantidad").val().replace(/,/g, '')
        let precioUnitarioString = $("#txt-detalle-precio-unitario").val().replace(/,/g, '')

        let cantidad = isNaN(Number(cantidadString)) ? 0 : Number(cantidadString);
        let precioUnitario = isNaN(Number(precioUnitarioString)) ? 0 : Number(precioUnitarioString);
        let importeTotal = cantidad * precioUnitario;

        $("#txt-detalle-cantidad").val(cantidad.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $("#txt-detalle-precio-unitario").val(precioUnitario.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
        $("#txt-detalle-importe-total").val(importeTotal.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }));
    },

    CargarCombo: async function (fnNext) {
        let user = common.ObtenerUsuario();
        let promises = [
            fetch(`${urlRoot}api/sede/listar-sedealmacen?empresaId=${user.Empresa.EmpresaId}`),
            fetch(`${urlRoot}api/serie/listar-serie-por-tipocomprobante?empresaId=${user.Empresa.EmpresaId}&tipoComprobanteId=${tipoComprobanteIdMovimiento}`),
            fetch(`${urlRoot}api/tipoproducto/listar-tipoproducto-por-empresa?empresaId=${user.Empresa.EmpresaId}`),
            fetch(`${urlRoot}api/moneda/listar-moneda-por-empresa?empresaId=${user.Empresa.EmpresaId}`),
            fetch(`${urlRoot}api/tipodocumentoidentidad/listar-tipodocumentoidentidad`),
            fetch(`${urlRoot}api/tipomovimiento/listar-tipomovimiento`),
            fetch(`${urlRoot}api/tipocomprobante/listar-tipocomprobante`)
        ]

        Promise.all(promises)
            .then(r => Promise.all(r.map(common.ResponseToJson)))
            .then(([SedeAlmacenLista, SerieLista, TipoProductoLista ,MonedaLista, TipoDocumentoIdentidadLista, TipoMovimientoLista,TipoComprobanteLista]) => {
                sedealmacenLista = SedeAlmacenLista|| [];
                serieLista = SerieLista || [];
                monedaLista = MonedaLista || [];
                tipoDocumentoIdentidadLista = TipoDocumentoIdentidadLista || [];
                tipoMovimientoLista = TipoMovimientoLista || [];
                tipoComprobanteLista = TipoComprobanteLista || [];
                tipoProductoLista = TipoProductoLista || [];

                pageMantenimientoMovimiento.ResponseSedeAlmacen(sedealmacenLista);
                pageMantenimientoMovimiento.ResponseSerieListar(serieLista);
                pageMantenimientoMovimiento.ResponseMonedaListar(monedaLista);
                pageMantenimientoMovimiento.ResponseTipoDocumentoIdentidadListar(tipoDocumentoIdentidadLista);
                pageMantenimientoMovimiento.ResponseTipoMovimientoListar(tipoMovimientoLista);
                pageMantenimientoMovimiento.ResponseTipoComprobanteListar(tipoComprobanteLista);

                if (typeof fnNext == "function") fnNext();
            })
    },

    CargaTipoOperacionAlmacen: async function () {
        let promises = [
            fetch(`${urlRoot}api/tipooperacionalmacen/listar-tipooperacionalmacen`)
        ]
        Promise.all(promises)
            .then(r => Promise.all(r.map(common.ResponseToJson)))
            .then(([TipoOperacionAlmacenLista]) => {
                tipoOperacionAlmacen = TipoOperacionAlmacenLista|| [];
                pageMantenimientoMovimiento.ResponseTipoOperacionAlmacenListar(tipoOperacionAlmacen);
            })
    },

    ObtenerDatos: function () {
        if (movimientoId != null) {

            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/movimiento/obtener-movimiento?empresaId=${empresaId}&MovimientoId=${movimientoId}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(common.ResponseToJson)
                .then(pageMantenimientoMovimiento.ResponseObtenerDatos);
        }
    },

    EnviarFormulario: function () {
        $("#btn-guardar").prop("disabled", true);

        let tipoMovimientoId = $("#cmb-tipo-movimiento").val();
        let serieId = $("#cmb-serie").val();
        let monedaId = $("#cmb-moneda").val();
        let clienteId = $("#hdn-cliente-id").val();
        let proveedorId = $("#hdn-proveedor-id").val();
        let personalId = $("#hdn-personal-id").val();
        let sedeAlmacenId = $("#cmb-almacen").val();
        let tipoOperacionAlmacenId = $("#cmb-tipo-operacion-almacen").val();
        let referenciaTipo = $("#cmb-referencia-tipo").val();
        let referenciaSerie = $("#txt-referencia-serie").val();
        let referenciaNumero = $("#txt-referencia-numero").val();

        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;

        let ObjectoJson = {
            EmpresaId: empresaId,
            SedeAlmacenId : sedeAlmacenId,
            MovimientoId: movimientoId,
            TipoMovimientoId: tipoMovimientoId,
            TipoOperacionAlmacenId: tipoOperacionAlmacenId,
            ReferenciaTipo: referenciaTipo,
            ReferenciaSerie: referenciaSerie,
            ReferenciaNumero: referenciaNumero,
            SerieId: serieId,
            ClienteId: clienteId,
            ProveedorId: proveedorId,
            PersonalId: personalId,
            MonedaId: monedaId,
            TotalImporte: detalleLista.map(x => x.TotalImporte).reduce((a, b) => a + b, 0),
            Usuario: user.Nombre,
            ListaMovimientoDetalle: detalleLista,
            ListaMovimientoDetalleEliminados: detalleListaEliminados
        }

        let url = `${urlRoot}api/movimiento/guardar-movimiento`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoMovimiento.ResponseEnviarFormulario);
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
                    location.href = `${urlRoot}Movimientos`
                }
            }
        });

    },
    
    CmbReferenciaTipoChange: function () {
        let tipoDocumentoId = $("#cmb-referencia-tipo").val();

        if (tipoDocumentoId > 0) {
            $("#txt-referencia-serie").prop("readonly", false);
            $("#txt-referencia-numero").prop("readonly", false);
        } else {
            $("#txt-referencia-serie").prop("readonly", true);
            $("#txt-referencia-numero").prop("readonly", true);
            $("#txt-referencia-serie").val("");
            $("#txt-referencia-numero").val("");
        }
    },
    ResponseObtenerDatos: function (data) {
        $("#cmb-tipo-movimiento").prop("disabled", true);
        $("#cmb-serie").prop("disabled", true);
        $("#cmb-serie").prop("disabled", true);
        $("#cmb-almacen").prop("disabled", true);
        $("#txt-fecha-hora-emision").val((new Date(data.FechaHoraEmision)).toLocaleDateString("es-PE", { year: "numeric", month: "2-digit", day: "2-digit" }));
        $("#cmb-serie").val(data.SerieId).trigger("change");
        $("#cmb-moneda").val(data.MonedaId).trigger("change");
        $("#txt-nro-movimiento").val(data.NroMovimiento.toLocaleString("es-PE", { minimumIntegerDigits: 8 }).replace(/,/g, ''));
        $("#cmb-tipo-movimiento").val(data.TipoMovimientoId).trigger("change");
        if (data.Cliente != null) { pageMantenimientoMovimiento.LlenarDatosCliente(data.Cliente); }
        pageMantenimientoMovimiento.LlenarDatosPersonal(data.Personal);
        if (data.Proveedor != null) { pageMantenimientoMovimiento.LlenarDatosProveedor(data.Proveedor); }
        detalleLista = data.ListaMovimientoDetalle;
        pageMantenimientoMovimiento.ListarDetalle();
    },
    ResponseSedeAlmacen: function (data) {
        let dataAlmacen = data.map(x => Object.assign(x, { id: x.SedeId, text: x.Nombre +' - '+ x.Direccion}));
        $("#cmb-almacen").select2({ data: dataAlmacen, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    ResponseSerieListar: function (data) {
        let dataSerie = data.map(x => Object.assign(x, { id: x.SerieId, text: x.Serial }));
        $("#cmb-serie").select2({ data: dataSerie, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseMonedaListar: function (data) {
        let dataMoneda = data.map(x => Object.assign(x, { id: x.MonedaId, text: x.Nombre }));
        $("#cmb-moneda").select2({ data: dataMoneda, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    ResponseTipoMovimientoListar: function (data) {
        let dataTipoMovimiento = data.map(x => Object.assign(x, { id: x.Id, text: x.Descripcion}));
        $("#cmb-tipo-movimiento").select2({ data: dataTipoMovimiento, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    ResponseTipoDocumentoIdentidadListar: function (data) {
        let dataTipoDocumentoIdentidad = data.map(x => Object.assign(x, { id: x.TipoDocumentoIdentidadId, text: x.Descripcion }));
        $("#cmb-tipo-documento-identidad-cliente").select2({ data: dataTipoDocumentoIdentidad, width: '100%', placeholder: '[SELECCIONE...]' });
        $("#cmb-tipo-documento-identidad-cliente").val("").trigger("change");
        $("#cmb-tipo-documento-identidad-personal").select2({ data: dataTipoDocumentoIdentidad, width: '100%', placeholder: '[SELECCIONE...]' });
        $("#cmb-tipo-documento-identidad-personal").val("").trigger("change");

        $("#cmb-tipo-documento-identidad-proveedor").select2({ data: dataTipoDocumentoIdentidad, width: '100%', placeholder: '[SELECCIONE...]' });
        $("#cmb-tipo-documento-identidad-proveedor").val("").trigger("change");
    },
    ResponseTipoOperacionAlmacenListar: function (data) {
        $("#cmb-tipo-operacion-almacen").empty();

        let tipoMovimientoId = $("#cmb-tipo-movimiento").val();
        data = data.filter(x => x.TipoMovimientoId == tipoMovimientoId);
        let dataTipoOperacionAlmacen = [];
        dataTipoOperacionAlmacen = data.map(x => Object.assign(x, { id: x.Id, text: x.Descripcion }));
        $("#cmb-tipo-operacion-almacen").select2({ data: dataTipoOperacionAlmacen, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    ResponseTipoComprobanteListar: function (data) {
        let sindoc = {
                Codigo: "00",
                Nombre: "Sin Documento Referencia",
                TipoComprobanteId: 0
            }

        data.unshift(sindoc);

        data = data.filter(x => x.TipoComprobanteId == 0 || x.TipoComprobanteId == 1 || x.TipoComprobanteId == 2);
        let dataTipoComprobante = data.map(x => Object.assign(x, { id: x.TipoComprobanteId, text: x.Nombre }));
        $("#cmb-referencia-tipo").select2({ data: dataTipoComprobante, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    ObtenerFiltroClienteNroDocumentoIdentidad: function () {
        return $("#txt-filtro-cliente-nro-documento-identidad").val();
    },

    ObtenerFiltroClienteNombres: function () {
        return $("#txt-filtro-cliente-nombres-razonsocial").val();

    },
    ObtenerFiltroProveedorNroDocumentoIdentidad: function () {
        return $("#txt-filtro-proveedor-nro-documento-identidad").val();
    },
    ObtenerFiltroProveedorNombres: function () {
        return $("#txt-filtro-proveedor-nombres-razonsocial").val();

    },
    ObtenerFiltroPersonalNroDocumentoIdentidad: function () {
        return $("#txt-filtro-personal-nro-documento-identidad").val();
    },

    ObtenerFiltroPersonalNombresCompletos: function () {
        return $("#txt-filtro-personal-nombres-completos").val();

    },
    CondicionCantidad: function () {
        let l = $("#cmb-tipo-movimiento").val();
        if (l == 2) {
            if (parseFloat($("#txt-detalle-cantidad").val()) > parseFloat($("#txt-detalle-cantidad-actual").val())) {
                return false;
            }
            else {
                return true;
            }
        } else { return true;}
    },
    ResponseTipoProductoListar: function (data) {
        let dataTipoProducto = data.map((x, i) => `<label class="radio-inline"><input type="radio" ${(i == 0 ? "checked" : "disabled")} id="rbt-detalle-tipo-producto-${x.TipoProductoId}" name="rbt-detalle-tipo-producto" value="${x.TipoProductoId}" /> ${x.Nombre}</label>`).join('');
        $("#div-detalle-tipo-producto").html(dataTipoProducto);
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
        $("#txt-detalle-cantidad-actual").val(data.StockActual);
        pageMantenimientoMovimiento.CargarOtrosCamposProducto(data);
    },
    CargarOtrosCamposProducto(data) {
        let existeCodigoSunat = data.CodigoSunat != null;
        $("#cmb-detalle-codigo-sunat").prop("disabled", existeCodigoSunat);
        if (existeCodigoSunat) {
            let optionDefaultCodigoSunat = new Option(data.CodigoSunat, data.CodigoSunat, true, true);
            $("#cmb-detalle-codigo-sunat").append(optionDefaultCodigoSunat);
        }

    },
    changeProducto: function () {
        var data = $("#cmb-detalle-descripcion").select2("data");
        if (data.length == 0) {
            $("#cmb-detalle-descripcion")
            return;
        }
        data = data[0];

        let optionDefaultDescripcion = new Option(data.Nombre, data.ProductoId, true, true);

        $("#txt-detalle-cantidad-actual").val(data.StockActual);
        $("#cmb-detalle-codigo").append(optionDefaultDescripcion);
        pageMantenimientoMovimiento.CargarOtrosCamposProducto(data);
    },
}