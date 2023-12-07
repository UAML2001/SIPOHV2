<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportesControl.ascx.cs" Inherits="SIPOH.Views.ReportesControl" %>


        <div>
            <h1 style="margin-left: 5%" class="h5">Control <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold"> Reportes</span> </h1>
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
                            <h5 class="text-secondary mb-5">Reportes</h5>
                        </div>


                        <div class="row ">
                            <div class="col-md-4 col-sm-4 col-xs-4">
                                <h6 class="help-block text-muted small-font">Tipo de Repprte: </h6>
                                <select class="form-select" aria-label="Default select example">
                                    <option selected>Seleccione el tipo de reporte</option>
                                    <option value="1">Dia</option>
                                    <option value="2">Fecha</option>
                                </select>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-4">
                                <h6 class="help-block text-muted small-font">Formato de Reporte: </h6>
                                <select class="form-select" aria-label="Default select example">
                                    <option selected>Seleccione el formato del reporte</option>
                                    <option value="1">Inicial</option>
                                    <option value="2">Promociones</option>
                                </select>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-4">
                                <h6 class="help-block text-muted small-font">Promociones: </h6>
                                <select class="form-select" aria-label="Default select example">
                                    <option selected>Seleccione el tipo de promocion</option>
                                    <option value="1">Causa Penal</option>
                                    <option value="2">Cuadernillo Preliminar</option>
                                    <option value="2">Amparo</option>
                                    <option value="2">Juicio Oral</option>
                                </select>
                            </div>
                        </div>

                        <br />

                        <div class="row ">
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <h6 class="help-block text-muted small-font">De </h6>
                                <input type="date" class="form-control" placeholder="Fecha" />
                            </div>
                            <div class="col-md-6 col-sm-6 col-xs-6">
                                <h6 class="help-block text-muted small-font">A </h6>
                                <input type="date" class="form-control" placeholder="Fecha" />
                            </div>
                        </div>

                        <br />
                        <br />

                        <center>
                            <button type="button" class="btn btn-primary">
                                <svg style="fill:white" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 512 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M0 64C0 28.7 28.7 0 64 0H224V128c0 17.7 14.3 32 32 32H384V304H176c-35.3 0-64 28.7-64 64V512H64c-35.3 0-64-28.7-64-64V64zm384 64H256V0L384 128zM176 352h32c30.9 0 56 25.1 56 56s-25.1 56-56 56H192v32c0 8.8-7.2 16-16 16s-16-7.2-16-16V448 368c0-8.8 7.2-16 16-16zm32 80c13.3 0 24-10.7 24-24s-10.7-24-24-24H192v48h16zm96-80h32c26.5 0 48 21.5 48 48v64c0 26.5-21.5 48-48 48H304c-8.8 0-16-7.2-16-16V368c0-8.8 7.2-16 16-16zm32 128c8.8 0 16-7.2 16-16V400c0-8.8-7.2-16-16-16H320v96h16zm80-112c0-8.8 7.2-16 16-16h48c8.8 0 16 7.2 16 16s-7.2 16-16 16H448v32h32c8.8 0 16 7.2 16 16s-7.2 16-16 16H448v48c0 8.8-7.2 16-16 16s-16-7.2-16-16V432 368z"/></svg>
                                 Consultar Reporte</button>
                        </center>

                        <br />
                        <br />

                        <embed src="https://tutempo.com.co/materiales/pluginfile.php/2359/block_html/content/Hoja%20Random%20%28notaci%C3%B3n%20americana%29.pdf" type="application/pdf" width="100%" height="500px" />

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

