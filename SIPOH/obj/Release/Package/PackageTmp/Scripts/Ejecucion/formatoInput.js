//FORMATO DE 0000/0000
function padLeadingZeros(num) {
    verificarNumeros(num);
    var valor = num.value.replace(/-/g, "");
    var partes = valor.split("/");
    if (partes.length === 1) {
        while (valor.length < 8) valor = "0" + valor;
        valor = valor.substring(0, 4) + "/" + valor.substring(4, 8);
    } else {
        for (var i = 0; i < partes.length; i++) {
            while (partes[i].length < 4) partes[i] = "0" + partes[i];
        }
        valor = partes.join("/");
    }
    if (valor.length > 9) valor = valor.substring(0, 9);
    const anioActual = new Date().getFullYear();
    const anioIngresado = parseInt(valor.split('/')[1], 10);
    if (anioIngresado > anioActual || anioIngresado < 1800) {
        num.value = "";
        toastError('La fecha ingresada no es correcta no debe de ser mayor a la fecha anual actual tampoco una fecha muy antigua');
        return;
    } else {
        num.value = valor;
    }
    return valor;
}


function verificarNumeros(input) {
    var valor = input.value;
    var ultimosCuatroDigitos = valor.substring(valor.length - 4);
    if (ultimosCuatroDigitos === '0000') {
        toastError('Numero de archivo incorrecto. Por favor, modificalo.');
        return
    }
}

//FORMATO DE 00-0000-0000
function formatNuc(num) {
    var valor = num.value.replace(/[\/\-]/g, "");
    var resultado = "";
    while (valor.length < 10) valor = "0" + valor;
    resultado = valor.substring(0, 2) + "-" + valor.substring(2, 6) + "-" + valor.substring(6);
    num.value = resultado;
    return resultado;
}
//FORMATO DE 0000-0000-00 & 0000/0000 por select y id
//INICIALES
function aplicarFormatoSegunSeleccion(num) {
    var seleccion = document.getElementById('inputIncomJuzgado').value;
    if (seleccion === "2") {
        formatNuc(num);
    } else {
        padLeadingZeros(num);
    }
}
//CONSIGNACIONES
function aplicarFormatoSegunSeleccion2(num2, dropdownId) {
    var seleccion2 = document.getElementById(dropdownId).value;
    if (seleccion2 === "2") {
        formatNuc(num2);
    } else {
        padLeadingZeros(num2);
    }
}

