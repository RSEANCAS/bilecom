const pageLogin = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {

    },
    Validar: function () {
        $("#frm-login")
            .bootstrapValidator({
                fields: {
                    "txt-ruc": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar el Ruc",
                            },
                            numeric: {
                                message: "Debe ingresar el dato correcto",
                            }
                        }

                    },
                    "txt-usuario": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar el Usuario"
                            },
                            regexp: {
                                regexp: /^[a-zA-Z]+$/g,
                                message: 'Solo puede ingresar caracteres alfanuméricos'
                            }

                        }
                    },
                    "txt-contraseña": {
                        validators: {
                            notEmpty: {
                                message: "Ingresar contraseña correta"
                            }
                        }
                    },
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageLogin.EnviarFormulario();
            });
    },
    EnviarFormulario: function () {
        let ruc = $("#txt-ruc").val();
        let usuario = $("#txt-usuario").val();
        let contraseña = $("#txt-contraseña").val();

        let headers = {};
        let url = `${urlRoot}`;

        fetch(url, headers)
            .then(r => r.json())
            .then(pageLogin.ResponseEnviarFormulario);
    },
    ResponseEnviarFormulario: function (data) {
        console.log(data);
    }
}