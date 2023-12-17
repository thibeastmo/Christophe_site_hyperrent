using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Autoinfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Als er geen waarde is opgeslagen in de Session dan niks doen
        if (Session[Constants.SESSION_AUTO_ID] != null)
        { //Er is wel een waarde opgeslagen
            string strAutoId = Session[Constants.SESSION_AUTO_ID].ToString(); //Slaag het op als string
            Auto anAuto = DataBase.readAuto(strAutoId); //Maak lesson variabele aan en inneens met de juiste waardes vanuit de database
            //Stel de juiste dingen in voor de textboxes en img en p (paragraph)
            imgauto.ImageUrl = "../images/uploaded/" + anAuto.Afbeelding;
            pauto.InnerText = string.Empty;
            foreach (var property in anAuto.GetType().GetProperties())
            {
                object valueObject = property.GetValue(anAuto, null);
                if (valueObject != null)
                {
                    string value = valueObject.ToString();

                    bool createNamelbl = true;
                    switch (property.Name.ToLower())
                    {
                        case "naam":
                            HtmlGenericControl autoNameControl = new HtmlGenericControl("h3");
                            autoNameControl.InnerText = value;
                            rautoinfoinnerdiv.Controls.AddAt(0, autoNameControl);
                            createNamelbl = false;
                            break;
                        case "afbeelding": createNamelbl = false; break;
                        case "id": createNamelbl = false; break;
                        default:
                            string prefix = string.Empty;
                            string suffix = string.Empty;
                            switch (property.Name.ToLower())
                            {
                                case "categorie": createNamelbl = false; break;
                                case "prijs": prefix = "€"; value = Helper.GetPriceAsString(value); createNamelbl = false; break;
                                case "cilinderinhoud": suffix = "cm³"; break;
                                case "vermogen": suffix = "PK"; break;
                                case "koppel": suffix = "Nm"; break;
                                case "versnellingen": suffix = "PDK"; break;
                                case "topsnelheid": suffix = "km/h"; break;
                                case "acceleratie": suffix = "sec"; break;
                                case "tussenacceleratie": suffix = "sec"; break;
                            }
                            HtmlGenericControl propertyDiv = new HtmlGenericControl("div");
                            if (createNamelbl)
                            {
                                value = Helper.GetPriceAsString(value);
                                Label lblProperty = new Label();
                                lblProperty.Text = property.Name;
                                lblProperty.ID = "lbl" + property.Name;
                                propertyDiv.Controls.Add(lblProperty);
                            }
                            Label lbl = new Label();
                            lbl.Text = prefix + value + (suffix.Length > 0 ? " " : string.Empty) + suffix;
                            lbl.ID = "lbl" + property.Name;
                            autospecs.Controls.Add(lbl);
                            propertyDiv.Controls.AddAt(0, lbl);
                            autospecs.Controls.Add(propertyDiv);
                            break;
                    }
                }
            }
        }
    }
    protected void btnInschrijven_Click(object sender, EventArgs e)
    {
        try //Indien er iets misloopt, dan crasht de site ni (komt er geen error voor de gebruiker) met deze try catch
        {
            //Stuur bericht naar Music Maestro
            Auto auto = DataBase.readAuto(Session[Constants.SESSION_AUTO_ID].ToString());
            string strBody = "Beste Christophe," +
                "\n\n" + txtVoornaam.Text + " " + txtAchternaam.Text + " zou graag een '" + auto.Naam + "' wilen reserveren." +
                "\nPrijs: " + auto.Prijs.ToString() + "" +
                "\n\n- Hyperrent";
            string strSendTo = Constants.RECEIVING_MAIL_ADDRESS;
            string strSubject = "Verzoek auto reserveren";
            Sendmail.Send(strSendTo, txtMail.Text, strSubject, strBody);

            //Bevestig de reservatie voor de klant
            strBody = "Beste " + txtVoornaam.Text + " " + txtAchternaam.Text + "\n\n Wij hebben uw reservatie goed ontvangen.\nDe auto dat u gereserveerd heeft is een '" + auto.Naam + "' met als prijs €" + auto.Prijs.ToString() + "\n" +
                "U kan betalen bij ontvangst of overschrijven.\n\n- Hyperrent";
            strSendTo = txtMail.Text;
            Sendmail.Send(strSendTo, Constants.RECEIVING_MAIL_ADDRESS, strSubject, strBody);

            //Bevestig dat het gelukt is om het bericht te versturen
            Response.Write("<script>alert('De reservatie is succesvol aangevraagd.');</script>");
        }
        catch
        {
            //indien er iets misliep, zeg het tegen de gebruiker.
            Response.Write("<script>alert('Er liep iets mis tijdens het reserveren van de auto. Probeer later opnieuw!');</script>");
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

        //Reset de pagina
        btnInschrijven.BackColor = Color.Green;
        txtVoornaam.Text = string.Empty;
        txtAchternaam.Text = string.Empty;
        txtMail.Text = string.Empty;
    }
}