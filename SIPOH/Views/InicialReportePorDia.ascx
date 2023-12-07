<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="InicialReportePorDia.ascx.cs" Inherits="SIPOH.Views.InicialReportePorDia" %>
<div class="row">
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputRadicacion" class="form-label text-secondary">Formato de Reporte</label>
            <select class="form-select" id="inputRadicacion">
                <option selected>Seleccionar</option>
                <option value="1">Iniciales</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputIncomJuzgado" class="form-label text-secondary">Nombre de Juzgado</label>
            <select class="form-select" id="inputIncomJuzgado">
                <option selected>Seleccionar</option>
                <option value="1">. . .</option>
            </select>
        </div>
        <div class="mb-5 col-12 col-sm-6 col-md-6 col-lg-4 col-xl-4 col-xxl-4">
            <label for="inputNuc" class="form-label text-secondary">Fecha de Hoy</label>
            <div class="input-group">
                <input type="date" class="form-control" placeholder="Fecha" />
                <div class="input-group-append">
                    <button class="btn btn-outline-success btn-sm" type="button">
                        <svg xmlns="http://www.w3.org/2000/svg" width="16" height="16" fill="currentColor" class="bi bi-save" viewBox="0 0 16 16">
                          <path d="M2 1a1 1 0 0 0-1 1v12a1 1 0 0 0 1 1h12a1 1 0 0 0 1-1V2a1 1 0 0 0-1-1H9.5a1 1 0 0 0-1 1v7.293l2.646-2.647a.5.5 0 0 1 .708.708l-3.5 3.5a.5.5 0 0 1-.708 0l-3.5-3.5a.5.5 0 1 1 .708-.708L7.5 9.293V2a2 2 0 0 1 2-2H14a2 2 0 0 1 2 2v12a2 2 0 0 1-2 2H2a2 2 0 0 1-2-2V2a2 2 0 0 1 2-2h2.5a.5.5 0 0 1 0 1H2z"/>
                        </svg>
                    </button>
                </div>
            </div>
        </div>
</div>