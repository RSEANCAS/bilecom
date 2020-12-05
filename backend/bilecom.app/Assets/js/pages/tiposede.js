const pageTipoSede = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        $("#btn-buscar").click(pageTipoSede.btnBuscarClick);
    },
    btnBuscarClick: function (e) {
        e.preventDefault();
        pageTipoSede.CreateDataTable("#tbl-lista")
        //pagePersonal.Listar();
    },
    Listar: function () {
        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;
        let nombre = $("#txt-nombre").val();
        let url = `${urlRoot}api/tiposede/listar?empresaId=${empresaId}&nombre=${nombre}`;
        //fetch(url)
        //    .then(r => r.json())
        //    .then(data => pagePersonal.CreateDataTable("#tbl-lista", data, {}));
    },
    ObtenerNombre: function () {
        let nombre = $("#txt-nombre").val();
        return nombre;
    },
    //CreateDataTable: function (id, data, options) {
    CreateDataTable: function (id) {
        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;
        $(id).dataTable({
            serverSide: true,
            ajax: `${urlRoot}api/tiposede/listar?empresaId=${empresaId}&nombre=${pageTipoSede.ObtenerNombre()}`,
            //destroy: true,
            //data: data,
            columns: [
                { data: "TipoSedeId" },
                { data: "Nombre" },
                {
                    data: "TipoSedeId", render: function (data, row) {
                        return '<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="#" data-original-title="Edit" data-container="body"></a><a class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" href = "#" data - original - title="Delete" data - container="body" ></a >'
                    }
                },
            ]
        })
    }
}