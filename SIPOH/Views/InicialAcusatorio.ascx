<%@ Register Assembly="System.Web.Extensions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialAcusatorio.ascx.cs" Inherits="SIPOH.Views.InicialAcusatorio" %>

<!-- Modal de resultado de búsqueda (oculto por defecto) -->
<div class="modal fade" id="exito" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header modalExitoso">
                <h4 class="modal-title">Resultado Encontrado </h4>
            </div>
            <div class="modal-body">
                <b>Exito</b>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-success" data-bs-dismiss="modal">Aceptar</button>
            </div>
        </div>
    </div>
</div>
<!-- Modal de error (oculto por defecto) -->
<div class="modal fade" id="modalError" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header modalError">
                <h4 class="modal-title">Error</h4>
            </div>
            <div class="modal-body">
                <b>Error</b>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-danger" data-bs-dismiss="modal">Cerrar</button>
            </div>
        </div>
    </div>
</div>
<!-- Modal de guardar datos -->




<asp:UpdatePanel ID="UpdateAcusatorio" runat="server">
    <ContentTemplate>

        <div class="modal fade" id="guardarDatos" role="dialog">
    <div class="modal-dialog">
        <div class="modal-content">
            <div class="modal-header modalGuardar">
                <h5 class="modal-title">
                    <asp:Literal ID="ltTituloModal" runat="server"></asp:Literal>
                </h5>
                <button type="button" class="close" data-bs-dismiss="modal">×</button>
            </div>
            <div class="modal-body">
                
                <p class="DatosModal mayusculas"><b>Fecha Ejecución:</b> <asp:Label ID="lblFechaEjecucion" runat="server"></asp:Label></p>
                <p class="DatosModal mayusculas"><b>Beneficiario: </b><asp:Label ID="lblnombreBeneficiario" runat="server"></asp:Label> <asp:Label ID="lblapellidoPaternoBeneficiario" runat="server"></asp:Label> <asp:Label ID="lblapellidoMaternoBeneficiario" runat="server"></asp:Label></p>
                <p class="DatosModal mayusculas"><b>Solicitante: </b><asp:Label ID="lblnombreSolicitanteSeleccionado" runat="server"></asp:Label></p>
                <p class="DatosModal mayusculas"><b>Solicitud: </b><asp:Label ID="lblnombreSolicitudSeleccionado" runat="server"></asp:Label></p>
                <p class="DatosModal mayusculas"><b>Detalle de la solicitud: </b><asp:Label ID="lbldetalleSolicitante" runat="server"></asp:Label></p>
                <p class="DatosModal mayusculas"><b>Otra Solicitud: </b><asp:Label ID="lblotraSolicitud" runat="server"></asp:Label></p>
                <p class="DatosModal mayusculas"><b>Interno: </b><asp:Label ID="lblinterno" runat="server"></asp:Label></p>
                <p class="DatosModal mayusculas"><b>Sala disponible y toca: </b><asp:Label ID="lblSalasYTocas" runat="server"></asp:Label></p>
                <p class="DatosModal mayusculas"><b>Sentencias: </b><asp:Label ID="lblSentencias" runat="server"></asp:Label></p>
                <p class="DatosModal mayusculas"><b>Anexos y Cantidad: </b><asp:Label ID="lblAnexos" runat="server"></asp:Label></p>
                
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-outline-danger" onclick="CerrarModalGuardarDatos()" data-bs-dismiss="modal">Cancelar</button>
                <asp:Button ID="btnGuardarAcusatorio" runat="server" CssClass="btn btn-outline-warning" OnClick="btnGuardarAcusatorio_Click" Text="Guardar" />
            </div>
        </div>
    </div>
</div>

<div class="modal" ID="errorModal" tabindex="-1" role="dialog" runat="server">
  <div class="modal-dialog" >
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title">Error</h5>
       <button type="button" class="close" data-bs-dismiss="modal">×</button>
      </div>
      <div class="modal-body">
        <p class="mayusculas">Se produjo un error al guardar los datos.</p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-outline-danger" data-bs-dismiss="modal">Cerrar</button>
      </div>
    </div>
  </div>
</div>


<div class="row" id="PrimerRow" runat="server">
    <form class="container-lg col-xxl-12">
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputRadicacion" class="form-label text-secondary">Juzgado de Procedencia</label>
            <select class="form-select form-select-sm mayusculas" id="inputRadicacion" runat="server" autopostback="true" onchange="habilitarElementos()">
                <option value="Seleccionar">Seleccionar</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputIncomJuzgado" class="form-label text-secondary">Causa | NUC</label>
            <select class="form-select form-select-sm mayusculas" id="inputIncomJuzgado" ClientIDMode="Static" runat="server" onchange="mostrarValorSeleccionado(this)">
                <option value="1" selected >Causa</option>
                <option value="2">NUC</option>
                <option value="3">Juicio Oral</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputNuc" class="form-label text-secondary">Número de Causa</label>
            <div class="input-group">
                <input type="text" class="form-control form-control-sm mayusculas" id="inputNuc" ClientIDMode="Static" runat="server" minlength="9" maxlength="16" onblur="aplicarFormatoSegunSeleccion(this)">
                <div class="input-group-append">
                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm" OnClick="btnBuscar_Click" />
                </div>
            </div>
        </div>
        <asp:Label ID="lblResultado" runat="server" CssClass="text-success mayusculas"></asp:Label>
    </form>
</div>
<div class="p-4">
    <div id="tablaResultadosHtmlDiv" class="table-responsive" runat="server"></div>
</div>
<div class="container" id="DivExAm" runat="server">
    <div class="row">
        <div class="mb-5 col-12 col-sm-6 col-md-3 col-lg-3 col-xl-3 col-xxl-3">
            <label class="form-label-sm text-secondary">Salas Disponibles</label>
            <asp:DropDownList ID="selectSalas" runat="server" CssClass="mayusculas form-select form-select-sm">
                <asp:ListItem Text="Seleccionar" />
            </asp:DropDownList>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-3 col-lg-3 col-xl-3 col-xxl-3">
            <label class="form-label text-secondary">Número de Toca</label>
            <div class="input-group">
                <asp:TextBox ID="inputNumeroToca" runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="" onblur="padLeadingZeros(this)" MaxLength="9"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="btnAgregar" runat="server" CssClass="btn btn-success btn-sm" OnClick="AgregarSalaATabla" Text="+" />
                </div>
            </div>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
            <label class="form-label text-secondary">Sentencia de Amparo Vinculada</label>
            <div class="input-group">
                <asp:TextBox ID="inputSentencia" runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Busca Sentencia" MaxLength="15"></asp:TextBox>
                <div class="input-group-append">
                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-success btn-sm" OnClick="AgregarSentenciaATabla" Text="+" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
           <asp:Label ID="tituloSalas" runat="server" CssClass="textoTablasArriba">
                <h2 class="textoTablasArriba"><i class="bi bi-table">Tabla De Salas</i></h2>
                    </asp:Label>
            <asp:GridView ID="tablaSalas" runat="server" CssClass="table" AutoGenerateColumns="False" OnRowDeleting="BorrarSala" OnRowDataBound="tablaSalas_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="NombreSala" HeaderText="Sala">
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:BoundField>
                    <asp:BoundField DataField="NumeroToca" HeaderText="No° Toca">
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:BoundField>
                    <asp:CommandField ShowDeleteButton="True" HeaderText="Quitar" DeleteText="✖️">
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
            <asp:Label ID="tituloSentencias" runat="server" CssClass="textoTablasArriba">
            <h2 class="textoTablasArriba"><i class="bi bi-table">Tabla De Amparos</i></h2>
                    </asp:Label>

            <asp:GridView ID="tablaSentencias" runat="server" CssClass="table" AutoGenerateColumns="False" OnRowDeleting="BorrarSentencia" OnRowDataBound="tablaSentencias_RowDataBound">
                <Columns>
                    <asp:BoundField DataField="Sentencia" HeaderText="Sentencia">
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:BoundField>
                    <asp:CommandField ShowDeleteButton="True" HeaderText="Quitar" DeleteText="✖️">
                        <HeaderStyle CssClass="bg-success text-white" />
                    </asp:CommandField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="row justify-content-center">
        <div class="col-lg-7 col-md-7 col-sm-12 text-center">
            <div style="display: flex; align-items: center; justify-content: center;">
                <p style="margin-right: 10px; margin-top: 10px;">¿Consultar Sentenciado / Beneficiario? </p>
                <div class="form-check" style="margin-right: 10px;">
                    <asp:RadioButton ID="CheckSi" runat="server" GroupName="respuesta" AutoPostBack="True"  CssClass="radio-custom" OnCheckedChanged="RadioButton2_CheckedChanged" />
                    <label class="form-check-label" for="CheckSi">
                        Si
                    </label>
                </div>
                <div class="form-check">
                    <asp:RadioButton ID="CheckNo" runat="server" GroupName="respuesta" AutoPostBack="True" OnCheckedChanged="RadioButton2_CheckedChanged" />
                    <label class="form-check-label" for="CheckNo">
                        No
                    </label>
                </div>
            </div>
        </div>
    </div>
</div>
<div class="container" id="RegistroPartesIn" runat="server">
    <div class="row">
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="InputNombreBusqueda" class="form-label text-secondary">Nombre(s) Parte</label>
            <input type="text" id="InputNombreBusqueda" class="form-control form-control-sm mayusculas" placeholder="" maxlength="250" runat="server" />
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="InputApPaternoBusqueda" class="form-label text-secondary">Ap. Paterno Parte</label>
            <input type="text" id="InputApPaternoBusqueda" class="form-control form-control-sm mayusculas" placeholder="" maxlength="100" runat="server" />
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputNuc" class="form-label text-secondary">Ap. Materno de Causa</label>
            <div class="input-group">
                <input type="text" id="inputApMaterno" class="form-control form-control-sm mayusculas" placeholder="" maxlength="100" runat="server">
                <div class="input-group-append" id="divBotonBuscar" runat="server">
                    <asp:Button ID="BuscarPartes" runat="server" Text="Buscar" CssClass="btn btn-outline-secondary btn-sm" OnClick="BuscarPartes_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="row">
        <asp:GridView ID="GridView1" runat="server" CssClass="table" AutoGenerateColumns="False" OnRowDataBound="GridView1_RowDataBound">
            <Columns>
                <asp:BoundField DataField="NoEjecucion" HeaderText="No°Ejecución">
                    <HeaderStyle CssClass="bg-success text-white" />
                </asp:BoundField>
                <asp:BoundField DataField="Juzgado" HeaderText="Juzgado">
                    <HeaderStyle CssClass="bg-success text-white" />
                </asp:BoundField>
                <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                    <HeaderStyle CssClass="bg-success text-white" />
                </asp:BoundField>
                <asp:BoundField DataField="ApPaterno" HeaderText="Apellido Paterno">
                    <HeaderStyle CssClass="bg-success text-white" />
                </asp:BoundField>
                <asp:BoundField DataField="ApMaterno" HeaderText="Apellido Materno">
                    <HeaderStyle CssClass="bg-success text-white" />
                </asp:BoundField>
                <asp:BoundField DataField="FechaEjecucion" HeaderText="Fecha Ejecución">
                    <HeaderStyle CssClass="bg-success text-white" />
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </div>
    <div class="row justify-content-center">
        <div class="col-lg-4 col-md-6 col-sm-12 text-center">
            <p>¿Deseas Continuar con el registro?</p>
            <div>
                <button type="button" id="idBotonSi" class="btn btn-success" runat="server" onserverclick="btSiRegistro2_Click"><i class="bi bi-check-circle"></i></button>
                <button type="button" id="idBotonNo" class="btn btn-danger" runat="server" onserverclick="btNoRegistro2_Click"><i class="bi bi-x-circle"></i></button>
            </div>
        </div>
    </div>
    <div id="ContinuarRegistro" runat="server">

        
            <div class="col-12 col-sm-6 col-md-4 col-lg-6" style="display: flex; align-items: center;">
                <p style="margin-right: 10px; margin-top: 10px;">¿Sentenciado Interno? (</p>
                <div class="form-check" style="margin-right: 10px;">
                    <input class="form-check-input radioSI" type="radio" value="S" id="siInterno" runat="server">
                    <label class="form-check-label">
                        Si
                    </label>
                </div>
                <div class="form-check">
                    <input class="form-check-input radioNO" type="radio" value="N" id="noInterno" runat="server">
                    <label class="form-check-label">
                        No )
                    </label>
                </div>
            </div>
    


        <div class="row">
            <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                <label class="form-label-sm text-secondary">Solicitante</label>
                <asp:DropDownList ID="CatSolicitantesDD" runat="server" CssClass="form-select form-select-sm mayusculas">
                    <asp:ListItem Text="Seleccionar" />
                </asp:DropDownList>
            </div>
            <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                <label class="form-label text-secondary">Detalle del Solicitante</label>
                <input type="text" class="form-control form-control-sm mayusculas" id="detalleSolicitantes" runat="server" maxlength="250"/>
            </div>
        </div>
        <div class="row">
            <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                <label class="form-label-sm text-secondary">Solicitud</label>
                <asp:DropDownList ID="CatSolicitudDD" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CatSolicitudDD_SelectedIndexChanged" CssClass="form-select form-select-sm mayusculas">
                    <asp:ListItem Text="Seleccionar" />
                </asp:DropDownList>
            </div>
            <div class="col-12 col-sm-6 col-md-6 col-lg-6">
                <label class="form-label text-secondary">Otra Solicitud</label>
                <input type="text" id="InputOtraSolicitud" runat="server" class="form-control form-control-sm mayusculas" maxlength="250" ClientIDMode="Static" />
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-12 col-sm-6 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                <label class="form-label-sm text-secondary">Anexos</label>
                <asp:DropDownList ID="CatAnexosDD" runat="server" CssClass="form-select form-select-sm mayusculas" AutoPostBack="true" OnSelectedIndexChanged="CatAnexosDD_SelectedIndexChanged">
                    <asp:ListItem Text="Seleccionar" />
                </asp:DropDownList>
            </div>
            <div class="col-12 col-sm-6 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                <label class="form-label text-secondary">Otro Anexo</label>
                <input type="text" id="OtroAnexo" class="form-control form-control-sm mayusculas" runat="server" maxlength="100" clientidmode="Static" />
            </div>
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
                <label  class="form-label text-secondary">Cantidad</label>
                <div class="input-group">
                    <input type="number" id="CantidadInput" class="form-control form-control-sm mayusculas" runat="server" max="1000" ClientIDMode="Static">
                    <div class="input-group-append">
                        <asp:Button ID="AgregarBtn" runat="server" Text="+" CssClass="btn btn-success btn-sm" OnClick="AgregarATabla" />
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <asp:Label ID="TablasAnexos" runat="server" CssClass="textoTablasArriba">
               <h2 class="textoTablasArriba"><i class="bi bi-table">Tabla de Anexos</i></h2>
            </asp:Label>
            <div class="col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12">
                <asp:GridView ID="tablaDatos" CssClass="table" runat="server" AutoGenerateColumns="False" OnRowDeleting="BorrarFila" OnRowDataBound="tablaDatos_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="NombreSala" HeaderText="Anexo">
                            <HeaderStyle CssClass="bg-success text-white" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NumeroToca" HeaderText="Cantidad">
                            <HeaderStyle CssClass="bg-success text-white" />
                        </asp:BoundField>
                        <asp:CommandField ShowDeleteButton="True" HeaderText="Quitar" DeleteText="✖️">
                            <HeaderStyle CssClass="bg-success text-white" />
                        </asp:CommandField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

        <div class="row justify-content-md-center">
            <div class="col-xl-2 col-sm-12 col-md-2">
                <asp:Button ID="btnGuardarDatosModal" runat="server" Text="Guardar Datos" CssClass="btn btn-outline-secondary btn-sm col-12" OnClick="btnGuardarDatosModal1_Click" />
            </div>
        </div>
    </div>
        </div>

        <div class="container" id="tituloSello" runat="server">
            <div class="row justify-content-center" >
                <div class="col-auto">
                    <h3 class="text-center">¡Se ha guardado tu informacion y ya puedes imprimir tu sello aqui!</h3>
                </div>
            </div>
            <p></p>
            <div class="row justify-content-center">
                <div class="col-sm-12 col-md-6 col-lg-6 col-xl-6 col-xxl-6 d-flex justify-content-center">
                    <button class="btn btn-success" onclick="imprimirTicketAcusatorio()">Imprimir SELLO</button>
                </div>
                <div class="col-sm-12 col-md-6 col-lg-6 col-xl-6 col-xxl-6 d-flex justify-content-center">
                    <button class="btn btn-primary" onclick="recargarPagina()">Registrar otra inicial</button>
                </div>
            </div>
        </div>
    <p></p>


    <pre id="TicketDivAcusatorio" runat="server"></pre>
    </ContentTemplate>
    </asp:UpdatePanel>


    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
   


    <script>
        //FUNCIONES DE ALERTAS EXITO Y ERROR
        function mostrarAlerta(message) {
            $('#exito .modal-body b').text(message); // Establece el mensaje en el modal
            $('#exito').modal('show'); // Muestra el modal
        }
        function mostrarError(mensaje) {
            $('#modalError .modal-body b').text(mensaje); // Establece el mensaje de error en el modal
            $('#modalError').modal('show'); // Muestra el modal de error
    }
    // Función para abrir el modal
    function abrirModalGuardarDatos1() {
        $('#guardarDatos').modal('show');
        }

        function CerrarModalGuardarDatos() {
            $('#guardarDatos').modal('hide');
            $('body').removeClass('modal-open').css('overflow', ''); // Restablece el overflow
            $('.modal-backdrop').remove();
        }
        
        function mostrarModalError() {
            $('#errorModal').modal('show');
            alert("Entro modal error insert");
        }

        function imprimirTicketAcusatorio() {
            var contenido = document.getElementById('<%= TicketDivAcusatorio.ClientID %>').innerHTML;
            var ventanaImpresion = window.open('', '_blank');

            var estilos = `<style>
                            pre {
                                font-family: monospace;
                                white-space: pre;
                                margin: 1em 0;
                            }
                        </style>`;

            ventanaImpresion.document.write(estilos + '<pre>' + contenido + '</pre>');
            ventanaImpresion.document.close();
            ventanaImpresion.print();
        }


 
        function recargarPagina() {
            window.location.href = window.location.href;
        }

    </script>
    <script type="text/javascript">
        function limpiarControles() {
            // Limpiar controles de entrada
            location.reload();
        }
    </script>
       <script>
           window.addEventListener('keydown', function (e) {
               var node = (e.target) ? e.target : ((e.srcElement) ? e.srcElement : null);
               if ((e.keyCode == 13) && (node.type == "text")) {
                   e.preventDefault();
                   return false;
               }
           }, true);
       </script>

