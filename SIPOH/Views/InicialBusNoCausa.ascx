<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialBusNoCausa.ascx.cs" Inherits="SIPOH.Views.InicialBusNoCausa" %>
<div class="row">
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputRadicacion" class="form-label text-secondary">Distrito de Procedencia</label>
            <select class="form-select" id="InputDistritoProcedencia">
                <option selected>Seleccionar</option>
                <option value="1">. . .</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputIncomJuzgado" class="form-label text-secondary">Juzgado de Procedencia</label>
            <select class="form-select" id="inputJuzgadoProcedencia">
                <option selected>Seleccionar</option>
                <option value="1">. . .</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputNuc" class="form-label text-secondary">Numero de Causa</label>
            <div class="input-group">
                <input type="text" class="form-control" id="inputNuc" placeholder="0000/0000">
                <div class="input-group-append">
                    <button class="btn btn-outline-secondary btn-sm" type="button">
                        <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                            <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z" />
                        </svg>
                    </button>
                    <button class="btn btn-outline-danger btn-sm" type="button">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-eraser-fill" viewBox="0 0 16 16">
                            <path d="M8.086 2.207a2 2 0 0 1 2.828 0l3.879 3.879a2 2 0 0 1 0 2.828l-5.5 5.5A2 2 0 0 1 7.879 15H5.12a2 2 0 0 1-1.414-.586l-2.5-2.5a2 2 0 0 1 0-2.828l6.879-6.879zm.66 11.34L3.453 8.254 1.914 9.793a1 1 0 0 0 0 1.414l2.5 2.5a1 1 0 0 0 .707.293H7.88a1 1 0 0 0 .707-.293l.16-.16z" />
                        </svg>
                    </button>
                </div>
            </div>
        </div>
</div>
<div class="row">
    <div class="table-responsive">
        <table class="table table-striped table-hover mb-0 ">
            <thead class=" text-center ">
                <tr class="">
                    <th scope="col" class="bg-primary text-white">N°Ejecución</th>
                    <th scope="col" class="bg-primary text-white">Juzgado de Ejecución</th>
                    <th scope="col" class="bg-primary text-white">Fecha Ejecución</th>
                    <th scope="col" class="bg-primary text-white">Solicitud</th>
                    <th scope="col" class="bg-primary text-white">Detalle del Solicitante</th>
                    <th scope="col" class="bg-primary text-white">Beneficiario</th>
                    <th scope="col" class="bg-primary text-white">Tipo Expediente</th>
                    <th scope="col" class="bg-primary text-white">Detalle</th>
                </tr>
            </thead>
            <tbody class="table table-striped text-center table-sm">
                <tr>
                    <th scope="row">Sin Datos</th>
                    <td class="text-secondary">Sin datos</td>
                    <td class="text-secondary">Sin datos</td>
                    <td class="text-secondary">Sin datos</td>
                    <td class="text-secondary">Sin datos</td>
                    <td class="text-secondary">Sin datos</td>
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