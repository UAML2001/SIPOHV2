
function CerrarModal() {
    $('#modalAgregarVictimas').modal('hide'); // Corrección en la selección del modal
    $('body').removeClass('modal-open').css('overflow', ''); // Restablece el overflow
    $('.modal-backdrop').remove();
     // Restablece el overflow
    $('body').css('overflow', 'auto !important');
    
}




function validarNumero(input) {
    // Obtener el valor del campo
    var valor = input.value;

    // Verificar si el valor es un número
    if (isNaN(valor)) {
        // Si no es un número, mostrar un mensaje de error y limpiar el campo
        var mensaje = "Este  campo solo acepta numeros";
        //alert("Este  campo solo acepta numeros");
        toastError(mensaje);


        input.value = "";
    }
}
function validarFechaC(input) {

    var inputFechaRecepcion = input
    var fechaSeleccionada = new Date(inputFechaRecepcion.value);
    var fechaActual = new Date();
    fechaActual.setHours(0, 0, 0, 0);

    if (fechaSeleccionada > fechaActual) {
        //toastError("!Estas loco!, o ¿Vives en el futuro?");
        toastError("No se puede seleccionar una fecha posterior a hoy.");
        inputFechaRecepcion.value = "";
    }
}
