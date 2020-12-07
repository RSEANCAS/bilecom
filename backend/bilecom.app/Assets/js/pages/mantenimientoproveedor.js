const pageMantenimientoProveedor = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {

    },
    Validar: function () {
        $("#frm-proveedor-mantenimiento")
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
                pageMantenimientoProveedor.EnviarFormulario();
            });
    },
    EnviarFormulario: function () {
        let nombres = $("#txt-nombres").val();
        let nrodocumento = $("#txt-numero-documento-identidad").val();
        let nombrecomercial = $("#txt-nombre-comercial").val();
        let direcion = $("#txt-direccion").val();
        let correo = $("#txt-correo").val();

        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            ClienteId: 0,
            EmpresaId: empresaId,
            TipoDocumento: 6,
            NroDocumentoIdentidad: nrodocumento,
            RazonSocial: nombres,
            NombreComercial: nombrecomercial,
            DistritoId: '100101',
            Direccion: direcion,
            Correo: correo,
            FlagActivo: 1,
            Usuario: user
        }

        let url = `${urlRoot}api/proveedor/guardar-proveedor`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoProveedor.ResponseEnviarFormulario);
    },
    ResponseEnviarFormulario: function (data) {
        if (data == true) {
            alert("Se ha guardado con éxito.");
            location.href = "Index";
        } else {
            alert("No se pudo guardar la proveedor intentelo otra vez.");
        }
    }
}