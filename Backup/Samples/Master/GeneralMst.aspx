<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GeneralMst.aspx.cs" Inherits="ITHeart.GeneralMst" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Default.css" type="text/css"  rel="Stylesheet" />
</head>
<body>

    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm1" runat="server"></asp:ScriptManager>
    <div id ="divGenMst">
    <asp:UpdatePanel ID="pnlGeneralMaster" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger controlid="SaveButton" eventname="Click" />
        </Triggers>
     <ContentTemplate>
        <table border="0" cellpadding="0" cellspacing="0" style ="width: 100%;"  >
            <tr><td colspan="3" class="FourPX" width="100%">&nbsp;</td></tr>
            <tr>
                <td width="24%">&nbsp;&nbsp;&nbsp;</td>
                <td nowrap width="52%" valign="top">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" style="background-color : #fBfBfB; border: 1pt solid silver;" class="silver Border">
                        <!------General Master Header -------------------------------------------->
                        <tr style="height: 24px">
                            <td nowrap colspan="7" align="left" class="DarkText PX12 Bold">
                                &nbsp;<asp:Label ID ="lblGenMstNm" runat ="server"></asp:Label>&nbsp;
                            </td>
                        </tr>
                        <!--------------------------------------------------------------->
                    
                        <!--====== Details ================================================================-->
                        <tr><td colspan="7" class="FourPX">&nbsp;</td></tr>
			            <tr>
                            <td width="1%" valign="top" class="FourPX">&nbsp;&nbsp;</td>
                            <td colspan="5" valign="top" class="">
                                 <asp:Panel ID ="pnlMain" runat ="server" >
                                  </asp:Panel>
                            </td>
                            <td width="1%" valign="top" class="FourPX">&nbsp;&nbsp;</td>
                        </tr>
                        <!--===============================================================================-->
                     
                        <!------ Save Button -------------------------------------------->
                        <tr><td colspan="7" class="FourPX">&nbsp;</td></tr>
                        <tr>
                            <td width="1%">&nbsp;</td>
                            <td nowrap align="left" width="49%" colspan="2">
                                <asp:Button ID="SaveButton" runat="server"  Text="  Save  " ValidationGroup="Master" SkinId="btnSubmit" onclick="SaveButton_Click"  />
                                <asp:UpdateProgress runat="server" id="UpdateProgressGeneralDefinition" AssociatedUpdatePanelID="pnlGeneralMaster" DisplayAfter="1" >
                                    <ProgressTemplate><img src="../Images/loading9.gif" /></ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                            <td align="left" width="50%" colspan="3">
                                <span runat="server" id="spnFailureTextPrompt" class="FailureText">
                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="true" ></asp:Literal></span>
                                <span runat="server" id="spnSuccessTextPrompt" class="DarkText PX12" style="white-space:normal">
                                    <asp:Literal ID="SuccessText" runat="server" EnableViewState="true" ></asp:Literal></span>
                                <span class="FailureText">
                                    <asp:ValidationSummary ID="ValidationSummaryGeneralDefinition" runat="server" ValidationGroup="ComplianceGeneralDefinition"  ShowSummary="true"  /></span>
                            </td>
                        </tr>
                        <tr><td colspan="7" class="FourPX">&nbsp;</td></tr>
                        <!--------------------------------------------------------------->
                    </table>
                </td>
                <td width="24%">&nbsp;&nbsp;&nbsp;</td>
            </tr>
        </table>
     </ContentTemplate>
     </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
