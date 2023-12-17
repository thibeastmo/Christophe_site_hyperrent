<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="logindiv">
        <div id="underlogin"></div>
        <div>
            <asp:Label runat="server" AssociatedControlID="UserName" ID="UserNameLabel" CssClass="loginlbl">Gebruikersnaam:</asp:Label>
            <asp:TextBox runat="server" ID="UserName" CssClass="txtlogin"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="UserName" ErrorMessage="Gebruikersnaam is verplicht." ValidationGroup="Login1" ToolTip="Gebruikersnaam is verplicht." ID="UserNameRequired">*</asp:RequiredFieldValidator>
            <asp:Label runat="server" AssociatedControlID="Password" ID="PasswordLabel" CssClass="loginlbl">Wachtwoord:</asp:Label>
            <asp:TextBox runat="server" TextMode="Password" ID="Password" CssClass="txtlogin"></asp:TextBox>
            <asp:RequiredFieldValidator runat="server" ControlToValidate="Password" ErrorMessage="Wachtwoord is verplicht." ValidationGroup="Login1" ToolTip="Wachtwoord is verplicht." ID="PasswordRequired">*</asp:RequiredFieldValidator>
            <%--<asp:CheckBox runat="server" Text="Onthoud mij" ID="RememberMe" Checked="true"></asp:CheckBox>--%>
            <div>
                <label class="cblbl">
                    Onthoud mij
                <input type="checkbox" class="cb" checked="checked" id="RememberMe" runat="server">
                    <span class="cbspan"></span>
                </label>
            </div>
            <asp:Literal runat="server" ID="FailureText" EnableViewState="False"></asp:Literal>
            <asp:Button runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1" ID="LoginButton" CssClass="button midlogin" OnClick="LoginButton_Click"></asp:Button>

        </div>
    </div>
</asp:Content>

