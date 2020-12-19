const pageCategoriaProducto = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        $("#btn-buscar").click(pageCategoriaProducto.BtnBuscarClick);
    },
    BtnBuscarClick: function (e) {
        e.preventDefault();
        pageCategoriaProducto.CreateDataTable("#tbl-lista")
    },
    ObtenerNombre: function () {
        let nombre = $("#txt-nombre").val();
        return nombre;
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
                //`${urlRoot}api/categoriaproducto/buscar-categoriaproducto?empresaId=${empresaId}&nombre=${pageCategoriaProducto.ObtenerNombre()}`,
                url: `${urlRoot}api/categoriaproducto/buscar-categoriaproducto`,
                data: {
                    empresaId: empresaId,
                    nombre: pageCategoriaProducto.ObtenerNombre
                }
            },
            columns: [
                { data: "CategoriaProductoId" },
                { data: "Nombre" },
                {
                    data: "CategoriaProductoId", render: function (data, row) {
                        return `<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="${urlRoot}CategoriaProducto/Editar?Id=${data}" data-original-title="Edit" data-container="body"></a><a class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" href = "#" data - original - title="Delete" data - container="body" ></a >`
                    }
                },
            ]
        })
    }
}