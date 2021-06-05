const common = {
    GuardarDataCookie(data) {
        common.GuardarToken(data.Token);
        common.GuardarUsuario(data.Usuario);
        common.GuardarFechaExpiracion(data.FechaExpiracion);

        let dataString = JSON.stringify(data);
        let fechaExpires = common.ObtenerFechaExpiracion();
        let cookieSession = `ss=${dataString}; expires=${fechaExpires.toUTCString()}`;
        document.cookie = cookieSession;
    },
    ObtenerDataCookie() {
        let cookies = common.ListarCookies();
        let data = cookies.find(x => x.key == "ss");
        return JSON.parse(data.value);
    },
    ObtenerToken() {
        let token = localStorage['ls.tk'];
        return token == null ? null : atob(token);
    },
    GuardarToken(data) {
        localStorage['ls.tk'] = btoa(data);
        //document.cookie = `ls.tk=${localStorage['ls.tk']}`
    },
    ObtenerUsuario() {
        let user = localStorage['ls.us'];
        return user == null ? null : JSON.parse(atob(user));
    },
    GuardarUsuario(data) {
        localStorage['ls.us'] = btoa(JSON.stringify(data));
        //document.cookie = `ls.us=${localStorage['ls.us']}`;
    },
    ObtenerFechaExpiracion() {
        let fechaExpiracion = localStorage['ls.fe'];
        return fechaExpiracion == null ? null : new Date(atob(fechaExpiracion));
    },
    GuardarFechaExpiracion(data) {
        localStorage['ls.fe'] = btoa(data);
    },
    ObtenerPerfilActualCookie() {
        let cookie = this.ObtenerDataCookie();
        let perfilActual = cookie.PerfilActual;
        return perfilActual == null ? null : perfilActual;
    },
    GuardarPerfilActualCookie(data) {
        let cookie = this.ObtenerDataCookie();
        cookie.PerfilActual = data;
        common.GuardarDataCookie(cookie);
    },
    ObtenerSedeActual() {
        let cookie = this.ObtenerDataCookie();
        let sedeActual = cookie.SedeActual;
        return sedeActual == null ? null : sedeActual;
    },
    GuardarSedeActual(data) {
        let cookie = this.ObtenerDataCookie();
        cookie.SedeActual = data;
        common.GuardarDataCookie(cookie);
    },
    ListarCookies() {
        let cookies = document.cookie.split(";").filter(x => x != '').map(x => {
            let array = x.trim().split('=');
            let data = { key: array[0], value: array[1] };
            return data;
        });

        return cookies;
    },
    ObtenerCookie(key) {
        let lista = common.ListarCookies();
        let value = lista.find(x => x.key == key);
        return value;
    },
    ResponseToJson(response) {
        if (response.status == 200) return response.json();
        else if (response.status == 401) return (async() => "La contraseña es incorrecta")();
        else new Error("");
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
                Processing: "Procesando...",
                infoEmpty: "Mostrando 0 a 0 de 0 registros",
                paginate: {
                    first: "Primero",
                    last: "Último",
                    next: "Siguiente",
                    previous: "Anterior"
                }
            }
        })
    },
    CreateDataTableFromAjax: function (id, ajax, columns, aditional = {}) {
        let estaInicializado = $.fn.DataTable.isDataTable(id);
        if (estaInicializado == true) {
            $(id).DataTable().ajax.reload();
            return;
        }
        $(id).dataTable({
            processing: true,
            serverSide: true,
            ajax: ajax,
            columns: columns,
            ...aditional
        })
    },
    CreateDataTableFromData: function (id, data, columns) {
        let estaInicializado = $.fn.DataTable.isDataTable(id);
        if (estaInicializado == true) {
            $(id).DataTable().clear()
            $(id).DataTable().rows.add(data);
            $(id).DataTable().draw();
            return;
        }
        $(id).dataTable({
            data: data,
            columns: columns
        })
    },
    ArrayExtensions(arrayPrototypes = []) {
        if (arrayPrototypes.some(x => x.toString().trim() == "distinct")) {
            Array.prototype.distinct = function (keys = []) {
                let esArray = Array.isArray(keys);
                let data = this;
                let newData = [];
                if (esArray) {
                    if (data.length == 0) return data;

                    let row = data[0];
                    let keysObject = Object.keys(row).filter(x => keys.some(y => x == y));

                    for (let item of data) {
                        let exists = false;

                        let dataUnique = keysObject.map(x => item[x]);

                        exists = newData.some((x) => {
                            let dataItemUnique = keysObject.map(y => x[y].toString().trim());
                            return dataItemUnique.join("") == dataUnique.join("");
                        });

                        if (exists) continue;

                        newData.push(item);
                    }
                }

                return newData;
            }
        }
    },
    KeyPressPreventSubmitEnter(e) {
        if (e.keyCode == 13) {
            e.preventDefault();
            let inputElementLength = $(":input:visible:focus").length;
            let inputElement = $(":input:visible:focus")[0];
            if (inputElementLength == 1) {
                let listaInputs = Array.from($(":input:visible"));
                let index = listaInputs.findIndex(x => x == inputElement);
                let newIndex = index == listaInputs.length - 1 ? 0 : index + 1;
                listaInputs[newIndex].focus();
            }
        }
        //$("#btnIngresar").trigger("click");
        //$(e.currentTarget).closest("form").trigger("submit");
        //if (e.keyCode == 13) return false;
    }
}