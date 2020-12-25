const pageMantenimientoCategoriaProducto = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {
        pageMantenimientoCategoriaProducto.ObtenerDatos();
    },
    Validar: function (){
        $("#frm-categoriaproducto-mantenimiento")
            .bootstrapValidator({
                fields: {
                    "txt-nombre": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Categoria.",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-_ñÑ .]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
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
    ObtenerDatos: function () {
        let numero = $("#txt-opcion").val();
        if (numero != 0) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/categoriaproducto/obtener-categoriaproducto?empresaId=${empresaId}&categoriaproductoId=${numero}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoCategoriaProducto.ResponseObtenerDatos);
        }
    },
    EnviarFormulario: function () {
        let categoriaproductoId = $("#txt-opcion").val();
        let nombre = $("#txt-nombre").val();
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            EmpresaId: empresaId,
            CategoriaProductoId: categoriaproductoId,
            Nombre: nombre,
            Usuario: user
        }

        let url = `${urlRoot}api/categoriaproducto/guardar-categoriaproducto`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST',body: params , headers};

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoCategoriaProducto.ResponseEnviarFormulario);
    },
    ResponseObtenerDatos: function (data) {
        $("#txt-nombre").val(data.Nombre);
    },

    ResponseEnviarFormulario: function (data) {
        let tipo = "", mensaje = "";
        if (data == true) {
            tipo = "success";
            mensaje = "¡Se ha guardado con éxito!";
        } else {
            tipo = "danger";
            mensaje = "¡Se ha producido un error, vuelve a intentarlo!";
        }
        $.niftyNoty({
            type: tipo,
            container: "floating",
            html: mensaje,
            floating: {
                position: "top-center",
                animationIn: "shake",
                animationOut: "fadeOut"
            },
            focus: true,
            timer: 1800,
            onHide: function () {
                if (data == true) {
                    location.href = `${urlRoot}CategoriasProducto`
                }
            }
        });
    }
}