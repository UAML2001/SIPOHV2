<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CustomDespacho.ascx.cs" Inherits="SIPOH.Views.CustomDespacho" %>
<div class="container col-12">
        <div style="text-align: center; padding: 3%">
            <h3>Registrar Despacho</h3>
        </div>


        <div class="row ">
            <div class="col-md-3 col-sm-3 col-xs-3">
                <h6 class="help-block text-muted small-font">Numero de Despacho: </h6>
                <input type="text" class="form-control" placeholder="Numero" />
            </div>
            <div class="col-md-3 col-sm-3 col-xs-3">
                <h6 class="help-block text-muted small-font">Quejoso: </h6>
                <input type="text" class="form-control" placeholder="Numero" />
            </div>
            <div class="col-md-3 col-sm-3 col-xs-3">
                <h6 class="help-block text-muted small-font">No. Documento de Envío: </h6>
                <input type="text" class="form-control" placeholder="Procedencia" />
            </div>

            <div class="col-md-3 col-sm-3 col-xs-3">
                <h6 class="help-block text-muted small-font">Fecha de Recepción: </h6>
                <input type="date" class="form-control" placeholder="Fecha" />
            </div>
        </div>

        <br />
        <br />

        <div class="row">
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
            <div class="col-md-9 col-sm-9 col-xs-9">
                <h6 class="help-block text-muted small-font">Observaciones: </h6>
                <textarea style="margin-bottom: 3%" class="form-control" placeholder="Leave a comment here" id="floatingTextarea"></textarea>
            </div>
        </div>

        <br />

        <center>
        <button type="button" class="btn btn-primary">Registrar</button>
        </center>

        </div>

        <br />
        <br />
