var paisLista = [], departamentoLista = [], provinciaLista = [], distritoLista = [];
const pageMantenimientoSede = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {
        pageMantenimientoSede.CargarCombo();
        pageMantenimientoSede.ObtenerDatos();

        $("#cmb-pais").change(pageMantenimientoSede.CmbPaisChange)
        $("#cmb-departamento").change(pageMantenimientoSede.CmbDepartamentoChange);
        $("#cmb-provincia").change(pageMantenimientoSede.CmbProvinciaChange);

    },

    CmbPaisChange: function () {
        let paisId = $("#cmb-pais").val();
        let DepartamentoFiltro = departamentoLista.filter(x => x.PaisId == paisId);
        pageMantenimientoSede.ResponseDepartamentoListar(DepartamentoFiltro);

        $("#cmb-departamento").change();
        $("#cmb-provincia").change();

    },
    CmbDepartamentoChange: function () {
        let departamentoId = $("#cmb-departamento").val();
        let ProvinciaFiltro = provinciaLista.filter(x => x.DepartamentoId == departamentoId);
        pageMantenimientoSede.ResponseProvinciaListar(ProvinciaFiltro);
    },
    CmbProvinciaChange: function () {
        let provinciaId = $("#cmb-provincia").val();
        let DistritoFiltro = distritoLista.filter(x => x.ProvinciaId == provinciaId);
        pageMantenimientoSede.ResponseDistritoListar(DistritoFiltro);
    },

    CargarCombo: async function () {
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let promises = [
            fetch(`${urlRoot}api/pais/listar-pais`),
            fetch(`${urlRoot}api/departamento/listar-departamento`),
            fetch(`${urlRoot}api/provincia/listar-provincia`),
            fetch(`${urlRoot}api/distrito/listar-distrito`),
            fetch(`${urlRoot}api/tiposede/listar-tiposede?empresaId=${empresaId}`)]
        Promise.all(promises)
            .then(r => Promise.all(r.map(x => x.json())))
            .then(([PaisLista, DepartamentoLista, ProvinciaLista, DistritoLista, TipoSedeLista]) => {
                paisLista = PaisLista;
                departamentoLista = DepartamentoLista;
                provinciaLista = ProvinciaLista;
                distritoLista = DistritoLista;

                pageMantenimientoSede.ResponsePaisListar(PaisLista);

                pageMantenimientoSede.ResponseDepartamentoListar(DepartamentoLista);
                pageMantenimientoSede.ResponseProvinciaListar(ProvinciaLista);
                pageMantenimientoSede.ResponseDistritoListar(DistritoLista);
                pageMantenimientoSede.ResponseTipoSedeListar(TipoSedeLista);

                $("#cmb-pais").val(145).trigger('change');
                $("#cmb-departamento").val(15).trigger('change');
                $("#cmb-provincia").val(128).trigger('change');
            })
    },

    Validar: function () {
        $("#frm-sede")
            .bootstrapValidator({
                fields: {
                    "txt-nombre": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Nombre de Sede.",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-_ñÑ .]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
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
                pageMantenimientoSede.EnviarFormulario();
            });
    },

    ObtenerDatos: function () {
        let numero = $("#txt-opcion").val();
        if (numero != 0) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/sede/obtener-sede?empresaId=${empresaId}&sedeId=${numero}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoSede.ResponseObtenerDatos);
        }
    },

    EnviarFormulario: function () {
        let sedeId = $("#txt-opcion").val();
        let nombre = $("#txt-nombre").val();
        let direccion = $("#txt-direccion").val();
        let distritoId = $("#cmb-distrito").val();
        let tiposedeId = $("#cmb-tipo-sede").val();
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            SedeId: sedeId,
            EmpresaId: empresaId,
            TipoSedeId: tiposedeId,
            DistritoId: distritoId,
            Nombre: nombre,
            Direccion: direccion,
            FlagActivo: 1,
            Usuario: user
        }

        let url = `${urlRoot}api/sede/guardar-sede`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoSede.ResponseEnviarFormulario);
    },

    ResponseObtenerDatos: function (data) {
        $("#txt-nombre").val(data.Nombre);
        $("#txt-direccion").val(data.Direccion);
        $("#cmb-tipo-sede").val(data.TipoSedeId).trigger('change');
        $("#cmb-distrito").val(data.DistritoId).trigger('change');
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
                    location.href = `${urlRoot}Sedes`
                }
            }
        });
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
        let datadistrito = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.ProvinciaId, text: item.Nombre }); });
        $("#cmb-distrito").select2({ data: datadistrito, width: '100%', placeholder: '[SELECCIONE...]' });
    },

    ResponseTipoSedeListar: function (data) {
        $("#cmb-tipo-sede").empty();
        let datatiposede = data.map(x => { let item = Object.assign({}, x); return Object.assign(item, { id: item.TipoSedeId, text: item.Nombre }); });
        $("#cmb-tipo-sede").select2({ data: datatiposede, width: '100%', placeholder: '[SELECCIONE...]' });
    }
}

$("#txt-nombre").bind('keypress', function (e) {
    let keyCode = (e.which) ? e.which : event.keyCode;
    //return (keyCode == 32 || (keyCode > 47 && keyCode < 58) || (keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123));
});
