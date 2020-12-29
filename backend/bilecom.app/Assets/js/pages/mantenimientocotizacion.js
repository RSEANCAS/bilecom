var serieLista = [], monedaLista = [], tipoDocumentoIdentidadLista = [], detalleLista = [];

const columnsDetalle = [
    { data: "CotizacionDetalleId" },
    { data: "Descripcion" },
    { data: "Cantidad" },
    { data: "PrecioUnitario" },
    { data: "PrecioVenta" },
    {
        data: "CotizacionDetalleId", render: function (data, type, row) {
            return `<button class="btn btn-xs btn-default btn-hover-dark ion-checkmark-circled add-tooltip" onclick='pageMantenimientoCotizacion.BtnSeleccionarPersonalClick(${JSON.stringify(row)})' data-original-title="Seleccionar" data-container="body"></button>`
        }
    },
];
const pageMantenimientoCotizacion = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.Validar();
        this.CargarCombo(this.InitEvents);
        this.ListarDetalle();
        //this.InitEvents();
    },

    InitEvents: function () {
        pageMantenimientoCotizacion.ObtenerDatos();

        $("#btn-buscar-cliente").click(pageMantenimientoCotizacion.BtnBuscarClienteClick);
        $("#btn-buscar-personal").click(pageMantenimientoCotizacion.BtnBuscarPersonalClick);
        $("#chk-obtener-datos-personal-usuario").change(pageMantenimientoCotizacion.ChkObtenerDatosPersonalUsuarioChange);
        $("#chk-obtener-datos-personal-usuario").trigger("click");
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
                let user = common.ObtenerUsuario();
                let empresaId = user.Empresa.EmpresaId;

                let ajax = {
                    url: `${urlRoot}api/cliente/buscar-cliente`,
                    data: {
                        empresaId: empresaId,
                        nroDocumentoIdentidad: pageMantenimientoCotizacion.ObtenerFiltroClienteNroDocumentoIdentidad,
                        razonSocial: pageMantenimientoCotizacion.ObtenerFiltroClienteNombres
                    }
                };

                let columns = [
                    { data: "ClienteId" },
                    { data: "TipoDocumentoIdentidad.Descripcion" },
                    { data: "NroDocumentoIdentidad" },
                    { data: "RazonSocial" },
                    {
                        data: "ClienteId", render: function (data, type, row) {
                            return `<button class="btn btn-xs btn-default btn-hover-dark ion-checkmark-circled add-tooltip" onclick='pageMantenimientoCotizacion.BtnSeleccionarClienteClick(${JSON.stringify(row)})' data-original-title="Seleccionar" data-container="body"></button>`
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
        pageMantenimientoCotizacion.LimpiarDatosCliente();
        pageMantenimientoCotizacion.LlenarDatosCliente(data);
        $("#modal-busqueda-cliente").modal('hide');
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
                let user = common.ObtenerUsuario();
                let empresaId = user.Empresa.EmpresaId;

                let ajax = {
                    url: `${urlRoot}api/personal/buscar-personal`,
                    data: {
                        empresaId: empresaId,
                        nroDocumentoIdentidad: pageMantenimientoCotizacion.ObtenerFiltroPersonalNroDocumentoIdentidad,
                        nombresCompletos: pageMantenimientoCotizacion.ObtenerFiltroPersonalNombresCompletos
                    }
                };

                let columns = [
                    { data: "PersonalId" },
                    { data: "TipoDocumentoIdentidad.Descripcion" },
                    { data: "NroDocumentoIdentidad" },
                    { data: "NombresCompletos" },
                    {
                        data: "PersonalId", render: function (data, type, row) {
                            return `<button class="btn btn-xs btn-default btn-hover-dark ion-checkmark-circled add-tooltip" onclick='pageMantenimientoCotizacion.BtnSeleccionarPersonalClick(${JSON.stringify(row)})' data-original-title="Seleccionar" data-container="body"></button>`
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
        pageMantenimientoCotizacion.LimpiarDatosPersonal();
        pageMantenimientoCotizacion.LlenarDatosPersonal(data);
        $("#modal-busqueda-personal").modal('hide');
    },

    ChkObtenerDatosPersonalUsuarioChange: function () {
        let flagObtenerDatosPersonalUsuario = $("#chk-obtener-datos-personal-usuario").prop("checked");

        if (flagObtenerDatosPersonalUsuario == true) {
            let user = common.ObtenerUsuario();
            if (user.Personal == null) return;
            pageMantenimientoCotizacion.LlenarDatosPersonal(user.Personal);
        } else {
            pageMantenimientoCotizacion.LimpiarDatosPersonal();
        }
    },

    LlenarDatosPersonal(data) {
        let user = common.ObtenerUsuario();
        let flagObtenerDatosPersonalUsuario = user.Personal.PersonalId == data.PersonalId;
        console.log(flagObtenerDatosPersonalUsuario);
        $("#chk-obtener-datos-personal-usuario").prop("checked", flagObtenerDatosPersonalUsuario);
        //if (flagObtenerDatosPersonalUsuario == false) {
            $("#hdn-personal-id").val(data.PersonalId);
            $("#cmb-tipo-documento-identidad-personal").val(data.TipoDocumentoIdentidadId).trigger('change');
            $("#txt-numero-documento-identidad-personal").val(data.NroDocumentoIdentidad);
            $("#txt-nombres-completos-personal").val(data.NombresCompletos);
            $("#txt-direccion-personal").val(data.Direccion);
        //} else {
        //    pageMantenimientoCotizacion.ChkObtenerDatosPersonalUsuarioChange();
        //}
    },

    LimpiarDatosPersonal() {
        $("#hdn-personal-id").val("");
        $("#cmb-tipo-documento-identidad-personal").val("").trigger('change');
        $("#txt-numero-documento-identidad-personal").val("");
        $("#txt-nombres-completos-personal").val("");
        $("#txt-direccion-personal").val("");
    },

    LlenarDatosCliente(data) {
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

    ListarDetalle: function () {
        common.CreateDataTableFromData("#tbl-lista-detalle", detalleLista, columnsDetalle);
    },

    Validar: function () {
        $("#frm-cotizacion-mantenimiento")
            .bootstrapValidator({
                fields: {
                    "txt-nombre": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Categoria.",
                            }
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoCotizacion.EnviarFormulario();
            });
    },

    CargarCombo: async function (fnNext) {
        let user = common.ObtenerUsuario();
        let promises = [
            fetch(`${urlRoot}api/serie/listar-serie-por-tipocomprobante?tipoComprobanteId=${tipoComprobanteIdCotizacion}`),
            fetch(`${urlRoot}api/moneda/listar-moneda-por-empresa?empresaId=${user.Empresa.EmpresaId}`),
            fetch(`${urlRoot}api/tipodocumentoidentidad/listar-tipodocumentoidentidad`)]
        Promise.all(promises)
            .then(r => Promise.all(r.map(common.ResponseToJson)))
            .then(([SerieLista, MonedaLista, TipoDocumentoIdentidadLista]) => {
                serieLista = SerieLista;
                monedaLista = MonedaLista;
                tipoDocumentoIdentidadLista = TipoDocumentoIdentidadLista;

                pageMantenimientoCotizacion.ResponseSerieListar(SerieLista);
                pageMantenimientoCotizacion.ResponseMonedaListar(MonedaLista);
                pageMantenimientoCotizacion.ResponseTipoDocumentoIdentidadListar(TipoDocumentoIdentidadLista);
                if (typeof fnNext == "function") fnNext();
            })
    },

    ObtenerDatos: function () {
        if (cotizacionId != null) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/cotizacion/obtener-cotizacion?empresaId=${empresaId}&cotizacionId=${cotizacionId}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoCotizacion.ResponseObtenerDatos);
        }
    },

    EnviarFormulario: function () {
        let serieId = $("#cmb-serie").val();
        let monedaId = $("#cmb-moneda").val();
        let clienteId = $("#hdn-cliente-id").val();
        let personalId = $("#hdn-personal-id").val();

        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            EmpresaId: empresaId,
            CotizacionId: cotizacionId,
            TipoDocumentoIdentidadId: tipodocumentoidentidadId,
            NroDocumentoIdentidad: nrodocumento,
            RazonSocial: nombres,
            NombreComercial: nombrecomercial,
            PaisId: paisId,
            DistritoId: distritoId,
            Direccion: direccion,
            Correo: correo,
            Usuario: user
        }

        let url = `${urlRoot}api/proveedor/guardar-proveedor`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoCotizacion.ResponseEnviarFormulario);
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
                    location.href = `${urlRoot}Cotizaciones`
                }
            }
        });
    },

    ResponseObtenerDatos: function (data) {
        $("#txt-numero-documento-identidad").val(data.NroDocumentoIdentidad);
        $("#txt-nombres").val(data.RazonSocial);
        $("#txt-nombre-comercial").val(data.NombreComercial);
        $("#txt-correo").val(data.Correo);
        $("#txt-direccion").val(data.Direccion);
        $("#cmb-tipo-documento-identidad").val(data.TipoDocumentoIdentidadId);
        $("#cmb-distrito").val(data.DistritoId);

        $("#cmb-tipo-documento-identidad").prop("disabled", true);
        $("#txt-numero-documento-identidad").prop("disabled", true);
    },

    ResponseSerieListar: function (data) {
        let dataSerie = data.map(x => Object.assign(x, { id: x.SerieId, text: x.Serial }));
        $("#cmb-serie").select2({ data: dataSerie, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseMonedaListar: function (data) {
        let dataMoneda = data.map(x => Object.assign(x, { id: x.MonedaId, text: x.Nombre }));
        $("#cmb-moneda").select2({ data: dataMoneda, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseTipoDocumentoIdentidadListar: function (data) {
        let dataTipoDocumentoIdentidad = data.map(x => Object.assign(x, { id: x.TipoDocumentoIdentidadId, text: x.Descripcion }));
        $("#cmb-tipo-documento-identidad-cliente").select2({ data: dataTipoDocumentoIdentidad, width: '100%', placeholder: '[SELECCIONE...]' });
        $("#cmb-tipo-documento-identidad-cliente").val("").trigger("change");
        $("#cmb-tipo-documento-identidad-personal").select2({ data: dataTipoDocumentoIdentidad, width: '100%', placeholder: '[SELECCIONE...]' });
        $("#cmb-tipo-documento-identidad-personal").val("").trigger("change");
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

    }
}