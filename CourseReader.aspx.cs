using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CourseReader : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ViewPDF();
    }
    private void ViewPDF()
    {
        string embed = "<object data=\"{0}\" type=\"application/pdf\" width=\"1200px\" height=\"1000px\"></object>";
        embedPdf.Text = string.Format(embed, ResolveUrl("/docs/course_reader.pdf"));
    }
}