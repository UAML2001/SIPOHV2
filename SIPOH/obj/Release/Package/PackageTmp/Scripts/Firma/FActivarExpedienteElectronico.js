function FirmaLineamientos() {

    var Lineamientos = $("[id*='MainContent_ChkTerminosCondiciones']")[0].checked;

    //var Nombre = $("[id*='TxtNombre']").val();
    //var ApPaterno = $("[id*='TxtAPaterno']").val();
    //var ApMaterno = $("[id*='TxtAMaterno']").val();

    var ClaveAcceso = $("[id*='TxtClaveAcceso']").val();
    var Certificado = $("[id*='FCertificado']").val();
    var LlavePrivada = $("[id*='FLlavePrivada']").val();

    if (!Lineamientos)
        MostrarMensaje("Debe aceptar los lineamientos del Expediente Electrónico.", "error", "Normal", "ErrorLineamientosEE");

    //else if (!Nombre)
    //    MostrarMensaje("Debe proporcionar su nombre.", "error", "Normal", "ErrorNombreNulo");
    //else if (!ApPaterno)
    //    MostrarMensaje("Debe proporcionar el apellido paterno.", "error", "Normal", "ErrorAPaternoNula");
    //else if (!ApMaterno)
    //    MostrarMensaje("Debe proporcionar el apellido materno.", "error", "Normal", "ErrorAMaternoNula");

    else if (!Certificado)
        MostrarMensaje("Debe proporcionar el certificado.", "error", "Normal", "ErrorCertificadoNulo");
    else if (!LlavePrivada)
        MostrarMensaje("Debe proporcionar la llave privada.", "error", "Normal", "ErrorLlavePrivadaNula");
    else if (!ClaveAcceso)
            MostrarMensaje("Debe proporcionar la clave de acceso.", "error", "Normal", "ErrorClaveAccesoNula");

    if (Lineamientos && Certificado && LlavePrivada && ClaveAcceso) {
        firma.validateKeyPairs(ClaveAcceso, function (objResult) {
            if (objResult.state == 0) {

                firma.decodeCertificate(true, function (objDetalles) {
                    if (objDetalles.state == 0) {
                        $("[id*='HFDatosFirmaUsuario']").val(JSON.stringify(objDetalles));

                        $.ajax({
                            type: "GET"
                            , url: UrlServicio + "NLineamientosHash?HashDocEE=1"
                            , dataType: "json"
                            , success: function (doc) {

                                Lineamientos = doc;

                                if (Lineamientos.IdNLineamientos != 0) {

                                    var detalles = [];
                                    var digestionArchivo = Lineamientos.Hash;

                                    if (digestionArchivo) {
                                        firma.signFileDigest(fielnet.Digest.Source.USER, digestionArchivo, fielnet.Format.B64, fielnet.Digest.SHA2, null, function (oResult) {
                                            if (oResult.state == 0) {
                                                detalles.push({ DigestHash: digestionArchivo, IdTransfer: oResult.transfer, Descripcion: oResult.description, Fecha: oResult.date, Evidencia: oResult.evidence, Huella: oResult.sign, CN: oResult.commonName, HexSerie: oResult.hexSerie, IdNLineamientos: Lineamientos.IdNLineamientos, NombreArchivo: Lineamientos.NombreArchivo });
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
                                    //$("[id*='btnGenerarPDF']").trigger("click");
                                        new PNotify({ title: 'SINE INFORMA', type: 'error', text: "Error al intentar obtener el documento de terminos y lineamientos, favor de intentar más tarde." });
                                }
                            }
                            //En caso de que la llamada al WS haya generado un error se muestra el error 
                            , error: function (msg) {
                                new PNotify({ title: 'SINE INFORMA', type: 'error', text: "Error al realizar la llamada a el servicio web. Asegurese de estar conectado a la red: " + msg });
                            }
                        });

                    } else {
                        $("[id*='HFCorreoCertificado']").val("");
                        MostrarMensaje(objDetalles.description, "error", "Normal", "ErrorResultLogin");
                    }
                });
            }
            else {
                //alert(objResult.description);
                MostrarMensaje(objResult.description, "error", "Normal", "ErrorResultLogin");
                //location.reload();
            }
        });

    }
}