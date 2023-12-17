<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Faq.aspx.cs" Inherits="Faq" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="faqdiv">
        <div id="underfaq"></div>
        <div>
            <div>
                <audio controls><source src="docs/interview_opname.mp3" type="audio/mp3"</audio>
            </div>

            <table style="color:GhostWhite;background-color:Transparent;border-color:Transparent;border-width:0px;border-style:None;border-collapse:collapse;" cellspacing="0" cellpadding="4">
		<tbody>
            <tr>
    <td class="vraag">Van waar komt Christophe zijn passie voor auto's?</td>
    <td class="antwoord">Hij heeft zijn passie voor auto's overgeërfd van zijn vader.</td>
</tr>
<tr>
    <td class="vraag">Wat is Christophe zijn favoriete auto?</td>
    <td class="antwoord"> De Porsche gt3 RS 992, het is een sportieve auto op semi deli driver, waardoor hij bestemd is voor dagelijks gebruik. Je kan er mee naar het werk rijden en ermee tegelijkertijd gebruiken als een race auto.</td>
</tr>
<tr>
    <td class="vraag">Wat is Christophe zijn favoriete automerk?</td>
    <td class="antwoord">Porsche, ze hebben heel uitlopende modellen, van oldtimers tot race auto's. Hij is er ook mee opgegroeid doordat zijn vader momenteel twee oldtimers bezit.</td>
</tr>
<tr>
    <td class="vraag">Wat is Christophe zijn mening over elektrische auto's</td>
    <td class="antwoord">  Een elektrische auto heeft zijn voor- en nadelen. Ze zijn duurzamer en sneller, maar je mist het gevoel en geluid van een benzine auto.</td>
</tr> 
<tr>
    <td class="vraag">Wat is Christophe zijn mening over hybride auto's?</td>
    <td class="antwoord">Ze waren heel populair, ze werden voornamelijk gebruikt voor het gewoon worden van de elektrische auto's. Ze gaan stilletjes aan verdwijnen van de markt.</td>
</tr> 
<tr>
    <td class="vraag">Wie hebben wij geïnterviewd?</td>
    <td class="antwoord">Christophe Proost, een 21 jarige business management student die een grote passie voor auto's heeft.</td>
</tr>
	</tbody></table>
        </div>
        <asp:SqlDataSource runat="server" ID="SqlDataSource1" ConnectionString='<%$ ConnectionStrings:connectionString %>' ProviderName='<%$ ConnectionStrings:connectionString.ProviderName %>' SelectCommand="SELECT * FROM tblfaq"></asp:SqlDataSource>
    </div>
</asp:Content>

