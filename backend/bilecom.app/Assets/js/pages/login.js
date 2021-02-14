const pageLogin = {
    Init: function () {
        //this.ValidarCuentaLogueada();
        this.Validar();
        this.RecordarCuenta();
        this.InitEvents();
    },
    InitEvents: function () {
        $("#btnIngresar").click(pageLogin.BtnIngresarClick);
    },
    BtnIngresarClick: function () {
        let ruc = $("#txt-ruc").val();
        let usuario = $("#txt-usuario").val();
        let contraseña = $("#txt-contraseña").val();

        let formIsValid = $("#frm-login").data("bootstrapValidator").isValid();

        if (!formIsValid) return;

        let url = `${urlRoot}api/usuario/autenticar-usuario?ruc=${ruc}&usuario=${usuario}&contraseña=${contraseña}`;
        
        let init = { method: 'POST', credentials: 'include'};

        fetch(url, init)
            .then(common.ResponseToJson)
            .then(pageLogin.ResponseThenAutenticarUsuario)


        //let formIsValid = $("#frm-login").data("bootstrapValidator").isValid();

        //if (formIsValid) {
        //    $("#frm-login").trigger("submit");
        //}
    },
    ResponseThenAutenticarUsuario: function (data) {
        //let validatorFieldToken = data != null && typeof (data) == "object";
        if (data != null && typeof (data) == "object") {
            common.GuardarToken(data.Token);
            common.GuardarUsuario(data.Usuario);
            common.GuardarFechaExpiracion(data.FechaExpiracion);
            debugger;
            let dataString = JSON.stringify(data);
            let fechaExpires = common.ObtenerFechaExpiracion();
            let cookieSession = `ss=${dataString}; expires=${fechaExpires.toUTCString()}`;
            document.cookie = cookieSession;

            location.reload();

            //$("#hdn-token").val(data.Token);
            //$("#token").val(data.Token);
            //$("#usuario").val(JSON.stringify(data.Usuario));
        } else {
            //$("#hdn-token").val("");
            //$("#token").val("");
            //$("#usuario").val("");
        }

        $('#frm-login').data('bootstrapValidator').enableFieldValidators('hdn-token', true);
        //$('#frm-login').data('bootstrapValidator').revalidateField('hdn-token');

        //$('#frm-login').trigger("submit");
    },
    RecordarCuenta: function () {
        let cuenta = localStorage["ls.ac"] == null ? null : JSON.parse(atob(localStorage["ls.ac"]));
        if (cuenta != null) {
            $("#chkRecordarCuenta").prop("checked", true);
            let { ruc, usuario, contraseña } = cuenta;
            $("#txt-ruc").val(ruc);
            $("#txt-usuario").val(usuario);
            $("#txt-contraseña").val(contraseña);

            $("#frm-login").data("bootstrapValidator").validate();
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
        //let token = common.ObtenerToken();

        let usuario = JSON.stringify(common.ObtenerUsuario());
        let token = common.ObtenerToken();

        if (token != null) location.href = `${urlRoot}Login/GuardarSession?usuario=${usuario}&token=${token}`;

        //if (token != null) location.href = `${urlRoot}`;
    },
    Validar: function () {
        $("#frm-login")
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
                    },
                    "txt-contraseña": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar contraseña"
                            }
                        }
                    },
                    "hdn-token": {
                        enabled: false,
                        validators: {
                            notEmpty: {
                                message: "La contraseña es incorrecta"
                            }
                        }
                    }
                },
            })
            .on('success.form.bv', function (e) {
                $("#frm-account").submit();
                //e.preventDefault();
                //pageLogin.GuardarCuenta();
                //pageLogin.EnviarFormulario();
            })
            .on('keyup', '[name="txt-contraseña"]', function () {
                $('#frm-login').data('bootstrapValidator').enableFieldValidators('hdn-token', false);
            });
    },
    //EnviarFormulario: function () {
    //    let ruc = $("#txt-ruc").val();
    //    let usuario = $("#txt-usuario").val();
    //    let contraseña = $("#txt-contraseña").val();

    //    let url = `${urlRoot}api/usuario/autenticar-usuario?ruc=${ruc}&usuario=${usuario}&contraseña=${contraseña}`;
    //    //let params = JSON.stringify({ ruc, usuario, contraseña });
    //    //let headers = { 'Content-Type': 'application/json' };
    //    let init = { method: 'POST' };

    //    fetch(url, init)
    //        .then(common.ResponseToJson)
    //        .then(pageLogin.ResponseThenEnviarFormulario)

    //},
    //ResponseThenEnviarFormulario: function (data) {
    //    //debugger;
    //    if (data != null && typeof (data) == "object") {
    //        common.GuardarUsuario(data.Usuario);
    //        common.GuardarToken(data.Token);
    //        common.GuardarPerfilActual(data.PerfilActual);
    //    }
    //    else if (typeof (data) == "string") {
    //        $.niftyNoty({
    //            type: "danger",
    //            container: "floating",
    //            html: data,
    //            floating: {
    //                position: "bottom-center",
    //                animationIn: "shake",
    //                animationOut: "fadeOut"
    //            },
    //            focus: true,
    //            timer: 0
    //        });
    //        pageLogin.RecordarCuenta();
    //    }
    //    pageLogin.ValidarCuentaLogueada();
    //},
}