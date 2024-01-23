//FORMATO DE 0000/0000
function padLeadingZeros(num) {
    var valor = num.value.replace(/-/g, "");
    var partes = valor.split("/");
    if (partes.length === 1) {
        while (valor.length < 8) valor = "0" + valor;
        valor = valor.substring(0, 4) + "/" + valor.substring(4);
    } else {
        for (var i = 0; i < partes.length; i++) {
            while (partes[i].length < 4) partes[i] = "0" + partes[i];
        }
        valor = partes.join("/");
    }
    if (valor.length > 9) {
        valor = valor.substring(0, 9);
    }
    num.value = valor;
    return valor;
}
//FORMATO DE 0000-0000-00

//FORMATO DE 0000-0000-00 & 0000/0000