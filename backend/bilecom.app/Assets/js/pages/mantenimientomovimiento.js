var serieLista = [], monedaLista = [], tipoDocumentoIdentidadLista = [], detalleLista = [], detalleListaEliminados = [],tipoMovimientoLista=[];

const columnsDetalle = [
    { data: "Fila" },
    { data: "Descripcion" },
    { data: "Cantidad", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
    { data: "PrecioUnitario", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
    { data: "TotalImporte", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
    {
        data: "MovimientoDetalleId", render: function (data, type, row) {
            return `<button type="button" class="btn btn-xs btn-default btn-hover-dark ion-edit add-tooltip btnEditar" onclick='pageMantenimientoMovimiento.BtnAgregarDetalleClick(event, ${data})' data-original-title="Editar" data-container="body"></button>
                    <button type="button" class="btn btn-xs btn-danger btn-hover-danger ion-trash-a add-tooltip btnEliminar" onclick='pageMantenimientoMovimiento.BtnEliminarDetalleClick(${data})' data-original-title="Eliminar" data-container="body"></button>`
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
            } else if (tipomov == 2) {
                $("#panel-cliente").css("display", "block");
                $("#panel-proveedor").css("display", "none");
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
                        <div class="col-sm-12">
                            <div class="form-group">
                                <span class="bg-dark box-block pad-lft pad-rgt">Descripción</span>
                                <select class="form-control val-exc select2-show-accessible" name="cmb-detalle-descripcion" id="cmb-detalle-descripcion" aria-hidden="false" style="width: 100%"></select>
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
                $(e.currentTarget).attr("id", "modal-detalle-agregar");

                $("#modal-detalle-agregar").on("hide.bs.modal", function () {
                    $("#frm-movimiento-mantenimiento").data('bootstrapValidator').revalidateField("hdn-detalle");
                })

                $("#txt-detalle-cantidad, #txt-detalle-precio-unitario").change(pageMantenimientoMovimiento.CalcularImporteTotal);

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
                            return { empresaId: user.Empresa.EmpresaId, nombre };
                        },
                        processResults: function (data) {
                            let results = (data || []).map(x => Object.assign(x, { id: x.ProductoId, text: x.Nombre }));
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
                            notEmpty: {
                                message: "Debe seleccionar un cliente.",
                            }
                        }
                    },
                    "hdn-proveedor-id": {
                        validators: {
                            notEmpty: {
                                message: "Debe seleccionar un proveedor.",
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
            fetch(`${urlRoot}api/serie/listar-serie-por-tipocomprobante?tipoComprobanteId=${tipoComprobanteIdMovimiento}`),
            fetch(`${urlRoot}api/moneda/listar-moneda-por-empresa?empresaId=${user.Empresa.EmpresaId}`),
            fetch(`${urlRoot}api/tipodocumentoidentidad/listar-tipodocumentoidentidad`),
            fetch(`${urlRoot}api/tipomovimiento/listar-tipomovimiento`)
        ]

        Promise.all(promises)
            .then(r => Promise.all(r.map(common.ResponseToJson)))
            .then(([SerieLista, MonedaLista, TipoDocumentoIdentidadLista,TipoMovimientoLista]) => {
                serieLista = SerieLista || [];
                monedaLista = MonedaLista || [];
                tipoDocumentoIdentidadLista = TipoDocumentoIdentidadLista || [];
                tipoMovimientoLista = TipoMovimientoLista || [];

                pageMantenimientoMovimiento.ResponseSerieListar(serieLista);
                pageMantenimientoMovimiento.ResponseMonedaListar(monedaLista);
                pageMantenimientoMovimiento.ResponseTipoDocumentoIdentidadListar(tipoDocumentoIdentidadLista);
                pageMantenimientoMovimiento.ResponseTipoMovimientoListar(tipoMovimientoLista);
                if (typeof fnNext == "function") fnNext();
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
        let tipoMovimientoId = $("#cmb-tipo-movimiento").val();
        let serieId = $("#cmb-serie").val();
        let monedaId = $("#cmb-moneda").val();
        let clienteId = $("#hdn-cliente-id").val();
        let personalId = $("#hdn-personal-id").val();

        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;

        let ObjectoJson = {
            EmpresaId: empresaId,
            MovimientoId: movimientoId,
            TipoMovimientoId : tipoMovimientoId,
            SerieId: serieId,
            ClienteId: clienteId,
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

    ResponseObtenerDatos: function (data) {
        $("#cmb-tipo-movimiento").val(data.TipoMovimientoId);
        $("#cmb-tipo-movimiento").prop("disabled", true);
        $("#txt-fecha-hora-emision").val((new Date(data.FechaHoraEmision)).toLocaleDateString("es-PE", { year: "numeric", month: "2-digit", day: "2-digit" }));
        $("#cmb-serie").val(data.SerieId).trigger("change");
        $("#cmb-moneda").val(data.MonedaId).trigger("change");
        $("#txt-nro-movimiento").val(data.NroMovimiento.toLocaleString("es-PE", { minimumIntegerDigits: 8 }).replace(/,/g, ''));
        pageMantenimientoMovimiento.LlenarDatosCliente(data.Cliente);
        pageMantenimientoMovimiento.LlenarDatosPersonal(data.Personal);
        pageMantenimientoMovimiento.LlenarDatosProveedor(data.Proveedor);
        detalleLista = data.ListaMovimientoDetalle;
        pageMantenimientoMovimiento.ListarDetalle();
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

    }
    
}