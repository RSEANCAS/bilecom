const pageInicio =
{

    Init: function () {
        pageInicio.ConfiguracionDataTableTablero();
        this.InitEvents();
        pageInicio.CreateDataTable("#tbl-ultimosdocumentos")
    },
    InitEvents: function () {
        this.CargaConteo_x_Documento();

    },
    CargaConteo_x_Documento: function () {
        var meses = ["Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"];

        let fechaActual = new Date();

        let anyo = fechaActual.getFullYear();
        let mes = fechaActual.getMonth() + 1;
        let nummes = mes - 1;
        if (nummes == 0) {
            nummes = 11;
        }
        let mesletras = meses[nummes];
        $("#span-mes").text(mesletras);

        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

        let url = `${urlRoot}api/tablero/conteo_x_documento-tablero?empresaId=${empresaId}&anio=${anyo}&mes=${mes}`;
        let init = { method: 'GET' };

        fetch(url, init)
            .then(r => r.json())
            .then(pageInicio.ResponseCargaConteo_x_Documento);
    },

    CreateDataTable: function (id) {
        let estaInicializado = $.fn.DataTable.isDataTable(id);
        if (estaInicializado == true) {
            $(id).DataTable().ajax.reload();
            return;
        }
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        $(id).dataTable({
            processing: true,
            serverSide: true,
            ajax: {
                //${urlRoot}api/cliente/buscar-cliente?empresaId=${empresaId}&nroDocumentoIdentidad=${pageCliente.ObtenerNroDocumentoIdentidad()}&razonSocial=${pageCliente.ObtenerNombres()}`,
                url: `${urlRoot}api/tablero/listar-tableroultimosdocumentos`,
                data: {
                    empresaId: empresaId
                },
                dataSrc: ""
            },
            columns: [
                { data: "TipoDocumentoDescripcion", render: (data) => data.trim() },
                { data: "Serie", render: function (data, type, row) { return `${row.Serie}-` + ("00000000" + row.Numero).slice(-8) } },
                { data: "FechaEmision", render: (data) => (new Date(data)).toLocaleDateString("es-PE", { year: "numeric", month: "2-digit", day: "2-digit" }) },
                { data: "Total", render: (data, type, row) => row.SimboloMoneda + " " + data.toLocaleString('en-US', { minimumFractionDigits: 2, maximumFractionDigits: 2 }) },
                { data: "ClienteRazonSocial", render: (data) => data.trim() },
                {
                    data: "EstadoSunatId", render: function (data, type, row) {
                        let tipolabel = "";
                        if (data == 1) {
                            tipolabel = "label-success";
                        } else if (data == 2) {
                            tipolabel = "label-warning";
                        } else if (data == 3) {
                            tipolabel = "label-danger";
                        } else if (data == 4) {
                            tipolabel = "label-info";
                        }

                        return `<span class="text-center label label-table ${tipolabel}">${row.EstadoSunatDescripcion}</span>`
                    }
                }
            ]
        })
    },

    ResponseCargaConteo_x_Documento: function (data) {

        $("#span-fa-emitidos").text(data.TotalDocumentoFa);
        $("#span-fa-anulados").text(data.TotalAnuladoFa);
        $("#span-bo-emitidos").text(data.TotalDocumentoBo);
        $("#span-bo-anulados").text(data.TotalAnuladoBo);
        $("#span-nc-emitidos").text(data.TotalDocumentoNC);
        $("#span-nc-anulados").text(data.TotalAnuladoNC);
        $("#span-nd-emitidos").text(data.TotalDocumentoND);
        $("#span-nd-anulados").text(data.TotalAnuladoND);
    },
    ConfiguracionDataTableTablero() {
        $.extend($.fn.dataTable.defaults, {
            searching: false,
            lengthChange: false,
            paging: false,
            info: false,
            language: {
                emptyTable: "No hay datos disponibles",
                zeroRecords: "No se encontraron registros coincidentes",
                loadingRecords: "Cargando...",
                Processing: "Procesando...",

            }
        })
    }
}