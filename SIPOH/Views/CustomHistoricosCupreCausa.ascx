<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomHistoricosCupreCausa.ascx.cs" Inherits="SIPOH.Views.CustomHistoricosCupreCausa" %>
<asp:UpdatePanel runat="server" ID="HistoricosCausaPanel" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
          

        <%--registro de datos--%>
        <div class="row pt-0">

            <form class="container-lg col-xxl-12">
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputNUC" class="form-label text-secondary">Número de histórico de causa:</label>
                    <asp:TextBox runat="server" ID="inputNumeroArchivo" CssClass="form-control form-control-sm " MaxLength="20" onblur="padLeadingZeros(this)" />

                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputNUC" class="form-label text-secondary">NUC:</label>
                    <asp:TextBox runat="server" ID="inputNUCHistorico" CssClass="form-control form-control-sm " MaxLength="20" onblur="formatNuc(this)" />

                </div>


                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inpuTipoSolicitud" class="form-label text-secondary">Tipo solicitud: </label>

                    <asp:DropDownList runat="server" ID="inputTipoSolicitudHistorico" CssClass="form-select form-select-sm text-secondary" AppendDataBoundItems="true" AutoPostBack="true">
                        <asp:ListItem Text="Selecciona una opción" Value="" />
                        <asp:ListItem Text="Innominada" Value="27" />
                        <asp:ListItem Text="Inicial" Value="26" />
                    </asp:DropDownList>
                </div>
                <div class=" mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputFechaRecepcion" class="form-label text-secondary">Fecha de recepción</label>
                    
                    <asp:TextBox runat="server" ID="inputFechaRecepcionC" CssClass="form-control form-control-sm" TextMode="DateTimeLocal" oninput="validarFechaC(this)"></asp:TextBox>
                </div>

                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputNumeroFojas" class="form-label text-secondary">Número de fojas:</label>
                    <asp:TextBox runat="server" ID="inputNumeroFojasCHistorico" CssClass="form-control form-control-sm" oninput="validarNumero(this)" MaxLength="6" />

                </div>



                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label class="form-label text-secondary">Tipo de radicación: </label>
                    <div class="d-flex align-items-center">
                        <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-secondary" ID="inpuTipoRadicacion" AutoPostBack="false">
                            <asp:ListItem Text="Selecciona una opción" Value="" />
                            <asp:ListItem Text=" C/Detenido" Value="C" CssClass="form-check-input" />
                            <asp:ListItem Text=" S/Detenido" Value="S" CssClass="form-check-input" />
                        </asp:DropDownList>

                    </div>

                </div>

                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="inputObservaciones" class="form-label text-secondary">Observaciones: </label>
                    <asp:TextBox runat="server" ID="inputObservaciones" CssClass="form-control form-control-sm" MaxLength="8000"></asp:TextBox>
                </div>

                <%--first part--%>
                <div class="row">


                    <div class="col-12 col-lg-6 text-left ">

                        <span class="text-success fw-bold m-2"><i class="bi bi-emoji-laughing"></i>Víctima</span>
                        <i class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modalAgregarVictimas">+</i>                        
                        <div class="table-responsive mt-2 ">
                            <table class="table table-striped table-hover mb-0 table-sm">
                                <thead class="text-center">
                                    <tr>
                                        <th scope="col" class="bg-success text-white">Nombre</th>
                                        <th scome="col" class="bg-success text-white">Apellidos</th>
                                        <th scope="col" class="bg-success text-white">Género</th>
                                        <th scope="col" class="bg-success text-white">Acciones</th>
                                    </tr>
                                </thead>
                                <tbody class="table table-striped text-center">
                                    <asp:Repeater ID="RepeaterVictimas" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <th scope="row" class="text-uppercase"><%# Eval("nombreParte")%></th>
                                                <th class="text-secondary text-uppercase"><%# Eval("apellidoPaterno") %> <%# Eval("apellidoMaterno") %></th>
                                                <td class="text-secondary text-uppercase"><%# Eval("genero").ToString().Equals("F") ? "FEMENINO" : (Eval("Genero").ToString().Equals("M") ? "MASCULINO" : "OTRO") %></td>
                                                <td>
                                                <asp:Button runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" OnClick="btnEliminarVictimaList" /></i></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>




                    <div class="col-12 col-lg-6 text-left">

                        <span class="text-success fw-bold m-2"><i class="bi bi-emoji-frown"></i>Imputado</span>
                        <i class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modalAgregarImputado">+</i>
                        <div class="table-responsive mt-2 ">

                            <table class="table table-striped table-hover mb-0 table-sm">
                                <thead class=" text-center">
                                    <tr class="">

                                        <th scope="col" class="bg-success text-white">Nombre</th>
                                        <th scope="col" class="bg-success text-white">Apellidos</th>
                                        <th scope="col" class="bg-success text-white">Género</th>
                                        <th scope="col" class="bg-success text-white">Acciones</th>

                                    </tr>
                                </thead>
                                <tbody class="table table-striped text-center ">
                                    <asp:Repeater ID="RepeaterInputados" runat="server">
                                        <ItemTemplate>
                                            <tr>
                                                <th scope="row" class="text-uppercase"><%# Eval("NombreParte") %></th>
                                                <th class="text-secondary text-uppercase"><%# Eval("ApellidoPaterno") %> <%# Eval("ApellidoMaterno") %></th>
                                                <td class="text-secondary text-uppercase"><%# Eval("Genero").ToString().Equals("F") ? "FEMENINO" : (Eval("Genero").ToString().Equals("M") ? "MASCULINO" : "OTRO") %></td>
                                                <td>
                                                    <asp:Button runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" OnClick="btnEliminarCulpadoList" /></td>
                                            </tr>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>




                <%--second part--%>
                <div class="row mt-4 d-flex">
                    <div class="col-12 col-lg-6 text-left">

                        <div class="mb-0 row">
                            <div class="col-md-5">

                                <label for="inputDelitos" class="form-label text-secondary align-self-center">Delitos: </label>


                                <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-secondary" AppendDataBoundItems="true" ID="inputDelitos" AutoPostBack="false">
                                    <asp:ListItem Text="Selecciona una opción" Value="" />
                                </asp:DropDownList>
                                <%--<asp:Label runat="server" ID="lblNombresDelitos" Text=""></asp:Label>--%>
                            </div>
                            <div class="col-1 d-flex align-items-end ">
                                <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="+" OnClick="btnAgregarDelito_Click" UseSubmitBehavior="false" />

                            </div>
                        </div>

                        <div class="table-responsive mt-2">
                            <table class="table table-striped table-hover mb-0 table-sm">
                                <thead class=" text-center">
                                    <tr>

                                        <th scope="col" class="bg-success text-white">Delito</th>
                                        <th scope="col" class="bg-success text-white">Acciones</th>
                                    </tr>
                                </thead>
                                <%--Boton de eliminar delitos no funciona--%>
                                <tbody class="table table-striped text-center ">
                                    <asp:Repeater ID="RepeaterDelitos" runat="server">
                                        <ItemTemplate>

                                            <tr>
                                                <td class="text-secondary text-uppercase "><%# Eval("Delito") %></td>
                                                <td class="">
                                                    <asp:Button runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" OnClick="btnEliminarDelitoList" />
                                                </td>
                                            </tr>

                                        </ItemTemplate>
                                    </asp:Repeater>
                                </tbody>
                            </table>

                        </div>
                    </div>

                </div>
                <div class=" d-flex justify-content-center mt-5">
                    <a class="btn btn-success btn-sm" data-bs-toggle="modal" onclick="valoresFinalesHistoricoCausa();" data-bs-target="#modalGuardarHistoricosCupreCausa"><i class="bi bi-floppy-fill mr-1"></i>Guardar</a>

                </div>
                <asp:Label runat="server" ID="lblSuccess" Text="" CssClass="text-success text-center"></asp:Label>
                <asp:Label runat="server" ID="lblError" Text="" CssClass="text-danger text-center"></asp:Label>
                <!-- FIN DIV OCULTO DOS -->


            </form>

        </div>



        <%--modales--%>
        <!-- Modal imputado -->
        <div class="modal fade" id="modalAgregarImputado" tabindex="-1" aria-labelledby="modalAgregarImputado" aria-hidden="true">
            <div class="modal-dialog modal-lg">
                <div class="modal-content">
                    <%--contenido--%>
                    <div class="modal-header ">
                        <div class="row d-flex align-items-center justify-content-center text-center col-12">
                            <i class="bi bi-emoji-frown text-success pr-2" style="font-size: 56px;"></i>
                            <h4 class="modal-title text-secondary ">Agregar partes</h4>
                        </div>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body d-flex flex-wrap justify-content-start">
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblAPaternoImputado" runat="server" AssociatedControlID="txtAPaternoImputado" CssClass="form-label text-secondary">Apellido Paterno:</asp:Label>
                            <asp:TextBox ID="txtAPaternoImputado" runat="server" CssClass="form-control form-control-sm" MaxLength="50" />
                        </div>
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblAMaternoImputado" runat="server" AssociatedControlID="txtAMaternoImputado" CssClass="form-label text-secondary">Apellido Materno:</asp:Label>
                            <asp:TextBox ID="txtAMaternoImputado" runat="server" CssClass="form-control form-control-sm" MaxLength="50" />
                        </div>
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblNombreImputado" runat="server" AssociatedControlID="txtNombreImputado" CssClass="form-label text-secondary">Nombre (s):</asp:Label>
                            <asp:TextBox ID="txtNombreImputado" runat="server" CssClass="form-control form-control-sm" MaxLength="40" />
                        </div>


                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblGeneroImputado" runat="server" AssociatedControlID="txtGeneroImputado" CssClass="form-label text-secondary">Sexo:</asp:Label>
                            <asp:DropDownList runat="server" ID="txtGeneroImputado" CssClass="form-select form-select-sm text-secondary">
                                <asp:ListItem Text="Selecciona una opción" Value="" />
                                <asp:ListItem Text="Femenino" Value="F" />
                                <asp:ListItem Text="Masculino" Value="M" />
                                <asp:ListItem Text="Otro" Value="O" />
                            </asp:DropDownList>
                        </div>
                        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                            <asp:Label ID="lblAliasImputado" runat="server" AssociatedControlID="txtAliasImputado" CssClass="form-label text-secondary">Alias:</asp:Label>
                            <asp:TextBox ID="txtAliasImputado" runat="server" CssClass="form-control form-control-sm" MaxLength="40" />
                        </div>


                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                        <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Guardar" OnClick="btnAgregarImputado_Click" data-bs-dismiss="modal" />

                    </div>
                </div>
            </div>
        </div>
       

        <!-- Agrega esto en tu HTML donde tengas tus otros elementos -->
        <div id="errorModal" class="modal fade" role="dialog">
            <div class="modal-dialog">
                <!-- Contenido del modal -->
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                        <h4 class="modal-title">Error</h4>
                    </div>
                    <div class="modal-body">
                        <p>Los datos no fueron correctamente llenados. Por favor, verifica e intenta nuevamente.</p>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar</button>
                    </div>
                </div>
            </div>
        </div>


        <!-- Modal Partes victima -->
    <div class="modal fade" id="modalAgregarVictimas" tabindex="-1" aria-labelledby="modalAgregarVictimas" aria-hidden="true">
        <div class="modal-dialog modal-lg" role="document">
            
               
            <div class="modal-content">
                <!-- Contenido del segundo modal -->
                <div class="modal-header">
                    <div class="row d-flex align-items-center justify-content-center text-center col-12">
                        <i class="bi bi-emoji-smile text-success pr-2" style="font-size: 56px;"></i>
                        <h4 class="modal-title text-secondary ">Agregar partes</h4>
                    </div>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close" onclick="CerrarModal()"></button>
                </div>

                <asp:UpdatePanel ID="UpdatePanelModal" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="modal-body d-flex flex-wrap justify-content-start">


                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                                <label for="inputvictimapersona" class="form-label text-secondary">Persona:</label>

                                <asp:DropDownList ID="ddlPersonaVictima" runat="server" CssClass="form-select form-select-sm text-secondary" AutoPostBack="true" OnSelectedIndexChanged="ddlPersonaVictima_SelectedIndexChanged">
                                    <asp:ListItem Text="Selecciona una opción" Value="" />
                                    <asp:ListItem Text="Física" Value="F" />
                                    <asp:ListItem Text="Moral" Value="M" />
                                </asp:DropDownList>
                            </div>

                            <%--//Apellido paterno es utizado para el registro de RAZON SOCIAL--%>
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3" id="containerRazonSocialVictima" runat="server" style="display: none;">
                                <asp:Label runat="server" class="form-label text-secondary">Razón social:</asp:Label>
                                <asp:TextBox ID="txtRazonSocialVictima" runat="server" CssClass="form-control form-control-sm" MaxLength="40" />
                            </div>
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3" id="containerApellidoPVictima" runat="server" style="display: none;">
                                <asp:Label ID="lblAP" runat="server" class="form-label text-secondary">Apellido paterno:</asp:Label>
                                <asp:TextBox ID="txtAPVictima" runat="server" CssClass="form-control form-control-sm" MaxLength="40" />
                            </div>
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3" id="containerApellidoMVictima" runat="server" style="display: none;">
                                <asp:Label ID="lblAM" runat="server" class="form-label text-secondary">Apellido materno:</asp:Label>
                                <asp:TextBox ID="txtAMVictima" runat="server" CssClass="form-control form-control-sm" MaxLength="40" />
                            </div>
                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3" id="containerNombreVictima" runat="server" style="display: none;">
                                <asp:Label ID="lblNombre" runat="server" CssClass="form-label text-secondary">Nombre(s):</asp:Label>
                                <asp:TextBox ID="txtNombreVictima" runat="server" CssClass="form-control form-control-sm" MaxLength="40" />
                            </div>


                            <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3" id="containerGeneroVictima" runat="server" style="display: none;">
                                <asp:Label ID="lblSexo" runat="server" class="form-label text-secondary">Sexo:</asp:Label>
                                <asp:DropDownList ID="ddlSexoVictima" runat="server" CssClass="form-select form-select-sm text-secondary">
                                    <asp:ListItem Text="Selecciona una opción" Value="" />
                                    <asp:ListItem Text="Femenino" Value="F" />
                                    <asp:ListItem Text="Masculino" Value="M" />
                                    <asp:ListItem Text="Otro" Value="O" />
                                </asp:DropDownList>
                            </div>

                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal" onclick="CerrarModal()"><i class="bi bi-x-lg"></i>Cerrar</button>
                    <%--<asp:Button runat="server" Text="Guardar" CssClass="btn btn-success btn-sm" OnClick="btnAgregarVictima_Click" OnClientClick="" data-bs-dismiss="modal" />--%>
                    <asp:Button runat="server" Text="Guardar" CssClass="btn btn-success btn-sm" OnClick="btnAgregarVictima_Click" OnClientClick="" data-bs-dismiss="modal" />



                </div>
            </div>
        </div>
    </div>



        <!-- MOdal Save Changes -->
        <div class="modal fade" id="modalGuardarHistoricosCupreCausa" tabindex="1" aria-labelledby="modalGuardarHistoricosCupreCausa" aria-hidden="true">
            <div class="modal-dialog modal-md">
                <div class="modal-content">
                    <!-- Contenido del segundo modal -->
                    <div class="modal-header">
                        <i class="bi bi-exclamation-circle text-success fs-3 pr-2"></i>
                        <h5 class="modal-title text-secondary fs-4 ">Guardar cambios.</h5>
                        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                    </div>
                    <div class="modal-body row">
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Número de archivo:</span>
                            <asp:TextBox runat="server" ID="copyNumeroArchivo" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Número de NUC:</span>
                            <asp:TextBox runat="server" ID="copyTextBoxNUC" CssClass="form-control form-control-sm text-center text-success text-uppercase" ReadOnly="true" />
                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Tipo de asunto:</span>
                            <asp:TextBox runat="server" ID="copyDropDownTipoAsunto" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />
                        </div>

                        <div class="col-6 pb-2">
                            <span class="text-secondary">Fecha de recepción:</span>
                            <asp:TextBox runat="server" ID="copyFechaRecepcionC" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>
                        <div class="col-6 pb-2">
                            <span class="text-secondary">Número de fojas:</span>
                            <asp:TextBox runat="server" ID="copyNumeroFojasCHistorico" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>

                        <div class="col-6 pb-2">
                            <span class="text-secondary">Tipo de radicación:</span>
                            <asp:TextBox runat="server" ID="copyTipoRadicacion" CssClass="form-control form-control-sm text-center text-success" ReadOnly="true" />

                        </div>

                        <div class="col-12">
                            <span class="text-secondary">Observaciones:</span>
                            <asp:TextBox runat="server" ID="copyObservaciones" CssClass="form-control form-control-sm text-center text-success text-uppercase" ReadOnly="true" TextMode="MultiLine" Rows="4" />

                        </div>
                    </div>

                    <div class="modal-footer">
                        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>--%>

                                <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Guardar" OnClick="btnGuardarInicial_Click" OnClientClick="return validarNumero();" data-bs-dismiss="modal" postback="true" />

                            <%--</ContentTemplate>
                        </asp:UpdatePanel>--%>

                    </div>
                </div>
            </div>
        </div>
                              
        <script type="text/javascript">
            //function ActivarScroll() {
            //    $('body').css('overflow', 'auto !important');
            //}
            
            
            function valoresFinalesHistoricoCausa() {
                copyNumeroDocumento();
                copyNUC();
                copyTipoSolicitud();
                copyNumeroFojasCHistorico();
                copyFechaRecepcionC();
                copyTipoRadicacion();
                copyObservaciones();

            }
            function copyNumeroDocumento() {
                var inputNumeroDocumento = $("#<%= inputNumeroArchivo.ClientID %>");
                  var copyInputNumeroDocumento = $("#<%= copyNumeroArchivo.ClientID %>");
                  copyInputNumeroDocumento.val(inputNumeroDocumento.val());
                  }
              function copyTipoSolicitud() {
                var inputTipoSolicitudHistorico = $("#<%= inputTipoSolicitudHistorico.ClientID %>");
                var copyInputTipoSolicitudHistorico = $("#<%= copyDropDownTipoAsunto.ClientID %>");
                copyInputTipoSolicitudHistorico.val(inputTipoSolicitudHistorico.val() === '27' ? 'INNOMINADA' : (inputTipoSolicitudHistorico.val() === '26' ? 'INICIAL': ''));
               }
               function copyNUC() {
                    var inputNUCHistorico = $("#<%= inputNUCHistorico.ClientID %>");
                    var copyTextBoxNUC = $("#<%= copyTextBoxNUC.ClientID %>");
                    copyTextBoxNUC.val(inputNUCHistorico.val());
               }
               function copyFechaRecepcionC() {
                    var inputFechaRecepcion = $("#<%= inputFechaRecepcionC.ClientID %>");
                    var copyFechaRecepcion = $("#<%= copyFechaRecepcionC.ClientID %>");
                    copyFechaRecepcion.val(inputFechaRecepcion.val());
               }
               function copyNumeroFojasCHistorico() {
                    var inputNumeroFojas = $(" #<%= inputNumeroFojasCHistorico.ClientID %>");
                    var copyNumeroFojas = $(" #<%= copyNumeroFojasCHistorico.ClientID %>");
                    copyNumeroFojas.val(inputNumeroFojas.val());
               }
               function copyTipoRadicacion( ) {
                    var inputTipoRadicaion = $(" #<%= inpuTipoRadicacion.ClientID %>");
                    var copyTipoRadicacion = $("#<%= copyTipoRadicacion.ClientID %>");
                    copyTipoRadicacion.val(inputTipoRadicaion.val() === 'C' ? 'C/DETENIDO' : (inpuTipoRadicacion.val() === 'S' ? 'S/DETENIDO' : ''));
               }
               function copyObservaciones()  {
                    var inputObservaciones = $(" #<%= inputObservaciones.ClientID %>");
                    var copyInputObservaciones = $("#<%= copyObservaciones.ClientID %>");
                copyInputObservaciones.val(inputObservaciones.val());
               }

             </script>
    </ContentTemplate>
</asp:UpdatePanel>
 
