﻿const pageLayout = {
    Init: function () {
        this.InitEvents();
        this.MostrarDatosUsuario();
    },
    InitEvents: function () {
        $('#btn-cambiar-perfil').click(pageLayout.BtnCambiarPerfilClick);
    },
    CargarComboUsuarioPerfil: function () {

    },
    BtnCambiarPerfilClick: function (e) {
        e.preventDefault();
        let user = common.ObtenerUsuario();
        let dataPerfil = user.ListaPerfil.map(x => { let item = Object.assign(x, { value: x.PerfilId, text: x.Nombre }); return item; });
        let perfilActual = common.ObtenerPerfilActual();

        bootbox.prompt({
            title: "Seleccione perfil",
            inputType: "select",
            inputOptions: dataPerfil,
            value: perfilActual.PerfilId,
            callback: function (result) {
                if (result != null) {
                    let perfilId = Number(result);
                    let perfil = dataPerfil.find(x => x.PerfilId == perfilId);
                    common.GuardarPerfilActual(perfil);
                    pageLayout.MostrarPerfilActual();
                    //$.niftyNoty({
                    //    type: 'success',
                    //    icon: 'pli-consulting icon-2x',
                    //    message: 'Hi ' + result,
                    //    container: 'floating',
                    //    timer: 5000
                    //});
                }
            }
        });
    },
    MostrarDatosUsuario: function () {
        let user = common.ObtenerUsuario();
        if (user != null) {
            $('.data-user-nombre').text(user.Nombre);
            pageLayout.MostrarPerfilActual();
        }
    },
    MostrarPerfilActual: function () {
        let perfilActual = common.ObtenerPerfilActual();
        $('.data-user-perfil-nombre').text(perfilActual.Nombre);
        let listaOpcionInicial = perfilActual.ListaOpcion == null ? [] : perfilActual.ListaOpcion.filter(x => x.OpcionPadreId == null);
        let listaOpcionGeneral = perfilActual.ListaOpcion || [];
        let opcionesHtml = pageLayout.ListarMenu(listaOpcionInicial, listaOpcionGeneral);
        opcionesHtml = `<li class="list-header">Opciones</li>${opcionesHtml}`;

        $('#mainnav-menu').html(opcionesHtml);
        $(document).trigger('nifty.ready');
    },
    ListarMenu: function (lista, listaGeneral) {
        let html = "";

        if (lista != null) {
            for (let data of lista) {
                let listaMenu = listaGeneral.filter(x => x.OpcionPadreId == data.OpcionId);
                let tieneListaMenu = listaMenu.length > 0;
                html += `<li>
                            <a href="${data.Enlace.replace("~/", urlRoot)}">
                                <i class="demo-pli-tactic"></i>
                                <span class="menu-title">${data.Nombre}</span>
                                ${(tieneListaMenu == true ? '<i class="arrow"></i>' : "")}
                            </a>
                            ${(tieneListaMenu == true) ?
                                `<ul class="collapse">
                                    ${pageLayout.ListarMenu(listaMenu, listaGeneral)}
                                </ul>`
                                : ""
                            }
                        </li>`;
            }
        }

        return html;
    }
}