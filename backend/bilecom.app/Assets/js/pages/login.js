const pageLogin = {
    Init: function () {
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
    },
    ResponseThenAutenticarUsuario: function (data) {
        pageLogin.GuardarCuenta();
        if (data != null && typeof (data) == "object") {
            common.GuardarDataCookie(data);

            location.reload();
        } else {
        }

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
                },
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
            })
    },
}