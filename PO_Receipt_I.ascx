<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PO_Receipt_I.ascx.cs"
    Inherits="WKF_OptionalFields_PO_Receipt_I" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>
<%@ Register Assembly="Infragistics2.WebUI.WebDateChooser.v6.2, Version=6.2.20062.1079, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb"
    Namespace="Infragistics.WebUI.WebSchedule" TagPrefix="igsch" %>
<%@ Register Assembly="Fast.EB.Component.Grid" Namespace="Fast.EB.Component" TagPrefix="Fast" %>
<script type="text/javascript" src="../../Common/javascript/jQuery/jquery.js"></script>
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
    .contentS2
    {
        height: 25px;
        background: #e5f1f4;
        width: 335px;
        border-top: 1px solid #fff;
    }
    .titleS2
    {
        width: 100px;
        height: 25px;
        background: #328aa4;
        border-top: 1px solid #fff;
        border-left: 1px solid #fff;
        border-right: 1px solid #fff;
        border-bottom: 1px solid #fff;
    }
    .style1
    {
        height: 25px;
        background: #e5f1f4;
        width: 354px;
        border-top: 1px solid #fff;
    }
</style>
<script>
    function CopyToClipBoard2() {
        clipboardData.setData("Text", event.srcElement.innerText);
    }
   
    function opendialog() {
        var tbZPONO = document.getElementById("<%=tbZPONO.ClientID %>");
        var nowvbeln = document.getElementById("<%=nowvbeln.ClientID %>");
        var btnget_= document.getElementById("<%=btnget.ClientID %>");
        var allClear_= document.getElementById("<%=allClear.ClientID %>");
        if (nowvbeln.value.length > 0) {
            if (!confirm('已有項目資料,要重新載入？')) {
                event.returnValue = false;
            }else { 
                btnget_.disabled=allClear_.disabled = true;
                 btnget_.value = '讀取中..';
                 <% Response.Write(Page.ClientScript.GetPostBackEventReference(btnget, "")); %>
        }

        } else { 
                btnget_.disabled=allClear_.disabled = true;
                btnget_.value = '讀取中..';
                  <% Response.Write(Page.ClientScript.GetPostBackEventReference(btnget, "")); %>
        }
    }
    function cleardialog() {
        if (!confirm('確定要清除目前網頁上所有採購單號？')) event.returnValue = false;
    }
 
</script>
<table cellpadding="0" cellspacing="0" class="tb1pohead">
    <tr>
        <td class="titleS">
            <asp:Label ID="Label2" runat="server" Text="採購單號:" Font-Bold="True" Font-Names="微軟正黑體"
                ForeColor="White"></asp:Label>
        </td>
        <td class="style1">
            <asp:TextBox ID="tbZPONO" runat="server" MaxLength="10" onblur="checkValueby(this);"
                Width="125px"></asp:TextBox>&nbsp;&nbsp;<asp:Button ID="btnget" runat="server" Text="讀取採購單資訊"
                    OnClick="btnget_Click" Width="101px" Font-Names="微軟正黑體" Font-Bold="True" />&nbsp;&nbsp;&nbsp;<asp:Button
                        ID="allClear" runat="server" OnClick="Button1_Click" Text="清除所有單號" Width="86px"
                        Font-Bold="True" Font-Names="微軟正黑體" />
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="Label1" runat="server" Text="訊息:" Font-Names="微軟正黑體" ForeColor="#666666"></asp:Label>
            <asp:Label ID="warring" runat="server" Font-Names="微軟正黑體" ForeColor="Red"></asp:Label> 
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0">
    <tr>
        <td>
            <div style="overflow-x: auto; width: 1046px;">
                <Fast:Grid ID="Grid1" runat="server" DataKeyNames="key" AllowSorting="True" AutoGenerateCheckBoxColumn="False"
                    AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False"
                    DefaultSortDirection="Ascending" EmptyDataText="No data found" EnableModelValidation="True"
                    EnhancePager="True" KeepSelectedRows="False" PageSize="5" SelectedRowColor=""
                    UnSelectedRowColor="" OnRowDataBound="Grid1_RowDataBound" Width="1046px" CellPadding="2"
                    BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
                    Style="margin-top: 0px; margin-bottom: 20px;" AllowPaging="True" OnPageIndexChanging="Grid1_PageIndexChanging"
                    OnRowCommand="Grid1_RowCommand" OnSorting="Grid1_Sorting">
                    <EnhancePagerSettings ShowHeaderPager="True" FirstAltImageUrl="" FirstImageUrl=""
                        LastAltImage="" LastImageUrl="" NextIAltImageUrl="" NextImageUrl="" PageInfoCssClass=""
                        PageNumberCssClass="" PageNumberCurrentCssClass="" PageRedirectCssClass="" PreviousAltImageUrl=""
                        PreviousImageUrl=""></EnhancePagerSettings>
                    <ExportExcelSettings AllowExportToExcel="False"></ExportExcelSettings>
                    <Columns>
                        <asp:BoundField DataField="PO_NUMBER" HeaderText="採購單號" SortExpression="PO_NUMBER">
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PO_ITEM" HeaderText="項目" SortExpression="PO_ITEM">
                            <ItemStyle HorizontalAlign="Center" Width="4%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MOVE_TYPE" HeaderText="異動類型" SortExpression="MOVE_TYPE">
                            <ItemStyle HorizontalAlign="Center" Width="6%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MATERIAL" HeaderText="物料號碼" SortExpression="MATERIAL">
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ORD_MATERIAL" HeaderText="工單料號" SortExpression="ORD_MATERIAL">
                            <ItemStyle HorizontalAlign="Center" Width="6%" />
                        </asp:BoundField>
                         <asp:BoundField DataField="ORDERID" HeaderText="工單號碼" SortExpression="ORDERID">
                            <ItemStyle HorizontalAlign="Center" Width="6%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SHORT_TEXT" HeaderText="短文 // 工單料號說明" SortExpression="SHORT_TEXT">
                            <ItemStyle HorizontalAlign="Center" Width="14%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PLANT" HeaderText="工廠">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VENDOR_name" HeaderText="供應商" SortExpression="VENDOR_name">
                            <ItemStyle Width="6%" HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="STGE_LOC" HeaderText="儲存地點" SortExpression="STGE_LOC">
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="QUANTITY" HeaderText="採購單數量" SortExpression="QUANTITY">
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OVER_DLV_TOL" HeaderText="超收百分比(%)" SortExpression="OVER_DLV_TOL">
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ENTRY_QNT" HeaderText="收貨數量" SortExpression="ENTRY_QNT">
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BATCH" HeaderText="批號" SortExpression="BATCH">
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RET_ITEM" HeaderText="退貨項目" SortExpression="RET_ITEM">
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="linkModify" runat="server" CommandName="lbtnModify" CommandArgument='<%# Bind("PO_ITEM") %>'>修改</asp:LinkButton></ItemTemplate>
                            <ItemStyle Width="7%" HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="key" Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="key" runat="server" Text='<%# Bind("key") %> '></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PO_UNIT" HeaderText="單位" Visible="False" />
                        <asp:BoundField DataField="PUR_GROUP" HeaderText="群組id" Visible="False" />
                        <asp:BoundField DataField="PUR_GROUP_Name" HeaderText="群組名稱" Visible="False" />
                        <asp:BoundField DataField="VENDOR" HeaderText="供應商id" Visible="False" />
                    </Columns>
                    <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                    <HeaderStyle BackColor="#0066FF" Font-Bold="True" ForeColor="#CCCCFF" />
                    <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                    <RowStyle BackColor="White" ForeColor="#003399" />
                    <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                </Fast:Grid>
              
            </div>
            <div style="overflow-y: auto; height: 60px; margin-left: 5px">
                <asp:DataList ID="DataList1" runat="server" CellPadding="1" Width="195px" OnItemDataBound="DataList1_ItemDataBound"
                    OnItemCommand="DataList1_ItemCommand" BackColor="White" BorderColor="White" BorderStyle="Ridge"
                    BorderWidth="2px" CellSpacing="1" Font-Bold="False" Font-Italic="False" Font-Names="微軟正黑體"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                    <FooterStyle BackColor="White" Font-Bold="False" ForeColor="Black" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
                    <ItemStyle BackColor="#DEDFDE" ForeColor="Black" />
                    <ItemTemplate>
                        <span ondblclick="CopyToClipBoard2();">
                            <asp:Label ID="PO_NUMBER" runat="server" Font-Bold="True" Font-Names="微軟正黑體" ForeColor="#333333"
                                Text='<%# Bind("PO_NUMBER") %> '></asp:Label></span>&nbsp;
                        <asp:LinkButton ID="lbDelete" runat="server" CommandName="lbtnDelete" CommandArgument='<%# Bind("PO_NUMBER") %>'>刪除</asp:LinkButton>&nbsp;
                        <asp:LinkButton ID="lbrefresh" runat="server" CommandName="lbtnrefresh" CommandArgument='<%# Bind("PO_NUMBER") %>'>重新讀取</asp:LinkButton>
                    </ItemTemplate>
                    <SelectedItemStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
                </asp:DataList>
            </div>
        </td>
    </tr>
</table>
<asp:TextBox ID="mvt" Style="display: none" runat="server"></asp:TextBox>
<asp:HiddenField ID="nowvbeln" runat="server" />
<asp:TextBox ID="txtFieldValue" Style="display: none" runat="server"></asp:TextBox>
<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False"
    meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>