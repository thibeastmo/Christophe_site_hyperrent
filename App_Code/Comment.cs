using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Comment
/// </summary>
public class Comment
{
    public int ID { get; set; }
    public int BlogId { get; set; }
    public string Text { get; set; }
}