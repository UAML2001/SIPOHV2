<%@ WebHandler Language="C#" Class="Controlador" %>

using System;
using System.Web;
    using SIPOH.Firma;

public class Controlador : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        HttpRequest request = context.Request;
        HttpResponse response = context.Response;

        response.ContentType = "application/json";
        response.ContentEncoding = System.Text.Encoding.UTF8;
        if (request.HttpMethod == "POST")
        {
            string method = request.Params["metodo"];
            Controller controller = new Controller();
            switch (method)
            {
                case "der":
                    if (string.IsNullOrEmpty(request.Params["digest"]) || string.IsNullOrEmpty(request.Params["fecha"]))
                    {
                        response.Write(controller.encodeError("Parámetros de petición incompletos"));
                    }
                    else
                    {
                        controller.Digestion = request.Params["digest"].Replace(" ", "+");
                        controller.Fecha = request.Params["fecha"];
                        response.Write(controller.getDerResult());
                    }
                    break;
                case "decodecert":
                    string cert = request.Params["cert"];
                    string ocsp = request.Params["ocsp"];
                    if (ocsp == null)
                    {
                        ocsp = "false";
                    }
                    if (!string.IsNullOrEmpty(cert))
                    {
                        cert = cert.Replace(" ", "+");
                        bool realizarOcsp = true;
                        if (ocsp == "false")
                        {
                            realizarOcsp = false;
                        }

                        response.Write(controller.getCertificateDetails(cert, realizarOcsp));
                    }
                    else
                    {
                        response.Write(controller.encodeError("Parámetros de petición incompletos"));
                    }

                    break;
                case "pkcs1":
                    string codificacion = request.Params["codificacion"];
                    string cadenaOriginal = request.Params["original"];
                    string firma = request.Params["firma"];
                    string certificado = request.Params["cert"];
                    string evidence = request.Params["evidence"];
                    if (string.IsNullOrEmpty(cadenaOriginal) || string.IsNullOrEmpty(firma) || string.IsNullOrEmpty(certificado))
                    {
                        response.Write(controller.encodeError("Parámetros de petición incompletos"));
                    }
                    else
                    {
                        if (codificacion == "3")
                        {
                            controller.CadenaOriginal = cadenaOriginal.Replace(" ", "+");
                        }
                        else
                        {
                            controller.CadenaOriginal = cadenaOriginal;
                        }

                        if (string.IsNullOrEmpty(evidence))
                        {
                            evidence = "0";
                        }
                        switch (evidence)
                        {
                            case "0":
                                controller.Nom = "NA";
                                controller.Tsa = "NA";
                                break;
                            case "1":
                                controller.Nom = "NA";

                                break;

                            case "2":
                                controller.Tsa = "NA";
                                break;
                        }
                        controller.Firma = firma.Replace(" ", "+");
                        controller.Certificado = certificado.Replace(" ", "+");
                        controller.Codificacion = 3;
                        response.Write(controller.validaCadena());

                    }
                    break;
                case "vector":
                    string vector = request.Params["vector"];
                    string firmaVector = request.Params["firma"];
                    string certificadoBase64 = request.Params["cert"];
                    if (string.IsNullOrEmpty(vector) || string.IsNullOrEmpty(firmaVector) || string.IsNullOrEmpty(certificadoBase64))
                    {
                        response.Write(controller.encodeError("Parámetros de petición incompletos"));
                    }
                    else
                    {
                        controller.Vector = vector.Replace(" ", "+");
                        controller.Firma = firmaVector.Replace(" ", "+");
                        controller.Certificado = certificadoBase64.Replace(" ", "+");
                        response.Write(controller.firmaExtendida());
                    }
                    break;
          
            }

        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}