using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Instrument
/// </summary>
public class Auto
{
    public int ID { get; set; }
    public string Naam { get; set; }
    public string Prijs { get; set; }
    public string Afbeelding { get; set; }
    public string Categorie { get; set; }
    public int? Cilinders { get; set; }
    public double? Cilinderinhoud { get; set; }
    public int? Vermogen { get; set; }
    public int? Koppel { get; set; }
    public int? Versnellingen { get; set; }
    public int? Topsnelheid { get; set; }
    public double? Acceleratie { get; set; }
    public double? Tussenacceleratie { get; set; }
}