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
                { data: "NroComprobante" },
                { data: "NroPedido" },
                { data: "FechaHoraEmision", render: (data) => data.toLocaleDateString("es-PE", { year: "numeric", month: "2-digit", day: "2-digit" }) },
                { data: "Personal.NombresCompletos" },
                { data: "Cliente.RazonSocial" },
                {
                    data: "PersonalId", render: function (data, row) {
                        return `<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="${urlRoot}Personal/Editar?Id=${data}" data-original-title="Edit" data-container="body"></a><a class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" href = "#" data - original - title="Delete" data - container="body" ></a >`
                    }
                },
            ]
        })
    }
}