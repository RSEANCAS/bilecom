﻿var paisLista = [], departamentoLista = [], provinciaLista = [], distritoLista = [];
const pageMantenimientoPersonal = {
    Init: function () {
        this.Validar();
        this.CargarCombo(this.InitEvents())
    },
    InitEvents: function () {

        $("#cmb-pais").change(pageMantenimientoPersonal.CmbPaisChange)
        $("#cmb-departamento").change(pageMantenimientoPersonal.CmbDepartamentoChange);
        $("#cmb-provincia").change(pageMantenimientoPersonal.CmbProvinciaChange);

        pageMantenimientoPersonal.ObtenerDatos();
    },

    CmbPaisChange: function () {
        let paisId = $("#cmb-pais").val();
        let DepartamentoFiltro = departamentoLista.filter(x => x.PaisId == paisId);
        pageMantenimientoPersonal.ResponseDepartamentoListar(DepartamentoFiltro);
    },
    CmbDepartamentoChange: function () {
        let departamentoId = $("#cmb-departamento").val();
        let ProvinciaFiltro = provinciaLista.filter(x=> x.DepartamentoId == departamentoId);
        pageMantenimientoPersonal.ResponseProvinciaListar(ProvinciaFiltro);
    },
    CmbProvinciaChange: function () {
        let provinciaId = $("#cmb-provincia").val();
        let DistritoFiltro = distritoLista.filter(x => x.ProvinciaId == provinciaId);
        pageMantenimientoPersonal.ResponseDistritoListar(DistritoFiltro);
    },

    CargarCombo: async function (fn=null) {
        let promises = [
            fetch(`${urlRoot}api/pais/listar-pais`),
            fetch(`${urlRoot}api/departamento/listar-departamento`),
            fetch(`${urlRoot}api/provincia/listar-provincia`),
            fetch(`${urlRoot}api/distrito/listar-distrito`),
            fetch(`${urlRoot}api/tipodocumentoidentidad/listar-tipodocumentoidentidad`)]
        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([PaisLista,DepartamentoLista,ProvinciaLista,DistritoLista,TipoDocumentoIdentidadLista]) =>
            {
                paisLista = PaisLista;
                departamentoLista = DepartamentoLista;
                provinciaLista = ProvinciaLista;
                distritoLista = DistritoLista;

                pageMantenimientoPersonal.ResponsePaisListar(PaisLista);
                
                pageMantenimientoPersonal.ResponseDepartamentoListar(DepartamentoLista);
                pageMantenimientoPersonal.ResponseProvinciaListar(ProvinciaLista);
                pageMantenimientoPersonal.ResponseDistritoListar(DistritoLista);
                pageMantenimientoPersonal.ResponseTipoDocumentoIdentidadListar(TipoDocumentoIdentidadLista);

                if (typeof fn == 'function') fn();
                
        })
    },

    ObtenerDatos: function () {
        let numero = $("#txt-opcion").val();
        if (numero != 0) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/personal/obtener-personal?EmpresaId=${empresaId}&PersonalId=${numero}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoPersonal.ResponseObtenerDatos);
        } else {
            $("#cmb-pais").val(145).trigger('change');
            $("#cmb-departamento").val(15).trigger('change');
            $("#cmb-provincia").val(128).trigger('change');
        }
    },

    PaisListar: async function () {
        let url = `${urlRoot}api/pais/listar`;
        let init = { method: 'GET' };

        return 
        fetch(url, init);
            //.then(r => r.json())
            //.then(pageMantenimientoPersonal.ResponsePaisListar);
    },
    DepartamentoListar: async function(){
        let url = `${urlRoot}api/departamento/listar`;
        let init = { method: 'GET' };

        return 
        fetch(url, init);
            //.then(r => r.json())
            //.then(pageMantenimientoPersonal.ResponseDepartamentoListar);
    },
    ProvinciaListar: async function () {
        let url = `${urlRoot}api/provincia/listar`;
        let init = { method: 'GET' };
        return 
        fetch(url, init);
            //.then(r => r.json())
            //.then(pageMantenimientoPersonal.ResponseProvinciaListar);
    },
    DistritoListar: async function () {
        let url = `${urlRoot}api/distrito/listar`;
        let init = { method: 'GET' };

        return 
        fetch(url, init);
            //.then(r => r.json())
            //.then(pageMantenimientoPersonal.ResponseDistritoListar);
    },
   
    Validar: function () {
        let tipoid=$("#cmb-tipo-documento-identidad").val();
        $("#frm-personal-mantenimiento")
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
                pageMantenimientoPersonal.EnviarFormulario();
            });
    },

    EnviarFormulario: function () {
        let personalId = $("#txt-opcion").val();
        let nrodocumento = $("#txt-numero-documento-identidad").val();
        let nombre = $("#txt-nombres").val();
        let correo = $("#txt-correo").val();
        let direccion = $("#txt-direccion").val();
        let distritoId = $("#cmb-distrito").val();
        let tipodocumentoidentidadId = $("#cmb-tipo-documento-identidad").val();

        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;
        
        let ObjectoJson = {
            PersonalId: personalId,
            EmpresaId: empresaId,
            TipoDocumentoIdentidadId: tipodocumentoidentidadId,
            NroDocumentoIdentidad: nrodocumento,
            NombresCompletos:nombre ,
            Direccion: direccion,
            Correo: correo,
            FlagActivo: 1,
            Usuario: user,
            DistritoId: distritoId
        }
        let url = `${urlRoot}api/personal/guardar-personal`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoPersonal.ResponseEnviarFormulario);
    },

    ResponseEnviarFormulario: function (data) {
        let tipo = "",mensaje="";
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
                    location.href = `${urlRoot}Personal`
                }
            }
        });
        
    },

    ResponseObtenerDatos: function (data) {
        $("#txt-numero-documento-identidad").val(data.NroDocumentoIdentidad);
        $("#txt-nombres").val(data.NombresCompletos);
        $("#txt-correo").val(data.Correo);
        $("#txt-direccion").val(data.Direccion);
        $("#cmb-tipo-documento-identidad").val(data.TipoDocumentoIdentidadId);
        $("#cmb-distrito").val(data.DistritoId);

        $("#cmb-tipo-documento-identidad").prop("disabled", true);
        $("#txt-numero-documento-identidad").prop("disabled",true);
    },

    ResponsePaisListar: function (data) {
        let datapais = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.PaisId, text: item.Nombre }); });
        $("#cmb-pais").select2({ data: datapais, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseDepartamentoListar: function (data) {
        $("#cmb-departamento").empty();
        let datadepartamento = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.DepartamentoId, text: item.Nombre}); });
        $("#cmb-departamento").select2({ data: datadepartamento, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseProvinciaListar: function (data) {
        $("#cmb-provincia").empty();
        let dataprovincia = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.ProvinciaId, text: item.Nombre }); });
        $("#cmb-provincia").select2({ data: dataprovincia, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseDistritoListar: function (data) {
        $("#cmb-distrito").empty();
        let datadistrito = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.ProvinciaId, text: item.Nombre }); });
        $("#cmb-distrito").select2({ data: datadistrito, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseTipoDocumentoIdentidadListar: function (data) {
        $("#cmb-tipo-documento-identidad").empty();
        let datatipodocumentoidentidad = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.TipoDocumentoIdentidadId, text: item.Descripcion }); });
        $("#cmb-tipo-documento-identidad").select2({ data: datatipodocumentoidentidad, width: '100%', placeholder: '[SELECCIONE...]' });
    }
}

$("#txt-numero-documento-identidad").bind('keypress', function (e) {
    let keyCode = (e.which) ? e.which : event.keyCode;
    let idtipodoc = $("#cmb-tipo-documento-identidad").val();
    if (idtipodoc == 1) {
        return ((keyCode > 47 && keyCode < 58) || (keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123));
    }
    if (idtipodoc == 2) {
        return ((keyCode > 47 && keyCode < 58));
    }
});

$("#txt-nombres").bind('keypress', function (e) {
    let keyCode = (e.which) ? e.which : event.keyCode;
    return (keyCode == 32 || (keyCode > 47 && keyCode < 58) || (keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123));
});
