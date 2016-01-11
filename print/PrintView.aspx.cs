using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Xml;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Fast.EB.Utility;
using System.Collections;

public partial class CDS_PO_Receipt_print_PrintView : BasePage
{
    string END_TIME = "";
    protected void Page_Load(object sender, EventArgs e)
    {

        setDefault();

        //Page.ClientScript.RegisterStartupScript(this.GetType(), "Prompt", "DP();", true);
    }
    private void setDefault()
    {
        //時間

        //xmlDoc.lo
        lbnow.Text = DateTime.Now.ToString("yyyy/MM/dd");
        setLiteral();

    }

    private void setLiteral()
    {
        StringBuilder sb = new StringBuilder();
        string DOC_NBR = Request.QueryString["DOC_NBR"];
        string Task_ID = Request.QueryString["Task_ID"];
        SqlConnection con = new SqlConnection();

        DataTable dt = new DataTable("CURRENT");
        string CURRENT_DOC = "";
        string USER = "";
        string BEGIN_TIME = "";
        try
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            SqlDataAdapter da = new SqlDataAdapter("select T.CURRENT_DOC,U.NAME,BEGIN_TIME from [TB_WKF_TASK] as T INNER JOIN TB_EB_USER as U ON T.USER_GUID = U.USER_GUID  where DOC_NBR = @DOC_NBR", con);
            da.SelectCommand.Parameters.Add("DOC_NBR", SqlDbType.NVarChar).Value = DOC_NBR;

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                CURRENT_DOC = dt.Rows[0]["CURRENT_DOC"].ToString();
                USER = dt.Rows[0]["NAME"].ToString();
                BEGIN_TIME = Convert.ToDateTime(dt.Rows[0]["BEGIN_TIME"]).ToString("yyyy/MM/dd");
            }
        }
        finally
        {
            con.Close();
        }
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(CURRENT_DOC);
        //收貨日期
        if (xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue").Attributes["PSTNG_DATE"] != null)
        {
            lbget.Text = xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue").Attributes["PSTNG_DATE"].Value;
        }
        //文件號碼
        XmlNodeList RELIST = xmlDoc.SelectNodes("/Form/FormFieldValue/FieldItem/FieldValue/Item/RETURNMSG");
        foreach (XmlNode nd in RELIST)
        {
            if (nd.Attributes["matnr"] != null)
            {
                lbMATERIALDOCUMENT.Text = nd.Attributes["matnr"].Value;
            }
        }
        //取異動類型
        if (xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue").Attributes["MOVE_TYPE"] != null)
        {
            lbmovetype.Text = xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue").Attributes["MOVE_TYPE"].Value;
        }
        //取得項目
        XmlNodeList list = xmlDoc.SelectNodes("/Form/FormFieldValue/FieldItem/FieldValue/Item");
        int count = 1;


        DataTable dtOrderTxt = getOrderTxt(list);
        //int in_count = 1;
        //bool first=true;
        foreach (XmlNode nd in list)
        {

            //if (first ==false)
            //{
            //    //
            //    if (in_count % 11 == 0)
            //    {
            //        sb.Append(@"<tr><td align='center'><br style='page-break-after: always'>&nbsp;");
            //        sb.Append(@"</td></tr>");
            //        in_count = 1;
            //    }

            //}
            //else
            //{
            //    //第一頁
            //    if (count == 8 )
            //    {
            //        sb.Append(@"<tr><td align='center'><br style='page-break-after: always'>&nbsp;");
            //        sb.Append(@"</td></tr>");
            //        in_count = 1;
            //    }
            //}
            if (Convert.ToDecimal(nd.Attributes["ENTRY_QNT"].Value).ToString("f2") != Convert.ToDecimal(0).ToString("f2"))
            {
                sb.Append(@"<tr><td align='center'>");
                if (nd.Attributes["PO_NUMBER"] != null)
                {
                    sb.Append(nd.Attributes["PO_NUMBER"].Value);
                }
                sb.Append(@"</td>");
                sb.Append(@"<td align='center'>");
                sb.Append(count);//項目
                sb.Append(@"</td>");
                //sb.Append(@"<td align='center'>");
                //if (nd.Attributes["MOVE_TYPE"] != null)
                //{
                //    sb.Append(nd.Attributes["MOVE_TYPE"].Value);//異動類型
                //}
                //sb.Append(@"</td>");
                sb.Append(@"<td align='center'>");
                if (nd.Attributes["MATERIAL"] != null)
                {
                    sb.Append(nd.Attributes["MATERIAL"].Value); //物料
                }

                sb.Append(@"</td>");
                sb.Append(@"<td align='center'>");
                if (nd.Attributes["ORD_MATERIAL"] != null)
                {
                    sb.Append(nd.Attributes["ORD_MATERIAL"].Value);//工單料號
                }

                sb.Append(@"</td>");
                sb.Append(@"<td align='center'>");
                if (nd.Attributes["SHORT_TEXT"] != null)
                {
                    sb.Append(nd.Attributes["SHORT_TEXT"].Value);//說明
                    if (!string.IsNullOrEmpty(nd.Attributes["ORD_MATERIAL"].Value))
                    {
                        for (int j = 0; j <= dtOrderTxt.Rows.Count - 1; j++)
                        {
                            if (nd.Attributes["ORD_MATERIAL"].Value == dtOrderTxt.Rows[j]["ZMCOD"].ToString().TrimStart('0'))
                            {
                                sb.Append("/" + dtOrderTxt.Rows[j]["ZMTXT"].ToString());//工單料號說明
                                break;
                            }

                        }
                    }
                }
                sb.Append(@"</td></tr>");
                sb.Append(@"<tr><td align='center' colspan='3'>");
                if (nd.Attributes["VENDOR_name"] != null)
                {
                    sb.Append(nd.Attributes["VENDOR_name"].Value);
                }
                if (nd.Attributes["VENDOR"] != null)
                {

                    sb.Append("/" + nd.Attributes["VENDOR"].Value);
                }
                sb.Append(@"</td>");
                sb.Append(@"<td align='center'>");
                //if (nd.Attributes["PUR_GROUP_Name"] != null)
                //{
                //    sb.Append(nd.Attributes["PUR_GROUP_Name"].Value);//採購群組
                //}
                //if (nd.Attributes["PUR_GROUP"] != null)
                //{

                //    sb.Append("/" + nd.Attributes["PUR_GROUP"].Value);  //採購群組id
                //}
                if(nd.Attributes["ORDERID"] != null)
                {
                   sb.Append(nd.Attributes["ORDERID"].Value); 
                }

                sb.Append(@"</td>");
                sb.Append(@"<td align='center' >");
                if (nd.Attributes["ENTRY_QNT"] != null)
                {
                    sb.Append(nd.Attributes["ENTRY_QNT"].Value); //數量
                }
                if (nd.Attributes["PO_UNIT"] != null)
                {
                    string UNIT = (nd.Attributes["PO_UNIT"].Value == "ST") ? "PC" : nd.Attributes["PO_UNIT"].Value;
                    sb.Append(" / " + UNIT);   //單位
                }
                sb.Append(@"</td></tr>");
                sb.Append(@"<tr><td colspan='5'><div class='tbtitle'></div></td></tr>");
                //first=false;
                count++;
            }
            //in_count++;
        }

        Literal1.Text = sb.ToString();
        lbuday.Text = BEGIN_TIME;

        lbuser.Text = USER;
        lbsinger.Text =  (lbmovetype.Text =="105") ? getSigner(Task_ID, lbmovetype.Text) : "";

        lbiday.Text = (END_TIME !="")?Convert.ToDateTime(END_TIME).ToString("yyyy/MM/dd"):"";

    }

    private DataTable getOrderTxt(XmlNodeList list)
    {

        DataTable sqdt = new DataTable();

        string st = "";
        foreach (XmlNode nd in list)
        {
            if (nd.Attributes["ORD_MATERIAL"] != null)
            {
                if (nd.Attributes["ORD_MATERIAL"].Value != "")
                {
                    st += ",'" + nd.Attributes["ORD_MATERIAL"].Value.PadLeft(18, '0') + "'";
                }
            }
        }
        st = (st != "") ? st.Substring(1) : "";
        if (st != "")
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ToString()))
            {
                string selectSQL = "";

                selectSQL = "Select ZMCOD,ZMTXT FROM ZIT010 WHERE ZMCOD IN (" + st + ")";
                SqlDataAdapter da = new SqlDataAdapter(selectSQL, con);
                da.Fill(sqdt);
                con.Close();
            }
        }
        return sqdt;


    }

    private string getSigner(string Task_ID, string lbmovetype)
    {
        //取得品管人員
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ToString()))
        {
            DataTable dt = new DataTable();
            string selectSQL = "";
            if (lbmovetype == "105")
            {
                selectSQL = @"select N.SITE_ID,S.TASK_ID,SIGNER_INFO_SNAPSHOT, N.FINISH_TIME 
                                FROM  TB_WKF_TASK_SITE AS S INNER JOIN TB_WKF_TASK_NODE AS N 
                                ON S.SITE_ID=N.SITE_ID where TASK_ID = @TASK_ID AND SOURCE_SITE_ID in 
                                (select SITE_ID from  TB_WKF_SITE AS si inner join TB_WKF_FLOW  as F on si.FLOW_ID = F.FLOW_ID where F.FLOW_NAME = @FLOW_NAME and si.PREV_SITE_ID is null )";
            }
            else
            {
                selectSQL = @"select SIGNER_INFO_SNAPSHOT,N.FINISH_TIME FROM  TB_WKF_TASK_SITE AS S INNER JOIN TB_WKF_TASK_NODE AS N  ON S.SITE_ID=N.SITE_ID where TASK_ID = @TASK_ID ORDER BY N.FINISH_TIME DESC";

            }
            SqlDataAdapter da = new SqlDataAdapter(selectSQL, con);
            if (lbmovetype == "105")
            {
                da.SelectCommand.Parameters.Add("FLOW_NAME", SqlDbType.NVarChar).Value = ConfigurationManager.AppSettings["PO_Receipt_QC"].ToString();
            }
            da.SelectCommand.Parameters.Add("TASK_ID", SqlDbType.NVarChar).Value = Task_ID;
            da.Fill(dt);
            con.Close();


            string MNAME = "";
            string tmp = "";
            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                if (dt.Rows[i]["SIGNER_INFO_SNAPSHOT"].ToString() !="")
                {
                    //取得簽核資訊
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(dt.Rows[dt.Rows.Count - 1]["SIGNER_INFO_SNAPSHOT"].ToString());
                    XmlNodeList list = xmlDoc.SelectNodes("/SignerInfoSnapshot/userName");
                    foreach (XmlNode nd in list)
                    {
                        if (tmp != nd.InnerText)
                        {
                            MNAME += " " + nd.InnerText;
                            tmp = nd.InnerText;
                        }
                    }
                    //簽核日期
                    END_TIME = (lbmovetype == "105") ? dt.Rows[dt.Rows.Count - 1]["FINISH_TIME"].ToString() : dt.Rows[0]["FINISH_TIME"].ToString();
                }
            }
            return MNAME;

        }
    }
    //protected void Button1_Click(object sender, EventArgs e)
    //{
    //    Page.ClientScript.RegisterStartupScript(this.GetType(), "onload", "DP();", true);
    //}
}