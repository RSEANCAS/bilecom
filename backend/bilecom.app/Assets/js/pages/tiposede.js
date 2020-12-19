const pageTipoSede = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        $("#btn-buscar").click(pageTipoSede.BtnBuscarClick);
    },
    BtnBuscarClick: function (e) {
        e.preventDefault();
        pageTipoSede.CreateDataTable("#tbl-lista")
    },
    ObtenerNombre: function () {
        let nombre = $("#txt-nombre").val();
        return nombre;
    },
    CreateDataTable: function (id) {
        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;
        $(id).dataTable({
            serverSide: true,
            ajax: `${urlRoot}api/tiposede/buscar-tiposede?empresaId=${empresaId}&nombre=${pageTipoSede.ObtenerNombre()}`,
            columns: [
                { data: "TipoSedeId" },
                { data: "Nombre" },
                {
                    data: "TipoSedeId", render: function (data, row) {
                        return `<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="${urlRoot}TipoSede/Editar?Id=${data}" data-original-title="Edit" data-container="body"></a><a class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" href = "#" data - original - title="Delete" data - container="body" ></a >`
                    }
                },
            ]
        })
    }
}