using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string strPagina = Request.RawUrl; //Slaag de url op in een string
        if (strPagina.Contains("Reserveren")) //Controleer of een bepaald woord in die url staat
        {
            underheader.Attributes.Add("style", "background-color:white"); //Als het woord in de url staat, dan de achtergrond van underheader aanpassen naargelang de pagina
        }
        else if (strPagina.Contains("Lessen"))
        {
            underheader.Attributes.Add("style", "background-color:ghostwhite");
        }
        else if (strPagina.Contains("Info"))
        {
            underheader.Attributes.Add("style", "background-color:#303030");
        }
        else if (strPagina.Contains("Default"))
        {
            underheader.Attributes.Add("style", "background-image: url(images/black_marble.jpg)");
        }
        else if (strPagina.Contains("Faq"))
        {
            underheader.Attributes.Add("style", "background-image: url(images/black_marble.jpg)");
        }
        else if (strPagina.Contains("Lesinfo"))
        {
            underheader.Attributes.Add("style", "background-color:#303030");
        }
        else //Als het geen van bovenstaande is dan heeft het deze als default
        {
            underheader.Attributes.Add("style", "background-image: url(images/black_marble.jpg)");
        }
    }
}
