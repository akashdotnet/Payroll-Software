<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePickerControl.ascx.cs" Inherits="FinORB_Web.UserControl.DatePickerControl" %>
<div style="width:100%;" runat="server" id="divDatePickerControl" >  
<table cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td nowrap align="left" valign="top" runat="server" id="tdDatePickerControl" style="border: 0pt none transparent">
            <table cellpadding="0" cellspacing="0" width="100%" border="0" >
                <tr>
                    <td align ="left" valign="middle" runat="server" id="tdDatePickerControl0">
                         <input type="text" id="txtDatePickerControl" runat="server" maxlength="11" class="Question11Px" style="border: none; background-color: transparent;  height:18px" />
                    </td>
                     <td align="left" valign="middle" runat="server" id="tdDatePickerControl1" style="visibility:visible" >
                         <img id='imgDatePickerControl' runat="server" width="15" style="cursor:pointer; visibility:hidden"  />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</div>