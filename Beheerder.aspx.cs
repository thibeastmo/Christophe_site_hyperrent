using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Beheerder : System.Web.UI.Page
{
    private static List<string> dontUseTheseProperties = new List<string>()
    {
        "id", "afbeelding", "categorie"
    };
    protected void Page_Load(object sender, EventArgs e)
    {//Controleer of er logingegevens zijn opgeslagen (of er dus iemand ingelogd heeft of niet)
        if (Session[Constants.SESSION_GEBRUIKERSNAAM] == null || Session[Constants.SESSION_GEBRUIKERSNAAM].ToString() == "")
        { //Niemand logde in --> terugsturen naar login.aspx
            Response.Redirect("Login.aspx");
        }
        else
        { //Iemand heeft ingelogd
            UpdatePanel1.Attributes.CssStyle.Add("height", "100%"); //Stel grootte in van de updatepanel (ging niet vanuit css voor een ongekende reden)
            Page.MaintainScrollPositionOnPostBack = true; //Dit zorgt ervoor dat wanneer de pagina herlaadt, de scrolpositie blijft. Dus je niet naar bovenaan de pagina gaat telkens wanneer je op een knop drukt
            if (true || !Page.IsPostBack) //Als de pagina niet herlaadt
            {
                ShowPanel(); //Laat het juiste panel zien
            }
            btnLogOut.ToolTip = "Log uit";
            btnAutoen.ToolTip = "Auto's";
            btnLessen.ToolTip = "Lessen";
            btnBlog.ToolTip = "Blog";
        }
    }
    protected void btnLogOut_Click(object sender, EventArgs e)
    {
        Session[Constants.SESSION_GEBRUIKERSNAAM] = null;
        FormsAuthentication.SignOut();
        Response.Redirect("Default.aspx");
    }
    protected void btnAutoen_Click(object sender, EventArgs e)
    {
        //Slaag de session voor de panel dat zichtbaar moet worden op voor instrumenten omdat er op de knop instrumenten geklikt werd
        Session["pnl"] = "instrumenten";
        ShowPanel(); //Laat de panel zien
    }
    protected void btnLessen_Click(object sender, EventArgs e)
    {
        //Slaag de session voor de panel dat zichtbaar moet worden op voor lessen omdat er op de knop lessen geklikt werd
        Session["pnl"] = "lessen";
        ShowPanel(); //Laat de panel zien
    }
    private void ShowPanel()
    {
        if (Session["pnl"] != null) //Als er de session een waarde heeft
        { //Dan de juiste panel laten zien
            switch (Session["pnl"].ToString())
            {
                case "instrumenten":
                    binstrumenten.Visible = true;
                    blessen.Visible = false;
                    bblog.Visible = false;
                    LoadAutoTXTs();
                    LoadAutoen(); //en oftewel de instrumenten laden
                    break;
                case "lessen":
                    binstrumenten.Visible = false;
                    blessen.Visible = true;
                    bblog.Visible = false;
                    LoadLessen(); //en oftewel de lessen laden
                    break;
                case "blog":
                    bblog.Visible = true;
                    binstrumenten.Visible = false;
                    blessen.Visible = false;
                    LoadBlogItems();
                    break;
            }
        }
        else
        {
            //Anders gewoon een witte panel laten zien dat niets doet (default view)
            binstrumenten.Visible = false;
            blessen.Visible = false;
            bblog.Visible = false;
        }
    }

    private void LoadAutoTXTs()
    {
        foreach (var property in typeof(Auto).GetProperties())
        {
            if (!dontUseTheseProperties.Contains(property.Name.ToLower()) && phAutoProperties.FindControl("txt" + property.Name) == null)
            {
                TextBox txt = new TextBox();
                txt.ID = "txt" + property.Name;
                txt.Attributes.Add("placeholder", property.Name);
                txt.CssClass = "btxt";
                phAutoProperties.Controls.Add(txt);
            }
        }
    }

    private void LoadAutoen()
    {
        //check of deze control ooit al eens is toegevoegd --> indien wel dan moet de pagina niet herladen worden
        if (phAutoProperties.FindControl("ddlAutoenLijst") == null)
        {
            //Reset de textboxes etc
            setAutoBorderColors();
            blackinstrumentendiv.Visible = false;
            instrumentspan.Visible = false;
            string strCommand = "SELECT id FROM tblinstrumenten;";
            List<string> instrumentID = DataBase.readColumn(strCommand, "id"); //Slaag alle id's op voor alle instrumenten uit de database
            DropDownList ddlAutoenLijst = new DropDownList();
            ddlAutoenLijst.ID = "ddlAutoenLijst";
            ddlAutoenLijst.Items.Clear(); //Maak de dropdownlist leeg
            ddlAutoenLijst.Items.Add(""); //Voeg een lege variabele toe aan de dropdownlist, puur voor het zicht
            foreach (string id in instrumentID) //Voor elke id in de instrumentenidlijst
            {
                Auto newAuto = DataBase.readAuto(id); //Maak een instrumentenvariabele aan met de waardes uit de database voor deze id
                ddlAutoenLijst.Items.Add(newAuto.Naam); //Voeg de naam van het instrument toe aan de dropdownlist
                                                        //ddlAutoenLijstoriginal.Items.Add(newAuto.Naam); //Voeg de naam van het instrument toe aan de dropdownlist
            }
            ddlAutoenLijst.CssClass = "bddl";
            //BackColor = "#303030" ForeColor = "GhostWhite"
            //ddlAutoenLijst.Attributes.Add("style", "color: GhostWhite; background - color:#303030;");
            ddlAutoenLijst.AutoPostBack = true;
            ddlAutoenLijst.SelectedIndexChanged += new EventHandler(ddlAutoenLijst_SelectedIndexChanged);
            phAutoProperties.Controls.Add(ddlAutoenLijst);
        }
    }

    private void LoadLessen()
    {
        //Reset de textboxes etc
        txtLesNaam.BorderColor = Color.Gray;
        txtLesLeerkracht.BorderColor = Color.Gray;
        txtLesDatum.BorderColor = Color.Gray;
        txtLesBeschrijving.BorderColor = Color.Gray;
        txtLesKostprijs.BorderColor = Color.Gray;
        ddlLesNiveaus.BorderColor = Color.Gray;
        fuLesAfbeelding.BorderColor = Color.Gray;
        lesdiv.Visible = false;
        string strCommand = "SELECT id FROM tbllessen;";
        List<string> lessenID = DataBase.readColumn(strCommand, "id"); //Slaag alle id's op voor alle lessen uit de database
        ddlLessenLijst.Items.Clear(); //Maak de dropdownlist leeg
        ddlLessenLijst.Items.Add(""); //Voeg een lege variabele toe aan de dropdownlist, puur voor het zicht
        foreach (string id in lessenID) //Voor elke id in de lessenidlijst
        {
            Lesson aLesson = DataBase.readLesson(id); //Maak een instrumentenvariabele aan met de waardes uit de database voor deze id
            ddlLessenLijst.Items.Add(aLesson.Naam); //Voeg de naam van het instrument toe aan de dropdownlist
        }
    }
    private void LoadBlogItems()
    {
        //Reset de textboxes etc
        txtBlogTitle.BorderColor = Color.Gray;
        txtBlogContent.BorderColor = Color.Gray;
        fuBlogAfbeelding.BorderColor = Color.Gray;
        string strCommand = "SELECT id FROM tblblog;";
        List<string> blodIDs = DataBase.readColumn(strCommand, "id");
        if (phAutoProperties.FindControl("ddlBlogItemLijst") == null)
        {
            DropDownList ddlBlogItemLijst = new DropDownList();
            ddlBlogItemLijst = new DropDownList();
            ddlBlogItemLijst.ID = "ddlBlogItemLijst";
            ddlBlogItemLijst.Items.Clear(); //Maak de dropdownlist leeg
            ddlBlogItemLijst.Items.Add(""); //Voeg een lege variabele toe aan de dropdownlist, puur voor het zicht
            foreach (string id in blodIDs)
            {
                var blogItem = DataBase.readBlogItem(id);
                ddlBlogItemLijst.Items.Add(blogItem.Title);
            }
            ddlBlogItemLijst.CssClass = "bddl";
            //BackColor = "#303030" ForeColor = "GhostWhite"
            //ddlAutoenLijst.Attributes.Add("style", "color: GhostWhite; background - color:#303030;");
            ddlBlogItemLijst.AutoPostBack = true;
            ddlBlogItemLijst.SelectedIndexChanged += new EventHandler(ddlBlogLijst_SelectedIndexChanged);
            phBlogProperties.Controls.Add(ddlBlogItemLijst);
        }
        else
        {
            DropDownList ddlBlogItemLijst = this.FindControl("ddlBlogItemLijst") as DropDownList;
            ddlBlogItemLijst = new DropDownList();
            ddlBlogItemLijst.ID = "ddlBlogItemLijst";
            ddlBlogItemLijst.Items.Clear(); //Maak de dropdownlist leeg
            ddlBlogItemLijst.Items.Add(""); //Voeg een lege variabele toe aan de dropdownlist, puur voor het zicht
            foreach (string id in blodIDs)
            {
                var blogItem = DataBase.readBlogItem(id);
                ddlBlogItemLijst.Items.Add(blogItem.Title);
            }
            ddlBlogItemLijst.CssClass = "bddl";
            //BackColor = "#303030" ForeColor = "GhostWhite"
            //ddlAutoenLijst.Attributes.Add("style", "color: GhostWhite; background - color:#303030;");
            ddlBlogItemLijst.AutoPostBack = true;
            ddlBlogItemLijst.SelectedIndexChanged += new EventHandler(ddlBlogLijst_SelectedIndexChanged);
            phBlogProperties.Controls.Add(ddlBlogItemLijst);
        }
    }
    protected void btnAutoToevoegen_Click(object sender, EventArgs e)
    {
        if (checkEveryProperty()) //Als de prijs wel omgezet kon worden
        {
            string strCommand = "SELECT id FROM tblinstrumenten;";
            List<string> instrumentID = DataBase.readColumn(strCommand, "id"); //Maak een array aan voor alle instrumentenid's uit de database
            bool AlreadyExists = false; //Bool voor te checken of het instrument al in de database zit of niet
            Auto anAuto = new Auto(); //Maak lege instrumentvariabele aan
            foreach (string id in instrumentID) //Voor elke id in de instrumentenidlijst
            {
                anAuto = DataBase.readAuto(id); //Stel de juiste variabele uit de database in voor het instrument
                if (anAuto.Naam == getAutoPropertyTxt("naam").Text && anAuto.Prijs == getAutoPropertyTxt("prijs").Text && anAuto.Categorie == ddlAutoCategorie.SelectedValue && anAuto.Afbeelding == fuAutoAfbeelding.FileName || fuAutoAfbeelding.FileName == "")
                { //Als het instrument (vanuit de database) dezelfde waardes heeft als dat de gebruiker heeft ingevuld op de pagina
                    AlreadyExists = true; //Dan bestaat dit instrument al
                    break; //Het contorleren is gebeurd omdat we al weten dat het instrument al bestaat dus, stop de loop
                }
            }
            if (!AlreadyExists)
            { //Als het nog niet bestaat
                //Stel dan de variabelen in voor het instrument
                foreach (var property in anAuto.GetType().GetProperties())
                {
                    var x = convertPropertyToType(property, getAutoPropertyTxt(property.Name));
                    if (x.Item2 && x.Item1 != null)
                    {
                        property.SetValue(anAuto, x.Item1, null);
                    }
                }
                anAuto.Afbeelding = fuAutoAfbeelding.FileName;
                anAuto.Categorie = ddlAutoCategorie.SelectedValue;
                DataBase.addAuto(anAuto); //Voeg toe aan de database
                uploadImg(); //Upload de afbeelding naar de server
                (phAutoProperties.FindControl("ddlAutoenLijst") as DropDownList).Items.Add(anAuto.Naam); //Voeg de naam toe aan de dropdownlist
                Succeeded(); //Bevestig dat het gelukt is
            }
            else
            { //Het instrument bestaat al dus zeg dat even
                Response.Write("<script>alert('Deze auto staat al in de lijst!');</script>");
            }
        }
        else
        {
            //De prijs is niet goed, het kon niet omgezet worden naar een decimaal getal
            getAutoPropertyTxt("prijs").BorderColor = Color.Orange;
        }
    }

    private List<TextBox> getAllTxtsFromAuto()
    {
        List<TextBox> txts = new List<TextBox>();
        foreach (PropertyInfo property in typeof(Auto).GetProperties())
        {
            if (!dontUseTheseProperties.Contains(property.Name.ToLower()))
            {
                txts.Add(getAutoPropertyTxt(property.Name));
            }
        }
        return txts;
    }
    private TextBox getAutoPropertyTxt(string propertyName)
    {
        var iets = phAutoProperties.FindControl("txt" + propertyName) as TextBox;
        if (iets == null)
        {
            bool ok = true;
        }
        return iets;
        //IEnumerable<TextBox> textBoxes = phAutoProperties.Controls.OfType<TextBox>();
        ////foreach (var control in autoinnerdiv.Controls.OfType<TextBox>())
        ////foreach (var control in autoinnerdiv.Controls)
        //foreach (TextBox control in textBoxes)
        //{
        //    if (control.ID.StartsWith("txt"))
        //    {
        //        if (control.ID.ToLower() == "txt" + propertyName.ToLower())
        //        {
        //            return (TextBox)control;
        //        }
        //    }
        //}
        //return null;
    }
    private Tuple<object, bool> convertPropertyToType(PropertyInfo propertyInfo, TextBox txt)
    {
        object convertedObject = null;
        bool worked = true;
        try
        {
            var propType = propertyInfo.PropertyType;
            var converter = TypeDescriptor.GetConverter(propType);
            convertedObject = converter.ConvertFromString(txt.Text.Replace('.', ','));
        }
        catch (Exception ex)
        {
            Response.Write("convertPropertyToType:<br />" +
                "Property: " + propertyInfo.Name + "<br />" +
                "Type: " + propertyInfo.PropertyType.Name +
                "<br /><br />Message: " + ex.Message + 
                "<br />StackTrace:<br />" + ex.StackTrace + "<br />");
            worked = false;
        }
        return new Tuple<object, bool>(convertedObject, worked);
    }
    private bool checkEveryProperty()
    {
        bool everythingOk = true;
        foreach (var property in typeof(Auto).GetProperties())
        {
            if (!dontUseTheseProperties.Contains(property.Name.ToLower()))
            {
                TextBox txt = getAutoPropertyTxt(property.Name);
                var x = convertPropertyToType(property, txt);
                if (x.Item2)
                {
                    txt.BorderColor = Color.Magenta;
                }
                else
                {
                    txt.BorderColor = Color.Red;
                    everythingOk = false;
                }
            }
        }
        return everythingOk;
    }
    private void setAutoBorderColors()
    {
        foreach (Control control in phAutoProperties.Controls)
        {
            if (control is TextBox)
            {
                (control as TextBox).BorderColor = Color.Gray;
            }
            else if (control is FileUpload)
            {
                (control as FileUpload).BorderColor = Color.Gray;
            }
            else if (control is DropDownList)
            {
                (control as DropDownList).BorderColor = Color.Gray;
            }
            ddlAutoCategorie.BorderColor = Color.Gray;
            fuAutoAfbeelding.BorderColor = Color.Gray;
        }
    }

    protected void btnAutoVerwijder_Click(object sender, EventArgs e)
    {
        string strCommand = "SELECT id FROM tblinstrumenten;";
        List<string> instrumentID = DataBase.readColumn(strCommand, "id"); //Maak een array aan voor alle instrumentenid's uit de database
        bool AlreadyExists = false; //Bool voor te checken of het instrument al in de database zit of niet
        Auto anAuto = new Auto(); //Maak lege instrumentvariabele aan
        string strID = string.Empty;
        foreach (string id in instrumentID) //Voor elke id in de instrumentenidlijst
        {
            anAuto = DataBase.readAuto(id); //Stel de juiste variabele uit de database in voor het instrument
            if (anAuto.Naam == getAutoPropertyTxt("naam").Text && anAuto.Prijs == getAutoPropertyTxt("prijs").Text && anAuto.Categorie == ddlAutoCategorie.SelectedValue)
            { //Als het instrument (vanuit de database) dezelfde waardes heeft als dat de gebruiker heeft ingevuld op de pagina
                AlreadyExists = true; //Dan bestaat dit instrument al
                break; //Het contorleren is gebeurd omdat we al weten dat het instrument al bestaat dus, stop de loop
            }
        }
        if (AlreadyExists)
        { //Als het instrument bestaat
          //Verwijder het instrument uit de database
            DataBase.deleteAuto(Convert.ToString(anAuto.ID));
            Succeeded(); //Bevestig dat het gelukt is
        }
        else
        {
            //De prijs is niet goed, het kon niet omgezet worden naar een decimaal getal
            Response.Write("<script>alert('Deze auto staat niet in de lijst!');</script>");
        }
    }
    protected void btnAutoOpslagen_Click(object sender, EventArgs e)
    {
        //Controleer of de session "instrument" leeg is of niet, deze session krijgt een variabele wanneer er een item in de dropdownlist geselecteerd wordt
        if (Session["instrument"] != null)
        {
            if (checkEveryProperty()) //Als de prijs wel omgezet kon worden
            {
                string strCommand = "SELECT id FROM tblinstrumenten;";
                List<string> instrumentID = DataBase.readColumn(strCommand, "id"); //Maak een array aan voor alle instrumentenid's uit de database
                foreach (string id in instrumentID) //Voor elke id in de instrumentenidlijst
                {
                    Auto anAuto = DataBase.readAuto(id); //Maak instrumentvariabele aan met waardes vanuit de database
                    //Controleer of de session dezelfde waarde heeft als de naam van het instrument
                    if (anAuto.Naam == Session["instrument"].ToString())
                    { //Als de session dezelfde waarde heeft als de naam van het instrument
                        string strVariabele = string.Empty; //string om de variabele in op te slagen voor de aanpassing in de database
                        string strColumn = string.Empty; //string om de kolom in op te slagen voor de aanpassing in de database
                        //if (anAuto.Naam != txtAutoNaam.Text)
                        //{ //Als de naam van het instrument niet hetzelfde is als dat wat de gebruiker ingegeven heeft
                        //    strVariabele = txtAutoNaam.Text;
                        //    strColumn = "naam";
                        //    DataBase.editAuto(id, strVariabele, strColumn); //Pas het dan aan
                        //    Succeeded(); //Bevestig
                        //}
                        //if (anAuto.Prijs != txtAutoPrijs.Text)
                        //{ //Als de prijs van het instrument niet hetzelfde is als dat wat de gebruiker ingegeven heeft
                        //    strVariabele = txtAutoPrijs.Text;
                        //    strColumn = "prijs";
                        //    DataBase.editAuto(id, strVariabele, strColumn);
                        //    Succeeded();
                        //}
                        foreach (var property in anAuto.GetType().GetProperties())
                        {
                            if (!dontUseTheseProperties.Contains(property.Name.ToLower()))
                            {
                                TextBox txt = getAutoPropertyTxt(property.Name);
                                object originalValue = property.GetValue(anAuto, null);
                                var newValue = convertPropertyToType(property, txt);
                                if (originalValue != newValue.Item1)
                                {
                                    strVariabele = txt.Text.Replace(',', '.');
                                    strColumn = property.Name.ToLower();
                                    DataBase.editAuto(id, strVariabele, strColumn);
                                    txt.BorderColor = Color.Green;
                                }
                            }
                        }
                        if (fuAutoAfbeelding.FileName != "" && anAuto.Afbeelding != fuAutoAfbeelding.FileName || fuAutoAfbeelding.FileName != "" && !File.Exists("images/uploaded/" + fuAutoAfbeelding.FileName))
                        { //Als de naam van de afbeelding van het instrument niet hetzelfde is als dat wat de gebruiker geupload heeft OF als er niets geupload is
                            strVariabele = fuAutoAfbeelding.FileName;
                            strColumn = "afbeelding";
                            DataBase.editAuto(id, strVariabele, strColumn); //Pas het dan aan
                            uploadImg(); //Upload afbeelding
                            Succeeded(); //Bevestig
                        }
                        if (anAuto.Categorie != ddlAutoCategorie.SelectedValue)
                        { //Als de categorie van het instrument niet hetzelfde is als dat wat de gebruiker geselecteerd heeft
                            strVariabele = ddlAutoCategorie.SelectedValue; //Stel variabele in met geselecteerde item in de dropdownlist
                            strColumn = "categorie";
                            DataBase.editAuto(id, strVariabele, strColumn); //Pas het dan aan
                            Succeeded(); //Bevestig
                        }
                        break; //De waardes zijn aangepast dus de loop mag stoppen
                    }
                    if (id == instrumentID[instrumentID.Count-1])
                    { //Als dit de laatste id (id = laatste id van de idlijst) was en niets aangepast is (dit komt na het aanpassen)
                        //Zeg dan dat dit instrument niet in de lijst stond
                        Response.Write("<script>alert('Deze auto staat niet in de lijst!');</script>");
                    }
                }
            }
        }
        else
        { //De session was leeg dus er is geen instrument geselecteerd
            Response.Write("<script>alert('Er is geen auto geselecteerd.');</script>");
        }
    }
    protected void ddlAutoenLijst_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList zender = (DropDownList)sender;
        //Reset de textboxes
        setAutoBorderColors();

        //haal alle textboxes op
        List<TextBox> txts = getAllTxtsFromAuto();

        //Controleer of het item dat geselecteer is niet niets is
        if (zender.SelectedItem.Text != "")
        {
            Session["instrument"] = zender.SelectedValue; //Slaag de naam van het item dat geselecteerd is op in de session "instrument"
            instrumentspan.Visible = false; //Puur voor het zicht (de lijn in de preview onzichtbaar maken omdat het anders als enige zichtbaar is)
            string strCommand = "SELECT id FROM tblinstrumenten;";
            List<string> instrumentID = DataBase.readColumn(strCommand, "id"); //Maak een array van alle instrumentenid's uit de database
            foreach (string id in instrumentID) //Voor elke id in de instrumtentenidlijst
            {
                Auto newAuto = DataBase.readAuto(id); //Maak een instrumentenvariabele aan met deze id met de waardes uit de database
                if (zender.SelectedValue == newAuto.Naam)
                { //Als het geselecteerd item in de dropdownlist hetzelfde is als de naam van het isntrument
                    //Stel de preview ervan in (en maak het zichtbaar)
                    blackinstrumentendiv.Visible = true;
                    instrumentnaam.InnerHtml = newAuto.Naam;
                    instrumentspan.Visible = true;
                    instrumentimg.ImageUrl = "images/uploaded/" + newAuto.Afbeelding;
                    ddlAutoCategorie.Text = newAuto.Categorie;
                    instrumentprijs.InnerHtml = "€" + Convert.ToString(newAuto.Prijs);
                    foreach(var property in newAuto.GetType().GetProperties())
                    {
                        if (!dontUseTheseProperties.Contains(property.Name.ToLower()))
                        {
                            object valueToSet = property.GetValue(newAuto, null);
                            if (valueToSet != null)
                            {
                                TextBox txtToSetTextOn = getAutoPropertyTxt(property.Name);
                                txtToSetTextOn.Text = valueToSet.ToString().Replace(',', '.');
                            }
                        }
                    }
                }
            }
        }
    }
    private void uploadImg()
    {
        //Check welke panel zichtbaar is
        if (binstrumenten.Visible)
        { //Als de instrumentenpanel zichtbaar is
            //Check of de gebruiker een bestand heeft geupload in fileupload
            if (fuAutoAfbeelding.HasFile)
            {
                try //Probeer het op te lsagen in de server in de folder images/uploaded/
                {
                    fuAutoAfbeelding.SaveAs(Server.MapPath("images/uploaded/" + fuAutoAfbeelding.FileName)); //Upload image to directory
                }
                catch (Exception ex)
                { //De foto kon niet geupload worden
                    //Zeg dit even en met welke reden
                    Response.Write("De foto kon niet geüpload worden. Met als reden:\n" + ex.Message + "'");
                }
            }
        }
        else if (blessen.Visible)
        { //Als de lessenpanel zichtbaar is
            //Check of de gebruiker een bestand heeft geupload in fileupload
            if (fuLesAfbeelding.HasFile)
            {
                try //Probeer het op te lsagen in de server in de folder images/uploaded/
                {
                    fuLesAfbeelding.SaveAs(Server.MapPath("images/uploaded/" + fuLesAfbeelding.FileName)); //Upload image to directory
                }
                catch (Exception ex)
                { //De foto kon niet geupload worden
                    //Zeg dit even en met welke reden
                    Response.Write("<script>alert('De foto kon niet geüpload worden. Met als reden:\n" + ex.Message + "');</script>");
                }
            }
        }
        else if (bblog.Visible)
        { 
            if (fuBlogAfbeelding.HasFile)
            {
                try //Probeer het op te lsagen in de server in de folder images/uploaded/
                {
                    fuBlogAfbeelding.SaveAs(Server.MapPath("images/uploaded/" + fuBlogAfbeelding.FileName)); //Upload image to directory
                }
                catch (Exception ex)
                { //De foto kon niet geupload worden
                    //Zeg dit even en met welke reden
                    Response.Write("<script>alert('De foto kon niet geüpload worden. Met als reden:\n" + ex.Message + "');</script>");
                }
            }
        }
    }
    private void Succeeded()
    {
        //Check welke panel zichtbaar is
        if (binstrumenten.Visible)
        { //De instrumentenpanel is zichtbaar
            //Zet alle borders in het groen om te laten zien dat het gelukt is
            foreach (TextBox txt in getAllTxtsFromAuto())
            {
                txt.BorderColor = Color.Green;
            }
            ddlAutoCategorie.BorderColor = Color.Green;
            fuAutoAfbeelding.BorderColor = Color.Green;
        }
        else if (blessen.Visible)
        { //De lessenpanel is zichtbaar
            //Zet alle borders in het groen om te laten zien dat het gelukt is
            txtLesNaam.BorderColor = Color.Green;
            txtLesLeerkracht.BorderColor = Color.Green;
            txtLesBeschrijving.BorderColor = Color.Green;
            txtLesDatum.BorderColor = Color.Green;
            ddlLesNiveaus.BorderColor = Color.Green;
            fuLesAfbeelding.BorderColor = Color.Green;
        }
    }
    protected void btnLesVerwijder_Click(object sender, EventArgs e)
    {
        string strCommand = "SELECT id FROM tbllessen;";
        List<string> lessenID = DataBase.readColumn(strCommand, "id"); //Maak een array met alle lessenid's uit de database
        bool AlreadyExists = false; //Bool voor te checken of de les bestaat
        Lesson aLesson = new Lesson(); //Maak een lege variabele in voor de lessen
        foreach (string id in lessenID) //Voor elke id in de lessenidlijst
        {
            aLesson = DataBase.readLesson(id); //Stel de waardes uit de database in voor de lesvariabele
            if (aLesson.Naam == txtLesNaam.Text && aLesson.Leerkracht == txtLesLeerkracht.Text && aLesson.Niveau == Convert.ToInt32(ddlLesNiveaus.SelectedValue) && Convert.ToString(aLesson.Datum) == txtLesDatum.Text && aLesson.Beschrijving == txtLesBeschrijving.Text && aLesson.Kostprijs == txtLesKostprijs.Text)
            { //Als de lesvariabele dezelfde naam, leerkracht, niveau, datum, beschrijving en afbeeldingnaam heeft als dat wat de gebruiker heeft opgegeven
                AlreadyExists = true; //Dan bestaat deze al
                break; //We weten dat deze les al bestaat dus mag de loop stoppen
            }
        }
        if (AlreadyExists)
        { //Als de les bestaat
            DataBase.deleteLesson(Convert.ToString(aLesson.ID)); //Verwijder deze dan
            Succeeded(); //Bevestig even
        }
        else
        { //Als de les niet bestaat
            //Zeg dat de les niet bestaat
            Response.Write("<script>alert('Deze les staat niet in de lijst!');</script>");
        }
    }
    protected void btnLesOpslagen_Click(object sender, EventArgs e)
    {
        //Contorleer of de session "les" niet leeg is
        if (Session["les"] != null)
        { //De session is niet leeg
            string strCommand = "SELECT id FROM tbllessen;";
            List<string> lessenID = DataBase.readColumn(strCommand, "id"); //Maak een array van alle lessenid's uit de database
            foreach (string id in lessenID) //Voor elke id in de lessenidlijst
            {
                Lesson aLesson = DataBase.readLesson(id); //Maak een variabele aan met de waardes uit de database voor deze id
                if (aLesson.Naam == Session["les"].ToString())
                { //Als de naam vna de les hetzelfde is als de session (de les dat geselecteerd is met de dropdownlist)
                    string strVariabele = string.Empty; //string om de variabele in op te slagen voor de aanpassing in de database
                    string strColumn = string.Empty; //string om de kolom in op te slagen voor de aanpassing in de database
                    if (txtLesNaam.Text != aLesson.Naam)
                    { //Als de naam van de les niet hetzelfde is als dat wat de gebruiker ingegeven heeft
                        strVariabele = txtLesNaam.Text;
                        strColumn = "naam";
                        DataBase.editLesson(id, strVariabele, strColumn); //Pas het dan aan
                        Succeeded(); //Bevestig
                    }
                    if (txtLesBeschrijving.Text != aLesson.Beschrijving)
                    { //Als de beschrijving van de les niet hetzelfde is als dat wat de gebruiker ingegeven heeft
                        strVariabele = txtLesBeschrijving.Text;
                        strColumn = "beschrijving";
                        DataBase.editLesson(id, strVariabele, strColumn); //Pas het dan aan
                        Succeeded(); //Bevestig
                    }
                    if (txtLesLeerkracht.Text != aLesson.Leerkracht)
                    { //Als de leerkracht van de les niet hetzelfde is als dat wat de gebruiker ingegeven heeft
                        strVariabele = txtLesLeerkracht.Text;
                        strColumn = "leerkracht";
                        DataBase.editLesson(id, strVariabele, strColumn); //Pas het dan aan
                        Succeeded(); //Bevestig
                    }
                    if (aLesson.Datum != txtLesDatum.Text)
                    {  //Als de datum van de les niet hetzelfde is als dat wat de gebruiker ingegeven heeft
                        strVariabele = txtLesDatum.Text;
                        strColumn = "datum";
                        DataBase.editLesson(id, strVariabele, strColumn); //Pas het dan aan
                        Succeeded(); //Bevestig
                    }
                    if (fuLesAfbeelding.FileName != "" && aLesson.Afbeelding != fuLesAfbeelding.FileName || fuLesAfbeelding.FileName != "" && !File.Exists("images/uploaded/" + fuLesAfbeelding.FileName))
                    { //Als de naam van de afbeelding van de les niet hetzelfde is als dat wat de gebruiker geupload heeft OF als er niets geupload is
                        strVariabele = fuLesAfbeelding.FileName;
                        strColumn = "afbeelding";
                        DataBase.editLesson(id, strVariabele, strColumn); //Pas het dan aan
                        uploadImg(); //Upload de afbeelding
                        Succeeded(); //Bevestig
                    }
                    if (aLesson.Niveau != Convert.ToInt32(ddlLesNiveaus.SelectedValue))
                    { //Als het niveau van de les niet hetzelfde is als dat wat de gebruiker geselecteerd heeft
                        strVariabele = ddlLesNiveaus.SelectedValue;
                        strColumn = "niveau";
                        DataBase.editLesson(id, strVariabele, strColumn); //Pas het dan aan
                        Succeeded(); //Bevestig
                    }
                    if (aLesson.Kostprijs != txtLesKostprijs.Text)
                    {  //Als de kostprijs van de les niet hetzelfde is als dat wat de gebruiker ingegeven heeft
                        strVariabele = txtLesKostprijs.Text;
                        strColumn = "kostprijs";
                        DataBase.editLesson(id, strVariabele, strColumn); //Pas het dan aan
                        Succeeded(); //Bevestig
                    }
                    break; //De waardes zijn aangepast dus de loop mag stoppen
                }
                if (id == lessenID[lessenID.Count - 1])
                { //Als dit de laatste id (id = laatste id van de idlijst) was en niets aangepast is (dit komt na het aanpassen)
                  //Zeg dan dat dit les niet in de lijst stond
                    Response.Write("<script>alert('Deze les staat niet in de lijst!');</script>");
                }
            }
        }
        else
        { //De session is leeg
            //Zeg even dat er geen les geselecteer is
            Response.Write("<script>alert('Er is geen les geselecteerd.');</script>");
        }
    }
    protected void btnLesToevoegen_Click(object sender, EventArgs e)
    {
        string strCommand = "SELECT id FROM tbllessen;";
        List<string> lessenID = DataBase.readColumn(strCommand, "id"); //Maak een array aan van alle lessenid's uit de database
        bool AlreadyExists = false; //Bool voor te checken of de les al in de database zit of niet
        Lesson aLesson = new Lesson(); //Maak een lege lesvariabele aan
        foreach (string id in lessenID) //Voor elke id in de lessenidlijst
        {
            aLesson = DataBase.readLesson(id); //Stel de waardes uit de database in voor de lesvariabele voor deze id
            if (aLesson.Naam == txtLesNaam.Text && aLesson.Leerkracht == txtLesLeerkracht.Text && aLesson.Niveau == Convert.ToInt32(ddlLesNiveaus.SelectedValue) && Convert.ToString(aLesson.Datum) == txtLesDatum.Text && aLesson.Beschrijving == txtLesBeschrijving.Text)
            { //Als de lesvariabele dezelfde naam, leerkracht, niveau, datum, beschrijving en afbeeldingnaam heeft als dat wat de gebruiker heeft opgegeven
                AlreadyExists = true; //Dan bestaat deze al
                break; //We weten dat deze les al bestaat dus mag de loop stoppen
            }
        }
        if (!AlreadyExists)
        { //Als het nog niet bestaat
          //Stel dan de variabelen in voor de les
            aLesson.Naam = txtLesNaam.Text;
            aLesson.Leerkracht = txtLesLeerkracht.Text;
            aLesson.Niveau = Convert.ToInt32(ddlLesNiveaus.SelectedValue);
            aLesson.Beschrijving = txtLesBeschrijving.Text;
            aLesson.Afbeelding = fuLesAfbeelding.FileName;
            aLesson.Datum = txtLesDatum.Text;
            aLesson.Kostprijs = txtLesKostprijs.Text;
            DataBase.addLesson(aLesson); //Voeg les toe aan de database
            uploadImg(); //Upload de afbeelding
            ddlLessenLijst.Items.Add(aLesson.Naam); //Voeg een item toe aan de dropdownlist
            Succeeded(); //Bevestig
            //Reset de panel
            txtLesNaam.Text = string.Empty;
            txtLesLeerkracht.Text = string.Empty;
            txtLesDatum.Text = string.Empty;
            txtLesBeschrijving.Text = string.Empty;
            lesdiv.Visible = false;
        }
        else
        { //De les bestaat al dus zeg dat even
            Response.Write("<script>alert('Deze les staat al in de lijst!');</script>");
        }
    }
    protected void ddlLessenLijst_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtLesNaam.BorderColor = Color.Gray;
        txtLesLeerkracht.BorderColor = Color.Gray;
        txtLesDatum.BorderColor = Color.Gray;
        txtLesBeschrijving.BorderColor = Color.Gray;
        txtLesKostprijs.BorderColor = Color.Gray;
        ddlLesNiveaus.BorderColor = Color.Gray;
        fuLesAfbeelding.BorderColor = Color.Gray;
        if ((sender as DropDownList).SelectedValue != "")
        {
            Session["les"] = (sender as DropDownList).SelectedValue;
            string strCommand = "SELECT id FROM tbllessen;";
            List<string> lesID = DataBase.readColumn(strCommand, "id");
            foreach (string id in lesID)
            {
                Lesson aLesson = DataBase.readLesson(id);
                if ((sender as DropDownList).SelectedValue == aLesson.Naam)
                {
                    lesdiv.Visible = true;
                    lessenp.InnerHtml = txtLesBeschrijving.Text = aLesson.Beschrijving;
                    lesleden.InnerHtml = Convert.ToString(aLesson.Leden) + " " + (aLesson.Leden == 1 ? "lid" : "leden");
                    lessenimg.ImageUrl = "images/uploaded/" + aLesson.Afbeelding;
                    ddlLesNiveaus.Text = Convert.ToString(aLesson.Niveau);
                    btnLesNivau.Text = "Niveau " + Convert.ToString(aLesson.Niveau);
                    txtLesNaam.Text = aLesson.Naam;
                    lestitel.InnerText = aLesson.Naam;
                    txtLesDatum.Text = Convert.ToString(aLesson.Datum);
                    txtLesLeerkracht.Text = aLesson.Leerkracht;
                    txtLesKostprijs.Text = aLesson.Kostprijs;
                }
            }
        }
    }

    protected void btnBlogVerwijder_Click(object sender, EventArgs e)
    {
        string strCommand = "SELECT id FROM tblblog;";
        List<string> lessenID = DataBase.readColumn(strCommand, "id"); //Maak een array met alle blogid's uit de database
        bool AlreadyExists = false; //Bool voor te checken of de blogitem bestaat
        var blogItem = new BlogItem(); //Maak een lege variabele in voor de lessen
        foreach (string id in lessenID) //Voor elke id in de blogitemidlijst
        {
            blogItem = DataBase.readBlogItem(id); //Stel de waardes uit de database in voor de blogitemvariabele
            if (blogItem.Title == txtBlogTitle.Text && blogItem.Content == txtBlogContent.Text)
            {
                AlreadyExists = true; //Dan bestaat deze al
                break; //We weten dat deze blogitem al bestaat dus mag de loop stoppen
            }
        }
        if (AlreadyExists)
        { //Als de les bestaat
            DataBase.deleteBlogItem(Convert.ToString(blogItem.ID)); //Verwijder deze dan
            Succeeded(); //Bevestig even
            txtBlogTitle.Text = string.Empty;
            txtBlogContent.Text = string.Empty;
        }
        else
        { //Als de les niet bestaat
            //Zeg dat de blogitem niet bestaat
            Response.Write("<script>alert('Deze blogitem staat niet in de lijst!');</script>");
        }
    }

    protected void btnBlogOpslagen_Click(object sender, EventArgs e)
    {
        //Contorleer of de session "blog" niet leeg is
        if (Session["blog"] != null)
        { //De session is niet leeg
            string strCommand = "SELECT id FROM tblblog;";
            List<string> blogIDs = DataBase.readColumn(strCommand, "id"); //Maak een array van alle blogID's uit de database
            foreach (string id in blogIDs) //Voor elke id in de lessenidlijst
            {
                var blogItem = DataBase.readBlogItem(id); //Maak een variabele aan met de waardes uit de database voor deze id
                if (blogItem.Title == Session["blog"].ToString())
                { //Als de naam vna de blogitem hetzelfde is als de session (de blogitem dat geselecteerd is met de dropdownlist)
                    string strVariabele = string.Empty; //string om de variabele in op te slagen voor de aanpassing in de database
                    string strColumn = string.Empty; //string om de kolom in op te slagen voor de aanpassing in de database
                    if (txtBlogTitle.Text != blogItem.Title)
                    { //Als de naam van de blogitem niet hetzelfde is als dat wat de gebruiker ingegeven heeft
                        strVariabele = txtBlogTitle.Text;
                        strColumn = "title";
                        DataBase.editBlog(id, strVariabele, strColumn); //Pas het dan aan
                        Succeeded(); //Bevestig
                    }
                    if (txtBlogContent.Text != blogItem.Content)
                    { //Als de beschrijving van de blogitem niet hetzelfde is als dat wat de gebruiker ingegeven heeft
                        strVariabele = txtBlogContent.Text;
                        strColumn = "content";
                        DataBase.editBlog(id, strVariabele, strColumn); //Pas het dan aan
                        Succeeded(); //Bevestig
                    }
                    if (ddlAuthor.Text != blogItem.Author)
                    { //Als de author van de blogitem niet hetzelfde is als dat wat de gebruiker ingegeven heeft
                        strVariabele = ddlAuthor.Text;
                        strColumn = "author";
                        DataBase.editBlog(id, strVariabele, strColumn); //Pas het dan aan
                        Succeeded(); //Bevestig
                    }
                    if (fuBlogAfbeelding.FileName != "" && blogItem.imageUrl != fuBlogAfbeelding.FileName || fuBlogAfbeelding.FileName != "" && !File.Exists("images/uploaded/" + fuBlogAfbeelding.FileName))
                    { //Als de naam van de afbeelding van de blogitem niet hetzelfde is als dat wat de gebruiker geupload heeft OF als er niets geupload is
                        strVariabele = fuBlogAfbeelding.PostedFile.FileName;
                        strColumn = "imageurl";
                        DataBase.editBlog(id, strVariabele, strColumn); //Pas het dan aan
                        uploadImg(); //Upload de afbeelding
                        Succeeded(); //Bevestig
                    }
                    txtBlogTitle.Text = string.Empty;
                    txtBlogContent.Text = string.Empty;
                    break; //De waardes zijn aangepast dus de loop mag stoppen
                }
                if (id == blogIDs[blogIDs.Count - 1])
                { //Als dit de laatste id (id = laatste id van de idlijst) was en niets aangepast is (dit komt na het aanpassen)
                  //Zeg dan dat dit blogitem niet in de lijst stond
                    Response.Write("<script>alert('Deze blogitem staat niet in de lijst!');</script>");
                }
            }
        }
        else
        { //De session is leeg
            //Zeg even dat er geen blogitem geselecteer is
            Response.Write("<script>alert('Er is geen blogitem geselecteerd.');</script>");
        }
    }

    protected void btnBlogToevoegen_Click(object sender, EventArgs e)
    {
        string strCommand = "SELECT id FROM tblblog;";
        List<string> blogIDs = DataBase.readColumn(strCommand, "id"); //Maak een array aan van alle blogid's uit de database
        bool AlreadyExists = false; //Bool voor te checken of de blogitem al in de database zit of niet
        var blogItem = new BlogItem(); //Maak een lege blogvariabele aan
        foreach (string id in blogIDs) //Voor elke id in de blogidlijst
        {
            blogItem = DataBase.readBlogItem(id); //Stel de waardes uit de database in voor de blogvariabele voor deze id
            if (blogItem.Title == txtBlogTitle.Text && blogItem.Content == txtBlogContent.Text)
            {
                AlreadyExists = true; //Dan bestaat deze al
                break; //We weten dat deze blogitem al bestaat dus mag de loop stoppen
            }
        }
        if (!AlreadyExists)
        { //Als het nog niet bestaat
          //Stel dan de variabelen in voor de blogitem
            blogItem.Title = txtBlogTitle.Text;
            blogItem.Content = txtBlogContent.Text;
            if (fuBlogAfbeelding.HasFile)
            {
                blogItem.imageUrl = fuBlogAfbeelding.PostedFile.FileName;
            }
            blogItem.CreationDate = DateTime.Now;
            blogItem.Author = ddlAuthor.Text;
            DataBase.addBlogItem(blogItem); //Voeg blogitem toe aan de database
            uploadImg(); //Upload de afbeelding
            ddlLessenLijst.Items.Add(blogItem.Title); //Voeg een item toe aan de dropdownlist
            Succeeded(); //Bevestig
            //Reset de panel
            txtBlogTitle.Text = string.Empty;
            txtBlogContent.Text = string.Empty;
        }
        else
        { //De blogitem bestaat al dus zeg dat even
            Response.Write("<script>alert('Deze blogitem staat al in de lijst!');</script>");
        }
    }

    protected void btnBlog_Click(object sender, EventArgs e)
    {
        //Slaag de session voor de panel dat zichtbaar moet worden op voor faq omdat er op de knop faq geklikt werd
        Session["pnl"] = "blog";
        ShowPanel(); //Laat de panel zien
    }

    protected void ddlBlogLijst_SelectedIndexChanged(object sender, EventArgs e)
    {
        txtBlogTitle.BorderColor = Color.Gray;
        txtBlogContent.BorderColor = Color.Gray;
        fuBlogAfbeelding.BorderColor = Color.Gray;
        if ((sender as DropDownList).SelectedValue != "")
        {
            Session["blog"] = (sender as DropDownList).SelectedValue;
            string strCommand = "SELECT id FROM tblblog;";
            List<string> blogIDs = DataBase.readColumn(strCommand, "id");
            foreach (string id in blogIDs)
            {
                var blogItem = DataBase.readBlogItem(id);
                if ((sender as DropDownList).SelectedValue == blogItem.Title)
                {
                    txtBlogContent.Text = blogItem.Content;
                    txtBlogTitle.Text = blogItem.Title;
                }
            }
        }
    }
}