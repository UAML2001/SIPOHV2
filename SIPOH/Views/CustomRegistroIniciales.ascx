<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomRegistroIniciales.ascx.cs" Inherits="SIPOH.Views.CustomRegistroIniciales" %>


       <script type="text/javascript">
           function toggleElements() {
              var ddlPersona = document.getElementById('<%= ddlPersonaVictima.ClientID %>');
              var lblNombre = document.getElementById('<%= lblNombre.ClientID %>');
              var txtNombre = document.getElementById('<%= txtNombreVictima.ClientID %>');
              var lblAP = document.getElementById('<%= lblAP.ClientID %>');
              var txtAP = document.getElementById('<%= txtAPVictima.ClientID %>');
              var lblAM = document.getElementById('<%= lblAM.ClientID %>');
              var txtAM = document.getElementById('<%= txtAMVictima.ClientID %>');
              var lblRazonSocial = document.getElementById('<%= lblRazonSocial.ClientID %>');
              var txtRazonSocial = document.getElementById('<%= txtRazonSocialVictima.ClientID %>');
              var lblSexo = document.getElementById('<%= lblSexo.ClientID %>');
               var ddlSexo = document.getElementById('<%= ddlSexoVictima.ClientID %>');

               if (ddlPersona.value === 'fisica') {
                   // Mostrar elementos para persona física
                   lblNombre.style.display = 'block';
                   txtNombre.style.display = 'block';
                   lblAP.style.display = 'block';
                   txtAP.style.display = 'block';
                   lblAM.style.display = 'block';
                   txtAM.style.display = 'block';
                   lblRazonSocial.style.display = 'none';
                   txtRazonSocial.style.display = 'none';
                   lblSexo.style.display = 'block';
                   ddlSexo.style.display = 'block';
               } else if (ddlPersona.value === 'moral') {
                   // Mostrar elementos para persona moral
                   lblNombre.style.display = 'none';
                   txtNombre.style.display = 'none';
                   lblAP.style.display = 'none';
                   txtAP.style.display = 'none';
                   lblAM.style.display = 'none';
                   txtAM.style.display = 'none';
                   lblRazonSocial.style.display = 'block';
                   txtRazonSocial.style.display = 'block';
                   lblSexo.style.display = 'none';
                   ddlSexo.style.display = 'none';
               } else {
                   // Mostrar elementos por defecto
                   lblNombre.style.display = 'none';
                   txtNombre.style.display = 'none';
                   lblAP.style.display = 'none';
                   txtAP.style.display = 'none';
                   lblAM.style.display = 'none';
                   txtAM.style.display = 'none';
                   lblRazonSocial.style.display = 'none';
                   txtRazonSocial.style.display = 'none';
                   lblSexo.style.display = 'none';
                   ddlSexo.style.display = 'none';
               }
           }

           window.onload = function () {
               // Ocultar elementos al cargar la página
               toggleElements();
               var ddlPersona = document.getElementById('<%= ddlPersonaVictima.ClientID %>');
               ddlPersona.value = '';
               lblNombre.value = '';
               txtAP.value = '';
               
               txtAM.value = '';
               lblRazonSocial.value = '';
               txtRazonSocial.value = '';
               lblSexo.value = '';
               
           }
               
               
    
</script>




        
 


<asp:UpdatePanel runat="server" ID="updPanel" ChildrenAsTriggers="false" UpdateMode="Conditional">

    <ContentTemplate>
 

     
<div class="d-flex justify-content-between align-items-center flex-wrap">
    <h5 class="text-secondary mb-0">Registro de iniciales</h5>
    <div class="row g-1 ">
        
        <div class="col-auto  ">            
            <input type="text" class="form-control form-control-sm " id="inputBuscar" placeholder="Busca si hay un registro actualmente">
        </div>
        <div class="col-auto ">
            <i class="bi bi-search btn btn-success btn-sm" ></i>
        </div>
    </div>
</div>

<div class="row pt-5">
   
   <form class="container-lg col-xxl-12">
       <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputNUC" class="form-label text-secondary">NUC:</label>
            <asp:TextBox runat="server" ID="inputNUC" CssClass="form-control form-control-sm" />
                
        </div>
       <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3 ">
           <label for="inputTipoAsunto" class="form-label text-secondary">Tipo de asunto: </label>
           
           <asp:DropDownList runat="server" ID="inputTipoAsunto" AutoPostBack="true" CssClass="form-select form-select-sm" OnSelectedIndexChanged="inputTipoAsunto_SelectedIndexChanged">
            <asp:ListItem Text="Seleccione..." Value="" />
            <asp:ListItem Text="C" Value="C" />
            <asp:ListItem Text="CP" Value="CP" />
        </asp:DropDownList>

        
       </div>
       <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
           <label for="inpuTipoSolicitud" class="form-label text-secondary">Tipo solicitud: </label>
           
            <asp:DropDownList runat="server" ID="inputRadicacion"  CssClass="form-select form-select-sm text-dark"></asp:DropDownList>

       </div>


       <div class=" mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputFechaRecepcion" class="form-label text-secondary">Fecha de recepcion</label>
            <asp:TextBox runat="server" ID="inputFechaRecepcion" CssClass="form-control form-control-sm" TextMode="Date"></asp:TextBox>
        </div>
        
        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputNumeroFojas" class="form-label text-secondary">Numero de fojas:</label>
            <asp:TextBox runat="server" ID="inputNumeroFojas" CssClass="form-control form-control-sm" />
                
        </div>
       <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
           <label  class="form-label text-secondary">Quien ingresa: </label>
           
           <asp:DropDownList runat="server" CssClass="form-select form-select-sm" ID="inputQuienIngresa" ClientIDMode="Static" AutoPostBack="false" >
               <asp:ListItem Text="Seleccionar" Value="0" Selected="True" />
               <asp:ListItem Text="MP" Value="M" ID="MP"/>
               <asp:ListItem Text="Particular" Value="P" ID="particular" />
               <asp:ListItem Text="Otro" Value="O"  />
           </asp:DropDownList>
           
       </div>
        <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputNombreParticular" class="form-label text-secondary">Especificar nombre de <span id="tipoPersona"></span>: </label>
            <asp:TextBox runat="server" ID="inputNombreParticular" CssClass="form-control form-control-sm" />
        </div>

       <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
           <label class="form-label text-secondary">Tipo de radicacion: </label>
           <div class="d-flex align-items-center">
           <asp:DropDownList runat="server" CssClass="form-select form-select-sm" ID="inpuTipoRadicacion" AutoPostBack="false" >
               <asp:ListItem Text="Seleccionar" Value="" Selected="True" />
               <asp:ListItem Text=" C/Detenido" Value="C"  CssClass="form-check-input"/>
                   <asp:ListItem Text=" S/Detenido" Value="S" CssClass="form-check-input" />
           </asp:DropDownList>
               
           </div>

       </div>


       <div class="mb-0 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label  class="form-label text-secondary">Prioridad: </label>
            <div class=" d-flex align-items-center">
                <asp:RadioButtonList runat="server" ID="inputPrioridad" CssClass="form-check text-secondary d-flex flex-row" ClientIDMode="Static">
                   <asp:ListItem Text="Alta" Value="A" />
                    <asp:ListItem Text="Normal" Value="N"/>
                </asp:RadioButtonList>
            </div>
       </div>
       <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputObservaciones" class="form-label text-secondary">Observaciones: </label>
            <asp:TextBox runat="server" ID="inputObservaciones" CssClass="form-control form-control-sm" ></asp:TextBox>
        </div>
       
       <%--first part--%>
       <div class="row">

          
               <div class="col-12 col-lg-6 text-left ">

                   <span class="text-success fw-bold m-2"><i class="bi bi-emoji-laughing"></i>Victima</span>
                   <i class="bi bi-plus-square-fill text-success fs-6 text-right" data-bs-toggle="modal" data-bs-target="#modal2"></i>

                   <div class="table-responsive mt-2">
                    
                

            <table class="table table-striped table-hover mb-0 table-sm">
                <thead class="text-center">
                    <tr>
                        <th scope="col" class="bg-success text-white">Nombre</th>
                        <th scope="col" class="bg-success text-white">Genero</th>
                        <th scope="col" class="bg-success text-white">Acciones</th>
                    </tr>
                </thead>
                <tbody class="table table-striped text-center">
                    <asp:Repeater ID="Repeater1" runat="server">
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval("Nombre") %></th>
                                <td class="text-secondary"><%# Eval("Genero") %></td>
                                <td><i class="bi bi-trash-fill text-danger"></i></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
                   </div>
            
                   
           
         
               <div class="col-12 col-lg-6 text-left">

                   <span class="text-success fw-bold m-2"><i class="bi bi-emoji-frown"></i>Imputado</span>
                   <i class="bi bi-plus-square-fill text-success fs-6 text-right " data-bs-toggle="modal" data-bs-target="#modal1"></i>
                   <div class="table-responsive mt-2">

                       <table class="table table-striped table-hover mb-0 table-sm">
                           <thead class=" text-center">
                               <tr class="">

                                   <th scope="col" class="bg-success text-white">Nombre</th>
                                   <th scope="col" class="bg-success text-white">Genero</th>
                                   <th scope="col" class="bg-success text-white">Acciones</th>

                               </tr>
                           </thead>
                           <tbody class="table table-striped text-center ">
                               <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval("NombreCulpado") %></th>
                                <td class="text-secondary"><%# Eval("GeneroCulpado") %></td>
                                <td><i class="bi bi-trash-fill text-danger"></i></td>
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
                       

                        <asp:DropDownList runat="server" CssClass="form-select form-select-sm text-capitalize" ID="inputDelitos" AutoPostBack="false">
                            <asp:ListItem Text="Seleccionar" Value="" Selected="True"  CssClass=" text-capitalize"/>
                        </asp:DropDownList>
                        <asp:Label runat="server" ID="lblNombresDelitos" Text=""></asp:Label>

                    </div>
                    <div class="col-1 d-flex justify-content-center align-content-center">
                        <i class="bi bi-plus-square-fill text-success fs-6 text-right p-2 align-self-end" data-bs-toggle="modal" data-bs-target="#modalDelitos" id="btnObtenerDelitos"></i>
                    </div>
                </div>
                <asp:Label runat="server" ID="lblIdDelitos" Text="Ingrese los delitos" CssClass="text-success"></asp:Label>
                <asp:Literal runat="server" ID="litTablaDelitos" />




                <div class="table-responsive mt-1 ">

                    <asp:GridView ID="gvResultados" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-hover mb-0 table-sm">
    <Columns>
        <asp:TemplateField HeaderText="Nombre" ItemStyle-CssClass="text-center text-bold ">
            <ItemTemplate >
                <asp:Label ID="lblNombre" runat="server" Text='<%# Eval("Nombre") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Acciones" ItemStyle-CssClass="text-center ">
            <ItemTemplate>
                <i class="bi bi-trash-fill text-danger"></i>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <HeaderStyle CssClass="text-center" />
    <RowStyle CssClass="text-center" />
</asp:GridView>
              
                 </div>
            </div>
            <div class="col-12 col-lg-6 text-left">

                <div class="mb-0 row">
                    <div class="col-md-4">
                        <label for="inputAnexos" class="form-label text-secondary align-self-center">Anexos: </label>
                        <asp:DropDownList runat="server" CssClass="form-select form-select-sm" ID="txtAnexosTipo"   AutoPostBack="false" >
                            <asp:ListItem Text="Seleccionar" Value="" Selected="True" />
                        </asp:DropDownList>

                    </div>

                    <div class="col-md-4">

                        <label for="inputDescripcionO" class="form-label text-secondary align-self-center">Descripcion: </label>
                        <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtDescripcionAnexos"/>
                    </div>
                     <div class="col-md-3">
                        
                        <label for="inputCantidad" class="form-label text-secondary align-self-center">Cantidad: </label>
                        <asp:TextBox runat="server" CssClass="form-control form-control-sm" ID="txtCantidadAnexos" />
                    </div>
                    <div class="col-1 d-flex justify-content-center align-content-center">
                        <i class="bi bi-plus-square-fill text-success fs-6 text-right p-2 align-self-end" data-bs-toggle="modal" data-bs-target="#modalAnexos"></i>
                    </div>
                </div>
                <div class="table-responsive mt-2">

                        <table class="table table-striped table-hover mb-0 table-sm">
                            <thead class=" text-center">
                                <tr class="">

                                    <th scope="col" class="bg-success text-white">Descripcion</th>
                                    <th scope="col" class="bg-success text-white">Cantidad</th>
                                    <th scope="col" class="bg-success text-white">Acciones</th>

                                </tr>
                            </thead>
                            <tbody class="table table-striped text-center ">
                               <asp:Repeater ID="Repeater3" runat="server">
                        <ItemTemplate>
                            <tr>
                                <th scope="row"><%# Eval("DescripcionAnexo") %></th>
                                <td class="text-secondary"><%# Eval("CantidadAnexo") %></td>
                                <td><i class="bi bi-trash-fill text-danger"></i></td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>                                                       
                            </tbody>
                        </table>         
                 </div>
            </div>
        </div>
         <div class=" d-flex justify-content-center mt-5">
            <a type="submit" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modal3" ><i class="bi bi-floppy-fill mr-1"></i>Enviar</a>
        </div>
             <asp:Label runat="server" ID="lblSuccess" Text="" CssClass="text-success text-center"></asp:Label>
             <asp:Label runat="server" ID="lblError" Text="" CssClass="text-danger text-center"></asp:Label>



       
        
    </form>
    




</div>


<%--modales--%>
<!-- Modal imputado -->
<div class="modal fade" id="modal1" tabindex="-1" aria-labelledby="modal1Label" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
        <%--contenido--%>
      <div class="modal-header ">
          <div class="row d-flex align-items-center justify-content-center text-center col-12">
            <i class="bi bi-emoji-frown text-success pr-2" style="font-size:56px;"></i>
            <h4 class="modal-title text-secondary " >Agregar partes</h4>
          </div>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body d-flex flex-wrap justify-content-end">
        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <asp:Label ID="lblNombreImputado" runat="server" AssociatedControlID="txtNombreImputado" CssClass="form-label text-secondary">Nombre (s):</asp:Label>
            <asp:TextBox ID="txtNombreImputado" runat="server" CssClass="form-control form-control-sm" />
        </div>
          
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
              <asp:Label ID="lblAPaternoImputado" runat="server" AssociatedControlID="txtAPaternoImputado" CssClass="form-label text-secondary">Apellido Paterno:</asp:Label>
              <asp:TextBox ID="txtAPaternoImputado" runat="server" CssClass="form-control form-control-sm"  />
          </div>
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <asp:Label ID="lblAMaternoImputado" runat="server" AssociatedControlID="txtAMaternoImputado" CssClass="form-label text-secondary">Apellido Materno:</asp:Label>
            <asp:TextBox ID="txtAMaternoImputado" runat="server" CssClass="form-control form-control-sm"  />
        </div>
            
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <asp:Label ID="lblGeneroImputado" runat="server" AssociatedControlID="txtGeneroImputado" CssClass="form-label text-secondary">Sexo:</asp:Label>
             <asp:DropDownList runat="server" ID="txtGeneroImputado" CssClass="form-select form-select-sm" >
                <asp:ListItem Text="Seleccionar" Value="" />
                <asp:ListItem Text="Femenino" value="F" />
                <asp:ListItem  Text="Masculino" value="M" />
                <asp:ListItem Text="Otro" Value="O" />                
            </asp:DropDownList>
        </div>
        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <asp:Label ID="lblAliasImputado" runat="server" AssociatedControlID="txtAliasImputado" CssClass="form-label text-secondary">Alias:</asp:Label>
            <asp:TextBox ID="txtAliasImputado" runat="server" CssClass="form-control form-control-sm" />
        </div>


      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cerrar</button>
        <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Guardar" OnClick="btnGuardarImputado_Click" data-bs-dismiss="modal"/>
          
      </div>
    </div>
  </div>
</div>

<!-- Modal Partes victima -->
<div class="modal fade" id="modal2" tabindex="-1" aria-labelledby="modal2Label" aria-hidden="true">
  <div class="modal-dialog modal-lg">
    <div class="modal-content">
      <!-- Contenido del segundo modal -->
        <div class="modal-header">
          <div class="row d-flex align-items-center justify-content-center text-center col-12">
            <i class="bi bi-emoji-smile text-success pr-2" style="font-size:56px;"></i>
            <h4 class="modal-title text-secondary " >Agregar partes</h4>
          </div>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      
      <div class="modal-body d-flex flex-wrap justify-content-end">
          

          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
              <label for="inputvictimapersona" class="form-label text-secondary">Persona:</label>

              <asp:DropDownList ID="ddlPersonaVictima" runat="server" CssClass="form-select form-select-sm" AutoPostBack="false" onchange="toggleElements()">
                  <asp:ListItem Text="Seleccionar" Value="" />
                  <asp:ListItem Text="Física" Value="fisica" />
                  <asp:ListItem Text="Moral" Value="moral" />
              </asp:DropDownList>
          </div>

          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
             <asp:Label ID="lblNombre" runat="server" AssociatedControlID="txtNombreVictima" CssClass="form-label text-secondary">Nombre(s):</asp:Label>
              <asp:TextBox ID="txtNombreVictima" runat="server" CssClass="form-control form-control-sm" />
          </div>

          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
              <asp:Label id="lblAP" runat="server" AssociatedControlID="txtAPVictima" class="form-label text-secondary">Apellido paterno:</asp:Label>
              <asp:TextBox ID="txtAPVictima" runat="server" CssClass="form-control form-control-sm" />
          </div>

          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
              <asp:Label id="lblAM" runat="server"  AssociatedControlID="txtAMVictima"  class="form-label text-secondary">Apellido materno:</asp:Label>
              <asp:TextBox ID="txtAMVictima" runat="server" CssClass="form-control form-control-sm" />
          </div>

          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
              <label id="lblRazonSocial" runat="server"  AssociatedControlID="txtRazonSocialVictima" class="form-label text-secondary">Razón Social:</label>
              <asp:TextBox ID="txtRazonSocialVictima" runat="server" CssClass="form-control form-control-sm" />
          </div>

          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
              <asp:Label id="lblSexo" runat="server"  AssociatedControlID="ddlSexoVictima"  class="form-label text-secondary">Sexo:</asp:Label>
              <asp:DropDownList ID="ddlSexoVictima" runat="server" CssClass="form-select form-select-sm">
                  <asp:ListItem Text="Seleccionar" Value="" />
                  <asp:ListItem Text="Femenino" Value="F" />
                  <asp:ListItem Text="Masculino" Value="M" />
                  <asp:ListItem Text="Otro" Value="O" />
              </asp:DropDownList>
          </div>





      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cerrar</button>
        <asp:Button runat="server" Text="Guardar" CssClass="btn btn-success btn-sm" OnClick="btnGuardarVictima_Click"  data-bs-dismiss="modal"/>

    
      </div>
    </div>
  </div>
</div>

<%--delitos--%>
<div class="modal fade" id="modalDelitos" tabindex="-1" aria-labelledby="modalDelitosLabel" aria-hidden="true">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
        <%--contenido--%>
      <div class="modal-header">
         <div class="row d-flex align-items-center justify-content-center text-center col-12">
            <i class="bi bi-question-lg text-warning pr-2" style="font-size:56px;"></i>
            <h4 class="modal-title text-secondary " >¿Desea añadir el delito?</h4>
         </div>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cancelar</button>
       <asp:Button runat="server" CssClass="btn btn-warning btn-sm" Text="Guardar" OnClick="btnEnviarDelito_Click" UseSubmitBehavior="false"   data-bs-dismiss="modal"/>
      </div>
    </div>
  </div>
</div>
<%--anexos--%>
<div class="modal fade" id="modalAnexos" tabindex="-1" aria-labelledby="modalAnexosLabel" aria-hidden="true">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
        <%--contenido--%>
      <div class="modal-header">
           <div class="row d-flex align-items-center justify-content-center text-center col-12">
            <i class="bi bi-question-lg text-warning pr-2" style="font-size:56px;"></i>
            <h4 class="modal-title text-secondary " >¿Desea añadir los anexos?</h4>
         </div>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        <p class="text-secondary">Anexos: <span class="text-success">...</span></p>
        <p class="text-secondary">Descripcion: <span class="text-success">...</span></p>
        <p class="text-secondary">Cantidad: <span class="text-success">...</span>   </p>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cerrar</button>        
        <asp:Button runat="server" CssClass="btn btn-warning btn-sm" Text="Guardar" OnClick="btnGuardarAnexos_Click" data-bs-dismiss="modal" />
         
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
        
<!-- MOdal Save Changes -->
<div class="modal fade" id="modal3" tabindex="1" aria-labelledby="modal3Label" aria-hidden="true">
  <div class="modal-dialog modal-sm">
    <div class="modal-content">
      <!-- Contenido del segundo modal -->
        <div class="modal-header">
          <i class="bi bi-exclamation-circle text-success fs-3 pr-2"></i>
        <h5 class="modal-title text-secondary fs-4 " >Guardar Cambios</h5>
        <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
      </div>
      <div class="modal-body">
        ...
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cerrar</button>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
          <asp:Button runat="server" CssClass="btn btn-success btn-sm" Text="Enviar" OnClick="btnEnviarInicial_Click" data-bs-dismiss="modal"  postback="true"  />
           
        </ContentTemplate>
    </asp:UpdatePanel>

      </div>
    </div>
  </div>
</div>
</ContentTemplate>
</asp:UpdatePanel>


<script src="../Scripts/consignaciones/RegistroIniciales.js"></script>
<!-- Asegúrate de tener una referencia a la biblioteca jQuery en tu proyecto -->

