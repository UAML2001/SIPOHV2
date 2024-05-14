<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InformacionExpediente.ascx.cs" Inherits="SIPOH.Views.ContenidoExpediente.InformacionExpediente" %>
<asp:UpdatePanel runat="server" ID="PanelInfoExpediente" ChildrenAsTriggers="false" UpdateMode="Conditional">
    <ContentTemplate>
        <div class="row g-3 justify-content-end align-content-center">
            <div class="col-auto">
                <label class="text-secondary">Número de expediente:</label>            
            </div>
            <div class="col-auto">
                <asp:TextBox runat="server" ID="inputNumeroExpediente" CssClass="form-control form-control-sm " MaxLength="20"/>
            </div>
            <div class="col-auto">                
                <Asp:Button class="btn btn-outline-success btn-sm" runat="server"  data-bs-toggle="offcanvas" data-bs-target="#offcanvasWithBothOptions" aria-controls="offcanvasWithBothOptions" OnClick="btnBuscarExpediente" Text="Guardar"/>
            </div>
        </div>

        <div class="offcanvas offcanvas-start bg-light" data-bs-scroll="true" tabindex="-1" id="offcanvasWithBothOptions" aria-labelledby="offcanvasWithBothOptionsLabel">
            <div class="offcanvas-header d-flex flex-column py-1" style="background-color: #3F5259;">
                <i class="bi bi-people text-success fs-1 align-self-center" data-bs-dismiss="offcanvas" aria-label="Close"></i>
                <h5 class="offcanvas-title text-light fw-bolder lh-1 mb-2" id="offcanvasWithBothOptionsLabel">Información de partes:</h5>
            </div>
            <div class="offcanvas-body row align-content-start justify-content-center">
                <div class="mb-2 px-1 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-6 col-xxl-6 ">
                    <label for="inputTipoAsunto" class="form-label text-secondary">Fecha de presentación: </label>
                    <asp:TextBox runat="server" ID="inputFechaPresentacion" CssClass="form-control form-control-sm" ReadOnly="true" Text="27/03/2024 12:30 p.m"/>
                </div>
                <div class="mb-4 px-1 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-6 col-xxl-6 ">
                    <label for="inputTipoAsunto" class="form-label text-secondary">Tipo de juicio: </label>
                    <asp:TextBox runat="server" ID="inputTipoJuicio" CssClass="form-control form-control-sm" ReadOnly="true" Text="ORDINARIO"/>
                </div>
                <div class="col-auto mb-2" data-bs-toggle="collapse" data-bs-target="#collapsePartes" aria-expanded="false" aria-controls="collapsePartes">
                    <span class="btn btn-outline-success btn-sm"><i class="bi bi-eye-fill"></i> Ver partes</span>                   
                </div>
                <div class=" row align-content-start justify-content-center">
                    <div class="collapse px-0" id="collapsePartes">
                      <div class="card card-body mx-0 ">
                        <div class="mb-4 px-0 col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12 ">
                            <label for="inputTipoAsunto" class=" text-secondary fw-semibold text-lg-end ">Victimas: </label>
                            <ul class="list-group list-group-flush">
                              <li class="list-group-item text-secondary small"><span class="fw-semibold text-dark">MARTA</span> SUÑIGA ORTEGA</li>
                              <li class="list-group-item text-secondary small"><span class="fw-semibold text-dark">MARCO FABIAN </span>LOPEZ HERRERA</li>
                              <li class="list-group-item text-secondary small"><span class="fw-semibold text-dark">SARAI</span> JUAREZ LOPEZ</li>
                              <li class="list-group-item text-success small">No hay mas victimas</li>                          
                            </ul>
                        </div>
                          <div class="mb-4 px-0 col-12 col-sm-12 col-md-12 col-lg-12 col-xl-12 col-xxl-12">
                            <label for="inputTipoAsunto" class="form-label text-secondary fw-semibold">Imputados: </label>
                            <ul class="list-group list-group-flush">
                              <li class="list-group-item text-secondary small"><span class="fw-semibold text-dark">ROSENDO</span> SUÑIGA ORTEGA -<span class="text-success"> Robo</span></li>
                              <li class="list-group-item text-secondary small"><span class="fw-semibold text-dark">NORMA</span> GALINDO SEA -<span class="text-success"> Robo</span></li>
                              <li class="list-group-item text-secondary small"><span class="fw-semibold text-dark">aMELIA</span> JUAREZ LOPEZ -<span class="text-success"> Robo</span></li>
                              <li class="list-group-item text-success small">No hay mas imputados</li>                          
                            </ul>
                        </div>
                      </div>
                    </div>
                </div>

                <div class="mb-4 pt-2 px-1 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-6 col-xxl-6 ">
                    <label for="inputTipoAsunto" class="form-label text-secondary">Número expediente: </label>
                    <asp:TextBox runat="server" ID="lblNumeroExpediente" CssClass="form-control form-control-sm text-success text-center" ReadOnly="true" Text="0012/2024"/>
                </div>
            </div>
        </div>
        <div class="mt-4 clearfix">                 
             <i class="btn btn-outline-success float-start btn-sm bi bi-hand-index-thumb" data-bs-toggle="offcanvas" data-bs-target="#offcanvasWithBothOptions" aria-controls="offcanvasWithBothOptions"></i>
            <span class="text-success fw-bold m-2 float-end" > Expediente:</span>
        </div>
        <div class="table-responsive  rounded rounded-3">
                            <table class="table table-hover mb-0 table-sm " id="tablaPermisos">
                                <thead class="">
                                    <tr>
                                        <th class=" text-white bg-success text-center align-middle">No. expediente</th>                                                                               
                                        <th class="text-white bg-success text-center align-middle">Tipo</th>
                                        <th class="text-white bg-success text-center align-middle">Tarea</th>
                                        <th class="text-white bg-success text-center align-middle">Documento</th>                                        
                                        <th class="text-white bg-success text-center align-middle">Proveniente</th>
                                        <th class="text-white bg-success text-center align-middle">Visualizar documento</th>
                                        <th class="text-white bg-success text-center align-middle">Ver notificaciones</th>
                                        <th class="text-white bg-success text-center align-middle">Seleccionar</th>
                                    </tr>
                                </thead>
                                <tbody>
                                     

                                        
                                     <asp:Repeater ID="busquedaExpediente" runat="server"  >
                                     <%--<asp:Repeater ID="busquedaExpediente" runat="server"  OnItemDataBound="RepeaterPermisoAsociado_ItemDataBound">--%>
                                        <ItemTemplate>
                                             <tr>
                                                                                                                                                                                     
                                           </tr> 
                                        </ItemTemplate>
                                    </asp:Repeater>
                                   <tr class="">
                                        <th class="text-success text-center small align-middle fw-bolder">0012/2024</th>                                        
                                        <td class="text-secondary text-center small align-middle">POSTERIOR</td>
                                        <td class="text-dark text-center small align-middle fw-bolder">TURNADO PARA SECRETARIO INSTRUCTOR</td>
                                        <td class="text-secondary text-center small align-middle">POSTERIOR</td>
                                        <td class="text-secondary text-center small align-middle">JOSUE</td>
                                        <td class="text-secondary text-center align-middle"><i class="bi bi-file-earmark-pdf-fill text-danger" data-bs-toggle="modal" data-bs-target="#staticBackdropPDF" ></i> </td>
                                        <td class="text-secondary text-center align-middle"><i class="bi bi-search text-success"></i></td>
                                        <td class="text-secondary text-center align-middle"><i class="bi bi-square text-secondary"></i></td>                                        
                                    </tr>
                                    
                                    <tr>
                                        <th class=""></th>
                                        <td class=""></td>
                                        <td class=""></td>                                                                                
                                        <td class="text-success small">No hay mas contenido por mostrar.</td>
                                        <td class=""></td>
                                        <td class=""></td>
                                        <td class=""></td>
                                        <td class=""></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
        <div class="mt-4 ">                 
            <span class="text-success fw-bold m-2 " > Motivo de recepción:</span>            
        </div>
        
         <asp:Repeater ID="Repeater1" runat="server"  >
            <%--<asp:Repeater ID="busquedaExpediente" runat="server"  OnItemDataBound="RepeaterPermisoAsociado_ItemDataBound">--%>
            <ItemTemplate>
                <tr>                                                                                                                                                                                     
                </tr> 
            </ItemTemplate>
         </asp:Repeater>
          <div class="col-12 border-1 rounded mt-3" >
            <div class="row justify-content-center">
                <label  class="col-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 col-xxl-3 d-flex justify-content-between align-content-center border rounded  py-3 px-2 m-2 bg-light"for="inputAcuerdo" >
                    <span class="float-start text-dark fw-bolder" >Acuerdo</span>
                    <input class="float-end " type="checkbox" id="inputAcuerdo" />
                </label>
                <div class="col-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 col-xxl-3 d-flex justify-content-between align-content-center border rounded  py-3 px-2 m-2 bg-light">
                    <span class="float-start text-dark fw-bolder">Oficio</span>
                    <input class="float-end " type="checkbox" />
                </div>
                <div class="col-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 col-xxl-3 d-flex justify-content-between align-content-center border rounded  py-3 px-2 m-2 bg-light">
                    <span class="float-start text-dark fw-bolder">Oficio</span>
                    <input class="float-end " type="checkbox" />
                </div>
                <div class="col-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 col-xxl-3 d-flex justify-content-between align-content-center border rounded  py-3 px-2 m-2 bg-light">
                    <span class="float-start text-dark fw-bolder">Corrección Documentos</span>
                    <input class="float-end" type="checkbox" />
                </div>
                <div class="col-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 col-xxl-3 d-flex justify-content-between align-content-center border rounded  py-3 px-2 m-2 bg-light">
                    <span class="float-start text-dark fw-bolder">Oficio</span>
                    <input class="float-end " type="checkbox" />
                </div>
                <div class="col-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 col-xxl-3 d-flex justify-content-between align-content-center border rounded  py-3 px-2 m-2 bg-light">
                    <span class="float-start text-dark fw-bolder">Oficio</span>
                    <input class="float-end " type="checkbox" />
                </div>
            </div>
        </div>
        <div class=" d-flex justify-content-center mt-5">
            <a class="btn btn-success btn-sm" data-bs-toggle="modal" OnClick="valoresFinalesCC();" data-bs-target="#modal3"><i class="bi bi-floppy-fill mr-1"></i>Guardar</a>                    
        </div>


        <!-- Button trigger modal -->


<!-- Modal -->
<div class="modal fade" id="staticBackdropPDF" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabelPDF" aria-hidden="true">
  <div class="modal-dialog">
    <div class="modal-content">
      <div class="modal-header">
        <h5 class="modal-title" id="staticBackdropLabelPDF">Modal title</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <iframe src="https://drive.google.com/file/d/13AuP4p33kQuUWw2YsTJA7Tu5otRdlDpE/view?usp=sharing" style="width: 100%; height: 500px;"></iframe>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Close</button>
        <button type="button" class="btn btn-primary">Understood</button>
      </div>
    </div>
  </div>
</div>

    </ContentTemplate>
</asp:UpdatePanel>
