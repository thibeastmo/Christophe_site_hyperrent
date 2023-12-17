<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Beheerder.aspx.cs" Inherits="Beheerder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/Beheerder.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section id="Beheerder">
        <div id="beheerderdiv">
            <div>

                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btnVerwijder" />
                        <asp:PostBackTrigger ControlID="btnOpslagen" />
                        <asp:PostBackTrigger ControlID="btnToevoegen" />
                        <asp:PostBackTrigger ControlID="btnLesVerwijder" />
                        <asp:PostBackTrigger ControlID="btnLesOpslagen" />
                        <asp:PostBackTrigger ControlID="btnLesToevoegen" />
                        <asp:PostBackTrigger ControlID="btnBlogVerwijder" />
                        <asp:PostBackTrigger ControlID="btnBlogOpslagen" />
                        <asp:PostBackTrigger ControlID="btnBlogToevoegen" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:Panel ID="binstrumenten" CssClass="menuitem binstrumenten" runat="server">
                            <h2>Auto's</h2>
                            <div>
                                <div>
                                    <asp:DropDownList ID="ddlAutoCategorie" CssClass="bddl" runat="server" placeholder="Categorie" required="true" BackColor="#303030" ForeColor="GhostWhite">
                                        <asp:ListItem>Porsche</asp:ListItem>
                                        <asp:ListItem>Ferrari</asp:ListItem>
                                        <asp:ListItem>Lamborghini</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:FileUpload ID="fuAutoAfbeelding" CssClass="bfu" runat="server" />
                                    
                                    <asp:PlaceHolder ID="phAutoProperties" runat="server"></asp:PlaceHolder>
                                    <div class="bbottom">
                                        <asp:Button ID="btnVerwijder" runat="server" Text="Verwijder" CssClass="bbutton" OnClick="btnAutoVerwijder_Click" />
                                        <asp:Button ID="btnOpslagen" runat="server" Text="Opslagen" CssClass="bbutton" OnClick="btnAutoOpslagen_Click" />
                                        <asp:Button ID="btnToevoegen" runat="server" Text="Toevoegen" CssClass="bbutton" OnClick="btnAutoToevoegen_Click" />
                                    </div>
                                </div>
                                <div>
                                    <div id="blackinstrumentendiv" runat="server" class="instrumentPreview">
                                        <div>
                                            <asp:Image ID="instrumentimg" runat="server" />
                                            <span id="instrumentspan" runat="server">─────</span>
                                            <h4 id="instrumentnaam" runat="server"></h4>
                                            <h4 id="instrumentprijs" runat="server"></h4>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>



                        <asp:Panel ID="blessen" CssClass="menuitem blessen" runat="server">
                            <h2>Lessen</h2>
                            <div>
                                <div>
                                    <asp:TextBox ID="txtLesNaam" runat="server" placeholder="Naam" required="true" CssClass="btxt"></asp:TextBox>
                                    <asp:TextBox ID="txtLesLeerkracht" runat="server" placeholder="Leerkracht" required="true" CssClass="btxt"></asp:TextBox>
                                    <asp:TextBox ID="txtLesDatum" runat="server" placeholder="Datum" required="true" CssClass="btxt"></asp:TextBox>
                                    <asp:TextBox ID="txtLesKostprijs" runat="server" placeholder="Kostprijs" required="true" CssClass="btxt"></asp:TextBox>
                                    <asp:DropDownList ID="ddlLesNiveaus" CssClass="bddl" runat="server" AutoPostBack="true" required="true" BackColor="#303030" ForeColor="GhostWhite">
                                        <asp:ListItem>1</asp:ListItem>
                                        <asp:ListItem>2</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtLesBeschrijving" runat="server" placeholder="Korte eschrijving" TextMode="MultiLine" required="true" CssClass="bmultitxt btxt"></asp:TextBox>
                                    <asp:FileUpload ID="fuLesAfbeelding" CssClass="bfu" runat="server" />
                                    
                                    <div class="bbottom">
                                        <asp:DropDownList ID="ddlLessenLijst" CssClass="bddl" runat="server" OnSelectedIndexChanged="ddlLessenLijst_SelectedIndexChanged" AutoPostBack="true" BackColor="#303030" ForeColor="GhostWhite"></asp:DropDownList>
                                        <asp:Button ID="btnLesVerwijder" runat="server" Text="Verwijder" CssClass="bbutton" OnClick="btnLesVerwijder_Click" />
                                        <asp:Button ID="btnLesOpslagen" runat="server" Text="Opslagen" CssClass="bbutton" OnClick="btnLesOpslagen_Click" />
                                        <asp:Button ID="btnLesToevoegen" runat="server" Text="Toevoegen" CssClass="bbutton" OnClick="btnLesToevoegen_Click" />
                                    </div>
                                </div>
                                <div>
                                    <div id="lesdiv" runat="server" class="lesdiv">
                                        <div>
                                            <asp:Image ID="lessenimg" runat="server" />
                                            <h3 id="lestitel" runat="server"></h3>
                                            <p id="lessenp" runat="server"></p>
                                            <div>
                                                <asp:Button ID="btnLesBekijk" runat="server" Text="Bekijk" />
                                                <asp:Button ID="btnLesNivau" runat="server" Text="Niveau" />
                                                <span id="lesleden" runat="server"></span>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>


                        <asp:Panel ID="bblog" CssClass="menuitem bblog" runat="server">
                            <h2>Blog</h2>
                            <div>
                                <div>
                                    <asp:TextBox ID="txtBlogTitle" runat="server" placeholder="Naam" required="true" CssClass="btxt"></asp:TextBox>
                                    <asp:TextBox ID="txtBlogContent" runat="server" placeholder="Korte beschrijving" TextMode="MultiLine" required="true" CssClass="bmultitxt btxt"></asp:TextBox>

                                    <asp:FileUpload ID="fuBlogAfbeelding" CssClass="bfu" runat="server" />

                                    <asp:DropDownList ID="ddlAuthor" CssClass="bddl" runat="server" placeholder="Author" required="true" BackColor="#303030" ForeColor="GhostWhite">
                                        <asp:ListItem>Thimo</asp:ListItem>
                                        <asp:ListItem>Shihab</asp:ListItem>
                                        <asp:ListItem>Hervé</asp:ListItem>
                                    </asp:DropDownList>
                                    
                                    <asp:PlaceHolder ID="phBlogProperties" runat="server"></asp:PlaceHolder>
                                    <div class="bbottom">
                                        <asp:Button ID="btnBlogVerwijder" runat="server" Text="Verwijder" CssClass="bbutton" OnClick="btnBlogVerwijder_Click" />
                                        <asp:Button ID="btnBlogOpslagen" runat="server" Text="Opslagen" CssClass="bbutton" OnClick="btnBlogOpslagen_Click" />
                                        <asp:Button ID="btnBlogToevoegen" runat="server" Text="Toevoegen" CssClass="bbutton" OnClick="btnBlogToevoegen_Click" />
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div id="bChoose">
            <div id="underchoose"></div>
            <asp:Button ID="btnAutoen" runat="server" Text="" CssClass="btnAutoen btnBeheerder" OnClick="btnAutoen_Click" CausesValidation="false" UseSubmitBehavior="false" />
            <asp:Button ID="btnLessen" runat="server" Text="" CssClass="btnLessen btnBeheerder" OnClick="btnLessen_Click" CausesValidation="false" UseSubmitBehavior="false" />
            <asp:Button ID="btnBlog" runat="server" Text="" CssClass="btnBlog btnBeheerder" OnClick="btnBlog_Click" CausesValidation="false" UseSubmitBehavior="false" />
            <asp:Button ID="btnLogOut" runat="server" Text="" CssClass="btnLogOut btnBeheerder" OnClick="btnLogOut_Click"  CausesValidation="false" UseSubmitBehavior="false" />
        </div>
    </section>
</asp:Content>

