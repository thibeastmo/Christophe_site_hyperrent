using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for Helper
/// </summary>
public static class Helper
{
    public static string GetPriceAsString(string price)
    {
        string[] splitted = price.Replace(',', '.').Split('.');
        StringBuilder sb = new StringBuilder();
        for (short i = (short)splitted[0].Length; i > 0; i--)
        {
            if ((splitted[0].Length - i) % 3 == 0 && i != splitted[0].Length)
            {
                sb.Insert(0, ' ');
            }
            sb.Insert(0, splitted[0][i - 1]);
        }
        return sb.ToString() + (splitted.Length > 1 ? "." + splitted[1] : string.Empty);
    }
    public static Control FindControlRecursive(Control parentControl, string controlID)
    {
        if (parentControl.ID != null && parentControl.ID.ToLower() == controlID.ToLower())
        {
            return parentControl;
        }
        if (parentControl.Controls != null)
        {
            for (int i = 0; i < parentControl.Controls.Count; i++)
            {
                var tempControl = FindControlRecursive(parentControl.Controls[i], controlID);
                if (tempControl != null)
                {
                    return tempControl;
                }
            }
        }
        return null;
    }
}