using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SIPOH.Firma;


    public class Controller
    {
        public WebServiceSoapClient Cliente
        {
            set;
            get;
        }
        public AuthSoapHd Autenticacion
        {
            set;
            get;
        }
        public string Operador
        {
            set;
            get;
        }
        public string Certificado
        {
            set;
            get;
        }
        public string CadenaOriginal
        {
            set;
            get;
        }
        public string Digestion
        {
            set;
            get;
        }
        public string Fecha
        {
            set;
            get;

        }
        public string Nom
        {
            set;
            get;
        }
        public string Tsa
        {
            set;
            get;
        }
        public string Firma
        {
            set;
            get;
        }
        public string Vector
        {
            set;
            get;
        }
        public string CadenaBase64
        {
            set;
            get;
        }
        public int Codificacion
        {
            set;
            get;
        }
        public Controller()
        {


            string entidad = (System.Web.Configuration.WebConfigurationManager.AppSettings["entidad"]);
            string usuario = ((System.Web.Configuration.WebConfigurationManager.AppSettings["usuario"]));
            string password = ((System.Web.Configuration.WebConfigurationManager.AppSettings["clave"]));
            this.Tsa = ((System.Web.Configuration.WebConfigurationManager.AppSettings["tsaid"]));
            this.Nom = ((System.Web.Configuration.WebConfigurationManager.AppSettings["nom"]));


            Cliente = new WebServiceSoapClient();
            Autenticacion = new AuthSoapHd();
            Autenticacion.Entidad = entidad;
            Autenticacion.Usuario = usuario;
            Autenticacion.Clave = password;


        }
        public string getDerResult()
        {
            string result = "";
            try
            {

                string param = Cliente.PwuDigestionExtendida(this.Digestion, this.Fecha);
                int estado = 0;
                string descripcion = "";
                if (param.Trim().Length == 0)
                {
                    estado = -99;
                    descripcion = "No se ha podido codificar parámetros digestión '" + this.Digestion + "' fecha '" + this.Fecha + "'";
                    result = "{\"state\":\"" + estado + "\",\"description\":\"" + descripcion + "\"}";
                }
                else
                {
                    result = "{\"state\":\"" + estado + "\",\"description\":\"" + descripcion + "\",\"data\":\"" + param + "\"}";
                }
            }
            catch (Exception e)
            {
                result = "{\"state\":\"" + -98 + "\",\"description\":\"" + e.Message + "\"}";
            }
            return result;
        }
        public string getCertificateDetails(string certificate, bool ocsp)
        {
            string result = "";
            try
            {
                CertificadoPropiedades properties = Cliente.PwuDecodificaCertificado(this.Autenticacion, ocsp == true ? "2" : "1", certificate, "Consulta de certificado", this.Tsa);
                result = "{\"state\":\"" + properties.Estado + "\",\"description\":\"" + properties.Descripcion + "\",\"hexSerie\":\"" + properties.HexSerie + "\",\"notBefore\":\"" + properties.FechaInicio + "\",\"notAfter\":\"" + properties.FechaFin + "\",\"subjectName\":\"" + properties.SubjectNombre + "\",\"subjectEmail\":\"" + properties.SubjectCorreo + "\",\"subjectOrganization\":\"" + properties.SubjectOrganizacion + "\",\"subjectDepartament\":\"" + properties.SubjectDepartamento + "\",\"subjectState\":\"" + properties.SubjectEstado + "\",\"subjectCountry\":\"" + properties.SubjectPais + "\",\"subjectRFC\":\"" + properties.SubjectRFC + "\",\"subjectCURP\":\"" + properties.SubjectCurp + "\",\"issuerName\":\"" + properties.IssuerNombre + "\",\"issuerEmail\":\"" + properties.IssuerCorreo + "\",\"issuerOrganization\":\"" + properties.IssuerOrganizacion + "\",\"issuerDepartament\":\"" + properties.IssuerDepartamento + "\",\"issuerState\":\"" + properties.IssuerEstado + "\",\"issuerCountry\":\"" + properties.IssuerPais + "\",\"issuerRFC\":\"" + properties.IssuerRFC + "\",\"issuerCURP\":\"" + properties.IssuerCurp + "\",\"publicKey\":\"" + properties.LlavePublica + "\",\"fingerPrint\":\"" + properties.Huella + "\",\"transfer\":\"" + properties.Id + "\",\"date\":\"" + properties.Fecha + "\",\"evidence\":\"" + properties.Evidencia + "\"}";
            }
            catch (Exception e)
            {
                result = "{\"state\":\"" + -99 + "\",\"description\":\"" + e.Message + "\"}";
            }
            return result;
        }
        public string validaCadena()
        {
            string result = "";
            try
            {
                Estado resultado = Cliente.PwuPkcs1Evidencias(this.Autenticacion, this.CadenaOriginal, this.Codificacion, this.Firma, this.Certificado, "Consulta validación firma", this.Tsa, this.Nom);

                //TResultado ResEstadoFirma =  new TResultado();
                //int Agregados = BdFirma.AgregarEstadoFirma(resultado, ref ResEstadoFirma);

                result = "{\"state\":\"" + resultado.Error + "\",\"description\":\"" + resultado.Descripcion + "\",\"transfer\":\"" + resultado.Id + "\",\"date\":\"" + resultado.Fecha + "\",\"evidence\":\"" + resultado.Evidencia + "\",\"commonName\":\"" + resultado.Cn + "\",\"hexSerie\":\"" + resultado.HexSerie + "\"}";
            }
            catch (Exception e)
            {
                result = "{\"state\":\"" + -99 + "\",\"description\":\"" + e.Message + "\"}";
            }
            return result;
        }
        public string firmaExtendida()
        {
            string result = "";
            try
            {
                Estado resultadoExtendida = Cliente.PwuPkcs1Extendido(this.Autenticacion, this.Vector, 3, this.Firma, this.Certificado, "Solicita PKCS1 extendido ", this.Tsa);
                result = "{\"state\":\"" + resultadoExtendida.Error + "\",\"description\":\"" + resultadoExtendida.Descripcion + "\",\"transfer\":\"" + resultadoExtendida.Id + "\",\"date\":\"" + resultadoExtendida.Fecha + "\",\"evidence\":\"" + resultadoExtendida.Evidencia + "\",\"commonName\":\"" + resultadoExtendida.Cn + "\",\"hexSerie\":\"" + resultadoExtendida.HexSerie + "\"}";
            }
            catch (Exception e)
            {
                result = "{\"state\":\"" + -99 + "\",\"description\":\"" + e.Message + "\"}";
            }
            return result;
        }
        public string encodeError(string detalles)
        {
            return "{\"state\":\"-95\",\"description\":\"" + detalles + "\"}";
        }
    }
