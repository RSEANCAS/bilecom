const common = {
    ObtenerToken() {
        let token = localStorage['ls.tk'];
        return token == null ? null : token;
    },
    ObtenerUsuario() {
        let user = localStorage['ls.us'];
        return user == null ? null : JSON.parse(user);
    }
}