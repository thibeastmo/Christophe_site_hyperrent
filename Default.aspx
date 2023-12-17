<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="intro">
        <div id="introdiv" runat="server">
            <%--<p id="p1" runat="server">
                Bent u opzoek naar een onvergetelijk moment met een echte super- en hypercar bent u op het
juiste adres. Ervaar zelf de luxe en kwaliteit van merken zoals Porsche, Ferrari en Lamborghini. Rijden
in deze racewagens op de openbare weg is een totaal andere beleving. Wilt u nog verder gaan en
lessen volgen op het circuit kan u ook altijd bij ons terecht. Ontdek de 2 legendarische circuits van
Zolder en Spa-Francorchamps als piloot van één van onze wagen. Krijg privéles om uw skills naar de
top te brengen.
            </p>--%>
            <iframe id="vp12K60Q" title="Video Player" frameborder="0" src="https://s3.amazonaws.com/embed.animoto.com/play.html?w=swf/production/vp1&e=1665744218&f=2K60Qnio4uIC0u834xfV8w&d=0&m=p&r=360x360+720x720&volume=100&start_res=360x360&i=m&asset_domain=s3-p.animoto.com&animoto_domain=animoto.com&options=autostart/loop" allowfullscreen></iframe>
        </div>
    </section>
    <section id="info">
        <div id="infodiv">
            <div>
                <div id="infoimg">
                    <img src="images/campus_national.png" />
                </div>
                <div id="p2" runat="server">
                    <h2>Wie zijn wij?</h2>
                     <%--<p <%--id="p2">
                       Hyperrent is een gloednieuwe garage in centrum Antwerpen die super- en
hypercars verhuurd. Onze passie zijn auto’s en wij willen deze passie delen.
Wilt u privélessen op het circuit als piloot met een echt supercar dan kan u
ook altijd bij ons terecht. Heeft u meer vragen bekijk dan even onze faq
pagina of stuur ons een bericht.
                    </p>--%>

                    <%--<p <%--id="p1">
                Bent u opzoek naar een onvergetelijk moment met een echte super- en hypercar bent u op het
juiste adres. Ervaar zelf de luxe en kwaliteit van merken zoals Porsche, Ferrari en Lamborghini. Rijden
in deze racewagens op de openbare weg is een totaal andere beleving. Wilt u nog verder gaan en
lessen volgen op het circuit kan u ook altijd bij ons terecht. Ontdek de 2 legendarische circuits van
Zolder en Spa-Francorchamps als piloot van één van onze wagen. Krijg privéles om uw skills naar de
top te brengen.
            </p>--%>
                    <%--<div>
                        <div>
                            <asp:Label ID="lblStraat" runat="server" Text="Kronenburgstraat 62"></asp:Label>
                            <asp:Label ID="lblGemeente" runat="server" Text="2000 Antwerpen"></asp:Label>
                            <asp:Label ID="lblMail" runat="server" href="info@hyperrent.be" Text="info@hyperrent.be"></asp:Label>
                            <asp:Label ID="lblTel" runat="server" Text="03 439 19 89" href="tel:034391989"></asp:Label>
                        </div>
                    </div>--%>
                </div>
            </div>
            <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2499.39219993784!2d4.395818016078253!3d51.211850340089846!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47c3f6ee1ec63ca7%3A0x1154e5dcdae1555a!2sKronenburgstraat%2062%2C%202000%20Antwerpen!5e0!3m2!1snl!2sbe!4v1641566337879!5m2!1snl!2sbe" height="250" frameborder="0" style="border: 0;" allowfullscreen="" aria-hidden="false" tabindex="0" loading="lazy"></iframe>
            <%--<iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d155.8371744322749!2d4.952097342851287!3d51.32225801173079!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x47c6b3d6bca58fff%3A0xc85be319d34d8fab!2sIndigo%20Park%20-%20Parking%20Turnova!5e0!3m2!1snl!2sbe!4v1592083041357!5m2!1snl!2sbe" height="250"></iframe>--%>
        </div>
    </section>
    <section id="contact">
        <div id="contactdiv">
            <div>
                <h2>Contacteer ons</h2>
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
        </div>
    </section>
</asp:Content>

