const pageCotizacion = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },

    InitEvents: function () {
        $("#btn-buscar").click(pageCotizacion.BtnBuscarClick);

        let fechaActual = new Date();
        $("#txt-fecha-emision-desde").datepicker({ format: "dd/mm/yyyy", autoclose: true });
        $("#txt-fecha-emision-desde").datepicker("setDate", new Date(fechaActual.getFullYear(), fechaActual.getMonth()));
        $("#txt-fecha-emision-hasta").datepicker({ format: "dd/mm/yyyy", autoclose: true });
        $("#txt-fecha-emision-hasta").datepicker("setDate", fechaActual);
    },

    BtnBuscarClick: function (e) {
        e.preventDefault();
        pageCotizacion.CreateDataTable("#tbl-lista")
    },

    BtnAnularClick: function (id) {
        bootbox.confirm({
            title: 'Anular Registro',
            message: '¿Está seguro de anular el registro?',
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
                    let user = common.ObtenerUsuario();
                    let url = `${urlRoot}api/cotizacion/anular-cotizacion`;
                    let data = { EmpresaId: user.Empresa.EmpresaId, CotizacionId: id, Usuario: user.Nombre };
                    let headers = { 'Content-Type': 'application/json' };
                    let init = { method: 'PUT', headers, body: JSON.stringify(data) };
                    fetch(url, init)
                        .then(common.ResponseToJson)
                        .then(pageCotizacion.ResponseEliminaRegistro);
                }
            }
        })
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
                $("#btn-buscar").trigger("click");
            }
        });
    },

    ObtenerRazonSocialCliente: function () {
        let nroIdentidad = $("#txt-razon-social-cliente").val();
        return nroIdentidad;
    },

    ObtenerNombreCompletosPersonal: function () {
        let nombresCompletos = $("#txt-nombres-completos-personal").val();
        return nombresCompletos;
    },

    ObtenerFechaEmisionDesde: function () {
        let fechaEmisionDesde = $("#txt-fecha-emision-desde").datepicker("getDate");
        return fechaEmisionDesde.toLocaleDateString("en-US", { year: "numeric", month: "2-digit", day: "2-digit" });
    },

    ObtenerFechaEmisionHasta: function () {
        let fechaEmisionHasta = $("#txt-fecha-emision-hasta").datepicker("getDate");
        return fechaEmisionHasta.toLocaleDateString("en-US", { year: "numeric", month: "2-digit", day: "2-digit" });
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
                url: `${urlRoot}api/cotizacion/buscar-cotizacion`,
                data: {
                    empresaId: empresaId,
                    nombresCompletosPersonal: pageCotizacion.ObtenerNombreCompletosPersonal,
                    razonSocialCliente: pageCotizacion.ObtenerRazonSocialCliente,
                    fechaEmisionDesde: pageCotizacion.ObtenerFechaEmisionDesde,
                    fechaEmisionHasta: pageCotizacion.ObtenerFechaEmisionHasta
                }
            },
            columns: [
                { data: "Fila" },
                { data: "Serie.Serial" },
                { data: "NroComprobante", render: (data) => Number(data).toLocaleString("es-PE", { minimumIntegerDigits: 8 }).replace(/,/g, '') },
                //{ data: "NroPedido" },
                { data: "FechaHoraEmision", render: (data) => (new Date(data)).toLocaleDateString("es-PE", { year: "numeric", month: "2-digit", day: "2-digit" }) },
                { data: "Personal.NombresCompletos" },
                { data: "Cliente.RazonSocial" },
                { data: "TotalImporte", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
                {
                    data: "CotizacionId", render: function (data, type, row) {
                        return `${row.FlagAnulado == true ? "" :
                            `<a class="btn btn-sm btn-${row.FlagAnulado == false ? "default" : "dark"} btn-hover-dark fa fa-pencil add-tooltip" href="${urlRoot}Cotizaciones/Editar?id=${data}" data-original-title="Editar" data-container="body"></a>
                            <a class="btn btn-sm btn-danger btn-hover-danger fa fa-ban add-tooltip" href="javascript:pageCotizacion.BtnAnularClick(${data})" data-original-title="Anular" data-container="body"></a>
                            <div class="btn-group">
                                <button type="button" class="btn btn-sm btn-success btn-hover-success dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                    <!--Más <span class="caret"></span>-->
                                    <i class="fa fa-ellipsis-v"></i>
                                </button>
                                <ul class="dropdown-menu">
                                    <li><a href="javascript:void(0)" class="small">Generar Boleta</a></li>
                                    <li><a href="javascript:void(0)" class="small">Generar Factura</a></li>
                                    <li role="separator" class="divider"></li>
                                    <li><a href="javascript:void(0)" class="small">Generar Nota de Venta</a></li>
                                </ul>
                            </div>`}`;
                    }
                },
            ],
            rowCallback: function (row, data) {
                if(data.FlagAnulado == true) $(row).addClass('bg-danger');
            }
        })
    }
}