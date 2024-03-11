function FirmaDocumentos() {
    var password = $("[id*='TxtClaveAcceso']").val();
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

                var DatosDocumentosFirmar = [];
                var DatosNotificacion = $("[id*='HFDatosNotificacion']").val();

                DatosDocumentosFirmar = JSON.parse(DatosNotificacion);

                if (DatosDocumentosFirmar.length != 0) {
                    var detalles = [];
                    for (var Doc in DatosDocumentosFirmar) {
                        var digestionArchivo = DatosDocumentosFirmar[Doc].DigestHash;

                        if (digestionArchivo) {
                            firma.signFileDigest(fielnet.Digest.Source.USER, digestionArchivo, fielnet.Format.B64, fielnet.Digest.SHA2, null, function (oResult) {
                                if (oResult.state == 0) {
                                    var transfer = oResult.transfer;

                                    detalles.push({ DigestHash: digestionArchivo, IdTransfer: transfer, IdSolicitudBuzon: DatosDocumentosFirmar[Doc].IdSolicitudBuzon, IdDocDigital: DatosDocumentosFirmar[Doc].IdDocDigital, Descripcion: oResult.description, Fecha: oResult.date, Evidencia: oResult.evidence, Huella: oResult.sign, CN: oResult.commonName, HexSerie: oResult.hexSerie, RutapdfOriginal: DatosDocumentosFirmar[Doc].RutapdfOriginal, Rutapdf_Firmado: DatosDocumentosFirmar[Doc].Rutapdf_Firmado, Nombrearchivopdf_Original: DatosDocumentosFirmar[Doc].Nombrearchivopdf_Original, Nombrearchivopdf_Firmado: DatosDocumentosFirmar[Doc].Nombrearchivopdf_Firmado });

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
                    toastError('No se encuentran datos de los documentos a firmar, intentar nuevamente.');
                }



            }
            else {
                toastError(objResult.description);
            }
        });

    } else {
        toastError('Compruebe los datos ingresados');
    }
}