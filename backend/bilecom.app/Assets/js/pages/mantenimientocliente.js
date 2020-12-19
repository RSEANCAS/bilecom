﻿var paisLista = [], departamentoLista = [], provinciaLista = [], distritoLista = [];
const pageMantenimientoCliente = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {
        pageMantenimientoCliente.CargarCombo();
        pageMantenimientoCliente.ObtenerDatos();

        $("#cmb-pais").change(pageMantenimientoCliente.CmbPaisChange)
        $("#cmb-departamento").change(pageMantenimientoCliente.CmbDepartamentoChange);
        $("#cmb-provincia").change(pageMantenimientoCliente.CmbProvinciaChange);
    },

    Validar: function () {
        $("#frm-cliente-mantenimiento")
            .bootstrapValidator({
                fields: {
                    "cmb-tipo-documento-identidad": {
                        validators: {
                            notEmpty: {
                                message: "Debe seleccionar tipo de documento de identidad",
                            }
                        }
                    },
                    "cmb-departamento": {
                        validators: {
                            notEmpty: {
                                message: "Debe seleccionar departamento",
                            }
                        }
                    },
                    "cmb-provincia": {
                        validators: {
                            notEmpty: {
                                message: "Debe seleccionar provincia",
                            }
                        }
                    },
                    "cmb-distrito": {
                        validators: {
                            notEmpty: {
                                message: "Debe seleccionar distrito",
                            }
                        }
                    },
                    "txt-nombres": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Nombres o Razon Social.",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-_ñÑ .]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
                            }
                        }
                    },
                    "txt-numero-documento-identidad": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar numero de documento de identidad",
                            }
                        }
                    },
                    "txt-correo": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar correo válido",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9._-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/,
                                message: 'Debe ingresar un correo válido'
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
                            }
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoCliente.EnviarFormulario();
            });
    },

    CmbPaisChange: function () {
        let paisId = $("#cmb-pais").val();
        let DepartamentoFiltro = departamentoLista.filter(x => x.PaisId == paisId);
        pageMantenimientoCliente.ResponseDepartamentoListar(DepartamentoFiltro);
    },

    CmbDepartamentoChange: function () {
        let departamentoId = $("#cmb-departamento").val();
        let ProvinciaFiltro = provinciaLista.filter(x => x.DepartamentoId == departamentoId);
        pageMantenimientoCliente.ResponseProvinciaListar(ProvinciaFiltro);
    },

    CmbProvinciaChange: function () {
        let provinciaId = $("#cmb-provincia").val();
        let DistritoFiltro = distritoLista.filter(x => x.ProvinciaId == provinciaId);
        pageMantenimientoCliente.ResponseDistritoListar(DistritoFiltro);
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

                pageMantenimientoCliente.ResponsePaisListar(PaisLista);

                pageMantenimientoCliente.ResponseDepartamentoListar(DepartamentoLista);
                pageMantenimientoCliente.ResponseProvinciaListar(ProvinciaLista);
                pageMantenimientoCliente.ResponseDistritoListar(DistritoLista);
                pageMantenimientoCliente.ResponseTipoDocumentoIdentidadListar(TipoDocumentoIdentidadLista);

                $("#cmb-pais").val(145).trigger('change');
                $("#cmb-departamento").val(15).trigger('change');
                $("#cmb-provincia").val(128).trigger('change');


            })
    },

    ObtenerDatos: function () {
        let numero = $("#txt-opcion").val();
        if (numero != 0) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/cliente/obtener-cliente?EmpresaId=${empresaId}&ClienteId=${numero}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoCliente.ResponseObtenerDatos);
        }
    },

    EnviarFormulario: function () {
        let clienteId = $("#txt-opcion").val();
        let nombres = $("#txt-nombres").val();
        let nrodocumento=$("#txt-numero-documento-identidad").val();
        let nombrecomercial = $("#txt-nombre-comercial").val();
        let direcion = $("#txt-direccion").val();
        let correo = $("#txt-correo").val();
        let tipodocumentoidentidadId = $("#cmb-tipo-documento-identidad").val();
        let distritoId = $("#cmb-distrito").val();

        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            ClienteId: clienteId ,
            EmpresaId: empresaId,
            TipoDocumentoIdentidadId: tipodocumentoidentidadId,
            NroDocumentoIdentidad: nrodocumento,
            RazonSocial: nombres,
            NombreComercial: nombrecomercial,
            DistritoId: distritoId,
            Direccion: direcion,
            Correo: correo,
            FlagActivo: 1,
            Usuario: user
        }

        let url = `${urlRoot}api/cliente/guardar-cliente`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoCliente.ResponseEnviarFormulario);
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
                    location.href = `${urlRoot}Cliente`
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