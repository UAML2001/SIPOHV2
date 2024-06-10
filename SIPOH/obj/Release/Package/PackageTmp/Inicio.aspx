<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="SIPOH.Inicio" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentCupre" runat="server">

  <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <main class="d-flex flex-column justify-content-center align-content-center align-items-center vh-100 bg-white" >
        <i class="bi bi-quote text-success" style="font-size:40px;"></i>
        <h1 class="text-black display-6 text-center ">Poder Judicial Del Estado De Hidalgo</h1>   
        <div class="row">
            <div class="col-12 bg-black px-4 py-1 mt-1"><span class="text-white fs-1 display-3 ">SIPOH</span></div>
        </div>

    </main>           
    <div class="position-fixed bottom-0 end-0 p-3" style="z-index: 11">
        <div id="liveToast" class="toast hide bg-light" role="alert" aria-live="assertive" aria-atomic="true">
            <div class="toast-header  ">
                
                    <i class="bi bi-emoji-laughing rounded text-success mr-2 fs-5"  data-bs-dismiss="toast" aria-label="Close"></i>
                    <strong class="me-auto"> SIPOH </strong>
                    <small>1 sec ago</small>
                    <button type="button" class="btn-close" data-bs-dismiss="toast" aria-label="Close"></button>                
            </div>
            <div class="toast-body">
                ¡Hola <asp:Label ID="lblMensajeBienvenida" runat="server" Text="" class="text-capitalize text-black" /> 👋!, ¿Qué tenemos hoy para ti? . . .
            </div>
        </div>
    </div>

<script>
    $(document).ready(function(){
        $('.toast').toast('show');
    });
</script>

</asp:Content>
