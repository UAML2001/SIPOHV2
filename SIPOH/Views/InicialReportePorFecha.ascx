<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialReportePorFecha.ascx.cs" Inherits="SIPOH.Views.InicialReportePorFecha" %>
<div class="row">
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-3 col-xxl-3">
            <label for="inputRadicacion" class="form-label text-secondary">Formato de Reporte</label>
            <select class="form-select" id="inputRadicacion">
                <option selected>Seleccionar</option>
                <option value="1">. . .</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 col-xxl-3">
            <label for="inputIncomJuzgado" class="form-label text-secondary">Nombre de Juzgado</label>
            <select class="form-select" id="inputIncomJuzgado">
                <option selected>Seleccionar</option>
                <option value="1">. . .</option>
            </select>
        </div>
            <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 col-xxl-3">
            <label for="inputIncomJuzgado" class="form-label text-secondary">Periodo Inicial</label>
                <input type="date" class="form-control" placeholder="Fecha" />
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-3 col-xl-3 col-xxl-3">
            <label for="inputNuc" class="form-label text-secondary">Periodo Final</label>
            <div class="input-group">
                <input type="date" class="form-control" placeholder="Fecha" />
                <div class="input-group-append">
                    <button class="btn btn-outline-secondary btn-sm" type="button">
                        <svg xmlns="http://www.w3.org/2000/svg" width="12" height="12" fill="currentColor" class="bi bi-search" viewBox="0 0 16 16">
                            <path d="M11.742 10.344a6.5 6.5 0 1 0-1.397 1.398h-.001c.03.04.062.078.098.115l3.85 3.85a1 1 0 0 0 1.415-1.414l-3.85-3.85a1.007 1.007 0 0 0-.115-.1zM12 6.5a5.5 5.5 0 1 1-11 0 5.5 5.5 0 0 1 11 0z" />
                        </svg>
                    </button>
                </div>
            </div>
        </div>
</div>