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
using Fast.EB.Utility;
using IsWhat;
using Infragistics.WebUI.WebSchedule;
using SAPCon;
public partial class WKF_OptionalFields_PO_Receipt_I : WKF_FormManagement_VersionFieldUserControl_VersionFieldUC
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
    FieldOptional fieldOptional2;


    //[AjaxPro.AjaxMethod]
    //public bool CheckData(string xml)
    //{
    //    XmlDocument xmlDoc = new XmlDocument();
    //    xmlDoc.LoadXml(xml);



    //    if (xmlDoc.SelectNodes("/FieldValue/Item").Count == 0)
    //    {
    //        return false;
    //    }
    //    else
    //    {
    //        XmlNodeList list = xmlDoc.SelectNodes("/FieldValue/Item");
    //        bool flag = false;
    //        foreach (XmlNode nd in list)
    //        {
    //            if (Convert.ToDouble(nd.Attributes["ENTRY_QNT"].Value) >0)
    //            {
    //                flag = true;
    //            }
    //        }
    //        if (flag ==false)
    //        {
    //            CustomValidator1.ErrorMessage = "至少一筆領收數量大於0";
    //        }
    //        return flag;
    //    }
    //}

    protected void Page_Load(object sender, EventArgs e)
    {
        //這裡不用修改
        //欄位的初始化資料都到SetField Method去做


        btnget.Attributes.Add("OnClick", "opendialog();");
        // btnget.Attributes["onclick"] = @"this.disabled = true;this.value = '讀取中..';" + allClear.ClientID + ".disabled = true;" + " if()" + Page.ClientScript.GetPostBackEventReference(btnget, "");
        allClear.Attributes.Add("OnClick", "cleardialog();");
        //AjaxPro.Utility.RegisterTypeForAjax(typeof(WKF_OptionalFields_PO_Receipt_I));

        SetField(m_versionField);


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
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(txtFieldValue.Text);
            return xmlDoc.OuterXml;
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
        fieldOptional2 = fieldOptional;
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
                txtFieldValue.Text = fieldOptional.FieldValue;
                GVGetData1();
                DataList1.DataSource = BindGrid2();
                DataList1.DataBind();
            }
            else
            {

                XmlDocument xmlDoc = new XmlDocument();

                XmlElement fieldValueElement = xmlDoc.CreateElement("FieldValue");

                txtFieldValue.Text = fieldValueElement.OuterXml;

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
        btnget.Enabled = allClear.Enabled = p;

    }

    protected void btnget_Click(object sender, EventArgs e)
    {
        Rg what = new Rg();
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtFieldValue.Text);
        XmlNodeList LIST = xmlDoc.SelectNodes("/FieldValue/Item");


        if (tbZPONO.Text == "")
        {
            warring.Text = "訂單號碼不能空白";
            return;
        }
        if (!what.IsNumericInt(tbZPONO.Text))
        {
            warring.Text = "訂單號碼必須是數字";
            return;
        }



        //   DropDownList ddZMOVETYPE = (DropDownList)UC.Controls[1].FindControl("versionFieldUC1").Controls[9].FindControl("ddZMOVETYPE");
        if (tbZPONO.Text == "")
        {
            warring.Text = "採購單號不能空白";
            return;
        }

        if (Session["MOVETYPE"].ToString() == "")
        {
            warring.Text = "異動類型不能空白";
            return;
        }

        UpdateItem(tbZPONO.Text);




    }

    private void UpdateItem(string nowitem)
    {
        //ContentPlaceHolder PH = (ContentPlaceHolder)Page.Master.FindControl("ContentPlaceHolder1");
        //Panel UC = (Panel)PH.FindControl("Panel1");
        //DropDownList ddZMOVETYPE = (DropDownList)UC.Controls[1].FindControl("versionFieldUC1").Controls[9].FindControl("ddZMOVETYPE");
        //string updateitem = (nowitem != "") ? nowitem : tbZPONO.Text;

        //xmlDoc.LoadXml(txtFieldValue.Text);

        //string updateitem = (nowitem != "") ? nowitem : tbZPONO.Text;
        //DataTable dt = new DataTable();
        //dt.Columns.Add("PO_NUMBER");
        //XmlNodeList nodeList = xmlDoc.SelectNodes(string.Format("/FieldValue/Item[@PO_NUMBER = '{0}']", updateitem));

        //if (nodeList.Count > 0)
        //{
        //    //先清除舊的,再新增
        //    foreach (XmlNode nd in nodeList)
        //    {
        //        xmlDoc.SelectSingleNode("/FieldValue").RemoveChild(xmlDoc.SelectSingleNode(string.Format("/FieldValue/Item[@PO_NUMBER = '{0}']", updateitem)));
        //    }

        //}
        //string MOVETYPE = (Session["MOVETYPE"] != null) ? Session["MOVETYPE"].ToString() : "";


        //Logon Con = new Logon();

        //try
        //{
        //    Con.Conncet(); //連接
        //    Function fn = new Function();
        //    fn.SetFunction("ZRFC006");
        //    fn.Field_SetValue("PURCHASEORDER", updateitem);
        //    fn.Field_SetValue("ZRFCTYPE", "G");

        //    //fn.Field_SetValue("MOVE_TYPE", MOVETYPE);
        //    fn.StartFunction();
        //    VENDOR = fn.getStruct("POHEADER");
        //    DT = fn.Return_Message("POITEM");
        //}
        //finally
        //{
        //    Con.dispose();
        //}
        string updateitem = (nowitem != "") ? nowitem : tbZPONO.Text;
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtFieldValue.Text);


        //DataTable dt = new DataTable();
        //dt.Columns.Add("PO_NUMBER");
        XmlNodeList nodeList = xmlDoc.SelectNodes(string.Format("/FieldValue/Item[@PO_NUMBER = '{0}']", updateitem));

        if (nodeList.Count > 0)
        {
            //先清除舊的,再新增
            foreach (XmlNode nd in nodeList)
            {
                xmlDoc.SelectSingleNode("/FieldValue").RemoveChild(xmlDoc.SelectSingleNode(string.Format("/FieldValue/Item[@PO_NUMBER = '{0}']", updateitem)));
            }

        }

      string MOVETYPE = (Session["MOVETYPE"] != null) ? Session["MOVETYPE"] .ToString(): "";
        axl.SapCon.PO_Receipt_get get = new axl.SapCon.PO_Receipt_get();
        get.Update(nowitem, MOVETYPE, txtFieldValue.Text);

        DataTable DT = get.POITEM;
        DataTable ACCOUNT = get.POACCOUNT;
        DataTable VENDOR = get.POHEADER;
        if (DT.Rows.Count > 0)
        {
            nowvbeln.Value = "," + updateitem;
        }
        string VENDOR_ = "";
        string VENDOR_name = "";
        string PUR_GROUP_Name = "";
        string PUR_GROUP_ = "";

        if (DT.Rows.Count > 0)
        {
            VENDOR_ = (VENDOR.Rows.Count > 0) ? VENDOR.Rows[0]["VENDOR"].ToString().TrimStart('0') : "";
            PUR_GROUP_ = (VENDOR.Rows.Count > 0) ? VENDOR.Rows[0]["PUR_GROUP"].ToString().TrimStart('0') : "";
            VENDOR_name = getVENDOR_name(VENDOR_);
            PUR_GROUP_Name = getPUR_GROUP_Name(PUR_GROUP_);

        }



        for (int i = 0; i <= DT.Rows.Count - 1; i++)
        {

            XmlElement itemElement = xmlDoc.CreateElement("Item");
            itemElement.SetAttribute("key", "");
            itemElement.SetAttribute("PO_NUMBER", updateitem);
            //增加顯示供應商
            itemElement.SetAttribute("VENDOR", VENDOR_);
            itemElement.SetAttribute("VENDOR_name", VENDOR_name);

            itemElement.SetAttribute("PO_ITEM", DT.Rows[i]["PO_ITEM"].ToString());
            //
            itemElement.SetAttribute("MATERIAL", DT.Rows[i]["MATERIAL"].ToString().TrimStart('0'));
            itemElement.SetAttribute("ORD_MATERIAL", DT.Rows[i]["ORD_MATERIAL"].ToString().TrimStart('0'));

            itemElement.SetAttribute("SHORT_TEXT", DT.Rows[i]["SHORT_TEXT"].ToString());
            itemElement.SetAttribute("QUANTITY", DT.Rows[i]["QUANTITY"].ToString());
            itemElement.SetAttribute("ENTRY_QNT", "0");
            itemElement.SetAttribute("RET_ITEM", DT.Rows[i]["RET_ITEM"].ToString());
            string inport = (DT.Rows[i]["RET_ITEM"].ToString().Trim() == "X") ? "161" : MOVETYPE;
            itemElement.SetAttribute("MOVE_TYPE", inport);
            itemElement.SetAttribute("BATCH", "");
            itemElement.SetAttribute("PLANT", DT.Rows[i]["PLANT"].ToString());
            itemElement.SetAttribute("OVER_DLV_TOL", DT.Rows[i]["OVER_DLV_TOL"].ToString());
            itemElement.SetAttribute("STGE_LOC", DT.Rows[i]["STGE_LOC"].ToString().Trim());

            //增加單位用於列印
            itemElement.SetAttribute("PO_UNIT", DT.Rows[i]["PO_UNIT"].ToString().Trim());
            //採購群組
            itemElement.SetAttribute("PUR_GROUP", PUR_GROUP_);
            itemElement.SetAttribute("PUR_GROUP_Name", PUR_GROUP_Name);

            //工單號碼
            string ORDERID_ = "";
            for (int h = 0; h <= ACCOUNT.Rows.Count - 1; h++)
            {
                if (DT.Rows[i]["PO_ITEM"].ToString() == ACCOUNT.Rows[h]["PO_ITEM"].ToString())
                {
                    //找到相符合的項目
                    ORDERID_ = ACCOUNT.Rows[h]["ORDERID"].ToString().TrimStart('0');
                }
            }
            itemElement.SetAttribute("ORDERID", ORDERID_);
            xmlDoc.SelectSingleNode("/FieldValue").InnerXml += itemElement.OuterXml;
            nowvbeln.Value = "yes";
            allClear.Enabled = true;
        }


        XmlNodeList nodeList2 = xmlDoc.SelectNodes("/FieldValue/Item");
        int count = 1;
        foreach (XmlNode nd in nodeList2)
        {
            nd.Attributes["key"].Value = count.ToString();
            count++;
        }


        txtFieldValue.Text = xmlDoc.OuterXml;




        DataList1.DataSource = BindGrid2();
        DataList1.DataBind();
        GVGetData1();
    }
    private DataTable BindGrid2()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtFieldValue.Text);
        DataTable dt = new DataTable();
        dt.Columns.Add("PO_NUMBER");
        XmlNodeList nodeList = xmlDoc.SelectNodes("/FieldValue/Item");
        string tmp = "";
        foreach (XmlNode node in nodeList)
        {
            if (tmp != node.Attributes["PO_NUMBER"].Value)
            {
                DataRow dr = dt.NewRow();

                dr["PO_NUMBER"] = tmp = node.Attributes["PO_NUMBER"].Value;
                dt.Rows.Add(dr);
                nowvbeln.Value += "," + node.Attributes["PO_NUMBER"].Value;

            }
        }
        return dt;
    }
    private DataTable BindGrid()
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtFieldValue.Text);
        DataTable dt = new DataTable("POITEM");
        dt.Columns.Add("key");
        dt.Columns.Add("PO_NUMBER");
        dt.Columns.Add("PO_ITEM");
        dt.Columns.Add("MATERIAL");
        dt.Columns.Add("ORD_MATERIAL");
        dt.Columns.Add("SHORT_TEXT");
        dt.Columns.Add("QUANTITY");
        dt.Columns.Add("ENTRY_QNT");
        dt.Columns.Add("RET_ITEM");
        dt.Columns.Add("OVER_DLV_TOL");
        dt.Columns.Add("STGE_LOC");
        dt.Columns.Add("MOVE_TYPE");
        dt.Columns.Add("BATCH");
        dt.Columns.Add("PLANT");
        dt.Columns.Add("VENDOR");
        //單位
        dt.Columns.Add("PO_UNIT");
        //採購群組相關
        dt.Columns.Add("PUR_GROUP");
        dt.Columns.Add("PUR_GROUP_Name");
        //供應商名稱
        dt.Columns.Add("VENDOR_name");
        //工單號碼
        dt.Columns.Add("ORDERID");

        XmlNodeList nodeList = xmlDoc.SelectNodes("/FieldValue/Item");
        DataTable dtOrderTxt = getOrderTxt(nodeList);
        foreach (XmlNode node in nodeList)
        {
            DataRow dr = dt.NewRow();
            dr["key"] = node.Attributes["key"].Value;
            //供應商
            if (node.Attributes["VENDOR"] != null)
            {
                dr["VENDOR"] = node.Attributes["VENDOR"].Value;
            }
            else
            {
                dr["VENDOR"] = "";
            }
            //供應商名稱
            if (node.Attributes["VENDOR_name"] != null)
            {
                dr["VENDOR_name"] = node.Attributes["VENDOR_name"].Value;
            }
            else
            {
                dr["VENDOR_name"] = "";
            }

            if (node.Attributes["ORD_MATERIAL"] != null)
            {
                dr["ORD_MATERIAL"] = node.Attributes["ORD_MATERIAL"].Value;

            }
            else
            {
                dr["ORD_MATERIAL"] = "";
            }
            dr["PO_NUMBER"] = node.Attributes["PO_NUMBER"].Value;
            dr["PO_ITEM"] = node.Attributes["PO_ITEM"].Value;
            dr["MATERIAL"] = node.Attributes["MATERIAL"].Value;
            dr["SHORT_TEXT"] = node.Attributes["SHORT_TEXT"].Value;
            if (!string.IsNullOrEmpty(dr["ORD_MATERIAL"].ToString()))
            {
                for (int i = 0; i <= dtOrderTxt.Rows.Count - 1; i++)
                {
                    if (dtOrderTxt.Rows[i]["ZMCOD"].ToString().TrimStart('0') == dr["ORD_MATERIAL"].ToString())
                    {
                        dr["SHORT_TEXT"] += " // " + dtOrderTxt.Rows[i]["ZMTXT"].ToString();
                        break;
                    }
                }
            }
            dr["QUANTITY"] = node.Attributes["QUANTITY"].Value;
            dr["ENTRY_QNT"] = node.Attributes["ENTRY_QNT"].Value;
            dr["RET_ITEM"] = node.Attributes["RET_ITEM"].Value;
            dr["OVER_DLV_TOL"] = node.Attributes["OVER_DLV_TOL"].Value;
            dr["STGE_LOC"] = node.Attributes["STGE_LOC"].Value;
            //string MOVETYPES = (Session["MOVETYPE"] != null) ? Session["MOVETYPE"].ToString() : "";
            //string MOVE_TYPE = (node.Attributes["RET_ITEM"].Value != "X") ? "" : MOVETYPES;
            dr["MOVE_TYPE"] = node.Attributes["MOVE_TYPE"].Value;
            dr["BATCH"] = node.Attributes["BATCH"].Value;
            dr["PLANT"] = node.Attributes["PLANT"].Value;
            if (node.Attributes["PO_UNIT"] != null)
            {
                dr["PO_UNIT"] = node.Attributes["PO_UNIT"].Value;
            }
            else
            {
                dr["PO_UNIT"] = "";
            }

            if (node.Attributes["PUR_GROUP"] != null)
            {
                dr["PUR_GROUP"] = node.Attributes["PUR_GROUP"].Value;
            }
            else
            {
                dr["PUR_GROUP"] = "";
            }

            if (node.Attributes["PUR_GROUP_Name"] != null)
            {
                dr["PUR_GROUP_Name"] = node.Attributes["PUR_GROUP_Name"].Value;
            }
            else
            {
                dr["PUR_GROUP_Name"] = "";
            }

            if (node.Attributes["ORDERID"] != null)
            {
                dr["ORDERID"] = node.Attributes["ORDERID"].Value;
            }
            else
            {
                dr["ORDERID"] = "";
            }
            dt.Rows.Add(dr);
        }
        return dt;
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
    private string getVENDOR_name(string p)
    {
        //取得供應商名稱
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ToString()))
        {
            DataTable dt = new DataTable();
            string selectSQL = "SELECT ZVTXT FROM ZIT011 WHERE ZVEND = @ZVEND";
            SqlDataAdapter da = new SqlDataAdapter(selectSQL, con);
            da.SelectCommand.Parameters.Add("ZVEND", SqlDbType.NVarChar).Value = p.PadLeft(10, '0');
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["ZVTXT"].ToString().Trim();
            }
            else
            {
                return "";
            }

        }
    }

    private string getPUR_GROUP_Name(string p)
    {
        //取得採購群組名稱
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ToString()))
        {
            DataTable dt = new DataTable();
            string selectSQL = "SELECT ZPGTX FROM ZIT001 WHERE ZPGRP = @ZPGRP";
            SqlDataAdapter da = new SqlDataAdapter(selectSQL, con);
            da.SelectCommand.Parameters.Add("ZPGRP", SqlDbType.NVarChar).Value = p;
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["ZPGTX"].ToString().Trim();
            }
            else
            {
                return "";
            }

        }
    }
    protected void Grid1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Grid1.PageIndex = e.NewPageIndex;
        GVGetData1();
    }
    protected void Grid1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "lbtnModify")
        {
            string id = e.CommandArgument.ToString();

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(txtFieldValue.Text);
            if (!string.IsNullOrEmpty(Dialog.GetReturnValue()))
            {
                XmlDocument returnXmlDoc = new XmlDocument();
                returnXmlDoc.LoadXml(Dialog.GetReturnValue());

                xmlDoc.SelectSingleNode(string.Format("/FieldValue/Item[@key='{0}']", id)).Attributes["BATCH"].Value = returnXmlDoc.SelectSingleNode("/Item").Attributes["BATCH"].Value;
                xmlDoc.SelectSingleNode(string.Format("/FieldValue/Item[@key='{0}']", id)).Attributes["MOVE_TYPE"].Value = returnXmlDoc.SelectSingleNode("/Item").Attributes["MOVE_TYPE"].Value;
                xmlDoc.SelectSingleNode(string.Format("/FieldValue/Item[@key='{0}']", id)).Attributes["ENTRY_QNT"].Value = returnXmlDoc.SelectSingleNode("/Item").Attributes["ENTRY_QNT"].Value;
                xmlDoc.SelectSingleNode(string.Format("/FieldValue/Item[@key='{0}']", id)).Attributes["STGE_LOC"].Value = returnXmlDoc.SelectSingleNode("/Item").Attributes["STGE_LOC"].Value;
                txtFieldValue.Text = xmlDoc.OuterXml;
                GVGetData1();
            }
        }

    }
    protected void Grid1_Sorting(object sender, GridViewSortEventArgs e)
    {
        string se = ViewState["se1"] != null ?
       Convert.ToString(ViewState["se1"]) : string.Empty;
        SortDirection sd = ViewState["sd1"] != null ?
            (SortDirection)ViewState["sd1"] : SortDirection.Ascending;

        if (string.IsNullOrEmpty(se))
        {
            se = e.SortExpression;
            sd = SortDirection.Ascending;
        }

        // 如果欄位與本來不同
        if (se != e.SortExpression)
        {
            // 切換為目前所指定欄位
            se = e.SortExpression;

            // 指定排列方式為升冪
            sd = SortDirection.Ascending;
        }
        // 如果欄位與本來相同
        else
        {
            // 切換升冪為降冪，降冪為升冪
            if (sd == SortDirection.Ascending)
                sd = SortDirection.Descending;
            else
                sd = SortDirection.Ascending;
        }

        // 紀錄欄位與排列方式 ( 升冪或降冪 )
        ViewState["se1"] = se;
        ViewState["sd1"] = sd;

        GVGetData1(sd, se);
    }
    protected void Grid1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DataRowView row = (DataRowView)e.Row.DataItem;
            Label key = (Label)e.Row.FindControl("key");
            LinkButton linkModify = (LinkButton)e.Row.FindControl("linkModify");
            linkModify.CommandName = "lbtnModify";
            linkModify.CommandArgument = key.Text;


            Dialog.Open(linkModify, "~/CDS/PO_Receipt/Dialog/PO_Receipt_I_Modify.aspx", "",
             580, 245, Dialog.PostBackType.AfterReturn,
             "key=" + key.Text,
                 "PO_NUMBER=" + row["PO_NUMBER"].ToString(),
                "PO_ITEM=" + row["PO_ITEM"].ToString(),
                "MATERIAL=" + row["MATERIAL"].ToString(),
                "ORD_MATERIAL=" + row["ORD_MATERIAL"].ToString(),
                "QUANTITY=" + row["QUANTITY"].ToString(),
                "ENTRY_QNT=" + row["ENTRY_QNT"].ToString(),
                "STGE_LOC=" + row["STGE_LOC"].ToString(),
                "PLANT=" + row["PLANT"].ToString(),
                "VENDOR_name=" + row["VENDOR_name"].ToString(),
                "MOVE_TYPE=" + row["MOVE_TYPE"].ToString(),
                "OVER_DLV_TOL=" + row["OVER_DLV_TOL"].ToString(),
            "BATCH=" + row["BATCH"].ToString(),
                "SHORT_TEXT=" + row["SHORT_TEXT"].ToString());
            if (fieldOptional2.FieldMode == FieldMode.View || fieldOptional2.FieldMode == FieldMode.Print || fieldOptional2.FieldMode == FieldMode.Design || fieldOptional2.FieldMode == FieldMode.Signin)
            {
                linkModify.Visible = false;
                if (fieldOptional2.FieldMode == FieldMode.Signin)
                {
                    string GETXML = base.taskObj.CurrentDocXml;
                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.LoadXml(GETXML);
                    if (xmlDoc.SelectSingleNode("/Form/FormFieldValue/FieldItem/FieldValue").Attributes["MOVE_TYPE"].Value == "105")
                    {
                        linkModify.Visible = true;
                    }
                }

            }
        }
    }
    private void GVGetData1()
    {
        DataTable _dt;

        if (ViewState["se1"] == null)
        {
            _dt = BindGrid();
            Grid1.DataSource = _dt;
            Grid1.DataBind();
        }
        else
        {
            string se = Convert.ToString(ViewState["se1"]);
            SortDirection sd = (SortDirection)ViewState["sd1"];
            this.GVGetData1(sd, se);
        }
    }
    private void GVGetData1(SortDirection pSortDirection, string pSortExpression)
    {
        DataTable _dt = BindGrid();

        string sSort = string.Empty;
        if (pSortDirection == SortDirection.Ascending)
        {
            sSort = pSortExpression;
        }
        else
        {
            sSort = string.Format("{0} {1}", pSortExpression, "DESC");
        }

        DataView dv = _dt.DefaultView;
        dv.Sort = sSort;

        Grid1.DataSource = dv;
        Grid1.DataBind();

    }

    protected void DataList1_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            Label PO_NUMBER = (Label)e.Item.FindControl("PO_NUMBER");
            LinkButton lbDelete = (LinkButton)e.Item.FindControl("lbDelete");
            lbDelete.CommandName = "lbtnDelete";
            lbDelete.CommandArgument = PO_NUMBER.Text;

            LinkButton lbrefresh = (LinkButton)e.Item.FindControl("lbrefresh");
            lbrefresh.CommandName = "lbrefresh ";
            lbrefresh.CommandArgument = PO_NUMBER.Text;

            if (fieldOptional2.FieldMode == FieldMode.View || fieldOptional2.FieldMode == FieldMode.Print || fieldOptional2.FieldMode == FieldMode.Design || fieldOptional2.FieldMode == FieldMode.Signin)
            {
                lbDelete.Visible = lbrefresh.Visible = false;

            }
        }
    }
    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        if (e.CommandName == "lbtnDelete")
        {
            string id = e.CommandArgument.ToString();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(txtFieldValue.Text);
            XmlNodeList nodeList = xmlDoc.SelectNodes(string.Format("/FieldValue/Item[@PO_NUMBER = '{0}']", id));


            foreach (XmlNode nd in nodeList)
            {
                xmlDoc.SelectSingleNode("/FieldValue").RemoveChild(xmlDoc.SelectSingleNode(string.Format("/FieldValue/Item[@PO_NUMBER = '{0}']", id)));
            }
            nowvbeln.Value = nowvbeln.Value.Replace(id, "");
            txtFieldValue.Text = xmlDoc.OuterXml;
            DataList1.DataSource = BindGrid2();
            DataList1.DataBind();
            GVGetData1();
        }
        if (e.CommandName == "lbtnrefresh")
        {
            string id = e.CommandArgument.ToString();
            UpdateItem(id);
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        XmlDocument xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(txtFieldValue.Text);
        XmlNodeList nodeList = xmlDoc.SelectNodes("/FieldValue/Item");

        if (nodeList.Count > 0)
        {
            //先清除舊的,再新增
            foreach (XmlNode nd in nodeList)
            {
                xmlDoc.SelectSingleNode("/FieldValue").RemoveChild(xmlDoc.SelectSingleNode("/FieldValue/Item"));
            }

        }
        allClear.Enabled = false;
        nowvbeln.Value = "";
        txtFieldValue.Text = xmlDoc.OuterXml;
        DataList1.DataSource = BindGrid2();
        DataList1.DataBind();
        GVGetData1();
    }
}