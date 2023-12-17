<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Autoinfo.aspx.cs" Inherits="Autoinfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="Autoinfo">
        <div id="autoinfodiv" class="autoinfodiv">
            <div id="lautoinfo">
                <asp:Image ID="imgauto" runat="server" ImageUrl="images/autosen/viool.jpg"></asp:Image>
            </div>
            <div id="rautoinfo">
                <div id="rautoinfoinnerdiv" runat="server">
                    <%--<asp:Label ID="lblAutonaam" runat="server" Text="DeAutoNaam"></asp:Label>--%>
                    <div id="autospecs" runat="server">
                        <%-- Auto generated labels come here --%>
                    </div>
                    <p id="pauto" runat="server">Dit is een lange tekst dat niet vanuit code behind in deze site is gezet.</p>
                    <a class="button" href="#Reserveren">Reserveer</a>
                </div>
            </div>
        </div>
    </section>
    <section id="Standaardinfo">
        <div id="standaardinfodiv">
            <div>
                <h2>Bijkomende info</h2>
                <p>
                    De betaling van het reserveren wordt via mail besproken samen met het contract omdat dit vaak op maat
moet gebeuren. Voor verdere vragen vermeld dit in de mail of bereik ons telefonisch.
                </p>
            </div>
        </div>
    </section>
    <section id="Reserveren">
        <div id="inschrijvendiv">
            <div>
                <h2>Reserveer</h2>
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
                <asp:Button ID="btnInschrijven" runat="server" Text="Reserveer nu" CssClass="button" OnClick="btnInschrijven_Click" />
            </div>
        </div>
    </section>
</asp:Content>

