//FORMATO DE 0000/0000
function padLeadingZeros(num) {
    verificarNumeros(num); // Asumo que esta función verifica la validez del número
    var valor = num.value.replace(/-/g, ""); // Elimina guiones
    var partes = valor.split("/"); // Separa la cadena por las barras
    if (partes.length === 1) {
        while (valor.length < 8) valor = "0" + valor; // Añade ceros al inicio si es necesario
        valor = valor.substring(0, 4) + "/" + valor.substring(4, 8); // Formatea la cadena
    } else {
        for (var i = 0; i < partes.length; i++) {
            while (partes[i].length < 4) partes[i] = "0" + partes[i]; // Añade ceros al inicio si es necesario
        }
        valor = partes.join("/"); // Une las partes con una barra
    }
    if (valor.length > 9) valor = valor.substring(0, 9); // Limita la longitud de la cadena

    // Verificar año
    const anioActual = new Date().getFullYear();
    const anioIngresado = parseInt(valor.split('/')[1], 10);
    if (anioIngresado > anioActual || anioIngresado < 1800) {
        num.value = ""; // Borrar el valor del campo
        toastError('La fecha ingresada no es correcta no debe de ser mayor a la fecha anual actual tampoco una fecha muy antigua');
        return; // Evitar que se establezca un valor inválido
    } else {
        num.value = valor; // Actualiza el valor del campo con el valor formateado
    }
    return valor; // Devuelve el valor formateado
}


function verificarNumeros(input) {
    // Obtener el valor del input
    var valor = input.value;


    // Obtener los últimos cuatro dígitos
    var ultimosCuatroDigitos = valor.substring(valor.length - 4);

    // Verificar si los últimos cuatro dígitos son todos ceros
    if (ultimosCuatroDigitos === '0000') {
        // Mostrar un mensaje de alerta o notificación
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
function aplicarFormatoSegunSeleccion(num) {
    var seleccion = document.getElementById('inputIncomJuzgado').value;
    if (seleccion === "2") {
        formatNuc(num);
    } else {
        padLeadingZeros(num);
    }
}