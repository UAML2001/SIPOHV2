<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomExhorto.ascx.cs" Inherits="SIPOH.Views.CustomExhorto" %>

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

<div class="container col-12">
    <div style="text-align: center; padding: 3%">
        <div class="col-md-10 ml-auto col-xl-11 mr-auto">
            <h3 style="text-align: center" id="titulo">Elija la opcion que desee
            </h3>
        </div>
    </div>
</div>


<%--<div>
        ID: <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        Nombre: <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        Apellido: <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
        Email: <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
        <asp:Button ID="Button9" runat="server" Text="Insertar" OnClick="Button1_Click" />
    </div>--%>


<asp:UpdatePanel ID="updPanel" runat="server" EnableViewState="true" >
    <ContentTemplate>

        <div class="row ">
            <div class="col-md-3 col-sm-3 col-xs-3">
                <h6 class="help-block text-muted small-font">Accion a elegir: </h6>
                <asp:DropDownList ID="OpExhorto" CssClass="form-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OpExhorto_SelectedIndexChanged">
                    <asp:ListItem Value="SO">Selecciona una opción</asp:ListItem>
                    <asp:ListItem Value="E">Exhorto</asp:ListItem>
                    <asp:ListItem Value="D">Despacho</asp:ListItem>
                    <asp:ListItem Value="R">Requesitoria</asp:ListItem>
                </asp:DropDownList> 
            </div>
        </div>

        <br />


        <asp:Panel ID="Panel1" runat="server" Visible="False">
            <!-- Contenido del formulario para Exhorto -->
            <h3 style="text-align: center" id="lblExhorto">Registro de Exhorto</h3>
            <br />
            <br />

            <div class="row ">
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Numero de Documento: </h6>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Numero de Documento" ID="numdoc1"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Procedente de: </h6>
                    <asp:TextBox runat="server" ID="procede1" CssClass="form-control" placeholder="Procedencia"></asp:TextBox>
                </div>

                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Fecha de Recepción: </h6>
                    <asp:TextBox ID="fecha1" runat="server" CssClass="form-control" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Número de Fojas: </h6>
                    <div class="form-outline">
                        <asp:TextBox runat="server" ID="fojas1" CssClass="form-control" Text="0" Type="Number"></asp:TextBox>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row ">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Parte a Notificar: </h6>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="row ">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <h6 class="help-block text-muted small-font">Delitos: </h6>
                            <asp:DropDownList ID="ddlDelitos1" CssClass="form-select" runat="server">
                            <asp:ListItem runat="server" Text="Seleccione un delito:"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <asp:Button ID="modalParte" runat="server" CssClass="btn btn-success" Text="Agregar Parte" OnClick="btnAbrirModal" />
                    </div>
                    <div class="col-md-6 mx-auto" style="max-width: 600px;">
                        <asp:GridView ID="gvPartes" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvPartes_RowCommand" OnRowDataBound="gvPartes_RowDataBound" ClientIDMode="Static">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                    <HeaderStyle CssClass="bg-success text-white"/>
                                </asp:BoundField>
                                <asp:BoundField DataField="Genero" HeaderText="Genero">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Parte" HeaderText="Parte">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarParte" CommandArgument='<%# Container.DisplayIndex %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>


                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <asp:Button ID="btnAgregarDelito" runat="server" CssClass="btn btn-success" Text="Agregar Delito" OnClick="btnAgregarDelito_Click" />
                    </div>
                    <asp:GridView ID="gvDelitos" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvDelitos_RowCommand" ClientIDMode="Static">
                        <Columns>
                            <asp:TemplateField HeaderText="Id Delito" SortExpression="NombreDelito">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdDelito" runat="server" Text='<%# Bind("IdDelito") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nombre del Delito" SortExpression="NombreDelito">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombreDelito" runat="server" Text='<%# Bind("NombreDelito") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                <ItemTemplate>
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarDelito" CommandArgument='<%# Container.DisplayIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>





            <br />
            <br />


            <div class="row">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <h6 class="help-block text-muted small-font">Prioridad: </h6>
                        <asp:RadioButtonList ID="prioridad" runat="server">
                            <asp:ListItem Text=" Alta" Value="A" />
                            <asp:ListItem Text=" Normal" Value="N" />
                        </asp:RadioButtonList>
                    </div>
                </div>

                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Diligencia Solicitada: </h6>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Ingrese Diligencia" ID="Diligencia1"></asp:TextBox>
                </div>

            </div>

            <br />
            <br />

            <div class="row ">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Anexos: </h6>
                    <div class="form-outline">
                        <asp:DropDownList ID="ddlAnexos" CssClass="form-select" runat="server">
                            <asp:ListItem runat="server" Text="Seleccione el anexo a agregar:"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Cantidad de Anexos: </h6>
                    <div class="form-outline">
                        <asp:TextBox runat="server" ID="noAnexos" CssClass="form-control" Text="0" Type="Number"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <div style="display: flex; justify-content: center;">
                            <asp:Button ID="addAnexo" runat="server" CssClass="btn btn-success" Text="Agregar Anexos" OnClick="btnAgregarAnexo_Click" />
                        </div>
                    </div>
                    <asp:GridView ID="gvAnexos" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvAnexos_RowCommand" ClientIDMode="Static">
                        <Columns>
                            <asp:TemplateField HeaderText="Anexo" SortExpression="NombreDelito">
                                <ItemTemplate>
                                    <asp:Label ID="lblAnexo" runat="server" Text='<%# Bind("descripcion") %>' CssClass="mayusculas"></asp:Label>
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
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarAnexo" CommandArgument='<%# Container.DisplayIndex %>' />
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

            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <h6 class="help-block text-muted small-font">Observaciones: </h6>
                    <asp:TextBox ID="observa1" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Observaciones"></asp:TextBox>
                </div>
            </div>

            <br />
            <br />



                 

                </div>

            <br />
            <br />

            <center>
            <asp:Button ID="ObDatos" runat="server" CssClass="btn btn-success" Text="Registrar" OnClick="ObtenerDatosYMostrarModal"  />
            </center>


                <%--                <button type="button" class="btn btn-success" data-toggle="modal" data-target="#conf" onclick="AbrirModalConf()">
                    <svg style="fill: #ffffff" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512">
                        <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                        <path d="M64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H384c35.3 0 64-28.7 64-64V173.3c0-17-6.7-33.3-18.7-45.3L352 50.7C340 38.7 323.7 32 306.7 32H64zm0 96c0-17.7 14.3-32 32-32H288c17.7 0 32 14.3 32 32v64c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V128zM224 288a64 64 0 1 1 0 128 64 64 0 1 1 0-128z" />
                    </svg>
                    Registrar</button>

<%--                <button type="button" class="btn btn-success" onclick="ObtenerDatosYMostrarModal()">
                    <svg style="fill: #ffffff" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512">
                        <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                        <path d="M64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H384c35.3 0 64-28.7 64-64V173.3c0-17-6.7-33.3-18.7-45.3L352 50.7C340 38.7 323.7 32 306.7 32H64zm0 96c0-17.7 14.3-32 32-32H288c17.7 0 32 14.3 32 32v64c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V128zM224 288a64 64 0 1 1 0 128 64 64 0 1 1 0-128z" />
                    </svg>
                    Registrar
                </button>--%>


            </center>

            </div>

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
                             <asp:DropDownList ID="parte" runat="server" CssClass="form-select" onchange="habilitarTextBoxSelecParte()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="V">Victima</asp:ListItem>
                                 <asp:ListItem Value="I">Imputado</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeParte" runat="server" CssClass="form-control" placeholder="Ingrese el tipo de parte" Enabled="false"></asp:TextBox>
                         </div>
                     </div>

                     <br />


                     <div class="row" style="padding: 2%">
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Nombre(s): </h6>
                             <asp:TextBox ID="nom2" runat="server" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                         </div>
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido Paterno: </h6>
                             <asp:TextBox ID="ap2" runat="server" CssClass="form-control" placeholder="A. Paterno"></asp:TextBox>
                         </div>

                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido materno: </h6>
                             <asp:TextBox ID="am2" runat="server" CssClass="form-control" placeholder="A. Materno"></asp:TextBox>
                         </div>
                     </div>

                     <br />

                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Sexo: </h6>
                             <asp:DropDownList ID="sexo" runat="server" CssClass="form-select" onchange="habilitarTextBoxSelecSexo()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="M">Masculino</asp:ListItem>
                                 <asp:ListItem Value="F">Femenino</asp:ListItem>
                                 <asp:ListItem Value="Otro">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeSexo" runat="server" CssClass="form-control" placeholder="Ingrese el sexo:" Enabled="false"></asp:TextBox>
                         </div>
                     </div>
                 </form>
                 <br />

             </div>
             <div class="modal-footer">
                 <button id="btnCerrar" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                 <asp:Button ID="btnGuardar" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregarParte_Click" />
             </div>
         </div>
     </div>
 </div>


 <!-- Modal confirmar datos -->
 <div class="modal fade" id="ModalConfirmacion1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
     <div class="modal-dialog" role="document">
         <div class="modal-content">
             <div class="modal-header" style="background-color: #ffcc00">
                 <h5 class="modal-title" style="color: white; font-weight: bold">¿Los datos son correctos?<br />
                     Revise los datos antes de continuar...</h5>
                 <button type="button" class="close" data-dismiss="modal" style="color: white" aria-label="Cerrar">
                     <span aria-hidden="true">&times;</span>
                 </button>
             </div>
             <div class="modal-body">
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                 <asp:Button ID="btnGuardarCambios" runat="server" Text="Guardar cambios" CssClass="btn btn-success" OnClick="btnGuardarDatosJudiciales_Click"  />
                 
             </div>
         </div>
     </div>
 </div>

        </asp:Panel>



        <asp:Panel ID="Panel2" runat="server" Visible="False">
            <!-- Contenido del formulario para Despacho -->
            <h3 style="text-align: center" id="lblDespacho">Registro de Despacho</h3>
            <br />
            <br />

            <div class="row ">
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Numero de Documento: </h6>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Numero de Despacho" ID="numdesp"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Quejoso: </h6>
                    <asp:TextBox runat="server" ID="quejoso" CssClass="form-control" placeholder="Quejoso"></asp:TextBox>
                </div>

                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Fecha de Recepción: </h6>
                    <asp:TextBox ID="fecha2" runat="server" CssClass="form-control" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Número de Envio: </h6>
                    <div class="form-outline">
                        <asp:TextBox runat="server" ID="fojas2" CssClass="form-control" Text="0" Type="Number"></asp:TextBox>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row ">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Parte a Notificar: </h6>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="row ">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <h6 class="help-block text-muted small-font">Delitos: </h6>
                            <asp:DropDownList ID="ddlDelitos2" CssClass="form-select" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Agregar Parte" OnClick="btnAbrirModal2" />
                    </div>
                    <div class="col-md-6 mx-auto" style="max-width: 600px;">
                        <asp:GridView ID="gvPartes2" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvPartes_RowCommand2" OnRowDataBound="GridView1_RowDataBound2">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Genero" HeaderText="Genero">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Parte" HeaderText="Parte">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarParte" CommandArgument='<%# Container.DisplayIndex %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>



                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-success" Text="Agregar Delito" OnClick="btnAgregarDelito_Click2" />
                    </div>
                    <asp:GridView ID="gvDelitos2" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvDelitos_RowCommand2">
                        <Columns>
                            <asp:TemplateField HeaderText="Id Delito" SortExpression="NombreDelito">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdDelito2" runat="server" Text='<%# Bind("IdDelito2") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Nombre del Delito" SortExpression="NombreDelito">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombreDelito2" runat="server" Text='<%# Bind("NombreDelito2") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                <ItemTemplate>
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarDelito2" CommandArgument='<%# Container.DisplayIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>


            <br />
            <br />


            <div class="row">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="form-group">
                        <h6 class="help-block text-muted small-font">Prioridad: </h6>
                        <asp:RadioButtonList ID="prioridad2" runat="server">
                            <asp:ListItem Text="Alta" Value="A" />
                            <asp:ListItem Text="Normal" Value="N" />
                        </asp:RadioButtonList>
                    </div>
                </div>

                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Diligencia Solicitada: </h6>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Ingrese Diligencia" ID="Diligencia2"></asp:TextBox>
                </div>
            </div>

            <br />
            <br />

                        <div class="row ">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Anexos: </h6>
                    <div class="form-outline">
                        <asp:DropDownList ID="ddlAnexos2" CssClass="form-select" runat="server">
                            <asp:ListItem runat="server" Text="Seleccione el anexo a agregar:"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Cantidad de Anexos: </h6>
                    <div class="form-outline">
                        <asp:TextBox runat="server" ID="noAnexos2" CssClass="form-control" Text="0" Type="Number"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <div style="display: flex; justify-content: center;">
                            <asp:Button ID="addAnexos2" runat="server" CssClass="btn btn-success" Text="Agregar Anexos" OnClick="btnAgregarAnexo_Click2" />
                        </div>
                    </div>
                    <asp:GridView ID="gvAnexos2" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvAnexos_RowCommand2" ClientIDMode="Static">
                        <Columns>
                            <asp:TemplateField HeaderText="Anexo" SortExpression="NombreDelito">
                                <ItemTemplate>
                                    <asp:Label ID="lblAnexo2" runat="server" Text='<%# Bind("descripcion2") %>' CssClass="mayusculas"></asp:Label>
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
                                    <asp:Button ID="btnEliminar2" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarAnexo2" CommandArgument='<%# Container.DisplayIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <h6 class="help-block text-muted small-font">Observaciones: </h6>
                    <asp:TextBox ID="observa2" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Observaciones"></asp:TextBox>
                </div>
            </div>

            <br />
            <br />

            <center>
            <asp:Button ID="Button3" runat="server" CssClass="btn btn-success" Text="Registrar" OnClick="ObtenerDatosYMostrarModal2" OnClientClick="return ValidarCampos();" />
            </center>


                <%--                <button type="button" class="btn btn-success" data-toggle="modal" data-target="#conf" onclick="AbrirModalConf()">
                    <svg style="fill: #ffffff" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512">
                        <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                        <path d="M64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H384c35.3 0 64-28.7 64-64V173.3c0-17-6.7-33.3-18.7-45.3L352 50.7C340 38.7 323.7 32 306.7 32H64zm0 96c0-17.7 14.3-32 32-32H288c17.7 0 32 14.3 32 32v64c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V128zM224 288a64 64 0 1 1 0 128 64 64 0 1 1 0-128z" />
                    </svg>
                    Registrar</button>--%>

<%--                <button type="button" class="btn btn-success" onclick="ObtenerDatosYMostrarModal()">
                    <svg style="fill: #ffffff" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512">
                        <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                        <path d="M64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H384c35.3 0 64-28.7 64-64V173.3c0-17-6.7-33.3-18.7-45.3L352 50.7C340 38.7 323.7 32 306.7 32H64zm0 96c0-17.7 14.3-32 32-32H288c17.7 0 32 14.3 32 32v64c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V128zM224 288a64 64 0 1 1 0 128 64 64 0 1 1 0-128z" />
                    </svg>
                    Registrar
                </button>--%>



            </center>

            </div>

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
                             <asp:DropDownList ID="parte2" runat="server" CssClass="form-select" onchange="habilitarTextBoxSelecParte2()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="V">Victima</asp:ListItem>
                                 <asp:ListItem Value="I">Imputado</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeParte2" runat="server" CssClass="form-control" placeholder="Ingrese el tipo de parte" Enabled="false"></asp:TextBox>
                         </div>
                     </div>

                     <br />


                     <div class="row" style="padding: 2%">
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Nombre(s): </h6>
                             <asp:TextBox ID="nom3" runat="server" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                         </div>
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido Paterno: </h6>
                             <asp:TextBox ID="ap3" runat="server" CssClass="form-control" placeholder="A. Paterno"></asp:TextBox>
                         </div>

                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido materno: </h6>
                             <asp:TextBox ID="am3" runat="server" CssClass="form-control" placeholder="A. Materno"></asp:TextBox>
                         </div>
                     </div>

                     <br />

                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Sexo: </h6>
                             <asp:DropDownList ID="sexo2" runat="server" CssClass="form-select" onchange="habilitarTextBoxSelecSexo2()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="M">Masculino</asp:ListItem>
                                 <asp:ListItem Value="F">Femenino</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeSexo2" runat="server" CssClass="form-control" placeholder="Ingrese el sexo:" Enabled="false"></asp:TextBox>
                         </div>
                     </div>
                 </form>
                 <br />

             </div>
             <div class="modal-footer">
                 <button id="btnCerrar2" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                 <asp:Button ID="Button4" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregarParte_Click2" />
             </div>
         </div>
     </div>
 </div>


 <!-- Modal confirmar datos -->
 <div class="modal fade" id="ModalConfirmacion2" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
     <div class="modal-dialog" role="document">
         <div class="modal-content">
             <div class="modal-header" style="background-color: #ffcc00">
                 <h5 class="modal-title" style="color: white; font-weight: bold">¿Los datos son correctos?<br />
                     Revise los datos antes de continuar...</h5>
                 <button type="button" class="close" data-dismiss="modal" style="color: white" aria-label="Cerrar">
                     <span aria-hidden="true">&times;</span>
                 </button>
             </div>
             <div class="modal-body">
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                 <asp:Button ID="Button9" runat="server" Text="Guardar cambios" CssClass="btn btn-success" OnClick="btnGuardarDatosJudiciales_Click2"  />
             </div>
         </div>
     </div>
 </div>



        </asp:Panel>
        
        
        <asp:Panel ID="Panel3" runat="server" Visible="False">
            <!-- Contenido del formulario para Requesitoria -->
                        <h3 style="text-align: center" id="lblRequisitoria">Registro de Requisitoria</h3>
            <br />
            <br />

            <div class="row ">
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Numero de Toca: </h6>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Numero de Toca" ID="numtoca"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Sala de procedencia: </h6>
                    <asp:TextBox runat="server" ID="salaproc" CssClass="form-control" placeholder="Sala de Procedencia"></asp:TextBox>
                </div>

                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Fecha de Recepción: </h6>
                    <asp:TextBox ID="fecha3" runat="server" CssClass="form-control" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Número de Fojas: </h6>
                    <div class="form-outline">
                        <asp:TextBox runat="server" ID="fojas3" CssClass="form-control" Text="0" Type="Number"></asp:TextBox>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row ">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Parte a Notificar: </h6>
                </div>
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="row ">
                        <div class="col-md-12 col-sm-12 col-xs-12">
                            <h6 class="help-block text-muted small-font">Delitos: </h6>
                            <asp:DropDownList ID="ddlDelitos3" CssClass="form-select" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <asp:Button ID="Button5" runat="server" CssClass="btn btn-success" Text="Agregar Parte" OnClick="btnAbrirModal3" />
                    </div>
                    <div class="col-md-6 mx-auto" style="max-width: 600px;">
                        <asp:GridView ID="gvPartes3" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvPartes_RowCommand3" OnRowDataBound="GridView1_RowDataBound3">
                            <Columns>
                                <asp:BoundField DataField="Nombre" HeaderText="Nombre">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Genero" HeaderText="Genero">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:BoundField>
                                <asp:BoundField DataField="Parte" HeaderText="Parte">
                                    <HeaderStyle CssClass="bg-success text-white" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarParte" CommandArgument='<%# Container.DisplayIndex %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>



                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <asp:Button ID="Button6" runat="server" CssClass="btn btn-success" Text="Agregar Delito" OnClick="btnAgregarDelito_Click3" />
                    </div>
                    <asp:GridView ID="gvDelitos3" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvDelitos_RowCommand3">
                        <Columns>
                            <asp:TemplateField HeaderText="Id Delito" SortExpression="NombreDelito">
                                <ItemTemplate>
                                    <asp:Label ID="lblIdDelito3" runat="server" Text='<%# Bind("IdDelito3") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Nombre del Delito" SortExpression="NombreDelito">
                                <ItemTemplate>
                                    <asp:Label ID="lblNombreDelito3" runat="server" Text='<%# Bind("NombreDelito3") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Acciones" HeaderStyle-CssClass="bg-success text-white">
                                <ItemTemplate>
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarDelito3" CommandArgument='<%# Container.DisplayIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>


            <br />
            <br />


            <div class="row">
                <div class="col-md-6 col-sm-6 col-xs-6">

                    <div class="form-group">
                        <h6 class="help-block text-muted small-font">Prioridad: </h6>
                        <asp:RadioButtonList ID="prioridad3" runat="server">
                            <asp:ListItem Text="Alta" Value="A" />
                            <asp:ListItem Text="Normal" Value="N" />
                        </asp:RadioButtonList>
                    </div>
                </div>

                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Diligencia Solicitada: </h6>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Ingrese Diligencia" ID="Diligencia3"></asp:TextBox>
                </div>

            </div>

            <br />
            <br />

            <div class="row ">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Anexos: </h6>
                    <div class="form-outline">
                        <asp:DropDownList ID="ddlAnexos3" CssClass="form-select" runat="server">
                            <asp:ListItem runat="server" Text="Seleccione el anexo a agregar:"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-6 col-sm-6 col-xs-6">
                    <h6 class="help-block text-muted small-font">Cantidad de Anexos: </h6>
                    <div class="form-outline">
                        <asp:TextBox runat="server" ID="noAnexos3" CssClass="form-control" Text="0" Type="Number"></asp:TextBox>
                    </div>
                </div>

                <div class="col-md-12 col-sm-12 col-xs-12">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <div style="display: flex; justify-content: center;">
                            <asp:Button ID="addAnexos3" runat="server" CssClass="btn btn-success" Text="Agregar Anexos" OnClick="btnAgregarAnexo_Click3" />
                        </div>
                    </div>
                    <asp:GridView ID="gvAnexos3" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvAnexos_RowCommand3" ClientIDMode="Static">
                        <Columns>
                            <asp:TemplateField HeaderText="Anexo" SortExpression="NombreDelito">
                                <ItemTemplate>
                                    <asp:Label ID="lblAnexo3" runat="server" Text='<%# Bind("descripcion3") %>' CssClass="mayusculas"></asp:Label>
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
                                    <asp:Button ID="btnEliminar" runat="server" Text="Eliminar" CssClass="btn btn-danger" CommandName="EliminarAnexo3" CommandArgument='<%# Container.DisplayIndex %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>
            </div>

            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <h6 class="help-block text-muted small-font">Observaciones: </h6>
                    <asp:TextBox ID="observa3" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Observaciones"></asp:TextBox>
                </div>
            </div>

            <br />
            <br />

            <center>
            <asp:Button ID="Button7" runat="server" CssClass="btn btn-success" Text="Registrar" OnClick="ObtenerDatosYMostrarModal3" OnClientClick="return ValidarCampos();" />
            </center>


                <%--                <button type="button" class="btn btn-success" data-toggle="modal" data-target="#conf" onclick="AbrirModalConf()">
                    <svg style="fill: #ffffff" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512">
                        <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                        <path d="M64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H384c35.3 0 64-28.7 64-64V173.3c0-17-6.7-33.3-18.7-45.3L352 50.7C340 38.7 323.7 32 306.7 32H64zm0 96c0-17.7 14.3-32 32-32H288c17.7 0 32 14.3 32 32v64c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V128zM224 288a64 64 0 1 1 0 128 64 64 0 1 1 0-128z" />
                    </svg>
                    Registrar</button>--%>

<%--                <button type="button" class="btn btn-success" onclick="ObtenerDatosYMostrarModal()">
                    <svg style="fill: #ffffff" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512">
                        <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                        <path d="M64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H384c35.3 0 64-28.7 64-64V173.3c0-17-6.7-33.3-18.7-45.3L352 50.7C340 38.7 323.7 32 306.7 32H64zm0 96c0-17.7 14.3-32 32-32H288c17.7 0 32 14.3 32 32v64c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V128zM224 288a64 64 0 1 1 0 128 64 64 0 1 1 0-128z" />
                    </svg>
                    Registrar
                </button>--%>



            </center>

            </div>

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
                             <asp:DropDownList ID="parte3" runat="server" CssClass="form-select" onchange="habilitarTextBoxSelecParte3()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="V">Victima</asp:ListItem>
                                 <asp:ListItem Value="I">Imputado</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeParte3" runat="server" CssClass="form-control" placeholder="Ingrese el tipo de parte" Enabled="false"></asp:TextBox>
                         </div>
                     </div>

                     <br />


                     <div class="row" style="padding: 2%">
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Nombre(s): </h6>
                             <asp:TextBox ID="nom4" runat="server" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                         </div>
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido Paterno: </h6>
                             <asp:TextBox ID="ap4" runat="server" CssClass="form-control" placeholder="A. Paterno"></asp:TextBox>
                         </div>

                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido materno: </h6>
                             <asp:TextBox ID="am4" runat="server" CssClass="form-control" placeholder="A. Materno"></asp:TextBox>
                         </div>
                     </div>

                     <br />

                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Sexo: </h6>
                             <asp:DropDownList ID="sexo3" runat="server" CssClass="form-select" onchange="habilitarTextBoxSelecSexo3()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="M">Masculino</asp:ListItem>
                                 <asp:ListItem Value="F">Femenino</asp:ListItem>
                                 <asp:ListItem Value="O">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="espeSexo3" runat="server" CssClass="form-control" placeholder="Ingrese el sexo:" Enabled="false"></asp:TextBox>
                         </div>
                     </div>
                 </form>
                 <br />

             </div>
             <div class="modal-footer">
                 <button id="btnCerrar3" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                 <asp:Button ID="Button8" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregarParte_Click3" />
             </div>
         </div>
     </div>
 </div>


 <!-- Modal confirmar datos -->
 <div class="modal fade" id="ModalConfirmacion3" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
     <div class="modal-dialog" role="document">
         <div class="modal-content">
             <div class="modal-header" style="background-color: #ffcc00">
                 <h5 class="modal-title" style="color: white; font-weight: bold">¿Los datos son correctos?<br />
                     Revise los datos antes de continuar...</h5>
                 <button type="button" class="close" data-dismiss="modal" style="color: white" aria-label="Cerrar">
                     <span aria-hidden="true">&times;</span>
                 </button>
             </div>
             <div class="modal-body">
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                 <asp:Button ID="Button10" runat="server" Text="Guardar cambios" CssClass="btn btn-success" OnClick="btnGuardarDatosJudiciales_Click3" />
             </div>
         </div>
     </div>
 </div>

                                    <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <!-- Etiqueta pre -->
                    <pre id="TicketDiv3" runat="server"></pre>
                </div>
            </div>

            </asp:Panel>


                    <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <!-- Etiqueta pre -->
                    <pre id="TicketDiv" runat="server"></pre>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

            <div class="row">
        <div class="col-md-12 col-sm-12 col-xs-12">
            <!-- Botón -->
            <asp:Button ID="btnImprimir" runat="server" Text="Imprimir Ticket" OnClick="btnImprimir_Click" CssClass="btn btn-success" />
        </div>
    </div>









<script>  

    //Funciones Exhorto
    function ObtenerDatosYMostrarModal() {
        // Abrir el modal
        $('#ModalConfirmacion1').modal('show');

        // Obtener los nombres de las partes
        var gvPartes = $('#<%= gvPartes.ClientID %> tr:gt(0)');
        var listaPartes = $('#listaPartes');
        listaPartes.empty(); // Limpiar la lista antes de agregar nuevos elementos

        var parteSeleccionada = ''; // Variable para almacenar partes seleccionadas

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

        gvDelitos.each(function () {
            var nombreDelito = $(this).find('td:eq(1)').text().trim(); // Asumiendo que el nombre del delito está en la segunda celda
            var li = $('<li>').text(nombreDelito);
            listaDelitos.append(li);

            // Lógica para seleccionar delitos (ajustar según sea necesario)
            delitoSeleccionado += nombreDelito + ', ';
        });

        // Verificar si hay al menos una parte o delito seleccionado
        if (parteSeleccionada === '' && delitoSeleccionado === '') {
            alert('Seleccione al menos una parte o un delito.');
            // Cerrar el modal si no hay partes o delitos seleccionados
            $('#ModalConfirmacion1').modal('hide');
            return;
        }


        // Obtener los valores de los TextBox
        var numdoc = document.getElementById('<%= numdoc1.ClientID %>').value;
        var procede = document.getElementById('<%= procede1.ClientID %>').value;
        var fecha = document.getElementById('<%= fecha1.ClientID %>').value;
        var fojas = document.getElementById('<%= fojas1.ClientID %>').value;
        var observa = document.getElementById('<%= observa1.ClientID %>').value;
        // Agregar más variables según sea necesario

        // Mostrar los valores en el modal
        document.getElementById('ModalConfirmacion1').querySelector('.modal-body').innerHTML =
            '<b>Número de Documento: </b>' + numdoc + '<br />' + '<br />' +
            '<b>Procedente de: </b>' + procede + '<br />' + '<br />' +
            '<b>Fecha de Recepción: </b>' + fecha + '<br />' + '<br />' +
            '<b>Número de Fojas: </b>' + fojas + '<br />' + '<br />' +
            '<b>Parte(s) a Notificar: </b>' + parteSeleccionada + '<br />' + '<br />' +
            '<b>Delito(s): </b>' + delitoSeleccionado + '<br />' + '<br />' +
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
        if (sexoDropDown.value === "Otro") {
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
        // Abrir el modal
        $('#ModalConfirmacion2').modal('show');

        // Obtener los nombres de las partes
        var gvPartes = $('#<%= gvPartes2.ClientID %> tr:gt(0)');
        var listaPartes = $('#listaPartes');
        listaPartes.empty(); // Limpiar la lista antes de agregar nuevos elementos

        var parteSeleccionada = ''; // Variable para almacenar partes seleccionadas

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

        gvDelitos.each(function () {
            var nombreDelito = $(this).find('td:eq(1)').text().trim(); // Asumiendo que el nombre del delito está en la segunda celda
            var li = $('<li>').text(nombreDelito);
            listaDelitos.append(li);

            // Lógica para seleccionar delitos (ajustar según sea necesario)
            delitoSeleccionado += nombreDelito + ', ';
        });

        // Verificar si hay al menos una parte o delito seleccionado
        if (parteSeleccionada === '' && delitoSeleccionado === '') {
            alert('Seleccione al menos una parte o un delito.');
            // Cerrar el modal si no hay partes o delitos seleccionados
            $('#ModalConfirmacion1').modal('hide');
            return;
        }

        // Obtener los valores de los TextBox
        var numdesp = document.getElementById('<%= numdesp.ClientID %>').value;
        var quejo = document.getElementById('<%= quejoso.ClientID %>').value;
        var fecha = document.getElementById('<%= fecha2.ClientID %>').value;
        var fojas = document.getElementById('<%= fojas2.ClientID %>').value;
        var observa = document.getElementById('<%= observa2.ClientID %>').value;
        // Agregar más variables según sea necesario

        // Mostrar los valores en el modal
        document.getElementById('ModalConfirmacion2').querySelector('.modal-body').innerHTML =
            '<b>Número de Despacho: </b>' + numdesp + '<br />' + '<br />' +
            '<b>Quejoso: </b>' + quejo + '<br />' + '<br />' +
            '<b>Fecha de Recepción: </b>' + fecha + '<br />' + '<br />' +
            '<b>Fojas: </b>' + fojas + '<br />' + '<br />' +
            '<b>Parte(s) a Notificar: </b>' + parteSeleccionada + '<br />' + '<br />' +
            '<b>Delito(s): </b>' + delitoSeleccionado + '<br />' + '<br />' +
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
        if (sexoDropDown.value === "Otro") {
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
        // Abrir el modal
        $('#ModalConfirmacion3').modal('show');

        // Obtener los nombres de las partes
        var gvPartes = $('#<%= gvPartes3.ClientID %> tr:gt(0)');
        var listaPartes = $('#listaPartes');
        listaPartes.empty(); // Limpiar la lista antes de agregar nuevos elementos

        var parteSeleccionada = ''; // Variable para almacenar partes seleccionadas

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

        gvDelitos.each(function () {
            var nombreDelito = $(this).find('td:eq(1)').text().trim(); // Asumiendo que el nombre del delito está en la segunda celda
            var li = $('<li>').text(nombreDelito);
            listaDelitos.append(li);

            // Lógica para seleccionar delitos (ajustar según sea necesario)
            delitoSeleccionado += nombreDelito + ', ';
        });

        // Verificar si hay al menos una parte o delito seleccionado
        if (parteSeleccionada === '' && delitoSeleccionado === '') {
            alert('Seleccione al menos una parte o un delito.');
            // Cerrar el modal si no hay partes o delitos seleccionados
            $('#ModalConfirmacion1').modal('hide');
            return;
        }


        // Obtener los valores de los TextBox
        var numtoca = document.getElementById('<%= numtoca.ClientID %>').value;
        var sala = document.getElementById('<%= salaproc.ClientID %>').value;
        var fecha = document.getElementById('<%= fecha3.ClientID %>').value;
        var fojas = document.getElementById('<%= fojas3.ClientID %>').value;
        var observa = document.getElementById('<%= observa3.ClientID %>').value;
        // Agregar más variables según sea necesario

        // Mostrar los valores en el modal
        document.getElementById('ModalConfirmacion3').querySelector('.modal-body').innerHTML =
            '<b>Número de Toca: </b>' + numtoca + '<br />' + '<br />' +
            '<b>Sala de Procedencia: </b>' + sala + '<br />' + '<br />' +
            '<b>Fecha de Recepción: </b>' + fecha + '<br />' + '<br />' +
            '<b>Numero de documento de envio: </b>' + fojas + '<br />' + '<br />' +
            '<b>Parte(s) a Notificar: </b>' + parteSeleccionada + '<br />' + '<br />' +
            '<b>Delito(s): </b>' + delitoSeleccionado + '<br />' + '<br />' +
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
    if (sexoDropDown.value === "Otro") {
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

    function EjemploErrorTblAnexo(errorMessage) {
        toastr.error('El anexo ya existe en la tabla, seleccione uno diferente')
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