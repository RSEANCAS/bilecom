$(document).ready(function () {
    $("#frm-login").bootstrapValidator({
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
                        message: 'Solo puede ingresar caracteres alphanuméricos'
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
