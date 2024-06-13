using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SIPOH
{
        public class LimpiarFormularioVictima
        {
            public void Reiniciar(Button UpVict, Button LimpVicti, Button SvVicti, TextBox APVic, TextBox AMVic, TextBox NomVic, DropDownList GeneVicti, TextBox CURPVicti,
                TextBox RFCVicti, TextBox FeNacVic, TextBox EdadVicti, DropDownList ContiNac, DropDownList PaisNac, DropDownList EstNaci, DropDownList MuniNac, DropDownList NacVicti, DropDownList HabLenExtra,
                DropDownList HablEsp, DropDownList LengIndi, DropDownList CondMigVic, DropDownList CondAlfVic, DropDownList HablLengIndi, DropDownList PuebloIndi, TextBox DomiTrabVicti,
                DropDownList EstCivil, DropDownList GradEst, DropDownList OcupaVicti, DropDownList DetaOcupaVic, DropDownList CuenDisca, DropDownList TipoDisca, DropDownList DiscaEspe, DropDownList ContiRes, DropDownList PaisRes,
                DropDownList EstaRes, DropDownList MuniRes, TextBox DomicPersonVicti, DropDownList AseJur, DropDownList ReqInter, TextBox TelCont, TextBox EmailCont, TextBox Fax, DropDownList RelacVic, TextBox HoraIndivi,
                DropDownList IDVicti, TextBox Domici, TextBox OtroMed, RadioButtonList AceptaDatos, DropDownList AsisMigra, TextBox NumId)
            {
                UpVict.Visible = true;
                LimpVicti.Visible = true;
                SvVicti.Visible = false;

                APVic.Text = String.Empty;
                AMVic.Text = String.Empty;
                NomVic.Text = String.Empty;
                GeneVicti.ClearSelection();
                CURPVicti.Text = String.Empty;
                RFCVicti.Text = String.Empty;
                FeNacVic.Text = String.Empty;
                EdadVicti.Text = String.Empty;
                ContiNac.ClearSelection();
                PaisNac.ClearSelection();
                EstNaci.ClearSelection();
                MuniNac.ClearSelection();
                NacVicti.ClearSelection();
                HabLenExtra.ClearSelection();
                HablEsp.ClearSelection();
                LengIndi.ClearSelection();
                CondMigVic.ClearSelection();
                CondAlfVic.ClearSelection();
                HablLengIndi.ClearSelection();
                PuebloIndi.ClearSelection();
                DomiTrabVicti.Text = String.Empty;
                EstCivil.ClearSelection();
                GradEst.ClearSelection();
                OcupaVicti.ClearSelection();
                DetaOcupaVic.ClearSelection();
                CuenDisca.ClearSelection();
                TipoDisca.ClearSelection();
                DiscaEspe.ClearSelection();
                ContiRes.ClearSelection();
                PaisRes.ClearSelection();
                EstaRes.ClearSelection();
                MuniRes.ClearSelection();
                DomicPersonVicti.Text = String.Empty;
                AseJur.ClearSelection();
                ReqInter.ClearSelection();
                TelCont.Text = String.Empty;
                EmailCont.Text = String.Empty;
                Fax.Text = String.Empty;
                RelacVic.ClearSelection();
                HoraIndivi.Text = String.Empty;
                IDVicti.ClearSelection();
                Domici.Text = String.Empty;
                OtroMed.Text = String.Empty;
                AceptaDatos.SelectedValue = "";
                AsisMigra.ClearSelection();
                NumId.Text = String.Empty;
            }

        }
    }
