<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Lesinfo.aspx.cs" Inherits="Lesinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="Lesinfo">
        <div id="lesinfodiv" class="lesinfodiv">
            <div id="llesinfo">
                <asp:Image ID="imgles" runat="server" ImageUrl="images/lessen/viool.jpg"></asp:Image>
            </div>
            <div id="rlesinfo">
                <div>
                    <asp:Label ID="lblLesnaam" runat="server" Text="DeLesNaam"></asp:Label>
                    <div>
                        <asp:Label ID="lblNiveau" runat="server" Text="HetNiveau"></asp:Label>
                        <asp:Label ID="lblLeerkracht" runat="server" Text="De leerkracht"></asp:Label>
                        <asp:Label ID="lblDatum" runat="server" Text="DeDatum"></asp:Label>
                        <asp:Label ID="lblLeden" runat="server" Text="AantalLeden"></asp:Label>
                    </div>
                    <p id="ples" runat="server">Dit is een lange tekst dat niet vanuit code behind in deze site is gezet.</p>
                    <a class="button" href="#Inschrijven">Neem deel</a>
                </div>
            </div>
        </div>
    </section>
    <section id="Standaardinfo">
        <div id="standaardinfodiv">
            <div>
                <h2>Bijkomende info</h2>
                <p>
                    De betaling van de les wordt via mail besproken samen met het contract omdat dit vaak op maat
moet gebeuren. Voor verdere vragen vermeld dit in de mail of bereik ons telefonisch.
                </p>
            </div>
        </div>
    </section>
    <section id="Inschrijven">
        <div id="inschrijvendiv">
            <div>
                <h2>Schrijf je in</h2>
                <asp:Label ID="_lblFirstname" runat="server" Text="Voornaam:"></asp:Label>
                <asp:TextBox ID="txtVoornaam" runat="server" CssClass="txt" required="true"></asp:TextBox>
                <asp:Label ID="_lblLastname" runat="server" Text="Achternaam:"></asp:Label>
                <asp:TextBox ID="txtAchternaam" runat="server" CssClass="txt" required="true"></asp:TextBox>
                <asp:Label ID="_lblMail" runat="server" Text="Mail:"></asp:Label>
                <asp:TextBox ID="txtMail" runat="server" CssClass="txt" required="true"></asp:TextBox>
                <label class="cblbl">
                    Ik ga akkoord met de&nbsp;<a href="./docs/Privacy%20Policy.pdf" target="_blank">privacyverklaring</a>
                <input type="checkbox" class="cb" id="cbAkkoord" required>
                    <span class="cbspan"></span>
                </label>
                <asp:Button ID="btnInschrijven" runat="server" Text="Schrijf je in" CssClass="button" OnClick="btnInschrijven_Click" />
            </div>
        </div>
    </section>
</asp:Content>

