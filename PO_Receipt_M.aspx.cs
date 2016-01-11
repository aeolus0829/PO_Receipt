﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Fast.EB.Utility;
using Fast.EB.WKF.Design;
using Fast.EB.WKF.Design.Data;
using Fast.EB.WKF.Design.Exceptions;
using Fast.EB.WKF.Utility;
using System.IO;
using System.Net;

public partial class WKF_OptionalFields_PO_Receipt_M : Fast.EB.Utility.BasePage
{
    private DesignFormFieldUCO designFormFieldUCO = new DesignFormFieldUCO();

	//欄位代號
    private string fieldId;
	//版本代號
    private string formVersionId;
	//明細欄位 GridId (區分是一般欄位或明細欄位)
    private string fieldParentId;
    private bool IsModify = false;
    private bool delFalg = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        //加入按下確定事件、按下儲存後繼續事件
        ((Master_DialogMasterPage)this.Master).Button1OnClick += new Master_DialogMasterPage.ButtonOnClickHandler(SureButtonClick);
        ((Master_DialogMasterPage)this.Master).Button2OnClick += new Master_DialogMasterPage.ButtonOnClickHandler(SaveAndContinueButtonClick);
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;

        #region ================ 拉選單傳值過來================
        if (Request.Form["ctl00$ContentPlaceHolder1$txtFieldId"] != null)
        {
            txtFieldId.Text = Request.Form["ctl00$ContentPlaceHolder1$txtFieldId"];
        }

        if (Request.Form["ctl00$ContentPlaceHolder1$txtFieldName"] != null)
        {
            txtFieldName.Text = Request.Form["ctl00$ContentPlaceHolder1$txtFieldName"];
        }
        #endregion

        #region ======================= Request參數=======================

        if (Request["NeedPostBack"] != null) { Dialog.SetReturnValue("NeedPostBack"); }
		//表單版本代號
        if (Request["formVersionId"] != null) { this.formVersionId = Request["formVersionId"]; }
        //明細欄位 GridId
        if (Request["fieldParentId"] != null && Request["fieldParentId"] != "") 
        { 
            fieldParentId = Request["fieldParentId"];
        }

        //欄位代號
        if (Request["fieldId"] != null)
        {
            fieldId = Request["fieldId"];
            IsModify = true;
            hiddenFieldId.Value = fieldId;
        }

        // 2006/11/27 如果是修改由別的頁傳送過來
        if (Request["orgFieldId"] != null)
        {
            hiddenFieldId.Value = Request["orgFieldId"];
            lblSeq.Text = Request["fieldSeq"];
            WebNumericEditFieldSeq.Value = Convert.ToInt32(Request["fieldSeq"]);
        }

        #endregion

        #region ============= 下拉選單設定 =============
        if (!IsPostBack)
        {
            UC_FiledDropList1.SelectedValue = FieldType.singleLineText.ToString();
            if (!String.IsNullOrEmpty(this.fieldParentId))
            {
                UC_FiledDropList1.IsDataGirdFiled = true;
            }
        }

        if (IsModify)
        {
            UC_FiledDropList1.TransParams = String.Format("formVersionId={0}&fieldParentId={1}&fieldId={2}&orgFieldId={3}&fieldSeq={4}", formVersionId, fieldParentId, fieldId, hiddenFieldId.Value, WebNumericEditFieldSeq.Value);
        }
        else
        {
            UC_FiledDropList1.TransParams = String.Format("formVersionId={0}&fieldParentId={1}", formVersionId, fieldParentId);
        }
        #endregion

        #region === IsPostBack ===
        if (!IsPostBack)
        {
            WebNumericEditFieldSeq.Visible = false;

            //判斷是否為修改
            if (IsModify)
            {
                //隱藏儲存後繼續
                ((Master_DialogMasterPage)Master).Button2Text = "";

                //如果是由別的欄位傳過來的修改狀態，就不給預設值
                if (Request["orgFieldId"] == null)
                {
                    SetModifyData();
                }
            }
            else
            {
                WebNumericEditFieldSeq.Value = 0;
            }
        }
        #endregion

        if (Request["fieldParentId"] == null || Request["fieldParentId"] == "")
        {
            Response.Write("<script For=window EVENT=onLoad> if (document.getElementById(\"GrideDetail\")) {document.getElementById(\"GrideDetail\").style.display = 'none';} </" + "script>");
        }
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        UC_FiledDropList1.SelectedValue = "optionalField_PO_Receipt_M";
    }
    
    /// <summary>
    /// 修改預設值
    /// </summary>
    private void SetModifyData()
    {
        VersionFieldDataSet versionFieldDs = designFormFieldUCO.GetSingleVersionField(this.formVersionId, this.fieldParentId, this.fieldId);

        //如果已發佈，則不允許修改
        if ((bool)versionFieldDs.FormVersion.Rows[0]["ISSUE_CTL"])
        {
            ((Master_DialogMasterPage)this.Master).Button1Text = "";
            UC_FiledDropList1.Enabled = false;
        }

        DataRow fieldDr = versionFieldDs.VersionField.Rows[0];

        //欄位代號
        txtFieldId.Text      = fieldDr["FIELD_ID"].ToString();
        //欄位名稱
        txtFieldName.Text    = fieldDr["FIELD_NAME"].ToString();
        //欄位備註
        txtFieldRemark.Text  = fieldDr["FIELD_REMARK"].ToString();
        //序號為Lable 顯示
        lblSeq.Text = fieldDr["FIELD_SEQ"].ToString();
        //欄位排列順序
        WebNumericEditFieldSeq.Value = (int)fieldDr["FIELD_SEQ"];

        //是否允許刪除此欄位(設計時)
        delFalg = Convert.ToBoolean(fieldDr["DELFLAG"]) == false ? false : true;
        txtFieldId.Enabled = delFalg;
    }
    
    /// <summary>
    /// 按下確定
    /// </summary>
    protected void SureButtonClick()
    {
        if (UpdateField())
        {
            Dialog.SetReturnValue("NeedPostBack");
            CloseDialog();
        }
    }

    /// <summary>
    /// 按下儲存後繼續
    /// </summary>
    private void SaveAndContinueButtonClick()
    {
        if (UpdateField())
        {
            Dialog.SetReturnValue("NeedPostBack");
            
            //導頁到SingleLineText(preserver Form 設 false 重新開始)
            Server.Transfer("~/WKF/FormManagement/SetupField_SingleLineText.aspx?formVersionId=" + formVersionId + "&fieldParentId=" + fieldParentId + "&NeedPostBack=Y", false);
        }
    }
    
    /// <summary>
    /// 更新表單欄位
    /// </summary>
    /// <returns></returns>
    private bool UpdateField()
    {
        
        VersionFieldDataSet versionFieldDs = new VersionFieldDataSet();
        DataRow versionFieldDr = versionFieldDs.FormVersion.NewRow();
        //表單版本編號
        versionFieldDr["FORM_VERSION_ID"] = formVersionId;
        versionFieldDs.FormVersion.Rows.Add(versionFieldDr);
       
        DataRow fieldDr = versionFieldDs.VersionField.NewRow();
        versionFieldDs.VersionField.Rows.Add(fieldDr);

        //判斷明細欄位用
        fieldDr["PARENT_FIELD_ID"] = fieldParentId;

        //欄位代號
        fieldDr["FIELD_ID"] = txtFieldId.Text;
        //欄位名稱
        fieldDr["FIELD_NAME"] = txtFieldName.Text;
        //欄位備註
        fieldDr["FIELD_REMARK"] = txtFieldRemark.Text;
        //欄位顯示寬度,如果有需要控制則可使用此屬性,沒用到就0即可
        fieldDr["DISPLAY_LENGTH"] = 0;
        //欄位內容長度,如果有需要控制則可使用此屬性,沒用到就0即可
        fieldDr["FIELD_LENGTH"] = 0;
        //是否允許修改
        fieldDr["FIELD_MODIFY"] = "no";
        //排序順序(此欄位不用更改，系統會自動給，要更改欄位排列位置請使用欄位調整功能來做)
        fieldDr["FIELD_SEQ"] = WebNumericEditFieldSeq.Value;
        //欄位型態(此欄位的值不可更改)
        fieldDr["FIELD_TYPE"] = "optionalField";
        //外掛欄位型態(此欄位的值不可更改)
        fieldDr["FIELD_SECTYPE"] = "PO_Receipt_M";

        //是否允許刪除欄位
        fieldDr["DELFLAG"] = delFalg.ToString();
        
        try
        {
            if (IsModify)
            {
                //如果是修改,就更新
                designFormFieldUCO.ModifyVersionField(versionFieldDs, hiddenFieldId.Value);
            }
            else
            {
                designFormFieldUCO.AddVersionField(versionFieldDs);
            }
        }
        catch (FieldIdDuplicateException ex)
        {
            //表單版本代號重複
            CustomValidatorFieldId.IsValid = false;
            return false;
        }
        catch (AggergateModifyOpTypeException ex)
        {
            CustomValidatorModifyFieldType.ErrorMessage = UC_FiledDropList1.ModifyAggeOpErrorMsg;
            CustomValidatorModifyFieldType.IsValid = false;
            return false;
        }
        catch (CalculateModifyOpTypeException ex)
        {
            CustomValidatorModifyFieldType.ErrorMessage = UC_FiledDropList1.ModifyCalcOpErrorMsg;
            CustomValidatorModifyFieldType.IsValid = false;
            return false;
        }
        return true;
    }
}