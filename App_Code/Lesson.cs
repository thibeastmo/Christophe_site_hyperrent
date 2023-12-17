using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Lesson
/// </summary>
public class Lesson
{
    public int ID { get; set; }
    public string Naam { get; set; }
    public string Beschrijving { get; set; }
    public string Leerkracht { get; set; }
    public int Leden { get; set; }
    public string Datum { get; set; }
    public string Afbeelding { get; set; }
    public int Niveau { get; set; }
    public string Kostprijs { get; set; }
}