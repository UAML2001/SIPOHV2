<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ConsultarControl.ascx.cs" Inherits="SIPOH.Views.ConsultarControl" %>

        <div>
            <h1 style="margin-left: 5%" class="h5">Control <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold"> Consultas</span> </h1>
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
            <h5 class="text-secondary mb-5">Consultar</h5>
        </div>


                        <div class="row ">
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <h6 class="help-block text-muted small-font">Parte: </h6>
                                <select class="form-select" aria-label="Default select example">
                                    <option selected>Seleccione el tipo de solicitud</option>
                                    <option value="1">Imputado</option>
                                    <option value="2">Victima</option>
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
    <h6 class="help-block text-muted small-font">Causa </h6>
    <div class="table-responsive">
        <table class="table table-striped table-hover mb-0 ">
            <thead class=" text-center ">
                <tr class="">
                    <th scope="col" class="bg-primary text-white">Causa | NUC</th>
                    <th scope="col" class="bg-primary text-white">No Juzgado</th>
                    <th scope="col" class="bg-primary text-white">Victima(s)</th>
                    <th scope="col" class="bg-primary text-white">Imputado(s)</th>
                    <th scope="col" class="bg-primary text-white">Delito(s)</th>
                </tr>
            </thead>
            <tbody class="table table-striped text-center table-sm">
                <tr>
                    <th scope="row"></th>
                    <td class="text-secondary"></td> 
                    <td class="text-secondary"></td>
                    <td class="text-secondary"></td>
                    <td class="text-secondary"></td>
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

