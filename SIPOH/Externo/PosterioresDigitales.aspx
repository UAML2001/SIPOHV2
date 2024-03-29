﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Externo/MasterExterno.Master" AutoEventWireup="true" CodeBehind="PosterioresDigitales.aspx.cs" Inherits="SIPOH.Externo.PosterioresDigitales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <script>
        function toastConfirmar() {
            toastr.info("<div class='container text-center p-4'><label class='h4'>¿Estas seguro de eliminar la solicitud?</label><button type='button' id='confirmationButtonYes' class='btn btn-block btn-success m-3'>Si Eliminar</button> <button type='button' id='confirmationButtonNO' class='btn btn-block btn-danger m-3'>NO</button>", '',
                {
                    preventDuplicates: true,
                    positionClass: 'toast-center-center',
                    timeOut: 50000,
                    tapToDismiss: false,
                    extendedTimeOut: 100000,
                    closeButton: false,
                    allowHtml: true,
                    onShown: function (toast) {
                        $("#confirmationButtonYes").click(function () {
                            console.log('clicked yes');
                            $("[id*='btnEliminar']").trigger("click");

                        });

                        $("#confirmationButtonNO").click(function () {
                            console.log('clicked no');
                            $(this).closest('.toast').remove();
                        });

                    }
                }).css({ "width": "30em", "max-width": "30em", "height":"20em" });

            }

            function check(e) {
                tecla = (document.all) ? e.keyCode : e.which;

                //Tecla de retroceso para borrar, siempre la permite
                if (tecla == 8) {
                    return true;
                }

                // Patrón de entrada, en este caso solo acepta numeros y letras
                patron = /[A-Za-z0-9:,]/;
                tecla_final = String.fromCharCode(tecla);
                return patron.test(tecla_final);
            }

        </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">

    <div class="card mt-5 mb-1 ml-2 mr-2 p-4" >
        <h5 class="card-header">Registro de nueva promoción</h5>
        <div class="card-body" runat="server" id="divBusqueda">
            <h5 class="card-title">Busqueda del asunto</h5>

            <div class="row mb-4">
                <div class="col-md-3">
                    <div class="form-outline">
                        <asp:DropDownList runat="server" ID="ddlCircuito" CssClass="form-select" OnSelectedIndexChanged="ddlCircuito_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                        <label class="form-label" for="ddlCircuito">Circuito</label>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-outline">
                        <asp:DropDownList runat="server" ID="ddlJuzgado" CssClass="form-select">
                        </asp:DropDownList>
                        <label class="form-label" for="ddlJuzgado">Juzgado</label>
                    </div>
                </div>


                <div class="col-md-3">
                    <div class="form-outline">
                        <asp:DropDownList runat="server" ID="ddlTipoAsunto" CssClass="form-select">
                        </asp:DropDownList>
                        <label class="form-label" for="ddlTipoAsunto">Tipo Asunto</label>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-outline mb-4">
                        <asp:TextBox runat="server" type="text" ID="txtasunto" class="form-control"  onblur="padLeadingZeros(this)"/>
                        <label class="form-label" for="txtasunto">Número de Asunto</label>
                    </div>
                </div>


                <div class="offset-4 col-md-4">
                    <asp:Button ID="btnBuscar" runat="server" OnClick="btnBuscar_Click" Text="Buscar" CssClass="btn btn-success btn-block mb-4"  />

                </div>


                <div class="col-12 col-lg-12 mt-4 table-responsive">
                    <asp:GridView ID="gridResultado" runat="server" DataKeyNames="IdAsunto, Delitos, NUC" OnRowCommand="gridResultado_RowCommand"
                        AutoGenerateColumns="false" CssClass="table table-sm table-striped table-hover table-bordered "
                        Width="100%">
                        <Columns>
                            <asp:BoundField DataField="Numero" HeaderText="Numero de asunto">
                                <HeaderStyle CssClass="bg-success text-white" />
                                <ItemStyle CssClass="p-2" />
                            </asp:BoundField>
                            <asp:BoundField DataField="NUC" HeaderText="NUC">
                                <HeaderStyle CssClass="bg-success text-white" />
                                <ItemStyle CssClass="p-2" />
                            </asp:BoundField>

                            <asp:BoundField DataField="Delitos" HeaderText="Delito">
                                <HeaderStyle CssClass="bg-success text-white" />
                                <ItemStyle CssClass="p-2" />
                            </asp:BoundField>

                            <asp:ButtonField CommandName="Seleccionar" Text="<i class='fa fa-check'></i>"
                                ButtonType="Link" ControlStyle-CssClass="btn btn-primary" HeaderText="Seleccionar">
                                <ItemStyle CssClass="p-2 text-center" />
                                <HeaderStyle CssClass="bg-success text-white" />

                            </asp:ButtonField>

                        </Columns>
                    </asp:GridView>
                </div>



            </div>
        </div>

        <div class="bg-white mt-1 mb-4 ml-2 mr-2 p-4" runat="server" id="divFormulario">
            <hr class="hr mb-4 bg-black" />

                    <asp:HiddenField ID="HFIdAunto" runat="server" />


            <div class="row mb-4">
                <div class="col-md-6">
                    <div class="form-outline mb-4">
                        <asp:TextBox runat="server" type="text" ID="txtJuzgado" class="form-control" ReadOnly="true" />
                        <label class="form-label" for="txtNUC">Juzgado</label>
                    </div>
                </div>

                    <div class="col-md-6">
                        <div class="form-outline mb-4">
                            <asp:TextBox runat="server" type="text" ID="txtNumero" class="form-control" ReadOnly="true" />
                            <label class="form-label" for="txtNumero">Número de Asunto</label>
                        </div>
                    </div>

            </div>

            <div class="row mb-4">
                <div class="col-md-6">
                    <div class="form-outline mb-4">
                        <asp:TextBox runat="server" type="text" ID="txtNUC" class="form-control"  ReadOnly="true"/>
                        <label class="form-label" for="txtNUC">NUC</label>
                    </div>
                </div>

                    <div class="col-md-6">
                        <div class="form-outline mb-4">
                            <asp:TextBox runat="server" type="text" ID="txtDelito" class="form-control" ReadOnly="true" />
                            <label class="form-label" for="txtDelito">Delito</label>
                        </div>
                    </div>

            </div>


            <div class="row mb-4">
                <div class="col-md-6">
                    <div class="form-outline">
                        <asp:DropDownList runat="server" ID="ddlTipoSolicitud" CssClass="form-select">
                        </asp:DropDownList>
                        <label class="form-label" for="ddlCircuito">Tipo de Solicitud</label>
                    </div>
                </div>



                <div class="col-md-6">
                    <div class="form-outline">
                        <asp:DropDownList runat="server" ID="ddlFiguraSolicitante" CssClass="form-select">
                        </asp:DropDownList>
                        <label class="form-label" for="ddlFiguraSolicitante">Figura Solicitante</label>
                    </div>
                </div>
            </div>


            <div class="form-outline mb-4">
                <asp:TextBox runat="server" type="text" ID="txtHechos" class="form-control" onkeypress="return check(event)" TextMode="MultiLine" Height="20px" />
                <label class="form-label" for="txtHechos">Posibles hechos</label>
            </div>

            <div class="form-outline mb-4">
                <asp:TextBox runat="server" type="text" ID="txtAnexos" onkeypress="return check(event)" class="form-control" />
                <label class="form-label" for="txtAnexos">Descripción de anexos</label>
            </div>

            <div class="form-outline mb-4">
                <label class="form-label" for="customFile">Adjuntar Documento</label>
                <asp:FileUpload runat="server" CssClass="form-control" ID="filePDF" accept="application/pdf" />
            </div>

             <div class="row mb-4">
                  <div class=" offset-2 col-md-3">
            <asp:Button ID="btn_cancelar" runat="server" OnClick="btn_cancelar_Click" Text="Cancelar" CssClass="btn btn-danger btn-block mb-4" />
                      </div>
                  <div class="offset-2 col-md-3">
            <asp:Button ID="btnPreguardar" runat="server" OnClick="btnPreguardar_Click" Text="Pre-Guardar" CssClass="btn btn-success btn-block mb-4" />
                 </div>
             </div>
        </div>

         <div class="card mt-5 mb-1 ml-2 mr-2 p-4" runat="server" id="divPreguardar">
        <h5 class="card-header">Posterior Pre- Guardada</h5>
        <div class="card-body">
            <h5 class="card-title">Datos de la solicitud</h5>

            <div class="row">
                <div class="col-lg-6">

                    <asp:HiddenField ID="HFNombre" runat="server" />
                    <asp:HiddenField ID="HFRuta" runat="server" />
                    <asp:HiddenField ID="HFFolio" runat="server" />
                    <div class="form-group">
                        <label for="formGroupExampleInput">Folio</label>
                        <asp:TextBox runat="server" type="text" class="form-control" ID="txtFolio" ReadOnly="true" />
                    </div>


                    <div class="form-group">
                        <label for="formGroupExampleInput">Tipo Solicitud</label>
                        <asp:TextBox runat="server" type="text" class="form-control" ID="txtTipoSolicitud" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="formGroupExampleInput">Figura Solicitante</label>
                        <asp:TextBox runat="server" type="text" class="form-control" ID="txtFiguraSolicitante" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="formGroupExampleInput">Juzgado</label>
                        <asp:TextBox runat="server" class="form-control" ID="txtJuzgadoMostrar" ReadOnly="true" />
                    </div>

                    <div class="form-group">
                        <label for="formGroupExampleInput">NUC</label>
                        <asp:TextBox runat="server" type="text" class="form-control" ID="txtNUCSolicitud" ReadOnly="true" />
                    </div>

                    <div class="form-group">
                        <label for="formGroupExampleInput">Posibles Hechos</label>
                        <asp:TextBox runat="server" type="text" class="form-control" ID="txtPosiblesHechosSolicitud" ReadOnly="true" />
                    </div>
                    <div class="form-group">
                        <label for="formGroupExampleInput">Descripcion de anexos</label>
                        <asp:TextBox runat="server" type="text" class="form-control" ID="txtAnexosSolicitud" ReadOnly="true" />
                    </div>


                    <div class="row mt-4 mb-4">
                        <div class="col">
                            <div class="form-outline">
                                <asp:Button ID="btnMostrar" runat="server" OnClientClick="toastConfirmar(); return false;"  
                                    Text="Eliminar" CssClass="btn btn-danger btn-block mb-4" />
                                <asp:Button ID="btnEliminar" runat="server" OnClick="btnEliminar_Click" Text="Eliminar" Style="display: none;" />
                            </div>
                        </div>
                        <div class="col">
                            <div class="form-outline">
                                <asp:Button ID="btnEnviar" runat="server" OnClick="btnEnviar_Click" Text="Firmar y enviar" CssClass="btn btn-success btn-block mb-4" />

                            </div>
                        </div>
                    </div>
                </div>

                <div class="col-lg-6">
                    <div class="">
                        <asp:Literal ID="embedPdf" runat="server" />
                        <iframe runat="server" id="iframepdf" width="800" height="700" />
                    </div>
                </div>
            </div>

        </div>
    </div>



    </div>
</asp:Content>
