<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Info.aspx.cs" Inherits="Reparaties" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="Info">
        <div id="reparatiesinfodiv">
            <div id="p3" runat="server">
                <h2>Info nodig?</h2>
                <span>────</span>
                <%--<p id="p3" runat="server">
                    Heeft u een vraag of wilt twijfelt u over iets, aarzel dan niet om ons te contacteren. Dit kan via onze
garage zelf. U kan ons terugvinden in de Kronenburgstraat nummer 62 te Antwerpen. U kan ons
ook bereiken via info@hyperrent.be of telefonisch op het nummer 03 439 19 89. Als u een andere
specifieke vraag heeft kan u deze ook rechtstreeks naar ons sturen via een bericht hieronder.
                </p>--%>
            </div>
        </div>
    </section>
    <section id="Soorten">
        <div id="reparatiesoortendiv">
            <div>
                <div>
                    <img src="images/herve.jpg" />
                    <div>
                        <h3>Hervé L'Ecluse</h3>
                         <p>
                            Ik ben Hervé L'Ecluse. Ik ben 20 jaar en ben een student aan de kdg hoge school. Ik ben een energiek een positiefe persoon met soms een te luide stem. Als hobby heb ik gamen uit gaan met vrienden en reizen. Het liefste speel ik bordspellen met vrienden en familie. Ik zie mij ook later in de toekomst as game designer werken dat is toch het doel op dit moment. Ik werk ook op dit moment in de horica bij Nordica 31.
                        </p>
                    </div>
                </div>
                <div>
                    <div>
                        <h3>Thimo Mortelmans</h3>
                        <p>
                            Ik ben Thimo Mortelmans en ik ben geboren in het jaar 2001. Tijdens mijn vrije tijd houd ik mij constant bezig met programmeren, games, uitgaan of boulderen. Op vlak van programmeren ben ik constant bezig met allerlei projectjes voor mezelf om bij te leren en om het leven makkeljker/leuker te maken. Ik ben redelijk sportief want ik heb 8 jaar meegespeeld in de A ploeg van een jaartje/niveau hoger dan mijn leeftijdsgenoten. Ooit heb ik zelfs meegedaan met een ploeg die waarvan de gemiddelde speler 2 jaar ouder was dan mij. Telkens waren wij in de top 3 geëindigd van onze competitie. Na mijn voetbalcarrière had ik zin fitnes en heb dat dus 1 jaar zeer intensief gedaan. Om je een idee te geven, ik ben van 73kg naar 81kg gestegen door puur mijn spiermassa te verhogen. Enkele maanden geleden ben ik begonnen met wekelijks te gaan boulderen. Daar ben ik momenteel op het niveau van 6b. Dat is vrij goed voor nog maar enkele maanden bezig te zijn.
                        </p>
                    </div>
                    <img src="images/mezelf.jpg" />
                </div>
                <div>
                    <img src="images/shihab.png" />
                    <div>
                        <h3>Shihab Ouassil</h3>
                        <p>
                             ik ben 19 jaar en heb Marokkaanse roots. Ik studeer T.I. (Toegepaste Informatica) op Karel de Grote hogeschool. Mijn hobby's zijn voetbal en schaken, fitness doe ik ook wekelijks, ik hou ook van reizen. Tijdens weekends ben ik meestal buiten met vrienden. Ik ben flexibel en  toegewijd aan projecten waar aan ik begin. Later zou ik graag een eigen bedrijf willen starten met een paar vrienden.
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section id="Reparatieaanvraag">
        <div id="reparatieaanvraagdiv">
            <h2>Verstuur uw vraag</h2>
            <div>
                <div>
                    <asp:Label ID="_lblFirstname" runat="server" Text="Voornaam:"></asp:Label>
                    <asp:TextBox ID="txtVoornaam" runat="server" CssClass="txt" required="true"></asp:TextBox>
                    <asp:Label ID="_lblLastname" runat="server" Text="Achternaam:"></asp:Label>
                    <asp:TextBox ID="txtAchternaam" runat="server" CssClass="txt" required="true"></asp:TextBox>
                    <asp:Label ID="_lblMail" runat="server" Text="Mail:"></asp:Label>
                    <asp:TextBox ID="txtMail" runat="server" CssClass="txt" required="true"></asp:TextBox>
                    <asp:Label ID="_lblMessage" runat="server" Text="Bericht:"></asp:Label>
                    <asp:TextBox ID="txtBericht" runat="server" CssClass="txtmulti" TextMode="multiline" required="true"></asp:TextBox>
                    <label class="cblbl">
                        Ik ga akkoord met de&nbsp;<a href="./docs/Privacy%20Policy.pdf" target="_blank">privacyverklaring</a>
                        <input type="checkbox" class="cb" id="cbAkkoord" required>
                        <span class="cbspan"></span>
                    </label>
                    <asp:Button ID="btnSend" runat="server" Text="Verstuur" CssClass="button" OnClick="btnSend_Click" />
                </div>
                <div id="raanvraag">
                    <div id="p6" runat="server">
                        <span>─────</span>
                        <h3>Let op!</h3>
                        <p>
                            Wij proberen u vraag zo snel mogelijk te beantwoorden. Als u een uitgebreide uitleg wilt over de
auto’s of ze in het echt wilt zien kan u gerust langkomen op ons adres in Antwerpen. Het overlopen
van het contract verbonden met de verhuur gebeurt altijd via mail en moet worden afgetekend in de
garage bij vertrek. Hetzelfde is ook van toepassing bij de lessen.
                        </p>
                        <span>─────</span>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>

