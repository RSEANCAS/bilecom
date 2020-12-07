const pageCliente = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        $("#btn-buscar").click(pageCliente.BtnBuscarClick);
    },
    BtnBuscarClick: function (e) {
        e.preventDefault();
        pageCliente.CreateDataTable("#tbl-lista")
    },
    ObtenerNroDocumentoIdentidad: function () {
        let nroIdentidad = $("#txt-numero-documento-identidad").val();
        return nroIdentidad;
    },
    ObtenerNombres: function () {
        let nombres = $("#txt-nombres-o-razon-social").val();
        return nombres;
    },
    CreateDataTable: function (id) {
        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;
        $(id).dataTable({
            serverSide: true,
            ajax: `${urlRoot}api/cliente/buscar-cliente?empresaId=${empresaId}&nroDocumentoIdentidad=${pageCliente.ObtenerNroDocumentoIdentidad()}&razonSocial=${pageCliente.ObtenerNombres()}`,
            columns: [
                { data: "ClienteId" },
                { data: "TipoDocumentoIdentidad.Descripcion" },
                { data: "NroDocumentoIdentidad" },
                { data: "RazonSocial" },
                {
                    data: "ClienteId", render: function (data, row) {
                        return '<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="#" data-original-title="Edit" data-container="body"></a><a class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" href = "#" data - original - title="Delete" data - container="body" ></a >'
                    }
                },
            ]
        })
    }
}