<%@ Page Title="" Language="C#" MasterPageFile="~/Externo/MasterExterno.Master" AutoEventWireup="true" CodeBehind="Inicio.aspx.cs" Inherits="SIPOH.Externo.Inicio" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .masthead {
            padding-top: calc(6rem + 74px);
            padding-bottom: 6rem
        }

            .masthead .masthead-heading {
                font-size: 2.75rem;
                line-height: 2.75rem
            }

            .masthead .masthead-subheading {
                font-size: 1.25rem
            }

            .masthead .masthead-avatar {
                width: 25rem
            }

        @media (min-width:992px) {
            .masthead {
                padding-top: calc(6rem + 104px);
                padding-bottom: 6rem
            }

                .masthead .masthead-heading {
                    font-size: 4rem;
                    line-height: 3.5rem
                }

                .masthead .masthead-subheading {
                    font-size: 1.5rem
                }
        }

        .divider-custom {
            margin: 1.25rem 0 1.5rem;
            width: 100%;
            display: flex;
            justify-content: center;
            align-items: center
        }

            .divider-custom .divider-custom-line {
                width: 100%;
                max-width: 7rem;
                height: .25rem;
                background-color: dimgray;
                border-radius: 1rem;
                border-color: dimgray !important
            }

                .divider-custom .divider-custom-line:first-child {
                    margin-right: 1rem
                }

                .divider-custom .divider-custom-line:last-child {
                    margin-left: 1rem
                }

            .divider-custom .divider-custom-icon {
                color: dimgray !important;
                font-size: 2rem
            }

            .divider-custom.divider-light .divider-custom-line {
                background-color: dimgray
            }

            .divider-custom.divider-light .divider-custom-icon {
                color: dimgray !important
            }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
    <header class="masthead  text-center">
         <div class="container d-flex align-items-center flex-column">
                <!-- Masthead Avatar Image--><img class="masthead-avatar mb-5" src="../Content/img/law.png" alt="">
                <!-- Masthead Heading-->
                <h1 class="masthead-heading mb-0">Bienvenido</h1>
                <!-- Icon Divider-->
                <div class="divider-custom divider-light">
                    <div class="divider-custom-line"></div>
                    <div class="divider-custom-icon"><i class="fas fa-landmark"></i></div>
                    <div class="divider-custom-line"></div>
                </div>
                <!-- Masthead Subheading-->
                <p class="pre-wrap masthead-subheading font-weight-light mb-0">Sistema Penal Digital del Poder Judicial de Estado de Hidalgo! </p>
            </div>
    </header>

</asp:Content>
