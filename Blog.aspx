<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Blog.aspx.cs" Inherits="Blog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="Soorten">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="reparatiesoortendiv">
                    <%--<div id="blogContent" runat="server"></div>--%>
                    <asp:PlaceHolder ID="blogContent" runat="server"></asp:PlaceHolder>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </section>
</asp:Content>

