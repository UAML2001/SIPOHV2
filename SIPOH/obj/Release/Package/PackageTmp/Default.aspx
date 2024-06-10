<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SIPOH._Default" %>


    <!DOCTYPE html>

    <html xmlns="">

    <head runat="server">
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>SIPOH</title>
        <link rel="shortcut icon" href="https://www.aseh.gob.mx/Shared/img/anticorrupcion/3.png" />
        <link href="Content/css/Default.css" rel="stylesheet" />
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
</head>
         <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
                <div id="liveToast" class="toast hide bg-light" role="alert" aria-live="assertive" aria-atomic="true">
                    <div class="toast-header">
                        <i class="bi bi-emoji-laughing rounded text-success mr-2 fs-5" data-bs-dismiss="toast" aria-label="Close"></i>                       
                        
                        <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>
                    </div>
                    <div class="toast-body">
                        <asp:Label ID="lblMensajeBienvenida" runat="server" Text="" class="text-capitalize text-danger" />
                    </div>
                </div>
            </div>
<body  class="fade-in container-fluid custom-bg door-wrapper">            
    <div class="door"></div>
    <div class="vw-100 vh-100 d-flex justify-content-center align-content-center contenido">
    <div class="d-flex flex-column-reverse align-items-center justify-content-center align-self-start col-lg-3 col-md-4 col-sm-6 col-8 bg-light py-4 px-3 shadow p-3 mb-5 bg-body rounded bg-white fade-in">
      <form id="form1" runat="server" class="card py-4 px-3 bg-light border-1 border-light col-auto" style="width: 18rem;">
        <div class="mb-3 fade-in">
          <label for="" class="form-label">Usuario:</label>
          <asp:TextBox ID="txtUsuario" runat="server" CssClass="form-control form-control-sm text-secondary bg-light btn-rebote" required placeholder="Ingrese su usuario"></asp:TextBox>
        </div>
        <div class="mb-3 fade-in">
          <label for="" class="form-label"><span class="fw-normal">Contraseña:</span></label>
          <div class="input-group mb-2">
            <asp:TextBox ID="txtPass" runat="server" CssClass="form-control form-control-sm text-secondary bg-light btn-rebote" TextMode="Password" required placeholder="Ingrese su contraseña"></asp:TextBox>
            <button class="btn form-control-sm fa fa-eye-slash fa-lg icon text-success btn-rebote" type="button" id="btnMostrarClave"></button>
          </div>
        </div>
        <asp:Button ID="BotonLogin" OnClick="BotonLogin_Click" Style="background-color: #3F5259; font-weight: bold; color: #ffffff;" runat="server" Text="SIPOH" CssClass="btn fade-in btn-rebote" />
        <asp:Label ID="MensajeErrorLabel" runat="server" Text=""  class="fade-in" CssClass="text-danger my-2  fw-bold"/>
        
      </form>
      <div class="col-auto pb-3 fade-in">
        <img src="https://upload.wikimedia.org/wikipedia/commons/thumb/7/76/Sello_del_Poder_Judicial_de_Hidalgo.svg/1080px-Sello_del_Poder_Judicial_de_Hidalgo.svg.png" alt="logo PJEH" style="height: 120px;" class="desvanecer img-fluid" />
      </div>
    </div>
  </div>
    </body>
       
    <script>
        //$(document).ready(function () {
        //    $('.toast').toast('show');
        //});


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
        <script src="Scripts/default/default.js"></script>

    </html>