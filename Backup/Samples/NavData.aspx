<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="NavData.aspx.cs" Inherits="Samples.NavData" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <div id="divNavData">
   <asp:UpdatePanel ID="updReport" runat="server">
            <ContentTemplate>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <!--------- Report Header -------------------------------------------->
                    <tr height="25px">
                        <td width="100%" colspan="6" align="left" valign="middle" class="silver BorderBottom">
                            <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                <tr>
                                    <td width="1%" align="left" class="FourPX" style="">
                                       <asp:Button ID="btnCreateNew" runat="server" Text="Create New" onclick="btnCreateNew_Click" />
                                    </td>
                                    <td nowrap width="1%" align="left" valign="bottom" class="Questions18Px">
                                        <asp:Literal ID="ReportTitle" runat="server"></asp:Literal>&nbsp;&nbsp;
                                    </td>
                                    <td width="98%" colspan="4" align="center" valign="bottom" class="Questions11Px Bold">
                                        <asp:UpdateProgress runat="server" ID="PageUpdateProgressReport" AssociatedUpdatePanelID="updReport"
                                            DisplayAfter="1">
                                            <ProgressTemplate>
                                                <img src="../Images/loading9.gif" alt="Processing, Please wait..." /></ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:Literal ID="ReportHeader" runat="server"></asp:Literal>&nbsp;
                                    </td>
                                    <td nowrap width="1%" align="left" valign="middle" class="Questions11Px" runat="server"
                                        id="tdGchart">
                                    </td>
                                    <td nowrap width="1%" align="left" valign="middle" class="Questions11Px" runat="server"
                                        id="tdToolbar">
                                        
                                        <asp:RadioButtonList runat="server" ID="rdTool" RepeatDirection="Horizontal"  AutoPostBack="true" CssClass="Border DarkText PX11" />
                                            <asp:HiddenField ID="hdTool" runat="server" />
                                            <asp:HiddenField ID="hdSelection" runat="server" />
                                            <asp:HiddenField ID="hdPageSize" runat="server" Value="0" />
                                    </td>
                                    <td>
                                      <table width="10%">
                                             <tr>
                                                 <td>Search</td>
                                                 <td><asp:TextBox ID="txtSearchOn" runat="server"></asp:TextBox></td>
                                                 <td> <asp:Button ID="btnGo" runat="server" Text="GO"  /></td>
                                             </tr>
                                         </table> 
                                     </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <!-------------------------------------------------------------------->
                    <!--------- Report Message ------------------------------------------->
                    <tr runat="server" id="trReportMessage">
                        <td width="100%" colspan="6" align="left" valign="middle">
                            <span runat="server" id="spnFailureTextPrompt" class="FailureText">
                                <asp:Literal ID="FailureText" runat="server" EnableViewState="true"></asp:Literal></span>
                            <span runat="server" id="spnSuccessTextPrompt" class="DarkText PX12" style="white-space: normal">
                                <asp:Literal ID="SuccessText" runat="server" EnableViewState="true"></asp:Literal></span>
                        </td>
                    </tr>
                    <!-------------------------------------------------------------------->
                </table>
                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                    <!--------- Report Calender ---------------------------------------------->
                    <tr runat="server" id="trCal" visible="false">
                        <td width="100%" colspan="6" align="center" valign="top" runat="server" id="tdCal"
                            class="">
                            
                        </td>
                    </tr>
                    <!--------- Report Data ---------------------------------------------->
                    <tr runat="server" id="trReportData">
                        <td width="100%" colspan="6" align="left" valign="top" runat="server" id="tdReport"
                            class="">
                            <asp:Panel Width="100%" Height="340px" ScrollBars="Vertical" runat="server" ID="pnlReport"
                                CssClass="">
                                <asp:GridView ID="gvNav" runat="server" AutoGenerateColumns="False" 
                                    BackColor="White" BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" 
                                    CellPadding="3" CellSpacing="1" GridLines="None" 
                                    onrowdatabound="gvNav_RowDataBound">
                                
                                    <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
                                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                                    <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
                                    <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
                                    <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#594B9C" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#33276A" />
                                
                                </asp:GridView>
                            </asp:Panel>
                        </td>
                    </tr>
                    <!-------------------------------------------------------------------->
                    <!--------- Report Chart --------------------------------------------->
                    <tr runat="server" id="trReportChart">
                        <td width="100%" colspan="6" align="center" valign="top" runat="server" id="tdChart">
                        </td>
                    </tr>
                    <!-------------------------------------------------------------------->
                    <!--------- Report Footer -------------------------------------------->
                    <tr runat="server" id="trReportFooter">
                        <td nowrap width="100%" colspan="6" align="left" class="Questions11Px BorderTop">
                            <br class="FourPX" />
                            <asp:Literal ID="ReportFooter" runat="server"></asp:Literal><br />
                            <br class="FourPX" />
                        </td>
                    </tr>
                    <!-------------------------------------------------------------------->
                </table>
                <asp:Button ID="btnRefresh" runat="server" Text="Refresh" Font-Bold="true" Width="55px"
                     />
            </ContentTemplate>
        </asp:UpdatePanel>
  </div>
</asp:Content>
