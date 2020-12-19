var CategoriaProductoLista = [];
const pageMantenimientoProducto = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {
        pageMantenimientoProducto.ObtenerDatos();
        pageMantenimientoProducto.CargarCombo();
    },
    Validar: function () {
        $("#frm-producto-mantenimiento")
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
                pageMantenimientoProducto.EnviarFormulario();
            });
    },
    ObtenerDatos: function () {
        let numero = $("#txt-opcion").val();
        if (numero != 0) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/producto/obtener-producto?empresaId=${empresaId}&productoId=${numero}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoProducto.ResponseObtenerDatos);
        }
    },
    CargarCombo: async function () {
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let promises = [
            fetch(`${urlRoot}api/categoriaproducto/listar-categoriaproducto?empresaId=${empresaId}`)]
        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([CategoriaProductoLista]) => {
                CategoriaProductoLista = CategoriaProductoLista;
                
                pageMantenimientoProducto.ResponseCategoriaProductoListar(CategoriaProductoLista);
                //$("#cmb-categoria").val(1).trigger('change');
            })
    },
    EnviarFormulario: function () {
        let productoId = $("#txt-opcion").val();
        let nombre = $("#txt-nombre").val();
        let categoriaId = $("#cmb-categoria").val();
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            EmpresaId : empresaId,
            ProductoId :productoId,
            CategoriaId: categoriaId,
            Nombre: nombre,
            Usuario : user
        }

        let url = `${urlRoot}api/producto/guardar-producto`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoProducto.ResponseEnviarFormulario);
    },
    ResponseObtenerDatos: function (data) {
        $("#txt-nombre").val(data.Nombre);
        $("#cmb-categoria").val(data.CategoriaId).trigger('change');
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
                    location.href = `${urlRoot}Producto`
                }
            }
        });
    },
    ResponseCategoriaProductoListar: function (data) {
        let datacategoriaproducto = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.CategoriaProductoId, text: item.Nombre }); });
        $("#cmb-categoria").select2({ data: datacategoriaproducto, width: '100%', placeholder: '[SELECCIONE...]' });
    },
}