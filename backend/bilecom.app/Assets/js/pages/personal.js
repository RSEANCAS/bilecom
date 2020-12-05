const pagePersonal = {
    Init: function () {
        common.ConfiguracionDataTable();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        $("#btn-buscar").click(pagePersonal.btnBuscarClick);
    },
    btnBuscarClick: function (e) {
        e.preventDefault();
        pagePersonal.CreateDataTable("#tbl-lista")
        //pagePersonal.Listar();
    },
    Listar: function () {
        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;
        let nroIdentidad = $("#txt-numero-documento-identidad").val();
        let nombresCompletos = $("#txt-nombres-completos").val();  
        let url = `${urlRoot}api/personal/listar?empresaId=${empresaId}&nroDocumentoIdentidad=${nroIdentidad}&nombresCompletos=${nombresCompletos}`;
        //fetch(url)
        //    .then(r => r.json())
        //    .then(data => pagePersonal.CreateDataTable("#tbl-lista", data, {}));
    },
    ObtenerNroDocumentoIdentidad: function () {
        let nroIdentidad = $("#txt-numero-documento-identidad").val();
        return nroIdentidad;
    },
    ObtenerNombreCompletos: function () {
        let nombresCompletos = $("#txt-nombres-completos").val();  
        return nombresCompletos;
    },
    //CreateDataTable: function (id, data, options) {
    CreateDataTable: function (id) {
        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;
        $(id).dataTable({
            serverSide: true,
            ajax: `${urlRoot}api/personal/listar?empresaId=${empresaId}&nroDocumentoIdentidad=${pagePersonal.ObtenerNroDocumentoIdentidad()}&nombresCompletos=${pagePersonal.ObtenerNombreCompletos()}`,
            destroy: true,
            //data: data,
            columns: [
                {data: "PersonalId"},
                {data: "TipoDocumentoIdentidad.Descripcion"},
                {data: "NroDocumentoIdentidad"},
                {data: "NombresCompletos"},
                {
                    data: "PersonalId", render: function (data, row) {
                        return `<a class="btn btn-sm btn-default btn-hover-dark demo-psi-pen-5 add-tooltip" href="Editar?Id=${data}" data-original-title="Edit" data-container="body"></a><a class="btn btn-sm btn-default btn-hover-danger demo-pli-trash add-tooltip" href = "#" data - original - title="Delete" data - container="body" ></a >`} },
            ]
        })
    }
}