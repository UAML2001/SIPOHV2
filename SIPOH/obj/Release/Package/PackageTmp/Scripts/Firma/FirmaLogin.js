function firmaLogeo() {
    var password = $("[id*='txtPass']").val();
    var Certificado = $("[id*='FCertificado']").val();
    var LlavePrivada = $("[id*='FLlavePrivada']").val();


     if (!Certificado)
        toastInfo("Seleccione el certificado.");
    else if (!LlavePrivada)
        toastInfo("Seleccione la llave privada.");
     else if (!password)
        toastInfo("Proporcione su clave de acceso.");


    if (Certificado && LlavePrivada && password) {
        firma.validateKeyPairs(password, function (objResult) {
            if (objResult.state == 0) {

                firma.decodeCertificate(true, function (objDetalles) {
                    if (objDetalles.state == 0) {
                        // En esta parte no entra pero deberia
                        $("[id*='HFCURP']").val(objDetalles.subjectCURP);
                        var datos = '{CURP:"' + objDetalles.subjectCURP +  '"}';

                        $.ajax({
                            type: "POST"
                            , contentType: "application/json; charset=utf-8"
                            , url: UrlServicio + "ObtenerUsuarioExterno"
                            //, data: JSON.stringify({ CURP: objDetalles.subjectCURP })
                            , data: datos
                            , dataType: "json"
                            , success: function (doc) {

                                Notificado = doc.d;



                                if (Notificado.IdUsuarioExterno > 0) {
                                    $("[id*='HFIdUsuarioExterno']").val(Notificado.IdUsuarioExterno);
                                    $("[id*='btnIngresar']").trigger("click");
                                }
                                else {
                                    toastError("Usuario no identificado, intente con credenciales validas.");

                                }
                            }
                            //En caso de que la llamada al WS haya generado un error se muestra el error 
                            , error: function (msg) {
                                toastError("Error al realizar la llamada a el servicio web. Asegurese de estar conectado a la red: " + msg );
                            }
                        });

                    } else {
                        $("[id*='HFCURP']").val("");
                        toastError(objDetalles.description);
                    }
                });
            }
            else {
                //alert(objResult.description);
                toastError(objResult.description);
            }
        });

    } else {
        toastError("Falta especificar contraseña");
        //alert("Falta especificar contraseña");
    }
}

