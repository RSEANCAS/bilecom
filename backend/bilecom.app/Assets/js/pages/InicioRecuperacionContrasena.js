const pageInicioRecuperarContrasena = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {
        $("#btnRecuperar").click(pageInicioRecuperarContrasena.BtnRecuperarClick);
    },
    BtnRecuperarClick: function () {
        let ruc = $("#txt-ruc").val();
        let usuario = $("#txt-usuario").val();

        let formIsValid = $("#frm-iniciarrecuperacion").data("bootstrapValidator").isValid();
        if (!formIsValid) return;

        let url = `${urlRoot}api/usuario/recuperar-contrasena?ruc=${ruc}&usuario=${usuario}`;
        let init = { method: 'POST', credentials: 'include' };

        fetch(url, init)
            .then(r => r.json())
            .then(pageInicioRecuperarContrasena.ResponseRecuperarContrasena)

    },
    ResponseRecuperarContrasena: function (data) {
        if (data) alert("Se envió correo")
    },
    Validar: function () {
        $("#frm-iniciarrecuperacion")
            .bootstrapValidator({
                excluded: [],
                fields: {
                    "txt-ruc": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar el Ruc",
                            },
                            numeric: {
                                message: "Debe ingresar el dato correcto",
                            },
                            remote: {
                                message: "El ruc ingresado no existe",
                                url: `${urlRoot}api/empresa/validar-empresa-por-ruc`,
                                data: (validator) => {
                                    return {
                                        ruc: validator.getFieldElements("txt-ruc").val()
                                    }
                                }
                            }

                        }

                    },
                    "txt-usuario": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar el Usuario"
                            },
                            regexp: {
                                regexp: /^[a-zA-Z]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
                            },
                            remote: {
                                message: "El usuario ingresado no existe",
                                url: `${urlRoot}api/usuario/validar-usuario`,
                                data: (validator) => {
                                    return {
                                        ruc: validator.getFieldElements("txt-ruc").val(),
                                        usuario: validator.getFieldElements("txt-usuario").val()
                                    }
                                }
                            }

                        }
                    }
                },
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
            });
    },
}