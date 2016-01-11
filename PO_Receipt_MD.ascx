<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PO_Receipt_M.ascx.cs"
    Inherits="WKF_OptionalFields_PO_Receipt_M" %>
<%@ Reference Control="~/WKF/FormManagement/VersionFieldUserControl/VersionFieldUC.ascx" %>
<%@ Register Assembly="Fast.EB.Component.Grid" Namespace="Fast.EB.Component" TagPrefix="Fast" %>
<table style="border-width: 1px; border-style: solid;">
    <tr>
        <td>
            <table>
                <tr>
                    <td>
                        <asp:Button ID="resend" runat="server" Text="重送" OnClick="resend_Click" Enabled="False"
                            Width="65px" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnDel" runat="server" Text="作廢RFC" Width="66px" OnClick="btnDel_Click"
                            Enabled="False" />
                    </td>
                </tr>
            </table>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <Fast:Grid ID="Grid4" runat="server" AllowSorting="True" AutoGenerateCheckBoxColumn="False"
                AutoGenerateColumns="False" CustomDropDownListPage="False" DataKeyOnClientWithCheckBox="False"
                DefaultSortDirection="Ascending" EmptyDataText="No data found" EnableModelValidation="True"
                EnhancePager="True" KeepSelectedRows="False" PageSize="5" SelectedRowColor=""
                UnSelectedRowColor="" OnRowDataBound="Grid4_RowDataBound" Width="590px" CellPadding="4"
                BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px"
                Style="margin-top: 0px" AllowPaging="True" OnPageIndexChanging="Grid4_PageIndexChanging">
                <EnhancePagerSettings FirstImageUrl="" FirstAltImageUrl="" PreviousImageUrl="" NextImageUrl=""
                    LastImageUrl="" LastAltImage="" PageNumberCssClass="" PageNumberCurrentCssClass=""
                    PageInfoCssClass="" PageRedirectCssClass="" NextIAltImageUrl="" PreviousAltImageUrl=""
                    ShowHeaderPager="True"></EnhancePagerSettings>
                <ExportExcelSettings AllowExportToExcel="False"></ExportExcelSettings>
                <Columns>
                    <asp:TemplateField HeaderText="錯誤代碼">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("type") %> '></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("type") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="40px" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="msg" HeaderText="訊息">
                        <ItemStyle Width="340px" />
                    </asp:BoundField>
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#0066FF" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
            </Fast:Grid>
        </td>
    </tr>
</table>
<asp:TextBox ID="txtFieldValue" Style="display: none" runat="server"></asp:TextBox>
<asp:Label ID="lblHasNoAuthority" runat="server" Text="無填寫權限" ForeColor="Red" Visible="False"
    meta:resourcekey="lblHasNoAuthorityResource1"></asp:Label>
<asp:Label ID="lblToolTipMsg" runat="server" Text="不允許修改(唯讀)" Visible="False" meta:resourcekey="lblToolTipMsgResource1"></asp:Label>
<asp:Label ID="lblModifier" runat="server" Visible="False" meta:resourcekey="lblModifierResource1"></asp:Label>
<asp:Label ID="lblMsgSigner" runat="server" Text="填寫者" Visible="False" meta:resourcekey="lblMsgSignerResource1"></asp:Label>
<asp:Label ID="lblAuthorityMsg" runat="server" Text="具填寫權限人員" Visible="False" meta:resourcekey="lblAuthorityMsgResource1"></asp:Label>