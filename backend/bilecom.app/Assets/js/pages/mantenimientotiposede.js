const pageMantenimientoTipoSede = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {
        pageMantenimientoTipoSede.ObtenerDatos();
    },
    Validar: function () {
        $("#frm-tiposede")
            .bootstrapValidator({
                fields: {
                    "txt-tipo-sede": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Tipo de Sede.",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-_ñÑ .]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
                            }
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoTipoSede.EnviarFormulario();
            });
    },
    ObtenerDatos: function () {
        let numero = $("#txt-opcion").val();
        if (numero != 0) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/tiposede/obtener-tiposede?empresaId=${empresaId}&tiposedeId=${numero}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoTipoSede.ResponseObtenerDatos);
        }
    },
    EnviarFormulario: function () {
        let tiposedeId = $("#txt-opcion").val();
        let nombre = $("#txt-tipo-sede").val();
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            TipoSedeId: tiposedeId,
            EmpresaId: empresaId,
            FlagActivo: 1,
            Nombre: nombre,
            Usuario: user
        }

        let url = `${urlRoot}api/tiposede/guardar-tiposede`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoTipoSede.ResponseEnviarFormulario);
    },
    ResponseObtenerDatos: function (data) {
        $("#txt-tipo-sede").val(data.Nombre);
    },

    ResponseEnviarFormulario: function (data) {
        let tipo = "", mensaje = "";
        if (data == true) {
            tipo = "success";
            mensaje = "¡Se ha guardado con éxito!";
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
                    location.href = `${urlRoot}tiposede`
                }
            }
        });
    }
}

$("#txt-tipo-sede").bind('keypress', function (e) {
    let keyCode = (e.which) ? e.which : event.keyCode;
    return (keyCode == 32 || (keyCode > 47 && keyCode < 58) || (keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123));
});
