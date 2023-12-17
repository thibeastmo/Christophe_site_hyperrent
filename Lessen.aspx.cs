using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Activities;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Lessen : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strCommand = "SELECT id FROM tbllessen;";
        List<string> lessenID = DataBase.readColumn(strCommand, "id"); //Slaag de id's op van de lessen uit de database
        foreach (string id in lessenID) //Voor elke id in de idlijst
        {
            //Haal de variabelen uit de database en voeg de les toe aan de pagina
            CreateLesson(DataBase.readLesson(id));
        }
    }
    private void CreateLesson(Lesson aLesson)
    {
        //Puur code toevoegen aan pagina, als variabele opgeslagen
        HtmlGenericControl div = new HtmlGenericControl("div");
        HtmlGenericControl img = new HtmlGenericControl("img");
        HtmlGenericControl h3 = new HtmlGenericControl("h3");
        HtmlGenericControl p = new HtmlGenericControl("p");
        HtmlGenericControl _div = new HtmlGenericControl("div");
        HtmlGenericControl __div = new HtmlGenericControl("div");
        Button btnBekijk = new Button();
        Button btnNiveau = new Button();
        HtmlGenericControl span = new HtmlGenericControl("span");

        //Stel de juiste dingen in voor elke variabele
        string strImage = string.Empty;
        //Controleer of er werkelijk een afbeelding is opgeslagen en indien dit niet het geval is, stel dan een default afbeelding in
        if (aLesson.Afbeelding != "0" && aLesson.Afbeelding != null)
        {
            strImage = "images/uploaded/" + aLesson.Afbeelding;
        }
        else
        {
            strImage = "images/no-img.png";
        }
        img.Attributes.Add("src", strImage); //attributes.add hier is --> src="variabele van strImage" toevoegen aan <img />
        img.Attributes.Add("alt", "afbeelding van " + aLesson.Naam);
        h3.InnerText = aLesson.Naam;
        p.InnerHtml = aLesson.Beschrijving; //De tekst in de p (paragraph) instellen
        btnBekijk.Text = "Bekijk";
        btnBekijk.Click += new EventHandler(GoToLesson); //Bij het klikken een code laten uitvoeren, zie GoToLesson
        btnBekijk.ID = "btn" + aLesson.ID.ToString();
        btnNiveau.Text = "Niveau " + Convert.ToString(aLesson.Niveau);
        btnNiveau.Click += new EventHandler(GoToNiveau); //Bij het klikken een code laten uitvoeren, zie GoToNiveau (niet gemaakt, heeft dus geen nut)
        span.InnerHtml = Convert.ToString(aLesson.Leden) + " " + (aLesson.Leden == 1 ? "lid" : "leden");

        //Voeg de variabele toe aan de hoofddiv van een les
        _div.Controls.Add(img);
        _div.Controls.Add(h3);
        _div.Controls.Add(p);
        __div.Attributes.Add("class", "bottomLes");
        __div.Controls.Add(btnBekijk);
        __div.Controls.Add(btnNiveau);
        __div.Controls.Add(span);
        div.Controls.Add(_div);
        div.Controls.Add(__div);
        lessendiv.Controls.Add(div); //lessendiv is de algemene div waar alle lessen in komen te staan
    }
    private void GoToNiveau(object sender, EventArgs e)
    {

    }
    private void GoToLesson(object sender, EventArgs e)
    {
        //Session is de variabele dat gebruikt kan worden door alle paginas ongeacht of de pagina herlaadt of niet
        //Je kan er eender welke variabele in opslagen
        Session[Constants.SESSION_LES_ID] = (sender as Button).ID.Replace("btn", ""); //(sender as Button) --> sender variabele = de knop die ingedrukt wordt --> daarvan het ID --> enkel het cijfer van het ID door btn te verwijderen uit het id
        Response.Redirect("Lesinfo.aspx"); //Ga naar pagina Lesinfo.aspx
    }
}