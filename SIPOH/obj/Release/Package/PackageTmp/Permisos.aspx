<%@ Page Title="" Language="C#" MasterPageFile="~/Consignacion.Master" AutoEventWireup="true" CodeBehind="Permisos.aspx.cs" Inherits="SIPOH.Permisos" %>

<asp:Content ID="Content10" ContentPlaceHolderID="ContentPermisos" runat="server">
    <link href="Content/css/Permiso.css" rel="stylesheet" />

    <div class="d-flex justify-content-between">
        <h1 style="margin-left: 5%" class="h5">Permisos <i class="fas fa-angle-right"></i><span class="text-success fw-bold">ADMINISTRADOR</span> <i class="bi bi-person-fill-lock text-secondary"></i></h1>
        <button type="button" class="btn btn-success fw-bold btn-sm mr-5 mb-1" data-bs-toggle="modal" data-bs-target="#modalCrearPerfil">Crear perfil <i class="bi bi-person-vcard-fill"></i></button>
    </div>
    <div class="m-0">
        <div class="row">
            <div class="col-md-11 col-md-11 ml-auto col-xl-11 mr-auto">
                <!-- Nav tabs -->
                <div class="card ">
                    <div class="card-header bg-white">
                        <div class="d-flex justify-content-between">

                            <div class="row g-3 ">
                                <div class="col-auto">
                                    <label for="inputMostrar" class="col-form-label">Mostrar</label>
                                </div>
                                <div class="col-auto">
                                    <select class="form-select form-select-sm" aria-label="Small select " id="cantidadFiltro">
                                        <option value="5">5</option>
                                        <option value="10">10</option>
                                        <option value="15">15</option>
                                        <option value="20">20</option>
                                    </select>
                                </div>
                                <div class="col-auto">
                                    <span id="spanHelpInline" class="form-text">Registros
                                    </span>
                                </div>
                            </div>
                            <div class="row g-3">
                                <div class="col-sm-auto col-12">
                                    <label for="inputBuscar" class="col-form-label text-secondary">Buscar</label>
                                </div>
                                <div class="col-sm-auto col-12 pr-0 mr-0">
                                    <input class="form-control form-control-sm" type="text" aria-label=".form-control-sm">
                                </div>

                            </div>
                        </div>
                        <div class="table-responsive">
                            <table class="table" id="tablaPermisos">
                                <thead class="">
                                    <tr>
                                        <th scope="col" class="d-flex justify-content-between"># <i class="bi bi-sort-down-alt text-secondary"></i></th>
                                        <th scope="col">Nombre / Area</th>
                                        <th scope="col">Icono</th>
                                        <th scope="col">Enlaces / Permisos</th>
                                        <th scope="col">Sub-Permisos</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">2001</th>
                                        <td class="fw-bold">Jefe Unidad</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi bi-journal-text</span> <i class="bi bi-journal-text"></i></td>
                                        <td class="text-secondary">
                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Agenda</li>
                                                    <li>/Busquedas</li>
                                                    <li>/PromocionesCtrl</li>
                                                </ul>
                                            </details>
                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">2011</th>
                                        <td class="fw-bold">Encargado de Causa</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi bi-body-text</span> <i class="bi bi-body-text"></i></td>
                                        <td class="text-secondary">
                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Agenda</li>
                                                    <li>/Reportes</li>
                                                    <li>/PromocionesCtrl</li>
                                                    <li>/Consultas</li>
                                                </ul>
                                            </details>
                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary">1011</th>
                                        <td class="fw-bold">Tecnico Multimedia</td>
                                        <td class="text-success form-label"><span class="text-secondary">bi-bar-chart-line-fill</span> <i class="bi bi-bar-chart-line-fill"></i></td>
                                        <td class="text-secondary d-flex">

                                            <details>
                                                <summary >Enlaces asignados</summary>
                                                <ul>
                                                    <li>/Consignaciones</li>
                                                    <li>/Agenda</li>
                                                    <li>/Promociones</li>
                                                </ul>
                                            </details>

                                        </td>
                                        <td class="text-right"><i class="bi bi-eye-fill btn btn-outline-info"></i><i class="bi bi-pen-fill btn btn-warning rounded-0" data-bs-toggle="modal" data-bs-target="#modalGuardarSubPermiso"></i><i class="bi bi-trash3-fill btn btn-danger"></i></td>
                                    </tr>
                                    <tr>
                                        <th class="text-secondary"></th>
                                        <td class="fw-bold"></td>
                                        <td class="text-success form-label">No hay mas contenido por mostrar.</td>
                                        <td class="text-center text-secondary"></td>
                                        <td class="text-center p-1"></td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                        <div class="row g-3 d-flex justify-content-end">
                            <div class="col-auto btn-group-sm pr-0">
                                <a class="btn btn-outline-secondary rounded-pill">Anterior</a>
                            </div>
                            <div class="col-auto btn-group-sm pr-0">
                                <a class="btn btn-outline-secondary">1</a>
                            </div>
                            <div class="col-auto btn-group-sm">
                                <a class="btn btn-outline-secondary rounded-pill">Siguiente</a>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>


    <%--modales--%>
    <%--Si los modales no aparecen, probablemente bootstrap cambio de version o query ha sido borrada de la pagina master o de aspx--%>
    <%--modal editar permiso--%>
    <!-- Modal Partes -->
    <div class="modal fade" id="modalGuardarSubPermiso" tabindex="1" aria-labelledby="modalGuardarSubPermisoLabel" aria-hidden="true">
        <div class="modal-dialog modal-xl">
            <div class="modal-content bg-white">
                <!-- Contenido del segundo modal -->
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <i class="bi bi-boxes text-warning subPermisosIcono pr-2 text-center"></i>
                <h5 class="modal-title fs-4 text-center">Sub-Permisos</h5>
                <div class="modal-body">
                    <div class="btn-group">
                        <button type="button" class="btn btn-success dropdown-toggle" data-bs-toggle="dropdown" aria-expanded="false">
                            Mostrar Modulo
                        </button>
                        <ul class="dropdown-menu">
                            <li><a class="dropdown-item" href="#">Consignaciones</a></li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li><a class="dropdown-item" href="#">Agenda</a></li>
                            <li>
                                <hr class="dropdown-divider">
                            </li>
                            <li><a class="dropdown-item" href="#">Promociones</a></li>

                        </ul>
                    </div>
                    <h5 class="text-success pt-4 pr-5 fw-bold text-right">Consignaciones <i class="fas fa-fw fa-landmark "></i></h5>
                    <%--tabla permiso consignaciones--%>
                    <div class="table-responsive">
                        <table class="table">
                            <thead class="">
                                <tr>
                                    <th scope="col"># </th>
                                    <th scope="col" class="d-flex justify-content-between">Modulo <i class="bi bi-sort-down-alt text-secondary"></i></th>
                                    <th scope="col">Ver</th>
                                    <th scope="col">Crear</th>
                                    <th scope="col">Actualizar</th>
                                    <th scope="col">Eliminar</th>

                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th class="text-secondary">1</th>
                                    <td class="fw-bold">Amparo</td>
                                    <td class="text-success">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckVer" checked>
                                            <label class="form-check-label" for="flexSwitchCheckVer">ON</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckCrear">
                                            <label class="form-check-label" for="flexSwitchCheckCrear">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckActualizar">
                                            <label class="form-check-label" for="flexSwitchCheckActualizar">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                            <label class="form-check-label" for="flexSwitchCheckEliminar">OFF</label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="text-secondary">2</th>
                                    <td class="fw-bold">Causa</td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckVer">
                                            <label class="form-check-label" for="flexSwitchCheckVer">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-success">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckCrear" checked>
                                            <label class="form-check-label" for="flexSwitchCheckCrear">ON</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckActualizar">
                                            <label class="form-check-label" for="flexSwitchCheckActualizar">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                            <label class="form-check-label" for="flexSwitchCheckEliminar">OFF</label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="text-secondary">3</th>
                                    <td class="fw-bold">Cupre</td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckVer">
                                            <label class="form-check-label" for="flexSwitchCheckVer">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckCrear">
                                            <label class="form-check-label" for="flexSwitchCheckCrear">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-success">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckActualizar" checked>
                                            <label class="form-check-label" for="flexSwitchCheckActualizar">ON</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                            <label class="form-check-label" for="flexSwitchCheckEliminar">OFF</label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="text-secondary">4</th>
                                    <td class="fw-bold">Exhorto</td>
                                    <td class="text-success">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckVer" checked>
                                            <label class="form-check-label" for="flexSwitchCheckVer">ON</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckCrear">
                                            <label class="form-check-label" for="flexSwitchCheckCrear">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-success">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckActualizar" checked>
                                            <label class="form-check-label" for="flexSwitchCheckActualizar">ON</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                            <label class="form-check-label" for="flexSwitchCheckEliminar">OFF</label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="text-secondary">5</th>
                                    <td class="fw-bold">Juicio Oral</td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckVer">
                                            <label class="form-check-label" for="flexSwitchCheckVer">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckCrear">
                                            <label class="form-check-label" for="flexSwitchCheckCrear">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckActualizar">
                                            <label class="form-check-label" for="flexSwitchCheckActualizar">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-success">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar" checked>
                                            <label class="form-check-label" for="flexSwitchCheckEliminar">ON</label>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>

                    <%--tabla permisos agenda--%>
                    <h5 class="text-success pt-4 pr-5 fw-bold text-right">Agenda <i class="bi bi-calendar-day-fill"></i></h5>
                    <div class="table-responsive">
                        <table class="table">
                            <thead class="">
                                <tr>
                                    <th scope="col" class="d-flex justify-content-between"># <i class="bi bi-sort-down-alt text-secondary"></i></th>
                                    <th scope="col">Modulo</th>
                                    <th scope="col">Ver</th>
                                    <th scope="col">Crear</th>
                                    <th scope="col">Actualizar</th>
                                    <th scope="col">Eliminar</th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th class="text-secondary">1</th>
                                    <td class="fw-bold">Eventos</td>
                                    <td class="text-success">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckVer" checked>
                                            <label class="form-check-label" for="flexSwitchCheckVer">ON</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckCrear">
                                            <label class="form-check-label" for="flexSwitchCheckCrear">OFF</label>
                                        </div>
                                    </td>
                                    <td class="text-success">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckActualizar" checked>
                                            <label class="form-check-label" for="flexSwitchCheckActualizar">ON</label>
                                        </div>
                                    </td>
                                    <td class="text-secondary">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckEliminar">
                                            <label class="form-check-label" for="flexSwitchCheckEliminar">OFF</label>
                                        </div>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                    <%--tabla permisos Promociones--%>
                    <h5 class="text-success pt-4 pr-5 fw-bold text-right">Promociones <i class="bi bi-calendar-day-fill"></i></h5>
                    <div class="table-responsive">
                        <table class="table">
                            <thead class="">
                                <tr>
                                    <th scope="col" class="d-flex justify-content-between"># <i class="bi bi-sort-down-alt text-secondary"></i></th>
                                    <th scope="col">Modulo</th>
                                    <th scope="col">Ver</th>



                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <th class="text-secondary">1</th>
                                    <td class="fw-bold">Consulta</td>
                                    <td class="text-success">
                                        <div class="form-check form-switch">
                                            <input class="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckVer" checked>
                                            <label class="form-check-label" for="flexSwitchCheckVer">ON</label>
                                        </div>
                                    </td>

                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal"><i class="bi bi-x-lg"></i>Cerrar</button>
                    <button type="button" class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modalEnviarCambiosPermisos"><i class="bi bi-check-lg"></i>Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <%--guardar cambios--%>
    <div class="modal fade" id="modalEnviarCambiosPermisos" aria-hidden="true" aria-labelledby="exampleModalEnviarCambiosPermisosToggleLabel" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <i class="bi bi-exclamation-diamond-fill superpermisoGuardarCambios text-warning text-center"></i>
                <h1 class="modal-title fs-5 text-center" id="exampleModalEnviarCambiosPermisosToggleLabel">¿Seguro que quiere guardar los cambios?</h1>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <button class="btn btn-secondary" data-bs-target="#modalGuardarSubPermiso" data-bs-toggle="modal">Cancelar</button>
                    <button class="btn btn-success">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    <%--Crear perfil modal--%>
    <div class="modal fade" id="modalCrearPerfil" aria-hidden="true" aria-labelledby="exampleModalCrearPerfilToggleLabel" tabindex="-1">
        <div class="modal-dialog modal-xl ">
            <div class="modal-content ">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <i class="bi bi-person-fill-add superpermisoGuardarCambios text-center text-success"></i>
                <h1 class="modal-title fs-5 text-center" id="exampleModalCrearPerfilToggleLabel">Crear Perfil</h1>
                <div class="modal-body container">
                    <div class="row">
                        <div class="col-12">
                            <label for="inputNombrePerfil" class="col-form-label">Nombre de perfil:</label>
                            <input class="form-control form-control-sm" type="text" id="inputNombrePerfil">
                        </div>
                        <div class="col-12">
                            <label for="inputTipoCircuito" class="col-form-label">Tipo de circuito:</label>
                            <input class="form-control form-control-sm" type="text" id="inputTipoCircuito">
                        </div>
                    </div>

                    <div class="col-auto mt-4 ">

                        <div class="row">
                            <i class="bi bi-bank2 text-success text-center iconoModulos"></i>
                            <h4 class="text-success mb-0 pb-0 text-center">Control</h4>
                        </div>
                        <hr class="border border-success border-1 opacity-90">
                        <div class="itemsEnlacesControl row">
                            <%--items--%>
                            <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexAgendaDefault">
                                <div class="row d-flex justify-content-between ">
                                    <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                                        <i class="bi bi-journal-bookmark-fill align-self-center p-auto"></i>
                                    </div>
                                    <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                        <h6 class="fw-bold text-black pb-0 mb-0 ">/Agenda</h6>
                                        <span class="text-secondary ml-1 ">bi bi-journal-bookmark-fill</span>
                                    </div>
                                    <div class="col-auto rounded-end d-flex text-center  ">
                                        <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexAgendaDefault">
                                    </div>
                                </div>
                            </label>
                            <%--items--%>
                            <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexConsultasDefault">
                                <div class="row d-flex justify-content-between ">
                                    <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                                        <i class="bi bi-search-heart-fill align-self-center p-auto"></i>
                                    </div>
                                    <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                        <h6 class="fw-bold text-black pb-0 mb-0 ">/Consultas</h6>
                                        <span class="text-secondary ml-1 ">bi bi-search-heart-fill</span>
                                    </div>
                                    <div class="col-auto rounded-end d-flex text-center  ">
                                        <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexConsultasDefault">
                                    </div>
                                </div>
                            </label>
                            <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexHistoricosDefault">
                                <div class="row d-flex justify-content-between ">
                                    <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                                        <i class="bi bi-hourglass-split align-self-center p-auto"></i>
                                    </div>
                                    <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                        <h6 class="fw-bold text-black pb-0 mb-0 ">/Historicos</h6>
                                        <span class="text-secondary ml-1 ">bi bi-hourglass-split</span>
                                    </div>
                                    <div class="col-auto rounded-end d-flex text-center  ">
                                        <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexHistoricosDefault">
                                    </div>
                                </div>
                            </label>
                            <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexConsignacionesDefault">
                                <div class="row d-flex justify-content-between ">
                                    <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                                        <i class="bi bi-file-earmark-person-fill align-self-center p-auto"></i>
                                    </div>
                                    <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                        <h6 class="fw-bold text-black pb-0 mb-0 ">/Consignaciones</h6>
                                        <span class="text-secondary ml-1 ">bi bi-file-earmark-person-fill</span>
                                    </div>
                                    <div class="col-auto rounded-end d-flex text-center  ">
                                        <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexConsignacionesDefault">
                                    </div>
                                </div>
                            </label>

                            <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexPromocionesDefault">
                                <div class="row d-flex justify-content-between ">
                                    <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                                        <i class="bi bi-envelope-plus-fill align-self-center p-auto"></i>
                                    </div>
                                    <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                        <h6 class="fw-bold text-black pb-0 mb-0 ">/Promociones</h6>
                                        <span class="text-secondary ml-1 ">bi bi-envelope-plus-fill</span>
                                    </div>
                                    <div class="col-auto rounded-end d-flex text-center  ">
                                        <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexPromocionesDefault">
                                    </div>
                                </div>
                            </label>
                            <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexReportesDefault">
                                <div class="row d-flex justify-content-between ">
                                    <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                                        <i class="bi bi-clipboard-check align-self-center p-auto"></i>
                                    </div>
                                    <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                        <h6 class="fw-bold text-black pb-0 mb-0 ">/Reportes</h6>
                                        <span class="text-secondary ml-1 ">bi bi-clipboard-check</span>
                                    </div>
                                    <div class="col-auto rounded-end d-flex text-center  ">
                                        <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexReportesDefault">
                                    </div>
                                </div>
                            </label>
                            <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexSolicitudCDDefault">
                                <div class="row d-flex justify-content-between ">
                                    <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                                        <i class="bi bi-floppy-fill align-self-center p-auto"></i>
                                    </div>
                                    <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                                        <h6 class="fw-bold text-black pb-0 mb-0 ">/SolicitudCD</h6>
                                        <span class="text-secondary ml-1 ">bi bi-floppy-fill</span>
                                    </div>
                                    <div class="col-auto rounded-end d-flex text-center  ">
                                        <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexSolicitudCDDefault">
                                    </div>
                                </div>
                            </label>
                            <hr class="border border-success border-1 opacity-90">
                        </div>

            <%--enlaces ejecucion--%>
            <div class="row pt-4">
                <i class="fas fa-fw fa-gavel text-success text-center w-100 iconoModulos"></i>
                <h4 class="text-success mb-0 pb-0 text-center">Ejecucion</h4>
            </div>
            <hr class="border border-success border-1 opacity-90">
            <div class="itemsEnlacesControl row">
               
                <%--items--%>
                <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexInicialesDefault">
                    <div class="row d-flex justify-content-between ">
                        <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                            <i class="bi bi-door-open-fill align-self-center p-auto"></i>
                        </div>
                        <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                            <h6 class="fw-bold text-black pb-0 mb-0 ">/Iniciales</h6>
                            <span class="text-secondary ml-1 ">bi bi-door-open-fill </span>
                        </div>
                        <div class="col-auto rounded-end d-flex text-center  ">
                            <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexInicialesDefault">
                        </div>
                    </div>
                </label><%--items--%>
                <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexPromocionesEjecucionDefault">
                    <div class="row d-flex justify-content-between ">
                        <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                            <i class="bi bi-envelope-plus-fill align-self-center p-auto"></i>
                        </div>
                        <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                            <h6 class="fw-bold text-black pb-0 mb-0 ">/Promociones</h6>
                            <span class="text-secondary ml-1 ">bi bi-envelope-plus-fill</span>
                        </div>
                        <div class="col-auto rounded-end d-flex text-center  ">
                            <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexPromocionesEjecucionDefault">
                        </div>
                    </div>
                </label>
                <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexBusquedaDefault">
                    <div class="row d-flex justify-content-between ">
                        <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                            <i class="bi bi-search-heart-fill align-self-center p-auto"></i>
                        </div>
                        <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                            <h6 class="fw-bold text-black pb-0 mb-0 ">/Busquedas</h6>
                            <span class="text-secondary ml-1 ">bi bi-search-heart-fill</span>
                        </div>
                        <div class="col-auto rounded-end d-flex text-center  ">
                            <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexBusquedaDefault">
                        </div>
                    </div>
                </label>
                <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexConsignacionesHistoricasDefault">
                    <div class="row d-flex justify-content-between ">
                        <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                            <i class="bi bi-hourglass-split align-self-center p-auto"></i>
                        </div>
                        <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                            <h6 class="fw-bold text-black pb-0 mb-0">/ConsignacionesHistoricas</h6>
                            <span class="text-secondary ml-1 ">bi bi-hourglass-split</span>
                        </div>
                        <div class="col-auto rounded-end d-flex text-center  ">
                            <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexConsignacionesHistoricasDefault">
                        </div>
                    </div>
                </label> <label class="item1 col-12 col-md-6 col-lg-4 rounded border border-success bg-light" for="flexReportesEjecucionDefault">
                    <div class="row d-flex justify-content-between ">
                        <div class="bg-success text-white col-auto d-flex text-center rounded-start">
                            <i class="bi bi-clipboard-check align-self-center p-auto"></i>
                        </div>
                        <div class="col-7 col-md-7 col-lg-8 align-self-start py-2">
                            <h6 class="fw-bold text-black pb-0 mb-0 ">/Reportes</h6>
                            <span class="text-secondary ml-1 ">bi bi-clipboard-check</span>
                        </div>
                        <div class="col-auto rounded-end d-flex text-center  ">
                            <input class="form-check-input  align-self-center" type="checkbox" value="" id="flexReportesEjecucionDefault">
                        </div>
                    </div>
                </label>
                <hr class="border border-success border-1 opacity-90">
            </div>
        </div>
    </div>
    <div class="modal-footer">
        <button class="btn btn-sm btn-secondary"><i class="bi bi-x-lg"></i>Cancelar</button>        
        <button type="button" class="btn btn-success btn-sm" data-bs-toggle="modal" data-bs-target="#modalEnviarCambiosPerfil"><i class="bi bi-check-lg"></i>Guardar</button>
    </div>
    </div>
  </div>
</div>
     <%--guardar cambios--%>
    <div class="modal fade" id="modalEnviarCambiosPerfil" aria-hidden="true" aria-labelledby="exampleModalEnviarCambiosPerfilToggleLabel" tabindex="-1">
        <div class="modal-dialog modal-dialog-centered">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <i class="bi bi-exclamation-diamond-fill superpermisoGuardarCambios text-warning text-center"></i>
                <h1 class="modal-title fs-5 text-center" id="exampleModalEnviarCambiosPerfilToggleLabel">¿Seguro que quiere guardar los cambios Perfil?</h1>
                <div class="modal-body">
                </div>
                <div class="modal-footer">
                    <a class="btn btn-secondary" data-bs-target="#modalCrearPerfil" data-bs-toggle="modal">Cancelar</a>
                    <button class="btn btn-success">Guardar</button>
                </div>
            </div>
        </div>
    </div>

    
    


    <script src="Scripts/Permisos/Permisos.js"></script>
</asp:Content>

