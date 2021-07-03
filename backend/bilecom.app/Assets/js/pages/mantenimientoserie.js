const pageMantenimientoSerie = {
    Init: function () {
        this.CargarCombo(() => {
            this.InitEvents();
            this.Validar();
        });
        
    },

    InitEvents: function () {
        pageMantenimientoSerie.ObtenerDatos();
        $("#cmb-tipo-comprobante").change();
    },

    Validar: function () {
        $("#frm-serie")
            .bootstrapValidator({
                fields: {
                    "cmb-tipo-comprobante": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Categoria.",
                            }
                        }
                    },
                    "txt-serial": {
                        validators: {
                            stringLength: {
                                min: 3,
                                max: 3,
                                message: "Máximo 3 dígitos."
                            },
                            notEmpty: {
                                message: "Debe ingresar Serial.",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9]/,
                                message: 'Solo puede ingresar caracteres alfanumericos'
                            }
                        }
                    },
                    "txt-valor-inicial": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Valor Inicial.",
                            },
                            regexp: {
                                regexp: /^[0-9]/,
                                message: 'Solo puede ingresar caracteres numericos'
                            }
                            
                        }
                    },
                    "cmb-tipo-comprobante-referencia": {
                        validators: {
                            callback: {
                                message: "Debe seleccionar tipo de comprobante de referencia.",
                                callback: function (value, validator, $field) {
                                    let l = $("#cmb-tipo-comprobante").val();
                                    if (l == 3 || l==4) {
                                        if ($("#cmb-tipo-comprobante-referencia").val().trim() == "") {
                                            return false;
                                        }
                                        else { return true; }
                                    }
                                    else { return true; }
                                }
                            }
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoSerie.EnviarFormulario();
            });
    },

    ObtenerDatos: function () {
        let numero = $("#txt-opcion").val();
        if (numero != 0) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/serie/obtener-serie?empresaId=${empresaId}&serieId=${numero}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoSerie.ResponseObtenerDatos);
        }
    },

    CargarCombo: async function (fn=null) {
        let promises = [
            fetch(`${urlRoot}api/tipocomprobante/listar-tipocomprobante`)]
        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([TipoComprobanteLista]) => {
                pageMantenimientoSerie.ResponseTipoComprobanteListar(TipoComprobanteLista || []);
                pageMantenimientoSerie.ResponseTipoComprobanteReferenciaListar(TipoComprobanteLista || []);
                if (typeof fn == 'function') fn();
            })
    },

    EnviarFormulario: function () {
        let serieId = $("#txt-opcion").val();
        let tipocomprobanteId = $("#cmb-tipo-comprobante").val();
        let serial = $("#txt-identificador").val().trim()+""+$("#txt-serial").val().trim();
        let valorinicial = $("#txt-valor-inicial").val();
        let valorfinal = $("#txt-valor-final").val();
        let valoractual = $("#txt-valor-actual").val();
        let flagSinFinal = $("#chk-flag-sin-final").val();
        flagSinFinal = Boolean(flagSinFinal);
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;
        let tipocomprobantereferenciaId = $("#cmb-tipo-comprobante-referencia").val();

        let ObjectoJson = {
            SerieId: serieId,
            EmpresaId: empresaId,
            AmbienteSunatId: ambienteSunatId,
            TipoComprobanteId: tipocomprobanteId,
            Serial: serial,
            ValorInicial: valorinicial,
            ValorFinal: valorfinal,
            ValorActual: valoractual,
            FlagSinFinal: flagSinFinal,
            Usuario: user,
            TipoComprobanteReferenciaId: tipocomprobantereferenciaId
        }

        let url = `${urlRoot}api/serie/guardar-serie`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoSerie.ResponseEnviarFormulario);
    },

    ResponseObtenerDatos: function (data) {
        $("#cmb-tipo-comprobante").val(data.TipoComprobanteId).trigger("change");
        $("#txt-serial").val(data.Serial);
        $("#txt-valor-inicial").val(data.ValorInicial);
        $("#chk-flag-sin-final").prop("checked",data.FlagSinFinal);
        $("#txt-valor-final").val(data.ValorFinal);
        $("#txt-valor-actual").val(data.ValorActual);
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
                    location.href = `${urlRoot}Series`
                }
            }
        });
    },

    ResponseTipoComprobanteListar: function (data) {
        $("#cmb-tipo-comprobante").empty();
        let datatipocomprobante = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.TipoComprobanteId, text: item.Nombre }); });
        $("#cmb-tipo-comprobante").select2({ data: datatipocomprobante, width: '100%', placeholder: '[TODOS...]' });
    },
    ResponseTipoComprobanteReferenciaListar: function (data) {
        $("#cmb-tipo-comprobante-referencia").empty();
        data = data.filter(x => x.TipoComprobanteId == 1 || x.TipoComprobanteId == 2);
        let datatipocomprobantereferencia = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.TipoComprobanteId, text: item.Nombre }); });
        $("#cmb-tipo-comprobante-referencia").select2({ data: datatipocomprobantereferencia, width: '100%', placeholder: '[TODOS...]' });
    },
    CmbChangeTipoComprobante: function () {
        let valorTipoComprobante = $("#cmb-tipo-comprobante").val();
        if (valorTipoComprobante == 3 || valorTipoComprobante == 4) {
            $("#namegruporeferencia").show();
            $("#cmb-tipo-comprobante-referencia").val(1);
            $("#cmb-tipo-comprobante-referencia").change();
        }else
        {
            $("#cmb-tipo-comprobante-referencia").val("");
            $("#namegruporeferencia").hide();
        }
    }

}

$("#cmb-tipo-comprobante").change(function () {
    let data = $("#cmb-tipo-comprobante").select2('data');
    $("#txt-identificador").val(data[0].IdentificadorSerie);
    pageMantenimientoSerie.CmbChangeTipoComprobante();
})

$("#cmb-tipo-comprobante-referencia").change(function () {
    let data = $("#cmb-tipo-comprobante-referencia").select2('data');
    $("#txt-identificador").val(data[0].IdentificadorSerie);
})