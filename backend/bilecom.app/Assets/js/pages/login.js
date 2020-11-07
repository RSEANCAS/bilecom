$(document).ready(function () {
    $("#frm-login").bootstrapValidator({
        fields: {
            "txt-ruc": {
                validators: {
                    notEmpty: {
                        message: "Debe ingresar el Ruc"
                    }
                }
            },
            "txt-usuario": {
                validators: {
                    notEmpty: {
                        message: "Debe ingresar el Usuario"
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
    });
});