const pageRecuperarContrasena = {
    Init: function () {
        this.Validar();
    },
    Validar: function () {
        $("#frm-recuperarcontrasena")
            .bootstrapValidator({
                //excluded: [],
                fields: {
                    "contrasena": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar la nueva contraseña",
                            },
                            numeric: {
                                message: "Debe ingresar la nueva contraseña",
                            }
                        }

                    },
                    "txt-repita-contrasena": {
                        validators: {
                            notEmpty: {
                                message: "Repita la nueva contraseña"
                            }
                        }
                    }
                },
            })
            .on('success.form.bv', function (e) {
                var $form = $(e.target),
                    validator = $form.data('bootstrapValidator');
                console.log(validator.getInvalidFields());
                //$("#frm-recuperarcontrasena").submit();
                //e.preventDefault();
                //pageLogin.GuardarCuenta();
                //pageLogin.EnviarFormulario();
            });
    }
}
