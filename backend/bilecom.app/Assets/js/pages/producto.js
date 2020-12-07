const pageProducto = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        $("#btn-buscar").click(pageProducto.BtnBuscarClick);
    },
    BtnBuscarClick: function (e) {
        e.preventDefault();
        pageProducto.CreateDataTable("#tbl-lista")
    },
    ObtenerNombre: function () {
        let nombre = $("#txt-nombre").val();
        return nombre;
    },
    ObtenerCategoria: function () {
        let categoria = $("#txt-categoria").val();
        return categoria;
    },
    CreateDataTable: function (id) {
        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;
        $(id).dataTable({
            serverSide: true,
            ajax: `${urlRoot}api/producto/buscar-producto?empresaId=${empresaId}&nombre=${pageProducto.ObtenerNombre()}&categoriaNombre=${pageProducto.ObtenerCategoria()}`,
            columns: [
                { data: "ProductoId" },
                { data: "Nombre" },
                { data: "categoriaProducto.Nombre" },
                { data: "Stock" }, 
                {
                    data: "ProductoId", render: function (data, row) {
                        return '<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="#" data-original-title="Edit" data-container="body"></a><a class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" href = "#" data - original - title="Delete" data - container="body" ></a >'
                    }
                },
            ]
        })
    }
}