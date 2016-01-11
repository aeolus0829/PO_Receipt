using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Fast.EB.WKF.Design;
using System.Collections.Generic;
using Fast.EB.WKF.Utility;
using Fast.EB.Organization.Util;
using Fast.EB.WKF.Design.Data;
using Fast.EB.WKF.VersionFields;
using System.Xml;
using System.Data.SqlClient;
using Fast.EB.EIP.PrivateMessage;
using PO_Receipt.db;
using SAPCon;
public partial class WKF_OptionalFields_PO_Receipt_M : WKF_FormManagement_VersionFieldUserControl_VersionFieldUC
{

    #region ==============公開方法及屬性==============
    //表單設計時
    //如果為False時,表示是在表單設計時
    private bool m_ShowGetValueButton = true;
    public bool ShowGetValueButton
    {
        get { return this.m_ShowGetValueButton; }
        set { this.m_ShowGetValueButton = value; }
    }

    #endregion
    DataTable outdt;
    string result;
    string DOC_NBR;
    string BEGIN_TIME;
    protected void Page_Load(object sender, EventArgs e)
    {
        //這裡不用修改
        //欄位的初始化資料都到SetField Method去做
        SetField(m_versionField);
        if (!IsPostBack)
        {
            btnDel.Attributes.Add("OnClick", "javascript:if(!confirm('確定作廢表單？'))event.returnValue=false; ");
            resend.Attributes["onclick"] = "this.disabled = true;this.value = '重送中..';" + Page.ClientScript.GetPostBackEventReference(resend, "");
        }
    }

    /// <summary>
    /// 外掛欄位的條件值
    /// </summary>
    public override string ConditionValue
    {
        get
        {
            //回傳字串
            //此字串的內容將會被表單拿來當做條件判斷的值
            return String.Empty;
        }
    }

    /// <summary>
    /// 是否被修改
    /// </summary>
    public override bool IsModified
    {
        get
        {
            //請自行判斷欄位內容是否有被修改
            //有修改回傳True
            //沒有修改回傳False
            return false;
        }
    }

    /// <summary>
    /// 查詢顯示的標題
    /// </summary>
    public override string DisplayTitle
    {
        get
        {
            //表單查詢或WebPart顯示的標題
            //回傳字串
            return String.Empty;
        }
    }

    /// <summary>
    /// 訊息通知的內容
    /// </summary>
    public override string Message
    {
        get
        {
            //表單訊息通知顯示的內容
            //回傳字串
            return String.Empty;
        }
    }

    /// <summary>
    /// 真實的值
    /// </summary>
    public override string RealValue
    {
        get
        {
            //回傳字串
            //取得表單欄位簽核者的UsetSet字串
            //內容必須符合EB UserSet的格式
            return String.Empty;
        }
        set
        {
            //這個屬性不用修改
            base.m_fieldValue = value;
        }
    }

    /// <summary>
    /// 欄位的內容
    /// </summary>
    public override string FieldValue
    {
        get
        {
            //回傳字串
            //取得表單欄位填寫的內容
            return String.Empty;
        }
        set
        {
            //這個屬性不用修改
            base.m_fieldValue = value;
        }
    }

    /// <summary>
    /// 是否為第一次填寫
    /// </summary>
    public override bool IsFirstTimeWrite
    {
        get
        {
            //這裡請自行判斷是否為第一次填寫
            return false;
        }
        set
        {
            //這個屬性不用修改
            base.IsFirstTimeWrite = value;
        }
    }

    /// <summary>
    /// 顯示時欄位初始值
    /// </summary>
    /// <param name="versionField">欄位集合</param>
    public override void SetField(Fast.EB.WKF.Design.VersionField versionField)
    {
        FieldOptional fieldOptional = versionField as FieldOptional;

        if (fieldOptional != null)
        {
            #region ==============屬性說明==============『』
            //fieldOptional.IsRequiredField『是否為必填欄位,如果是必填(True),如果不是必填(False)』
            //fieldOptional.DisplayOnly『是否為純顯示,如果是(True),如果不是(False),一般在觀看表單及列印表單時,屬性為True』
            //fieldOptional.HasAuthority『是否有填寫權限,如果有填寫權限(True),如果沒有填寫權限(False)』
            //fieldOptional.FieldValue『如果已有人填寫過欄位,則此屬性為記錄其內容』
            //fieldOptional.FieldDefault『如果欄位有預設值,則此屬性為記錄其內容』
            //fieldOptional.FieldModify『是否允許修改,如果允許(fieldOptional.FieldModify=FieldModifyType.yes),如果不允許(fieldOptional.FieldModify=FieldModifyType.no)』
            //fieldOptional.Modifier『如果欄位有被修改過,則Modifier的內容為EBUser,如果沒有被修改過,則會等於Null』
            #endregion
            if (fieldOptional.FieldMode == FieldMode.View || fieldOptional.FieldMode == FieldMode.Print)
            {
                SqlConnection con = new SqlConnection();
                DataTable dt = new DataTable("CURRENT");
                try
                {
                    con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
                    SqlDataAdapter da = new SqlDataAdapter("select CURRENT_DOC,TASK_RESULT,DOC_NBR,BEGIN_TIME from [UOF].[dbo].[TB_WKF_TASK] where TASK_ID = @TASK_ID", con);
                    da.SelectCommand.Parameters.Add("TASK_ID", SqlDbType.NVarChar).Value = Request.QueryString["TASK_ID"].ToString();
                    da.Fill(dt);
                }
                finally
                {
                    con.Close();
                }

                //string url = Request.Url.AbsoluteUri;
                txtFieldValue.Text = dt.Rows[0][0].ToString();
                result = dt.Rows[0]["TASK_RESULT"].ToString();
                DOC_NBR = dt.Rows[0]["DOC_NBR"].ToString();
                BEGIN_TIME = dt.Rows[0]["BEGIN_TIME"].ToString();
                //Response.Write("<script>alert('" + dt.Rows[0][0].ToString() + "')</script>");
                Grid4.DataSource = BindGrid4();
                Grid4.DataBind();
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(txtFieldValue.Text);
                XmlNodeList last = xmlDoc.SelectNodes("/Form/FormFieldValue/FieldItem");
                int lastcount = last.Count;
                int xmllistcount = 1;
                foreach (XmlNode nd in last)
                {
                    if (xmllistcount == lastcount)
                    {
                        if (result == "0" && (nd.Attributes["ConditionValue"].Value == "" || nd.Attributes["ConditionValue"].Value == "0"))
                        {
                            XmlNodeList DELlast = xmlDoc.SelectNodes("/Form/FormFieldValue/FieldItem/FieldValue/Item/DEL"); //有此節點則是代表此表單作廢
                            if (DELlast.Count == 0)
                            {

                                resend.Enabled = btnDel.Enabled = true;
                            }
                            else
                            {
                                resend.Visible = btnDel.Visible = false;
                            }
                        }
                    }
                    xmllistcount++;
                }
            }
            #region ==============如果有修改，要顯示修改者資訊==============
            if (fieldOptional.Modifier != null)
            {
                lblModifier.Visible = true;
                lblModifier.ForeColor = System.Drawing.Color.Red;
                lblModifier.Text = String.Format("( {0}：{1} )", this.lblMsgSigner.Text, fieldOptional.Modifier.Name);
            }
            #endregion
        }
    }
    private DataTable BindGrid4()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtFieldValue.Text);

        DataTable dt = new DataTable();
        dt.Columns.Add("type");
        dt.Columns.Add("msg");
        XmlNodeList nodeList = xmlDoc.SelectNodes("Form/FormFieldValue/FieldItem/FieldValue/Item/RETURNMSG");

        foreach (XmlNode node in nodeList)
        {
            DataRow dr = dt.NewRow();

            dr["type"] = node.Attributes["type"].Value;
            dr["msg"] = node.Attributes["msg"].Value;

            dt.Rows.Add(dr);

        }
        return dt;
    }
    protected void resend_Click(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtFieldValue.Text);
        XmlNodeList msglist = xmlDoc.SelectNodes("Form/FormFieldValue/FieldItem/FieldValue/Item/RETURNMSG");
        foreach (XmlNode nd in msglist)
        {
            xmlDoc.SelectSingleNode("Form/FormFieldValue/FieldItem/FieldValue/Item").RemoveChild(xmlDoc.SelectSingleNode("Form/FormFieldValue/FieldItem/FieldValue/Item/RETURNMSG"));
        }
        Logon Con = new Logon();
        string ZFLAG = "";
        string ZMSG = "";
        try
        {
            Con.Conncet(); //連接
            Function fn = new Function();
            fn.SetFunction("ZRFC006");
           // fn.Field_SetValue("PURCHASEORDER", xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue").Attributes["PURCHASEORDER"].Value);
            fn.Field_SetValue("ZRFCTYPE", "M");
            fn.Field_SetValue("PSTNG_DATE", xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue").Attributes["PSTNG_DATE"].Value);
            fn.Field_SetValue("DOC_DATE", xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue").Attributes["DOC_DATE"].Value);
            fn.Field_SetValue("MOVE_TYPE", xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue").Attributes["MOVE_TYPE"].Value);
            fn.Set_TableName("POITEM");
            DataTable POITEM = new DataTable("POITEM");
            POITEM.Columns.Add("ITEM");
            POITEM.Columns.Add("value");
            XmlNodeList ItemList = xmlDoc.SelectNodes("/Form/FormFieldValue/FieldItem/FieldValue/Item");
            DataRow dr;
            foreach (XmlNode node in ItemList)
            {
                POITEM.Rows.Clear();
                XmlElement XE = (XmlElement)node;

                XmlAttributeCollection xac = node.Attributes;
                foreach (XmlAttribute xa in xac)
                {
                    if (xa.Name != "key")
                    {
                        dr = POITEM.NewRow();
                        dr[0] = xa.Name;
                        dr[1] = xa.Value;
                        POITEM.Rows.Add(dr);
                    }

                }

                fn.Set_Table(POITEM);
            }

            fn.StartFunction();
            ZFLAG = fn.Retrun_String("ZFLAG");
            ZMSG = fn.Retrun_String("ZMSG");

        }
        finally
        {
            Con.dispose();
        }
        bool flag = false;
        int ty = 0;

        XmlElement fieldElement = xmlDoc.CreateElement("RETURNMSG");
        switch (ZFLAG)
        {
            case "S":
                fieldElement.SetAttribute("type", "成功");
                ty = 1;

                break;
            case "W":
                fieldElement.SetAttribute("type", "警告");
                break;
            case "E":
                fieldElement.SetAttribute("type", "失敗");
                ty = 0;
                flag = true;
                break;
        }
        fieldElement.SetAttribute("msg", ZMSG);

        xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue/Item").InnerXml += fieldElement.OuterXml;

        XmlNodeList last = xmlDoc.SelectNodes("/Form/FormFieldValue/FieldItem");
        int lastcount = last.Count;
        int xmllistcount = 1;
        foreach (XmlNode nd in last)
        {
            if (xmllistcount == lastcount)
            {
                if (flag == true)
                {
                    nd.Attributes["ConditionValue"].Value = "0";
                }
                else
                {
                    nd.Attributes["ConditionValue"].Value = ty.ToString();
                }
            }
            xmllistcount++;
        }
        string txt = (ty == 1) ? "成功" : "失敗";
        PrivateMessageUCO msgUco = new PrivateMessageUCO();
        string createUser = xmlDoc.SelectSingleNode("/Form/Applicant").Attributes["userGuid"].Value;
        msgUco.SendOneNewMessage("admin", "採購單-收貨回傳資訊", txt, createUser);
        PO_Receipt.UCO.UCO uco = new PO_Receipt.UCO.UCO();
        msg ds = new msg();
        //DataTable re = new DataTable();
        msg.TB_WKF_TASKRow TASKRow = ds.TB_WKF_TASK.NewTB_WKF_TASKRow();
        TASKRow.DOC_NBR = DOC_NBR;
        TASKRow.CURRENT_DOC = xmlDoc.OuterXml;
        uco.UpdateXmlsetMsg(TASKRow);
        Response.Write("<script>alert('重送完成');location.href='" + Request.Url.AbsoluteUri + "';</script>");
    }
    protected void Grid4_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid4.DataSource = BindGrid4();
        Grid4.DataBind();
    }
    protected void Grid4_RowDataBound(object sender, GridViewRowEventArgs e)
    {


    }
    protected void btnDel_Click(object sender, EventArgs e)
    {
        #region ==============作廢表單==============
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtFieldValue.Text);
        XmlElement fieldElement = xmlDoc.CreateElement("DEL");
        xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue/Item").InnerXml += fieldElement.OuterXml;
        txtFieldValue.Text = xmlDoc.OuterXml;
        PO_Receipt.UCO.UCO uco = new PO_Receipt.UCO.UCO();
        msg ds = new msg();
        //DataTable re = new DataTable();
        msg.TB_WKF_TASKRow TASKRow = ds.TB_WKF_TASK.NewTB_WKF_TASKRow();
        TASKRow.DOC_NBR = DOC_NBR;
        TASKRow.CURRENT_DOC = txtFieldValue.Text;
        uco.UpdateXmlsetMsg(TASKRow);
        Response.Write("<script>alert('作廢完成');location.href='" + Request.Url.AbsoluteUri + "';</script>");
        #endregion
    }

}