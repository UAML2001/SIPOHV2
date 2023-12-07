<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomPromocion.ascx.cs" Inherits="SIPOH.Views.CustomPromocion" %>

        <div>
            <h1 style="margin-left: 5%" class="h5">Control <i class="fas fa-angle-right"></i><span id="dataSplash" class="text-primary fw-bold"> Promociones</span> </h1>
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
                            <h5 class="text-secondary mb-5">Promocionar</h5>
                        </div>

                        <div class="row ">
                            <div class="col-md-8 col-sm-8 col-xs-8">
                                <h6 class="help-block text-muted small-font">Tipo de Documento: </h6>
                                <select id="formSelector" class="form-select" aria-label="Default select example">
                                    <option selected>Seleccione el tipo de Documento</option>
                                    <option value="Amparo">Amparo</option>
                                    <option value="Causa">Causa Penal</option>
                                    <option value="Juicio">Juicio Oral</option>
                                    <option value="Cuadernillo">Cuadernillo Preliminar</option>
                                </select>
                            </div>
                            </div>


                            <br /> 

                            <div id="Amparo" class="formulario">
                                <div class="row ">
                                <!-- Contenido del primer formulario -->
                                    <div class="col-md-8 col-sm-8 col-xs-8">
                                    <h6 class="help-block text-muted small-font">Numero de Amparo: </h6>
                                    <div class="input-group">
                                        <input type="text" class="form-control" id="inputNuc" placeholder="0000/0000">
                                        <div class="input-group-append">
                                            <button class="btn btn-outline-primary btn-sm" type="button">
                                                <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                                                    <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z" />
                                                </svg>
                                            </button>

                                            <button class="btn btn-outline-primary btn-sm" type="button">
                                                <svg style="fill: #ff0000" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 576 512">
                                                    <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                                                    <path d="M290.7 57.4L57.4 290.7c-25 25-25 65.5 0 90.5l80 80c12 12 28.3 18.7 45.3 18.7H288h9.4H512c17.7 0 32-14.3 32-32s-14.3-32-32-32H387.9L518.6 285.3c25-25 25-65.5 0-90.5L381.3 57.4c-25-25-65.5-25-90.5 0zM297.4 416H288l-105.4 0-80-80L227.3 211.3 364.7 348.7 297.4 416z" />
                                                </svg>
                                            </button>
                                        </div>
                                    </div>
                                </div>
                            </div>

                                <br />

                                <div class="card">
                                    <div class="card-body">

                                        <div class="container">
                                            <div class="row">
                                                <div class="col">
                                                    <h5><b>Autoridad Federal</b></h5>
                                                    <h6>Autoridad Federal</h6>
                                                </div>
                                                <div class="col">
                                                    <h5><b>Numero de Amparo Interno</b></h5>
                                                    <h6>Numero de Amparo Interno</h6>
                                                </div>
                                                <div class="col">
                                                    <h5><b>Quejoso</b></h5>
                                                    <h6>Quejoso</h6>
                                                </div>
                                            </div>

                                            <br />

                                            <div class="row">
                                                <div class="col">
                                                    <h5><b>Numero de Causa</b></h5>
                                                    <h6>0000/0000</h6>
                                                </div>
                                                <div class="col">
                                                    <h5><b>Tipo de Amparo</b></h5>
                                                    <h6>Directo</h6>
                                                </div>
                                                <div class="col">
                                                    <h5><b>Acto Reclamdo</b></h5>
                                                    <h6>Acto Reclamdo</h6>
                                                </div>
                                            </div>

                                            <br />
                                            
                                            <div class="row">
                                                <div class="col">
                                                    <h5><b>Numero de Amparo Externo</b></h5>
                                                    <h6>Numero de Amparo Externo</h6>
                                                </div>
                                            </div>
                                        </div>

                                        </div>
                                    </div>

                                <br />

                                <div class="row">
                                    <label for="inputNuc" class="form-label text-secondary"><b>Informes de Amparo</b></label>
                                    <div class="table-responsive">
                                        <table class="table table-striped table-hover mb-0 ">
                                            <thead class=" text-center ">
                                                <tr class="">
                                                    <th scope="col" class="bg-primary text-white">Informe</th>
                                                    <th scope="col" class="bg-primary text-white">Numero Informe</th>
                                                    <th scope="col" class="bg-primary text-white">Descripcion</th>
                                                </tr>
                                            </thead>
                                            <tbody class="table table-striped text-center table-sm">
                                                <tr>
                                                    <th scope="row">Previo</th>
                                                    <td class="text-secondary">Numero Informe</td>
                                                    <td class="text-secondary">Descripcion</td>
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

                                <div class="row ">
                                    <div class="col-md-6 col-sm-6 col-xs-6">
                                        <h6 class="help-block text-muted small-font">Fecha de Recepción: </h6>
                                        <input type="date" class="form-control" placeholder="Fecha" />
                                    </div>
                                    <div class="col-md-6 col-sm-6 col-xs-6">
                                        <h6 class="help-block text-muted small-font">Prioridad: </h6>
                                        <fieldset data-role="controlgroup"
                                            data-type="horizontal">

                                            <input type="radio" id="normal"
                                                value="on" checked="checked" />
                                            <label for="normal">Normal</label>

                                            <input type="radio"
                                                id="alta" value="off" />
                                            <label for="alta">Alta</label>
                                        </fieldset>
                                    </div>
                                </div>

                                <br />

                                <div class="row ">
                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                        <h6 class="help-block text-muted small-font">Promovente: </h6>
                                        <select class="form-select" aria-label="Default select example">
                                            <option selected>Seleccione la procedencia</option>
                                            <option value="1">MP</option>
                                            <option value="2">Defensa Publica</option>
                                            <option value="2">Defensa Privada</option>
                                            <option value="2">Asesor Juridico</option>
                                            <option value="2">Victima</option>
                                            <option value="2">Otro</option>
                                        </select>
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                        <h6 class="help-block text-muted small-font">Especificar Nombre: </h6>
                                        <input type="text" class="form-control" placeholder="Nombre" />
                                    </div>

                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                        <h6 class="help-block text-muted small-font">Numero Telefonico: </h6>
                                        <input type="text" class="form-control" placeholder="55-00-00-00-00" />
                                    </div>
                                </div>

                                <br />

                                <div class="row ">
                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                        <h6 class="help-block text-muted small-font">Nombre: </h6>
                                        <input type="text" class="form-control" placeholder="Nombre" />
                                    </div>
                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                        <h6 class="help-block text-muted small-font">Identificacion: </h6>
                                        <select class="form-select" aria-label="Default select example">
                                            <option selected>Seleccione la procedencia</option>
                                            <option value="1">Catalogo Identicacion</option>
                                        </select>
                                    </div>

                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                        <h6 class="help-block text-muted small-font">Serie: </h6>
                                        <input type="text" class="form-control" placeholder="Serie" />
                                    </div>
                                </div>

                                <br />

                                <div class="row ">
                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                        <h6 class="help-block text-muted small-font">Tipo Solicitud: </h6>
                                        <select class="form-select" aria-label="Default select example">
                                            <option selected>Seleccione la procedencia</option>
                                            <option value="1">Copias</option>
                                            <option value="1">Audiencia</option>
                                            <option value="1">Otro</option>
                                        </select>
                                    </div>

                                    <div class="col-md-8 col-sm-8 col-xs-8">
                                        <h6 class="help-block text-muted small-font">Especificar Solicitud: </h6>
                                        <input type="text" class="form-control" placeholder="Solicitud" />
                                    </div>
                                </div>

                                <br />

                                <div class="row ">
                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                        <h6 class="help-block text-muted small-font">Anexos: </h6>
                                        <select class="form-select" aria-label="Default select example">
                                            <option selected>Seleccione el Anexo</option>
                                        </select>
                                    </div>

                                    <div class="col-md-4 col-sm-4 col-xs-4">
                                        <h6 class="help-block text-muted small-font">Descripcion Otros: </h6>
                                        <input type="text" class="form-control" placeholder="Solicitud" />
                                    </div>


                                    <div class="mb-4 col-4 col-sm-4 col-md-4 col-lg-4 col-xl-4 col-xxl-4">
                                        <h6 class="help-block text-muted small-font">Cantidad: </h6>
                                        <div class="input-group">
                                            <input type="text" class="form-control" id="" placeholder="0000/0000">
                                            <div class="input-group-append">
                                                <button class="btn btn-outline-primary btn-sm" type="button">
                                                    <svg xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z"/></svg>
                                                </button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <label for="inputNuc" class="form-label text-secondary"><b></b></label>
                                    <div class="table-responsive">
                                        <table class="table table-striped table-hover mb-0 ">
                                            <thead class=" text-center ">
                                                <tr class="">
                                                    <th scope="col" class="bg-primary text-white">Descripcion</th>
                                                    <th scope="col" class="bg-primary text-white">Cantidad</th>
                                                    <th scope="col" class="bg-primary text-white">Acciones</th>
                                                </tr>
                                            </thead>
                                            <tbody class="table table-striped text-center table-sm">
                                                <tr>
                                                    <th scope="row"></th>
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

                                <br />
                                <br />


                               
                                <center>
                                    <button type="button" class="btn btn-primary">
                                        <svg style="fill:#ffffff" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H384c35.3 0 64-28.7 64-64V173.3c0-17-6.7-33.3-18.7-45.3L352 50.7C340 38.7 323.7 32 306.7 32H64zm0 96c0-17.7 14.3-32 32-32H288c17.7 0 32 14.3 32 32v64c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V128zM224 288a64 64 0 1 1 0 128 64 64 0 1 1 0-128z"/></svg>
                                        Guardar</button>
                                    <button type="button" class="btn btn-primary">
                                        <svg style="fill:#ffffff" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 512 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M367.2 412.5L99.5 144.8C77.1 176.1 64 214.5 64 256c0 106 86 192 192 192c41.5 0 79.9-13.1 111.2-35.5zm45.3-45.3C434.9 335.9 448 297.5 448 256c0-106-86-192-192-192c-41.5 0-79.9 13.1-111.2 35.5L412.5 367.2zM0 256a256 256 0 1 1 512 0A256 256 0 1 1 0 256z"/></svg>
                                        Cancelar</button>
                                </center>

                            </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>


<script>
    const formSelector = document.getElementById('formSelector');
    const formularios = document.querySelectorAll('.formulario');

    formSelector.addEventListener('change', () => {
        // Oculta todos los formularios al principio
        formularios.forEach(formulario => {
            formulario.style.display = 'none';
        });

        // Muestra el formulario seleccionado
        const selectedForm = document.getElementById(formSelector.value);
        selectedForm.style.display = 'block';
    });
</script>


<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.bundle.min.js" integrity="sha384-C6RzsynM9kWDrMNeT87bh95OGNyZPhcTNXj1NW7RuBCsyN/o0jlpcV8Qyq46cDfL" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.11.8/dist/umd/popper.min.js" integrity="sha384-I7E8VVD/ismYTF4hNIPjVp/Zjvgyol6VFvRkX/vR+Vc4jQkC+hVqc2pM8ODewa9r" crossorigin="anonymous"></script>
        <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.2/dist/js/bootstrap.min.js" integrity="sha384-BBtl+eGJRgqQAUMxJ7pMwbEyER4l1g+O15P+16Ep7Q9Q+zqX6gSbd85u4mG4QzX+" crossorigin="anonymous"></script>
            <script src="Scripts/consignaciones/Consignaciones.js"></script> 
