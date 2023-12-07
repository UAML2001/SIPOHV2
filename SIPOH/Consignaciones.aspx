<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="Consignaciones.aspx.cs" Inherits="SIPOH.Consignaciones" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentCausa" runat="server">
    <%--query general para consiganciones, carpeta de origen: scripts--%>
    <script src="Scripts/jquery-3.4.1.min.js"></script>
     <%--query general para consiganciones, carpeta de origen: scripts--%>
    <link href="Content/css/Consignaciones.css" rel="stylesheet" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/css/toastr.min.css" />
    <div>
        <h1 style="margin-left: 5%" class="h5" >Consignaciones <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-success fw-bold">Amparo</span> </h1>
	</div>
    
            <div class="col-md-10 ml-auto col-xl-11 mr-auto">
                <!-- Nav tabs -->
                <div class="card">
                    <div class="card-header">
                        <ul class="nav nav-tabs justify-content-center" role="tablist">
                            <li class="nav-item">
                                <a class="nav-link active  onSplash" data-toggle="tab" href="#amparo" role="tab" >
                                    Amparo
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link  onSplash" data-toggle="tab" href="#registroIniciales" role="tab">
                                    Registro de iniciales
                                </a>
                            </li>
                            
                            <li class="nav-item">
                                <a class="nav-link onSplash" data-toggle="tab" href="#exhorto" role="tab" >
                                    Exhorto
                                </a>
                            </li>

                            <li class="nav-item">
                                <a class="nav-link onSplash" data-toggle="tab" href="#juciooral" role="tab">
                                    Juicio Oral
                                </a>
                            </li>
                        </ul>
                    </div>


                    <div class="card-body mt-0 pt-0">
                        <!-- Tab panes -->
                        <div class="tab-content ">
                            <div class="tab-pane active" id="amparo" role="tabpanel">
                                <%--Importacion de Controles--%>
                                <%@ Register Src="~/Views/CustomAmparo.ascx" TagPrefix="form" TagName="CustomAmparo" %>
                                <form:CustomAmparo runat="server" ID="CustomAmparo" />
                            </div>

                            <div class="tab-pane" id="registroIniciales" role="tabpanel">
                                <%--Importacion de Controles--%>
                                <%@ Register Src="~/Views/CustomRegistroIniciales.ascx" TagPrefix="form" TagName="CustomRegistroIniciales" %>
                                <form:CustomRegistroIniciales runat="server" ID="CustomRegistroIniciales1" />
                            </div>

                            

                            <div class="tab-pane" id="exhorto" role="tabpanel">
                                <%--Importacion de Controles--%>
                                <%@ Register Src="~/Views/CustomExhorto.ascx" TagPrefix="form" TagName="CustomExhorto" %>
                                <form:CustomExhorto runat="server" ID="CustomExhorto" />
                            </div>




                            <div class="tab-pane" id="juciooral" role="tabpanel">
                                 <%--Importacion de Controles--%>
                                <%@ Register Src="~/Views/CustomJuicio.ascx" TagPrefix="form" TagName="CustomJuicio" %>

                                <form:CustomJuicio runat="server" ID="CustomJuicio" />
                            </div>


                        </div>
                    </div>



             <div class="p-4">
                        <div class="nav-item d-flex justify-content-end p-3">
                            <a class="nav-link bg-success text-white rounded mr-3"  role="tab" data-bs-toggle="modal" data-bs-target="#modal5"><i class="fas fa-plus"></i> Registrar 
                            </a>
                            <a class="nav-link bg-success text-white rounded"  role="tab" data-bs-toggle="modal" data-bs-target="#modal4"><i class="fas fa-user-plus mr-1"></i>Asignar
                            </a>
                        </div>
                 <div class="table-responsive">

                        <table class="table table-striped table-hover mb-0 ">
                            <thead class=" text-center ">
                                <tr class="">

                                    <th scope="col" class="bg-success text-white">Causa</th>
                                    <th scope="col" class="bg-success text-white">Fecha</th>
                                    <th scope="col" class="bg-success text-white">Prioridad</th>
                                    <th scope="col" class="bg-success text-white">Estatus</th>
                                    <th scope="col" class="bg-success text-white"></th>
                                    <th scope="col" class="bg-success text-white">Accion</th>

                                </tr>
                            </thead>
                            <tbody class="table table-striped text-center table-sm">
                                <tr>
                                    <th scope="row">0002/2023</th>
                                    <td class="text-secondary">7 Agosto</td>
                                    <td class="text-danger">Alta</td>
                                    <td class="text-secondary">Activo</td>
                                    <td>
                                        <input type="checkbox" id="cbox1" value="first_checkbox" class="" checked /></td>
                                    <td><i class="fas fa-pen text-warning"></i></td>
                                </tr>
                                <tr>
                                    <th scope="row">0002/2023</th>
                                    <td class="text-secondary">7 Agosto</td>
                                    <td class="text-success">Normal</td>
                                    <td class="text-secondary">Activo</td>
                                    <td>
                                        <input type="checkbox" id="cbox1" value="first_checkbox" class="" /></td>
                                    <td><i class="fas fa-pen text-warning text-hover"></i></td>
                                </tr>
                                <tr>
                                    <th scope="row"></th>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td>
                                        <input type="checkbox" id="cbox1" value="first_checkbox" class=""/></td>
                                    <td><i class="fas fa-pen text-secondary"></i></td>
                                </tr>
                            </tbody>
                        </table>
                 </div>
                        <div class="nav-item d-flex justify-content-end mt-2">
                            <a class="nav-link btn btn-outline-secondary btn-sm rounded-pill mr-1"  role="tab"><span class="fs-7">Anterior</span></a>
                            <a class="nav-link btn-secondary btn-sm rounded-circle mr-1 fs-7"><span class="fs-7">1</span></a>
                            <a class="nav-link btn btn-outline-secondary btn-sm rounded-pill" role="tab"><span class="fs-7">Siguiente</span></a>
                        </div>
                    </div>
                </div>
            </div>
            <%--modales--%>
            <!-- Modal Registrar -->
            <div class="modal fade" id="modal5" tabindex="-1" aria-labelledby="modal5Label" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <%--contenido--%>
                        <div class="modal-header">
                            <i class="bi bi-eye text-secondary fs-6 pr-2"></i>
                            <h5 class="modal-title text-secondary fs-6">Registrar</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            ...
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                            <button type="button" class="btn btn-primary btn-sm"><i class="bi bi-check-lg"></i>Guardar</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- Modal Registrar -->
            <div class="modal fade" id="modal4" tabindex="-1" aria-labelledby="modal4Label" aria-hidden="true">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content">
                        <%--contenido--%>
                        <div class="modal-header">
                            <i class="bi bi-eye text-secondary fs-6 pr-2"></i>
                            <h5 class="modal-title text-secondary fs-6">Asignar</h5>
                            <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div class="modal-body">
                            ...
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                            <button type="button" class="btn btn-primary btn-sm"><i class="bi bi-check-lg"></i>Guardar</button>
                        </div>
                    </div>
                </div>
            </div>


            

            <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
        <script src="https://cdnjs.cloudflare.com/ajax/libs/toastr.js/latest/js/toastr.min.js"></script>
            <script src="Scripts/consignaciones/Consignaciones.js"></script> 
            


</asp:Content>

