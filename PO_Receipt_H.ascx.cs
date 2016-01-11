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
using IsWhat;
public partial class WKF_OptionalFields_PO_Receipt_H : WKF_FormManagement_VersionFieldUserControl_VersionFieldUC
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

    protected void Page_Load(object sender, EventArgs e)
    {
        //這裡不用修改
        //欄位的初始化資料都到SetField Method去做
        if (Request.Url.AbsoluteUri.ToUpper().IndexOf("FormPrint.aspx".ToUpper()) == -1)
        {
            ((Master_DialogMasterPage)Page.Master).Button2OnClick += new Master_DialogMasterPage.ButtonOnClickHandler(Button2);
        }
        if (!IsPostBack)
        {
            setddZMOVETYPE();
            DOC_DATE.Value =PSTNG_DATE.Value= DateTime.Now;
        }
        SetField(m_versionField);
        //Page.ClientScript.RegisterStartupScript(this.GetType(), "alert", "ZMOVETYPEchange();", true);
        
    }
    void Button2()
    {
        Rg what=new Rg();
     
        if(ddZMOVETYPE.SelectedValue=="")
        {
            Response.Write("<script>alert('異動類型不能為空白');location.href='" + Request.Url.AbsoluteUri + "';</script>");
        }
    }
    private void setddZMOVETYPE()
    {
        ddZMOVETYPE.Items.Clear();
        SqlConnection con = new SqlConnection();
        DataTable dt = new DataTable();
        try
        {
            con.ConnectionString = ConfigurationManager.ConnectionStrings["connectionstring"].ToString();
            SqlDataAdapter da = new SqlDataAdapter("SELECT ZMTYP,ZMTXT  from ZIT015 where ZMTYP in ('101','102','103','104','105','106','161','162') ", con);
            da.Fill(dt);
        }
        finally {
            con.Close();
        }

        for (int j = 0; j <= dt.Rows.Count - 1; j++)
        {
            ddZMOVETYPE.Items.Add(new ListItem(dt.Rows[j]["ZMTYP"].ToString().Trim() + ":" + dt.Rows[j]["ZMTXT"].ToString().Trim(), dt.Rows[j]["ZMTYP"].ToString().Trim()));
        }

        ddZMOVETYPE.SelectedValue = "103";
        Session["MOVETYPE"] = ddZMOVETYPE.SelectedValue;

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
            return this.ddZMOVETYPE.SelectedValue.Trim();
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
            XmlDocument DOC = new XmlDocument();
            XmlElement Element = DOC.CreateElement("FieldValue");
            //Element.SetAttribute("PURCHASEORDER", this.tbZPONO.Text.Trim());
            Element.SetAttribute("ZRFCTYPE", "M");
            Element.SetAttribute("PSTNG_DATE", PSTNG_DATE.Text);
            Element.SetAttribute("DOC_DATE", DOC_DATE.Text);
            Element.SetAttribute("MOVE_TYPE", this.ddZMOVETYPE.SelectedValue.Trim());

            return Element.OuterXml;

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
            if (!string.IsNullOrEmpty(fieldOptional.FieldValue))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(fieldOptional.FieldValue);

               //tbZPONO.Text = xmlDoc.SelectSingleNode("/FieldValue").Attributes["PURCHASEORDER"].Value;

               if (!string.IsNullOrEmpty(xmlDoc.SelectSingleNode("/FieldValue").Attributes["MOVE_TYPE"].Value))
                {
                    ddZMOVETYPE.SelectedValue = xmlDoc.SelectSingleNode("/FieldValue").Attributes["MOVE_TYPE"].Value;
                }
                else
                {
                    Response.Write("<script>alert('表頭異動類型紀錄是空白需從新選擇')</script>");
                }
                PSTNG_DATE.Value = Convert.ToDateTime(xmlDoc.SelectSingleNode("/FieldValue").Attributes["PSTNG_DATE"].Value);
                DOC_DATE.Value = Convert.ToDateTime(xmlDoc.SelectSingleNode("/FieldValue").Attributes["DOC_DATE"].Value);
            }

            if (fieldOptional.FieldMode == FieldMode.View || fieldOptional.FieldMode == FieldMode.Print || fieldOptional.FieldMode == FieldMode.Design || fieldOptional.FieldMode == FieldMode.Signin)
            {
                controlVisable(false);
            }
            else
            {
                controlVisable(true);
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

    private void controlVisable(bool p)
    {
        // ddZMOVETYPE.Enabled = PSTNG_DATE.Enabled = DOC_DATE.Enabled = p;
        ddZMOVETYPE.Enabled =  p;
    }
    protected void ddZMOVETYPE_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["MOVETYPE"] = ddZMOVETYPE.SelectedValue;
      
    }
}