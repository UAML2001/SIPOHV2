using DatabaseConnection;
using SIPOH;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.Web.SessionState;

public static class SessionManagerRegistroIniciales
{
    private static HttpSessionState Session => HttpContext.Current.Session;

    private static string GetString(string key)
    {
        return Session[key] as string;
    }

    private static void SetString(string key, string value)
    {
        Session[key] = value;
    }

    public static string TipoAsunto
    {
        get { return GetString("TipoAsunto"); }
        set { SetString("TipoAsunto", value); }
    }

    public static string TipoSolicitud
    {
        get { return GetString("TipoSolicitud"); }
        set { SetString("TipoSolicitud", value); }
    }
    public static string FechaRecepcion
    {
        get { return GetString("FechaRecepcion"); }
        set { SetString("FechaRecepcion", value); }
    }
    public static string NumeroFojas
    {
        get { return GetString("NumeroFojas"); }
        set { SetString("NumeroFojas", value); }
    }

    public static string QuienIngresa
    {
        get { return GetString("QuienIngresa"); }
        set { SetString("QuienIngresa", value); }
    }
    public static string NombreQuienIngresa
    {
        get { return GetString("NombreQuienIngresa"); }
        set { SetString("NombreQuienIngresa", value); }
    }
    public static string TipoRadicacion
    {
        get { return GetString("TipoRadicacion"); }
        set { SetString("TipoRadicacion", value); }
    }
    public static string Prioridad
    {
        get { return GetString("Prioridad"); }
        set { SetString("Prioridad", value); }
    }

}



