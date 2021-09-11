const fechaActual = new Date();
const pageNotaDebito = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },

    InitEvents: function () {
        $("#btn-buscar").click(pageNotaDebito.BtnBuscarClick);

        $("#txt-fecha-emision-desde").datepicker({ format: "dd/mm/yyyy", autoclose: true });
        $("#txt-fecha-emision-desde").datepicker("setDate", new Date(fechaActual.getFullYear(), fechaActual.getMonth()));
        $("#txt-fecha-emision-hasta").datepicker({ format: "dd/mm/yyyy", autoclose: true });
        $("#txt-fecha-emision-hasta").datepicker("setDate", fechaActual);
    },

    BtnBuscarClick: function (e) {
        e.preventDefault();
        pageNotaDebito.CreateDataTable("#tbl-lista")
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
                    let url = `${urlRoot}api/notadebito/anular-notadebito`;
                    let data = { EmpresaId: user.Empresa.EmpresaId, NotaDebitoId: id, Usuario: user.Nombre };
                    let headers = { 'Content-Type': 'application/json' };
                    let init = { method: 'PUT', headers, body: JSON.stringify(data) };
                    fetch(url, init)
                        .then(common.ResponseToJson)
                        .then(pageNotaDebito.ResponseEliminaRegistro);
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

    ObtenerNroDocumentoIdentidadCliente: function () {
        let nroDocumentoIdentidad = $("#txt-nro-documento-identidad-cliente").val();
        return nroDocumentoIdentidad;
    },

    ObtenerFechaEmisionDesde: function () {
        let fechaEmisionDesde = $("#txt-fecha-emision-desde").datepicker("getDate");
        return fechaEmisionDesde.toLocaleDateString("en-US", { year: "numeric", month: "2-digit", day: "2-digit" });
    },

    ObtenerFechaEmisionHasta: function () {
        let fechaEmisionHasta = $("#txt-fecha-emision-hasta").datepicker("getDate");
        return fechaEmisionHasta.toLocaleDateString("en-US", { year: "numeric", month: "2-digit", day: "2-digit" });
    },

    ObtenerNombreColumna: function () {
        var [columnIndex] = $('#tbl-lista').DataTable().order()[0];
        var columnName = $('#tbl-lista').DataTable().column(columnIndex).dataSrc();
        return columnName;
    },

    ObtenerOrdenColumna: function () {
        var [columnIndex, columnSort] = $('#tbl-lista').DataTable().order()[0];

        return columnSort;
    },

    CreateDataTable: function (id) {
        let estaInicializado = $.fn.DataTable.isDataTable(id);
        if (estaInicializado == true) {
            $(id).DataTable().ajax.reload();
            return;
        }

        let user = common.ObtenerUsuario();

        let ajax = {
            url: `${urlRoot}api/notadebito/buscar-notadebito`,
            data: {
                empresaId: user.Empresa.EmpresaId,
                ambienteSunatId: ambienteSunatId,
                nroDocumentoIdentidadCliente: pageNotaDebito.ObtenerNroDocumentoIdentidadCliente,
                razonSocialCliente: pageNotaDebito.ObtenerRazonSocialCliente,
                fechaEmisionDesde: pageNotaDebito.ObtenerFechaEmisionDesde,
                fechaEmisionHasta: pageNotaDebito.ObtenerFechaEmisionHasta,
                columnaOrden: pageNotaDebito.ObtenerNombreColumna,
                ordenMax: pageNotaDebito.ObtenerOrdenColumna
            }
        };

        let columns = [
            { data: "Fila" },
            { data: "Serie.Serial" },
            {
                data: "NroComprobante", render: (data) => Number(data).toLocaleString("es-PE", { minimumIntegerDigits: 8 }).replace(/,/g, '')
            },
            { data: "FechaHoraEmision", render: (data) => (new Date(data)).toLocaleDateString("es-PE", { year: "numeric", month: "2-digit", day: "2-digit" }) },
            { data: "FechaVencimiento", render: (data) => (new Date(data)).toLocaleDateString("es-PE", { year: "numeric", month: "2-digit", day: "2-digit" }) },
            { data: "Cliente.NroDocumentoIdentidad" },
            { data: "Cliente.RazonSocial" },
            { data: "ImporteTotal", render: (data) => data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
            { data: "EstadoStrRespuestaSunat", render: (data, type, row) => `<span class="label label-${row.EstadoColorRespuestaSunat}">${data}</span>` },
            {
                data: "NotaDebitoId", render: function (data, type, row) {
                    var user = common.ObtenerUsuario();
                    let nombreArchivo = `${user.Empresa.Ruc}-01-${row.Serie.Serial}-${row.NroComprobante}`;
                    let rutaArchivo = `${urlRoot}App_Files/${user.Empresa.Ruc}/Sunat/${ambienteSunatNombre}/Comprobantes/01/${row.Serie.Serial}-${row.NroComprobante}/${nombreArchivo}`;
                    let rutaArchivoCdr = `${urlRoot}App_Files/${user.Empresa.Ruc}/Sunat/${ambienteSunatNombre}/Comprobantes/01/${row.Serie.Serial}-${row.NroComprobante}/R-${nombreArchivo}`;

                    return `${row.FlagAnulado == true ? "" :
                        `<a class="btn btn-sm btn-danger btn-hover-danger fa fa-ban add-tooltip" href="javascript:pageNotaDebito.BtnAnularClick(${data})" data-original-title="Anular" data-container="body"></a>
                        <div class="input-group-btn dropdown">
	                        <button data-toggle="dropdown" class="btn btn-sm btn-primary dropdown-toggle" type="button">
		                        <i class="fa fa-download"></i> <i class="fa fa-sort-down"></i>
	                        </button>
	                        <ul class="dropdown-menu">
		                        <li><a download="${nombreArchivo}.pdf" href="${rutaArchivo}.pdf">PDF</a></li>
		                        <li><a download="${nombreArchivo}.xml" href="${rutaArchivo}.xml">XML</a></li>
		                        <li><a download="R-${nombreArchivo}.zip" href="${rutaArchivoCdr}.zip">CDR</a></li>
	                        </ul>
                        </div>`
                        }`;
                }
            }
        ];

        let aditional = {
            rowCallback: function (row, data) {
                if (data.FlagAnulado == true) $(row).addClass('bg-danger');
            }
        };

        common.CreateDataTableFromAjax(id, ajax, columns, aditional);
    }
}