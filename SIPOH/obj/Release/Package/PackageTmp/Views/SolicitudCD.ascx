<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SolicitudCD.ascx.cs" Inherits="SIPOH.Views.SolicitudCD" %>

        <div>
            <h1 style="margin-left: 5%" class="h5">Control <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold">Solicitud de CDs</span> </h1>
        </div>

<link href="Content/css/Consignaciones.css" rel="stylesheet" />
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />

<div class="m-0">
    <div class="row">
        <div class="col-md-10 ml-auto col-xl-11 mr-auto">
            <!-- Nav tabs -->
            <div class="card">

                <div class="card-body">
                    <div class="container col-12">
                        <div style="padding: 2%">
                            <h5 class="text-secondary mb-5">Solicitud de CDs</h5>
                        </div>


                        <div class="row ">
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <h6 class="help-block text-muted small-font">Tipo de Solicitud: </h6>
                                <select class="form-select" aria-label="Default select example">
                                    <option selected>Seleccione el tipo de solicitud</option>
                                    <option value="1">Causa Penal</option>
                                    <option value="2">Juicio</option>
                                    <option value="3">Cuadernillo Provisional</option>
                                </select>
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <h6 class="help-block text-muted small-font">Numero de Causa: </h6>
                                <div class="input-group">
                                    <input type="text" class="form-control" id="inputNuc" placeholder="0000/0000">
                                    <div class="input-group-append">
                                        <button class="btn btn-outline-primary btn-sm" type="button">
                                            <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                                                <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z" />
                                            </svg>
                                        </button>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <br />
                        <br />
                        <br />


                        <div class="row">
                            <label for="inputNuc" class="form-label text-secondary"><b></b></label>
                            <div class="table-responsive">
                                <table class="table table-striped table-hover mb-0 ">
                                    <thead class=" text-center ">
                                        <tr class="">
                                            <th scope="col" class="bg-primary text-white">Audiencia</th>
                                            <th scope="col" class="bg-primary text-white">Fecha Audiencia</th>
                                            <th scope="col" class="bg-primary text-white">
                                                <svg style="fill: #ffffff" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512">
                                                    <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                                                    <path d="M64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H384c35.3 0 64-28.7 64-64V96c0-35.3-28.7-64-64-64H64zM337 209L209 337c-9.4 9.4-24.6 9.4-33.9 0l-64-64c-9.4-9.4-9.4-24.6 0-33.9s24.6-9.4 33.9 0l47 47L303 175c9.4-9.4 24.6-9.4 33.9 0s9.4 24.6 0 33.9z" />
                                                </svg>
                                            </th>
                                            <th scope="col" class="bg-primary text-white">Autorizacion</th>
                                        </tr>
                                    </thead>
                                    <tbody class="table table-striped text-center table-sm">
                                        <tr>
                                            <th scope="row">Audiencia Inicial</th>
                                            <td class="text-secondary">11/11/2019</td>
                                            <td class="text-secondary">
                                                <center>
                                                    <div class="form-check">
                                                        <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault">
                                                    </div>
                                                </center>
                                            </td>
                                            <td class="text-secondary">
                                                <svg xmlns="http://www.w3.org/2000/svg" height="2em" viewBox="0 0 576 512">
                                                    <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                                                    <path d="M288 32c-80.8 0-145.5 36.8-192.6 80.6C48.6 156 17.3 208 2.5 243.7c-3.3 7.9-3.3 16.7 0 24.6C17.3 304 48.6 356 95.4 399.4C142.5 443.2 207.2 480 288 480s145.5-36.8 192.6-80.6c46.8-43.5 78.1-95.4 93-131.1c3.3-7.9 3.3-16.7 0-24.6c-14.9-35.7-46.2-87.7-93-131.1C433.5 68.8 368.8 32 288 32zM144 256a144 144 0 1 1 288 0 144 144 0 1 1 -288 0zm144-64c0 35.3-28.7 64-64 64c-7.1 0-13.9-1.2-20.3-3.3c-5.5-1.8-11.9 1.6-11.7 7.4c.3 6.9 1.3 13.8 3.2 20.7c13.7 51.2 66.4 81.6 117.6 67.9s81.6-66.4 67.9-117.6c-11.1-41.5-47.8-69.4-88.6-71.1c-5.8-.2-9.2 6.1-7.4 11.7c2.1 6.4 3.3 13.2 3.3 20.3z" />
                                                </svg></td>
                                        </tr>

                                        <tr>
                                            <th scope="row">Audiencia Continuidad</th>
                                            <td class="text-secondary">11/12/2019</td>
                                            <td class="text-secondary">
                                                <center>
                                                    <div class="form-check">
                                                        <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault">
                                                    </div>
                                                </center>
                                            </td>
                                            <td class="text-secondary">
                                                <svg xmlns="http://www.w3.org/2000/svg" height="2em" viewBox="0 0 576 512">
                                                    <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                                                    <path d="M288 32c-80.8 0-145.5 36.8-192.6 80.6C48.6 156 17.3 208 2.5 243.7c-3.3 7.9-3.3 16.7 0 24.6C17.3 304 48.6 356 95.4 399.4C142.5 443.2 207.2 480 288 480s145.5-36.8 192.6-80.6c46.8-43.5 78.1-95.4 93-131.1c3.3-7.9 3.3-16.7 0-24.6c-14.9-35.7-46.2-87.7-93-131.1C433.5 68.8 368.8 32 288 32zM144 256a144 144 0 1 1 288 0 144 144 0 1 1 -288 0zm144-64c0 35.3-28.7 64-64 64c-7.1 0-13.9-1.2-20.3-3.3c-5.5-1.8-11.9 1.6-11.7 7.4c.3 6.9 1.3 13.8 3.2 20.7c13.7 51.2 66.4 81.6 117.6 67.9s81.6-66.4 67.9-117.6c-11.1-41.5-47.8-69.4-88.6-71.1c-5.8-.2-9.2 6.1-7.4 11.7c2.1 6.4 3.3 13.2 3.3 20.3z" />
                                                </svg></td>
                                        </tr>


                                    </tbody>
                                </table>
                            </div>
                            <div class="nav-item d-flex justify-content-end mt-2">
                                <a class="nav-link btn btn-outline-secondary btn-sm rounded-pill mr-1" role="tab"><span class="fs-7">Anterior</span></a>
                                <a class="nav-link btn-secondary btn-sm rounded-circle mr-1 fs-7"><span class="fs-7">1</span></a>
                                <a class="nav-link btn btn-outline-secondary btn-sm rounded-pill" role="tab"><span class="fs-7">Siguiente</span></a>
                            </div>
                        </div>


                        <br />
                        <br />
                        <br />

                        <div class="row ">
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <h6 class="help-block text-muted small-font">Quien Solicita: </h6>
                                <select class="form-select" aria-label="Default select example">
                                    <option selected>Seleccione quien solicita</option>
                                    <option value="1">MP</option>
                                    <option value="2">Defensa Publica</option>
                                    <option value="3">Defensa Privada</option>
                                    <option value="4">Asesor Juridico</option>
                                    <option value="5">Victima</option>
                                    <option value="6">Otro</option>
                                </select>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <h6 class="help-block text-muted small-font">Solicitante </h6>
                                <input type="text" class="form-control" placeholder="Solicitante" />
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <h6 class="help-block text-muted small-font">Identificacion</h6>
                                <input type="text" class="form-control" placeholder="Identificacion" />
                            </div>

                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <h6 class="help-block text-muted small-font">Serie </h6>
                                <input type="text" class="form-control" placeholder="Serie" />
                            </div>
                        </div>

                        <br />

                        <div class="row ">
                            
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <h6 class="help-block text-muted small-font">Número de Copias </h6>
                                <div class="form-outline">
                                    <input value="0" type="number" id="typeNumber" class="form-control" />
                                </div>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <h6 class="help-block text-muted small-font">Tipo de Copia </h6>
                                <select class="form-select" aria-label="Default select example">
                                    <option selected>Seleccione el tipo de copia</option>
                                    <option value="1">Simple</option>
                                    <option value="2">Certificada</option>
                                </select>
                            </div>
                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <h6 class="help-block text-muted small-font">Numero de Audiencia </h6>
                                <input type="text" class="form-control" placeholder="No de Audiencia" />
                            </div>

                            <div class="col-md-3 col-sm-3 col-xs-3">
                                <h6 class="help-block text-muted small-font">Fecha de Recepcion </h6>
                                <input type="date" class="form-control" placeholder="Serie" />
                            </div>
                        </div>

                        <br />
                        <br />

                        <center>
                            <button type="button" class="btn btn-primary">Registrar</button>
                        </center>



                    </div>
                </div>
            </div>
        </div>
    </div>
</div>



<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
            <script src="Scripts/consignaciones/Consignaciones.js"></script> 

