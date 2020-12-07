const pageMantenimientoProducto = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {

    },
    Validar: function () {
        $("#frm-producto-mantenimiento")
            .bootstrapValidator({
                fields: {
                    "txt-nombre": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Categoria.",
                            }
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoProducto.EnviarFormulario();
            });
    },
    EnviarFormulario: function () {
        let nombre = $("#txt-nombre").val();
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            EmpresaId : empresaId,
            ProductoId :0,
            CategoriaId :1,
            Nombre: nombre,
            Usuario : user
        }

        let url = `${urlRoot}api/producto/guardar-producto`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoProducto.ResponseEnviarFormulario);
    },
    ResponseEnviarFormulario: function (data) {
        if (data == true) {
            alert("Se ha guardado con éxito.");
            location.href = "Index";
        } else {
            alert("No se pudo guardar la categoria intentelo otra vez.");
        }
    }
}