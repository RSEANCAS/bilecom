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
                url: `${urlRoot}api/tiposede/buscar-tiposede`,
                data: {
                    empresaId: empresaId,
                    nombre:pageTipoSede.ObtenerNombre
                }
            },
            columns: [
                { data: "TipoSedeId" },
                { data: "Nombre" },
                { data: "CodigoPlantillaStr" },
                { data: "FlagEditable", render: function (data) { return `<input type="checkbox" ${data == true ? "checked" : ""} disabled />` } },
                {
                    data: "TipoSedeId", render: function (data, row) {
                        return `<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="${urlRoot}TiposSede/Editar?Id=${data}" data-original-title="Edit" data-container="body"></a><a class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" onclick="pageTipoSede.btnEliminaClick(${data})" data - original - title="Delete" data - container="body" ></a >`
                    }
                },
            ]
        })
    },

    btnEliminaClick: function (id) {
        $.niftyNoty({
            icon: "danger",
            type: "danger",
            container: "floating",
            floating: {
                position: "top-center",
                animationIn: "shake",
                animationOut: "fadeOut",
            },
            closeBtn: false,
            html: `<h4 class="alert-title" align="center">¿Desea eliminar registro?</h4><div class="mar-top" align="center"><button type="button" onclick="pageTipoSede.EliminaRegistro(${id})" class="btn btn-primary" data-dismiss="noty">Si</button><button type="button" class="btn btn-info" data-dismiss="noty">No</button></div>`

        });
    },

    EliminaRegistro: function (id) {
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let url = `${urlRoot}api/tiposede/eliminar-tiposede?empresaId=${empresaId}&tiposedeId=${id}&Usuario=${user}`;
        let init = { method: 'POST' };

        fetch(url, init)
            .then(r => r.json())
            .then(pageTipoSede.ResponseEliminaRegistro);
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
                if (data == true) {
                    location.href = `${urlRoot}TiposSede`
                }
            }
        });
    },
}