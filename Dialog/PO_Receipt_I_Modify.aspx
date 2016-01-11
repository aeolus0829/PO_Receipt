<%@ Page Title="" Language="C#" MasterPageFile="~/Master/DialogMasterPage.master"
    AutoEventWireup="true" CodeFile="PO_Receipt_I_Modify.aspx.cs" Inherits="CDS_PO_Receipt_Dialog_PO_Receipt_I_Modify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .titleS
        {
            height: 25px;
            background: #328aa4;
            width: 158px;
            border-top: 1px solid #fff;
            border-left: 1px solid #fff;
            border-right: 1px solid #fff;
        }
        .contentS
        {
            height: 25px;
            background: #e5f1f4;
            width: 300px;
            border-top: 1px solid #fff;
        }
        .contentS_x
        {
            height: 25px;
            background: #e5f1f4;
            width: 470px;
            border-top: 1px solid #fff;
        }
        .out
        {
            height: 155px;
            background: #e5f1f4;
            width: 572px;
        }
        .contentSlw
        {
            border-top: 1px solid #fff;
            border-left: 1px solid #fff;
            background: #e5f1f4;
        }
    </style>
    <script>
     
        function checkValue(txt) {
            var txtControl = document.getElementById('<%= warring.ClientID%>');
            var lbZPOQTY = document.getElementById('<%= lbZPOQTY.ClientID%>'); //總數量
            var pifb=document.getElementById('<%= lbpi.ClientID%>');
            if (txt.value != "") {
                if (isNaN(txt.value)) {
                    txtControl.innerText = "數量須為數值";
                } else {
                var pifbtmp = (pifb.innerText == "") ? 0 : parseFloat(pifb.innerText);
                var totl = parseFloat(lbZPOQTY.innerText) + (parseFloat(lbZPOQTY.innerText) * parseFloat(pifbtmp) / 100);
                if (parseFloat(txt.value) > parseFloat(totl)) {
               
                        txtControl.innerText = "收貨數量不能大於採購單數量";
                    } else {
                        txtControl.innerText = "";
                    }
                }
            } else {
                txtControl.innerText = "";
            }
        }
    </script>
    <table cellpadding="0" cellspacing="0" class="out">
        <tr>
            <td class="titleS">
                <asp:Label ID="Label2" runat="server" Text="採購單號:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS">
                <asp:Label ID="lbPO_NUMBER" runat="server" Font-Bold="True" Font-Names="微軟正黑體" 
                    ForeColor="#666666"></asp:Label>
            </td>
              <td class="titleS">
                <asp:Label ID="lbcomp" runat="server" Text="項目號碼:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS">
                <asp:Label ID="lbZPOITEM" runat="server" Font-Bold="True" Font-Names="微軟正黑體" ForeColor="#666666"></asp:Label>&nbsp;
            </td>
        </tr>
        <tr>
            <td class="titleS">
                <asp:Label ID="lbcomp0" runat="server" Text="物料號碼:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS_x" colspan="3">
                <asp:Label ID="lbZMATNR" runat="server" Font-Bold="True" Font-Names="微軟正黑體" ForeColor="#666666"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="lbZDESC" runat="server" Font-Bold="True" Font-Names="微軟正黑體" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="titleS">
                <asp:Label ID="Label7" runat="server" Text="工單料號:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS_x" colspan="3">
                <asp:Label ID="lbORD_MATERIAL" runat="server" Font-Bold="True" 
                    Font-Names="微軟正黑體" ForeColor="#666666"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                </td>
        </tr>
        <tr>
            <td class="titleS">
                <asp:Label ID="lbcomp1" runat="server" Text="異動類型:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS">
                <asp:DropDownList ID="ddmoveType" runat="server" Height="17px" Width="131px">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                </td>
              <td class="titleS">
                <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="微軟正黑體" 
                    ForeColor="White" Text="超收百分比:"></asp:Label>
            </td>
            <td class="contentS">
                <asp:Label ID="lbpi" runat="server" Font-Bold="True" Font-Names="微軟正黑體" 
                    ForeColor="Red"></asp:Label>
                <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="微軟正黑體" 
                    ForeColor="#666666" Text="%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="titleS">
                <asp:Label ID="Label5" runat="server" Text="收貨數量:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS">
                <asp:TextBox ID="tbqty" runat="server" Width="97px" onblur="checkValue(this);" 
                    MaxLength="15"></asp:TextBox>&nbsp;</td>
              <td class="titleS">
                <asp:Label ID="Label11" runat="server" Text="採購單數量:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS">
                <asp:Label ID="lbZPOQTY" runat="server" Font-Bold="True" Font-Names="微軟正黑體" ForeColor="#666666"></asp:Label>
            </td>
        </tr>
         <tr>
            <td class="titleS">
                <asp:Label ID="Label4" runat="server" Text="工廠:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS" >
                <asp:Label ID="lbplant" runat="server" Font-Bold="True" Font-Names="微軟正黑體" 
                    ForeColor="#666666"></asp:Label>
            </td>
              <td class="titleS">
                <asp:Label ID="Label1" runat="server" Text="儲存地點:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS">
                <asp:TextBox ID="tbsavloc" runat="server" Width="97px" onblur="checkValue(this);" 
                    MaxLength="4" AutoPostBack="True" ontextchanged="tbsavloc_TextChanged"></asp:TextBox>
                <asp:Label ID="lbsavname" runat="server" Font-Names="微軟正黑體" ForeColor="#666666"></asp:Label>
                <asp:Button ID="btnsavplace" runat="server" Font-Bold="True" Font-Names="細明體" 
                    onclick="btnsavplace_Click" Text="+" Width="22px" />
            </td>
        </tr>

        <tr>
         <td class="titleS">
                <asp:Label ID="Label6" runat="server" Text="供應商:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS" >
                <asp:Label ID="lbVENDOR" runat="server" Font-Bold="True" Font-Names="微軟正黑體" 
                    ForeColor="#666666"></asp:Label>
            </td>
            <td class="titleS">
                <asp:Label ID="Label3" runat="server" Text="批次:" Font-Bold="True" 
                    ForeColor="White" Font-Names="微軟正黑體"></asp:Label>
            </td>
            <td class="contentS">
                
                <asp:TextBox ID="tbBATCH" runat="server" MaxLength="10" Width="97px"></asp:TextBox>
                <asp:Button ID="btnBatch" runat="server" onclick="btnBatch_Click" Text="+" 
                    Width="22px" />
                
            </td>
        </tr>
        <tr>
            <td class="contentSlw" colspan="4">
                <asp:Label ID="Label19" runat="server" ForeColor="#999999" Text="訊息:"></asp:Label><asp:Label
                    ID="warring" runat="server" Font-Bold="True" ForeColor="Red" 
                    Font-Names="微軟正黑體"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
