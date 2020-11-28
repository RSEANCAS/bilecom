const pageMantenimientoCategoriaProducto = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {
        
    },
    Validar: function (){
        $("#form-categoriaproducto-mantimiento")
            .bootstrapValidator({
                fields: {
                    "txt-categoria": {
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
                pageMantenimientoCategoriaProducto.EnviarFormulario();
            });
    },
    EnviarFormulario: function () {
        let nombre = $("#txt-nombre").val();
        let ObjectoJson = {
            categoriaProductoBe: {
                EmpresaId : '10762193987',
                CategoriaProductoId : 0,
                Nombre: nombre ,
                Usuario: 'pepe lucho',
                Fecha: Date.now
            }
        }

        let url = `${urlRoot}api/categoriaproducto/guardar`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST',body: params , headers};

        fetch(url, init)
            .then(r => r.json())
            .then(pageLogin.ResponseEnviarFormulario);
    },
    ResponseEnviarFormulario: function (data) {
        console.log(data);
    }
}