function ValidarFirmaGenerarAcceso() {

    var TerminosCondiciones = $("[id*='ChkTerminosCondiciones']")[0].checked;

    var Nombre = $("[id*='TxtNombre']").val();
    var ApPaterno = $("[id*='TxtAPaterno']").val();
    var ApMaterno = $("[id*='TxtAMaterno']").val();

    var ClaveAcceso = $("[id*='txtPass']").val();
    var Certificado = $("[id*='FCertificado']").val();
    var LlavePrivada = $("[id*='FLlavePrivada']").val();
    
    if (!TerminosCondiciones)
        toastInfo("Debe aceptar los terminos y condiciones de la notificación electrónica.");
    else if (!Nombre)
        toastInfo("Debe proporcionar su nombre.");
    else if (!ApPaterno)
        toastInfo("Debe proporcionar el apellido paterno.");
    else if (!ApMaterno)
        toastInfo("Debe proporcionar el apellido materno.");

    else if (!Certificado)
        toastInfo("Seleccione el certificado.");
    else if (!LlavePrivada)
        toastInfo("Seleccione la llave privada.");
    else if (!ClaveAcceso)
        toastInfo("Proporcione su clave de acceso.");

    if (TerminosCondiciones && Nombre && ApPaterno && ApMaterno && Certificado && LlavePrivada && ClaveAcceso) {
        firma.validateKeyPairs(ClaveAcceso, function (objResult) {
            if (objResult.state == 0) {

                firma.decodeCertificate(true, function (objDetalles) {
                    if (objDetalles.state == 0) {
                        // En esta parte no entra pero deberia
                        $("[id*='HFDatosFirmaUsuario']").val(JSON.stringify(objDetalles));

                        $.ajax({
                            type: "POST"
                            , url: UrlServicio + "GetLineamientos"
                            ,contentType: "application/json; charset=utf-8"
                            , dataType: "json"
                            , success: function (doc) {

                                Lineamientos = doc.d;

                                if (Lineamientos.IdNLineamientos != 0) {

                                    var detalles = [];
                                    var digestionArchivo = Lineamientos.Hash;

                                    if (digestionArchivo) {
                                        firma.signFileDigest(fielnet.Digest.Source.USER, digestionArchivo, fielnet.Format.B64, fielnet.Digest.SHA2, null, function (oResult) {
                                            if (oResult.state == 0) {
                                                //detalles.push({ DigestHash: digestionArchivo, IdTransfer: transfer, IdNNotificacion: Notificados[Not].IdNNotificacion, Descripcion: oResult.description, Fecha: oResult.date, Evidencia: oResult.evidence, Huella: oResult.sign, CN: oResult.commonName, HexSerie: oResult.hexSerie, IdNNotificado: Notificados[Not].IdNNotificado, Ruta: Notificados[Not].Ruta });
                                                detalles.push({ DigestHash: digestionArchivo, IdTransfer: oResult.transfer, Descripcion: oResult.description, Fecha: oResult.date, Evidencia: oResult.evidence, Huella: oResult.sign, CN: oResult.commonName, HexSerie: oResult.hexSerie, IdNLineamientos: Lineamientos.IdNLineamientos, NombreArchivo: Lineamientos.NombreArchivo, Nombre: Nombre, ApPaterno: ApPaterno, ApMaterno: ApMaterno });
                                            }
                                            else {
                                                MostrarMensaje(oResult.description, "error", "Normal", "ErrorResulTransfer");
                                            }
                                        });

                                    }

                                    $("[id*='HFDatosTransfer']").val(JSON.stringify(detalles));
                                    $("[id*='BtnActivarNE']").trigger("click"); 
                                }
                                else {
                                    toastError("Error al intentar obtener el documento de terminos y lineamientos, favor de intentar más tarde.");
                                }
                            }
                            //En caso de que la llamada al WS haya generado un error se muestra el error 
                            , error: function (msg) {
                                toastError("Error al realizar la llamada a el servicio web. Asegurese de estar conectado a la red: " + msg );
                            }
                        });
                    } else {
                        $("[id*='HFCorreoCertificado']").val("");
                        toastError(objDetalles.description);
                    }
                });
            }
            else {
                toastError(objResult.description);
            }
        });
    }
}
