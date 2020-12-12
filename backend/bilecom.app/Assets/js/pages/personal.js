const pagePersonal = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        $("#btn-buscar").click(pagePersonal.BtnBuscarClick);
    },
    BtnBuscarClick: function (e) {
        e.preventDefault();
        pagePersonal.CreateDataTable("#tbl-lista")
    },
    ObtenerNroDocumentoIdentidad: function () {
        let nroIdentidad = $("#txt-numero-documento-identidad").val();
        return nroIdentidad;
    },
    ObtenerNombreCompletos: function () {
        let nombresCompletos = $("#txt-nombres-completos").val();  
        return nombresCompletos;
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
                //url: `${urlRoot}api/personal/buscar-personal?empresaId=${empresaId}&nroDocumentoIdentidad=${pagePersonal.ObtenerNroDocumentoIdentidad()}&nombresCompletos=${pagePersonal.ObtenerNombreCompletos()}`,
                url: `${urlRoot}api/personal/buscar-personal`,
                data: {
                    empresaId: empresaId,
                    nroDocumentoIdentidad: pagePersonal.ObtenerNroDocumentoIdentidad,
                    nombresCompletos: pagePersonal.ObtenerNombreCompletos
                }
            },
            columns: [
                {data: "PersonalId"},
                {data: "TipoDocumentoIdentidad.Descripcion"},
                {data: "NroDocumentoIdentidad"},
                {data: "NombresCompletos"},
                {
                    data: "PersonalId", render: function (data, row) {
                        return `<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="${urlRoot}Personal/Editar?Id=${data}" data-original-title="Edit" data-container="body"></a><a class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" href = "#" data - original - title="Delete" data - container="body" ></a >`
                    }
                },
            ]
        })
    }
}