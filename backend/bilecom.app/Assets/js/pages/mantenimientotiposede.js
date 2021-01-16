var plantillaTipoSedeLista = []
const pageMantenimientoTipoSede = {
    Init: function () {
        this.CargarCombo(() => {
            this.Validar();
            this.InitEvents();
        });
    },

    InitEvents: function () {
        pageMantenimientoTipoSede.ObtenerDatos();
        $("#cmb-codigo-plantilla").change(pageMantenimientoTipoSede.CmbCodigoPlantillaChange);
        $("#cmb-codigo-plantilla").trigger("change");
    },

    CmbCodigoPlantillaChange: function () {
        let plantillaTipoSede = $("#cmb-codigo-plantilla").select2("data");
        let tieneValor = plantillaTipoSede.length > 0;
        if (!tieneValor) {
            plantillaTipoSede = null;
            $("#txt-tipo-sede").val("");
        } else {
            plantillaTipoSede = plantillaTipoSede[0];
            let codigoPlantilla = plantillaTipoSede.Value;
            let textoPlantilla = plantillaTipoSede.Text;
            $("#txt-tipo-sede").val(textoPlantilla);
        }
        $("#txt-tipo-sede").prop("readonly", tieneValor);
        $("#chk-editable").prop("checked", tieneValor);

    },

    Validar: function () {
        $("#frm-tiposede")
            .bootstrapValidator({
                fields: {
                    "txt-tipo-sede": {
                        validators: {
                            notEmpty: {
                                message: "Debe ingresar Tipo de Sede.",
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9-_ñÑ .á-úÁ-Ú]+$/,
                                message: 'Solo puede ingresar caracteres alfabéticos'
                            }
                        }
                    }
                }
            })
            .on('success.form.bv', function (e) {
                e.preventDefault();
                pageMantenimientoTipoSede.EnviarFormulario();
            });
    },

    CargarCombo: async function (fnNext) {
        let user = common.ObtenerUsuario();
        let promises = [
            fetch(`${urlRoot}api/common/listar-enum-tipo-sede`)
        ];
        Promise.all(promises)
            .then(r => Promise.all(r.map(common.ResponseToJson)))
            .then(([PlantillaTipoSedeLista]) => {
                plantillaTipoSedeLista = PlantillaTipoSedeLista || [];

                pageMantenimientoTipoSede.ResponsePlantillaTipoSedeListar(plantillaTipoSedeLista);
                if (typeof fnNext == "function") fnNext();
            })
    },

    ObtenerDatos: function () {
        let numero = $("#txt-opcion").val();
        if (numero != 0) {
            let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;

            let url = `${urlRoot}api/tiposede/obtener-tiposede?empresaId=${empresaId}&tiposedeId=${numero}`;
            let init = { method: 'GET' };

            fetch(url, init)
                .then(r => r.json())
                .then(pageMantenimientoTipoSede.ResponseObtenerDatos);
        }
    },

    EnviarFormulario: function () {
        let tiposedeId = $("#txt-opcion").val();
        let nombre = $("#txt-tipo-sede").val();
        let codigoPlantilla = $("#cmb-codigo-plantilla").val();
        let flagEditable = $("#chk-editable").prop("checked");
        let empresaId = common.ObtenerUsuario().Empresa.EmpresaId;
        let user = common.ObtenerUsuario().Nombre;

        let ObjectoJson = {
            TipoSedeId: tiposedeId,
            EmpresaId: empresaId,
            FlagActivo: 1,
            Nombre: nombre,
            CodigoPlantilla: codigoPlantilla,
            FlagEditable: flagEditable,
            Usuario: user
        };

        let url = `${urlRoot}api/tiposede/guardar-tiposede`;
        let params = JSON.stringify(ObjectoJson);
        let headers = { 'Content-Type': 'application/json' };
        let init = { method: 'POST', body: params, headers };

        fetch(url, init)
            .then(r => r.json())
            .then(pageMantenimientoTipoSede.ResponseEnviarFormulario);
    },

    ResponseObtenerDatos: function (data) {
        $("#cmb-codigo-plantilla").val(data.CodigoPlantilla).trigger("change");
        $("#txt-tipo-sede").val(data.Nombre);
        $("#chk-editable").prop("checked", data.FlagEditable);
    },

    ResponsePlantillaTipoSedeListar: function (data) {
        let dataPlantillaTipoSede = data.map(x => Object.assign(x, { id: x.Value, text: x.Text }));
        $("#cmb-codigo-plantilla").select2({ allowClear: true, data: dataPlantillaTipoSede, width: '100%', placeholder: '[SELECCIONE...]' });
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
                    location.href = `${urlRoot}TiposSede`
                }
            }
        });
    }
}

$("#txt-tipo-sede").bind('keypress', function (e) {
    let keyCode = (e.which) ? e.which : event.keyCode;
    return (keyCode == 32 || (keyCode > 47 && keyCode < 58) || (keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123));
});
