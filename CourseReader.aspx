<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CourseReader.aspx.cs" Inherits="CourseReader" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <section id="courseReaderDiv">
        <asp:Literal ID="embedPdf" runat="server"/>
    </section>
</asp:Content>

