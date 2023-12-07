
   
function enviarDato() {
    var dato = document.getElementById('txtDato').value;
    PageMethods.EnviarDato(dato, onSuccess, onError);
}

function onSuccess(result) {
    // Muestra la respuesta en la página.
    document.getElementById('resultado').innerHTML = "Respuesta: " + result;
}

function onError(error) {
    alert("Error: " + error.get_message());
}
