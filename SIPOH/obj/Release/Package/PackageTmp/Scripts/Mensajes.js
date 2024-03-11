
toastr.options = {
    closeButton: true,
    debug: false,
    newestOnTop: false,
    progressBar: true,
    positionClass: "toast-top-right",
    preventDuplicates: false,
    onclick: null,
    showDuration: "2000",
    hideDuration: "1000",
    timeOut: "5000",
    extendedTimeOut: "1000",
    showEasing: "swing",
    hideEasing: "linear",
    showMethod: "fadeIn",
    hideMethod: "fadeOut"
};

function mostrarToast(mensaje) {
    toastr.success(mensaje, "Exito");
}
function toastError(mensaje) {
    toastr.error(mensaje, "Error");
}
function toastInfo(mensaje) {
    toastr.warning(mensaje, "Informacion");
}


    function toastRedireccion(mensaje) {
        toastr.success(mensaje, "Exito", {
            timeOut: 5000,
            preventDuplicates: true,
            positionClass: 'toast-top-center',
            // Redirect 
            onHidden: function () {
                window.location.href = 'LoginExterno.aspx';
            }
        });
}


function toastRedireccionPagina(mensaje, pagina) {
    toastr.success("<div class='container text-center centered-element' ><label class='h4'>" + mensaje + "</label>", '', {
        preventDuplicates: true,
        positionClass: 'toast-center-center',
        timeOut: 5000,
        allowHtml: true,
        preventDuplicates: true,
        // Redirect 
        onHidden: function () {
            window.location.href = pagina;
        }
    }).css({ "width": "30em", "max-width": "30em", "height": "15em" });
}