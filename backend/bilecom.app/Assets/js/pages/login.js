const pageLogin = {
    Init: function () {
        this.RecordarCuenta();
        this.ValidarCuentaLogueada();
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {

    },
    RecordarCuenta: function () {
        let cuenta = localStorage["ls.ac"] == null ? null : JSON.parse(atob(localStorage["ls.ac"]));
        if (cuenta != null) {
            $("#chkRecordarCuenta").prop("checked", true);
            let { ruc, usuario, contraseña } = cuenta;
            $("#txt-ruc").val(ruc);
            $("#txt-usuario").val(usuario);
            $("#txt-contraseña").val(contraseña);
        }
    },
    GuardarCuenta: function () {
        let recordarCuenta = $("#chkRecordarCuenta").prop("checked");
        if (recordarCuenta) {
            let cuenta = {
                ruc: $("#txt-ruc").val(),
                usuario: $("#txt-usuario").val(),
                contraseña: $("#txt-contraseña").val()
            }

            localStorage["ls.ac"] = btoa(JSON.stringify(cuenta));
        }
        else localStorage.removeItem("ls.ac");
    },
    ValidarCuentaLogueada: function () {
        let token = common.ObtenerToken();

        if (token != null) location.href = `${urlRoot}`;
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
                            },
                            remote: {
                                message: "El ruc ingresado no existe",
                                url: `${urlRoot}api/empresa/validar-ruc`,
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
                    },
                    "txt-contraseña": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar contraseña"
                            }
                        }
                    },
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageLogin.GuardarCuenta();
                pageLogin.EnviarFormulario();
            });
    },
    EnviarFormulario: function () {
        let ruc = $("#txt-ruc").val();
        let usuario = $("#txt-usuario").val();
        let contraseña = $("#txt-contraseña").val();

        let url = `${urlRoot}api/usuario/autenticar?ruc=${ruc}&usuario=${usuario}&contraseña=${contraseña}`;
        //let params = JSON.stringify({ ruc, usuario, contraseña });
        //let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST' };

        fetch(url, init)
            .then(common.ResponseToJson)
            .then(pageLogin.ResponseThenEnviarFormulario)

    },
    ResponseThenEnviarFormulario: function (data) {
        if (data != null && typeof (data) == "object") {
            localStorage['ls.us'] = JSON.stringify(data.Usuario);
            localStorage['ls.tk'] = data.Token;
        }
        else if(typeof(data) == "string") {
            $.niftyNoty({
                type: "danger",
                container: "floating",
                html: data,
                floating: {
                    position: "bottom-center",
                    animationIn: "shake",
                    animationOut: "fadeOut"
                },
                focus: true,
                timer: 2500
            });
            pageLogin.RecordarCuenta();
        }
        pageLogin.ValidarCuentaLogueada();
    },
}