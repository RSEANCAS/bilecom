﻿const pageMantenimientoProveedor = {
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
                    "txt-nombre": {
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

            let url = `${urlRoot}api/proveedor/obtener-proveedor?EmpresaId=${empresaId}&ProveedorId=${numero}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoProveedor.ResponseObtenerDatos);
        }
    },

    EnviarFormulario: function () {
        let nombres = $("#txt-nombres").val();
        let nrodocumento = $("#txt-numero-documento-identidad").val();
        let nombrecomercial = $("#txt-nombre-comercial").val();
        let direcion = $("#txt-direccion").val();
        let correo = $("#txt-correo").val();

        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            ClienteId: 0,
            EmpresaId: empresaId,
            TipoDocumento: 6,
            NroDocumentoIdentidad: nrodocumento,
            RazonSocial: nombres,
            NombreComercial: nombrecomercial,
            DistritoId: '100101',
            Direccion: direcion,
            Correo: correo,
            FlagActivo: 1,
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
                    location.href = `${urlRoot}Proveedor`
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