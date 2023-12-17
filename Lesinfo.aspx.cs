using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Lesinfo : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Als er geen waarde is opgeslagen in de Session dan niks doen
        if (Session[Constants.SESSION_LES_ID] != null)
        { //Er is wel een waarde opgeslagen
            string strLesid = Session[Constants.SESSION_LES_ID].ToString(); //Slaag het op als string
            Lesson aLesson = DataBase.readLesson(strLesid); //Maak lesson variabele aan en inneens met de juiste waardes vanuit de database
            //Stel de juiste dingen in voor de textboxes en img en p (paragraph)
            imgles.ImageUrl = "../images/uploaded/" + aLesson.Afbeelding;
            lblLesnaam.Text = aLesson.Naam;
            lblDatum.Text = aLesson.Datum;
            lblLeden.Text = aLesson.Leden.ToString() + " leden";
            lblLeerkracht.Text = aLesson.Leerkracht;
            lblNiveau.Text = "Niveau " + aLesson.Niveau.ToString();
            ples.InnerHtml = aLesson.Beschrijving; //De tekst in de paragraph instellen
        }
    }
    protected void btnInschrijven_Click(object sender, EventArgs e)
    {
        //Response.Write("<script>alert('lesid = " + Session["lesid"].ToString() + "')</script>");
        //Response.Write("lesid = " + Session["lesid"].ToString());


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

        //Schrijf in
        string strCommand = "SELECT id FROM tblklanten;";
        List<string> klantenIDlijst = DataBase.readColumn(strCommand, "id"); //Haal alle klantenid's uit de database en zet ze in een array
        Klant tempKlant = new Klant(); //Maak een tijdelijke klantvariabele aan (zonder waardes)
        foreach (string id in klantenIDlijst) //Voor elke id in de klantenidlijst
        {
            tempKlant = DataBase.readKlant(id); //stel tijdelijke klantvariabele in met waardes uit database voor deze id
            if (tempKlant.Voornaam == aKlant.Voornaam && tempKlant.Achternaam == aKlant.Achternaam && tempKlant.Mail == aKlant.Mail)
            { //Als de tijdelijke klantvariabele met voornaam, achternaam en mail overeenkomt met die van de klant op de pagina
                DataBase.addInschrijving(Convert.ToString(tempKlant.ID), Session[Constants.SESSION_LES_ID].ToString()); //Voeg deze klant toe aan de inschrijvingen in de database
                break; //Stop de foreach want hetgeen dat moest gebeuren is al gebeurd
            }
        }

        //Update het aantal leden in de les
        strCommand = "SELECT id FROM tblinschrijvingen;";
        List<string> inschrijvingenIDlijst = DataBase.readColumn(strCommand, "id"); //Haal alle inschrijvingid's uit de datbase en zet ze in een array
        int intTeller = 0; //De teller voor hoeveel klanten er zijn
        Inschrijving anInschrijving = new Inschrijving(); //Maak een lege inschrijvingsvariabele
        foreach (string id in inschrijvingenIDlijst) //Voor elke id in de inschrijvingsidlijst
        {
            anInschrijving = DataBase.readInschrijving(id); //Stel de inschrijvingsvariabele in met de waardes uit de database voor deze id
            if (anInschrijving.LesID == Convert.ToInt32(Session[Constants.SESSION_LES_ID]))
            { //Als het lesid (dat geselecteer is op Lessen.aspx) hetzelfde is als het id in de foreach, voeg dan 1 toe aan de teller
                intTeller++;
            }
        }

        //Pas het aantal leden aan in de database
        DataBase.editLesson(Session["lesid"].ToString(), Convert.ToString(intTeller), "leden");

        //automatische mail
        //minstens 2 dagen op voorhand overschrijven van bedrag

        //Haal les op
        Lesson les = DataBase.readLesson(Session["lesid"].ToString());

        string strBody = "Beste " + aKlant.Voornaam + "," +
                "\n\nDit is de bevestiging dat je je hebt ingeschreven voor '" + les.Naam + "'." +
                "\nNiveau: " + les.Niveau.ToString() + "" +
                "\nDatum: " + les.Datum + "" +
                "\nKostprijs: €" + les.Kostprijs +  
                "\n\nGelieve het bedrag uiterst 2 dagen op voorhand over te schrijven." + 
                "\nTot dan!" +
                "\n\nMet vriendelijke groeten," + 
                "\n\n- Hyperrent";
        string strSendTo = aKlant.Mail;
        string strSubject = "Besvestiging inschrijving " + les.Naam.ToUpper();
        Sendmail.Send(strSendTo, txtMail.Text, strSubject, strBody);

        strBody = "Beste Christophe," +
                "\n\n" + aKlant.Voornaam + " " + aKlant.Achternaam + " heeft zich ingeschreven voor `" + les.Naam + "`." +
                "\nNiveau: " + les.Niveau.ToString() + "" +
                "\nDatum: " + les.Datum + "" +
                "\nLeerkracht: " + les.Leerkracht.ToString() + "" +
                "\n\n- Hyperrent";
        strSendTo = Constants.RECEIVING_MAIL_ADDRESS;
        strSubject = "Nieuwe inschrijving " + les.Naam.ToUpper();
        Sendmail.Send(strSendTo, txtMail.Text, strSubject, strBody);

        //Reset de pagina
        btnInschrijven.BackColor = Color.Green;
        txtVoornaam.Text = string.Empty;
        txtAchternaam.Text = string.Empty;
        txtMail.Text = string.Empty;
    }
}