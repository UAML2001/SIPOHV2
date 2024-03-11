var fielnet = fielnet || {};
var CONTROLLER = "Controlador.ashx";
var ajaxAsync = true;
var evidence = 0;

fielnet.Format = {
    HEX: 0,
    B64: 1
};

fielnet.Digest = {
    MD5: 1,
    SHA1: 2,
    SHA2: 3,
    SHA512: 4
};

fielnet.Encoding = {
    UTF8: 2,
    B64: 3,
    HEX: 4
};

fielnet.Digest.Source = {
    USER: 0,
    DOM: 1
};

fielnet.Storages = {
    LOCAL_STORAGE: 0,
    SESSION_STORAGE: 1
};

fielnet.Evidences = {
    NONE: 0,
    TSA: 1,
    NOM: 2,
    TSA_NOM: 3

};

function addScripts(strSubDirectory) {
    if (typeof strSubDirectory == "undefined") {
        strSubDirectory = "";
    }
    var scripts = [
        "util.js",
        "debug.js",
        "jsbn.js",
        "oids.js",
        "asn1.js",
        "sha1.js",
        "sha256.js",
        "sha512.js",
        "md5.js",
        "md.js",
        "prng.js",
        "random.js",
        "jsbn.js",
        "pkcs1.js",
        "rsa.js",
        "cipherModes.js",
        "cipher.js",
        "aes.js",
        "des.js",
        "rc2.js",
        "pbe.js",
        "pem.js",
        "hmac.js",
        "pbkdf2.js",
        "pkcs7.js",
        "pkcs7asn1.js",
        "pkcs12.js",
        "pss.js",
        "mgf1.js",
        "mgf.js",
        "x509.js",
        "pki.js"
    ];

    var satLibs = [
        "yahoo/yahoo-min.js",
        "jsrsasign/x509-1.1.js",
        "jsrsasign/asn1-1.0.js",
        "jsbn/jsbn.js",
        "jsbn/jsbn2.js",
        "jsbn/jsbn2.js",
        "asn1/asn1hex-1.1.js",
        "asn1/asn1.js",
        "asn1/base64.js",
        "jsrsasign/base64.js",
        "cryptojs/pbkdf2.js",
        "cryptojs/enc-base64.js",
        "rsa/rsa.js",
        "rsa/rsa2.js",
        "rsa/rsasign-1.2.js",
        "jsrsasign/crypto-1.1.js",
        "sjcl/sjcl.js",
        "sjcl/sha1.js",
        "cryptojs/tripledes.js"
    ];

    for (var str in satLibs) {
        document.write("<script src='" + (strSubDirectory.length == 0 ? "" : strSubDirectory + "/") + "" + satLibs[str] + "'></script>");
    }

    for (var str in scripts) {
        document.write("<script src='" + (strSubDirectory.length == 0 ? "" : strSubDirectory + "/") + "forge/" + scripts[str] + "'></script>");
    }

}

if (typeof Object.freeze == "function") {
    Object.freeze(fielnet.Digest);
    Object.freeze(fielnet.Encoding);
    Object.freeze(fielnet.Storages);
}

fielnet.Firma = function Firma(oProperties) {
    if (typeof oProperties == "object") {
        if (oProperties.controller) {
            CONTROLLER = oProperties.controller;
        }
        if (oProperties.subDirectory) {
            addScripts(String(oProperties.subDirectory));
        } else {
            addScripts();
        }
        if (oProperties.ajaxAsync != undefined) {
            ajaxAsync = oProperties.ajaxAsync;
        }

        if (oProperties.evidence) {
            evidence = oProperties.evidence;
        }
    } else {
        addScripts();
    }

};

fielnet.Firma.prototype = (function () {
    //Variables privadas
    var strPrivateKey = null;
    var strCertificate = null;
    var strCerPem;

    var strPfx;
    var fileSizePfx;

    var fileSizeCertificate;
    var fileSizePrivateKey;

    var oPrivateKey;

    var extraParameters = "";
    //Constantes
    var NOT_FOUND = "El elemento cuyo id ':id' no ha sido encontrado, verifique que ya existe en el DOM";

    var strReferencia = "";

    var aFirmantesCentrales = [];
    var ws = null;
    var urlWS = "ws://localhost:7555/Token";

    //Funciones privadas
    function rstrtohex(s) {
        var result = "";
        for (var i = 0; i < s.length; i++) {
            result += ("0" + s.charCodeAt(i).toString(16)).slice(-2);
        }
        return result;
    }
    var XMLHttpFactories = [
        function () {
            return new XMLHttpRequest();
        },
        function () {
            return new ActiveXObject("Msxml2.XMLHTTP");
        },
        function () {
            return new ActiveXObject("Msxml3.XMLHTTP");
        },
        function () {
            return new ActiveXObject("Microsoft.XMLHTTP");
        }
    ];

    function getXMLHttpRequest() {
        var xmlhttp = false;
        for (var i = 0; i < XMLHttpFactories.length; i++) {
            try {
                xmlhttp = XMLHttpFactories[i]();
            } catch (e) {
                continue;
            }
            currentXMLHttpRequest = xmlhttp;
            break;
        }
        return xmlhttp;
    }

    function ajaxRequest(opts) {
        var ajaxRequest = getXMLHttpRequest();
        if (ajaxRequest != null) {
            if (typeof opts.url == "undefined" || opts.url.length == 0) {
                alert("No se ha especificado la url para iniciar la petición");
                return;
            }
            if (typeof opts.method == "undefined") {
                opts.method = "POST";
            }

            opts.async = ajaxAsync;

            try {
                if (opts.method.toUpperCase() == "GET") {
                    opts.url += "?" + opts.data;
                }
                ajaxRequest.open(opts.method, opts.url, opts.async);
                if (typeof opts.contentType == "undefined") {
                    ajaxRequest.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
                } else {
                    ajaxRequest.setRequestHeader("Content-Type", opts.contentType);
                }
                if (typeof opts.headers != "undefined") {
                    if (!Array.isArray) {
                        Array.isArray = function (arg) {
                            return Object.prototype.toString.call(arg) === '[object Array]';
                        };
                    }
                    if (Array.isArray(opts.headers)) {
                        for (var key in opts.headers)
                            ajaxRequest.setRequestHeader(key, opts.headers[key]);
                    }
                }
                //Esto aplica para web browsers viejos
                if (typeof ajaxRequest.onloadend == "undefined") {
                    ajaxRequest.onreadystatechange = function (e) {
                        if (ajaxRequest.readyState == 4) { //Cuando la petición esté terminada
                            if (ajaxRequest.status == 200) { //200 exito
                                if (typeof opts.success == "function") {
                                    var data = ajaxRequest.responseText;
                                    opts.success(data, ajaxRequest.status, ajaxRequest);
                                }
                                if (typeof opts.complete == "function") {
                                    var data = ajaxRequest.responseText;
                                    opts.complete(data, ajaxRequest.status, ajaxRequest);
                                }
                            } else { //error
                                if (typeof opts.error == "function") {
                                    var data = ajaxRequest.responseText;
                                    opts.error(data, ajaxRequest.status, ajaxRequest);
                                }
                            }
                        }
                    };
                } //Aplica para web browsers actuales
                else {
                    ajaxRequest.onerror = function (event) {
                        var data = event.currentTarget.responseText;
                        var status = event.currentTarget.status;
                        if (typeof opts.error != "undefined") {
                            if (status == 0) {
                                data = "Error: Verifique que no tenga un firewall que bloquee la petición o que tenga permiso CORS ";
                            }
                            opts.error(data, status, event.currentTarget);
                        } else {
                            alert(data);
                        }
                    };

                    if (typeof opts.success == "function") {
                        ajaxRequest.onload = function (event) {
                            var data = event.currentTarget.responseText;
                            var status = event.currentTarget.status;
                            if (typeof opts.success != "undefined") {
                                opts.success(data, status, event.currentTarget);
                            }
                        };
                    }
                    if (typeof opts.complete == "function") {
                        ajaxRequest.onloadend = function (event) {
                            var data = event.currentTarget.responseText;
                            var status = event.currentTarget.status;
                            if (status == 0) {
                                data = "Error: Verifique que no tenga un firewall que bloquee la petición o que tenga permiso CORS ";
                            }
                            if (typeof opts.complete != "undefined") {
                                opts.complete(data, status, event.currentTarget);
                            }
                        };
                    }
                    if (typeof opts.progress != "undefined") {
                        ajaxRequest.upload.addEventListener("progress", opts.progress, false);
                    }
                }
                if (ajaxAsync) {
                    ajaxRequest.timeout = 300000;
                }
                ajaxRequest.send((opts.method.toUpperCase() == "POST" ? opts.data : null));
            } catch (e) {
                alert("Error: " + e.message);
            }
        }
    }

    function base64ToHex(str) {
        for (var i = 0, bin = atob(str.replace(/[ \r\n]+$/, "")), hex = []; i < bin.length; ++i) {
            var tmp = bin.charCodeAt(i).toString(16);
            if (tmp.length === 1)
                tmp = "0" + tmp;
            hex[hex.length] = tmp;
        }
        return hex.join("");
    }
    function hexToBase64(hex) {
        var base64map = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/";
        if (typeof hex == "undefined")
            return;
        var bytes = [];
        for (var i = 0, c = 0; c < hex.length; c += 2) {
            bytes.push(parseInt(hex.substr(c, 2), 16));
        }

        for (var base64 = [], i = 0; i < bytes.length; i += 3) {
            var triplet = (bytes[i] << 16) | (bytes[i + 1] << 8) | bytes[i + 2];
            for (var j = 0; j < 4; j++) {
                if (i * 8 + j * 6 <= bytes.length * 8)
                    base64.push(base64map.charAt((triplet >>> 6 * (3 - j)) & 0x3F));
                else
                    base64.push("=");
            }
        }
        return base64.join("");
    }

    function privateKeyObject(strPrivateKey) {
        var pkeyDerEncoded = forge.util.decode64(strPrivateKey);
        var pkeyAsn1Encoded = forge.asn1.fromDer(pkeyDerEncoded);
        var privateKey = forge.pki.privateKeyFromAsn1(pkeyAsn1Encoded);
        return privateKey;
    }

    // End Funciones privadas


    //Interfaz pública

    function validateWebBrowser(strMessage) {
        if (!(window.File && window.FileReader && window.FileList && window.Blob && window.sessionStorage && window.localStorage)) {
            if (typeof strMessage != "undefined") {
                alert(strMessage);
            }
            return false;
        }
        return true;
    }

    function getCertificate() {
        return strCertificate;
    }

    function readCertificate(strIdElement) {
        if (typeof strIdElement == "string") {
            var oItem = document.getElementById(strIdElement);
            if (oItem != null) {
                oItem.setAttribute("accept", ".cer");
                oItem.value = "";
                oItem.onchange = function (evt) {

                    if (oItem.files.length > 0) {
                        var file = new FileReader();
                        file.onload = function () {
                            var bytes = new Uint8Array(file.result);
                            var binary = "";
                            for (var i = 0; i < bytes.byteLength; i++) {
                                binary += String.fromCharCode(bytes[i])
                            }
                            var certHex = rstrtohex(binary);
                            strCertificate = (forge.util.encode64(forge.util.hexToBytes(certHex)));
                            if (fileSizeCertificate != bytes.length) {
                                alert("No se ha podido leer el Certificado por completo");
                                strCertificate = null;
                                return;
                            }
                            strCerPem = KJUR.asn1.ASN1Util.getPEMStringFromHex(certHex, 'CERTIFICATE');

                        };
                        file.onerror = function (e) {
                            alert("Ha ocurrido un error al leer el certificado: " + e);
                        };
                        fileSizeCertificate = oItem.files[0].size;
                        file.readAsArrayBuffer(oItem.files[0]);

                    }
                };
            } else {
                alert(NOT_FOUND.replace(":id", strIdElement));
            }
        } else {
            alert("El argumento no tiene un formato válido se require un valor tipo cadena");
        }
    }

    function decodeCertificate(strCert, bOcsp, fCallback) {

        if (strCert == null) {
            if (typeof fCallback == "function") {
                var oResult = {};
                oResult.state = -99;
                oResult.description = "No se ha cargado ningún certificado";
                fCallback(oResult);
            }
            return;
        }

        var url = "metodo=decodecert&cert=" + strCert + "&ocsp=" + bOcsp + "&referencia=" + strReferencia + extraParameters;
        ajaxRequest({
            url: CONTROLLER,
            method: "POST",
            data: url,
            success: function (oResponse, status, xmlhttp) {
                if (typeof fCallback == "function") {
                    try {
                        var oJSONResponse = JSON.parse(oResponse);
                        fCallback(oJSONResponse);
                    } catch (e) {
                        var oError = {};
                        oError.state = -99;
                        oError.description = "Error leyendo respuesta del servidor: " + e;
                        fCallback(oError);
                    }
                }
            },
            complete: function (data) {
                if (typeof fCallback == "function") { }
            },
            error: function (data) {
                if (typeof fCallback == "function") { }
            }
        });
    };

    function readPrivateKey(strIdElement) {
        if (typeof strIdElement == "string") {
            var oItem = document.getElementById(strIdElement);
            if (oItem != null) {
                oItem.setAttribute("accept", ".key");
                oItem.value = "";
                oItem.onchange = function (evt) {
                    if (oItem.files.length > 0) {
                        var fileReader = new FileReader();
                        fileReader.onload = function () {
                            try {
                                var base64Key = fileReader.result.split("base64,")[1];
                                var decode = forge.util.decode64(base64Key);
                                if (fileSizePrivateKey != decode.length) {
                                    alert("No se ha leído la llave privada");
                                    strPrivateKey = null;
                                    return;
                                }
                                strPrivateKey = base64Key;
                            } catch (e) {
                                alert(e);
                            }
                        };
                        fileReader.onerror = function (e) {
                            alert("Ocurrió un error al leer la llave privada: " + e);
                        };
                        fileReader.readAsDataURL(oItem.files[0]);
                        fileSizePrivateKey = oItem.files[0].size;
                    }
                };
            } else {
                alert(NOT_FOUND.replace(":id", strIdElement));
            }
        } else {
            alert("El argumento no tiene un formato válido se require un valor tipo cadena");
        }
    }

    function deriveKeyV1(password, salt, iterations, keySizeInBits, ivSizeInBits) {
        var bytePassword = "";
        var ab = [];
        for (var i = 0; i < password.length; i++) {
            bytePassword += password.charCodeAt(i).toString();
            ab[i] = password.charCodeAt(i).toString();
        }

        var keySize = keySizeInBits / 8;
        var ivSize = ivSizeInBits / 8;
        var md = forge.md.md5.create();
        md.start();
        md.update(password);
        var result = md.update(salt);
        var aux = result.digest().data;
        for (var i = 1; i < iterations; i++) {
            md.start();
            aux = md.update(aux).digest().data;

        }

        var key = null;
        var iv = [];
        key = aux.substr(0, keySize);
        iv = aux.substr(keySize);
        return [key, iv];

    }

    function validateModules(a, b) {
        var iguales = true;
        var i = 0;
        while (typeof a[i] != "undefined" && typeof b[i] != "undefined") {
            if (a[i] != b[i]) {
                iguales = false;
                break;
            }
            i++;
        }
        return iguales;
    }
    function decodeItem(dataCoded) {
        var offset = 0x02;
        var sizeData = "0x" + forge.util.bytesToHex(dataCoded[1]);

        if (sizeData > 0x80) {
            var dByte = sizeData - 0x80;
            offset = 02 + dByte;
            sizeData = 00;
            for (var n = 0; n < dByte; n++) {
                sizeData = (sizeData << 0x08) | dataCoded[02 + n];
            }
        }

        var data = [];
        data = dataCoded.slice(offset);
        return data;
    }
    function firelKey(strKey, certModule, cert, strPass) {
        var format = ASN1HEX.getDecendantHexTLVByNthList(strKey, 0, [0]);
        var algoritmId = ASN1HEX.getDecendantHexTLVByNthList(format, 0, [0]);
        var citer = ASN1HEX.getDecendantHexTLVByNthList(format, 0, [1]);
        var iteraciones = ASN1HEX.getDecendantHexTLVByNthList(citer, 0, [1]);
        var salt = ASN1HEX.getDecendantHexTLVByNthList(citer, 0, [0]);
        citer = decodeItem(decodeItem(salt));
        var saltos = ASN1HEX.getDecendantHexTLVByNthList(iteraciones, 0, [0]);
        var encryptedData = ASN1HEX.getDecendantHexTLVByNthList(strKey, 0, [1]);
        var privateKey = encryptedData.substring(encryptedData.indexOf(ASN1HEX.getDecendantHexTLVByNthList(encryptedData, 0, [0])));

        saltos = parseInt(saltos, 16);
        if (isNaN(saltos)) {
            throw "Error al decodificar la llave privada";
        }
        var deriveKey = deriveKeyV1(strPass, forge.util.hexToBytes(citer), saltos, 64, 64);
        try {
            var cipher = forge.des.createDecryptionCipher(deriveKey[0]);
            cipher.start(deriveKey[1]);
            cipher.update(forge.util.createBuffer(forge.util.hexToBytes(privateKey)));
            cipher.finish();
            var result = cipher.output.toHex();
            if (result.indexOf("30") != 0) {
                throw "No se ha podido acceder a la llave privada";
            } else {
                var pkey = privateKeyObject(hex2b64(cipher.output.toHex()));
                var moduloPkey = pkey.n;
                if (!validateModules(certModule, moduloPkey.data)) {
                    throw "No existe relación entre el certificado y la llave privada seleccionada";
                } else {
                    oPrivateKey = hexToBase64(result);
                    strCertificate = cert;
                    return [0, "La verificación del par de llaves ha sido satisfactoria"];
                }
            }
        } catch (e) {
            throw e;
        }
    }

    function validateKeyPairs(strPass, fCallback) {
        var oResult = {};
        if (strCertificate != null) {
            if (strPrivateKey != null) {
                var oResult = {};
                if (typeof strPass != "undefined") {
                    if (strCerPem != null && strPrivateKey != null) {
                        var certificate = new X509();
                        certificate.readCertPEM(strCerPem);
                        var certModule = certificate.subjectPublicKeyRSA.n;

                        try {
                            var privateKeyDecoded = ASN1.decode(Base64.unarmor(strPrivateKey));
                            var privateKeyHex = obtieneLlavePrivada(privateKeyDecoded.toHexString(), strPass);
                            var privateKeyB64 = hexToBase64(privateKeyHex);
                            var rsakey = getKeyFromPlainPrivatePKCS8Hex(privateKeyHex);
                            var moduloPrivada = rsakey.n;
                        } catch (e) {
                            if (e.indexOf("PKCS8 private key(code:001)") != -1) {
                                oResult.state = -97;
                                oResult.description = "La contraseña para acceder a la llave privada no es correcta, intente de nuevo.";
                            } else if (e.indexOf("this only supports pkcs5PBES2") != -1) {
                                try {
                                    var description = firelKey(privateKeyDecoded.toHexString(), certModule, hexToBase64(certificate.hex), strPass);
                                    if (description.length == 2) {
                                        oResult.state = description[0];
                                        oResult.description = description[1];
                                    }
                                } catch (e) {
                                    oResult.state = -95;
                                    oResult.description = e;
                                }
                            } else {
                                oResult.state = -95;
                                oResult.description = "Error: " + e;
                            }
                            if (typeof fCallback != "undefined") {
                                fCallback(oResult);
                            }
                            return;
                        }
                        if (typeof moduloPrivada != "undefined") {
                            if (certModule.equals(moduloPrivada)) {
                                oPrivateKey = privateKeyB64;
                                strCertificate = hexToBase64(certificate.hex);
                                oResult.state = 0;
                                oResult.description = "La verificación del par de llaves ha sido satisfactoria";
                            } else {
                                oResult.estado = -99;
                                oResult.descripcion = "El certificado no corresponde con la llave privada proporcionada";
                            }
                        } else {
                            oResult.state = -98;
                            oResult.description = "No se ha podido entender la llave privada";
                        }
                    }
                }
                if (typeof fCallback != "undefined") {
                    fCallback(oResult);
                }

            } else {
                //alert("No se ha cargado la llave privada");
                MostrarMensaje("No se ha cargado la llave privada", "error", "", "LlavePrivadaNOSeleccionada");
            }

        } else {
            //alert("No se ha cargado la llave el certificado");
            MostrarMensaje("No se ha cargado la llave el certificado", "error", "", "CertificadoNOSeleccionado");
        }
    }

    function readPfx(strId) {
        if (typeof strId != "undefined") {
            var oItem = document.getElementById(strId);
            if (oItem != null) {
                oItem.setAttribute("accept", ".pfx");
                oItem.value = "";
                oItem.onchange = function (e) {
                    if (oItem.files.length > 0) {
                        var fileReader = new FileReader();
                        fileReader.onload = function (e) {
                            var dataItems = e.target.result.split("base64,");
                            var decode = forge.util.decode64(dataItems[1]);
                            if (decode.length != fileSizePfx) {
                                strPfx = null;
                                alert("Ha ocurrido un error en la lectura del PFX: No se ha podido leer el archivo por completo");
                                return;
                            }
                            if (dataItems.length == 2) {
                                strPfx = dataItems[1];
                            }
                        }
                        fileReader.readAsDataURL(oItem.files[0]);
                        fileSizePfx = e.target.files[0].size;
                        fileReader.onerror = function (e) {
                            strPfx = null;
                            alert("Ha ocurrido un error en la lectura del archivo PFX " + e.target.error.code);
                        };
                    }
                };
            }
        }

    }

    function openPfx(strPass, fCallback) {
        var oResult = {};
        if (typeof strPass != "string") {
            strPass = String(strPass);
        }

        if (typeof strPfx != "undefined") {
            try {
                var pfxDerEncoded = forge.util.decode64(strPfx);
                var pfxAsn1Encoded = forge.asn1.fromDer(pfxDerEncoded);
                var pfx = forge.pkcs12.pkcs12FromAsn1(pfxAsn1Encoded, strPass);
                strPass = null;
                var keyBags = pfx.getBags({
                    bagType: forge.pki.oids.pkcs8ShroudedKeyBag
                });
                var bag = keyBags[forge.pki.oids.pkcs8ShroudedKeyBag][0];
                var privateKey = bag.key;
                var keyAsn1Encoded = forge.pki.privateKeyToAsn1(privateKey);
                var keyDerEncoded = forge.asn1.toDer(keyAsn1Encoded);
                oPrivateKey = forge.util.encode64(keyDerEncoded.getBytes());
                var certBags = pfx.getBags({
                    bagType: forge.pki.oids.certBag
                });
                var certificate = certBags[forge.pki.oids.certBag][0].cert;
                var certAsn1Encoded = forge.pki.certificateToAsn1(certificate);
                var certDerEncoded = forge.asn1.toDer(certAsn1Encoded);
                strCertificate = forge.util.encode64(certDerEncoded.getBytes());
                oResult.state = 0;
                oResult.description = "Encapsulado abierto satisfactoriamente";
            } catch (e) {
                oResult.state = -97;
                oResult.description = (e.message.indexOf("password") > 0 ? "Frase de acceso no válido" : e.message);
            }
        } else {
            oResult.state = -98;
            oResult.description = "No se ha especificado el encapsulado PFX";
        }
        if (typeof fCallback == "function") {
            fCallback(oResult);
        }
    }

    function signPKCS1(strText, iAlgoritm, iCodification, fCallback, bInternalRegisterSign, strVector) {
        var oResult = {};
        var mainArgs = arguments;
        if (oPrivateKey != null) {
            try {
                var privateKey = privateKeyObject(oPrivateKey);
                var kindDigest = null;
                switch (iAlgoritm) {
                    case fielnet.Digest.MD5:
                        kindDigest = forge.md.md5.create();
                        break;
                    case fielnet.Digest.SHA1:
                        kindDigest = forge.md.sha1.create();
                        break;
                    case fielnet.Digest.SHA2:
                        kindDigest = forge.md.sha256.create();
                        break;
                    case fielnet.Digest.SHA512:
                        kindDigest = forge.md.sha512.create();
                        break;
                    default:
                        kindDigest = forge.md.sha1.create();
                }

                switch (iCodification) {
                    case fielnet.Encoding.UTF8:
                    case fielnet.Encoding.B64:
                        break;
                    default:
                        iCodification = fielnet.Encoding.UTF8;
                }
                if (iCodification == fielnet.Encoding.B64) {
                    try {
                        kindDigest.update(forge.util.text.utf8.decode(forge.util.binary.base64.decode(strText)), "utf8");
                    } catch (encodingException) {
                        kindDigest.update(forge.util.decode64(strText), "raw");
                    }
                } else {
                    kindDigest.update(strText, "utf8");
                }
                oResult.state = 0;
                oResult.sign = forge.util.encode64(privateKey.sign(kindDigest));

            } catch (e) {
                oResult.state = -98;
                oResult.description = "Error " + e;
            }
        } else {
            oResult.state = -99;
            oResult.description = "No existe una llave privada para realizar la firma";
        }
        if (typeof fCallback == "function") {
            if (iCodification != fielnet.Encoding.B64) {
                strText = forge.util.binary.base64.encode(forge.util.text.utf8.encode(strText));

            }
            if (oResult.state == 0) {
                if (bInternalRegisterSign != true) {
                    ajaxRequest({
                        url: CONTROLLER,
                        data: "metodo=pkcs1&codificacion=" + iCodification + "&original=" + strText + "&firma=" + oResult.sign + "&cert=" + strCertificate + "&evidence=" + evidence + "&referencia=" + strReferencia + extraParameters,
                        method: "POST",
                        error: function (data) { },
                        complete: function (data) { },
                        success: function (data, status, xmlhttp) {
                            try {
                                var oJSONResult = JSON.parse(data);
                                oJSONResult.sign = oResult.sign;
                                if (typeof strVector != "undefined") {
                                    oJSONResult.vectorSigned = mainArgs[4];
                                }
                                fCallback(oJSONResult);
                            } catch (e) {
                                var oError = {};
                                oError.state = -99;
                                oError.description = "Error al leer respuesta del servidor: " + e;
                                fCallback(oError);
                            }
                        }
                    });
                } else {
                    var oTempResult = {};
                    oTempResult.state = 0;
                    oTempResult.description = "Firma realizada correctamente";
                    oTempResult.sign = oResult.sign;
                    if (typeof fCallback == "function") {
                        fCallback(oTempResult);
                    }

                }
            } else {
                fCallback(oResult);
            }
        }
    };

    function signPKCS1WithKeyPairs(strCertificateParam, strPrivateKeyParam, strPass, strText, iAlgoritm, iCodification, fCallback, bInternalRegisterSign) {
        strCertificate = strCertificateParam;
        strPrivateKey = strPrivateKeyParam;
        validateKeyPairs(strPass, function (oResult) {
            if (oResult.state == 0) {
                signPKCS1(strText, iAlgoritm, iCodification, function (oSignResult) {
                    if (typeof fCallback == "function") {
                        fCallback(oSignResult);
                    }
                }, bInternalRegisterSign);
            } else {
                if (typeof fCallback == "function") {
                    fCallback(oResult);
                }
            }
        });

    }
    function signPkcs1WithPfx(strPfxParam, strPass, strText, iAlgoritm, iCodification, fCallback, bInternalRegisterSign) {
        strPfx = strPfxParam;
        openPfx(strPass, function (oResult) {
            if (oResult.state == 0) {
                signPKCS1(strText, iAlgoritm, iCodification, function (oSignResult) {
                    if (typeof fCallback == "function") {
                        fCallback(oSignResult);
                    }
                }, bInternalRegisterSign);
            } else {
                if (typeof fCallback == "function") {
                    fCallback(oResult);
                }
            }
        });
    }

    function verifySign(strCadenaOriginal, strFirma, strCertificate, fCallback) {
        ajaxRequest({
            url: CONTROLLER,
            data: "metodo=pkcs1&codificacion=3&original=" + strCadenaOriginal + "&firma=" + strFirma + "&cert=" + strCertificate + "&referencia=" + strReferencia,
            method: "POST",
            complete: function (data, status, jqxhr) { },
            error: function (data, status, jqxhr) {
                if (console.log) {
                    console.log(data);
                }
            },
            success: function (data, status, jqxhr) {
                var JSONResponse = JSON.parse(data);
                if (typeof fCallback == "function") {
                    fCallback(JSONResponse);
                }
            }
        });
    }

    function setReferencia(referencia) {
        strReferencia = referencia;
    }
    function padding(val) {
        return val.length == 1 ? "0" + val : val;
    }

    function getVectorFile(strDigest, iCodification, fCallback) {
        var oDate = new Date();
        var strDate = oDate.getFullYear().toString().substring(2) + "" + padding(oDate.getMonth().toString()) + "" + padding(oDate.getDate().toString()) + "" + padding(oDate.getHours().toString()) + "" + padding(oDate.getMinutes().toString()) + "" + padding(oDate.getSeconds().toString()) + "Z";
        ajaxRequest({
            url: CONTROLLER,
            method: "POST",
            data: "metodo=der&digest=" + strDigest + "&fecha=" + strDate + "&referencia=" + strReferencia,
            success: function (dataDer, status, xmlhttp) {
                var oResult = JSON.parse(dataDer);
                if (oResult.state == 0) {
                    signPKCS1(oResult.data, iCodification, fielnet.Encoding.B64, function (oDataSigned) {
                        ajaxRequest({
                            url: CONTROLLER,
                            data: "metodo=vector&vector=" + oResult.data + "&firma=" + oDataSigned.sign + "&cert=" + strCertificate + "&referencia=" + strReferencia + "&id=" + oResult.transfer + extraParameters,
                            method: "POST",
                            success: function (oResponse, status, xmlhttp) {
                                if (typeof fCallback == "function") {
                                    var oJSONResponse = JSON.parse(oResponse);
                                    oJSONResponse.sign = oDataSigned.sign;
                                    oJSONResponse.digest = strDigest;
                                    oJSONResponse.vectorSigned = oDataSigned.vectorSigned;
                                    fCallback(oJSONResponse);
                                }
                            },
                            error: function (data, status, xmlhttp) {
                                if (typeof fCallback == "function") {
                                    fCallback(data);
                                }
                            }
                        });
                    }, true, oResult.data);
                } else {
                    alert("Ha ocurrido un error: " + oResult.description);
                }
            }
        });
    }

    function signPKCS7(file, iChunkSize, iCodification, fCallbackChunk, fCallbackComplete, fCallbackError, bVector) {
        if (typeof file == "string") {
            iCodification = 0;
            var strBinDigestion = forge.util.decode64(file);
            switch (strBinDigestion.length) {
                case 16:
                    iCodification = fielnet.Digest.MD5;
                    break;
                case 20:
                    iCodification = fielnet.Digest.SHA1;
                    break;
                case 32:
                    iCodification = fielnet.Digest.SHA2;
                    break;
                case 64:
                    iCodification = fielnet.Digest.SHA512;
                    break;
                default:
                    if (typeof fCallback == "function") {
                        fCallback({
                            state: -244,
                            description: "Digestión no válido longitud: " + strBinDigestion.length
                        });
                    }

                    return;
            }
            getVectorFile(file, iCodification, function (oResponse) {
                if (typeof fCallbackComplete == "function") {
                    fCallbackComplete(oResponse);
                }

            });
        }

        else {
            var fileSize = file.size;
            if (typeof iChunkSize == "undefined") {
                iChunkSize = 10000;
            }
            var chunkSize = iChunkSize * 1024; // bytes
            var offset = 0;
            var block = null;
            var digest = null;
            switch (iCodification) {
                case fielnet.Digest.MD5:
                    digest = forge.md.md5.create();
                    break;
                case fielnet.Digest.SHA1:
                    digest = forge.md.sha1.create();
                    break;
                case fielnet.Digest.SHA2:
                    digest = forge.md.sha256.create();
                    break;
                case fielnet.Digest.SHA512:
                    digest = forge.md.sha512.create();
                    break;
                default:
                    digest = forge.md.sha1.create();
            }

            var readBlock = function (evt) {
                if (evt.target.error == null) {
                    offset += evt.target.result.byteLength;
                    var binary = "";
                    var bytes = new Uint8Array(evt.target.result);
                    var length = bytes.length;
                    for (var i = 0; i < length; i++) {
                        binary += String.fromCharCode(bytes[i]);
                    }
                    digest.update(binary);
                    if (typeof fCallbackChunk == "function") {
                        fCallbackChunk((offset * 100) / fileSize);
                    }
                } else {
                    if (typeof callbackError == "function") {
                        callbackError("Ha ocurrido un error leyendo archivo: " + evt.target.error);
                    }
                    return;
                }
                if (offset >= fileSize) {
                    if (bVector) {
                        getVectorFile(forge.util.encode64(digest.digest().data), iCodification, function (oResponse) {
                            if (typeof fCallbackComplete == "function") {
                                fCallbackComplete(oResponse);
                            }

                        });
                    } else {
                        signPKCS1(forge.util.encode64(digest.digest().data), iCodification, fielnet.Encoding.B64, function (data) {
                            data.digest = forge.util.encode64(digest.digest().data);
                            fCallbackComplete(data);
                        }, true);
                    }
                    return;
                }
                block(offset, chunkSize, file);
            };

            block = function (_offset, length, _file) {
                var fileReader = new FileReader();
                var blob = _file.slice(_offset, length + _offset);
                fileReader.onload = readBlock;
                fileReader.readAsArrayBuffer(blob);
            };
            block(offset, chunkSize, file);
        }
    }

    function getFileDigest(file, iChunkSize, iAlgoritm, fCallback, fCallbackError) {
        var fileSize = file.size;
        if (typeof iChunkSize == "undefined") {
            iChunkSize = 10000;
        }
        var chunkSize = iChunkSize * 1024; // bytes
        var offset = 0;
        var block = null;
        var digest = null;

        switch (iAlgoritm) {
            case fielnet.Digest.MD5:
                digest = forge.md.md5.create();
                break;
            case fielnet.Digest.SHA1:
                digest = forge.md.sha1.create();
                break;
            case fielnet.Digest.SHA2:
                digest = forge.md.sha256.create();
                break;
            case fielnet.Digest.SHA512:
                digest = forge.md.sha512.create();
                break;
            default:
                digest = forge.md.sha1.create();
        }

        var readBlock = function (evt) {
            if (evt.target.error == null) {
                offset += evt.target.result.byteLength;
                var binary = "";
                var bytes = new Uint8Array(evt.target.result);
                var length = bytes.length;
                for (var i = 0; i < length; i++) {
                    binary += String.fromCharCode(bytes[i]);
                }
                digest.update(binary);

            } else {
                if (typeof fCallbackError == "function") {
                    fCallbackError("Ha ocurrido un error leyendo archivo: " + evt.target.error);
                }
                return;
            }
            if (offset >= fileSize) {
                if (typeof fCallback == "function") {
                    fCallback(forge.util.encode64(digest.digest().data));
                }
                return;
            }
            block(offset, chunkSize, file);
        };

        block = function (_offset, length, _file) {
            var fileReader = new FileReader();
            var blob = _file.slice(_offset, length + _offset);
            fileReader.onload = readBlock;
            fileReader.readAsArrayBuffer(blob);
        };
        block(offset, chunkSize, file);

    }

    function propertyLoaded(strProperty) {
        var loaded = true;

        switch (strProperty) {
            case "certificate":
                loaded = (strCertificate != null && typeof strCertificate == "string");
                break;
            case "key":
                loaded = (oPrivateKey != null && typeof oPrivateKey == "string");
                break;
            case "pfx":
                loaded = (strPfx != null && typeof strPfx == "string");
                break;
        }

        return loaded;

    }
    //Métodos de utilidad

    function saveInStorage(strStorage, strKey, strElement) {
        var oResult = {};
        var isPropertyLoaded = propertyLoaded(strElement);
        if (!isPropertyLoaded) {
            oResult.state = -99;
            oResult.description = "No se ha cargado el elemento deseado";
        } else {
            var strProperty = "";
            switch (strStorage) {
                case fielnet.Storages.LOCAL_STORAGE:
                case fielnet.Storages.SESSION_STORAGE:
                    break;
                default:
                    strStorage = fielnet.Storages.SESSION_STORAGE;
            }
            switch (strElement) {
                case "certificate":
                    strProperty = strCertificate;
                    break;
                case "key":
                    strProperty = oPrivateKey;
                    break;
                case "pfx":
                    strProperty = strPfx;
                    break;
            }
            if (strStorage == fielnet.Storages.LOCAL_STORAGE) {
                localStorage.setItem(strKey, strProperty);
            } else {
                sessionStorage.setItem(strKey, strProperty);
            }
            oResult.state = 0;
            oResult.description = "Elemento guardado correctamente";
        }
        return oResult;
    }

    function loadElementFromStorage(strStorage, strKey, strElement) {
        var oResult = {};
        var inStorage = true;
        switch (strStorage) {
            case fielnet.Storages.LOCAL_STORAGE:
            case fielnet.Storages.SESSION_STORAGE:
                break;
            default:
                inStorage = false;

        }
        if (inStorage) {
            var strItem = (strStorage == fielnet.Storages.LOCAL_STORAGE ? localStorage.getItem(strKey) : sessionStorage.getItem(strKey));
            if (strItem == null) {
                oResult.state = -99;
                oResult.description = "No se encontró elemento con la llave asociativa: '" + strKey + "' dentro del almacén " + (strStorage == fielnet.Storages.LOCAL_STORAGE ? "localStorage" : "sessionStorage");
            } else {
                switch (strElement) {
                    case "certificate":
                        strCertificate = strItem;
                        var certHex = forge.util.bytesToHex(forge.util.decode64(getCertificate()))
                        strCerPem = KJUR.asn1.ASN1Util.getPEMStringFromHex(certHex, 'CERTIFICATE');
                        break;
                    case "key":
                        strPrivateKey = strItem;
                        oPrivateKey = strPrivateKey;
                        break;
                    case "pfx":
                        strPfx = strItem;
                        break;

                }
                oResult.state = 0;
                oResult.description = "Elemento cargado correctamente";
            }
        } else {
            oResult.state = -99;
            oResult.description = "El almacén especificado '" + strStorage + "' no existe";
        }
        return oResult;
    }

    function parseObject(oData) {
        var oTransfer = {};

        for (var prop in oData) {
            eval("oTransfer." + prop + "='" + oData[prop] + "';");
        }
        return oTransfer;
    }
    function saveTransfers(oData) {
        var oResult = {};
        if (oData.length == 3) {
            if (typeof oData[0] == "object") {
                var bSaved = false;
                var strData = (oData[1] == fielnet.Storages.LOCAL_STORAGE ? localStorage.getItem(oData[2]) : sessionStorage.getItem(oData[2]));
                if (strData == null) {
                    var aItems = [];
                    aItems.push(parseObject(oData[0]));
                    var JSONTransfer = JSON.stringify(aItems);
                    var oStorage = (oData[1] == fielnet.Storages.LOCAL_STORAGE ? localStorage : sessionStorage);
                    oStorage.setItem(oData[2], JSONTransfer);
                    bSaved = true;
                } else {
                    var oStorage = (oData[1] == fielnet.Storages.LOCAL_STORAGE ? localStorage : sessionStorage);
                    var strData = oStorage.getItem(oData[2]);
                    var JSONItems = JSON.parse(strData);
                    //Quitamos el registro previamente realizado
                    for (var idx in JSONItems) {
                        if (JSONItems[idx].serie == oData[0].serie) {
                            JSONItems.splice(idx, 1);
                        }
                    }
                    JSONItems.push(parseObject(oData[0]));
                    oStorage.setItem(oData[2], JSON.stringify(JSONItems));
                    bSaved = true;
                }
                if (bSaved) {
                    oResult.state = 0;
                    oResult.description = "Objeto guardado correctamente";
                } else {
                    oResult.state = -98;
                    oResult.description = "Objeto no guardado";
                }
            } else {
                oResult.state = -99;
                oResult.description = "Formato de datos no válido";
            }
        }
    }

    function getTransfers(iStorage, strKey) {
        var storage = null;
        switch (iStorage) {
            case fielnet.Storages.LOCAL_STORAGE:
                storage = localStorage;
                break;
            case fielnet.Storages.SESSION_STORAGE:
                storage = sessionStorage;
                break;

        }
        if (storage == null) {
            return null;
        } else {
            try {
                return JSON.parse(storage.getItem(strKey));
            } catch (e) {
                return null;
            }
        }
    }

    function clearTransfers(iStorage, strKey) {
        var oStorage = null;
        switch (iStorage) {
            case fielnet.Storages.LOCAL_STORAGE:
                oStorage = localStorage;
                break;
            case fielnet.Storages.SESSION_STORAGE:
                oStorage = sessionStorage;
                break;

        }
        if (oStorage != null) {
            if (typeof strKey == "string") {
                oStorage.removeItem(strKey);
            } else {
                oStorage.clear();
            }
        }
    }

    function signFileDigest(iSource, strId, iFormat, iAlgoritm, fCallback) {
        var oResult = {};
        if (typeof strId == "string") {

            switch (iSource) {
                case fielnet.Digest.Source.USER:
                    if (iFormat == fielnet.Format.HEX) {
                        strId = forge.util.encode64(forge.util.hexToBytes(strId));
                    }
                    getVectorFile(strId, iAlgoritm, function (oResponse) {
                        if (typeof fCallback == "function") {
                            oResult = oResponse;
                        }
                    });
                    break;
                case fielnet.Digest.Source.DOM:
                    var element = document.getElementById(strId);
                    if (element) {
                        try {
                            var strJsonData = JSON.parse(element.value);
                            var digestiones = strJsonData.digestiones;
                            if (digestiones) {
                                for (var i = 0; i < digestiones.length; i++) {
                                    var oFile = digestiones[i];
                                    if (iFormat == fielnet.Format.HEX) {
                                        oFile.digestion = forge.util.encode64(forge.util.hexToBytes(oFile.digestion));
                                    }

                                    getVectorFile(oFile.digestion, iAlgoritm, function (oResponse) {
                                        if (typeof fCallback == "function") {
                                            oResult = oResponse;
                                        }

                                    });
                                }
                            } else {
                                oResult.state = -55;
                                oResult.description = "No hay datos que procesar digitalmente";
                            }
                        } catch (e) {
                            oResult.state = -56;
                            oResult.description = "El formato de datos a procesar no es válido";
                        }
                    } else {
                        oResult.state = -54;
                        oResult.description = "El id:'" + strId + "'  no existe en el DOM";
                    }
                    break;
                default:
                    oResult.state = -55;
                    oResult.description = "Origen de datos no válido";
            }

        } else {
            oResult.state = -67;
            oResult.description = "Tipo de valor no válido para el identificador del elemento origen de los datos";
        }
        if (typeof fCallback == "function") {
            fCallback(oResult);
        }
    }
    function addExtraParameters(strUrl) {
        if (strUrl.indexOf("&") != 0) {
            strUrl = "&" + strUrl;
        }
        extraParameters = strUrl;
    }

    function signFileFromPath(strCentralSource, strTargetSource, iAlgorithm, fCallback) {
        ajaxRequest({
            url: CONTROLLER,
            data: "metodo=getCeltralDigest&source=" + forge.util.encode64(strCentralSource) + "&target=" + forge.util.encode64(strTargetSource) + "&cert=" + strCertificate,
            method: "POST",
            error: function (data) { },
            complete: function (data) { },
            success: function (data, status, xmlhttp) {
                try {
                    var oJSONResult = JSON.parse(data);
                    if (oJSONResult.state == 0) {
                        var digestionCalculada = oJSONResult.digest;
                        addExtraParameters("&digestion=" + digestionCalculada);
                        signPKCS1(digestionCalculada, iAlgorithm, fielnet.Encoding.B64, function (e) {
                            fCallback(e);
                        });

                    } else {
                        if (typeof fCallback == "function") {
                            fCallback(oJSONResult);
                        }
                    }
                } catch (e) {
                    var oError = {};
                    oError.state = -99;
                    oError.description = "Error al leer respuesta del servidor: " + e;
                    fCallback(oError);
                }
            }
        });

    }



    function signPKCS1Token(strSignerId, strText, iAlgorithm, iCodification, fCallback) {
        cadenaOriginal = strText;
        codification = iCodification;
        if (cadenaOriginal && strSignerId && iAlgorithm && iCodification && (fCallback && typeof fCallback == "function")) {

            if (aFirmantesCentrales.length == 0) {
                fCallback({
                    state: -234,
                    description: "No se han cargado firmantes "
                });
                return;
            }
            var bExists = false;
            for (var i in aFirmantesCentrales) {
                if (aFirmantesCentrales[i] == strSignerId) {
                    bExists = true;
                    break;
                }
            }

            if (!bExists) {
                fCallback({
                    state: -235,
                    description: "El firmante central no existe getSigners"
                });
                return;
            }

            try {
                if (!ws) {
                    ws = new WebSocket(urlWS);
                }
            } catch (e) { }
            ws.onmessage = function (d) {
                var jsonData = JSON.parse(d.data);
                switch (jsonData.accion) {
                    case "resultadoFirma":
                        switch (jsonData.estado) {
                            case 0:
                                var firma = jsonData.firma;
                                var certificado = jsonData.certificadoBase64;
                                var strVerifyString = (codification == fielnet.Encoding.B64 ? cadenaOriginal : forge.util.binary.base64.encode(forge.util.text.utf8.encode(cadenaOriginal)));
                                setReferencia("Verificación firma Token");
                                verifySign(strVerifyString, firma, certificado, function (dataResponse) {
                                    if (dataResponse.state == 0) {
                                        fCallback({
                                            state: 0,
                                            description: "Satisfactorio",
                                            commonName: dataResponse.commonName,
                                            sign: firma,
                                            transfer: dataResponse.transfer,
                                            hexSerie: dataResponse.hexSerie
                                        });
                                    } else {
                                        fCallback({
                                            state: -233,
                                            description: "Error al autenticar firma digital avanzada"
                                        });
                                    }
                                    delete codification;
                                    delete cadenaOriginal;

                                });
                                break;
                            default:
                                if (typeof fCallback == "function") {
                                    fCallback({
                                        state: jsonData.estado,
                                        description: jsonData.descripcion
                                    });
                                }
                                break;
                        }

                        break;
                }
            };

            var obj = {
                accion: "signData",
                datos: (iCodification != fielnet.Encoding.B64 ? forge.util.binary.base64.encode(forge.util.text.utf8.encode(cadenaOriginal)) : cadenaOriginal),
                firmante: strSignerId,
                algoritmoDigestion: iAlgorithm
            };
            sendData(obj)
        } else {
            if (typeof fCallback == "function") {
                fCallback({
                    state: -233,
                    description: "Argumentos no válidos"
                });
            }

        }

    }

    function signPKCS7Token(strSignerId, iAlgorithm, strB64Digest, strSourcePath, strSourceTarget, fCallback) {
        if (strSignerId && strB64Digest && (fCallback && typeof fCallback == "function")) {
            if (aFirmantesCentrales.length == 0) {
                fCallback({
                    state: -234,
                    description: "No se han cargado firmantes "
                });
                return;
            }
            var bExists = false;
            for (var i in aFirmantesCentrales) {
                if (aFirmantesCentrales[i] == strSignerId) {
                    bExists = true;
                    break;
                }
            }

            if (!bExists) {
                fCallback({
                    state: -235,
                    description: "El firmante central no existe getSigners"
                });
                return;
            }

            try {
                if (!ws) {
                    ws = new WebSocket(urlWS);
                }
            } catch (e) { }
            ws.onmessage = function (d) {
                var jsonData = JSON.parse(d.data);
                switch (jsonData.accion) {
                    case "resultadoFirma":
                        switch (jsonData.estado) {
                            case 0:
                                var firma = jsonData.firma;
                                var certificado = jsonData.certificadoBase64;
                                ajaxRequest({
                                    url: CONTROLLER,
                                    data: "metodo=vector&vector=" + vectorFile + "&firma=" + firma + "&cert=" + certificado + "&referencia=" + strReferencia + extraParameters,
                                    method: "POST",
                                    success: function (oResponse, status, xmlhttp) {
                                        if (typeof fCallback == "function") {
                                            var oJSONResponse = JSON.parse(oResponse);
                                            oJSONResponse.sign = firma;
                                            oJSONResponse.digest = strB64Digest;
                                            oJSONResponse.vectorSigned = vectorFile;
                                            oJSONResponse.centralDigest = jsonData.digestionDocumentoCentral;
                                            delete vectorFile;
                                            fCallback(oJSONResponse);
                                        }
                                    },
                                    error: function (data, status, xmlhttp) {
                                        if (typeof fCallback == "function") {
                                            fCallback(data);
                                        }
                                    }
                                });
                                break;
                            default:
                                if (typeof fCallback == "function") {
                                    fCallback({
                                        state: jsonData.estado,
                                        description: jsonData.descripcion
                                    });
                                }
                                break;
                        }

                        break;
                }
            };

            var oDate = new Date();
            var strDate = oDate.getFullYear().toString().substring(2) + "" + padding(oDate.getMonth().toString()) + "" + padding(oDate.getDate().toString()) + "" + padding(oDate.getHours().toString()) + "" + padding(oDate.getMinutes().toString()) + "" + padding(oDate.getSeconds().toString()) + "Z";
            ajaxRequest({
                url: CONTROLLER,
                method: "POST",
                data: "metodo=der&digest=" + strB64Digest + "&fecha=" + strDate,
                success: function (dataDer, status, xmlhttp) {
                    var oResult = JSON.parse(dataDer);
                    if (oResult.state == 0) {
                        vectorFile = oResult.data;
                        var obj = {
                            accion: "signData",
                            datos: oResult.data,
                            firmante: strSignerId,
                            algoritmoDigestion: iAlgorithm
                        };
                        if (strSourcePath != null && strSourceTarget != null) {

                            obj.digestion = strB64Digest;
                        }
                        if (strSourcePath != null) {
                            obj.ruta = strSourcePath;
                        }
                        if (strSourceTarget != null) {
                            obj.target = strSourceTarget;
                        }
                        sendData(obj)
                    } else {
                        alert("Ha ocurrido un error: " + oResult.description);
                    }
                }
            });
        } else {
            if (typeof fCallback == "function") {
                fCallback({
                    state: -233,
                    description: "Argumentos no válidos"
                });
            }
        }
    }

    function sendData(oData) {
        var iIntervalId = -1;
        var wsAux = null;
        var div = document.createElement("div");
        var totalIntents = 0;
        if (ws) {
            if (ws.readyState == WebSocket.OPEN) {
                ws.send(JSON.stringify(oData));
            } else {
                div.style = "style='position:fixed; bottom:0; right:0; border:1px solid red; width:150px; height:20px; margin-right:3px; margin-bottom:3px;'";
                div.innerHTML = "Conectando con token";
                div.id = "divDescripcionConexionToken02349955";
                document.getElementsByTagName('body')[0].appendChild(div);

                iIntervalId = setInterval(function () {
                    totalIntents++;
                    try {
                        var text = div.innerHTML;
                        switch (text.length) {
                            case 20:
                                div.innerHTML = "Conectando con token .";
                                break;

                            case 22:
                                div.innerHTML = "Conectando con token ..";
                                break;

                            case 23:
                                div.innerHTML = "Conectando con token ...";
                                break;
                            case 24:
                                div.innerHTML = "Conectando con token";
                                break;
                        }
                        if (ws.readyState == WebSocket.CLOSED) {
                            if (wsAux == null) {
                                wsAux = {};
                                wsAux.onmessage = ws.onmessage;
                                wsAux.onoerror = ws.onerror;
                                wsAux.onclose = ws.onclose;
                            }
                            ws = new WebSocket(urlWS);
                            if (ws != null) {
                                ws.onopen = function () {
                                    var element = document.getElementById("divDescripcionConexionToken02349955");
                                    element.outerHTML = "";
                                    delete element;
                                    ws.onmessage = wsAux.onmessage
                                    ws.onerror = wsAux.onerror;
                                    ws.onclose = ws.onclose;
                                    resendData(oData);
                                    clearInterval(iIntervalId);
                                };

                            }
                        }
                    } catch (e) { }
                    if (totalIntents == 100) {
                        clearInterval(iIntervalId);
                        div.innerHTML = "No se pudo conectarse al servicio token";
                    }
                }, 1000);

            }
        }
    }
    function resendData(oData) {
        var iIntervalSend = setInterval(function () {
            if (ws.readyState == WebSocket.OPEN) {
                clearInterval(iIntervalSend);
                sendData(oData);
            }
        }, 100);
    }
    function closeTokenConnection() {
        if (ws) {
            ws.close();
        }
    }

    function getSigners(fCallback) {
        if (fCallback && typeof fCallback == "function") {
            try {
                if (!ws) {
                    ws = new WebSocket(urlWS);
                } else {
                    var obj = {
                        accion: "getSigners"
                    };
                    sendData(obj);
                }
            } catch (e) {
                console.log(e);
            }
            ws.onerror = function (d) {
                ws = null;
                fCallback({
                    state: -237,
                    description: "Ha ocurrido un error en la comunicación"
                });
            };
            ws.onclose = function (d) { };
            ws.onmessage = function (d) {
                var jsonData = JSON.parse(d.data);
                if (jsonData.state == 0) {
                    aFirmantesCentrales = jsonData.signers;
                }
                ws.onclose = null;
                ws.onopen = null;
                ws.onmessage = null;
                fCallback(jsonData);

            };
            ws.onopen = function () {
                var obj = {
                    accion: "getSigners"
                };
                sendData(obj);
            };

        }

    }

    function getSignerCertificate(strId, fCallback) {
        if (aFirmantesCentrales.length == 0) {
            fCallback({
                state: -234,
                description: "No se han cargado firmantes "
            });
            return;
        }
        var bExists = false;
        for (var i in aFirmantesCentrales) {
            if (aFirmantesCentrales[i] == strId) {
                bExists = true;
                break;
            }
        }

        if (!bExists) {
            fCallback({
                state: -235,
                description: "El firmante central no existe getSigners"
            });
            return;
        }
        try {
            if (!ws) {
                ws = new WebSocket(urlWS);
            }
        } catch (e) { }
        ws.onmessage = function (d) {
            var jsonData = JSON.parse(d.data);
            switch (jsonData.estado) {
                case 0:
                    var certificado = jsonData.certificadoBase64;
                    fCallback({
                        state: jsonData.estado,
                        description: jsonData.descripcion,
                        certificate: certificado
                    });
                    break;
                default:
                    if (typeof fCallback == "function") {
                        fCallback({
                            state: jsonData.estado,
                            description: jsonData.descripcion
                        });
                    }
                    break;
            }
        }
        var obj = {
            accion: "getCertificate",
            firmante: strId
        };
        sendData(obj)
    }
    function doSignPDF(strB64Digest, iAlgorithm, strSource, strTarget, fCallback) {
        try {
            if (!ws) {
                ws = new WebSocket(urlWS);
            }
            else {
                var obj = {
                    accion: "getDigestPDF",
                    digestion: strB64Digest,
                    certificate: getCertificate(),
                    ruta: strSource,
                    target: strTarget,
                    algoritmoDigestion: iAlgorithm
                };
                sendData(obj)
            }
        } catch (e) { }
        ws.onerror = function () {
            if (typeof fCallback == "function") {
                fCallback({ state: 233, description: "Error de comunicación servicio token" });
            }
        };
        ws.onmessage = function (d) {
            try {
                var jsonData = JSON.parse(d.data);
            }
            catch (e) {
                if (typeof fCallback == "function") {
                    fCallback({ state: 224, description: e.message });
                }
                return;
            }
            switch (jsonData.estado) {
                case 0:
                    var digestionCentral = jsonData.digestionDocumentoCentral;
                    if (digestionCentral) {
                        firma.signPKCS1(digestionCentral, fielnet.Digest.SHA1, fielnet.Encoding.B64, function (pkcs1) {
                            if (pkcs1.state == 0) {
                                ws.onmessage = function (pdf) {
                                    var jsonBlinda = JSON.parse(pdf.data);
                                    jsonBlinda.sign = pkcs1.sign;
                                    jsonBlinda.transfer = pkcs1.transfer;
                                    jsonBlinda.date = pkcs1.date;
                                    jsonBlinda.commonName = pkcs1.commonName;
                                    jsonBlinda.hexSerie = pkcs1.hexSerie;
                                    if (jsonBlinda) {
                                        delete jsonBlinda.firma;
                                        delete jsonBlinda.certificadoBase64;
                                        delete jsonBlinda.accion;
                                    }
                                    if (typeof fCallback == "function") {
                                        fCallback(jsonBlinda);
                                    }
                                };
                                var obj = {
                                    accion: "signPDF",
                                    signature: pkcs1.sign,
                                    digestion: digestionCentral,
                                    algoritmoDigestion: iAlgorithm
                                };
                                sendData(obj);
                            }
                            else {
                                if (typeof fCallback == "function") {
                                    fCallback({ state: pkcs1.state, description: pkcs1.description });
                                }
                            }
                        });
                    }
                    else {
                        if (typeof fCallback == "function") {
                            fCallback({ state: 2334, description: "No se obtuvo digestión del PDF a blindar" });
                        }
                    }
                    break;
                default:
                    if (typeof fCallback == "function") {
                        fCallback({
                            state: jsonData.estado,
                            description: jsonData.descripcion
                        });
                    }
                    break;
            }
        };
        ws.onopen = function () {
            var obj = {
                accion: "getDigestPDF",
                digestion: strB64Digest,
                certificate: getCertificate(),
                ruta: strSource,
                target: strTarget,
                algoritmoDigestion: iAlgorithm
            };
            sendData(obj)
        };

    }
    function signPKCS7PDF(oFile, iAlgorithm, strSource, strTarget, fCallback) {
        if (strSource == strTarget) {
            if (typeof fCallback == "function") {
                fCallback({ state: 2234, description: "La ruta origen y destino no deben de ser la misma" });
            }
            return;
        }
        if (typeof oFile == "object") {
            getFileDigest(oFile, 1000, iAlgorithm, function (strB64Digest) {
                doSignPDF(strB64Digest, iAlgorithm, strSource, strTarget, fCallback);
            }, function (strError) {
                if (typeof fCallback == "function") {
                    fCallback({
                        state: -240,
                        description: strError
                    });
                }
            });
        }
        else if (typeof oFile == "string") {
            doSignPDF(oFile, iAlgorithm, strSource, strTarget, fCallback);
        }
    }
    function getCertificateObject(strB64Certificate) {
        try {
            var certDerBytes = forge.util.decode64(strB64Certificate);
            var obj = forge.asn1.fromDer(certDerBytes);
            var cert = forge.pki.certificateFromAsn1(obj);
            return cert;
        } catch (e) {
            return null;
        }
    }

    function getAttributeFromSubject(certificate, strOID) {
        for (var i = 0; i < certificate.subject.attributes.length; i++) {
            if (certificate.subject.attributes[i].type == strOID) {
                var attribute = certificate.subject.attributes[i].value;
                if (certificate.subject.attributes[i].valueTagClass == 12) {
                    attribute = forge.util.decodeUtf8(attribute);
                }
                return attribute;
            }
        }
        return null;
    }
    function getRFC(strB64Certificate) {
        var oid = "2.5.4.45";
        if (typeof strB64Certificate == "undefined") {
            if (strCertificate == null) {
                return null;
            }
            return getAttributeFromSubject(getCertificateObject(strCertificate), oid);
        }
        else {
            return getAttributeFromSubject(getCertificateObject(strB64Certificate), oid);
        }
    }

    function encryptFileDigest(b64Digest, fCallback) {
        var objResult = {};
        var pki = forge.pki;
        var asn1 = forge.asn1;
        if (oPrivateKey == null || typeof oPrivateKey == "undefined") {
            objResult.state = -2223;
            objResult.description = "No existe elemento llave privada para relizar firma";
            return;
        }
        var emsaPkcs1v15encode = function (algorithm, digest) {
            var oid;
            if (algorithm in pki.oids) {
                oid = pki.oids[algorithm];
            } else {
                var error = new Error('Unknown message digest algorithm.');
                error.algorithm = algorithm;
                throw error;
            }
            var oidBytes = asn1.oidToDer(oid).getBytes();
            var digestInfo = asn1.create(
                asn1.Class.UNIVERSAL, asn1.Type.SEQUENCE, true, []);
            var digestAlgorithm = asn1.create(
                asn1.Class.UNIVERSAL, asn1.Type.SEQUENCE, true, []);
            digestAlgorithm.value.push(asn1.create(
                asn1.Class.UNIVERSAL, asn1.Type.OID, false, oidBytes));
            digestAlgorithm.value.push(asn1.create(
                asn1.Class.UNIVERSAL, asn1.Type.NULL, false, ''));
            var digest = asn1.create(
                asn1.Class.UNIVERSAL, asn1.Type.OCTETSTRING,
                false, forge.util.decode64(digest));
            digestInfo.value.push(digestAlgorithm);
            digestInfo.value.push(digest);
            return asn1.toDer(digestInfo).getBytes();
        };
        var digestDecoded = forge.util.binary.base64.decode(b64Digest);
        var algorithm = "";
        switch (digestDecoded.length) {
            case 20:
                algorithm = "sha1";
                break;
            case 32:
                algorithm = "sha256";
                break;
            case 64:
                algorithm = "sha512";
                break;
            default:
                objResult.state = -233;
                objResult.description = "Algoritmo de digestión no válido";
                break;
        }
        if (algorithm) {
            var encoded = emsaPkcs1v15encode(algorithm, b64Digest);
            var privateKey = privateKeyObject(oPrivateKey);
            var signature = forge.util.encode64(pki.rsa.encrypt(encoded, privateKey, 1));
            objResult.state = 0;
            objResult.description = "Satisfactorio";
            objResult.sign = signature;
            objResult.algorithm = algorithm;
            fCallback(objResult);
        }
        else {
            fCallback(objResult);
        }
    }
    //Interfaz publica
    return {

        /*
         * Valida si el web browser objetos de html5 que se requieren para firma digital
         * @param strMessage en caso de que se especifique un valor para este parámetro, si el web browser
         *  no soporta html5 se despliega una ventana con el mensaje con el mensaje especificado
         * @return true| false en caso de soportar o no html5
         */
        validateWebBrowser: function (strMessage) {
            return validateWebBrowser(strMessage);
        },
        /*
         * Método encargado de realizar la lectura del certificado
         * @param Id del elemento file con el que selccionarán el certificado del usuario
         * @return
         */
        readCertificate: function (strIdElement) {
            readCertificate(strIdElement);
        },
        /*
         * Obtiene el certificado leído previamente
         * @param
         * @return Regresa el certificado codificado en base 64, en caso de que no se haya leído previamente, regresa null
         */
        getCertificate: function () {
            return getCertificate();
        },
        /*
         * Decodifica certificado
         * @param strCertificate representa el certificado codificado en base 64
         * @param bOcsp determina si se realizará consulta ocsp del certificado
         * @param fCallback función que tendrá los resultados de la operación realizada
         *  El objeto pasado como argumento a fCallback contiene las siguientes propiedades:
         *  -state código de resultado del proceso
         *  -description descripción textual del código del proceso
         *  -hexSerie Número de serie del certificado codificado en hexadecimal
         *  -notBefore Inicio de la vigencia del certificado
         *  -notAfter Fin de la vigencia del certificado
         *  -publicKey: LLave publica
         *  -fingerPrint: Huella digital del certificado
         *
         *
         *  -transfer: Identificador de la operación en el buscriptográfico
         *  -date: fecha en la que se realizó la operación
         *  -evidence: firma digital del buscriptográfico
         *
         *   PROPIETARIO
         *
         *  -subjectName: Nombre
         *  -subjectEmail: Correo electrónico
         *  -subjectOrganization: Organización a la que pertenece
         *  -subjectDepartament: Departamente a la que pertenece
         *  -subjectState: Estado donde habita
         *  -subjectCountry: País donde habita
         *  -subjectRFC: RFC
         *  -subjectCURP: CURP
         *
         *  Emisor
         *
         *  -issuerName: Nombre
         *  -issuerEmail: Correo electrónico
         *  -issuerOrganization: Organización
         *  -issuerDepartament: Departamento
         *  -issuerState: Estado
         *  -issuerCountry: País
         *  -issuerRFC : RFC
         *  -issuerCURP: CURP
        
         * @return
         */
        decodeCertificate: function (strCertificate, bOcsp, fCallback) {
            if (typeof strCertificate == "string" && typeof bOcsp == "boolean") {
                decodeCertificate(strCertificate, bOcsp, fCallback);
            } else if (typeof strCertificate == "boolean") {
                decodeCertificate(getCertificate(), strCertificate, bOcsp);
            }

        },
        /*
         * Método encargado de realizar la lectura de la llave privada
         * @param Id del elemento file con el que selccionarán la llave privada del usuario
         * @return
         */
        readPrivateKey: function (strIdElement) {
            readPrivateKey(strIdElement);
        },
        /*
         * Método encargado de realizar la lectura del certificado y la llave privada del usuario
         * este método funciona como una abreviación para readCertificate() y readPrivateKey()
         * @param strIdCertificate id del elemento file que realizará la lectura del certificado
         * @param strIdPrivateKey id del elemento file que realizar la lectura de la llave privada
         * @return
         */
        readCertificateAndPrivateKey: function (strIdCertificate, strIdPrivateKey) {
            readCertificate(strIdCertificate);
            readPrivateKey(strIdPrivateKey);
        },
        /*
         * Método encargado de validar la relación entre el par de llaves proporcionados
         * @param strPass representa la frase de acceso al par de llaves
         * @fCallback función que entregará los detalles de la operación realizada
         *   El objeto que  recibe como argumento fCallback contiene 2 propiedades
         *   -state : Código de resultado
         *   -description: Descripción textual del código de resultado
         *
         * @return
         */
        validateKeyPairs: function (strPass, fCallback) {
            validateKeyPairs(strPass, fCallback);
        },
        /*
         * Método encargado de realizar la lectura del pfx
         * @param strIdElement representa el id del elemento file que realizará la carga del PFX
         * @return
         */
        readPfx: function (strIdElement) {
            readPfx(strIdElement);
        },
        /*
         * Método encargado de acceder al par de llaves del certificado
         * @param strPass representa la frase de acceso al encapsulado
         * @param fCallback función que entregará los detalles de la operación realizada
         *   El objeto que  recibe como argumento fCallback contiene 2 propiedades
         *   -state : Código de resultado
         *   -description: Descripción textual del código de resultado
         * @return
         */
        openPfx: function (strPass, fCallback) {
            openPfx(strPass, fCallback);
        },
        /*
         * Realiza la firma digital de una cadena
         * @param strText texto a firmar
         * @param iAlgoritm valor numérico que define el tipo de digestión aplicada al contenido que se firmará digitalmente
         *  Los valores para este parámetro están definidos dentro del objeto Digest y son:
         *  fielnet.Digest.MD5
         *  fielnet.Digest.SHA1
         *  fielnet.Digest.SHA2
         * @param iCodification tipo de codificación aplicada al contenido a firmar
         *  Los valores para este parámetro están definidos dentro del objeto Encoding y son:
         *  fielnet.Encoding.UTF8
         *  fielnet.Encoding.B64
         * @param fCallback función que entregará los detalles de la operación realizada
         *  El objeto que recibe como argumento fCallback contiene las siguientes propiedades
         *   -state: Código de resultado
         *   -description: Descripción textual del código de resultado
         *   -transfer : Id del registro en el buscriptográfico
         *   -date: Fecha en la que se realizó la operación
         *   -evidence : Firma digital del buscriptográfico
         *   -commonName: Nombre del propietario que realizó la firma
         *   -hexSerie : Número de serie en formato hexadecimal del certificado
         *   -sign: firma digital
         * @return
         */
        signPKCS1: function (strText, iAlgoritm, iCodification, fCallback) {
            signPKCS1(strText, iAlgoritm, iCodification, fCallback, false);
        },
        /*
         * Realizar firma digital de cadenas usando par de llaves
         * @param strCertificate certificado del usuario codificado en base 64
         * @param strPrivateKey llave privada codificada en base 64
         * @param strPass frase de acceso del par de llaves
         * @param strText cadena a firmar
         * @param iAlgoritm valor numérico que define el tipo de digestión aplicada al contenido que se firmará digitalmente
         *  Los valores para este parámetro están definidos dentro del objeto Digest y son:
         *  fielnet.Digest.MD5
         *  fielnet.Digest.SHA1
         *  fielnet.Digest.SHA2
         * @param iCodification tipo de codificación aplicada al contenido a firmar
         *  Los valores para este parámetro están definidos dentro del objeto Encoding y son:
         *  fielnet.Encoding.UTF8
         *  fielnet.Encoding.B64
         * @param fCallback función con los resultados del proceso de firma
         *  El objeto que recibe como argumento fCallback contiene las siguientes propiedades
         *   -state: Código de resultado
         *   -description: Descripción textual del código de resultado
         *   -transfer : Id del registro en el buscriptográfico
         *   -date: Fecha en la que se realizó la operación
         *   -evidence : Firma digital del buscriptográfico
         *   -commonName: Nombre del propietario que realizó la firma
         *   -hexSerie : Número de serie en formato hexadecimal del certificado
         *   -sign: firma digital
         * @return
         */
        signPKCS1WithKeyPairs: function (strCertificate, strPrivateKey, strPass, strText, iAlgoritm, iCodification, fCallback) {
            signPKCS1WithKeyPairs(strCertificate, strPrivateKey, strPass, strText, iAlgoritm, iCodification, fCallback, false);
        },
        /*
         * Realizar firma digital de cadenas usando PFX
         * @param strPfx pfx codificado en base 64
         * @param strPass frase de acceso del par de llaves
         * @param strText cadena a firmar
         * @param iAlgoritm valor numérico que define el tipo de digestión aplicada al contenido que se firmará digitalmente
         *  Los valores para este parámetro están definidos dentro del objeto Digest y son:
         *  fielnet.Digest.MD5
         *  fielnet.Digest.SHA1
         *  fielnet.Digest.SHA2
         * @param iCodification tipo de codificación aplicada al contenido a firmar
         *  Los valores para este parámetro están definidos dentro del objeto Encoding y son:
         *  fielnet.Encoding.UTF8
         *  fielnet.Encoding.B64
         * @param fCallback función con los resultados del proceso de firma
         *  El objeto que recibe como argumento fCallback contiene las siguientes propiedades
         *   -state: Código de resultado
         *   -description: Descripción textual del código de resultado
         *   -transfer : Id del registro en el buscriptográfico
         *   -date: Fecha en la que se realizó la operación
         *   -evidence : Firma digital del buscriptográfico
         *   -commonName: Nombre del propietario que realizó la firma
         *   -hexSerie : Número de serie en formato hexadecimal del certificado
         *   -sign: firma digital
         * @return
         */
        signPkcs1WithPfx: function (strPfx, strPass, strText, iAlgoritm, iCodification, fCallback) {
            signPkcs1WithPfx(strPfx, strPass, strText, iAlgoritm, iCodification, fCallback, false);
        },

        /*
         * Método encargado de verificar la firma digital
         * @param strCadenaOriginal cadena que fue sobre la cual se realizó la firma digital
         * @param strFirma firma codificada en base 64
         * @param strCertificate   certificado codificado en base 64
         * @param fCallback función con los resultados del proceso de verificación
         *  El objeto que recibe como argumento fCallback contiene las siguientes propiedades
         *   -state: Código de resultado
         *   -description: Descripción textual del código de resultado
         *   -transfer : Id del registro en el buscriptográfico
         *   -date: Fecha en la que se realizó la operación
         *   -evidence : Firma digital del buscriptográfico
         *   -commonName: Nombre del propietario que realizó la firma
         *   -hexSerie : Número de serie en formato hexadecimal del certificado
         *   -sign: firma digital
         * @return
         */
        verifySign: function (strCadenaOriginal, strFirma, strCertificate, iCodification, fCallback) {
            // var cadenaOriginal = (iCodification == fielnet.Encoding.B64 ? strCadenaOriginal : forge.util.encode64(strCadenaOriginal));
            var cadenaOriginal = (iCodification == fielnet.Encoding.B64 ? strCadenaOriginal : forge.util.binary.base64.encode(forge.util.text.utf8.encode(strCadenaOriginal)));

            verifySign(cadenaOriginal, strFirma, strCertificate, fCallback);
        },
        /*
         * Realiza la lectura de un archivo determinado
         * @param file representa el archivo a leer
         * @param iChunkSize valor numérico
         * @param iAlgoritm representa el tipo de digestión aplicada al contenido del archivo
         *  Los valores para este parámetro están definidos dentro del objeto Digest y son:
         *  fielnet.Digest.MD5
         *  fielnet.Digest.SHA1
         *  fielnet.Digest.SHA2
         * @param fCallbackComplete función que entregará los detalles del proceso de firmado
         * @param fCallbackError función que entregará los detalles del error ocurrido en la lectura del archivo
         *  El objeto que recibe como argumento fCallback contiene las siguientes propiedades
         *   -state: Código de resultado
         *   -description: Descripción textual del código de resultado
         *   -transfer : Id del registro en el buscriptográfico
         *   -date: Fecha en la que se realizó la operación
         *   -evidence : Firma digital del buscriptográfico
         *   -commonName: Nombre del propietario que realizó la firma
         *   -hexSerie : Número de serie en formato hexadecimal del certificado
         *   -sign: firma digital
         *   -digest: digestión del archivo
         * @return
         */
        signPKCS7: function (file, iChunkSize, iAlgoritm, fCallbackChunk, fCallbackComplete, fCallbackError) {
            signPKCS7(file, iChunkSize, iAlgoritm, fCallbackChunk, fCallbackComplete, fCallbackError, true);
        },
        signFilePCKS1: function (file, iChunkSize, iAlgoritm, fCallbackChunk, fCallbackComplete, fCallbackError) {
            signPKCS7(file, iChunkSize, iAlgoritm, fCallbackChunk, fCallbackComplete, fCallbackError, false);
        },
        getFileDigest: function (file, iChunkSize, iAlgoritm, fCallback, fCallbackError) {
            getFileDigest(file, iChunkSize, iAlgoritm, fCallback, fCallbackError);
        },
        /*
         * Guarda el certificado en algún almacén especificado
         * @param Valor numérico que define en que almacén se guardará el certificado
         * @param strKey 'llave' del arreglo asociativo con el que se accederá al certificado
         * @return regresa un objeto con dos propiedades
         *  state valor numérico con el código de resultado
         *  description valor de tipo cadena con los detalles del código de resultado
         *
         */
        saveCertificate: function (strStorage, strKey) {
            return saveInStorage(strStorage, strKey, 'certificate');
        },

        /*
         * Carga el certificado dentro de la instancia
         * @param Valor numérico que define de que almacén se cargará el certificado
         * @param strKey 'llave' del arreglo asociativo con el que se guardó el certificado
         * @return regresa un objeto con dos propiedades
         *  state valor numérico con el código de resultado
         *  description valor de tipo cadena con los detalles del código de resultado
         *
         */
        loadCertificate: function (strStorage, strKey) {
            return loadElementFromStorage(strStorage, strKey, 'certificate');
        },
        /*
         * Guarda la llave privada en algún almacén especificado
         *
         * Importante: Guarda encontenido del archivo .key, no la llave privada desencriptada.
         *
         * @param Valor numérico que define en que almacén se guardará la llave privada
         * @param strKey 'llave' del arreglo asociativo con el que se accederá a la llave privada
         * @return regresa un objeto con dos propiedades
         *  state valor numérico con el código de resultado
         *  description valor de tipo cadena con los detalles del código de resultado
         *
         */
        saveCertificateAndPrivateKey: function (strStorage, strKeyCertificate, strKeyPrivate) {
            var obj = {};
            obj = saveInStorage(strStorage, strKeyPrivate, 'key');
            if (obj.state == 0) {
                obj = saveInStorage(strStorage, strKeyCertificate, 'certificate');
            }

            return obj;
        },
        /*
         * Carga la llave privada dentro de la instancia
         * @param Valor numérico que define de que almacén se cargará la llave privada
         * @param strKey 'llave' del arreglo asociativo con el que se guardó la llave privada
         * @return regresa un objeto con dos propiedades
         *  state valor numérico con el código de resultado
         *  description valor de tipo cadena con los detalles del código de resultado
         *
         */
        loadCertificateAndPrivateKey: function (strStorage, strKeyCertificate, strKeyPrivateKey) {
            var obj = {};
            obj = loadElementFromStorage(strStorage, strKeyCertificate, "certificate");
            if (obj.state == 0) {
                obj = loadElementFromStorage(strStorage, strKeyPrivateKey, 'key');
            }
            return obj;
        },
        /*
         * Guarda el PFX en algún almacén especificado
         * @param Valor numérico que define en que almacén se guardará al PFX
         * @param strKey 'llave' del arreglo asociativo con el que se accederá al PFX
         * @return regresa un objeto con dos propiedades
         *  state valor numérico con el código de resultado
         *  description valor de tipo cadena con los detalles del código de resultado
         *
         */
        savePfx: function (strStorage, strKey) {
            return saveInStorage(strStorage, strKey, 'pfx');
        },
        /*
         * Carga el pfx dentro de la instancia
         * @param Valor numérico que define de que almacén se cargará el pfx
         * @param strKey 'llave' del arreglo asociativo con el que se guardó el pfx
         * @return regresa un objeto con dos propiedades
         *  state valor numérico con el código de resultado
         *  description valor de tipo cadena con los detalles del código de resultado
         */
        loadPfx: function (strStorage, strKey) {
            return loadElementFromStorage(strStorage, strKey, 'pfx');
        },
        /*
         * Define el tipo de evidencias a generar
         * @param iEvidence
         * cuando es 0 no se Genera TSA ni NOM
         * cuando es 1 Sólo se genera TSA
         * cuando es 2 Sólo se genera NOM
         * Con cualquier otro valor se genera tanto TSA como NOM
         *
         */
        setEvidences: function (iEvidence) {
            if (!isNaN(iEvidence)) {
                evidence = iEvidence;
            } else {
                evidence = fielnet.Evidences.NONE;
            }
        },
        saveTransfers: function () {
            saveTransfers(arguments);
        },
        getTransfers: function (iStorage, strKey) {
            return getTransfers(iStorage, strKey);
        },
        clearTransfers: function (iStorage, strKey) {
            clearTransfers(iStorage, strKey);
        },
        setReferencia: function (strReferencia) {
            setReferencia(strReferencia);
        },
        /*
         * Método encargado de firmar digitalmente digestiones de archivos
         * @param iSource: Define el tipo de origen de las digestiones, los posibles valores son:
         *  fielnet.Source.USER: La digestión será proporcionada directamente por el usuario a este método
         *  fielnet.Source.DOM: La digestión estará contenida dentro del DOM bajo un formato JSON
         * @param strId: Contiene el identificador del elemento dentro del DOM que almacena la digestión a firmar en caso
         *  de que el fielnet.Source.DOM sea especificado como origen de datos. Caso contrario contiene la digestión
         *  proporcionada por el usuario
         * @param iFormat: Define el tipo de formato en la cual se proporciona la digestión los posibles valores son:
         *  fielnet.Format.HEX : Para una digestión en formato hexadecimal. Ejemplo: 5CB83D7CA46E84F72336A8FC9AF8AAD6
         *  fielnet.Format.B64: Para una digestión en formato Base 64. Ejemplo:XLg9fKRuhPcjNqj8mviq1g==
         * @param iAlgoritm valor numérico que define el tipo de digestión aplicada al vector que se firmará digitalmente
         *  Los valores para este parámetro están definidos dentro del objeto Digest y son:
         *  fielnet.Digest.MD5
         *  fielnet.Digest.SHA1
         *  fielnet.Digest.SHA2
         *  @param fCallback: Función que se invocará una vez que el proceso de firma haya finalizado.
         */
        signFileDigest: function (iSource, strId, iFormat, iAlgoritm, objDetails, fCallback) {
            signFileDigest(iSource, strId, iFormat, iAlgoritm, fCallback);
        },
        /*
         * Método encargado de porporcionar paramétros extras en las peticiones de firma hacia el componente Controlador
         * que pueden ser usados por el sistema del usuario.
         * Este método debe ser invocado previo a métodos de sign**
         */
        addExtraParameters: function (strUrl) {
            addExtraParameters(strUrl);
        },
        clearStorage: function (iStorage) {
            var state = false;
            switch (iStorage) {
                case fielnet.Storages.LOCAL_STORAGE:
                    localStorage.clear();
                    state = true;
                    break;
                case fielnet.Storages.SESSION_STORAGE:
                    sessionStorage.clear();
                    state = true;
                    break;
            }
            return state;
        },
        removeItemFromStorage: function (iStorage, strKey) {
            var state = false;
            switch (iStorage) {
                case fielnet.Storages.LOCAL_STORAGE:
                    if (localStorage.getItem(strKey) != null) {
                        localStorage.removeItem(strKey);
                        state = true;
                    }
                    break;
                case fielnet.Storages.SESSION_STORAGE:
                    if (sessionStorage.getItem(strKey) != null) {
                        sessionStorage.removeItem(strKey);
                        state = true;
                    }
                    break;
            }
            return state;
        },
        /*
         * Método encargado de firmar un archivo a partir de una ruta central
         *
         * @param strCentralSource Ruta absoluta del documento a firmar
         * @param strTargetSource Ruta absoluta destino donde quedará el documento firmado
         * @param iAlgorithm Tipo de digestión aplicada al documento
         *  Los valores para este parámetro están definidos dentro del objeto Digest y son:
         *  fielnet.Digest.MD5
         *  fielnet.Digest.SHA1
         *  fielnet.Digest.SHA2
         * @param fCallback Callback que se ejecutará cuando el proceso de firmado finalice
         */
        signFileFromPath: function (strCentralSource, strTargetSource, iAlgorithm, fCallback) {
            signFileFromPath(strCentralSource, strTargetSource, iAlgorithm, fCallback);
        },

        /*
         * Métodos encargados de trabajar con el dispositivo criptográfico token
         */

        /*
         * Método encargado de obtener la lista de los firmantes del almacén de windows
         * @param fCallback Callback invocada una vez que el proceso de conexión con el token finalice
         *  en dicha callback se proporciona un arreglo con los firmantes disponibles
         */
        getSigners: function (fCallback) {
            getSigners(fCallback);
        },
        getSignerCertificate: function (strId, fCallback) {
            if (typeof strId == "string") {
                getSignerCertificate(strId, fCallback);
            }
            else {
                if (typeof fCallback == "function") {
                    fCallback({ state: -222, description: "Argumentos no válidos" });

                }

            }

        },
        /*
         * Método encargado de firmar cadena de datos usando el dispositivo token
         * @param strSignerId Identificador del firmante, el cual fue obtenido a partir del método getSigners
         * @param strText Cadena de datos a firmar
         * @param iAlgorithm Algoritmo de digestión aplicado a la cadena original
         *  Los valores para este parámetro están definidos dentro del objeto Digest y son:
         *  fielnet.Digest.MD5
         *  fielnet.Digest.SHA1
         *  fielnet.Digest.SHA2
         * @param iCodification Codificación en la cual se proporciona la cadena original
         * @param fCallback Callback ejecutada una vez que el proceso de firma finalice
         */
        signPKCS1Token: function (strSignerId, strText, iAlgorithm, iCodification, fCallback) {
            signPKCS1Token(strSignerId, strText, iAlgorithm, iCodification, fCallback);
        },
        /*
         * Método encargado de firmar un documento
         * @param strSignerId Identificador del firmante, el cual fue obtenido a partir del método getSigners
         * @param oFile referencia al documento obtenida desde el elemento file
         * @param iAlgorithm Algoritmo de digestión aplicado a la cadena original
         *  Los valores para este parámetro están definidos dentro del objeto Digest y son:
         *  fielnet.Digest.MD5
         *  fielnet.Digest.SHA1
         *  fielnet.Digest.SHA2
         * @param fCallback Callback ejecutada una vez que el proceso de firma finalice
         */
        signPKCS7Token: function (strSignerId, oFile, iAlgorithm, strSourcePath, strTargetPath, fCallback) {
            if (arguments.length == 4) {
                fCallback = strSourcePath;
                strSourcePath = null;
                strTargetPath = null;
            }
            if (!oFile) {
                if (typeof fCallback == "function") {
                    fCallback({
                        state: -244,
                        description: "No se ha especificado archivo a firmar digitalmente"
                    });
                    return;
                }
            }
            if (typeof oFile == "object") {
                getFileDigest(oFile, 1000, iAlgorithm, function (strB64Digest) {
                    signPKCS7Token(strSignerId, iAlgorithm, strB64Digest, strSourcePath, strTargetPath, fCallback);
                }, function (strError) {
                    if (typeof fCallback == "function") {
                        fCallback({
                            state: -240,
                            description: strError
                        });
                    }
                });
            }
            else {
                iAlgorithm = 0;
                var strBinDigestion = forge.util.decode64(oFile);
                switch (strBinDigestion.length) {
                    case 16:
                        iAlgorithm = fielnet.Digest.MD5;
                        break;
                    case 20:
                        iAlgorithm = fielnet.Digest.SHA1;
                        break;
                    case 32:
                        iAlgorithm = fielnet.Digest.SHA2;
                        break;
                    case 64:
                        iAlgorithm = fielnet.Digest.SHA512;
                        break;
                    default:
                        if (typeof fCallback == "function") {
                            fCallback({
                                state: -244,
                                description: "Digestión no válido longitud: " + strBinDigestion.length
                            });
                        }

                        return;
                }
                signPKCS7Token(strSignerId, iAlgorithm, oFile, strSourcePath, strTargetPath, fCallback)
            }

        },
        signPKCS7PDF: function (oFile, iChunkSize, iAlgorithm, strSource, strTarget, fCallback) {
            signPKCS7PDF(oFile, iChunkSize, iAlgorithm, strSource, strTarget, fCallback);

        },
        /* 
         * Método encargado cerrar la conexión con el dispositivo token
        */
        closeTokenConnection: function () {
            closeTokenConnection();
        },
        getRFC: function (strB64Certificate) {
            return getRFC(strB64Certificate);

        },
        encryptFileDigest: function (b64Digest, fCallback) {
            encryptFileDigest(b64Digest, fCallback);
        }
    };
})();