<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PO_Receipt_H.ascx.cs"
    Inherits="WKF_OptionalFields_PO_Receipt_H" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.2, Version=6.2.20062.1079, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<style>
    .titleS
    {
        height: 25px;
        background: #328aa4;
        width: 100px;
        border-top: 1px solid #fff;
        border-left: 1px solid #fff;
        border-right: 1px solid #fff;
    }
    .titleS_2
    {
        height: 25px;
        background: #328aa4;
        width: 180px;
        border-top: 1px solid #fff;
        border-left: 1px solid #fff;
        border-right: 1px solid #fff;
    }
    
    .contentS2
    {
        height: 25px;
        background: #e5f1f4;
        width: 275px;
        border-top: 1px solid #fff;
    }
    .tb1pohead
    {
        background: #e5f1f4;
        width: 480px;
        border: 1px solid #E8E7F0;
    }
    .foot
    {
        height: 25px;
        background: #e5f1f4;
        border-top: 1px solid #fff;
    }
</style>
<script>
    function checkValueby(txt) {
        var r = /^\+?[1-9][0-9]*$/;
        var txtControl = document.getElementById('<%=  warring.ClientID%>');
        if (txt.value != "") {
            if (!r.test(txt.value)) {
                txt.value = "";
                txtControl.innerText = "採購單號須為數字";
            } else {
                txtControl.innerText = "";
            }
        } else {
            txtControl.innerText = "";
        }
    }

</script>
<table cellpadding="0" cellspacing="0" class="tb1pohead">
    <tr>
        <td class="titleS">
            <asp:Label ID="Label3" runat="server" Text="異動類型:" Font-Bold="True" Font-Names="微軟正黑體"
                ForeColor="White"></asp:Label>
        </td>
        <td class="contentS2">
            <asp:DropDownList ID="ddZMOVETYPE" runat="server" Height="16px" Width="272px" 
                AutoPostBack="True" 
                onselectedindexchanged="ddZMOVETYPE_SelectedIndexChanged">
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="titleS">
            <asp:Label ID="Label2" runat="server" Text="過帳日期:" Font-Bold="True" Font-Names="微軟正黑體"
                ForeColor="White"></asp:Label>
        </td>
        <td class="contentS2">
            <igsch:WebDateChooser ID="PSTNG_DATE" runat="server" meta:resourcekey="wdcDeliveryDateResource1"
                AllowNull="False" Height="19px" Value="07/23/2013 15:11:22" Width="95px">
            </igsch:WebDateChooser>
        </td>
    </tr>
    <tr>
        <td class="titleS">
            <asp:Label ID="Label6" runat="server" Text="文件日期:" Font-Bold="True" Font-Names="微軟正黑體"
                ForeColor="White"></asp:Label>
        </td>
        <td class="contentS2">
            <igsch:WebDateChooser ID="DOC_DATE" runat="server" meta:resourcekey="wdcDeliveryDateResource1"
                AllowNull="False" Height="19px" Value="07/23/2013 15:11:22" Width="95px">
            </igsch:WebDateChooser>
        </td>
    </tr>
    <tr>
        <td class="foot" colspan="2">
            <asp:Label ID="Label4" runat="server" Text="訊息:" Font-Bold="True" Font-Names="微軟正黑體"
                ForeColor="#666666"></asp:Label>
            <asp:Label ID="warring" runat="server" Font-Bold="True" Font-Names="微軟正黑體" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>
<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False"
    meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>