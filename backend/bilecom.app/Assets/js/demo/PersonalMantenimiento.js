
$("#txt-numero-documento-identidad").bind('keypress', function (e) {
    let keyCode = (e.which) ? e.which : event.keyCode;
    return ((keyCode > 47 && keyCode < 58) || (keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123));
});

$("#txt-nombres").bind('keypress', function (e) {
    let keyCode = (e.which) ? e.which : event.keyCode;
    //console.log(keyCode);
    //return !(keyCode > 32 && (keyCode < 48 || keyCode > 90) && ((keyCode < 97 || keyCode > 122) || keyCode == 32));
    return (keyCode == 32 || (keyCode > 47 && keyCode < 58) || (keyCode > 64 && keyCode < 91) || (keyCode > 96 && keyCode < 123));
});

$("#txt-guardar").click(function () {
    $("#btn-guardar").prop("disabled", false);
});
