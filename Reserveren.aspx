<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Reserveren.aspx.cs" Inherits="Kopen" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="categorieën">
        <div id="categorieendiv">
            <h2>Selecteer een categorie</h2>
            <div>
                <a href="#Porsche" class="soortinstrument">
                    <div>
                        <img src="images/Porsche.png" />
                        <span>•</span>
                        <h3>Porsche</h3> <%--Om deze woorden op een goede manier te breken wanneer het te klein word zijn er speciale tekens in gezet (https://www.w3schools.com/cssref/tryit.asp?filename=trycss3_hyphens --> zie rode bollen in tekst)--%> 
                    </div>
                </a>
                <a href="#Ferrari" class="soortinstrument">
                    <div>
                        <img src="images/Ferrari.png" />
                        <span>•</span>
                        <h3>Ferrari</h3>
                    </div>
                </a>
                <a href="#Lamborghini" class="soortinstrument">
                    <div>
                        <img src="images/Lamborghini.png" />
                        <span>•</span>
                        <h3>­­­Lamborghini</h3>
                    </div>
                </a>
            </div>
        </div>
    </section>
    <section id="Porsche">
        <div id="dvPorsche" runat="server" class="blackinstrumentendiv dvinstrumenten">
            <%--<div>
                <img src="images/instrumenten/banjo1.png" />
                <span>─────</span>
                <h4>Banjo</h4>
                <h4>€29.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/basgitaar1.png" />
                <span>─────</span>
                <h4>Basgitaar</h4>
                <h4>€59.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/cello1.png" />
                <span>─────</span>
                <h4>Cello</h4>
                <h4>€49.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/gitaar1.png" />
                <span>─────</span>
                <h4>Elektrische gitaar</h4>
                <h4>€89.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/contrabas1.png" />
                <span>─────</span>
                <h4>Contrabas</h4>
                <h4>€79.99</h4>
            </div>--%>
        </div>
    </section>
    <section id="Ferrari">
        <div id="dvFerrari" runat="server" class="whiteinstrumentendiv dvinstrumenten">
            <%--<div>
                <img src="images/instrumenten/bass_drumm1.png" />
                <span>─────</span>
                <h4>Bass drum</h4>
                <h4>€39.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/congaset1.png" />
                <span>─────</span>
                <h4>Congaset</h4>
                <h4>€19.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/djembe1.png" />
                <span>─────</span>
                <h4>Djembe</h4>
                <h4>€69.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/trommel.png" />
                <span>─────</span>
                <h4>Trommel</h4>
                <h4>€9.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/xylofoon1.png" />
                <span>─────</span>
                <h4>Xylofoon</h4>
                <h4>€129.99</h4>
            </div>--%>
        </div>
    </section>
    <section id="Lamborghini">
        <div id="dvLamborghini" runat="server" class="blackinstrumentendiv dvinstrumenten">
            <%--<div>
                <img src="images/instrumenten/dwarsfluit1.png" />
                <span>─────</span>
                <h4>Dwarsfluit</h4>
                <h4>€49.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/euphonium1.png" />
                <span>─────</span>
                <h4>Euphonium</h4>
                <h4>€109.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/klarinet1.png" />
                <span>─────</span>
                <h4>Klarinet</h4>
                <h4>€99.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/saxofoon1.png" />
                <span>─────</span>
                <h4>Saxofoon</h4>
                <h4>€79.99</h4>
            </div>
            <div>
                <img src="images/instrumenten/trompet1.png" />
                <span>─────</span>
                <h4>Trompet</h4>
                <h4>€49.99</h4>
            </div>--%>
        </div>
    </section>
   <%-- <section id="Reserveren">
        <div id="reserverendiv">
            <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div>
                        <asp:Label ID="_lblFirstname" runat="server" Text="Voornaam:"></asp:Label>
                        <asp:TextBox ID="txtVoornaam" runat="server" CssClass="txt" required="true"></asp:TextBox>
                        <asp:Label ID="_lblLastname" runat="server" Text="Achternaam:"></asp:Label>
                        <asp:TextBox ID="txtAchternaam" runat="server" CssClass="txt" required="true"></asp:TextBox>
                        <asp:Label ID="_lblMail" runat="server" Text="Mail:"></asp:Label>
                        <asp:TextBox ID="txtMail" runat="server" CssClass="txt" required="true"></asp:TextBox>
                        <asp:DropDownList ID="ddlInstrumenten" runat="server" CssClass="ddl" OnSelectedIndexChanged="ddlInstrumenten_SelectedIndexChanged" AutoPostBack="true" required="true" ForeColor="#303030" BackColor="GhostWhite"></asp:DropDownList>
                        <label class="cblbl">
                    Ik ga akkoord met de&nbsp;<a href="./docs/Privacy%20Policy.pdf" target="_blank">privacyverklaring</a>
                <input type="checkbox" class="cb" id="cbAkkoord" required>
                    <span class="cbspan"></span>
                </label>
                        <asp:Button ID="btnReserveren" runat="server" Text="Reserveer nu" CssClass="button" OnClick="btnReserveren_Click" />
                    </div>
                    <div>
                        <div>
                            <asp:Image ID="imgPreview" runat="server" />
                        </div>
                        <asp:Label ID="lblPrijs" runat="server" Text="Prijs: €0,00"></asp:Label>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </section>--%>
</asp:Content>

