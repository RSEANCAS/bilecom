var CategoriaProductoLista = [], UnidadMedidaLista = [], TipoAfectacionLista = [], TipoCalculo = [], TipoProductoLista = [];
TipoCalculo = [{
    Id: "VALORUNITARIO",
    Descripcion: "Valor Unitario (monto sin igv)"
}, { Id: "PRECIOUNITARIO", Descripcion: "Precio Unitario (monto con igv)" }, { Id: "IMPORTETOTAL", Descripcion: "Importe Total" }];

const pageMantenimientoProducto = {
    Init: function() {
        this.Validar();
        this.InitEvents();
        
    },
    InitEvents: function () {
        pageMantenimientoProducto.CargarCombo(pageMantenimientoProducto.ObtenerDatos);
        
    },
    Validar: function() {
        $("#frm-producto-mantenimiento")
            .bootstrapValidator({
                fields: {
                    "txt-nombre": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar nombre.",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-_ñÑ .]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
                            }
                        }
                    },
                    "txt-codigo": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar código de producto."
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-_ñÑ .]+$/,
                                message: 'Solo puede ingresar caracteres alfanumericos.'
                            }
                        }
                    },
                    "txt-codigo-sunat": {
                        validators: {
                            stringLength: {
                                min: 8,
                                max: 8,
                                message: "Código Sunat consta de 8 dígitos."
                            },
                            notEmpty: {
                                message: "Debe de ingresar código sunat."
                            }
                        }
                    },
                    "txt-stock-minimo": {
                        validators: {
                            notEmpty: {
                                message: "Debe de ingresar stock minimo."
                            },
                            regexp: {
                                regexp: /^[0-9]+([.][0-9]+)?$/,
                                message: 'Solo se aceptan números.'
                            }
                        }
                    },
                    "txt-monto": {
                        validators: {
                            notEmpty: {
                                message: "Debe de ingresar monto."
                            },
                            regexp: {
                                regexp: /^[0-9]+([.][0-9]+)?$/,
                                message: 'Solo se aceptan números.'
                            }
                        }
                    },

                }
            })
            .on('success.form.bv', function(e) {
                e.preventDefault();
                pageMantenimientoProducto.EnviarFormulario();
            });
    },

    ObtenerDatos: async function() {
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

    CargarCombo: async function(fn = null) {
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let promises = [
            fetch(`${urlRoot}api/categoriaproducto/listar-categoriaproducto?empresaId=${empresaId}`),
            fetch(`${urlRoot}api/tipoafectacionigv/listar-tipoafectacionigv`),
            fetch(`${urlRoot}api/unidadmedida/listar-unidadmedida-por-empresa?empresaId=${empresaId}`),
            fetch(`${urlRoot}api/tipoproducto/listar-tipoproducto`)
        ]
        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([CategoriaProductoLista, TipoAfectacionLista, UnidadMedidaLista, tipoProductoLista]) => {
                CategoriaProductoLista = CategoriaProductoLista;
                TipoAfectacionLista = TipoAfectacionLista;
                UnidadMedidaLista = UnidadMedidaLista;
                TipoProductoLista = tipoProductoLista;

                pageMantenimientoProducto.ResponseCategoriaProductoListar(CategoriaProductoLista);
                pageMantenimientoProducto.ResponseTipoAfectacionIgvListar(TipoAfectacionLista);
                pageMantenimientoProducto.ResponseUnidadMedidaListar(UnidadMedidaLista);
                pageMantenimientoProducto.ResponseTipoCalculoListar(TipoCalculo);
                pageMantenimientoProducto.ResponseTipoProductoListar(TipoProductoLista);
                if (typeof fn == 'function') fn();
                //$("#cmb-categoria").val(1).trigger('change');
            })
    },

    EnviarFormulario: function() {
        let productoId = $("#txt-opcion").val();
        let nombre = $("#txt-nombre").val();
        let categoriaId = $("#cmb-categoria").val();
        let tipoAfectacionIgvId = $("#cmb-tipo-afectacion").val();
        let unidadMedidaId = $("#cmb-unidad-medida").val();
        let tipoProductoId = $("#cmb-tipo-producto").val();
        let codigoSunat = $("#txt-codigo-sunat").val();
        let codigo = $("#txt-codigo").val();
        let tipoCalculo = $("#cmb-tipo-calculo").val();
        let monto = $("#txt-monto").val();
        let stockMinimo = $("#txt-stock-minimo").val();
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            EmpresaId: empresaId,
            ProductoId: productoId,
            CategoriaId: categoriaId,
            Nombre: nombre,
            TipoAfectacionIgvId: tipoAfectacionIgvId,
            UnidadMedidaId: unidadMedidaId,
            TipoProducto: tipoProductoId,
            TipoCalculo: tipoCalculo,
            Monto: monto,
            StockMinimo: stockMinimo,
            Usuario: user,
            TipoProductoId: tipoProductoId,
            CodigoSunat: codigoSunat,
            Codigo: codigo
        }

        let url = `${urlRoot}api/producto/guardar-producto`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoProducto.ResponseEnviarFormulario);
    },

    ResponseEnviarFormulario: function(data) {
        let tipo = "",
            mensaje = "";
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
            onHide: function() {
                if (data == true) {
                    location.href = `${urlRoot}Productos`
                }
            }
        });
    },

    ResponseCategoriaProductoListar: function(data) {
        let datacategoriaproducto = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.CategoriaProductoId, text: item.Nombre }); });
        $("#cmb-categoria").select2({ data: datacategoriaproducto, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    ResponseTipoAfectacionIgvListar: function(data) {
        let datatipoafectacionigv = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.TipoAfectacionIgvId, text: item.Descripcion }); });
        $("#cmb-tipo-afectacion").select2({ data: datatipoafectacionigv, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    ResponseUnidadMedidaListar: function (data) {
        $("#cmb-unidad-medida").empty();
        let dataunidadmedida = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.UnidadMedidaId, text: item.Descripcion }); });
        $("#cmb-unidad-medida").select2({ data: dataunidadmedida, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    ResponseTipoCalculoListar: function(data) {
        let datatipocalculo = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.Id, text: item.Descripcion }); });
        $("#cmb-tipo-calculo").select2({ data: datatipocalculo, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    ResponseTipoProductoListar: function(data) {
        let datatipoproducto = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.TipoProductoId, text: item.Nombre }); });
        $("#cmb-tipo-producto").select2({ data: datatipoproducto, width: '100%', placeholder: '[SELECCIONE...]' });
    },
    ResponseObtenerDatos: function (data) {
        $("#txt-nombre").val(data.Nombre);
        $("#cmb-categoria").val(data.CategoriaId).trigger('change');
        $("#txt-codigo").val(data.Codigo);
        $("#txt-nombre").val(data.Nombre);
        $("#txt-stock-minimo").val(data.StockMinimo);
        $("#cmb-tipo-producto").val(data.TipoProductoId).trigger('change');
        $("#cmb-unidad-medida").val(data.UnidadMedidaId).trigger('change');
        $("#txt-codigo-sunat").val(data.CodigoSunat);
        $("#cmb-tipo-afectacion").val(data.TipoAfectacionIgvId).trigger('change');
        $("#cmb-tipo-calculo").val(data.TipoCalculo).trigger('change');
        $("#txt-monto").val(data.Monto);

    },
}