<%@ Page Title="" Language="C#" MasterPageFile="~/Externo/MasterExterno.Master" AutoEventWireup="true" CodeBehind="FirmaDocumentos.aspx.cs" Inherits="SIPOH.Externo.FirmaDocumentos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Scripts/Firma/Conexion.js" type="text/javascript"></script>
    <script src="../Scripts/Firma/Firma.js" type="text/javascript"></script>
    <script src="../Scripts/Firma/FDocumentos.js" type="text/javascript"></script>

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

    <style>
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <asp:UpdateProgress runat="server" ID="idUpdateProgress"
        AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="true">
        <ProgressTemplate>
            <div style="position: fixed; background-color: black; opacity: .5; width: 100%; height: 100%; top: 50%; left: 50%; transform: translate(-50%, -50%); z-index: 2000;">
                <img style="position: fixed; top: 50%; left: 50%; transform: translate(-50%, -50%); width: 100px; height: 100px;" id="idAjaxLoader" alt="Enviando ..." src="../Content/img/loading.gif" />
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card mt-5 mb-1 ml-2 mr-2 p-4" runat="server" id="divFirmar">
                <h5 class="card-header">Firmar y enviar solicitud</h5>
                <div class="card-body">
                    <div class="row">
                        <div class="alert alert-success" role="alert">
                            Aviso: En este modulo usará sus archivos de firma electrónica FEJEH para el envio de su solicitud.
                        </div>

                        <div class="col-6 col-lg-4 mt-4">
                            <label class="form-label" for="customFile">Seleccione Certificado:</label>
                            <input type="file" class="form-control" id="FCertificado">
                        </div>
                        <div class="col-6 col-lg-4 mt-4 form-outline">
                            <label class="form-label" for="customFile">Seleccione Llave privada:</label>
                            <input type="file" class="form-control" id="FLlavePrivada">
                        </div>

                        <div class="col-12 col-lg-4 mt-4">
                            <label class="form-label">Clave de acceso:</label>
                            <asp:TextBox ID="TxtClaveAcceso" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                        </div>

                        <div class="col-12 col-lg-12 mt-4 table-responsive">
                            <asp:GridView ID="gridFirma" runat="server"
                                AutoGenerateColumns="false" CssClass="table table-sm table-striped table-hover "
                                Width="100%">
                                <Columns>
                                    <asp:BoundField DataField="IdSolicitudBuzon" HeaderText="Folio">
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NUC" HeaderText="NUC">
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="NombreJuzgado" HeaderText="Juzgado">
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Solicitud" HeaderText="Solicitud">
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:BoundField>

                                </Columns>
                            </asp:GridView>
                        </div>

                        <div class="offset-7 col-md-4  mt-2">
                            <asp:Button ID="BtnFirma" runat="server" Text="FIRMAR Y ENVIAR NOTIFICACIÓN"
                                CssClass="btn btn-success btn-block" OnClientClick="FirmaDocumentos(); return false;" />
                            <asp:Button runat="server" ID="BtnGenerarArchivosFEA" Text="Generar" Style="display: none;" OnClick="BtnGenerarArchivosFEA_Click" />
                        </div>
                    </div>

                    <asp:HiddenField ID="HFDatosTransfer" runat="server" />
                    <asp:HiddenField ID="HFDatosNotificacion" runat="server" />

                    <asp:HiddenField ID="HFIdUsuario" runat="server" />
                    <asp:HiddenField ID="HFIdSerie" runat="server" />
                    <asp:HiddenField ID="HFIdNAcuerdo" runat="server" />
                    <asp:HiddenField ID="HFNombreJuzgado" runat="server" />
                    <asp:HiddenField ID="HFTipoJuicio" runat="server" />

                    <asp:HiddenField ID="HFIdMachote" runat="server" />
                    <asp:HiddenField ID="HFIdExpediente" runat="server" />
                </div>
            </div>





        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
