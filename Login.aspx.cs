using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session[Constants.SESSION_GEBRUIKERSNAAM] != null && Session[Constants.SESSION_GEBRUIKERSNAAM].ToString() != "")
        { //Niemand logde in --> terugsturen naar login.aspx
            Response.Redirect("Beheerder.aspx");
        }
    }

    protected void LoginButton_Click(object sender, EventArgs e)
    {
        string strGebruikersnaam;
        //Maak connectie met de databank
        MySqlConnection scnnLogin = new MySqlConnection();
        MySqlCommand scmdGebruiker = new MySqlCommand();
        scnnLogin.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[Constants.CONNECTIONSTRING].ToString();
        //Commando-object
        scmdGebruiker.Connection = scnnLogin;
        scmdGebruiker.CommandText = "select gebruikersnaam from tblbeheerder where (gebruikersnaam = @UserName and wachtwoord = @Password)";
        scmdGebruiker.Parameters.Add("@UserName", MySqlDbType.VarChar, 25);
        scmdGebruiker.Parameters.Add("@Password", MySqlDbType.VarChar, 50);
        scmdGebruiker.Parameters["@UserName"].Value = UserName.Text;
        scmdGebruiker.Parameters["@Password"].Value = Password.Text;
        //Open de verbinding
        scnnLogin.Open();
        //Voer het commando uit
        if (scmdGebruiker.ExecuteScalar() == null)
        {
            strGebruikersnaam = String.Empty;
        }
        else
        {
            strGebruikersnaam = scmdGebruiker.ExecuteScalar().ToString();
            /*ExecuteScalar => 1 waarde opvragen*/
        }
        //Sluit de verbinding
        scnnLogin.Close();
        //Logincontrole
        if (strGebruikersnaam != String.Empty)
        {
            Session[Constants.SESSION_GEBRUIKERSNAAM] = strGebruikersnaam;
            FormsAuthentication.SetAuthCookie(strGebruikersnaam, false);
            Response.Redirect("Beheerder.aspx"); //Ga naar pagna Beheerder.aspx
        }
        else
        {
            Session[Constants.SESSION_GEBRUIKERSNAAM] = String.Empty;
            FailureText.Text = "Foutieve aanmelding. Probeer nogmaals!";
        }
    }
}