<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HelpControl.ascx.cs" Inherits="ITHeart.UserControl.HelpControl" %>
<%@ Register Assembly="C1.Web.UI.Controls.2" Namespace="C1.Web.UI.Controls.C1FormDecorator" TagPrefix="c1" %>
<%@ Register Assembly="C1.Web.UI.Controls.2" Namespace="C1.Web.UI.Controls.C1Window" TagPrefix="c2" %>
<link  runat="server" id="HLPCSSID" rel="stylesheet"  type="text/css" />
<div >
<c1:C1FormDecorator runat="server" ID="HelpControlDecorator" VisualStylePath="App_Themes/" UseEmbeddedVisualStyles="true" VisualStyle="Arcticfox" DecorationZoneID="helpUserControl" />

  <div id ="helpUserControl">
  <asp:Panel ID ="pnlHelpControl"  BorderStyle="Solid"  runat ="server" BorderColor="LightGray"  BackColor="Transparent" BorderWidth="1px" Height="20">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" >
             <tr style ="width :100%;">
               <!-- Help Text Box And Link -->
               <td align ="left" valign="top" runat="server" id="tdMakeNullHelp" width="1%" >
                    <asp:CheckBox ID="chkMakeNullHelp"  runat ="server" AutoPostBack ="false"  Width="20"    />
               </td>
               <td style="white-space:nowrap" align ="left" style ="width :95%;" valign="middle">
                    <asp:TextBox ID ="txtHelp" runat ="server" EnableViewState ="true" ReadOnly="true" SkinId="txtNoBorderNoWidthNoBold"></asp:TextBox>
                </td>
               <td align ="left" style ="width :1%;" valign="middle" runat="server" id="tdhelp"   >
                    <img id='imghelp' runat="server" width="15" style="cursor:pointer"   />
               </td>
                 <td align ="left" style ="width :1%;" valign="top"   runat="server" id="tdReset"  > 
                       <img id='imgReset' runat="server" width="15" style="cursor:pointer"   />
                </td>
               </tr>
             <tr>
              <td colspan ="2" style ="width :1%;" >
                   <asp:HiddenField ID ="hdHelpSelectID"  runat ="server" EnableViewState ="true"    />
                   <asp:HiddenField ID ="hdValueChangeJS"  runat ="server" EnableViewState ="true"    />
                   <asp:HiddenField ID ="hdValueChangeJSParam"  runat ="server" EnableViewState ="true"    />
                    <asp:HiddenField ID ="hdLinkedObj"  runat ="server" EnableViewState ="true"   />
                    <asp:HiddenField ID ="hdLink"  runat ="server" EnableViewState ="true"   />
                     <asp:HiddenField ID ="hdMulti"  runat ="server" EnableViewState ="true"  />
                     <asp:HiddenField ID='hdDefaultText' runat="server" EnableViewState ="true" />
                     <asp:HiddenField ID='hdDefaultID' runat="server" EnableViewState ="true" />
                     <asp:HiddenField ID='hdAdditonalFiler' runat="server" EnableViewState ="true" />
                     <asp:HiddenField ID='hdEnabled' runat="server" EnableViewState="true" Value='1' />
                  </td>
             </tr>
        
             
    </table>
  
  </asp:Panel>
 
  </div> 
</div>
