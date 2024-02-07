<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialCHSAcusatorio.ascx.cs" Inherits="SIPOH.Views.InicialCHSAcusatorio" %>
<div class="row">
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputRadicacion" class="form-label text-secondary">Juzgado de Procedencia</label>
            <select class="form-select" id="inputRadicacion">
                <option selected>Seleccionar</option>
                <option value="1">. . .</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputIncomJuzgado" class="form-label text-secondary">Causa | Nuc</label>
            <select class="form-select" id="inputIncomJuzgado">
                <option selected>Seleccionar</option>
                <option value="1">. . .</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputNuc" class="form-label text-secondary">Numero de Causa</label>
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
<div class="row">
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
