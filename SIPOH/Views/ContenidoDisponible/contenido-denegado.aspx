<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contenido-denegado.aspx.cs" Inherits="SIPOH.Views.ContendoDisponible.contenido_denegado" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-T3c6CoIi6uLrA9TneNEoa7RxnatzjcDSCmG1MXxSR1GAsXEV/Dwwykc2MPK8M2HN" crossorigin="anonymous">
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">
    <title>Acceso Denegado!</title>
</head>

<body class="d-flex justify-content-center align-items-center vh-100 text-center flex-column ">
    <form id="accesoDenegado" class="" runat="server">
        <i class="bi bi-shield-lock-fill text-success" style="font-size:100px;"></i>
        <h1 class="mb-5">¡Pagina no disponible!</h1>
        <span class="text-secondary">El contenido que desea ver, no esta disponible para tu perfil. </span>
        <br />
        <asp:Button runat="server"  ID="BotonSoporte" OnClick="BotonLogin_Click" class="btn fw-bold btn-outline-success mt-3 " text="Por favor, contacta a soporte! 😊" />   
    </form>    
        <p class="text-secondary mt-5 fw-light"><span class="text-success fw-bold">Nota: </span>Ya contactaste a soporte y si el acceso no se actualiza, por favor inicia sesión de nuevo.</p>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
</body>
</html>
