<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomAmparo.ascx.cs" Inherits="SIPOH.Views.CustomAmparo" %>

<h5 class="text-secondary mb-5">Registro de Amparo</h5>

<center>
    <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
        <label for="inputNuc" class="form-label text-secondary"><b>Numero de Amparo:</b></label>
        <div class="input-group">
            <input type="text" class="form-control" id="inputNuc" placeholder="0000/0000">
            <div class="input-group-append">
                <button class="btn btn-outline-primary btn-sm" type="button">
                    <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                        <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z" />
                    </svg>
                </button>

                <button class="btn btn-outline-primary btn-sm" type="button">
                    <svg style="fill:#ff0000" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 576 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M290.7 57.4L57.4 290.7c-25 25-25 65.5 0 90.5l80 80c12 12 28.3 18.7 45.3 18.7H288h9.4H512c17.7 0 32-14.3 32-32s-14.3-32-32-32H387.9L518.6 285.3c25-25 25-65.5 0-90.5L381.3 57.4c-25-25-65.5-25-90.5 0zM297.4 416H288l-105.4 0-80-80L227.3 211.3 364.7 348.7 297.4 416z"/></svg>
                </button>


            </div>
        </div>
    </div>
</center>

<br />

<div class="row">
    <label for="inputNuc" class="form-label text-secondary"><b></b></label>
    <div class="table-responsive">
        <table class="table table-striped table-hover mb-0 ">
            <thead class=" text-center ">
                <tr class="">
                    <th scope="col" class="bg-primary text-white">No. Amapro</th>
                    <th scope="col" class="bg-primary text-white">No. Interno Amparo</th>
                    <th scope="col" class="bg-primary text-white">Tipo de Amparo</th>
                    <th scope="col" class="bg-primary text-white">Juzgado de Procedencia</th>
                    <th scope="col" class="bg-primary text-white">No de Causa</th>
                    <th scope="col" class="bg-primary text-white">Quejoso</th>
                    <th scope="col" class="bg-primary text-white">Tipo Informe</th>
                </tr>
            </thead>
            <tbody class="table table-striped text-center table-sm">
                <tr>
                    <th scope="row">0000/0000</th>
                    <td class="text-secondary">0000/0000</td> 
                    <td class="text-secondary">T.AMPARO</td>
                    <td class="text-secondary">NOMBRE DEL JUZGADO RELACIONADO AL AMPARO</td>
                    <td class="text-secondary">0000/0000</td>
                    <td class="text-secondary">HERNANDEZ MARTINEZ MARCO</td>
                    <td><i class="fas fa-eye"></i></td>
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
    <div class="col-md-6 col-sm-6 col-xs-6">
        <h6 class="help-block text-muted small-font">Tipo de Solicitud: </h6>
        <select class="form-select" aria-label="Default select example">
            <option selected>Seleccione el tipo de solicitud</option>
            <option value="1">Causa</option>
            <option value="2">Quejoso</option>
            <option value="3">Sin Causa</option>
        </select>
    </div>

    <div class="mb-6 col-12 col-sm-6 col-md-6 col-lg-6 col-xl-6 col-xxl-6">
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
                    <th scope="col" class="bg-primary text-white">No. Amapro</th>
                    <th scope="col" class="bg-primary text-white">No. Interno Amparo</th>
                    <th scope="col" class="bg-primary text-white">Tipo de Amparo</th>
                    <th scope="col" class="bg-primary text-white">Juzgado de Procedencia</th>
                    <th scope="col" class="bg-primary text-white">No de Causa</th>
                    <th scope="col" class="bg-primary text-white">Quejoso</th>
                    <th scope="col" class="bg-primary text-white">Acciones</th>
                </tr>
            </thead>
            <tbody class="table table-striped text-center table-sm">
                <tr>
                    <th scope="row">0000/0000</th>
                    <td class="text-secondary"></td> 
                    <td class="text-secondary"></td>
                    <td class="text-secondary"></td>
                    <td class="text-secondary"></td>
                    <td class="text-secondary"></td>
                    <td><i class="fas fa-eye"></i></td>
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
        <h6 class="help-block text-muted small-font">Tipo de Amparo: </h6>
        <select class="form-select" aria-label="Default select example">
            <option selected>Seleccione el tipo de amparo</option>
            <option value="1">Directo</option>
            <option value="1">Indirecto</option>
        </select>
    </div>
    <div class="col-md-3 col-sm-3 col-xs-3">
        <h6 class="help-block text-muted small-font">Autoridad Federal: </h6>
        <input type="text" class="form-control" placeholder="Autoridad Federal" />
    </div>
    <div class="col-md-3 col-sm-3 col-xs-3">
        <h6 class="help-block text-muted small-font">Numero de Amparo: </h6>
        <input type="text" class="form-control" placeholder="N° de Amparo" />
    </div>

    <div class="col-md-3 col-sm-3 col-xs-3">
        <h6 class="help-block text-muted small-font">Fecha de Recepción: </h6>
        <input type="date" class="form-control" placeholder="Fecha" />
    </div>
</div>

<br />

<div class="row ">
    <div class="col-md-4 col-sm-4 col-xs-4">
        <h6 class="help-block text-muted small-font">Acto Reclamado: </h6>
        <input type="text" class="form-control" placeholder="Acto Reclamado" />
    </div>
    <div class="col-md-4 col-sm-4 col-xs-4">
        <h6 class="help-block text-muted small-font">Observaciones:</h6>
        <input type="text" class="form-control" placeholder="N° de Amparo" />
    </div>
        <div class="col-md-4 col-sm-4 col-xs-4">
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
<br />
<br />

<div class="row ">
    <div class="col-md-6 col-sm-6 col-xs-6">
        <label for="inputNuc" class="form-label text-secondary"><b>Quejoso</b></label>
        <div class="table-responsive">
            <table class="table table-striped table-hover mb-0 ">
                <thead class=" text-center ">
                    <tr class="">
                        <th scope="col" class="bg-primary text-white">Nombre</th>
                        <th scope="col" class="bg-primary text-white">Genero</th>
                        <th scope="col" class="bg-primary text-white">Acciones</th>
                    </tr>
                </thead>
                <tbody class="table table-striped text-center table-sm">
                    <tr>
                        <th scope="row">Marco Mejia Salas</th>
                        <td class="text-secondary">H</td>
                        <td><i class="fas fa-trash" style="color: red"></i></td>
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


    <div class="col-md-6 col-sm-6 col-xs-6">
        <label for="inputNuc" class="form-label text-secondary"><b>Tercer Perjudicado</b></label>
        <div class="table-responsive">
            <table class="table table-striped table-hover mb-0 ">
                <thead class=" text-center ">
                    <tr class="">
                        <th scope="col" class="bg-primary text-white">Nombre</th>
                        <th scope="col" class="bg-primary text-white">Genero</th>
                        <th scope="col" class="bg-primary text-white">Acciones</th>
                    </tr>
                </thead>
                <tbody class="table table-striped text-center table-sm">
                    <tr>
                        <th scope="row">Emmanuel Martinez Montoya</th>
                        <td class="text-secondary">H</td>
                        <td><i class="fas fa-trash" style="color: red"></i></td>
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

<br />
<br />
<br />

<div class="row ">
<div class="col-md-4 col-sm-4 col-xs-4">
    <h6 class="help-block text-muted small-font">Tipo de Informe: </h6>
    <select class="form-select" aria-label="Default select example">
        <option selected>Seleccione el tipo de amparo</option>
        <option value="1">Previo</option>
        <option value="1">Justificado</option>
        <option value="1">Otros</option>
    </select>
</div>
    <div class="col-md-4 col-sm-4 col-xs-4">
        <h6 class="help-block text-muted small-font">Descripcion Otros:</h6>
        <input type="text" class="form-control" placeholder="Descripcion" />
    </div>

    <div class="col-md-4 col-sm-4 col-xs-4">
        <h6 class="help-block text-muted small-font">Numero de Oficio: </h6>
            <div class="input-group">
            <input type="text" class="form-control" id="inputNuc" placeholder="0000/0000">
            <div class="input-group-append">
                <button class="btn btn-outline-primary btn-sm" type="button">
                    <svg style="fill:#003ea8" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z"/></svg>
                </button>
            </div>
        </div>
        </div>       
    </div>

<br />
<br />
<br />

<div class="row ">
    <div class="col-md-12 col-sm-12 col-xs-12">
        <label for="inputNuc" class="form-label text-secondary"><b></b></label>
        <div class="table-responsive">
            <table class="table table-striped table-hover mb-0 ">
                <thead class=" text-center ">
                    <tr class="">
                        <th scope="col" class="bg-primary text-white">Tipo de Informe</th>
                        <th scope="col" class="bg-primary text-white">Numero de Oficio de Informe</th>
                        <th scope="col" class="bg-primary text-white">Acciones</th>
                    </tr>
                </thead>
                <tbody class="table table-striped text-center table-sm">
                    <tr>
                        <th scope="row"></th>
                        <td class="text-secondary"></td>
                        <td></td>
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

<br />
<br />
<br />

<div class="row ">
    <div class="col-md-6 col-sm-6 col-xs-6">
        <h6 class="help-block text-muted small-font">Delitos: </h6>
        <select class="form-select" aria-label="Default select example">
            <option selected>Selecciona un Delito</option>
            <option value="1">ABUSO DE AUTORIDAD</option>
            <option value="2">ACTOS LIBIDINOSOS</option>
            <option value="3">AMENAZAS</option>
        </select>
    </div>

    <div class="col-md-6 col-sm-6 col-xs-6">
        <h6 class="help-block text-muted small-font">Subtipo: </h6>
        <select class="form-select" aria-label="Default select example">
            <option selected>Selecciona un subtipo de delito</option>
            <option value="1">ABUSO DE AUTORIDAD</option>
            <option value="2">ACTOS LIBIDINOSOS</option>
            <option value="3">AMENAZAS</option>
        </select>

        <br />
    </div>
</div>

<div class="row ">
            <div class="col-md-12 col-sm-12 col-xs-12">
                <div class="nav-item d-flex justify-content-end p-3">
                    <button type="button" class="btn btn-primary"><b>+</b></button>
                </div>
                <div class="table-responsive">

                    <table class="table table-striped table-hover mb-0 ">
                        <thead class=" text-center">
                            <tr class="">

                                <th scope="col" class="bg-primary text-white">Descripcion Delito</th>
                                <th scope="col" class="bg-primary text-white">Descripcion Subtipo</th>
                                <th scope="col" class="bg-primary text-white">Acciones</th>

                            </tr>
                        </thead>
                        <tbody class="table table-striped text-center table-sm">
                            <tr><th>ACOSO LABORAL</th>
                                <th></th>
                                <th></th>
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

<br />
<br />
<br />

<center>
    <button type="button" class="btn btn-primary">Registrar</button>
</center>