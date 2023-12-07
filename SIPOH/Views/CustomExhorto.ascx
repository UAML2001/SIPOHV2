<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomExhorto.ascx.cs" Inherits="SIPOH.Views.CustomExhorto" %>



<div class="container col-12">
    <div style="text-align: center; padding: 3%">
        <div class="col-md-10 ml-auto col-xl-11 mr-auto">
            <h3 style="text-align: center" id="titulo">Elija la opcion que desee
            </h3>
        </div>
    </div>
</div>



<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="updPanel" runat="server" EnableViewState="true" >
    <ContentTemplate>

        <div class="row ">
            <div class="col-md-3 col-sm-3 col-xs-3">
                <h6 class="help-block text-muted small-font">Accion a elegir: </h6>
                <asp:DropDownList ID="DropDownList1" CssClass="form-select" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>Selecciona una opción</asp:ListItem>
                    <asp:ListItem>Exhorto</asp:ListItem>
                    <asp:ListItem>Despacho</asp:ListItem>
                    <asp:ListItem>Requesitoria</asp:ListItem>
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
                            <asp:DropDownList ID="ddlDelitos" CssClass="form-select" runat="server"></asp:DropDownList>
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
                        <asp:GridView ID="gvPartes" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvPartes_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-Width="80%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                        <asp:Button ID="btnAgregarDelito" runat="server" CssClass="btn btn-success" Text="Agregar Delito" OnClick="btnAgregarDelito_Click" />
                    </div>
                    <asp:GridView ID="gvDelitos" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvDelitos_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="bg-success text-white">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NombreDelito" HeaderText="Nombre del Delito">
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>
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
                        <asp:RadioButtonList ID="radioOptions" runat="server">
                            <asp:ListItem Text="Alta" Value="Alta" />
                            <asp:ListItem Text="Normal" Value="Normal" />
                        </asp:RadioButtonList>
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

            <center>
            <asp:Button ID="ObDatos" runat="server" CssClass="btn btn-success" Text="Registrar" OnClick="ObtenerDatosYMostrarModal" OnClientClick="return ValidarCampos();" />
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
                                 <asp:ListItem Value="Victima">Victima</asp:ListItem>
                                 <asp:ListItem Value="Imputado">Imputado</asp:ListItem>
                                 <asp:ListItem Value="Otro">Otro</asp:ListItem>
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
                                 <asp:ListItem Value="Masculino">Masculino</asp:ListItem>
                                 <asp:ListItem Value="Femenino">Femenino</asp:ListItem>
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
                 Documento a enviar:
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                 <button type="button" class="btn btn-success">Guardar cambios</button>
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
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Numero de Documento" ID="TextBox1"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Procedente de: </h6>
                    <asp:TextBox runat="server" ID="TextBox2" CssClass="form-control" placeholder="Procedencia"></asp:TextBox>
                </div>

                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Fecha de Recepción: </h6>
                    <asp:TextBox ID="TextBox3" runat="server" CssClass="form-control" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Número de Fojas: </h6>
                    <div class="form-outline">
                        <asp:TextBox runat="server" ID="TextBox4" CssClass="form-control" Text="0" Type="Number"></asp:TextBox>
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
                            <asp:DropDownList ID="DropDownList2" CssClass="form-select" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-success" Text="Agregar Parte" OnClick="btnAbrirModal" />
                    </div>
                    <div class="col-md-6 mx-auto" style="max-width: 600px;">
                        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvPartes_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-Width="80%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-success" Text="Agregar Delito" OnClick="btnAgregarDelito_Click" />
                    </div>
                    <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvDelitos_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="bg-success text-white">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NombreDelito" HeaderText="Nombre del Delito">
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>
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
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server">
                            <asp:ListItem Text="Alta" Value="Alta" />
                            <asp:ListItem Text="Normal" Value="Normal" />
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <h6 class="help-block text-muted small-font">Observaciones: </h6>
                    <asp:TextBox ID="TextBox5" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Observaciones"></asp:TextBox>
                </div>
            </div>

            <br />
            <br />

            <center>
            <asp:Button ID="Button3" runat="server" CssClass="btn btn-success" Text="Registrar" OnClick="ObtenerDatosYMostrarModal" OnClientClick="return ValidarCampos();" />
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
                 <form id="myForm">
                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Parte: </h6>
                             <asp:DropDownList ID="DropDownList3" runat="server" CssClass="form-select" onchange="habilitarTextBoxSelecParte()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="Victima">Victima</asp:ListItem>
                                 <asp:ListItem Value="Imputado">Imputado</asp:ListItem>
                                 <asp:ListItem Value="Otro">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="TextBox6" runat="server" CssClass="form-control" placeholder="Ingrese el tipo de parte" Enabled="false"></asp:TextBox>
                         </div>
                     </div>

                     <br />


                     <div class="row" style="padding: 2%">
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Nombre(s): </h6>
                             <asp:TextBox ID="TextBox7" runat="server" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                         </div>
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido Paterno: </h6>
                             <asp:TextBox ID="TextBox8" runat="server" CssClass="form-control" placeholder="A. Paterno"></asp:TextBox>
                         </div>

                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido materno: </h6>
                             <asp:TextBox ID="TextBox9" runat="server" CssClass="form-control" placeholder="A. Materno"></asp:TextBox>
                         </div>
                     </div>

                     <br />

                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Sexo: </h6>
                             <asp:DropDownList ID="DropDownList4" runat="server" CssClass="form-select" onchange="habilitarTextBoxSelecSexo()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="Masculino">Masculino</asp:ListItem>
                                 <asp:ListItem Value="Femenino">Femenino</asp:ListItem>
                                 <asp:ListItem Value="Otro">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="TextBox10" runat="server" CssClass="form-control" placeholder="Ingrese el sexo:" Enabled="false"></asp:TextBox>
                         </div>
                     </div>
                 </form>
                 <br />

             </div>
             <div class="modal-footer">
                 <button id="btnCerrar2" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                 <asp:Button ID="Button4" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregarParte_Click" />
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
                 Documento a enviar:
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                 <button type="button" class="btn btn-success">Guardar cambios</button>
             </div>
         </div>
     </div>
 </div>


        </asp:Panel>
        <asp:Panel ID="Panel3" runat="server" Visible="False">
            <!-- Contenido del formulario para Requesitoria -->
                        <h3 style="text-align: center" id="lblDespacho">Registro de Requisitoria</h3>
            <br />
            <br />

            <div class="row ">
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Numero de Documento: </h6>
                    <asp:TextBox runat="server" CssClass="form-control" placeholder="Numero de Documento" ID="TextBox11"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Procedente de: </h6>
                    <asp:TextBox runat="server" ID="TextBox12" CssClass="form-control" placeholder="Procedencia"></asp:TextBox>
                </div>

                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Fecha de Recepción: </h6>
                    <asp:TextBox ID="TextBox13" runat="server" CssClass="form-control" placeholder="Fecha" TextMode="Date"></asp:TextBox>
                </div>
                <div class="col-md-3 col-sm-3 col-xs-3">
                    <h6 class="help-block text-muted small-font">Número de Fojas: </h6>
                    <div class="form-outline">
                        <asp:TextBox runat="server" ID="TextBox14" CssClass="form-control" Text="0" Type="Number"></asp:TextBox>
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
                            <asp:DropDownList ID="DropDownList5" CssClass="form-select" runat="server"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row">
                <div class="col-md-6 col-sm-6 col-xs-6">
                    <div class="nav-item d-flex justify-content-end p-3">
                        <asp:Button ID="Button5" runat="server" CssClass="btn btn-success" Text="Agregar Parte" OnClick="btnAbrirModal" />
                    </div>
                    <div class="col-md-6 mx-auto" style="max-width: 600px;">
                        <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvPartes_RowCommand">
                            <Columns>
                                <asp:TemplateField HeaderText="" HeaderStyle-CssClass="bg-success text-white" HeaderStyle-Width="80%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
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
                        <asp:Button ID="Button6" runat="server" CssClass="btn btn-success" Text="Agregar Delito" OnClick="btnAgregarDelito_Click" />
                    </div>
                    <asp:GridView ID="GridView4" runat="server" AutoGenerateColumns="False" CssClass="table" OnRowCommand="gvDelitos_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderStyle-CssClass="bg-success text-white">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chkSeleccionar" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="NombreDelito" HeaderText="Nombre del Delito">
                                <HeaderStyle CssClass="bg-success text-white" />
                            </asp:BoundField>
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
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server">
                            <asp:ListItem Text="Alta" Value="Alta" />
                            <asp:ListItem Text="Normal" Value="Normal" />
                        </asp:RadioButtonList>
                    </div>
                </div>
            </div>

            <br />
            <br />

            <div class="row">
                <div class="col-md-12 col-sm-12 col-xs-12">
                    <h6 class="help-block text-muted small-font">Observaciones: </h6>
                    <asp:TextBox ID="TextBox15" runat="server" CssClass="form-control" TextMode="MultiLine" Rows="4" placeholder="Observaciones"></asp:TextBox>
                </div>
            </div>

            <br />
            <br />

            <center>
            <asp:Button ID="Button7" runat="server" CssClass="btn btn-success" Text="Registrar" OnClick="ObtenerDatosYMostrarModal" OnClientClick="return ValidarCampos();" />
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
                 <form id="myForm">
                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Parte: </h6>
                             <asp:DropDownList ID="DropDownList6" runat="server" CssClass="form-select" onchange="habilitarTextBoxSelecParte()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="Victima">Victima</asp:ListItem>
                                 <asp:ListItem Value="Imputado">Imputado</asp:ListItem>
                                 <asp:ListItem Value="Otro">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="TextBox16" runat="server" CssClass="form-control" placeholder="Ingrese el tipo de parte" Enabled="false"></asp:TextBox>
                         </div>
                     </div>

                     <br />


                     <div class="row" style="padding: 2%">
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Nombre(s): </h6>
                             <asp:TextBox ID="TextBox17" runat="server" CssClass="form-control" placeholder="Nombre"></asp:TextBox>
                         </div>
                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido Paterno: </h6>
                             <asp:TextBox ID="TextBox18" runat="server" CssClass="form-control" placeholder="A. Paterno"></asp:TextBox>
                         </div>

                         <div class="col-md-4 col-sm-4 col-xs-4">
                             <h6 class="help-block text-muted small-font">Apellido materno: </h6>
                             <asp:TextBox ID="TextBox19" runat="server" CssClass="form-control" placeholder="A. Materno"></asp:TextBox>
                         </div>
                     </div>

                     <br />

                     <div class="row" style="padding: 2%">
                         <div class="col-md-6 <col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Sexo: </h6>
                             <asp:DropDownList ID="DropDownList7" runat="server" CssClass="form-select" onchange="habilitarTextBoxSelecSexo()">
                                 <asp:ListItem Value="Seleccionar" Selected="True">Seleccione...</asp:ListItem>
                                 <asp:ListItem Value="Masculino">Masculino</asp:ListItem>
                                 <asp:ListItem Value="Femenino">Femenino</asp:ListItem>
                                 <asp:ListItem Value="Otro">Otro</asp:ListItem>
                             </asp:DropDownList>
                         </div>
                         <div class="col-md-6 col-sm-6 col-xs-6">
                             <h6 class="help-block text-muted small-font">Especifique: </h6>
                             <asp:TextBox ID="TextBox20" runat="server" CssClass="form-control" placeholder="Ingrese el sexo:" Enabled="false"></asp:TextBox>
                         </div>
                     </div>
                 </form>
                 <br />

             </div>
             <div class="modal-footer">
                 <button id="btnCerrar2" class="btn btn-danger" data-bs-dismiss="modal">Cerrar</button>
                 <asp:Button ID="Button8" runat="server" CssClass="btn btn-success" Text="Agregar" OnClick="btnAgregarParte_Click" />
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
                 Documento a enviar:
             </div>
             <div class="modal-footer">
                 <button type="button" class="btn btn-danger" data-dismiss="modal">Cerrar</button>
                 <button type="button" class="btn btn-success">Guardar cambios</button>
             </div>
         </div>
     </div>
 </div>
        </asp:Panel>
    </ContentTemplate>
</asp:UpdatePanel>


<script>  
    function ObtenerDatosYMostrarModal() {
        // Abrir el modal
        $('#ModalConfirmacion1').modal('show');

        var gvPartes = document.getElementById('<%= gvPartes.ClientID %>');
        var checkboxesPartes = gvPartes.getElementsByTagName('input');
        var parteSeleccionada = '';

        for (var i = 0; i < checkboxesPartes.length; i++) {
            if (checkboxesPartes[i].type === 'checkbox' && checkboxesPartes[i].checked) {
                // Obtiene el nombre de la celda "Nombre" en la fila seleccionada
                // Incrementa el índice en 1 para comenzar en la fila 1
                var index = checkboxesPartes[i].parentNode.parentNode.rowIndex;
                parteSeleccionada += gvPartes.rows[index].cells[1].innerText + '; ';
            }
        }

        // Tomar los datos de la tabla de delitos
        var gvDelitos = document.getElementById('<%= gvDelitos.ClientID %>');
        var checkboxesDelitos = gvDelitos.getElementsByTagName('input');
        var delitoSeleccionado = '';

        for (var j = 0; j < checkboxesDelitos.length; j++) {
            if (checkboxesDelitos[j].type === 'checkbox' && checkboxesDelitos[j].checked) {
                // Obtiene el nombre de la celda "Nombre del Delito" en la fila seleccionada
                // Incrementa el índice en 1 para comenzar en la fila 1
                var indexDelito = checkboxesDelitos[j].parentNode.parentNode.rowIndex;
                delitoSeleccionado += gvDelitos.rows[indexDelito].cells[1].innerText + '; ';
            }
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
        if (parteDropDown.value === "Otro") {
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

    function ValidarCampos() {
        // Verificar campos obligatorios
        if (!ValidarCampo('<%= numdoc1.ClientID %>', 'Numero de Documento') ||
            !ValidarCampo('<%= procede1.ClientID %>', 'Procedencia') ||
            !ValidarCampo('<%= fecha1.ClientID %>', 'Fecha de Recepción') ||
            !ValidarCampo('<%= fojas1.ClientID %>', 'Número de Fojas') ||
            !ValidarCampo('<%= ddlDelitos.ClientID %>', 'Delitos') ||
            !ValidarCampo('<%= radioOptions.ClientID %>', 'Prioridad') ||
            !ValidarCampo('<%= observa1.ClientID %>', 'Observaciones')) {
            return false;
        }

        // Agregar más campos si es necesario

        return true;
    }

    function ValidarCampo(clientId, nombreCampo) {
        var campo = document.getElementById(clientId);
        if (campo && campo.value.trim() === '') {
            toastr.error('El campo "' + nombreCampo + '" es obligatorio.');
            return false;
        }
        return true;
    }

</script>