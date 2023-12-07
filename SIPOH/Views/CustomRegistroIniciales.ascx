<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomRegistroIniciales.ascx.cs" Inherits="SIPOH.Views.CustomRegistroIniciales" %>

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
       
        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3 ">
            <label for="inputTipoAsunto" class="form-label text-secondary">Tipo de asunto: </label>
            <select class="form-select form-select-sm" id="inputTipoAsunto">
                <option selected>Seleccionar</option>
                <option value="1">Causa</option>
                <option value="2">Cupre</option>                
            </select>
        </div>
        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputRadicacion" class="form-label text-secondary">Tipo solicitud: </label>
            <select class="form-select form-select-sm" id="inputRadicacion">
                <option selected>Seleccionar</option>
                <option value="1">Catalogo audiencia</option>
                <option value="2">Innominada</option>
                <option value="3">. . .</option>
            </select>
        </div>


        <div class=" mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputFechaRecepcion" class="form-label text-secondary">Fecha de recepcion</label>
            <input type="date" class="form-control form-control-sm" id="inputFechaRecepcion">
        </div>
        
        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputNumeroFojas" class="form-label text-secondary">Numero de fojas:</label>
            <input type="text" class="form-control form-control-sm" id="inputNumeroFojas" />
                
        </div>
       <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
           <label  class="form-label text-secondary">Quien ingresa: </label>
           <div class="d-flex align-items-center">
            <div class="form-check pr-3">
                <input class="form-check-input" type="radio" name="flexRadioQuienIngresa" id="MP">
                <label class="form-check-label text-secondary" for="MP">
                    MP
                </label>
            </div>
            <div class="form-check">
                <input class="form-check-input" type="radio" name="flexRadioQuienIngresa" id="particular">
                <label class="form-check-label text-secondary" for="particular">
                    Particular
                </label>
            </div>
           </div>
       </div>
        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputNombreParticular" class="form-label text-secondary">Especificar nombre de <span id="tipoPersona"></span>: </label>
            <input type="text" class="form-control form-control-sm" id="inputNombreParticular" />
        </div>

       <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
           <label  class="form-label text-secondary">Tipo de radicacion: </label>
           <div class="d-flex align-items-center">
            <div class="form-check pr-3 ">
                <input class="form-check-input" type="radio" name="flexRadioTipoRadicacion" id="flexRadioTipoRadicacionConDetenido">
                <label class="form-check-label text-secondary" for="flexRadioTipoRadicacionConDetenido">
                    C/Detenido
                </label>
           </div>
            <div class="form-check ">
                <input class="form-check-input" type="radio" name="flexRadioTipoRadicacion" id="flexRadioTipoRadicacionSinDetenido" >
                <label class="form-check-label text-secondary" for="flexRadioTipoRadicacionSinDetenido">
                    S/Detenido
                </label>
            </div>
           </div>

       </div>
          
        
        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label  class="form-label text-secondary">Prioridad: </label>
            <div class=" d-flex align-items-center">
                <div class="form-check pr-4">
                    <input class="form-check-input" type="radio" name="Prioridad" id="flexRadioPrioridadAlta">
                    <label class="form-check-label text-secondary" for="flexRadioPrioridadAlta">
                        Alta
                    </label>
                </div>
                <div class="form-check">
                    <input class="form-check-input" type="radio" name="Prioridad" id="flexRadioPrioridadBaja">
                    <label class="form-check-label text-secondary" for="flexRadioPrioridadBaja">
                        Baja
                    </label>
                </div>
            </div>
       </div>
        
        <%--first part--%> 
        <div class="row mt-5">
            <div class="col-12 col-lg-6 text-left">
                
                    <span class="text-success fw-bold m-2"><i class="bi bi-emoji-laughing"></i> Victima</span>
                    <i class="bi bi-plus-square-fill text-success fs-6 text-right"  data-bs-toggle="modal" data-bs-target="#modal2"></i>
                
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
                                <tr>
                                    <th scope="row">Marta Morales Torres</th>
                                    <td class="text-secondary">Mujer</td>                                    
                                    <td><i class="bi bi-trash-fill text-danger"></i></td>
                                </tr> 
                                <tr>
                                    <th scope="row">Juan Angel Morales Torres</th>
                                    <td class="text-secondary">Hombre</td>                                    
                                    <td><i class="bi bi-trash-fill text-danger"></i></td>
                                </tr> 
                            </tbody>
                        </table>
                     
                 </div>
            </div>
            <div class="col-12 col-lg-6 text-left">
                
                <span class="text-success fw-bold m-2"><i class="bi bi-emoji-frown"></i> Imputado</span>
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
                                <tr>
                                    <th scope="row">Luis Angel Morales Torres</th>
                                    <td class="text-secondary">Hombre</td>                                    
                                    <td><i class="bi bi-trash-fill text-danger"></i></td>
                                </tr>                                                         
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
                        <select class="form-select form-select-sm" id="inputDelitos">
                            <option selected>Seleccionar</option>
                            <option value="1">. . .</option>
                            <option value="2">. . .</option>
                            <option value="3">. . .</option>
                        </select>
                    </div>

                    
                    <div class="col-1 d-flex justify-content-center align-content-center">
                        <i class="bi bi-plus-square-fill text-success fs-6 text-right p-2 align-self-end" data-bs-toggle="modal" data-bs-target="#modalDelitos"></i>
                    </div>
                </div>



                <div class="table-responsive mt-1 ">

                        <table class="table table-striped table-hover mb-0 table-sm">
                           <thead class=" text-center">
                                <tr class="">

                                    <th scope="col" class="bg-success text-white">Descripcion delito</th>
                                    <th scope="col" class="bg-success text-white">Descripcion Subtipo</th>
                                    <th scope="col" class="bg-success text-white">Acciones</th>

                                </tr>
                            </thead>
                            <tbody class="table table-striped text-center ">
                                <tr>
                                    <th scope="row">. . .</th>
                                    <td class="text-secondary">. . .</td>                                    
                                    <td><i class="fas fa-pen text-warning"></i></td>
                                </tr> 
                                <tr>
                                    <th scope="row">. . .</th>
                                    <td class="text-secondary">. . .</td>                                    
                                    <td><i class="fas fa-pen text-warning"></i></td>
                                </tr> 
                            </tbody>
                        </table>
                     
                 </div>
            </div>
            <div class="col-12 col-lg-6 text-left">
                
                 <div class="mb-0 row">
                    <div class="col-md-4">
                        
                        <label for="inputAnexos" class="form-label text-secondary align-self-center">Anexos: </label>
                        <select class="form-select form-select-sm" id="inputAnexos">
                            <option selected>Seleccionar</option>
                            <option value="1">. . .</option>
                            <option value="2">. . .</option>
                            <option value="3">. . .</option>
                        </select>
                    </div>

                    <div class="col-md-4">
                        
                        <label for="inputDescripcionO" class="form-label text-secondary align-self-center">Descripcion: </label>
                        <input type="text" class="form-control form-control-sm" id="inputDescripcionO">
                    </div>
                     <div class="col-md-3">
                        
                        <label for="inputCantidad" class="form-label text-secondary align-self-center">Cantidad: </label>
                        <input type="text" class="form-control form-control-sm" id="inputCantidad">
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
                                <tr>
                                    <th scope="row">lorem itzum</th>
                                    <td class="text-secondary">2</td>                                    
                                    <td><i class="fas fa-pen text-warning"></i></td>
                                </tr>                                                         
                            </tbody>
                        </table>         
                 </div>
            </div>
        </div>
         <div class=" d-flex justify-content-center mt-5">
            <a type="submit" class="btn btn-success" data-bs-toggle="modal" data-bs-target="#modal3" ><i class="bi bi-floppy-fill mr-1"></i>Enviar</a>
        </div>
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
            <label for="inputNombreImputado" class="form-label text-secondary">Nombre (s):</label>
            <input type="text" class="form-control form-control-sm" id="inputNombreImputado" />
        </div>
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
              <label for="inputApellidoPImputado" class="form-label text-secondary">Apellido Paterno:</label>
              <input type="text" class="form-control form-control-sm" id="inputApellidoPImputado" />
          </div>
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputApellidoMImputado" class="form-label text-secondary">Apellido Materno:</label>
            <input type="text" class="form-control form-control-sm" id="inputApellidoMImputado" />
        </div>
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputSexoImputado" class="form-label text-secondary">Sexo:</label>
             <select class="form-select form-select-sm" id="inputSexoImputado">
                <option selected>Seleccionar</option>
                <option value="1">Femenino</option>
                <option value="2">Masculino</option>  
                <option value="3">Otro</option>                
            </select>
        </div>
        <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputAliasImputado" class="form-label text-secondary">Alias:</label>
            <input type="text" class="form-control form-control-sm" id="inputAliasImputado" />
        </div>


      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cerrar</button>
        <button type="button" class="btn btn-success btn-sm"> <i class="bi bi-check-lg"></i>Guardar</button>
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
            <label for="inputVictimaPersona" class="form-label text-secondary">Persona:</label>
             <select class="form-select form-select-sm" name="RadioPersonaVictima" id="inputVictimaPersona">
                <option selected>Seleccionar</option>
                <option value="Fisica">Fisica</option>
                <option value="Moral">Moral</option>                               
            </select>
        </div>
        <%--<div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputRazonVictima" class="form-label text-secondary">Razon:</label>
            <input type="text" class="form-control form-control-sm" id="inputRazonVictima" />
        </div>--%>
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3 tipoPersonaFisica ">
              <label for="inputNombreVictima" class="form-label text-secondary ">Nombres(s):</label>
              <input type="text" class="form-control form-control-sm" id="inputNombreVictima" />
          </div>
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3 tipoPersonaFisica">
              <label for="inputApellidoPVictima " class="form-label text-secondary ">Apellido paterno:</label>
              <input type="text" class="form-control form-control-sm" id="inputApellidoPVictima" />
          </div>
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3 tipoPersonaFisica">
            <label for="inputApellidoMVictima " class="form-label text-secondary ">Apellido materno:</label>
            <input type="text" class="form-control form-control-sm" id="inputApellidoMVictima" />
        </div>
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3 tipoPersonaMoral">
            <label for="inputRazonSocialVictima " class="form-label text-secondary">Razon social:</label>
            <input type="text" class="form-control form-control-sm" id="inputRazonSocialVictima" />
        </div>
          <div class="mb-4 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3 tipoPersonaFisica">
            <label for="inputSexoVictima" class="form-label text-secondary">Sexo:</label>
             <select class="form-select form-select-sm" id="inputSexoVictima">
                <option selected>Seleccionar</option>
                <option value="1">Femenino</option>
                <option value="2">Masculino</option>  
                <option value="3">Otro</option>                
            </select>
        </div>
        
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cerrar</button>
        <button type="button" class="btn btn-success btn-sm"><i class="bi bi-check-lg"></i> Guardar</button>
      </div>
    </div>
  </div>
</div>
<!-- MOdal Save Changes -->
<div class="modal fade" id="modal3" tabindex="-1" aria-labelledby="modal3Label" aria-hidden="true">
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
        <button type="button" class="btn btn-success btn-sm "><i class="bi bi-check-lg"></i> Guardar</button>
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
      <div class="modal-body">
        <div class="col-12 ">
            <label for="inputPreviewDelitos" class="form-label text-secondary">Delitos:</label>
            <p class="text-success">Robo Armado</p>
        </div>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i> Cancelar</button>
        <button type="button" class="btn btn-warning btn-sm"> <i class="bi bi-check-lg"></i>Guardar</button>
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
        <button type="button" class="btn btn-warning btn-sm"> <i class="bi bi-check-lg"></i>Guardar</button>
      </div>
    </div>
  </div>
</div>
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" />
            <input type="text" id="txtDato" placeholder="Escribe un dato" />
            <asp:Button ID="btnEnviarDato" runat="server" Text="Enviar" OnClientClick="enviarDato(); return false;" />
            <div id="resultado"></div>--%>
        

<%--<script src="../Scripts/consignaciones/RegistroIniciales.js"></script>--%>
