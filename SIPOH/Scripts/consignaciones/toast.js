﻿toastr.options = {
    closeButton: true,
    debug: false,
    newestOnTop: false,
    progressBar: true,
    positionClass: "toast-bottom-right",
    preventDuplicates: false,
    onclick: null,
    showDuration: "300",
    hideDuration: "1000",
    timeOut: "5000",
    extendedTimeOut: "1000",
    showEasing: "swing",
    hideEasing: "linear",
    showMethod: "fadeIn",
    hideMethod: "fadeOut"
};
function toastExito(mensaje) {
    toastr.success(mensaje, "Exito");
}
function toastError(mensaje) {
    toastr.error(mensaje, "Error");
}
function toastInfo(mensaje) {
    toastr.info(mensaje, "Informacion");
}
function toastWarning(mensaje) {
    toastr.warning(mensaje, "Atención");
}