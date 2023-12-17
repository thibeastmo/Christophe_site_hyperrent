using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Blog : System.Web.UI.Page
{
    protected void Page_Init(object sender, EventArgs e)
    {
        string strCommand = "SELECT id FROM tblblog;";
        List<string> blogIDs = DataBase.readColumn(strCommand, "id"); //Slaag de id's op van de lessen uit de database
        int counter = 0;
        foreach (string id in blogIDs) //Voor elke id in de idlijst
        {
            //Haal de variabelen uit de database en voeg de les toe aan de pagina
            try
            {
                CreateBlogItem(DataBase.readBlogItem(id), counter % 2 == 0);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('"+ex.Message+"\n\n"+ex.StackTrace+"')</script>");
            }
            counter++;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
    }
    private void CreateBlogItem(BlogItem blogItem, bool imageRight)
    {
        //Puur code toevoegen aan pagina, als variabele opgeslagen
        HtmlGenericControl div_ = new HtmlGenericControl("div");
        HtmlGenericControl div = new HtmlGenericControl("div");
        HtmlGenericControl _div = new HtmlGenericControl("div");
        HtmlGenericControl __div = new HtmlGenericControl("div");
        HtmlGenericControl ___div = new HtmlGenericControl("div");
        HtmlGenericControl ____div = new HtmlGenericControl("div");
        HtmlGenericControl img = new HtmlGenericControl("img");
        HtmlGenericControl h3 = new HtmlGenericControl("h3");
        HtmlGenericControl p = new HtmlGenericControl("p");
        HtmlGenericControl spanDate = new HtmlGenericControl("span");
        HtmlGenericControl spanAuthor = new HtmlGenericControl("span");
        Button btnComment = new Button();
        btnComment.ID = "btnComment_" + blogItem.ID;
        btnComment.Click += BtnComment_Click;
        TextBox txtComment = new TextBox();
        txtComment.CssClass = "txt";
        txtComment.ID = "txtComment_" + blogItem.ID;
        txtComment.Attributes.Add("placeholder", "Comment...");

        //Stel de juiste dingen in voor elke variabele
        string strImage = string.Empty;
        //Controleer of er werkelijk een afbeelding is opgeslagen en indien dit niet het geval is, stel dan een default afbeelding in
        if (blogItem.imageUrl != "0" && blogItem.imageUrl != null)
        {
            strImage = "images/uploaded/" + blogItem.imageUrl;
        }
        else
        {
            strImage = "images/no-img.png";
        }
        img.Attributes.Add("src", strImage); //attributes.add hier is --> src="variabele van strImage" toevoegen aan <img />
        img.Attributes.Add("alt", "afbeelding van " + blogItem.Title);
        h3.InnerText = blogItem.Title;
        p.InnerHtml = blogItem.Content.Replace("\r\n", "<br>").Replace("\n", "<br>"); //De tekst in de p (paragraph) instellen
        spanDate.InnerHtml = "Gepubliceerd op: <br>" + blogItem.CreationDate.ToString("dd/MM/yyyy hh:mm:ss");
        spanAuthor.InnerHtml = "Geschreven door: <br>" + blogItem.Author;

        //Voeg de variabele toe aan de hoofddiv van een les
        __div.Controls.Add(spanAuthor);
        __div.Controls.Add(spanDate);
        __div.Attributes.Add("class", "blogiteminfo");
        _div.Controls.Add(h3);
        _div.Controls.Add(p);
        _div.Controls.Add(__div);
        _div.Controls.Add(___div);
        _div.Controls.Add(____div);
        ___div.Controls.Add(txtComment);
        ___div.Controls.Add(btnComment);
        var comments = DataBase.readComments(blogItem.ID);
        if (comments.Count > 0)
        {
            HtmlGenericControl spanComment = new HtmlGenericControl("span");
            spanComment.InnerText = "Comments:";
            ____div.Controls.Add(spanComment);
            for (int i = 0; i < comments.Count; i++)
            {
                HtmlGenericControl pComment = new HtmlGenericControl("p");
                pComment.InnerText = comments[i].Text;
                ____div.Controls.Add(pComment);
            }
        }
        if (imageRight)
        {
            div.Controls.Add(_div);
            div.Controls.Add(img);
        }
        else
        {
            div.Controls.Add(img);
            div.Controls.Add(_div);
        }
        div_.Controls.Add(div);
        blogContent.Controls.Add(div_); //lessendiv is de algemene div waar alle lessen in komen te staan
    }

    private void BtnComment_Click(object sender, EventArgs e)
    {
        string strId = (sender as Button).ID.Split('_')[1];
        var txt = blogContent.FindControl("txtComment_" + strId);
        if (txt != null)
        {
            string text = ((TextBox)txt).Text;
            var comment = new Comment()
            {
                BlogId = Convert.ToInt32(strId),
                Text = text
            };
            DataBase.addComment(comment);
            (sender as Button).Text = string.Empty;
            Response.Redirect(Request.RawUrl);
        }
    }
}