const pageProveedor = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        $("#btn-buscar").click(pageProveedor.BtnBuscarClick);
    },
    BtnBuscarClick: function (e) {
        e.preventDefault();
        pageProveedor.CreateDataTable("#tbl-lista")
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
                //`${urlRoot}api/proveedor/buscar-proveedor?empresaId=${empresaId}&nroDocumentoIdentidad=${pageProveedor.ObtenerNroDocumentoIdentidad()}&razonSocial=${pageProveedor.ObtenerNombres()}`,
                url: `${urlRoot}api/proveedor/buscar-proveedor`,
                data: {
                    empresaId: empresaId,
                    nroDocumentoIdentidad: pageProveedor.ObtenerNroDocumentoIdentidad,
                    razonSocial: pageProveedor.ObtenerNombres
                }
            },
            columns: [
                { data: "ProveedorId" },
                { data: "TipoDocumentoIdentidad.Descripcion" },
                { data: "NroDocumentoIdentidad" },
                { data: "RazonSocial" },
                {
                    data: "ProveedorId", render: function (data, row) {
                        return `<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="${urlRoot}Proveedores/Editar?Id=${data}" data-original-title="Edit" data-container="body"></a><a class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" onclick="pageProveedor.btnEliminaClick(${data})" data - original - title="Delete" data - container="body" ></a >`
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
            html: `<h4 class="alert-title" align="center">¿Desea eliminar registro?</h4><div class="mar-top" align="center"><button type="button" onclick="pageProveedor.EliminaRegistro(${id})" class="btn btn-primary" data-dismiss="noty">Si</button><button type="button" class="btn btn-info" data-dismiss="noty">No</button></div>`

        });
    },

    EliminaRegistro: function (id) {
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let url = `${urlRoot}api/proveedor/eliminar-proveedor?empresaId=${empresaId}&proveedorId=${id}&Usuario=${user}`;
        let init = { method: 'POST' };

        fetch(url, init)
            .then(r => r.json())
            .then(pageProveedor.ResponseEliminaRegistro);
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
                    location.href = `${urlRoot}Proveedores`
                }
            }
        });
    },
}