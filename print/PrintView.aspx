<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintView.aspx.cs" Inherits="CDS_PO_Receipt_print_PrintView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script src="print.js" type="text/javascript"></script>
    <link href="print.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body onload="DP();">
    <form id="form1" runat="server">
    <asp:Panel ID="Panel1" runat="server">
        <table cellpadding="0" cellspacing="5" width="100%">
            <tr>
                <td width="70%">
                    <asp:Label ID="Label1" runat="server" Text="收   料   單" Font-Names="微軟正黑體"></asp:Label>
                </td>
                <td width="20%">
                    <asp:Label ID="Label2" runat="server" Text="號碼:" Font-Names="微軟正黑體"></asp:Label>
                    <asp:Label ID="lbMATERIALDOCUMENT" runat="server" Font-Names="微軟正黑體"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="aaa">
                        &nbsp;
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="30px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td class="style1">
                                <asp:Label ID="Label3" runat="server" Text="收貨日期:" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;<asp:Label
                                    ID="lbget" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="目前日期:" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;<asp:Label
                                    ID="lbnow" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="aaa">
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" height="40px">
                    <table cellpadding="0" cellspacing="0" width="100%">
                        <tr>
                            <td width="10%">
                                <asp:Label ID="Label11" runat="server" Text="工廠:" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lpplant" runat="server" Font-Names="微軟正黑體">1000</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label13" runat="server" Text="說明:" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label14" runat="server" Font-Names="微軟正黑體">富荃工業股份有限公司</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label16" runat="server" Text="異動類型:" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lbmovetype" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="aaa">
                        &nbsp;
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table cellpadding="0" cellspacing="1" width="100%">
                        <tr>
                            <td width="12%" align="center">
                                <asp:Label ID="Label15" runat="server" Text="採購單" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="6%" align="center">
                                <asp:Label ID="Label5" runat="server" Text="項目" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="10%" align="center">
                                <asp:Label ID="Label6" runat="server" Text="物料" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="10%" align="center">
                                <asp:Label ID="Label17" runat="server" Text="工單料號" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="33%" align="center">
                                <asp:Label ID="Label7" runat="server" Text="說明/工單料號說明" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td width="12%" align="center" colspan="3">
                                <asp:Label ID="Label19" runat="server" Text="供應商" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="12%" align="center">
                                <asp:Label ID="Label21" runat="server" Text="工單號碼" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td align="center">
                                <asp:Label ID="Label8" runat="server" Text="數量" Font-Names="微軟正黑體"></asp:Label>
                                <asp:Label ID="Label9" runat="server" Text=" / 單位" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5">
                                <div class="tbtitle">
                                </div>
                            </td>
                        </tr>
                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="aaa">
                    </div>
                </td>
            </tr>
            <tr>
                <td width="100%" colspan="2">
                    <table cellpadding="0" cellspacing="2" width="100%">
                        <tr>
                            <td width="30%">
                                <asp:Label ID="Label10" runat="server" Text="倉管人員:" Font-Names="微軟正黑體"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbuser" runat="server" Font-Names="微軟正黑體"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label22" runat="server" Font-Bold="False" Font-Names="微軟正黑體" 
                                    Text="申請日期:"></asp:Label>
                                <asp:Label ID="lbuday" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                            <td width="30%">
                                <asp:Label ID="Label12" runat="server" Text="品管人員:" Font-Names="微軟正黑體"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lbsinger" runat="server" Font-Names="微軟正黑體"></asp:Label>
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="微軟正黑體" 
                                    Text="簽核日期:"></asp:Label>
                                <asp:Label ID="lbiday" runat="server" Font-Names="微軟正黑體"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="aaa">
                    </div>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
