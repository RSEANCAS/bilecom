const pageMantenimientoPersonal = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {

    },
    Validar: function () {
        $("#frm-personal-mantenimiento")
            .bootstrapValidator({
                fields: {
                    "txt-nombre": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Categoria",
                            }
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoPersonal.EnviarFormulario();
            });
    },
    EnviarFormulario: function () {
        let nrodocumento = $("#txt-numero-documento-identidad").val();
        let nombre = $("#txt-nombres").val();
        let correo = $("#txt-correo").val();
        let direccion = $("#txt-direccion").val();

        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;
        
        let ObjectoJson = {
            PersonalId: 0,
            EmpresaId: empresaId,
            TipoDocumentoIdentidadId: 1,
            NroDocumentoIdentidad: nrodocumento,
            NombresCompletos:nombre ,
            Direccion: direccion,
            Correo: correo,
            FlagActivo: 1,
            Usuario: user,
            DistritoId: '010101',
        }

        let url = `${urlRoot}api/personal/guardar`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoPersonal.ResponseEnviarFormulario);
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