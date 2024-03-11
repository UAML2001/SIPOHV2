function FirmaNotificacion() {
    var password = $("[id*='TxtClaveAcceso']").val();
    if (password) {
        firma.validateKeyPairs(password, function (objResult) {
            if (objResult.state == 0) {

                //firma.decodeCertificate(true, function (objDetalles) {
                //    if (objDetalles.state == 0) {
                        // En esta parte no entra pero deberia
                        //$("[id*='HFCorreoCertificado']").val(objDetalles.subjectEmail);


                        var Notificados = [];
                        var DatosNotificacion = $("[id*='HFDatosNotificacion']").val();

                        Notificados = JSON.parse(DatosNotificacion);

                        if (Notificados.length != 0) {
                            var detalles = [];
                            for (var Not in Notificados) {
                                var digestionArchivo = Notificados[Not].DigestHash;

                                if (digestionArchivo) {
                                    firma.signFileDigest(fielnet.Digest.Source.USER, digestionArchivo, fielnet.Format.B64, fielnet.Digest.SHA2, null, function (oResult) {
                                        if (oResult.state == 0) {
                                            var transfer = oResult.transfer;

                                            detalles.push({ DigestHash: digestionArchivo, IdTransfer: transfer, IdNNotificacion: Notificados[Not].IdNNotificacion, Descripcion: oResult.description, Fecha: oResult.date, Evidencia: oResult.evidence, Huella: oResult.sign, CN: oResult.commonName, HexSerie: oResult.hexSerie, IdNNotificado: Notificados[Not].IdNNotificado, RutaSinFirma: Notificados[Not].RutaSinFirma, RutaConFirma: Notificados[Not].RutaConFirma, CorreoE: Notificados[Not].CorreoE, NombreCompleto: Notificados[Not].NombreCompleto, NombreArchivoPDF: Notificados[Not].NombreArchivoPDF });

                                        }
                                        else {
                                            MostrarMensaje(oResult.description, "error", "Normal", "ErrorResulTransfer");
                                        }
                                    });

                                }
                                else {

                                }

                            }
                            $("[id*='HFDatosTransfer']").val(JSON.stringify(detalles));
                            $("[id*='BtnGenerarArchivosFEA']").trigger("click");
                        }
                        else {
                            //    new PNotify({ title: 'AGENDA INFORMA', type: 'error', text: "El servicio web devolvio el siguiente Error: " + ResWS.Resultado.Mensaje.toString() });
                        }

                
                //    } else {
                //        $("[id*='HFCorreoCertificado']").val("");
                //        MostrarMensaje(objDetalles.description, "error", "Normal", "ErrorResultLogin");
                //    }
                //});
            }
            else {
                //alert(objResult.description);
                MostrarMensaje(objResult.description, "error", "Normal", "ErrorResultLogin");
            }
        });

    } else {
        MostrarMensaje("Falta especificar contraseña", "error", "Normal", "ErrorResultContraseña");
        //alert("Falta especificar contraseña");
    }
}

