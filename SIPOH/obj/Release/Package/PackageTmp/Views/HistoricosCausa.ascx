<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HistoricosCausa.ascx.cs" Inherits="SIPOH.Views.HistoricosCausa" %>

<div class="container col-12">
    <div style="text-align: center; padding: 3%">
        <h3>Registrar Causa</h3>
    </div>


    <div class="row ">
        <div class="col-md-3 col-sm-3 col-xs-3">
            <h6 class="help-block text-muted small-font">NUC: </h6>
            <input type="text" class="form-control" placeholder="Ingrese el NUC" />
        </div>
        <div class="col-md-3 col-sm-3 col-xs-3">
            <h6 class="help-block text-muted small-font">Tipo de Solicitud: </h6>
            <select class="form-select" id="miDropdown" aria-label="Default select example">
                <option selected>Seleccione el tipo de documento</option>
                <option value="habilitar">Innominada</option>
                <option value="deshabilitar">Catalogo Audiencia</option>
            </select>
        </div>
        <div class="col-md-3 col-sm-3 col-xs-3">
            <h6 class="help-block text-muted small-font" id="miInput">Descripcion Innominada: </h6>
            <input type="text" class="form-control" placeholder="Innominada" disabled />
        </div>

        <div class="col-md-3 col-sm-3 col-xs-3">
            <h6 class="help-block text-muted small-font">Fecha de Recepción: </h6>
            <input type="date" class="form-control" placeholder="Fecha" />
        </div>
    </div>

    <br />

        <div class="row ">
            <div class="col-md-3 col-sm-3 col-xs-3">
                <h6 class="help-block text-muted small-font" id="miInput">Numero de Fojas: </h6>
                <div class="form-outline">
                    <input value="0" type="number" id="typeNumber" class="form-control" />
                </div>
            </div>
            <div class="col-md-3 col-sm-3 col-xs-3">
                <h6 class="help-block text-muted small-font">Quien Ingresa: </h6>
                <fieldset data-role="controlgroup"
                    data-type="horizontal">

                    <input type="radio" id="mp"
                        value="on" checked="checked" />
                    <label for="normal">MP</label>

                    <input type="radio"
                        id="particular" value="off" />
                    <label for="alta">Particular</label>
                </fieldset>
            </div>

            <div class="col-md-3 col-sm-3 col-xs-3">
            <h6 class="help-block text-muted small-font">Nombre MP: </h6>
            <select class="form-select" id="miDropdown" aria-label="Default select example">
                <option selected>Seleccione el tipo de documento</option>
                <option value="habilitar">catalogo MP</option>
                <option value="deshabilitar">Otro</option>
            </select>
        </div>

        <div class="col-md-3 col-sm-3 col-xs-3">
            <h6 class="help-block text-muted small-font">Especifique Nombre: </h6>
            <input type="text" class="form-control" placeholder="Ingrese el NUC" />
        </div>
    </div>

    <br />

    <div class="row ">
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

        <div class="col-md-6 col-sm-6 col-xs-6">
            <h6 class="help-block text-muted small-font">Tipo de Redicacion: </h6>
            <fieldset data-role="controlgroup"
                data-type="horizontal">

                <input type="radio" id="sin"
                    value="on" checked="checked" />
                <label for="normal">Sin detenido</label>

                <input type="radio"
                    id="con" value="off" />
                <label for="alta">Con detenido</label>
            </fieldset>
        </div>
    </div>
</div>

<br />
<br />

<div class="row">
    <div class="col-md-6 col-sm-6 col-xs-6">
        <label for="inputNuc" class="form-label text-secondary"><b>Imputado: </b></label>
        <div class="nav-item d-flex justify-content-end p-3">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"><b>+</b></button>
        </div>
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
                        <th scope="row">Carlos Perez</th>
                        <td class="text-secondary">H</td>
                        <td class="text-secondary">
                            <svg style="fill: #ff0000" xmlns="http://www.w3.org/2000/svg" height="1.5em" viewBox="0 0 448 512">
                                <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                                <path d="M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z" /></svg></td>
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
        <label for="inputNuc" class="form-label text-secondary"><b>Victima: </b></label>
        <div class="nav-item d-flex justify-content-end p-3">
            <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#exampleModal"><b>+</b></button>
        </div>
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
                        <th scope="row">Tecnova S.A de C.V</th>
                        <td class="text-secondary">*</td>
                        <td class="text-secondary">
                            <svg style="fill: #ff0000" xmlns="http://www.w3.org/2000/svg" height="1.5em" viewBox="0 0 448 512">
                                <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                                <path d="M135.2 17.7L128 32H32C14.3 32 0 46.3 0 64S14.3 96 32 96H416c17.7 0 32-14.3 32-32s-14.3-32-32-32H320l-7.2-14.3C307.4 6.8 296.3 0 284.2 0H163.8c-12.1 0-23.2 6.8-28.6 17.7zM416 128H32L53.2 467c1.6 25.3 22.6 45 47.9 45H346.9c25.3 0 46.3-19.7 47.9-45L416 128z" /></svg></td>
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
<br />

<div class="row">
    <div class="col-md-6 col-sm-6 col-xs-6">
        <div class="row">
            <div class="col-md-6 col-sm-6 col-xs-6">
                <h6 class="help-block text-muted small-font">Delito: </h6>
                <select class="form-select" aria-label="Default select example">
                    <option selected>Selecciona un subtipo de delito</option>
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
                <div class="input-group-append">
                    <button class="btn btn-outline-primary btn-sm" type="button">
                        <svg xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512">
                            <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                            <path d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z" />
                        </svg>
                    </button>
                </div>
            </div>
        </div>
        <label for="inputNuc" class="form-label text-secondary"><b></b></label>
        <div class="table-responsive">
            <table class="table table-striped table-hover mb-0 ">
                <thead class=" text-center ">
                    <tr class="">
                        <th scope="col" class="bg-primary text-white">Descripcion Delito</th>
                        <th scope="col" class="bg-primary text-white">Descripcion Subtipo</th>
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




    <div class="col-md-6 col-sm-6 col-xs-6">
        <div class="row">
            <div class="col-md-4 col-sm-4 col-xs-4">
                <h6 class="help-block text-muted small-font">Anexo: </h6>
                <select class="form-select" aria-label="Default select example">
                    <option selected>Selecciona un subtipo de delito</option>
                    <option value="1"></option>
                    <option value="2"></option>
                    <option value="3"></option>
                </select>
            </div>
            <div class="col-md-4 col-sm-4 col-xs-4">
                <h6 class="help-block text-muted small-font">Descripcion Otros: </h6>
                <input type="text" class="form-control" placeholder="Descripcion" />
            </div>
            <div class="col-md-4 col-sm-4 col-xs-4">
                <h6 class="help-block text-muted small-font">Cantidad: </h6>
                <div class="form-outline">
                    <input value="0" type="number" id="typeNumber" class="form-control" />
                </div>
                <div class="input-group-append">
                    <button class="btn btn-outline-primary btn-sm" type="button">
                        <svg xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512">
                            <!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. -->
                            <path d="M256 80c0-17.7-14.3-32-32-32s-32 14.3-32 32V224H48c-17.7 0-32 14.3-32 32s14.3 32 32 32H192V432c0 17.7 14.3 32 32 32s32-14.3 32-32V288H400c17.7 0 32-14.3 32-32s-14.3-32-32-32H256V80z" />
                        </svg>
                    </button>
                </div>
            </div>
        </div>
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


    <center>
        <button type="button" style="margin-top:5%" class="btn btn-primary"><svg style="fill:#ffffff" xmlns="http://www.w3.org/2000/svg" height="1em" viewBox="0 0 448 512"><!--! Font Awesome Free 6.4.2 by @fontawesome - https://fontawesome.com License - https://fontawesome.com/license (Commercial License) Copyright 2023 Fonticons, Inc. --><path d="M64 32C28.7 32 0 60.7 0 96V416c0 35.3 28.7 64 64 64H384c35.3 0 64-28.7 64-64V173.3c0-17-6.7-33.3-18.7-45.3L352 50.7C340 38.7 323.7 32 306.7 32H64zm0 96c0-17.7 14.3-32 32-32H288c17.7 0 32 14.3 32 32v64c0 17.7-14.3 32-32 32H96c-17.7 0-32-14.3-32-32V128zM224 288a64 64 0 1 1 0 128 64 64 0 1 1 0-128z"/></svg> Registrar</button>
    </center>
</div>