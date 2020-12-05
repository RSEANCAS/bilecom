const common = {
    ObtenerToken() {
        let token = localStorage['ls.tk'];
        return token == null ? null : token;
    },
    ObtenerUsuario() {
        let user = localStorage['ls.us'];
        return user == null ? null : JSON.parse(user);
    },
    ResponseToJson(response) {
        debugger;
        if (response.status == 200) return response.json();
        else if (response.status == 401) return (async () => "La contraseña es incorrecta")();
        else new Error("");
    }
}