var paisLista = [], departamentoLista = [], provinciaLista = [], distritoLista = [];
const pageMantenimientoPersonal = {
    Init: function () {
        this.Validar();
        this.InitEvents();
    },
    InitEvents: function () {
        pageMantenimientoPersonal.CargarCombo();
        pageMantenimientoPersonal.ObtenerDatos();
        
        //pageMantenimientoPersonal.CmbPaisChange
        //$("#cmb-pais").select2("val", 145); //a lil' bit more :) 
        //pageMantenimientoPersonal.CmbDepartamentoChange
        //pageMantenimientoPersonal.CmbProvinciaChange
        $("#cmb-pais").change(pageMantenimientoPersonal.CmbPaisChange)
        $("#cmb-departamento").change(pageMantenimientoPersonal.CmbDepartamentoChange);
        $("#cmb-provincia").change(pageMantenimientoPersonal.CmbProvinciaChange);
        
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
    CargarCombo: async function () {
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

                $("#cmb-pais").val(145).trigger('change');
                $("#cmb-departamento").val(15).trigger('change');
                $("#cmb-provincia").val(128).trigger('change');

                
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
                .then(pageMantenimientoPersonal.RespondeObtenerDatos);
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
        $("#frm-personal-mantenimiento")
            .bootstrapValidator({
                fields: {
                    "txt-nombre": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Categoria",
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
        if (data == true) {
            alert("Se ha guardado con éxito.");
            location.href = "Index";
        } else {
            alert("No se pudo guardar la categoria intentelo otra vez.");
        }
    },

    RespondeObtenerDatos: function (data) {
        console.log(data);
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