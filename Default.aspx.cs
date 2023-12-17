using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //introdiv.InnerText = DataBase.readTekst(1).Item2;
        //p2.InnerText = DataBase.readTekst(2).Item2;

        string tekst1 = DataBase.readTekst(1).Item2;
        string tekst2 = DataBase.readTekst(2).Item2;
        if (true || Session[Constants.SESSION_GEBRUIKERSNAAM] == null || Session[Constants.SESSION_GEBRUIKERSNAAM].ToString() == "")
        { //Niemand logde in --> paragraph toevoegen
            HtmlGenericControl par1 = new HtmlGenericControl("p");
            par1.InnerText = tekst1;
            HtmlGenericControl par2 = new HtmlGenericControl("p");
            par2.InnerText = tekst2;
            //introdiv.Controls.Add(par1);
            //p2.Controls.AddAt(1, par1);
            //p2.Controls.AddAt(1, par2);

            p2.Controls.Add(par2);
            p2.Controls.Add(par1);

        }
        else
        { //Iemand heeft ingelogd --> textbox toevoegen
            TextBox txt1 = new TextBox();
            TextBox txt2 = new TextBox();
            txt1.Text = tekst1;
            txt2.Text = tekst2;
            txt1.ID = "txtParagraph1";
            txt2.ID = "txtParagraph1";
            //txt1.Attributes.Add("onchange", "DoPostBack");
            //txt2.Attributes.Add("onchange", "DoPostBack");
            //txt1.Attributes.Add("dblclick", ClientScript.GetPostBackEventReference(txt1, "dblclick"));
            //txt2.Attributes.Add("dblclick", ClientScript.GetPostBackEventReference(txt2, "dblclick"));
            introdiv.Controls.Add(txt1);
            p2.Controls.AddAt(1, txt2);
        }
        Label lblAdres1 = new Label();
        Label lblAdres2 = new Label();
        Label lblMail = new Label();
        Label lblTel = new Label();
        lblAdres1.Text = "Kronenburgstraat 62";
        lblAdres2.Text = "2000 Antwerpen";
        lblMail.Text = "info@hyperrent.be";
        lblTel.Text = "03 439 19 89";
        HtmlGenericControl div1 = new HtmlGenericControl("div");
        HtmlGenericControl div2 = new HtmlGenericControl("div");
        div2.Controls.Add(lblAdres1);
        div2.Controls.Add(lblAdres2);
        div2.Controls.Add(lblMail);
        div2.Controls.Add(lblTel);
        div1.Controls.Add(div2);
        p2.Controls.Add(div1);
    }

    protected void btnSend_Click(object sender,  EventArgs e)
    {
        try //Indien er iets misloopt, dan crasht de site ni (komt er geen error voor de gebruiker) met deze try catch
        {
            //Stuur bericht naar Music Maestro
            string strBody = "Beste ,\n\n" + txtVoornaam.Text + " " + txtAchternaam.Text + " heeft u een bericht gestuurd via de site.\n\n" + txtBericht.Text + "\n\n- Music Maestro Site";
            string strSendTo = Constants.RECEIVING_MAIL_ADDRESS;
            string strSubject = "Contactverzoek via site";
            Sendmail.Send(strSendTo, txtMail.Text, strSubject, strBody);

            //Bevestig de reservatie voor de klant
            strBody = "Beste " + txtVoornaam.Text + " " + txtAchternaam.Text + "\n\n Wij hebben uw bericht goed ontvangen.\nHet zal spoedig beantwoord  worden.\n\nMet vriendelijke groeten,\n- Hyperrent";
            strSendTo = txtMail.Text;
            Sendmail.Send(strSendTo, Constants.RECEIVING_MAIL_ADDRESS, strSubject, strBody);


            //Bevestig dat het gelukt is om het bericht te versturen
            Response.Write("<script>alert('Het bericht is succesvol verzonden.');</script>");
        }
        catch
        {
            //indien er iets misliep, zeg het tegen de gebruiker.
            Response.Write("<script>alert('Er liep iets mis tijdens het versturen van het bericht. Probeer later opnieuw!');</script>");
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
    }

    protected void ParagraphChanged(object sender, EventArgs e)
    {
        bool isOk = false;
        if (isOk)
        {
            DataBase.editTekst((sender as HtmlGenericControl).ID.Replace("p", string.Empty),
                (sender as HtmlGenericControl).InnerText);
        }
    }
}