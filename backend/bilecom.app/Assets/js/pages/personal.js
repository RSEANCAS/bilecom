const pagePersonal = {
    Init: function () {
        this.Validar();
        this.InitEvents();
        $("#btn-buscar").trigger("click");
    },
    InitEvents: function () {
        $("#btn-buscar").click(pagePersonal.btnBuscarClick);
    },
    btnBuscarClick: function (e) {
        e.preventDefault();
        pagePersonal.Listar();
    },
    Listar: function () {
        let user = common.ObtenerUsuario();
        let empresaId = user.Empresa.EmpresaId;
        let nroIdentidad = $("#txt-numero-documento-identidad").val();
        let nombresCompletos = $("#txt-nombres-completos").val();  
        let url = `${urlRoot}api/personal/listar/${empresaId}/${nroIdentidad}/${nombresCompletos}`;
        fetch(url, init)
            .then(r => r.json())
            .then(pagePersonal.ResponseEnviarFormulario);
    },
}