using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace SIPOH.ExpedienteDigital.Victimas.CSVictimas
{
    public class CambioTipoVictima
    {
        public void TipoVictChanged(object sender, EventArgs e, DropDownList TipoVict, DropDownList SectorVicti, DropDownList TipoSocie, TextBox AMVic, TextBox NomVic, 
            DropDownList GeneVicti, DropDownList ClasifVicti, TextBox CURPVicti, TextBox RFCVicti, TextBox FeNacVic, TextBox EdadVicti, DropDownList ContiNac, DropDownList PaisNac, 
            DropDownList EstNaci, DropDownList MuniNac, DropDownList NacVicti, DropDownList HabLenExtra, DropDownList HablEsp, DropDownList LengIndi, DropDownList VicVulne, DropDownList CondMigVic,
            DropDownList CondAlfVic, DropDownList HablLengIndi, DropDownList PuebloIndi, TextBox DomiTrabVicti, DropDownList EstCivil, DropDownList GradEst, DropDownList OcupaVicti, DropDownList DetaOcupaVic,
            DropDownList CuenDisca, DropDownList TipoDisca, DropDownList DiscaEspe, DropDownList ContiRes, DropDownList PaisRes, DropDownList EstaRes, DropDownList MuniRes, TextBox DomicPersonVicti,
            DropDownList AseJur, DropDownList ReqInter, TextBox TelCont, TextBox EmailCont, TextBox Fax, DropDownList RelacVic, TextBox HoraIndivi, DropDownList IDVicti, TextBox Domici, TextBox OtroMed, 
            RadioButtonList AceptaDatos, DropDownList AsisVictima)
        {
            if (TipoVict.SelectedValue == "1" || TipoVict.SelectedValue == "SV")
            {
                SectorVicti.Enabled = false;
                TipoSocie.Enabled = false;
                SectorVicti.SelectedValue = "9";
                TipoSocie.SelectedValue = "9";

                AMVic.Enabled = true;
                NomVic.Enabled = true;
                GeneVicti.Enabled = true;
                ClasifVicti.Enabled = true;
                CURPVicti.Enabled = true;
                RFCVicti.Enabled = true;
                FeNacVic.Enabled = true;
                EdadVicti.Enabled = true;
                ContiNac.Enabled = true;
                PaisNac.Enabled = true;
                EstNaci.Enabled = true;
                MuniNac.Enabled = true;
                NacVicti.Enabled = true;
                HabLenExtra.Enabled = true;
                HablEsp.Enabled = true;
                LengIndi.Enabled = true;
                VicVulne.Enabled = true;
                CondMigVic.Enabled = true;
                CondAlfVic.Enabled = true;
                HablLengIndi.Enabled = true;
                PuebloIndi.Enabled = true;
                DomiTrabVicti.Enabled = true;
                EstCivil.Enabled = true;
                GradEst.Enabled = true;
                OcupaVicti.Enabled = true;
                DetaOcupaVic.Enabled = true;
                CuenDisca.Enabled = true;
                TipoDisca.Enabled = true;
                DiscaEspe.Enabled = true;
                ContiRes.Enabled = true;
                PaisRes.Enabled = true;
                EstaRes.Enabled = true;
                MuniRes.Enabled = true;
                DomicPersonVicti.Enabled = true;
                AseJur.Enabled = true;
                ReqInter.Enabled = true;
                TelCont.Enabled = true;
                EmailCont.Enabled = true;
                Fax.Enabled = true;
                RelacVic.Enabled = true;
                HoraIndivi.Enabled = true;
                IDVicti.Enabled = true;
                Domici.Enabled = true;
                OtroMed.Enabled = true;
                AceptaDatos.Enabled = true;
                AsisVictima.Enabled = true;
            }
            else
            {
                SectorVicti.Enabled = true;
                TipoSocie.Enabled = true;
                SectorVicti.SelectedValue = "S";
                TipoSocie.SelectedValue = "S";
                GeneVicti.SelectedValue = "3";

                AMVic.Enabled = false;
                NomVic.Enabled = false;
                GeneVicti.Enabled = false;
                ClasifVicti.Enabled = true;
                CURPVicti.Enabled = false;
                RFCVicti.Enabled = false;
                FeNacVic.Enabled = false;
                EdadVicti.Enabled = false;
                ContiNac.Enabled = false;
                PaisNac.Enabled = false;
                EstNaci.Enabled = false;
                MuniNac.Enabled = false;
                NacVicti.Enabled = false;
                HabLenExtra.Enabled = false;
                HablEsp.Enabled = false;
                LengIndi.Enabled = false;
                VicVulne.Enabled = false;
                CondMigVic.Enabled = false;
                CondAlfVic.Enabled = false;
                HablLengIndi.Enabled = false;
                PuebloIndi.Enabled = false;
                DomiTrabVicti.Enabled = false;
                EstCivil.Enabled = false;
                GradEst.Enabled = false;
                OcupaVicti.Enabled = false;
                DetaOcupaVic.Enabled = false;
                CuenDisca.Enabled = false;
                TipoDisca.Enabled = false;
                DiscaEspe.Enabled = false;
                ContiRes.Enabled = false;
                PaisRes.Enabled = false;
                EstaRes.Enabled = false;
                MuniRes.Enabled = false;
                DomicPersonVicti.Enabled = false;
                AseJur.Enabled = false;
                ReqInter.Enabled = false;
                TelCont.Enabled = false;
                EmailCont.Enabled = false;
                Fax.Enabled = false;
                RelacVic.Enabled = false;
                HoraIndivi.Enabled = false;
                IDVicti.Enabled = false;
                Domici.Enabled = false;
                OtroMed.Enabled = false;
                AceptaDatos.Enabled = true;
                AsisVictima.Enabled = false;
            }
        }
    }
}
