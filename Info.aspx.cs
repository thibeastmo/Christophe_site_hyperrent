using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Reparaties : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    protected void p_Click(object sender, EventArgs e)
    {
        
    }

    protected void btnSend_Click(object sender, EventArgs e)
    {
        try //Indien er iets misloopt, dan crasht de site ni (komt er geen error voor de gebruiker) met deze try catch
        {
            //Stuur bericht naar Music Maestro
            string strBody = "Beste ,\n\n" + txtVoornaam.Text + " " + txtAchternaam.Text + " heeft een vraag gesteld via de site.\n\n" + txtBericht.Text + "\n\n- Hyperrent Site";
            string strSendTo = Constants.RECEIVING_MAIL_ADDRESS;
            string strSubject = "Vraag via site";
            Sendmail.Send(strSendTo, txtMail.Text, strSubject, strBody);

            //Bevestig de reservatie voor de klant
            strBody = "Beste " + txtVoornaam.Text + " " + txtAchternaam.Text + "\n\n Wij hebben uw aanvraag goed ontvangen.\nHet zal spoedig beantwoord worden.\n\nMet vriendelijke groeten,\n- Hyperrent";
            strSendTo = txtMail.Text;
            Sendmail.Send(strSendTo, Constants.RECEIVING_MAIL_ADDRESS, strSubject, strBody);

            //Bevestig dat het gelukt is om het bericht te versturen
            Response.Write("<script>alert('De vraag is succesvol verstuurd.');</script>");
        }
        catch
        {
            //indien er iets misliep, zeg het tegen de gebruiker.
            Response.Write("<script>alert('Er liep iets mis tijdens het versturen van de vraag. Probeer later opnieuw!');</script>");
        }

        //Maak klant variabele aan
        Klant aKlant = new Klant();
        aKlant.Voornaam = txtVoornaam.Text;
        aKlant.Achternaam = txtAchternaam.Text;
        aKlant.Mail = txtMail.Text;

        //controleer of deze is toegevoegd of niet en pas aan indien het wel toegevoegd is
        if (!DataBase.CheckIfKlantIsAdded(aKlant))
        {
            //Voeg de klant toe omdat deze nog niet toegevoegd is
            DataBase.addKlant(aKlant);
        }

        //Reset de textboxes
        txtVoornaam.Text = string.Empty;
        txtAchternaam.Text = string.Empty;
        txtMail.Text = string.Empty;
        txtBericht.Text = string.Empty;
        //txtInstrument.Text = string.Empty;
    }
}