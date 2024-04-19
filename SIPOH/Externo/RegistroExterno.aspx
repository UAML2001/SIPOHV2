<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistroExterno.aspx.cs" Inherits="SIPOH.Externo.RegistroExterno" %>

<!DOCTYPE html>

<html xmlns="">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SIPOH PJEH - Ingreso</title>
    <link rel="shortcut icon" href="https://www.aseh.gob.mx/Shared/img/anticorrupcion/3.png" />
    <link href="../Content/css/loginexterno.css" rel="stylesheet" />
    <link rel="stylesheet"
        href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <link rel="stylesheet" href="https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/css/bootstrap.min.css" />
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.7/dist/umd/popper.min.js"
        integrity="sha384-zYPOMqeu1DAVkHiLqWBUTcbYfZ8osu1Nd6Z89ify25QV9guujx43ITvfi12/QExE"
        crossorigin="anonymous"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.0-alpha3/dist/js/bootstrap.min.js"
        integrity="sha384-Y4oOpwW3duJdCWv5ly8SCFYWqFDsfob/3GkgExXKV4idmbt98QcxXYs9UoXAB7BZ"
        crossorigin="anonymous"></script>
    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.11.1/font/bootstrap-icons.css">

        <link rel="stylesheet" type="text/css" href="//cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css">

    <script src="../Scripts/Firma/Conexion.js" type="text/javascript"></script>
    <script src="../Scripts/Firma/Firma.js" type="text/javascript"></script>
    <script src="../Scripts/Firma/FAccesoSistemasElectronicos.js?v=2.1" type="text/javascript"></script>


    <script type="text/javascript">


        var firma = new fielnet.Firma({
            subDirectory: "../Scripts/Firma",
            controller: "../Scripts/Firma/Controlador.ashx",
            ajaxAsync: false
        });
        $(function () {

            firma.readCertificate("FCertificado");
            firma.readPrivateKey("FLlavePrivada");

        });

    </script>

     <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
    <script src="../Scripts/Mensajes.js"></script>



</head>

<body>
    <header>
        <nav class="navbar bg-transparent">
            <div class="container">
            </div>
        </nav>
    </header>
    <form id="form1" runat="server" class="background-form pt-5 ">
        <div class="px-6">
            <div class="row d-flex justify-content-center align-content-center">
                <div class="col-10 col-sm-5 col-md-5 col-lg-4 col-xl-4 col-xxl-6 my-5 mx-3">
                    <div class="text-center">
                        <img src="https://www.aseh.gob.mx/Shared/img/anticorrupcion/3.png" alt="logo PJEH"
                            class="img-fluid col-8 col-xxl-4 col-xl-6 col-lg-8 col-md-7 col-sm-9" height="225" />
                        <h1 class="display-4 text-md" style="color: white"><b>SIPOH</b></h1>
                        <h5 style="color: white;" class="text-md">¡Bienvenido(a) al Sistema
                                Penal Digital del PJEGH!</h5>
                        <br />
                    </div>
                    <br />
                </div>
                <div class=""></div>
                <div class=" col-10 col-sm-6 col-md-5 col-lg-4 col-xl-3 col-xxl-4  ">
                    <div align="center" class=" bg-white boxshadow p-4 rounded"
                        style="width: auto; height: auto">
                        <div class="card-header-pills text-dark">
                            <%--<i class="bi bi-lock-fill text-danger" style="font-size:35px;"></i>--%>
                            <img src="https://img.icons8.com/3d-fluency/100/user-credentials.png" width="60px"
                                height="60px" id="login-cerrar" />
                            <h2 style="font-size: 27px"><b>REGISTRO CON FEJEH</b></h2>
                        </div>
                        <div class="form-group mt-3" style="width: 100%">


                                        
                            <div id="AceptarTC" class="m-3" style="text-align: center;">
                    <asp:CheckBox ID="ChkTerminosCondiciones" runat="server" Text="Aceptar Términos, Condiciones"
                        Font-Bold="true" Font-Size="Large" /><a style="margin-left: 10px;" class="fa fa-file-pdf-o" 
                            href="http://www.pjhidalgo.gob.mx/PortalVirtual/AIndex/Documentos/SINEJ/Lineamientos.pdf" target="_blank">Ver Documento</a>
                </div>
                                                                           
                            <div class="form-group d-flex flex-column">
                                <h4 class="title">Nombre(s):</h4>
                                    <asp:TextBox ID="TxtNombre" runat="server" CssClass="form-control form-control-sm"
                                        Width="100%" BorderColor="Gray" ></asp:TextBox>
                                </div>
                                                
                            <div class="form-group d-flex flex-column">
                                <h4 class="title">Apellido paterno:</h4>
                                    <asp:TextBox ID="TxtAPaterno" runat="server" CssClass="form-control form-control-sm"
                                        Width="100%" BorderColor="Gray" ></asp:TextBox>
                                </div>
                                                <div class="form-group d-flex flex-column">
                                <h4 class="title">Apellido materno:</h4>
                                    <asp:TextBox ID="TxtAMaterno" runat="server" CssClass="form-control form-control-sm"
                                        Width="100%" BorderColor="Gray" ></asp:TextBox>
                                </div>


                            <div class="col-12 mt-4">
                                <h4 class="title">Seleccione Certificado:</h4>
                                <input type="file" class="form-control-file" id="FCertificado">
                            </div>
                            <div class="col-12 mt-4">
                                <h4 class="title">Seleccione Llave privada:</h4>
                                <input type="file" class="form-control-file" id="FLlavePrivada">
                            </div>



                            <div class="form-group d-flex flex-column mt-4">
                                <h4 class="title">Clave de acceso:</h4>

                                <div class="input-group mb-2">
                                    <asp:TextBox ID="txtPass" runat="server" CssClass="form-control form-control-sm"
                                        TextMode="Password" Width="100%" BorderColor="Gray" ></asp:TextBox>
                                    <button class="btn btn-transparent" type="button" id="btnMostrarClave">
                                        <span
                                            class="fa fa-eye-slash fa-lg icon text-success"
                                            style="color: black"></span>
                                    </button>
                                </div>
                                <asp:HyperLink ID="hyperlink3" NavigateUrl="LoginExterno"
                                    Text="Ya tienes cuenta. Inicia Sesión" runat="server" />
                            </div>


                                                 
                            <div class="col-12 col-lg-12 mt-4 text-center">
                            <div class="col-12 my-4">
                                <button onclick="ValidarFirmaGenerarAcceso(); return false;" class="btn btn-success">REGISTRARME</button>
                                <asp:Button runat="server" ID="BtnActivarNE" Text="Generar" Style="display: none;" OnClick="BtnActivarNE_Click" />
                            </div>


                        </div>

                            
                            <div style="float: left; width: 100%;">
                        <asp:HiddenField ID="HFDatosFirmaUsuario" runat="server" />
                        <asp:HiddenField ID="HFDatosTransfer" runat="server" />
                    </div>

                        </div>
                 
                    </div>
                </div>
            </div>
        </div>
        <br />

    </form>
    <footer class="footer text-muted pb-3">
        <div class="container">
            <b style="color: white; font-size: 18px;">Copyright &copy; 2023
                   
                <asp:HyperLink Style="color: white" ID="hyperlink2" NavigateUrl="http://www.pjhidalgo.gob.mx/"
                    Text="Poder Judicial del Estado de Hidalgo" Target="_new" runat="server" />
                - SIPOH PJEH
                </b>
        </div>



    </footer>

    <script>
        $(document).ready(function () {
            $('#show_password').hover(function show() {
                //Cambiar el atributo a texto
                $('#txtPassword').attr('type', 'text');
                $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
            },
                function () {
                    //Cambiar el atributo a contraseña
                    $('#txtPassword').attr('type', 'password');
                    $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                });
        });


        $(document).ready(function () {
            $("input#txtPass").focus(function () {
                $(this).val('');
                $(this).get(0).type = 'password';
            });
            $("input#txtPass").click(function () {
                $(this).val('');
                $(this).get(0).type = 'password';
            });
            $('#btnMostrarClave').click(function () {
                var tipo = document.getElementById("txtPass");

                if (tipo.type == "password") {
                    var input = $('#txtPass').attr('type');
                    // alert (input);
                    $('#txtPass').attr('type', 'text');
                    $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
                } else {
                    var input = $('#txtPass').attr('type');
                    // alert (input);
                    $('#txtPass').attr('type', 'password');
                    $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
                }
            });
        });
        </script>


</body>


</html>
