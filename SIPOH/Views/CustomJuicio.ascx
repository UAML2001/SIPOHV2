<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomJuicio.ascx.cs" Inherits="SIPOH.Views.CustomJuicio" %>

<center>
    <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
        <label for="inputNuc" class="form-label text-secondary"><b>Numero de Causa:</b></label>
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

</center>

<div class="row">
    <label for="inputNuc" class="form-label text-secondary"><b>Causa:</b></label>
    <div class="table-responsive">
        <table class="table table-striped table-hover mb-0 ">
            <thead class=" text-center ">
                <tr class="">
                    <th scope="col" class="bg-primary text-white">Causa|Nuc</th>
                    <th scope="col" class="bg-primary text-white">N°Juzgado</th>
                    <th scope="col" class="bg-primary text-white">Ofendido(s)</th>
                    <th scope="col" class="bg-primary text-white">Inculpado(s)</th>
                    <th scope="col" class="bg-primary text-white">Delito(s)</th>
                    <th scope="col" class="bg-primary text-white">Quitar</th>
                </tr>
            </thead>
            <tbody class="table table-striped text-center table-sm">
                <tr>
                    <th scope="row">0000/0000</th>
                    <td class="text-secondary">Sin datos</td>
                    <td class="text-secondary">Sin datos</td>
                    <td class="text-secondary">Sin datos</td>
                    <td class="text-secondary">Sin datos</td>
                    <td><i class="fas fa-pen text-warning"></i></td>
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

<div class="col-md-3 col-sm-3 col-xs-3">
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

<br />
<br />
<br />

<div class="row">
    <label for="inputNuc" class="form-label text-secondary"><b>Inculpado(s):</b></label>
    <div class="table-responsive">
        <table class="table table-striped table-hover mb-0 ">
            <thead class=" text-center ">
                <tr class="">
                    <th scope="col" class="bg-primary text-white">Nombre</th>
                    <th scope="col" class="bg-primary text-white">Genero</th>
                    <th scope="col" class="bg-primary text-white">Acciones</th>
            </thead>
            <tbody class="table table-striped text-center table-sm">
                <tr>
                    <th scope="row">0000/0000</th>
                    <td class="text-secondary">Sin datos</td>
                    <td class="text-secondary">Sin datos</td>
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
        <div class="accordion" id="accordionExample">
            <div class="accordion-item">
                <h2 class="accordion-header">
                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        Delitos Cometidos
                    </button>
                </h2>
                <div id="collapseOne" class="accordion-collapse collapse show" data-bs-parent="#accordionExample">
                    <div class="accordion-body">
                        <div class="form-check">
                            <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault" checked>
                            <label class="form-check-label" for="flexCheckDefault">
                                ABUSO DE AUTORIDAD
                            </label>
                        </div>
                        <div class="form-check">
                            <input class="form-check-input" type="checkbox" value="" id="flexCheckChecked1">
                            <label class="form-check-label" for="flexCheckChecked">
                                ACTOS LIBIDINOSOS
                            </label>
                        </div>
                        <div class="form-check">
                            <input class="form-check-input" type="checkbox" value="" id="flexCheckChecked2">
                            <label class="form-check-label" for="flexCheckChecked">
                                AMENAZAS
                            </label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="col-md-6 col-sm-6 col-xs-6">
        <div class="accordion" id="accordionExample2">
            <div class="accordion-item">
                <h2 class="accordion-header">
                    <button class="accordion-button" type="button" data-bs-toggle="collapse" data-bs-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                        Victimas Relacionadas
                    </button>
                </h2>
                <div id="collapseTwo" class="accordion-collapse collapse show" data-bs-parent="#accordionExample">
                    <div class="accordion-body">
                        <div class="form-check">
                            <input class="form-check-input" type="checkbox" value="" id="flexCheckDefault1" checked>
                            <label class="form-check-label" for="flexCheckDefault">
                                MARCO ALBERTO HERNANDEZ MEJIA
                            </label>
                        </div>
                        <div class="form-check">
                            <input class="form-check-input" type="checkbox" value="" id="flexCheckChecked3">
                            <label class="form-check-label" for="flexCheckChecked">
                                MIRIAM MONTOYA ZAMORA
                            </label>
                        </div>
                        <div class="form-check">
                            <input class="form-check-input" type="checkbox" value="" id="flexCheckChecked4">
                            <label class="form-check-label" for="flexCheckChecked">
                                PEDRO NAVA CORTES
                            </label>
                        </div>
                    </div>
                </div>
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
                    <th scope="col" class="bg-primary text-white">Nombre</th>
                    <th scope="col" class="bg-primary text-white">Delitos</th>
                    <th scope="col" class="bg-primary text-white">Victimas</th>
                    <th scope="col" class="bg-primary text-white">Acciones</th>
            </thead>
            <tbody class="table table-striped text-center table-sm">
                <tr>
                    <th scope="row">Marco Megia Salas</th>
                    <td class="text-secondary">Abuso de Autoridad</td>
                    <td class="text-secondary capitalize">MARCO ALBERTO HERNANDEZ MEJIA, MIRIAM MONTOYA ZAMORA, PEDRO NAVA CORTES</td>
                    <td class="text-secondary">Eliminar</td>
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
    <button type="button" class="btn btn-primary">Registrar</button>
</center>


<br />
<br />