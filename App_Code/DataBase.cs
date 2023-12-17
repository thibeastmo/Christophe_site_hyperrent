using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

/// <summary>
/// Summary description for DataBase
/// </summary>
public class DataBase
{
    static string strConnstring = Constants.CONNECTIONSTRING;

    #region teksten


    public static Tuple<int, string> readTekst(int tekstID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblteksten where id = @id";
        mysqlcom.Parameters.Add("@id", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@id"].Value = tekstID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        Tuple<int, string> result = null;
        if (reader.Read())
        {
            result = new Tuple<int, string>(Convert.ToInt32(reader["id"]), reader["tekst"].ToString());
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return result;
    }


    public static void editTekst(string strID, string strVariabele, string strColumn = "tekst")
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "UPDATE tblteksten SET " + strColumn + " = @variabele WHERE id = " + strID + ";";
        mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@variabele"].Value = strVariabele;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }

    #endregion

    public static Auto readAuto(string strAutoID)
    {
        Auto anAuto = new Auto();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblinstrumenten where id = @id";
        mysqlcom.Parameters.Add("@id", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@id"].Value = strAutoID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        if (reader.Read())
        {
            for (short i = 0; i < reader.FieldCount; i++)
            {
                foreach (var property in anAuto.GetType().GetProperties())
                {
                    if (reader.GetName(i).ToLower() == property.Name.ToLower())
                    {
                        if (!(reader[reader.GetName(i)] is DBNull))
                        {
                            Type typeToConvertTo = property.PropertyType;
                            if (Nullable.GetUnderlyingType(property.PropertyType) != null)
                            {
                                typeToConvertTo = Nullable.GetUnderlyingType(property.PropertyType);
                            }
                            property.SetValue(anAuto, Convert.ChangeType(reader[reader.GetName(i)], typeToConvertTo), null);
                        }
                        break;
                    }
                }
            }
            //anAuto.ID = Convert.ToInt32(reader["id"]);
            //anAuto.Naam = reader["naam"].ToString();
            //anAuto.Prijs = reader["prijs"].ToString();
            //anAuto.Afbeelding = reader["afbeelding"].ToString();
            //anAuto.Categorie = reader["categorie"].ToString();
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return anAuto;
    }
    public static Inschrijving readInschrijving(string strInschrijvingID)
    {
        Inschrijving anInschrijving = new Inschrijving();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblinschrijvingen where id = @id";
        mysqlcom.Parameters.Add("@id", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@id"].Value = strInschrijvingID;
        bool isOpen = false;
        try
        {
            mysqalcon.Open();
            isOpen = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        if (isOpen)
        {
            MySqlDataReader reader = mysqlcom.ExecuteReader();
            if (reader.Read())
            {
                anInschrijving.ID = Convert.ToInt32(reader["id"]);
                anInschrijving.LesID = Convert.ToInt32(reader["lesid"]);
                anInschrijving.KlandID = Convert.ToInt32(reader["klantid"]);
            }
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return anInschrijving;
    }
    public static Lesson readLesson(string strLessonID)
    {
        Lesson aLesson = new Lesson();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tbllessen where id = @id";
        mysqlcom.Parameters.Add("@id", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@id"].Value = strLessonID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        if (reader.Read())
        {
            aLesson.ID = Convert.ToInt32(reader["id"]);
            aLesson.Naam = reader["naam"].ToString();
            aLesson.Beschrijving = reader["beschrijving"].ToString();
            aLesson.Leerkracht = reader["leerkracht"].ToString();
            aLesson.Leden = Convert.ToInt32(reader["leden"]);
            aLesson.Datum = reader["datum"].ToString();
            aLesson.Afbeelding = reader["afbeelding"].ToString();
            aLesson.Niveau = Convert.ToInt32(reader["niveau"]);
            aLesson.Kostprijs = reader["kostprijs"].ToString();
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return aLesson;
    }
    public static BlogItem readBlogItem(string strLessonID)
    {
        var blogItem = new BlogItem();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblblog where id = @id";
        mysqlcom.Parameters.Add("@id", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@id"].Value = strLessonID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        if (reader.Read())
        {
            blogItem.ID = Convert.ToInt32(reader["id"]);
            blogItem.Title = reader["title"].ToString();
            blogItem.Content = reader["content"].ToString();
            blogItem.Author = reader["author"].ToString();
            if (reader["uploaddate"] != System.DBNull.Value)
            {
                blogItem.CreationDate = Convert.ToDateTime(reader["uploaddate"]);
            }
            blogItem.imageUrl = reader["imageurl"].ToString();
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return blogItem;
    }
    public static List<Comment> readComments(int blogID)
    {
        var comments = new List<Comment>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblcomments where blogid = @id";
        mysqlcom.Parameters.Add("@id", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@id"].Value = blogID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        while(reader.Read())
        {
            var comment = new Comment();
            comment.BlogId = Convert.ToInt32(reader["blogid"]);
            comment.Text = reader["text"].ToString();
            comments.Add(comment);
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return comments;
    }
    public static List<string> readColumn(string strCommand, string strColumnName)
    {
        List<string> aColumn = new List<string>();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = strCommand;
        mysqlcom.Parameters.Add("@" + strColumnName, MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@" + strColumnName].Value = strColumnName;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        while (reader.Read())
        {
            int intAmount = reader.FieldCount;
            if (intAmount != 0)
            {
                for (int intCounter = 0; intCounter < intAmount; intCounter++)
                {
                    try
                    {
                        aColumn.Add(reader.GetString(intCounter));
                    }
                    catch
                    {

                    }
                }
            }
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return aColumn;
    }
    public static void editAuto(string strID, string strVariabele, string strColumn, bool isNumber = false)
    {
        if (isNumber)
        {
            strVariabele = strVariabele.Replace(",", ".");
        }
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "UPDATE tblinstrumenten SET " + strColumn + " = @variabele WHERE id = " + strID + ";";
        mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@variabele"].Value = strVariabele;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void editLesson(string strID, string strVariabele, string strColumn)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "UPDATE tbllessen SET " + strColumn + " = @variabele WHERE id = " + strID + ";";
        mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@variabele"].Value = strVariabele;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void editBlog(string strID, string strVariabele, string strColumn)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "UPDATE tblblog SET " + strColumn + " = @variabele WHERE id = " + strID + ";";
        mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@variabele"].Value = strVariabele;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void addAuto(Auto anAuto)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        string propertyListString = "";
        string valueListString = "";
        bool firstTime = true;
        foreach (var property in anAuto.GetType().GetProperties())
        {
            string parameterValue = '@' + property.Name.ToLower();
            mysqlcom.Parameters.Add(parameterValue, MySqlDbType.VarChar, 25);
            mysqlcom.Parameters[parameterValue].Value = property.GetValue(anAuto, null);
            if (firstTime)
            {
                firstTime = false;
            }
            else
            {
                propertyListString += ", ";
                valueListString += ", ";
            }
            propertyListString += property.Name.ToLower();
            valueListString += '@' + property.Name.ToLower();
        }
        mysqlcom.CommandText = "INSERT INTO tblinstrumenten (" + propertyListString + ") VALUES (" + valueListString + ");";
        mysqlcom.Connection = mysqalcon;
        //mysqlcom.CommandText = "INSERT INTO tblinstrumenten (naam, prijs, afbeelding, categorie) VALUES (@naam, @prijs, @afbeelding, @categorie);";
        //mysqlcom.Parameters.Add("@naam", MySqlDbType.VarChar, columnLength);
        //mysqlcom.Parameters["@naam"].Value = anAuto.Naam;
        //mysqlcom.Parameters.Add("@prijs", MySqlDbType.VarChar, columnLength);
        //mysqlcom.Parameters["@prijs"].Value = anAuto.Prijs;
        //mysqlcom.Parameters.Add("@afbeelding", MySqlDbType.VarChar, columnLength);
        //mysqlcom.Parameters["@afbeelding"].Value = anAuto.Afbeelding;
        //mysqlcom.Parameters.Add("@categorie", MySqlDbType.VarChar, columnLength);
        //mysqlcom.Parameters["@categorie"].Value = anAuto.Categorie;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void addLesson(Lesson newLesson)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tbllessen (naam, beschrijving, leerkracht, leden, datum, afbeelding, niveau, kostprijs) VALUES (@naam, @beschrijving, @leerkracht, @leden, @datum, @afbeelding, @niveau, @kostprijs);";
        mysqlcom.Parameters.Add("@naam", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@naam"].Value = newLesson.Naam;
        mysqlcom.Parameters.Add("@beschrijving", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@beschrijving"].Value = newLesson.Beschrijving;
        mysqlcom.Parameters.Add("@leerkracht", MySqlDbType.VarChar, 75);
        mysqlcom.Parameters["@leerkracht"].Value = newLesson.Leerkracht;
        mysqlcom.Parameters.Add("@leden", MySqlDbType.VarChar, 100);
        mysqlcom.Parameters["@leden"].Value = newLesson.Leden;
        mysqlcom.Parameters.Add("@datum", MySqlDbType.VarChar, 125);
        mysqlcom.Parameters["@datum"].Value = newLesson.Datum;
        mysqlcom.Parameters.Add("@afbeelding", MySqlDbType.VarChar, 150);
        mysqlcom.Parameters["@afbeelding"].Value = newLesson.Afbeelding;
        mysqlcom.Parameters.Add("@niveau", MySqlDbType.VarChar, 175);
        mysqlcom.Parameters["@niveau"].Value = newLesson.Niveau;
        mysqlcom.Parameters.Add("@kostprijs", MySqlDbType.VarChar, 200);
        mysqlcom.Parameters["@kostprijs"].Value = newLesson.Kostprijs;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void addBlogItem(BlogItem newBlogItem)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tblblog (title, content, imageurl, author, uploaddate) VALUES (@title, @content, @imageurl, @author, @uploaddate);";
        mysqlcom.Parameters.Add("@title", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@title"].Value = newBlogItem.Title;
        mysqlcom.Parameters.Add("@content", MySqlDbType.VarChar, 2000);
        mysqlcom.Parameters["@content"].Value = newBlogItem.Content;
        mysqlcom.Parameters.Add("@author", MySqlDbType.VarChar, 45);
        mysqlcom.Parameters["@author"].Value = newBlogItem.Author;
        mysqlcom.Parameters.Add("@imageurl", MySqlDbType.VarChar, 75);
        mysqlcom.Parameters["@imageurl"].Value = newBlogItem.imageUrl;
        mysqlcom.Parameters.Add("@uploaddate", MySqlDbType.DateTime, 25);
        mysqlcom.Parameters["@uploaddate"].Value = newBlogItem.CreationDate;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void addComment(Comment newComment)
    {
        try
        {
            //Maak connectie met de databank
            MySqlConnection mysqalcon = new MySqlConnection();
            MySqlCommand mysqlcom = new MySqlCommand();
            mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
            //Commando-object
            mysqlcom.Connection = mysqalcon;
            mysqlcom.CommandText = "INSERT INTO tblcomments (blogid, text) VALUES (@id, @text);";
            mysqlcom.Parameters.Add("@text", MySqlDbType.VarChar, 25);
            mysqlcom.Parameters["@text"].Value = newComment.Text;
            mysqlcom.Parameters.Add("@id", MySqlDbType.Int32, 25);
            mysqlcom.Parameters["@id"].Value = newComment.BlogId;
            mysqalcon.Open();
            mysqlcom.ExecuteNonQuery();
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
    public static void addFaq(string strVraag, string strAntwoord)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tblfaq (vraag, antwoord) VALUES (@vraag, @antwoord);";
        mysqlcom.Parameters.Add("@vraag", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@vraag"].Value = strVraag;
        mysqlcom.Parameters.Add("@antwoord", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@antwoord"].Value = strAntwoord;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void deleteAuto(string strID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "DELETE FROM tblinstrumenten WHERE id = @id;";
        mysqlcom.Parameters.Add("@id", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@id"].Value = strID;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void deleteLesson(string strID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "DELETE FROM tbllessen WHERE id = @id;";
        mysqlcom.Parameters.Add("@id", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@id"].Value = strID;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void deleteBlogItem(string strID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "DELETE FROM tblblog WHERE id = @id;";
        mysqlcom.Parameters.Add("@id", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@id"].Value = strID;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static Klant readKlant(string strID)
    {
        Klant aKlant = new Klant();
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "select * from tblklanten where id = @id";
        mysqlcom.Parameters.Add("@id", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@id"].Value = strID;
        try
        {
            mysqalcon.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        MySqlDataReader reader = mysqlcom.ExecuteReader();
        if (reader.Read())
        {
            aKlant.ID = Convert.ToInt32(reader["id"]);
            aKlant.Voornaam = reader["voornaam"].ToString();
            aKlant.Achternaam = reader["achternaam"].ToString();
            aKlant.Mail = reader["mail"].ToString();
        }
        try
        {
            mysqalcon.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

        return aKlant;
    }
    public static void editKlant(string strID, string strVariabele, string strColumn)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "UPDATE tblklanten SET " + strColumn + " = @variabele WHERE id = " + strID + ";";
        mysqlcom.Parameters.Add("@variabele", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@variabele"].Value = strVariabele;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void addKlant(Klant newKlant)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tblklanten (voornaam, achternaam, mail) VALUES (@voornaam, @achternaam, @mail);";
        mysqlcom.Parameters.Add("@voornaam", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@voornaam"].Value = newKlant.Voornaam;
        mysqlcom.Parameters.Add("@achternaam", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@achternaam"].Value = newKlant.Achternaam;
        mysqlcom.Parameters.Add("@mail", MySqlDbType.VarChar, 75);
        mysqlcom.Parameters["@mail"].Value = newKlant.Mail;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static void addInschrijving(string strKlantID, string strLesID)
    {
        //Maak connectie met de databank
        MySqlConnection mysqalcon = new MySqlConnection();
        MySqlCommand mysqlcom = new MySqlCommand();
        mysqalcon.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[strConnstring].ToString();
        //Commando-object
        mysqlcom.Connection = mysqalcon;
        mysqlcom.CommandText = "INSERT INTO tblinschrijvingen (klantid, lesid) VALUES (@klantid, @lesid);";
        mysqlcom.Parameters.Add("@klantid", MySqlDbType.VarChar, 25);
        mysqlcom.Parameters["@klantid"].Value = strKlantID;
        mysqlcom.Parameters.Add("@lesid", MySqlDbType.VarChar, 50);
        mysqlcom.Parameters["@lesid"].Value = strLesID;
        mysqalcon.Open();
        mysqlcom.ExecuteNonQuery();
        mysqalcon.Close();
    }
    public static bool CheckIfKlantIsAdded(Klant aKlant)
    {
        string strCommand = "SELECT id FROM tblklanten;";
        List<string> klantID = readColumn(strCommand, "id");
        Klant tempKlant = new Klant();
        bool AlreadyExists = false;
        if (klantID.Count != 0)
        {
            foreach (string id in klantID)
            {
                tempKlant = readKlant(id);
                if (tempKlant.Voornaam == aKlant.Voornaam && tempKlant.Achternaam == aKlant.Achternaam)
                {
                    AlreadyExists = true;
                    if (tempKlant.Mail != aKlant.Mail && aKlant.Mail != "")
                    {
                        editKlant(id, aKlant.Mail, "mail");
                    }
                    break;
                }
            }
        }

        return AlreadyExists;
    }
}