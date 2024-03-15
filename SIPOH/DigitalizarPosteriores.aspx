<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Consignacion.Master" CodeBehind="DigitalizarPosteriores.aspx.cs" Inherits="SIPOH.DigitalizarPosteriores" %>

<asp:Content ID="ContentDigitInici" ContentPlaceHolderID="ContentDigitInici" runat="server">

<div>
    <h1 style="margin-left: 5%" class="h5"><i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold"> Digitalización de Posteriores</span> </h1>
</div>

    <link href="Content/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

    <style type="text/css">
        .mayusculas {
            text-transform: uppercase;
        }

        .h5 {
            margin-left: 5%;
        }
    </style>

    <!-- Include Toastr CSS -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.css" />

    <!-- Include jQuery (Toastr depends on it) -->
    <script src="https://code.jquery.com/jquery-3.6.4.min.js"></script>

    <!-- Include Toastr JS -->
    <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/toastr.min.js"></script>

    <!-- Toastr initialization -->
    <script>
        toastr.options = {
            "closeButton": true,
            "debug": false,
            "newestOnTop": false,
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "preventDuplicates": false,
            "onclick": null,
            "showDuration": "300",
            "hideDuration": "1000",
            "timeOut": "5000",
            "extendedTimeOut": "1000",
            "showEasing": "swing",
            "hideEasing": "linear",
            "showMethod": "fadeIn",
            "hideMethod": "fadeOut"
        }
    </script>

   <div class="m-0">
       <div class="row">
           <div class="col-md-10 ml-auto col-xl-11 mr-auto">
               <!-- Nav tabs -->
               <div class="card">
                   <div class="card-body">
                       <div class="container col-12">
                           <div style="padding: 2%">
                               <h5 class="text-secondary mb-5">Digitalizar Posteiores</h5>
                           </div>
                           <div class="col-md-12 col-sm-12 col-xs-12">
                               <h6 class="help-block text-muted small-font"><b>Pendientes de digitalizar (Seleccione la posterior a digitalizar): </b></h6>
                               <br />
                               <div class="scrollable">
                                   <asp:GridView ID="PDigitalizar" CssClass="table table-striped text-center table-hover table-sm" runat="server" AutoGenerateColumns="False" AllowPaging="True" PageSize="3" OnPageIndexChanging="PDigitalizar_PageIndexChanging">
                                       <Columns>
                                           <asp:TemplateField HeaderStyle-CssClass="bg-success text-white">
                                               <ItemTemplate>
                                                   <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelect_CheckedChanged" />
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                           <asp:TemplateField HeaderText="Asunto" SortExpression="NoAsunto" Visible="false">
                                               <ItemTemplate>
                                                   <asp:Label ID="lblIdAsunto" runat="server" Text='<%# Bind("IdAsunto") %>'></asp:Label>
                                               </ItemTemplate>
                                               <HeaderStyle CssClass="bg-success text-white" />
                                           </asp:TemplateField>
                                           <asp:BoundField DataField="Numero" HeaderText="Folio" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                           <asp:BoundField DataField="TipoPromocion" HeaderText="Tipo de Asunto" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                           <asp:BoundField DataField="FechaIngreso" HeaderText="Fecha de Ingreso" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                           <asp:BoundField DataField="Digitalizado" HeaderText="Digitalizado" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" />
                                       </Columns>
                                       <PagerTemplate>
                                           <asp:Button ID="btnFirst" CssClass="btn btn-primary" runat="server" CommandName="Page" CommandArgument="First" Text=" Primero" />
                                           <asp:Button ID="btnPrev" CssClass="btn btn-success" runat="server" CommandName="Page" CommandArgument="Prev" Text="⏮️ Anterior" />
                                           <asp:Button ID="btnNext" CssClass="btn btn-success" runat="server" CommandName="Page" CommandArgument="Next" Text="⏭️ Siguiente" />
                                           <asp:Button ID="btnLast" CssClass="btn btn-primary" runat="server" CommandName="Page" CommandArgument="Last" Text=" Ultimo" />
                                       </PagerTemplate>
                                   </asp:GridView>
                               </div>

                               <br />

                               <div class="row ">
                                   <div class="col-md-12 col-sm-12 col-xs-12">
                                       <asp:Label ID="lblInicialInfo" runat="server" CssClass="help-block text-muted small-font" Visible="false"><b>Informacion de la inicial: </b></asp:Label>
                                       <br />
                                       <br />
                                       <div runat="server" id="infoInicial">
                                           <asp:Label ID="descripNum" runat="server" Font-Bold="True" Font-Size="Larger" />
                                           <br />
                                           <asp:Label ID="delitos" runat="server" Font-Bold="True" Font-Size="Larger" />
                                           <br />
                                           <br />
                                           <center>
                                               <asp:Label ID="lblPartes" runat="server" Font-Bold="True" Font-Size="Larger" />
                                           </center>
                                           <br />
                                       </div>
                                   </div>
                               </div>

                               <div class="row">
                                   <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                       <center>
                                           <asp:Label ID="lblVictima" runat="server" Font-Bold="True" Font-Size="Larger" />
                                       </center>
                                       <br />
                                       <asp:GridView ID="infoVictima" CssClass="table table-borderless d-sm-table-row" GridLines="None" runat="server" AutoGenerateColumns="False" ShowHeader="False">
                                           <Columns>
                                               <asp:TemplateField>
                                                   <ItemTemplate>
                                                       <div style="font-size: larger; font-weight: bold; font-style: italic;">
                                                           <span style="font-size: 1em;" class="text-success">• </span>
                                                           <asp:Label ID="NCC" runat="server" CssClass="text-success" Text='<%# Eval("NombreCompleto") %>' />
                                                       </div>
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                           </Columns>
                                       </asp:GridView>
                                   </div>

                                   <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
                                       <center>
                                           <asp:Label ID="lblImputado" runat="server" Font-Bold="True" Font-Size="Larger" />
                                       </center>
                                       <br />
                                       <asp:GridView ID="infoImputado" CssClass="table table-borderless d-sm-table-row" GridLines="None" runat="server" AutoGenerateColumns="False" ShowHeader="False">
                                           <Columns>
                                               <asp:TemplateField>
                                                   <ItemTemplate>
                                                       <div style="font-size: larger; font-weight: bold; font-style: italic;">
                                                           <span style="font-size: 1em;" class="text-success">• </span>
                                                           <asp:Label ID="NCC" runat="server" CssClass="text-success" Text='<%# Eval("NombreCompleto") %>' />
                                                       </div>
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                           </Columns>
                                       </asp:GridView>
                                   </div>
                               </div>

                               <br />
                               <br />

                               <div class="row ">
                                   <div class="col-md-6 col-sm-6 col-xs-6">
                                       <asp:Label ID="lblDocsNoDigit" runat="server" CssClass="help-block text-muted small-font" Visible="false"><b>*Documentos no digitalizados:</b></asp:Label>
                                       <br />
                                       <br />
                                       <asp:GridView ID="noDigit" CssClass="table table-striped text-center table-hover table-sm" runat="server" AutoGenerateColumns="False">
                                           <Columns>
                                               <asp:TemplateField HeaderStyle-CssClass="bg-success text-white">
                                                   <ItemTemplate>
                                                       <asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="False" Checked='<%# Eval("Digitalizado").ToString() == "N" %>' />
                                                   </ItemTemplate>
                                               </asp:TemplateField>
                                               <asp:BoundField DataField="Descripcion" HeaderText="Anexo" HeaderStyle-CssClass="bg-success text-white" />
                                               <asp:BoundField DataField="Cantidad" HeaderText="Cantidad" HeaderStyle-CssClass="bg-success text-white" />
                                           </Columns>
                                       </asp:GridView>

                                       <asp:Label ID="lblinfo" runat="server" CssClass="help-block text-muted small-font" Style="text-align: justify" Visible="false"><b>(*) Los elementos seleccionados estan en estatus "NO DIGITALIZADOS", quite la seleccion si el anexo ya esta digitalizado.</b></asp:Label>
                                       <br />
                                       <br />
                                       <br />
                                   </div>


                                   <br />

                                   <div class="col-md-6 col-sm-6 col-xs-6">
                                       <asp:Label ID="lblAdjuntar" runat="server" CssClass="help-block text-muted small-font" Visible="false"><b>Adjuntar documentos:</b>
                                       </asp:Label>
                                       <br />
                                       <br />

                                       <div style="margin-bottom: 10px;">
                                           <asp:FileUpload ID="UploadFileDigit" Visible="false" runat="server" CssClass="form-control" accept=".pdf" />
                                       </div>
                                       <div>
                                           <asp:Label ID="VP" runat="server" CssClass="help-block text-muted small-font" Style="display: none"><b>Vista previa del documento adjunto:</b>
                                           </asp:Label>
                                           <br />
                                           <asp:Panel runat="server" ID="preview" Style="margin: auto; max-height: 50%; overflow-y: auto;"></asp:Panel>
                                           <br />
                                       </div>
                                   </div>

                                    <center>
                                        <div class="row">
                                            <div class="col-md-12 col-sm-12 col-xs-12" style="padding: 10px;">
                                                <%--<asp:Button ID="btnImpresionPort" Visible="true" runat="server" Text="🖨️ Imprimir Portada" CssClass="btn btn-success btn-sm mayusculas" />--%>
                                                <asp:Button ID="btnDigitalizar" Visible="false" runat="server" Text="🧑‍💻 Digitalizar" CssClass="btn btn-success btn-sm mayusculas" OnClick="btnDigitalizar_Click" />
                                            </div>
                                        </div>
                                    </center>

                               </div>
                           </div>
                       </div>
                   </div>
               </div>
           </div>
       </div>
   </div>



   <script>
       function PreviewPDF() {
           var file = document.getElementById('<%= UploadFileDigit.ClientID %>').files[0]; // Selecciona el archivo
           var reader = new FileReader(); // Crea un nuevo FileReader
           var panel = document.getElementById('<%= preview.ClientID %>');
           panel.onclick = function (e) {
               // Previene el postback
               e.preventDefault();
           };

           document.getElementById('<%= UploadFileDigit.ClientID %>').addEventListener('change', PreviewPDF);

           // Añade un evento de cambio a todos los checkboxes en el GridView
           var checkboxes = document.querySelectorAll('#<%= PDigitalizar.ClientID %> input[type=checkbox]');
           for (var i = 0; i < checkboxes.length; i++) {
               checkboxes[i].addEventListener('change', PreviewPDF);
           }

           reader.onloadend = function () {
               var previewDiv = document.getElementById('<%= preview.ClientID %>'); // Obtiene el div de vista previa
               previewDiv.innerHTML = ''; // Limpia el div de vista previa

               var embed = document.createElement('embed'); // Crea un nuevo elemento embed
               embed.src = reader.result; // Establece el src al resultado del FileReader
               embed.type = "application/pdf"; // Establece el tipo de contenido a PDF
               embed.style.width = '500px'; // Establece el ancho del visor
               embed.style.height = '500px'; // Establece la altura del visor

               previewDiv.appendChild(embed); // Añade el elemento embed al div de vista previa
               // Muestra el elemento al final del script
               document.getElementById('VP').style.display = 'block';
           }

           if (file) {
               reader.readAsDataURL(file); // Lee el archivo como URL de datos
           }
       }
       document.getElementById('<%= UploadFileDigit.ClientID %>').addEventListener('change', PreviewPDF); // Añade un evento de cambio al input para llamar a la función PreviewPDF
   </script>
    </asp:Content>