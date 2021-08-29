var paisLista = [], departamentoLista = [], provinciaLista = [], distritoLista = [], monedaLista = [], tipoAfectacionIgvLista = [], tipoComprobanteTipoOperacionVentaLista = [], tipoProductoLista = [], unidadMedidaLista = [], formatoLista = [];
const pageConfiguracion = {
    Init: function () {
        common.ArrayExtensions(["distinct"]);
        this.Validar();
        this.InitEvents();
    },

    InitEvents: function () {
        pageConfiguracion.InitDropZone('#fle-imagen-logo-container', 'fle-imagen-logo');
        pageConfiguracion.InitDropZone('#fle-imagen-logo-formato-container', 'fle-imagen-logo-formato');
        //pageConfiguracion.CargarCombo(pageConfiguracion.ObtenerDatos());
        pageConfiguracion.CargarCombo(pageConfiguracion.ObtenerDatos);
        $("#cmb-pais").change(pageConfiguracion.CmbPaisChange)
        $("#cmb-departamento").change(pageConfiguracion.CmbDepartamentoChange);
        $("#cmb-provincia").change(pageConfiguracion.CmbProvinciaChange);
    },

    InitDropZone: function (idContainer, nameFile, objDropZone, options = {}) {
        $(idContainer).dropzone({
            url: options.url || urlRoot,
            autoProcessQueue: options.autoProcessQueue || false,
            autoDiscover: options.autoDiscover || false,
            addRemoveLinks: options.addRemoveLinks || true,
            maxFiles: options.maxFiles || 1,
            paramName: nameFile,
            init: function () {
                //var myDropzone = ;
                this.on('maxfilesexceeded', function (file) {
                    this.removeAllFiles();
                    this.addFile(file);
                });
            }
        });
    },

    Validar: function () {
        $("#frm-configuracion")
            .bootstrapValidator({
                fields: {
                    //"cmb-tipo-documento-identidad": {
                    //    validators: {
                    //        notEmpty: {
                    //            message: "Debe seleccionar tipo de documento de identidad",
                    //        }
                    //    }
                    //},
                    //"cmb-departamento": {
                    //    validators: {
                    //        notEmpty: {
                    //            message: "Debe seleccionar departamento",
                    //        }
                    //    }
                    //},
                    //"cmb-provincia": {
                    //    validators: {
                    //        notEmpty: {
                    //            message: "Debe seleccionar provincia",
                    //        }
                    //    }
                    //},
                    //"cmb-distrito": {
                    //    validators: {
                    //        notEmpty: {
                    //            message: "Debe seleccionar distrito",
                    //        }
                    //    }
                    //},
                    //"txt-nombres": {
                    //    validators: {
                    //        notEmpty: {
                    //            message: "Debe ingresar nombres o razón social",
                    //        },
                    //        callback: {
                    //            message: 'El nombre o razón social no es válido',
                    //            callback: function (value, validator, $field) {
                    //                let tipoDocumentoIdentidad = $("#cmb-tipo-documento-identidad").select2('data')[0];
                    //                if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiDNI) {
                    //                    if (value === "") {
                    //                        return true;
                    //                    }
                    //                    if (!(/^[a-zA-ZñÑá-úÁ-Ú ]*$/).test(value)) {
                    //                        return {
                    //                            valid: false,
                    //                            message: "Nombre o razón social inválido"
                    //                        }
                    //                    }
                    //                }
                    //                if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiRUC) {
                    //                    if (value === "") {
                    //                        return true;
                    //                    }
                    //                    if (!(/^[a-zA-Z0-9ñÑá-úÁ-Ú ]+$/)) {
                    //                        return {
                    //                            valid: false,
                    //                            message: "Nombre o razón social inválido"
                    //                        }
                    //                    }
                    //                }
                    //                return true;
                    //            }
                    //        },

                    //    }
                    //},
                    //"txt-nombre-comercial": {
                    //    validators: {
                    //        notEmpty: {
                    //            message: "Debe ingresar nombre comercial",
                    //        },
                    //        regexp: {
                    //            regexp: /^[a-zA-Z0-9-ñÑá-úÁ-Ú ]+$/,
                    //            message: 'Solo puede ingresar caracteres alfabéticos'
                    //        }
                    //    }
                    //},
                    //"txt-numero-documento-identidad": {
                    //    validators: {
                    //        notEmpty: {
                    //            message: "Debe ingresar un número de documento de identidad",
                    //        },
                    //        callback: {
                    //            message: 'El número de documento de identidad no es válido',
                    //            callback: function (value, validator, $field) {
                    //                let tipoDocumentoIdentidad = $("#cmb-tipo-documento-identidad").select2('data')[0];
                    //                if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiDNI) {
                    //                    if (value === "") {
                    //                        return true;
                    //                    }
                    //                    if (value.length != 8) {
                    //                        return {
                    //                            valid: false,
                    //                            message: "DNI inválido"
                    //                        }
                    //                    }
                    //                    else {
                    //                        if (!(/^[0-9]*$/).test(value)) {
                    //                            return {
                    //                                valid: false,
                    //                                message: "DNI inválido"
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //                if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiRUC) {
                    //                    if (value === "") {
                    //                        return true;
                    //                    }
                    //                    if (value.length != 11) {
                    //                        return {
                    //                            valid: false,
                    //                            message: "RUC inválido"
                    //                        }
                    //                    }
                    //                    else {
                    //                        if (!(/^[0-9]*$/).test(value)) {
                    //                            return {
                    //                                valid: false,
                    //                                message: "RUC inválido"
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //                if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiCE) {
                    //                    if (value === "") {
                    //                        return true;
                    //                    }
                    //                    if (value.length != 11) {
                    //                        return {
                    //                            valid: false,
                    //                            message: "Carnet de Extranjería inválido"
                    //                        }
                    //                    }
                    //                    else {
                    //                        if (!(/^[0-9]*$/).test(value)) {
                    //                            return {
                    //                                valid: false,
                    //                                message: "Carnet de Extranjería inválido"
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //                if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiPasaporte) {
                    //                    if (value === "") {
                    //                        return true;
                    //                    }
                    //                    if (value.length != 7) {
                    //                        return {
                    //                            valid: false,
                    //                            message: "Pasaporte inválido"
                    //                        }
                    //                    }
                    //                    else {
                    //                        if (!(/^[0-9]*$/).test(value)) {
                    //                            return {
                    //                                valid: false,
                    //                                message: "Pasaporte inválido"
                    //                            }
                    //                        }
                    //                    }
                    //                }
                    //                return true;
                    //            }
                    //        }
                    //    }
                    //},
                    //"txt-correo": {
                    //    validators: {
                    //        notEmpty: {
                    //            message: "Debe ingresar correo válido",
                    //        },
                    //        emailAddress: {
                    //            message: 'La dirección de correo no es válido'
                    //        }
                    //    }
                    //},
                    //"txt-direccion": {
                    //    validators: {
                    //        notEmpty: {
                    //            message: "Debe ingresar direccion",
                    //        },
                    //        regexp: {
                    //            regexp: /^[a-zA-Z0-9-_ñÑ .]+$/,
                    //            message: 'Solo puede ingresar caracteres alfabéticos'
                    //        },
                    //        stringLength: {
                    //            min: 5,
                    //            message: 'Dirección no válida'
                    //        }
                    //    }
                    //}
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageConfiguracion.EnviarFormulario();
            });
    },

    CmbPaisChange: function () {
        let paisId = $("#cmb-pais").val();
        let DepartamentoFiltro = departamentoLista.filter(x => x.PaisId == paisId);
        pageConfiguracion.ResponseDepartamentoListar(DepartamentoFiltro);
    },

    CmbDepartamentoChange: function () {
        let departamentoId = $("#cmb-departamento").val();
        let ProvinciaFiltro = provinciaLista.filter(x => x.DepartamentoId == departamentoId);
        pageConfiguracion.ResponseProvinciaListar(ProvinciaFiltro);
    },

    CmbProvinciaChange: function () {
        let provinciaId = $("#cmb-provincia").val();
        let DistritoFiltro = distritoLista.filter(x => x.ProvinciaId == provinciaId);
        pageConfiguracion.ResponseDistritoListar(DistritoFiltro);
    },

    CmbListaMonedaChange: function () {
        let data = $("#cmb-lista-moneda").select2("data").map(x => { x.selected = false; return x; });

        $("#cmb-moneda-default").empty();
        $("#cmb-moneda-default").select2({ data: data, width: '100%', placeholder: '[SELECCIONE...]'})

    },

    CmbListaTipoAfectacionIgvChange: function () {
        let data = $("#cmb-lista-tipo-afectacion-igv").select2("data").map(x => { x.selected = false; return x; });

        $("#cmb-tipo-afectacion-igv-default").empty();
        $("#cmb-tipo-afectacion-igv-default").select2({ data: data, width: '100%', placeholder: '[SELECCIONE...]' })

    },

    CmbListaTipoOperacionVentaChange: function () {
        let idsSelected = $("#cmb-lista-tipo-operacion-venta").select2("data").map(x => x.id);
        let dataBase = tipoComprobanteTipoOperacionVentaLista.filter(x => idsSelected.some(y => y == `${x.TipoComprobanteId}|${x.TipoOperacionVentaId}`));
        //let data = $("#cmb-lista-tipo-operacion-venta").select2("data").map(x => { x.selected = false; return x; });
        let data = dataBase.map(x => Object.assign({}, { ...x.TipoComprobante })).distinct(["TipoComprobanteId"]).map(x => Object.assign({}, { ...x, text: x.Nombre, element: HTMLOptGroupElement }));
        //let dataTipoComprobante = data.map(x => Object.assign({}, { ...x.TipoComprobante })).distinct(["TipoComprobanteId"]).map(x => Object.assign({}, { ...x, text: x.Nombre, element: HTMLOptGroupElement }));
        let dataTipoComprobante = data.distinct(["TipoComprobanteId"]).map(x => Object.assign({}, { ...x, text: x.Nombre, element: HTMLOptGroupElement }));
        if (dataTipoComprobante != null) {
            dataTipoComprobante.forEach(x => x.children = dataBase.filter(y => y.TipoComprobanteId == x.TipoComprobanteId).map(y => Object.assign({}, { ...y, id: `${x.TipoComprobanteId}|${y.TipoOperacionVentaId}`, text: `${x.Nombre} - ${y.TipoOperacionVenta.Nombre}`, element: HTMLOptionElement })));
        }

        $("#cmb-tipo-operacion-venta-default").empty();
        $("#cmb-tipo-operacion-venta-default").select2({ data: dataTipoComprobante, width: '100%', placeholder: '[SELECCIONE...]' });

    },

    CmbListaTipoProductoChange: function () {
        let data = $("#cmb-lista-tipo-producto").select2("data").map(x => { x.selected = false; return x; });

        $("#cmb-tipo-producto-default").empty();
        $("#cmb-tipo-producto-default").select2({ data: data, width: '100%', placeholder: '[SELECCIONE...]' })

    },

    CmbListaUnidadMedidaChange: function () {
        let data = $("#cmb-lista-unidad-medida").select2("data").map(x => { x.selected = false; return x; });

        $("#cmb-unidad-medida-default").empty();
        $("#cmb-unidad-medida-default").select2({ data: data, width: '100%', placeholder: '[SELECCIONE...]' })

    },

    CargarCombo: async function (fn = null) {
        let promises = [
            fetch(`${urlRoot}api/pais/listar-pais`),
            fetch(`${urlRoot}api/departamento/listar-departamento`),
            fetch(`${urlRoot}api/provincia/listar-provincia`),
            fetch(`${urlRoot}api/distrito/listar-distrito`),
            fetch(`${urlRoot}api/tipodocumentoidentidad/listar-tipodocumentoidentidad`),
            fetch(`${urlRoot}api/moneda/listar-moneda`),
            fetch(`${urlRoot}api/tipoafectacionigv/listar-tipoafectacionigv`),
            fetch(`${urlRoot}api/tipocomprobantetipooperacionventa/listar-tipocomprobantetipooperacionventa?withTipoComprobante=true&withTipoOperacionVenta=true`),
            fetch(`${urlRoot}api/tipoproducto/listar-tipoproducto`),
            fetch(`${urlRoot}api/unidadmedida/listar-unidadmedida`),
            fetch(`${urlRoot}api/formato/listar-formato?withTipoComprobante=true`)]
        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([PaisLista, DepartamentoLista, ProvinciaLista, DistritoLista, TipoDocumentoIdentidadLista, MonedaLista, TipoAfectacionIgvLista, TipoComprobanteTipoOperacionVentaLista, TipoProductoLista, UnidadMedidaLista, FormatoLista]) => {
                paisLista = PaisLista;
                departamentoLista = DepartamentoLista;
                provinciaLista = ProvinciaLista;
                distritoLista = DistritoLista;
                monedaLista = MonedaLista;
                tipoAfectacionIgvLista = TipoAfectacionIgvLista;
                tipoComprobanteTipoOperacionVentaLista = TipoComprobanteTipoOperacionVentaLista;
                tipoProductoLista = TipoProductoLista;
                unidadMedidaLista = UnidadMedidaLista;
                formatoLista = FormatoLista;

                $("#cmb-moneda-default").select2({ data: [], width: '100%', placeholder: '[SELECCIONE...]' });
                $("#cmb-tipo-afectacion-igv-default").select2({ data: [], width: '100%', placeholder: '[SELECCIONE...]' });
                $("#cmb-tipo-operacion-venta-default").select2({ data: [], width: '100%', placeholder: '[SELECCIONE...]' });
                $("#cmb-tipo-producto-default").select2({ data: [], width: '100%', placeholder: '[SELECCIONE...]' });
                $("#cmb-unidad-medida-default").select2({ data: [], width: '100%', placeholder: '[SELECCIONE...]' });

                pageConfiguracion.ResponsePaisListar(PaisLista);
                pageConfiguracion.ResponseDepartamentoListar(DepartamentoLista);
                pageConfiguracion.ResponseProvinciaListar(ProvinciaLista);
                pageConfiguracion.ResponseDistritoListar(DistritoLista);
                pageConfiguracion.ResponseTipoDocumentoIdentidadListar(TipoDocumentoIdentidadLista);
                pageConfiguracion.ResponseMonedaListar(MonedaLista);
                pageConfiguracion.ResponseTipoAfectacionIgvListar(TipoAfectacionIgvLista);
                pageConfiguracion.ResponseTipoOperacionVentaListar(TipoComprobanteTipoOperacionVentaLista);
                pageConfiguracion.ResponseTipoProductoListar(TipoProductoLista);
                pageConfiguracion.ResponseUnidadMedidaListar(unidadMedidaLista);
                pageConfiguracion.ResponseFormatoListar(formatoLista);


                if (typeof fn == 'function') fn();
            })
    },

    ObtenerDatos: function () {
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

        let url = `${urlRoot}api/empresa/obtener-empresa?empresaId=${empresaId}&withUbigeo=true&withEmpresaConfiguracion=true&withListaMoneda=true&withListaTipoAfectacionIgv=true&withListaTipoComprobanteTipoOperacionVenta=true&withListaTipoProducto=true&withListaUnidadMedida=true&withLogo=true&withLogoFormato=true`;
        let init = { method: 'GET' };

        fetch(url, init)
            .then(r => r.json())
            .then(pageConfiguracion.ResponseObtenerDatos);
    },

    EnviarFormulario: function () {
        let imageLogoBlob = Dropzone.forElement("#fle-imagen-logo-container").files[0];
        let imageLogoFile = new File([imageLogoBlob], imageLogoBlob.name, { type: imageLogoBlob.type });

        let imageLogoFormatoBlob = Dropzone.forElement("#fle-imagen-logo-formato-container").files[0];
        let imageLogoFormatoFile = new File([imageLogoFormatoBlob], imageLogoFormatoBlob.name, { type: imageLogoFormatoBlob.type});

        // DATOS DE LA EMPRESA
        let nombreComercialEmpresa = $("#txt-nombre-comercial").val();

        // DATOS PREDETERMINADOS
        let listaMonedaIds = $("#cmb-lista-moneda").val();
        let monedaIdPorDefecto = $("#cmb-moneda-default").val();
        let listaTipoAfectacionIgvIds = $("#cmb-lista-tipo-afectacion-igv").val();
        let tipoAfectacionIgvPorDefecto = $("#cmb-tipo-afectacion-igv-default").val();
        let listaTipoOperacionVentaIds = $("#cmb-lista-tipo-operacion-venta").val();
        let tipoOperacionVentaIdsPorDefecto = $("#cmb-tipo-operacion-venta-default").val();
        let listaTipoProductoIds = $("#cmb-lista-tipo-producto").val();
        let tipoProductoIdPorDefecto = $("#cmb-tipo-producto-default").val();
        let listaUnidadMedidaIds = $("#cmb-lista-unidad-medida").val();
        let unidadMedidaIdPorDefecto = $("#cmb-unidad-medida-default").val();

        // DATOS SUNAT
        let cuentaCorriente = $("#txt-cuenta-corriente").val();
        let comentarioLegal = $("#txt-comentario-legal").val();
        let comentarioLegalDetraccion = $("#txt-comentario-legal-detraccion").val();
        let formatoIds = $("#cmb-formato").val();
        let cantidadDecimalGeneral = $("#txt-cantidad-decimal-general").val();
        let cantidadDecimalDetallado = $("#txt-cantidad-decimal-detallado").val();


        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let formData = new FormData();
        formData.append("EmpresaId", empresaId);
        formData.append("Empresa.NombreComercial", nombreComercialEmpresa);
        formData.append("Empresa.EmpresaImagen.LogoFile", imageLogoFile);
        formData.append("Empresa.EmpresaImagen.LogoFormatoFile", imageLogoFormatoFile);
        formData.append("ListaMoneda", listaMonedaIds);
        formData.append("MonedaIdPorDefecto", monedaIdPorDefecto);
        formData.append("ListaTipoAfectacionIgv", listaTipoAfectacionIgvIds);
        formData.append("TipoAfectacionIgvIdPorDefecto", tipoAfectacionIgvPorDefecto);
        formData.append("ListaTipoComprobanteTipoOperacionVenta", listaTipoOperacionVentaIds);
        formData.append("TipoComprobanteTipoOperacionVentaIdsPorDefecto", tipoOperacionVentaIdsPorDefecto);
        formData.append("ListaTipoProducto", listaTipoProductoIds);
        formData.append("TipoProductoIdPorDefecto", tipoProductoIdPorDefecto);
        formData.append("ListaUnidadMedida", listaUnidadMedidaIds);
        formData.append("UnidadMedidaIdPorDefecto", unidadMedidaIdPorDefecto);
        formData.append("CuentaCorriente", cuentaCorriente);
        formData.append("ComentarioLegal", comentarioLegal);
        formData.append("ComentarioLegalDetraccion", comentarioLegalDetraccion);
        formData.append("FormatoIds", formatoIds);
        formData.append("CantidadDecimalGeneral", cantidadDecimalGeneral);
        formData.append("CantidadDecimalDetallado", cantidadDecimalDetallado);

        let url = `${urlRoot}api/empresaconfiguracion/guardar-empresaconfiguracion`;
        let params = formData;
        let init = { method: 'POST', body: params };

        fetch(url, init)
            .then(r => r.json())
            .then(pageConfiguracion.ResponseEnviarFormulario);
    },

    ResponseEnviarFormulario: function (data) {
        let tipo = "", mensaje = "";
        if (data > 0) {
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
                    location.href = `${urlRoot}Configuracion`
                }
            }
        });
    },

    dataURItoBlob: function (dataURI) {
        'use strict'
        var byteString,
            mimestring

        if (dataURI.split(',')[0].indexOf('base64') !== -1) {
            byteString = atob(dataURI.split(',')[1])
        } else {
            byteString = decodeURI(dataURI.split(',')[1])
        }

        mimestring = dataURI.split(',')[0].split(':')[1].split(';')[0]

        var content = new Array();
        for (var i = 0; i < byteString.length; i++) {
            content[i] = byteString.charCodeAt(i)
        }

        return new Blob([new Uint8Array(content)], { type: mimestring });
    },

    ResponseObtenerDatos: function (data) {
        // DATOS DE EMPRESA
        $("#txt-ruc").val(data.Ruc);
        $("#txt-razon-social").val(data.RazonSocial);
        $("#txt-nombre-comercial").val(data.NombreComercial);
        $("#cmb-pais").val(data.Distrito.Provincia.Departamento.PaisId).trigger("change");
        $("#cmb-departamento").val(data.Distrito.Provincia.DepartamentoId).trigger("change");
        $("#cmb-provincia").val(data.Distrito.ProvinciaId).trigger("change");
        $("#cmb-distrito").val(data.DistritoId).trigger("change");
        $("#txt-direccion").val(data.Direccion);
        //let extensionLogo = data.EmpresaImagen.LogoTipoContenido.split('/')[1];
        let logoBase64 = `data:${data.EmpresaImagen.LogoTipoContenido};base64,${data.EmpresaImagen.Logo}`;
        let blob = pageConfiguracion.dataURItoBlob(logoBase64);
        blob.name = data.EmpresaImagen.LogoNombre;
        blob.contentType = data.EmpresaImagen.LogoTipoContenido;
        let fileLogoDropZone = Dropzone.forElement("#fle-imagen-logo-container");
        fileLogoDropZone.addFile(blob);
        fileLogoDropZone.emit("complete", blob);
        //this.emit("complete", file);
        //$(this.previewsContainer).find('.dz-progress').hide();

        // DATOS POR DEFECTO
        if (data.EmpresaConfiguracion != null) {
            $("#cmb-lista-moneda").val((data.EmpresaConfiguracion.ListaMoneda || []).map(x => x.MonedaId)).trigger("change");
            $("#cmb-lista-tipo-afectacion-igv").val((data.EmpresaConfiguracion.ListaTipoAfectacionIgv || []).map(x => x.TipoAfectacionIgvId)).trigger("change");
            $("#cmb-lista-tipo-operacion-venta").val((data.EmpresaConfiguracion.ListaTipoComprobanteTipoOperacionVenta || []).map(x => `${x.TipoComprobanteId}|${x.TipoOperacionVentaId}`)).trigger("change");
            $("#cmb-lista-tipo-producto").val((data.EmpresaConfiguracion.ListaTipoProducto || []).map(x => x.TipoProductoId)).trigger("change");
            $("#cmb-lista-unidad-medida").val((data.EmpresaConfiguracion.ListaUnidadMedida || []).map(x => x.UnidadMedidaId)).trigger("change");

            $("#cmb-moneda-default").val(data.EmpresaConfiguracion.MonedaIdPorDefecto).trigger("change");
            $("#cmb-tipo-afectacion-igv-default").val(data.EmpresaConfiguracion.TipoAfectacionIgvIdPorDefecto).trigger("change");
            $("#cmb-tipo-operacion-venta-default").val(data.EmpresaConfiguracion.ListaTipoComprobanteTipoOperacionVentaIdPorDefecto).trigger("change");
            $("#cmb-tipo-producto-default").val(data.EmpresaConfiguracion.TipoProductoIdPorDefecto).trigger("change");
            $("#cmb-unidad-medida-default").val(data.EmpresaConfiguracion.UnidadMedidaIdPorDefecto).trigger("change");
            $("#txt-cuenta-corriente").val(data.EmpresaConfiguracion.CuentaCorriente);
            $("#txt-comentario-legal").val(data.EmpresaConfiguracion.ComentarioLegal);
            $("#txt-comentario-legal-detraccion").val(data.EmpresaConfiguracion.ComentarioLegalDetraccion);
            $("#cmb-formato").val(data.EmpresaConfiguracion.ListaFormatoId).trigger("change");
            $("#txt-cantidad-decimal-general").val(data.EmpresaConfiguracion.CantidadDecimalGeneral);
            $("#txt-cantidad-decimal-detallado").val(data.EmpresaConfiguracion.CantidadDecimalDetallado);
        }
        //let extensionLogoFormato = data.EmpresaImagen.LogoFormatoTipoContenido.split('/')[1];
        let logoFormatoBase64 = `data:${data.EmpresaImagen.LogoFormatoTipoContenido};base64,${data.EmpresaImagen.LogoFormato}`;
        let blobFormato = pageConfiguracion.dataURItoBlob(logoFormatoBase64);
        blobFormato.name = data.EmpresaImagen.LogoFormatoNombre;
        blobFormato.contentType = data.EmpresaImagen.LogoFormatoTipoContenido;
        let fileLogoFormatoDropZone = Dropzone.forElement("#fle-imagen-logo-formato-container");
        fileLogoFormatoDropZone.addFile(blobFormato);
        fileLogoFormatoDropZone.emit("complete", blobFormato);

        //$("#cmb-tipo-documento-identidad").val(data.TipoDocumentoIdentidadId).trigger("change");
        //$("#cmb-distrito").val(data.DistritoId).trigger("change");

        //$("#cmb-tipo-documento-identidad").prop("disabled", true);
        //$("#txt-numero-documento-identidad").prop("disabled", true);
    },

    ResponsePaisListar: function (data) {
        let datapais = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.PaisId, text: item.Nombre }); });
        $("#cmb-pais").select2({ data: datapais, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseDepartamentoListar: function (data) {
        $("#cmb-departamento").empty();
        let datadepartamento = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.DepartamentoId, text: item.Nombre }); });
        $("#cmb-departamento").select2({ data: datadepartamento, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseProvinciaListar: function (data) {
        $("#cmb-provincia").empty();
        let dataprovincia = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.ProvinciaId, text: item.Nombre }); });
        $("#cmb-provincia").select2({ data: dataprovincia, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseDistritoListar: function (data) {
        $("#cmb-distrito").empty();
        let datadistrito = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.DistritoId, text: item.Nombre }); });
        $("#cmb-distrito").select2({ data: datadistrito, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseTipoDocumentoIdentidadListar: function (data) {
        $("#cmb-tipo-documento-identidad").empty();
        let datatipodocumentoidentidad = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.TipoDocumentoIdentidadId, text: item.Descripcion }); });
        $("#cmb-tipo-documento-identidad").select2({ data: datatipodocumentoidentidad, width: '100%', placeholder: '[SELECCIONE...]' });

    },

    ResponseMonedaListar: function (data) {
        let dataMoneda = data.map(x => { return Object.assign(x, { id: x.MonedaId, text: x.Nombre }); });
        $("#cmb-lista-moneda").select2({ data: dataMoneda, width: '100%', placeholder: '[SELECCIONE...]' });
        $("#cmb-lista-moneda").change(pageConfiguracion.CmbListaMonedaChange);
    },

    ResponseTipoAfectacionIgvListar: function (data) {
        let dataTipoAfectacionIgv = data.map(x => { return Object.assign(x, { id: x.TipoAfectacionIgvId, text: x.Descripcion }); });
        $("#cmb-lista-tipo-afectacion-igv").select2({ data: dataTipoAfectacionIgv, width: '100%', placeholder: '[SELECCIONE...]' });
        $("#cmb-lista-tipo-afectacion-igv").change(pageConfiguracion.CmbListaTipoAfectacionIgvChange);
    },

    ResponseTipoOperacionVentaListar: function (data) {
        let dataTipoComprobante = data.map(x => Object.assign({}, { ...x.TipoComprobante })).distinct(["TipoComprobanteId"]).map(x => Object.assign({}, { ...x, text: x.Nombre, element: HTMLOptGroupElement }));
        if (dataTipoComprobante != null) {
            dataTipoComprobante.forEach(x => x.children = data.filter(y => y.TipoComprobanteId == x.TipoComprobanteId).map(y => Object.assign({}, { ...y, id: `${x.TipoComprobanteId}|${y.TipoOperacionVentaId}`, text: `${x.Nombre} - ${y.TipoOperacionVenta.Nombre}`, element: HTMLOptionElement })));
        }
        //let dataTipoOperacionVenta = data.map(x => { return Object.assign(x, { id: x.TipoAfectacionIgvId, text: x.Descripcion }); });
        $("#cmb-lista-tipo-operacion-venta").select2({ data: dataTipoComprobante, width: '100%', placeholder: '[SELECCIONE...]' });
        $("#cmb-lista-tipo-operacion-venta").change(pageConfiguracion.CmbListaTipoOperacionVentaChange)
    },

    ResponseTipoProductoListar: function (data) {
        let dataTipoProducto = data.map(x => { return Object.assign(x, { id: x.TipoProductoId, text: x.Nombre }); });
        $("#cmb-lista-tipo-producto").select2({ data: dataTipoProducto, width: '100%', placeholder: '[SELECCIONE...]' });
        $("#cmb-lista-tipo-producto").change(pageConfiguracion.CmbListaTipoProductoChange);
    },

    ResponseUnidadMedidaListar: function (data) {
        let dataUnidadMedida = data.map(x => { return Object.assign(x, { id: x.UnidadMedidaId, text: x.Descripcion }); });
        $("#cmb-lista-unidad-medida").select2({ data: dataUnidadMedida, width: '100%', placeholder: '[SELECCIONE...]' });
        $("#cmb-lista-unidad-medida").change(pageConfiguracion.CmbListaUnidadMedidaChange);
    },

    ResponseFormatoListar: function (data) {
        let dataTipoComprobante = data.map(x => Object.assign({}, { ...x.TipoComprobante })).distinct(["TipoComprobanteId"]).map(x => Object.assign({}, { ...x, text: x.Nombre, element: HTMLOptGroupElement }));
        if (dataTipoComprobante != null) {
            dataTipoComprobante.forEach(x => x.children = data.filter(y => y.TipoComprobanteId == x.TipoComprobanteId).map(y => Object.assign({}, { ...y, id: `${x.TipoComprobanteId}|${y.FormatoId}`, text: `${x.Nombre} - ${y.Nombre}`, element: HTMLOptionElement })));
        }
        //let dataTipoOperacionVenta = data.map(x => { return Object.assign(x, { id: x.TipoAfectacionIgvId, text: x.Descripcion }); });
        $("#cmb-formato").select2({ data: dataTipoComprobante, width: '100%', placeholder: '[SELECCIONE...]' });
        //$("#cmb-").change(pageConfiguracion.CmbListaTipoOperacionVentaChange)
    },

}