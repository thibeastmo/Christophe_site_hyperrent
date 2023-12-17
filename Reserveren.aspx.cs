using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Kopen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strCommand = "SELECT id FROM tblinstrumenten;";
        List<string> instrumentID = DataBase.readColumn(strCommand, "id"); //Slaag de id's op van de instrumenten uit de database
                                                                           //ddlInstrumenten.Items.Clear(); //Reset de dropdownlist onderaan de pagina om de previews te zien
        foreach (string id in instrumentID) //Voor elke id in de idlijst
        {
            //Creeer een nieuwe instrumentvariabele 
            Auto newAuto = DataBase.readAuto(id);
            //Voeg instrument toe aan pagina
            CreateInstrument(newAuto);
            //Voeg instrument toe aan dropdownlist
            //ddlInstrumenten.Items.Add(new ListItem(newAuto.Naam, id));
        }
    }
    private void CreateInstrument(Auto newAuto)
    {
        //Puur code toevoegen aan pagina, als variabele opgeslagen
        //HtmlGenericControl div = new HtmlGenericControl("div");
        LinkButton div = new LinkButton();
        HtmlGenericControl img = new HtmlGenericControl("img");
        HtmlGenericControl span = new HtmlGenericControl("span");
        HtmlGenericControl name = new HtmlGenericControl("h4");
        HtmlGenericControl prise = new HtmlGenericControl("h4");

        //Stel de juiste dingen in voor elke variabele
        img.Attributes.Add("src", "images/uploaded/" + newAuto.Afbeelding);
        span.InnerHtml = "─────";
        name.InnerHtml = newAuto.Naam;
        prise.InnerHtml = "€" + GetPriceAsString(newAuto.Prijs);
        div.ID = "btnAuto" + newAuto.ID;
        div.Click += new EventHandler(Div_Click);
        div.Text = div.ID;

        //Voeg de variabele toe aan de hoofddiv van een instrument
        div.Controls.Add(img);
        div.Controls.Add(span);
        div.Controls.Add(name);
        div.Controls.Add(prise);
        //Controleer in welke categorie het hoord
        switch (newAuto.Categorie)
        {
            case "Porsche": //Indien het een snaarinstrument is, voeg het bij de snaarinstrumentendiv toe
                dvPorsche.Controls.Add(div);
                break;
            case "Ferrari": //Indien het een Blaasinstrument is, voeg het bij de Blaasinstrumentdiv toe
                dvFerrari.Controls.Add(div);
                break;
            case "Lamborghini": //Indien het een Slaginstrument is, voeg het bij de Slaginstrumentdiv toe
                dvLamborghini.Controls.Add(div);
                break;
        }
    }

    protected void Div_Click(object sender, EventArgs e)
    {
        Session[Constants.SESSION_AUTO_ID] = (sender as LinkButton).ID.Replace("btnAuto", ""); //(sender as Button) --> sender variabele = de knop die ingedrukt wordt --> daarvan het ID --> enkel het cijfer van het ID door btn te verwijderen uit het id
        Response.Redirect("Autoinfo.aspx"); //Ga naar pagina Lesinfo.aspx
    }

    //protected void ddlInstrumenten_SelectedIndexChanged(object sender, EventArgs e)
    //{ //Als er een item geselecteerd is in de dropdownlist
    //    //Maak een instrumentvariabele aan
    //    Auto anAuto = DataBase.readAuto((sender as DropDownList).SelectedValue); //Slaag het inneens op als het juiste instrument vanuit de database
    //    //Stel de juiste dingen in voor de preview
    //    imgPreview.ImageUrl = "../images/uploaded/" + anAuto.Afbeelding;
    //    lblPrijs.Text = "Prijs: €" + GetPriceAsString(anAuto.Prijs);
    //}
    //protected void btnReserveren_Click(object sender, EventArgs e)
    //{
    //    try //Indien er iets misloopt, dan crasht de site ni (komt er geen error voor de gebruiker) met deze try catch
    //    {
    //        //Stuur bericht naar Music Maestro
    //        Auto auto = DataBase.readAuto(ddlInstrumenten.SelectedValue);
    //        string strBody = "Beste Christophe," +
    //            "\n\n" + txtVoornaam.Text + " " + txtAchternaam.Text + " zou graag een '" + auto.Naam + "' wilen reserveren." +
    //            "\nPrijs: " + auto.Prijs.ToString() + "" +
    //            "\n\n- Hyperrent";
    //        string strSendTo = Constants.RECEIVING_MAIL_ADDRESS;
    //        string strSubject = "Verzoek auto reserveren";
    //        Sendmail.Send(strSendTo, txtMail.Text, strSubject, strBody);

    //        //Bevestig de reservatie voor de klant
    //        strBody = "Beste " + txtVoornaam.Text + " " + txtAchternaam.Text + "\n\n Wij hebben uw reservatie goed ontvangen.\nDe auto dat u gereserveerd heeft is een '" + auto.Naam + "' met als prijs €" + auto.Prijs.ToString() + "\n" +
    //            "U kan betalen bij ontvangst of overschrijven.\n\n- Hyperrent";
    //        strSendTo = txtMail.Text;
    //        Sendmail.Send(strSendTo, Constants.RECEIVING_MAIL_ADDRESS, strSubject, strBody);

    //        //Bevestig dat het gelukt is om het bericht te versturen
    //        Response.Write("<script>alert('De reservatie is succesvol aangevraagd.');</script>");
    //    }
    //    catch
    //    {
    //        //indien er iets misliep, zeg het tegen de gebruiker.
    //        Response.Write("<script>alert('Er liep iets mis tijdens het reserveren van de auto. Probeer later opnieuw!');</script>");
    //    }

    //    //Maak klant variabele aan
    //    Klant aKlant = new Klant();
    //    aKlant.Voornaam = txtVoornaam.Text;
    //    aKlant.Achternaam = txtAchternaam.Text;
    //    aKlant.Mail = txtMail.Text;

    //    //controleer of deze is toegevoegd of niet en pas aan indien het wel toegevoegd is
    //    if (!DataBase.CheckIfKlantIsAdded(aKlant))
    //    {
    //        //Voeg de klant toe omdat deze nog niet toegevoegd is
    //        DataBase.addKlant(aKlant);
    //    }

    //    //Reset de textboxes
    //    txtVoornaam.Text = string.Empty;
    //    txtAchternaam.Text = string.Empty;
    //    txtMail.Text = string.Empty;
    //    ddlInstrumenten.Text = string.Empty;
    //    imgPreview.ImageUrl = string.Empty;
    //    lblPrijs.Text = "Prijs: €0.00";
    //}

    private string GetPriceAsString(string price)
    {
        string[] splitted = price.Replace(',', '.').Split('.');
        StringBuilder sb = new StringBuilder();
        for (short i = (short)splitted[0].Length; i > 0; i--)
        {
            if ((splitted[0].Length - i) % 3 == 0 && i != splitted[0].Length)
            {
                sb.Insert(0, ' ');
            }
            sb.Insert(0, splitted[0][i-1]);
        }
        return sb.ToString() + "." + splitted[1];
    }
}