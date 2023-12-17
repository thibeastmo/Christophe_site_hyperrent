using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

/// <summary>
/// Summary description for Sendmail
/// </summary>
public class Sendmail
{
    public static bool Send(string strsendTo, string strsendFrom, string strsubject, string strbody)
    {
        SmtpClient client = new SmtpClient();
        MailMessage msg = new MailMessage();
        try
        {
            //setup SMTP Host Here
            client.Host = "smtp.mijnhostingpartner.nl";
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            System.Net.NetworkCredential smtpCreds = new System.Net.NetworkCredential(Constants.RECEIVING_MAIL_ADDRESS, Constants.RECEIVING_MAIL_ADDRESS_PASSWORD);
            client.Credentials = smtpCreds;

            //convert strings to MailAdresses
            MailAddress to = new MailAddress(strsendTo);
            MailAddress from = new MailAddress(strsendFrom);

            //set up message settings
            msg.Subject = strsubject;
            msg.Body = strbody;
            msg.From = from;
            msg.To.Add(to);
            msg.IsBodyHtml = false;

            //Send mail
            client.Send(msg);
            return true;
        }
        catch
        {
            return false;
        }
    }
}