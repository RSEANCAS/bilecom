const pageMantenimientoProveedor = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {
        pageMantenimientoProveedor.CargarCombo();
        pageMantenimientoProveedor.ObtenerDatos();

        $("#cmb-pais").change(pageMantenimientoProveedor.CmbPaisChange)
        $("#cmb-departamento").change(pageMantenimientoProveedor.CmbDepartamentoChange);
        $("#cmb-provincia").change(pageMantenimientoProveedor.CmbProvinciaChange);
    },
    Validar: function () {
        $("#frm-proveedor-mantenimiento")
            .bootstrapValidator({
                fields: {
                    "txt-nombres": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar nombres o razón social",
                            },
                            callback: {
                                message: 'El nombre o razón social no es válido',
                                callback: function (value, validator, $field) {
                                    let tipoDocumentoIdentidad = $("#cmb-tipo-documento-identidad").select2('data')[0];
                                    if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiDNI) {
                                        if (value === "") {
                                            return true;
                                        }
                                        if (!(/^[a-zA-ZñÑá-úÁ-Ú ]*$/).test(value)) {
                                            return {
                                                valid: false,
                                                message: "Nombre o razón social inválido"
                                            }
                                        }
                                    }
                                    if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiRUC) {
                                        if (value === "") {
                                            return true;
                                        }
                                        if (!(/^[a-zA-Z0-9ñÑá-úÁ-Ú ]+$/)) {
                                            return {
                                                valid: false,
                                                message: "Nombre o razón social inválido"
                                            }
                                        }
                                    }
                                    return true;
                                }
                            },

                        }
                    },
                    "txt-numero-documento-identidad": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar un número de documento de identidad",
                            },
                            callback: {
                                message: 'El número de documento de identidad no es válido',
                                callback: function (value, validator, $field) {
                                    let tipoDocumentoIdentidad = $("#cmb-tipo-documento-identidad").select2('data')[0];
                                    if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiDNI) {
                                        if (value === "") {
                                            return true;
                                        }
                                        if (value.length != 8) {
                                            return {
                                                valid: false,
                                                message: "DNI inválido"
                                            }
                                        }
                                        else {
                                            if (!(/^[0-9]*$/).test(value)) {
                                                return {
                                                    valid: false,
                                                    message: "DNI inválido"
                                                }
                                            }
                                        }
                                    }
                                    if (tipoDocumentoIdentidad.CodigoTipoDocumentoIdentidad == tdiRUC) {
                                        if (value === "") {
                                            return true;
                                        }
                                        if (value.length != 11) {
                                            return {
                                                valid: false,
                                                message: "RUC inválido"
                                            }
                                        }
                                        else {
                                            if (!(/^[0-9]*$/).test(value)) {
                                                return {
                                                    valid: false,
                                                    message: "RUC inválido"
                                                }
                                            }
                                        }
                                    }
                                    return true;
                                }
                            }
                        }
                    },
                    "txt-nombre-comercial": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar nombre comercial",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-ñÑá-úÁ-Ú ]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
                            }
                        }
                    },
                    "txt-correo": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar correo válido",
                            },
                            emailAddress: {
                                message: 'La dirección de correo no es válido'
                            }
                        }
                    },
                    "txt-direccion": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar direccion",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-_ñÑ .]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
                            },
                            stringLength: {
                                min: 5,
                                message: 'Dirección no válida'
                            }
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoProveedor.EnviarFormulario();
            });
    },

    CargarCombo: async function () {
        let promises = [
            fetch(`${urlRoot}api/pais/listar-pais`),
            fetch(`${urlRoot}api/departamento/listar-departamento`),
            fetch(`${urlRoot}api/provincia/listar-provincia`),
            fetch(`${urlRoot}api/distrito/listar-distrito`),
            fetch(`${urlRoot}api/tipodocumentoidentidad/listar-tipodocumentoidentidad`)]
        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([PaisLista, DepartamentoLista, ProvinciaLista, DistritoLista, TipoDocumentoIdentidadLista]) => {
                paisLista = PaisLista;
                departamentoLista = DepartamentoLista;
                provinciaLista = ProvinciaLista;
                distritoLista = DistritoLista;

                pageMantenimientoProveedor.ResponsePaisListar(PaisLista);

                pageMantenimientoProveedor.ResponseDepartamentoListar(DepartamentoLista);
                pageMantenimientoProveedor.ResponseProvinciaListar(ProvinciaLista);
                pageMantenimientoProveedor.ResponseDistritoListar(DistritoLista);
                pageMantenimientoProveedor.ResponseTipoDocumentoIdentidadListar(TipoDocumentoIdentidadLista);

                $("#cmb-pais").val(145).trigger('change');
                $("#cmb-departamento").val(15).trigger('change');
                $("#cmb-provincia").val(128).trigger('change');


            })
    },

    ObtenerDatos: function () {
        let numero = $("#txt-opcion").val();
        if (numero != 0) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/proveedor/obtener-proveedor?empresaId=${empresaId}&proveedorId=${numero}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoProveedor.ResponseObtenerDatos);
        }
    },

    EnviarFormulario: function () {
        let proveedorId = $("#txt-opcion").val();
        let nombres = $("#txt-nombres").val();
        let nrodocumento = $("#txt-numero-documento-identidad").val();
        let nombrecomercial = $("#txt-nombre-comercial").val();
        let direccion = $("#txt-direccion").val();
        let correo = $("#txt-correo").val();
        let tipodocumentoidentidadId = $("#cmb-tipo-documento-identidad").val();
        let paisId = $("#cmb-pais").val();
        let distritoId = $("#cmb-distrito").val();

        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            EmpresaId: empresaId,
            ProveedorId: proveedorId,
            TipoDocumentoIdentidadId: tipodocumentoidentidadId,
            NroDocumentoIdentidad: nrodocumento,
            RazonSocial: nombres,
            NombreComercial: nombrecomercial,
            PaisId: paisId,
            DistritoId: distritoId,
            Direccion: direccion,
            Correo: correo,
            Usuario: user
        }

        let url = `${urlRoot}api/proveedor/guardar-proveedor`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoProveedor.ResponseEnviarFormulario);
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
                    location.href = `${urlRoot}Proveedores`
                }
            }
        });
    },

    ResponseObtenerDatos: function (data) {
        $("#txt-numero-documento-identidad").val(data.NroDocumentoIdentidad);
        $("#txt-nombres").val(data.RazonSocial);
        $("#txt-nombre-comercial").val(data.NombreComercial);
        $("#txt-correo").val(data.Correo);
        $("#txt-direccion").val(data.Direccion);
        $("#cmb-tipo-documento-identidad").val(data.TipoDocumentoIdentidadId);
        $("#cmb-distrito").val(data.DistritoId);

        $("#cmb-tipo-documento-identidad").prop("disabled", true);
        $("#txt-numero-documento-identidad").prop("disabled", true);
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

    }


}