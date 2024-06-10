<%@ Control Language="C#" AutoEventWireup="true" EnableViewState="true" CodeBehind="CustomExhorto.ascx.cs" Inherits="SIPOH.Views.CustomExhorto" %>

<style type="text/css">
    .mayusculas {
        text-transform: uppercase;
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
            "progressBar": true,
            "positionClass": "toast-bottom-right",
            "timeOut": "5000", // Time in milliseconds
            // You can customize other options as needed
        };
    </script>

<asp:UpdatePanel ID="updPanel" runat="server" EnableViewState="true" UpdateMode="Conditional">
    <ContentTemplate>

        <div class="container col-12">
            <div style="text-align: center; padding: 3%">
                <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                    <h3 runat="server" visible="true" style="text-align: center" id="titulo">Elija la opción que desee</h3>
                    <h3 runat="server" visible="false" style="text-align: center" id="lblExhorto">Registro de Exhorto</h3>
                    <h3 runat="server" visible="false" style="text-align: center" id="lblDespacho">Registro de Despacho</h3>
                    <h3 runat="server" visible="false" style="text-align: center" id="lblRequisitoria">Registro de Requisitoria</h3>
                </div>
            </div>
        </div>

        <div class="container">
            <div class="row align-items-center">
                <div class="col-3">
                    <label class="form-label text-secondary">Acción a elegir: </label>
                    <asp:DropDownList ID="OpExhorto" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OpExhorto_SelectedIndexChanged">
                        <asp:ListItem Value="SO">Selecciona una opción</asp:ListItem>
                        <asp:ListItem Value="E">Exhorto</asp:ListItem>
                        <asp:ListItem Value="D">Despacho</asp:ListItem>
                        <asp:ListItem Value="R">Requisitoria</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <br>


        <asp:Panel ID="Panel1" runat="server" Visible="False">
            <!-- Contenido del formulario para Exhorto -->
            <%-- Formularios de texto Exhorto --%>
            <div class="row pt-5">
                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                        <label for="numdoc1" class="form-label text-secondary">Número de Documento:</label>
                        <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Número de Documento" ID="numdoc1"></asp:TextBox>
                    </div>
                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                        <label for="Procedencia" class="form-label text-secondary">Procedente de:</label>
                        <asp:TextBox runat="server" ID="procede1" CssClass="form-control form-control-sm mayusculas" placeholder="Procedencia"></asp:TextBox>
                    </div>
                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                        <label for="fecha1" class="form-label text-secondary">Fecha de Recepción: </label>
                        <asp:TextBox runat="server" ID="fecha1" CssClass="form-control form-control-sm mayusculas" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                    </div>
                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                        <label for="fojas1" class="form-label text-secondary">Número de Fojas: </label>
                        <asp:TextBox runat="server" ID="fojas1" CssClass="form-control form-control-sm mayusculas" Text="0" Type="Number"></asp:TextBox>
                    </div>
                    <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                        <label class="form-label text-secondary">Prioridad: </label>
                        <div class=" d-flex align-items-center">
                            <asp:RadioButtonList ID="prioridad" runat="server" CssClass="mayusculas form-check">
                                <asp:ListItem Text="Alta" Value="A" Selected="False" />
                                <asp:ListItem Text="Normal" Value="N" Selected="False" />
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                        <label for="Diligencia1" class="form-label text-secondary">Diligecia Solicitada: </label>
                        <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese Diligencia" ID="Diligencia1"></asp:TextBox>
                    </div>
                    <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                        <label for="observa1" class="form-label text-secondary">Observaciones: </label>
                        <asp:TextBox ID="observa1" runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Observaciones"></asp:TextBox>
                    </div>


                    <!-- Tabla Partes Exhorto -->
                    <div class="col-12 col-lg-6 mb-4">
                        <div class="text-left">
                            <div class="mb-2">
                                <br />
                                <span class="text-success fw-bold m-2"><i class="bi bi-emoji-laughing"></i>Parte(s) a notificar: </span>
                                <asp:Button ID="Button11" runat="server" CssClass="btn btn-success btn-sm mayusculas" Text="+" OnClick="btnAbrirModal" />
                            </div>
                            <div class="table-responsive">
                                <asp:GridView ID="gvPartes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover table-sm" OnRowCommand="gvPartes_RowCommand" OnRowDataBound="gvPartes_RowDataBound" ClientIDMode="Static" ShowHeaderWhenEmpty="True" >
                                    <Columns>
                                        <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                            <HeaderStyle CssClass="bg-success text-white" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Genero" HeaderText="Género">
                                            <HeaderStyle CssClass="bg-success text-white" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Parte" HeaderText="Parte">
                                            <HeaderStyle CssClass="bg-success text-white" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                            <ItemTemplate>
                                                <asp:Button ID="btnEliminar" runat="server" Text="✖️" CssClass="btn btn-sm m-0 p-0" CommandName="EliminarParte" CommandArgument='<%# Container.DisplayIndex %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                            <%-- Tabla Delitos Exhorto --%>
                            <div class="col-12 col-lg-6 text-left">
                                <div class="mb-0 row">
                                    <div class="col-md-5">
                                        <span class="text-success fw-bold m-2"><i class="fa-solid fa-people-robbery"></i>Delitos: </span>
                                        <asp:DropDownList ID="ddlDelitos1" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                            <asp:ListItem runat="server" Value="Seleccionar" Text="Seleccione un delito:"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-1 d-flex align-items-end ">
                                        <asp:Button ID="btnAgregarDelito" runat="server" CssClass="btn btn-success btn-sm mayusculas" Text="+" OnClick="btnAgregarDelito_Click" />
                                    </div>
                                </div>
                                <div class="table-responsive mt-2">
                                    <asp:GridView ID="gvDelitos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover mb-0 table-sm" OnRowCommand="gvDelitos_RowCommand" ClientIDMode="Static" ShowHeaderWhenEmpty="True" >
                                        <Columns>
                                            <asp:TemplateField HeaderText="Id Delito" SortExpression="NombreDelito" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIdDelito" CssClass="text-center" runat="server" Text='<%# Bind("IdDelito") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="bg-success text-white text-center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Delito" SortExpression="NombreDelito">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblNombreDelito" runat="server" Text='<%# Bind("NombreDelito") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle CssClass="bg-success text-white text-center font-weight-bold" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                                <ItemTemplate>
                                                    <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" CommandName="EliminarDelito" CommandArgument='<%# Container.DisplayIndex %>' />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>


                <%-- Tabla Anexos Exhorto --%>
                <div class="col-12 col-lg-6 text-left">
                    <div class="mb-0 row">
                        <div class="col-md-5">
                            <br />
                            <label for="ddlAnexos" class="form-label text-secondary align-self-center">Anexos: </label>
                            <asp:DropDownList ID="ddlAnexos" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                <asp:ListItem runat="server" Text="Seleccione el anexo a agregar:"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="noAnexos" class="form-label text-secondary align-self-center">Cantidad de Anexos: </label>
                            <asp:TextBox runat="server" ID="noAnexos" CssClass="form-control form-control-sm" Text="0" Type="Number"></asp:TextBox>
                        </div>
                        <div class="col-1 d-flex align-items-end">
                            <asp:Button ID="addAnexo" runat="server" CssClass="btn btn-success mayusculas" Text="+" OnClick="btnAgregarAnexo_Click" />
                        </div>

                        <div class="table-responsive mt-2">
                            <asp:GridView ID="gvAnexos" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover mb-0 table-sm" OnRowCommand="gvAnexos_RowCommand" ClientIDMode="Static" ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Anexo" SortExpression="NombreDelito">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAnexo" runat="server" CssClass="mayusculas" Text='<%# Bind("descripcion") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cantidad" SortExpression="NombreDelito">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantAnexos" runat="server" Text='<%# Bind("Cantidad") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" CommandName="EliminarAnexo" CommandArgument='<%# Container.DisplayIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                </div>

            <br />
            <br />

                <div class="col-12 col-lg-12 text-left">
                    <div class="mb-0 row">
                        <center>
                            <asp:Button ID="ObDatos" runat="server" CssClass="btn btn-outline-success btn-sm mayusculas" Text="💾 ENVIAR" OnClick="ObtenerDatosYMostrarModal" />
                        </center>
                    </div>
                </div>

            <br />
            <br />

             <!-- Modal Agregar Partes -->
 <div class="modal fade" id="ModalPartes">
     <div class="modal-dialog modal-dialog-centered modal-lg">
         <div class="modal-content">
             <div class="modal-header">
                 <h1 class="modal-title fs-5" id="exampleModalLabel2">Agregar Partes</h1>
                 <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
             </div>
             <div class="modal-body">
                 <form id="myForm">
                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Parte: </h6>
                             <asp:DropDownList ID="parte" runat="server" CssClass="form-select mayusculas" onchange="habilitarTextBoxSelecParte()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="V">Victima</asp:ListItem>
                                 <asp:ListItem Value="I">Imputado</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeParte" runat="server" CssClass="form-control mayusculas" placeholder="Ingrese el tipo de parte" Enabled="false"></asp:TextBox>
                         </div>
                     </div>

                     <br />


                     <div class="row" style="padding: 2%">
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Nombre(s): </h6>
                             <asp:TextBox ID="nom2" runat="server" CssClass="form-control mayusculas" placeholder="Nombre"></asp:TextBox>
                         </div>
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido Paterno: </h6>
                             <asp:TextBox ID="ap2" runat="server" CssClass="form-control mayusculas" placeholder="A. Paterno"></asp:TextBox>
                         </div>

                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido materno: </h6>
                             <asp:TextBox ID="am2" runat="server" CssClass="form-control mayusculas" placeholder="A. Materno"></asp:TextBox>
                         </div>
                     </div>

                     <br />

                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Sexo: </h6>
                             <asp:DropDownList ID="sexo" runat="server" CssClass="form-select mayusculas" onchange="habilitarTextBoxSelecSexo()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="M">Masculino</asp:ListItem>
                                 <asp:ListItem Value="F">Femenino</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeSexo" runat="server" CssClass="form-control mayusculas" placeholder="Ingrese el sexo:" Enabled="false"></asp:TextBox>
                         </div>
                     </div>
                 </form>
                 <br />

             </div>
             <div class="modal-footer">
                 <button id="btnCerrar" class="btn btn-outline-secondary btn-sm mayusculas" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cerrar</button>
                 <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-outline-success btn-sm mayusculas" Text="Agregar" OnClick="btnAgregarParte_Click" />
             </div>
         </div>
     </div>
 </div>


 <!-- Modal confirmar datos -->
 <div class="modal fade" id="ModalConfirmacion1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
     <div class="modal-dialog" role="document">
         <div class="modal-content">
             <div class="modal-header" style="background-color: #ffcc00">
                 <h5 class="modal-title mayusculas" style="color: white; font-weight: bold">¿Los datos son correctos?<br />
                     Revise los datos antes de continuar...</h5>
                 <button type="button" class="close" data-dismiss="modal" style="color: white" aria-label="Cerrar">
                     <span aria-hidden="true">&times;</span>
                 </button>
             </div>
             <div class="modal-body mayusculas">
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-outline-secondary btn-sm mayusculas" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cerrar</button>
                 <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar cambios" CssClass="btn btn-outline-success btn-sm mayusculas" OnClick="btnGuardarDatosJudiciales_Click"  />
                 
             </div>
         </div>
     </div>
 </div>
        </asp:Panel>



        <asp:Panel ID="Panel2" runat="server" Visible="False">
            <!-- Contenido del formulario para Despacho -->
            <%-- Formularios de texto Despacho --%>
            <div class="row pt-5">
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="numdesp" class="form-label text-secondary">Número de Despacho:</label>
                    <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Número de Despacho" ID="numdesp"></asp:TextBox>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="quejoso" class="form-label text-secondary">Quejoso:</label>
                    <asp:TextBox runat="server" ID="quejoso" CssClass="form-control form-control-sm mayusculas" placeholder="Quejoso"></asp:TextBox>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="fecha2" class="form-label text-secondary">Fecha de Recepción: </label>
                    <asp:TextBox runat="server" ID="fecha2" CssClass="form-control form-control-sm mayusculas" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="fojas2" class="form-label text-secondary">NNúmero de Envio: </label>
                    <asp:TextBox runat="server" ID="fojas2" CssClass="form-control form-control-sm mayusculas" Text="0" Type="Number"></asp:TextBox>
                </div>
                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label class="form-label text-secondary">Prioridad: </label>
                    <div class=" d-flex align-items-center">
                        <asp:RadioButtonList ID="prioridad2" runat="server" CssClass="mayusculas form-check">
                            <asp:ListItem Text="Alta" Value="A" Selected="False" />
                            <asp:ListItem Text="Normal" Value="N" Selected="False" />
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="Diligencia1" class="form-label text-secondary">Diligecia Solicitada: </label>
                    <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese Diligencia" ID="Diligencia2"></asp:TextBox>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="observa1" class="form-label text-secondary">Observaciones: </label>
                    <asp:TextBox ID="observa2" runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Observaciones"></asp:TextBox>
                </div>


                <!-- Tabla Partes Despacho -->
                <div class="col-12 col-lg-6 mb-4">
                    <div class="text-left">
                        <div class="mb-2">
                            <br />
                            <span class="text-success fw-bold m-2"><i class="bi bi-emoji-laughing"></i>Parte(s) a notificar: </span>
                            <asp:Button ID="Button1" runat="server" CssClass="btn btn-success btn-sm mayusculas" Text="+" OnClick="btnAbrirModal2" />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="gvPartes2" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover table-sm" OnRowCommand="gvPartes_RowCommand2" OnRowDataBound="GridView1_RowDataBound2" ClientIDMode="Static" ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Genero" HeaderText="Género">
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Parte" HeaderText="Parte">
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminar" runat="server" Text="✖️" CssClass="btn btn-sm m-0 p-0" CommandName="EliminarParte" CommandArgument='<%# Container.DisplayIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <%-- Tabla Delitos Despacho --%>
                <div class="col-12 col-lg-6 text-left">
                    <div class="mb-0 row">
                        <div class="col-md-5">
                            <span class="text-success fw-bold m-2"><i class="fa-solid fa-people-robbery"></i>Delitos: </span>
                            <asp:DropDownList ID="ddlDelitos2" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                <asp:ListItem runat="server" Value="Seleccionar" Text="Seleccione un delito:"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 d-flex align-items-end ">
                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-success btn-sm mayusculas" Text="+" OnClick="btnAgregarDelito_Click2" />
                        </div>
                    </div>
                    <div class="table-responsive mt-2">
                        <asp:GridView ID="gvDelitos2" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover mb-0 table-sm" OnRowCommand="gvDelitos_RowCommand2" ClientIDMode="Static" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Id Delito" SortExpression="NombreDelito" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdDelito2" CssClass="text-center" runat="server" Text='<%# Bind("IdDelito2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="bg-success text-white text-center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delito" SortExpression="NombreDelito">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombreDelito2" runat="server" Text='<%# Bind("NombreDelito2") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="bg-success text-white text-center font-weight-bold" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" CommandName="EliminarDelito2" CommandArgument='<%# Container.DisplayIndex %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>


                <%-- Tabla Anexos Despacho --%>
                <div class="col-12 col-lg-6 text-left">
                    <div class="mb-0 row">
                        <div class="col-md-5">
                            <br />
                            <label for="ddlAnexos2" class="form-label text-secondary align-self-center">Anexos: </label>
                            <asp:DropDownList ID="ddlAnexos2" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                <asp:ListItem runat="server" Text="Seleccione el anexo a agregar:"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="noAnexos2" class="form-label text-secondary align-self-center">Cantidad de Anexos: </label>
                            <asp:TextBox runat="server" ID="noAnexos2" CssClass="form-control form-control-sm" Text="0" Type="Number"></asp:TextBox>
                        </div>
                        <div class="col-1 d-flex align-items-end">
                            <asp:Button ID="addAnexos2" runat="server" CssClass="btn btn-success mayusculas" Text="+" OnClick="btnAgregarAnexo_Click2" />
                        </div>

                        <div class="table-responsive mt-2">
                            <asp:GridView ID="gvAnexos2" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover mb-0 table-sm" OnRowCommand="gvAnexos_RowCommand2" ClientIDMode="Static" ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Anexo" SortExpression="NombreDelito">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAnexo2" runat="server" CssClass="mayusculas" Text='<%# Bind("descripcion2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cantidad" SortExpression="NombreDelito">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantAnexos2" runat="server" Text='<%# Bind("Cantidad2") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminar2" runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" CommandName="EliminarAnexo2" CommandArgument='<%# Container.DisplayIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="col-12 col-lg-12 text-left">
                <div class="mb-0 row">
                    <center>
                        <asp:Button ID="Button15" runat="server" CssClass="btn btn-outline-success btn-sm mayusculas" Text="💾 ENVIAR" OnClick="ObtenerDatosYMostrarModal2" />
                    </center>
                </div>
            </div>

            <br />
            <br />


            

             <!-- Modal Agregar Partes -->
 <div class="modal fade" id="ModalPartes2">
     <div class="modal-dialog modal-dialog-centered modal-lg">
         <div class="modal-content">
             <div class="modal-header">
                 <h1 class="modal-title fs-5" id="exampleModalLabel3">Agregar Partes</h1>
                 <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
             </div>
             <div class="modal-body">
                 <form id="myForm2">
                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Parte: </h6>
                             <asp:DropDownList ID="parte2" runat="server" CssClass="form-select mayusculas" onchange="habilitarTextBoxSelecParte2()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="V">Victima</asp:ListItem>
                                 <asp:ListItem Value="I">Imputado</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeParte2" runat="server" CssClass="form-control mayusculas" placeholder="Ingrese el tipo de parte" Enabled="false"></asp:TextBox>
                         </div>
                     </div>

                     <br />


                     <div class="row" style="padding: 2%">
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Nombre(s): </h6>
                             <asp:TextBox ID="nom3" runat="server" CssClass="form-control mayusculas" placeholder="Nombre"></asp:TextBox>
                         </div>
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido Paterno: </h6>
                             <asp:TextBox ID="ap3" runat="server" CssClass="form-control mayusculas" placeholder="A. Paterno"></asp:TextBox>
                         </div>

                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido materno: </h6>
                             <asp:TextBox ID="am3" runat="server" CssClass="form-control mayusculas" placeholder="A. Materno"></asp:TextBox>
                         </div>
                     </div>

                     <br />

                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Sexo: </h6>
                             <asp:DropDownList ID="sexo2" runat="server" CssClass="form-select mayusculas" onchange="habilitarTextBoxSelecSexo2()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="M">Masculino</asp:ListItem>
                                 <asp:ListItem Value="F">Femenino</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeSexo2" runat="server" CssClass="form-control mayusculas" placeholder="Ingrese el sexo:" Enabled="false"></asp:TextBox>
                         </div>
                     </div>
                 </form>
                 <br />

             </div>
             <div class="modal-footer">
                 <button id="btnCerrar" class="btn btn-outline-secondary btn-sm mayusculas" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                 <asp:Button ID="Button4" runat="server" CssClass="btn btn-outline-success btn-sm mayusculas" Text="Agregar" OnClick="btnAgregarParte_Click2" />
             </div>
         </div>
     </div>
 </div>


 <!-- Modal confirmar datos -->
 <div class="modal fade" id="ModalConfirmacion2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
     <div class="modal-dialog" role="document">
         <div class="modal-content">
             <div class="modal-header" style="background-color: #ffcc00">
                 <h5 class="modal-title mayusculas" style="color: white; font-weight: bold">¿Los datos son correctos?<br />
                     Revise los datos antes de continuar...</h5>
                 <button type="button" class="close" data-dismiss="modal" style="color: white" aria-label="Cerrar">
                     <span aria-hidden="true">&times;</span>
                 </button>
             </div>
             <div class="modal-body mayusculas">
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-outline-secondary btn-sm mayusculas" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                 <asp:Button ID="Button6" runat="server" Text="Guardar cambios" CssClass="btn btn-outline-success btn-sm mayusculas" OnClick="btnGuardarDatosJudiciales_Click2" />
             </div>
         </div>
     </div>
 </div>



        </asp:Panel>


        <asp:Panel ID="Panel3" runat="server" Visible="False">
            <!-- Contenido del formulario para Requisitoria -->
            <%-- Formularios de texto Requisitoria --%>
            <div class="row pt-5">
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="numdesp" class="form-label text-secondary">Número de toca:</label>
                    <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Número de toca" ID="numtoca" onchange="formatoNumeroToca(this)"></asp:TextBox>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="quejoso" class="form-label text-secondary">Sala de Procedencia:</label>
                    <asp:TextBox runat="server" ID="salaproc" CssClass="form-control form-control-sm mayusculas" placeholder="Sala"></asp:TextBox>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="fecha2" class="form-label text-secondary">Fecha de Recepción: </label>
                    <asp:TextBox runat="server" ID="fecha3" CssClass="form-control form-control-sm mayusculas" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="fojas2" class="form-label text-secondary">Numero de Fojas: </label>
                    <asp:TextBox runat="server" ID="fojas3" CssClass="form-control form-control-sm mayusculas" Text="0" Type="Number"></asp:TextBox>
                </div>
                <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label class="form-label text-secondary">Prioridad: </label>
                    <div class=" d-flex align-items-center">
                        <asp:RadioButtonList ID="prioridad3" runat="server" CssClass="mayusculas form-check">
                            <asp:ListItem Text="Alta" Value="A" Selected="False" />
                            <asp:ListItem Text="Normal" Value="N" Selected="False" />
                        </asp:RadioButtonList>
                    </div>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="Diligencia1" class="form-label text-secondary">Diligecia Solicitada: </label>
                    <asp:TextBox runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Ingrese Diligencia" ID="Diligencia3"></asp:TextBox>
                </div>
                <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
                    <label for="observa1" class="form-label text-secondary">Observaciones: </label>
                    <asp:TextBox ID="observa3" runat="server" CssClass="form-control form-control-sm mayusculas" placeholder="Observaciones"></asp:TextBox>
                </div>


                <!-- Tabla Partes Requisitoria -->
                <div class="col-12 col-lg-6 mb-4">
                    <div class="text-left">
                        <div class="mb-2">
                            <br />
                            <span class="text-success fw-bold m-2"><i class="bi bi-emoji-laughing"></i>Parte(s) a notificar: </span>
                            <asp:Button ID="Button3" runat="server" CssClass="btn btn-success btn-sm mayusculas" Text="+" OnClick="btnAbrirModal3" />
                        </div>
                        <div class="table-responsive">
                            <asp:GridView ID="gvPartes3" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover table-sm" OnRowCommand="gvPartes_RowCommand3" OnRowDataBound="GridView1_RowDataBound3" ClientIDMode="Static" ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Genero" HeaderText="Género">
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Parte" HeaderText="Parte">
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminar" runat="server" Text="✖️" CssClass="btn btn-sm m-0 p-0" CommandName="EliminarParte" CommandArgument='<%# Container.DisplayIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <%-- Tabla Delitos Requisitoria --%>
                <div class="col-12 col-lg-6 text-left">
                    <div class="mb-0 row">
                        <div class="col-md-5">
                            <span class="text-success fw-bold m-2"><i class="fa-solid fa-people-robbery"></i>Delitos: </span>
                            <asp:DropDownList ID="ddlDelitos3" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                <asp:ListItem runat="server" Value="Seleccionar" Text="Seleccione un delito:"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-1 d-flex align-items-end ">
                            <asp:Button ID="Button12" runat="server" CssClass="btn btn-success btn-sm mayusculas" Text="+" OnClick="btnAgregarDelito_Click3" />
                        </div>
                    </div>
                    <div class="table-responsive mt-2">
                        <asp:GridView ID="gvDelitos3" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover mb-0 table-sm" OnRowCommand="gvDelitos_RowCommand3" ClientIDMode="Static" ShowHeaderWhenEmpty="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Id Delito" SortExpression="NombreDelito" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblIdDelito3" CssClass="text-center" runat="server" Text='<%# Bind("IdDelito3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="bg-success text-white text-center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Delito" SortExpression="NombreDelito">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNombreDelito3" runat="server" Text='<%# Bind("NombreDelito3") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle CssClass="bg-success text-white text-center font-weight-bold" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" CommandName="EliminarDelito3" CommandArgument='<%# Container.DisplayIndex %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>


                <%-- Tabla Anexos Requisitoria --%>
                <div class="col-12 col-lg-6 text-left">
                    <div class="mb-0 row">
                        <div class="col-md-5">
                            <br />
                            <label for="ddlAnexos3" class="form-label text-secondary align-self-center">Anexos: </label>
                            <asp:DropDownList ID="ddlAnexos3" CssClass="form-select form-select-sm text-secondary mayusculas" runat="server">
                                <asp:ListItem runat="server" Text="Seleccione el anexo a agregar:"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            <label for="noAnexos3" class="form-label text-secondary align-self-center">Cantidad de Anexos: </label>
                            <asp:TextBox runat="server" ID="noAnexos3" CssClass="form-control form-control-sm" Text="0" Type="Number"></asp:TextBox>
                        </div>
                        <div class="col-1 d-flex align-items-end">
                            <asp:Button ID="Button13" runat="server" CssClass="btn btn-success mayusculas" Text="+" OnClick="btnAgregarAnexo_Click3" />
                        </div>

                        <div class="table-responsive mt-2">
                            <asp:GridView ID="gvAnexos3" runat="server" AutoGenerateColumns="False" CssClass="table table-striped text-center table-hover mb-0 table-sm" OnRowCommand="gvAnexos_RowCommand3" ClientIDMode="Static" ShowHeaderWhenEmpty="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="Anexo" SortExpression="NombreDelito">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAnexo3" runat="server" CssClass="mayusculas" Text='<%# Bind("descripcion3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Cantidad" SortExpression="NombreDelito">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCantAnexos3" runat="server" Text='<%# Bind("Cantidad3") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="bg-success text-white" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                        <ItemTemplate>
                                            <asp:Button ID="btnEliminar" runat="server" CssClass="btn btn-sm m-0 p-0" Text="✖️" CommandName="EliminarAnexo3" CommandArgument='<%# Container.DisplayIndex %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="col-12 col-lg-12 text-left">
                <div class="mb-0 row">
                    <center>
                        <asp:Button ID="Button14" runat="server" CssClass="btn btn-outline-success btn-sm mayusculas" Text="💾 ENVIAR" OnClick="ObtenerDatosYMostrarModal3" />
                    </center>
                </div>
            </div>

            <br />
            <br />

             <!-- Modal Agregar Partes -->
 <div class="modal fade" id="ModalPartes3">
     <div class="modal-dialog modal-dialog-centered modal-lg">
         <div class="modal-content">
             <div class="modal-header">
                 <h1 class="modal-title fs-5" id="exampleModalLabel4">Agregar Partes</h1>
                 <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
             </div>
             <div class="modal-body">
                 <form id="myForm3">
                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Parte: </h6>
                             <asp:DropDownList ID="parte3" runat="server" CssClass="form-select mayusculas" onchange="habilitarTextBoxSelecParte3()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="V">Victima</asp:ListItem>
                                 <asp:ListItem Value="I">Imputado</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeParte3" runat="server" CssClass="form-control mayusculas" placeholder="Ingrese el tipo de parte" Enabled="false"></asp:TextBox>
                         </div>
                     </div>

                     <br />


                     <div class="row" style="padding: 2%">
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Nombre(s): </h6>
                             <asp:TextBox ID="nom4" runat="server" CssClass="form-control mayusculas" placeholder="Nombre"></asp:TextBox>
                         </div>
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido Paterno: </h6>
                             <asp:TextBox ID="ap4" runat="server" CssClass="form-control mayusculas" placeholder="A. Paterno"></asp:TextBox>
                         </div>

                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido materno: </h6>
                             <asp:TextBox ID="am4" runat="server" CssClass="form-control mayusculas" placeholder="A. Materno"></asp:TextBox>
                         </div>
                     </div>

                     <br />

                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Sexo: </h6>
                             <asp:DropDownList ID="sexo3" runat="server" CssClass="form-select mayusculas" onchange="habilitarTextBoxSelecSexo3()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="M">Masculino</asp:ListItem>
                                 <asp:ListItem Value="F">Femenino</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeSexo3" runat="server" CssClass="form-control mayusculas" placeholder="Ingrese el sexo:" Enabled="false"></asp:TextBox>
                         </div>
                     </div>
                 </form>
                 <br />

             </div>
             <div class="modal-footer">
                 <button id="btnCerrar" class="btn btn-outline-secondary btn-sm mayusculas" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                 <asp:Button ID="Button5" runat="server" CssClass="btn btn-outline-success btn-sm mayusculas" Text="Agregar" OnClick="btnAgregarParte_Click3" />
             </div>
         </div>
     </div>
 </div>


 <!-- Modal confirmar datos -->
 <div class="modal fade" id="ModalConfirmacion3" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
     <div class="modal-dialog" role="document">
         <div class="modal-content">
             <div class="modal-header" style="background-color: #ffcc00">
                 <h5 class="modal-title mayusculas" style="color: white; font-weight: bold">¿Los datos son correctos?<br />
                     Revise los datos antes de continuar...</h5>
                 <button type="button" class="close" data-dismiss="modal" style="color: white" aria-label="Cerrar">
                     <span aria-hidden="true">&times;</span>
                 </button>
             </div>
             <div class="modal-body">
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-outline-secondary btn-sm mayusculas" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                 <asp:Button ID="Button7" runat="server" Text="Guardar cambios" CssClass="btn btn-outline-success btn-sm mayusculas" OnClick="btnGuardarDatosJudiciales_Click3" />
             </div>
         </div>
     </div>
 </div>

            </asp:Panel>

        <div id="InsertExhorto" runat="server">
            <div class="d-flex justify-content-center align-items-center flex-column" runat="server">
                <asp:Label ID="TituloExhorto" runat="server" CssClass="h4 text-center" Text="¡Registro de exhorto listo! 🎉" />
                <br />
                <asp:Button ID="btnImprimir" runat="server" Text="🖨️ Imprimir Ticket" OnClick="btnImprimir_Click" CssClass="btn btn-outline-success" />
                <br />
                <asp:Button ID="btnGenerarOtro" runat="server" Text="🔙 Insertar Otro Exhorto" OnClick="GenerarOtro_Click" CssClass="btn btn-outline-success" />
            </div>
            <br />
            <br />
            <div class="d-flex justify-content-center align-items-center flex-column" runat="server">
                <pre id="TicketDiv" runat="server"></pre>
            </div>
        </div>

    </ContentTemplate>
</asp:UpdatePanel>


<script>

    //Funciones Exhorto
    function ObtenerDatosYMostrarModal() {
        // Obtener los nombres de las partes
        var gvPartes = $('#<%= gvPartes.ClientID %> tr:gt(0)');
        var listaPartes = $('#listaPartes');
        listaPartes.empty(); // Limpiar la lista antes de agregar nuevos elementos

        var parteSeleccionada = ''; // Variable para almacenar partes seleccionadas

        // Verificar si la tabla de partes está vacía
        if (gvPartes.length == 0) {
            toastr.error('Debe agregar al menos una parte.');
            return;
        }

        gvPartes.each(function () {
            var nombreParte = $(this).find('td:eq(0)').text().trim(); // Asumiendo que el nombre está en la primera celda
            var li = $('<li>').text(nombreParte);
            listaPartes.append(li);

            // Lógica para seleccionar partes (ajustar según sea necesario)
            parteSeleccionada += nombreParte + ', ';
        });

        // Obtener los nombres de los delitos
        var gvDelitos = $('#<%= gvDelitos.ClientID %> tr:gt(0)');
        var listaDelitos = $('#listaDelitos');
        listaDelitos.empty(); // Limpiar la lista antes de agregar nuevos elementos

        var delitoSeleccionado = ''; // Variable para almacenar delitos seleccionados

        // Verificar si la tabla de delitos está vacía
        if (gvDelitos.length == 0) {
            toastr.error('Debe agregar al menos un delito.');
            return;
        }

        gvDelitos.each(function () {
            var nombreDelito = $(this).find('td:eq(0)').text().trim(); // Asumiendo que el nombre del delito está en la segunda celda
            var li = $('<li>').text(nombreDelito);
            listaDelitos.append(li);

            // Lógica para seleccionar delitos (ajustar según sea necesario)
            delitoSeleccionado += nombreDelito + ', ';
        });

        // Obtener los valores de los TextBox
        var numdoc = document.getElementById('<%= numdoc1.ClientID %>').value;
        var procede = document.getElementById('<%= procede1.ClientID %>').value;
        var fecha = document.getElementById('<%= fecha1.ClientID %>').value;
        var fojas = document.getElementById('<%= fojas1.ClientID %>').value;
        var observa = document.getElementById('<%= observa1.ClientID %>').value;

        var diligencia1 = document.getElementById('<%= Diligencia1.ClientID %>').value;

        // Verificar si el valor de "numdoc" está vacío
        if (numdoc == "") {
            toastr.error('El campo Número de Documento no puede estar vacío.');
            return;
        }

        // Verificar si el valor de "procede" está vacío
        if (procede == "") {
            toastr.error('El campo Procedente de no puede estar vacío.');
            return;
        }

        let fechaUsuario = new Date(fecha); // Fecha proporcionada por el usuario
        let fechaActual = new Date(); // Fecha actual

        // Verificar si la fecha del usuario es mayor a la fecha actual
        if (fechaUsuario > fechaActual) {
            toastr.error('La fecha no puede ser mayor a la fecha actual.');
            return;
        }

        // Verificar si el valor de "fecha" está vacío
        if (fecha == "") {
            toastr.error('El campo Fecha de Recepción no puede estar vacío.');
            return;
        }

        // Verificar si el valor de "fojas" es 0
        if (fojas == "0") {
            toastr.error('El campo Número de Fojas no puede ser 0.');
            return;
        }

        // Obtén todos los elementos de radio con el mismo nombre
        var prioridad = document.getElementsByName('<%= prioridad.UniqueID %>');
        // Verificar si se ha seleccionado una opción en "prioridad"
        var prioridadSeleccionada = false;
        for (var i = 0; i < prioridad.length; i++) {
            if (prioridad[i].checked) {
                prioridadSeleccionada = true;
                break;
            }
        }
        if (!prioridadSeleccionada) {
            toastr.error('Debe seleccionar una opción en el campo Prioridad.');
            return;
        }

        // Verificar si el valor de "Diligencia1" está vacío
        if (diligencia1 == "") {
            toastr.error('El campo Diligencia no puede estar vacío.');
            return;
        }

        // Si todas las validaciones pasan, entonces proceder con el resto del código

        // Abrir el modal
        $('#ModalConfirmacion1').modal('show');

        // Mostrar los valores en el modal
        document.getElementById('ModalConfirmacion1').querySelector('.modal-body').innerHTML =
            '<b>Número de Documento: </b>' + numdoc + '<br />' + '<br />' +
            '<b>Procedente de: </b>' + procede + '<br />' + '<br />' +
            '<b>Fecha de Recepción: </b>' + fecha + '<br />' + '<br />' +
            '<b>Número de Fojas: </b>' + fojas + '<br />' + '<br />' +
            '<b>Parte(s) a Notificar: </b>' + parteSeleccionada + '<br />' + '<br />' +
            '<b>Delito(s): </b>' + delitoSeleccionado + '<br />' + '<br />';
        '<b>Observaciones: </b>' + observa + ' <br /> ' + ' <br /> ';
        // Agregar más líneas según sea necesario
    }




    function AbrirModal() {
        $('#ModalPartes').modal('show'); // Utiliza jQuery para mostrar el modal
    }

    function AbrirModalConf() {
        $('#ModalConfirmacion1').modal('show'); // Utiliza jQuery para mostrar el modal
    }

    function CerrarModalGuardarDatos() {
        $('#guardarDatos').modal('hide');
        $('body').removeClass('modal-open').css('overflow', ''); // Restablece el overflow
        $('.modal-backdrop').remove();
    }

    function CerrarModalPartes() {
        $('#ModalPartes').modal('hide');
        $('body').removeClass('modal-open').css('overflow', ''); // Restablece el overflow
        $('.modal-backdrop').remove();
    }

    function LimpiarFormulario() {
        // Obtén referencias a los elementos del formulario
        var nom2 = document.getElementById('<%= nom2.ClientID %>');
        var ap2 = document.getElementById('<%= ap2.ClientID %>');
        var am2 = document.getElementById('<%= am2.ClientID %>');
        var parte = document.getElementById('<%= parte.ClientID %>');
        var sexo = document.getElementById('<%= sexo.ClientID %>');
        var espeParte = document.getElementById('<%= espeParte.ClientID %>');
        var espeSexo = document.getElementById('<%= espeSexo.ClientID %>');

        // Limpia los valores de los campos del formulario
        nom2.value = '';
        ap2.value = '';
        am2.value = '';
        parte.selectedIndex = -1; // Borra la selección
        sexo.selectedIndex = -1; // Borra la selección
        espeParte.value = '';
        espeSexo.value = '';
    }




    function habilitarTextBoxSelecParte() {
        var parteDropDown = document.getElementById('<%= parte.ClientID %>');
        var espeParteTextBox = document.getElementById('<%= espeParte.ClientID %>');

        // Verifica si la opción seleccionada es "Otro"
        if (parteDropDown.value === "O") {
            // Habilita el TextBox
            espeParteTextBox.disabled = false;
        } else {
            // Deshabilita el TextBox
            espeParteTextBox.disabled = true;
        }
    }

    function habilitarTextBoxSelecSexo() {
        var sexoDropDown = document.getElementById('<%= sexo.ClientID %>');
        var espeSexoTextBox = document.getElementById('<%= espeSexo.ClientID %>');

        // Verifica si la opción seleccionada es "Otro"
        if (sexoDropDown.value === "O") {
            // Habilita el TextBox
            espeSexoTextBox.disabled = false;
        } else {
            // Deshabilita el TextBox
            espeSexoTextBox.disabled = true;
        }

    }
    //Fin Funciones Exhorto

    //Funciones Despacho
    function ObtenerDatosYMostrarModal2() {
        // Obtener los nombres de las partes
        var gvPartes = $('#<%= gvPartes2.ClientID %> tr:gt(0)');
        var listaPartes = $('#listaPartes');
        listaPartes.empty(); // Limpiar la lista antes de agregar nuevos elementos

        var parteSeleccionada = ''; // Variable para almacenar partes seleccionadas

        // Verificar si la tabla de partes está vacía
        if (gvPartes.length == 0) {
            toastr.error('Debe agregar al menos una parte.');
            return;
        }

        gvPartes.each(function () {
            var nombreParte = $(this).find('td:eq(0)').text().trim(); // Asumiendo que el nombre está en la primera celda
            var li = $('<li>').text(nombreParte);
            listaPartes.append(li);

            // Lógica para seleccionar partes (ajustar según sea necesario)
            parteSeleccionada += nombreParte + ', ';
        });

        // Obtener los nombres de los delitos
        var gvDelitos = $('#<%= gvDelitos2.ClientID %> tr:gt(0)');
        var listaDelitos = $('#listaDelitos');
        listaDelitos.empty(); // Limpiar la lista antes de agregar nuevos elementos

        var delitoSeleccionado = ''; // Variable para almacenar delitos seleccionados

        // Verificar si la tabla de delitos está vacía
        if (gvDelitos.length == 0) {
            toastr.error('Debe agregar al menos un delito.');
            return;
        }

        gvDelitos.each(function () {
            var nombreDelito = $(this).find('td:eq(0)').text().trim(); // Asumiendo que el nombre del delito está en la segunda celda
            var li = $('<li>').text(nombreDelito);
            listaDelitos.append(li);

            // Lógica para seleccionar delitos (ajustar según sea necesario)
            delitoSeleccionado += nombreDelito + ', ';
        });

        // Obtener los valores de los TextBox
        var numdesp = document.getElementById('<%= numdesp.ClientID %>').value;
        var quejo = document.getElementById('<%= quejoso.ClientID %>').value;
        var fecha = document.getElementById('<%= fecha2.ClientID %>').value;
        var fojas = document.getElementById('<%= fojas2.ClientID %>').value;
        var observa = document.getElementById('<%= observa2.ClientID %>').value;
        var diligencia1 = document.getElementById('<%= Diligencia2.ClientID %>').value;

        // Verificar si el valor de "numdoc" está vacío
        if (numdesp == "") {
            toastr.error('El campo Número de despacho no puede estar vacío.');
            return;
        }

        // Verificar si el valor de "procede" está vacío
        if (quejo == "") {
            toastr.error('El campo Quejoso de no puede estar vacío.');
            return;
        }

        let fechaUsuario = new Date(fecha); // Fecha proporcionada por el usuario
        let fechaActual = new Date(); // Fecha actual

        // Verificar si la fecha del usuario es mayor a la fecha actual
        if (fechaUsuario > fechaActual) {
            toastr.error('La fecha no puede ser mayor a la fecha actual.');
            return;
        }

        // Verificar si el valor de "fecha" está vacío
        if (fecha == "") {
            toastr.error('El campo Fecha de Recepción no puede estar vacío.');
            return;
        }

        // Verificar si el valor de "fojas" es 0
        if (fojas == "0") {
            toastr.error('El campo Número de Fojas no puede ser 0.');
            return;
        }

        // Obtén todos los elementos de radio con el mismo nombre
        var prioridad = document.getElementsByName('<%= prioridad2.UniqueID %>');
        // Verificar si se ha seleccionado una opción en "prioridad"
        var prioridadSeleccionada = false;
        for (var i = 0; i < prioridad.length; i++) {
            if (prioridad[i].checked) {
                prioridadSeleccionada = true;
                break;
            }
        }
        if (!prioridadSeleccionada) {
            toastr.error('Debe seleccionar una opción en el campo Prioridad.');
            return;
        }

        // Verificar si el valor de "Diligencia1" está vacío
        if (diligencia1 == "") {
            toastr.error('El campo Diligencia no puede estar vacío.');
            return;
        }

        // Si todas las validaciones pasan, entonces proceder con el resto del código

        // Abrir el modal
        $('#ModalConfirmacion2').modal('show');

        // Mostrar los valores en el modal
        document.getElementById('ModalConfirmacion2').querySelector('.modal-body').innerHTML =
            '<b>Número de Documento: </b>' + numdesp + '<br />' + '<br />' +
            '<b>Procedente de: </b>' + quejo + '<br />' + '<br />' +
            '<b>Fecha de Recepción: </b>' + fecha + '<br />' + '<br />' +
            '<b>Número de Fojas: </b>' + fojas + '<br />' + '<br />' +
            '<b>Parte(s) a Notificar: </b>' + parteSeleccionada + '<br />' + '<br />' +
            '<b>Delito(s): </b>' + delitoSeleccionado + '<br />' + '<br />';
        '<b>Observaciones: </b>' + observa + ' <br /> ' + ' <br /> ';
        // Agregar más líneas según sea necesario
    }

    function AbrirModal2() {
        $('#ModalPartes2').modal('show'); // Utiliza jQuery para mostrar el modal
    }

    function CerrarModalGuardarDatos2() {
        $('#guardarDatos').modal('hide');
        $('body').removeClass('modal-open').css('overflow', ''); // Restablece el overflow
        $('.modal-backdrop').remove();
    }


    function LimpiarFormulario2() {
        // Obtén referencias a los elementos del formulario
        var nom2 = document.getElementById('<%= nom3.ClientID %>');
        var ap2 = document.getElementById('<%= ap3.ClientID %>');
        var am2 = document.getElementById('<%= am3.ClientID %>');
        var parte = document.getElementById('<%= parte2.ClientID %>');
        var sexo = document.getElementById('<%= sexo2.ClientID %>');
    var espeParte = document.getElementById('<%= espeParte2.ClientID %>');
        var espeSexo = document.getElementById('<%= espeSexo2.ClientID %>');

        // Limpia los valores de los campos del formulario
        nom2.value = '';
        ap2.value = '';
        am2.value = '';
        parte.selectedIndex = -1; // Borra la selección
        sexo.selectedIndex = -1; // Borra la selección
        espeParte.value = '';
        espeSexo.value = '';
    }

    function habilitarTextBoxSelecParte2() {
        var parteDropDown = document.getElementById('<%= parte2.ClientID %>');
            var espeParteTextBox = document.getElementById('<%= espeParte2.ClientID %>');

        // Verifica si la opción seleccionada es "Otro"
        if (parteDropDown.value === "O") {
            // Habilita el TextBox
            espeParteTextBox.disabled = false;
        } else {
            // Deshabilita el TextBox
            espeParteTextBox.disabled = true;
        }
    }

    function habilitarTextBoxSelecSexo2() {
        var sexoDropDown = document.getElementById('<%= sexo2.ClientID %>');
        var espeSexoTextBox = document.getElementById('<%= espeSexo2.ClientID %>');

        // Verifica si la opción seleccionada es "Otro"
        if (sexoDropDown.value === "O") {
            // Habilita el TextBox
            espeSexoTextBox.disabled = false;
        } else {
            // Deshabilita el TextBox
            espeSexoTextBox.disabled = true;
        }

    }

    //Fin Funciones Despacho



    //Funciones Requisitoria
    function ObtenerDatosYMostrarModal3() {
        // Obtener los nombres de las partes
        var gvPartes = $('#<%= gvPartes3.ClientID %> tr:gt(0)');
    var listaPartes = $('#listaPartes');
    listaPartes.empty(); // Limpiar la lista antes de agregar nuevos elementos

    var parteSeleccionada = ''; // Variable para almacenar partes seleccionadas

    // Verificar si la tabla de partes está vacía
    if (gvPartes.length == 0) {
        toastr.error('Debe agregar al menos una parte.');
        return;
    }

    gvPartes.each(function () {
        var nombreParte = $(this).find('td:eq(0)').text().trim(); // Asumiendo que el nombre está en la primera celda
        var li = $('<li>').text(nombreParte);
        listaPartes.append(li);

        // Lógica para seleccionar partes (ajustar según sea necesario)
        parteSeleccionada += nombreParte + ', ';
    });

    // Obtener los nombres de los delitos
    var gvDelitos = $('#<%= gvDelitos3.ClientID %> tr:gt(0)');
    var listaDelitos = $('#listaDelitos');
    listaDelitos.empty(); // Limpiar la lista antes de agregar nuevos elementos

    var delitoSeleccionado = ''; // Variable para almacenar delitos seleccionados

    // Verificar si la tabla de delitos está vacía
    if (gvDelitos.length == 0) {
        toastr.error('Debe agregar al menos un delito.');
        return;
    }

    gvDelitos.each(function () {
        var nombreDelito = $(this).find('td:eq(0)').text().trim(); // Asumiendo que el nombre del delito está en la segunda celda
        var li = $('<li>').text(nombreDelito);
        listaDelitos.append(li);

        // Lógica para seleccionar delitos (ajustar según sea necesario)
        delitoSeleccionado += nombreDelito + ', ';
    });

    // Obtener los valores de los TextBox
    var numtoca = document.getElementById('<%= numtoca.ClientID %>').value;
    var sala = document.getElementById('<%= salaproc.ClientID %>').value;
    var fecha = document.getElementById('<%= fecha3.ClientID %>').value;
    var fojas = document.getElementById('<%= fojas3.ClientID %>').value;
    var observa = document.getElementById('<%= observa3.ClientID %>').value;
    var diligencia1 = document.getElementById('<%= Diligencia3.ClientID %>').value;

    // Verificar si el valor de "numdoc" está vacío
        if (numtoca == "") {
        toastr.error('El campo Número de toca no puede estar vacío.');
        return;
    }

    // Verificar si el valor de "procede" está vacío
        if (sala == "") {
        toastr.error('El campo Sala de no puede estar vacío.');
        return;
    }

        let fechaUsuario = new Date(fecha); // Fecha proporcionada por el usuario
        let fechaActual = new Date(); // Fecha actual

        // Verificar si la fecha del usuario es mayor a la fecha actual
        if (fechaUsuario > fechaActual) {
            toastr.error('La fecha no puede ser mayor a la fecha actual.');
            return;
        }

        // Verificar si el valor de "fecha" está vacío
        if (fecha == "") {
            toastr.error('El campo Fecha de Recepción no puede estar vacío.');
            return;
        }

    // Verificar si el valor de "fojas" es 0
    if (fojas == "0") {
        toastr.error('El campo Número de Fojas no puede ser 0.');
        return;
        }

        // Obtén todos los elementos de radio con el mismo nombre
        var prioridad = document.getElementsByName('<%= prioridad3.UniqueID %>');
        // Verificar si se ha seleccionado una opción en "prioridad"
        var prioridadSeleccionada = false;
        for (var i = 0; i < prioridad.length; i++) {
            if (prioridad[i].checked) {
                prioridadSeleccionada = true;
                break;
            }
        }
        if (!prioridadSeleccionada) {
            toastr.error('Debe seleccionar una opción en el campo Prioridad.');
            return;
        }

    // Verificar si el valor de "Diligencia1" está vacío
    if (diligencia1 == "") {
        toastr.error('El campo Diligencia no puede estar vacío.');
        return;
    }

    // Si todas las validaciones pasan, entonces proceder con el resto del código

    // Abrir el modal
    $('#ModalConfirmacion3').modal('show');

    // Mostrar los valores en el modal
    document.getElementById('ModalConfirmacion3').querySelector('.modal-body').innerHTML =
        '<b>Número de Documento: </b>' + numtoca + '<br />' + '<br />' +
        '<b>Procedente de: </b>' + sala + '<br />' + '<br />' +
        '<b>Fecha de Recepción: </b>' + fecha + '<br />' + '<br />' +
        '<b>Número de Fojas: </b>' + fojas + '<br />' + '<br />' +
        '<b>Parte(s) a Notificar: </b>' + parteSeleccionada + '<br />' + '<br />' +
        '<b>Delito(s): </b>' + delitoSeleccionado + '<br />' + '<br />';
        '<b>Observaciones: </b>' + observa + ' <br /> ' + ' <br /> ';
    // Agregar más líneas según sea necesario
    }

    function AbrirModal3() {
        $('#ModalPartes3').modal('show'); // Utiliza jQuery para mostrar el modal
    }

    function CerrarModalGuardarDatos3() {
        $('#guardarDatos').modal('hide');
        $('body').removeClass('modal-open').css('overflow', ''); // Restablece el overflow
        $('.modal-backdrop').remove();
    }


    function LimpiarFormulario3() {
        // Obtén referencias a los elementos del formulario
    var nom2 = document.getElementById('<%= nom4.ClientID %>');
    var ap2 = document.getElementById('<%= ap4.ClientID %>');
    var am2 = document.getElementById('<%= am4.ClientID %>');
    var parte = document.getElementById('<%= parte3.ClientID %>');
    var sexo = document.getElementById('<%= sexo3.ClientID %>');
    var espeParte = document.getElementById('<%= espeParte3.ClientID %>');
    var espeSexo = document.getElementById('<%= espeSexo3.ClientID %>');

    // Limpia los valores de los campos del formulario
    nom2.value = '';
    ap2.value = '';
    am2.value = '';
    parte.selectedIndex = -1; // Borra la selección
    sexo.selectedIndex = -1; // Borra la selección
    espeParte.value = '';
    espeSexo.value = '';
}

function habilitarTextBoxSelecParte3() {
    var parteDropDown = document.getElementById('<%= parte3.ClientID %>');
        var espeParteTextBox = document.getElementById('<%= espeParte3.ClientID %>');

    // Verifica si la opción seleccionada es "Otro"
    if (parteDropDown.value === "O") {
        // Habilita el TextBox
        espeParteTextBox.disabled = false;
    } else {
        // Deshabilita el TextBox
        espeParteTextBox.disabled = true;
    }
}

function habilitarTextBoxSelecSexo3() {
    var sexoDropDown = document.getElementById('<%= sexo3.ClientID %>');
    var espeSexoTextBox = document.getElementById('<%= espeSexo3.ClientID %>');

    // Verifica si la opción seleccionada es "Otro"
    if (sexoDropDown.value === "O") {
        // Habilita el TextBox
        espeSexoTextBox.disabled = false;
    } else {
        // Deshabilita el TextBox
        espeSexoTextBox.disabled = true;
    }

}
    //Fin Funciones Requisitoria


    // Activar toast
    function EjemploExito() {
        var selectedValue = document.getElementById('<%= OpExhorto.ClientID %>').value;

        switch (selectedValue) {
            case 'E':
                toastr.success('Exhorto agregado con éxito');
                break;
            case 'D':
                toastr.success('Despacho agregado con éxito');
                break;
            case 'R':
                toastr.success('Requisitoria agregada con éxito');
                break;
            default:
                toastr.success('Registro Exitoso');
                break;
        }
    }


    function EjemploError(errorMessage) {
        toastr.error('Algo ha ocurrido con su transacción: <br>' + errorMessage + ' Error');
    }

    // Activar toast tbldelitos
    function EjemploErrorTblDelitos(errorMessage) {
        toastr.error('El delito ya existe en la tabla, seleccione uno diferente')
    }
    function EjemploErrorSelecTblDelitos(errorMessage) {
        toastr.error('El delito seleccionado no es valido, seleccione uno diferente')
    }

    function EjemploErrorTblAnexo(errorMessage) {
        toastr.error('El anexo ya existe en la tabla, seleccione uno diferente')
    }

    function EjemploErrorCantAnexo(errorMessage) {
        toastr.error('El número de anexos no puede ser 0')
    }

    function EjemploErrorSelAnexo(errorMessage) {
        toastr.error('El anexo seleccionado no es valido, seleccione uno diferente')
    }

    // Default Configuration
    $(document).ready(function () {
        toastr.options = {
            'closeButton': true,
            'debug': false,
            'newestOnTop': false,
            'progressBar': true,
            'positionClass': 'toast-bottom-right',
            'preventDuplicates': true,
            'showDuration': '1000',
            'hideDuration': '1000',
            'timeOut': '5000',
            'extendedTimeOut': '1000',
            'showEasing': 'swing',
            'hideEasing': 'linear',
            'showMethod': 'fadeIn',
            'hideMethod': 'fadeOut',
        }
    });

    function CerrarConfirmacion() {
        $('#guardarDatos').modal('hide');
        $('body').removeClass('modal-open').css('overflow', ''); // Restablece el overflow
        $('.modal-backdrop').remove();
    }

    function imprimirTicket() {
        var contenido = document.getElementById('<%= TicketDiv.ClientID %>').innerHTML;
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
        ventanaImpresion.onfocus = function () { setTimeout(function () { ventanaImpresion.close(); }, 500); }
    }

    function limpiarFormularioAnexos() {
        // Obtenemos los elementos del formulario
        var ddlAnexos = document.getElementById('<%= ddlAnexos.ClientID %>');
        var noAnexos = document.getElementById('<%= noAnexos.ClientID %>');

        // Restablecemos los valores
        ddlAnexos.selectedIndex = 0;
        noAnexos.value = 0;
    }

    function limpiarFormularioAnexos2() {
        // Obtenemos los elementos del formulario
        var ddlAnexos = document.getElementById('<%= ddlAnexos2.ClientID %>');
            var noAnexos = document.getElementById('<%= noAnexos2.ClientID %>');

            // Restablecemos los valores
            ddlAnexos.selectedIndex = 0;
            noAnexos.value = 0;
    }

    function limpiarFormularioAnexos3() {
        // Obtenemos los elementos del formulario
        var ddlAnexos = document.getElementById('<%= ddlAnexos3.ClientID %>');
        var noAnexos = document.getElementById('<%= noAnexos3.ClientID %>');

        // Restablecemos los valores
        ddlAnexos.selectedIndex = 0;
        noAnexos.value = 0;
    }

</script>

<script type="text/javascript">
    function formatoNumeroToca(input) {
        // Divide el valor ingresado en el número y el año
        var [number, year] = input.value.split("/");

        // Rellena el número con ceros a la izquierda hasta que tenga 4 dígitos
        number = number.padStart(4, '0');

        // Combina el número y el año para obtener el número de toca
        var numeroToca = number + "/" + year;

        // Establece el valor del input al número de toca
        input.value = numeroToca;
    }
</script>