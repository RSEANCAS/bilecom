﻿const common = {
    ObtenerToken() {
        let token = localStorage['ls.tk'];
        return token == null ? null : token;
    },
    ObtenerUsuario() {
        let user = localStorage['ls.us'];
        return user == null ? null : JSON.parse(user);
    },
    ConfiguracionDataTable() {
        $.extend($.fn.dataTable.defaults, {
            searching: false,
            lengthChange: false,
            language: {
                info: "Mostrando _START_ a _END_ de _TOTAL_ registros",
                emptyTable: "No hay datos disponibles",
                zeroRecords: "No se encontraron registros coincidentes",
                loadingRecords: "Cargando...",
                Procesing: "Procesando...",
                infoEmpty:"Mostrando 0 a 0 de 0 registros",
                paginate: {
                    first: "Primero",
                    last: "Último",
                    next: "Siguiente",
                    previous: "Anterior"
                }
            }
        })
    }
}