using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using Fast.EB.Utility;
using IsWhat;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class CDS_PO_Receipt_Dialog_PO_Receipt_I_Modify : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        ((Master_DialogMasterPage)this.Master).Button2Text = "";
        ((Master_DialogMasterPage)this.Master).Button1AutoCloseWindow = false;
       
        ((Master_DialogMasterPage)this.Master).Button1OnClick += new Master_DialogMasterPage.ButtonOnClickHandler(CDS_PO_Receipt_Dialog_PO_Receipt_I_Modify_Button1OnClick);
        
        if (!IsPostBack)
        {
            setDefault();
        }

        OpenDialog();


        Dialog.Open(btnsavplace, "~/CDS/Public_Dialog/Public_Dialog.aspx", "",
                    370, 325, Dialog.PostBackType.AfterReturn,
                        "PLANT=" + lbplant.Text, "MATNR=" + lbZMATNR.Text, "file=ZIT016_2");
    }
    private void OpenDialog()
    {
        string MoveT = Request["MOVE_TYPE"];
        if (MoveT == "101" || MoveT == "102" || MoveT == "105" || MoveT == "106" || MoveT == "161")
        {
            if (tbsavloc.Text != "")
            {
                Dialog.Open(btnBatch, "~/CDS/Public_Dialog/Public_Dialog.aspx", "",
                       370, 325, Dialog.PostBackType.AfterReturn,
                       "ZMCOD=" + lbZMATNR.Text, "ZPLNT=" + lbplant.Text, "savlocal=" + tbsavloc.Text, "file=ZIT014");
            }
            else
            {
                warring.Text = "請先選儲存位置";
            }
        }
        else
        {
            Dialog.Open(btnBatch, "~/CDS/Public_Dialog/Public_Dialog.aspx", "",
                      370, 325, Dialog.PostBackType.AfterReturn
                    , "file=ZIT014_2");
        }

    }
    private void setDefault()
    {
        ddmoveTypeControl();
        lbPO_NUMBER.Text = Request["PO_NUMBER"];
        lbZPOITEM.Text = Request["PO_ITEM"];
        lbZMATNR.Text = Request["MATERIAL"];
        lbZDESC.Text = Request["SHORT_TEXT"];
        lbZPOQTY.Text = Request["QUANTITY"];
        tbqty.Text = Request["ENTRY_QNT"];
        lbpi.Text = Request["OVER_DLV_TOL"];
        tbsavloc.Text = Request["STGE_LOC"];
        lbVENDOR.Text = Request["VENDOR_name"];
        lbORD_MATERIAL.Text = Request["ORD_MATERIAL"];
        ddmoveType.Enabled = (Request["MOVE_TYPE"] == "161") ? false : true;

        ddmoveType.SelectedValue = Request["MOVE_TYPE"];
        lbplant.Text = Request["PLANT"];
        tbBATCH.Text = Request["BATCH"];
        tbsavloc.Enabled = btnsavplace.Enabled = (lbZMATNR.Text == "") ? false : true;
    }

    private void ddmoveTypeControl()
    {
        ddmoveType.Items.Clear();
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ToString()))
        {
            string selectSQL = "SELECT ZMTYP,ZMTXT  from ZIT015 where ZMTYP in ('101','102','103','104','105','106','161','162') ";

            SqlDataAdapter da = new SqlDataAdapter(selectSQL, con);

            DataTable dt = new DataTable();
            da.Fill(dt);



            for (int i = 0; i <= dt.Rows.Count - 1; i++)
            {

                ddmoveType.Items.Add(new ListItem(dt.Rows[i]["ZMTYP"].ToString() + "/" + dt.Rows[i]["ZMTXT"].ToString(), dt.Rows[i]["ZMTYP"].ToString().Trim()));
            }


        }

    }
    void CDS_PO_Receipt_Dialog_PO_Receipt_I_Modify_Button1OnClick()
    {
        if (ddmoveType.SelectedValue != "104")
        {
            if (lbZMATNR.Text != "" && lbORD_MATERIAL.Text == "" && tbsavloc.Text.Trim() == "")
            {
                warring.Text = "料號有值,工單料號為空白時,儲存地點必填";
                return;
            }
        }
       
        Rg what = new Rg();
        if (!what.Ispositive(tbqty.Text))
        {
            warring.Text = "收貨數量不能為負";
            return;
        }
        if (!what.IsNumber(tbqty.Text, 11, 3) && tbqty.Text != "")
        {
            warring.Text = "數量必須是單行數字整數11位,小數3位 ";
            return;
        }
        string lbpi_ = (lbpi.Text == "") ? "0" : lbpi.Text;
        double TOTL = Convert.ToDouble(lbZPOQTY.Text) + (Convert.ToDouble(lbZPOQTY.Text) * Convert.ToDouble(lbpi_) / 100);
        if (Convert.ToDouble(tbqty.Text) > TOTL)
        {
            warring.Text = "收貨數量不能大於採購單數量";
            return;
        }
        if (lbZPOITEM.Text == "" || lbZDESC.Text == "" || lbZPOQTY.Text == "")
        {
            return;
        }
        if (!what.IsNumeric(tbqty.Text))
        {
            warring.Text = "收貨數量必須為數值";
            return;
        }
        if (ddmoveType.SelectedValue == "161" && tbBATCH.Text == "")
        {
            warring.Text = "是退貨項目時,批次必填";
            return;
        }
        string qty = (tbqty.Text == "") ? "0" : tbqty.Text;
        XmlDocument xmlDoc = new XmlDocument();
        XmlElement itemElement = xmlDoc.CreateElement("Item");
        itemElement.SetAttribute("key", Request["key"]);
        itemElement.SetAttribute("PO_NUMBER", Request["PO_NUMBER"]);
        itemElement.SetAttribute("PO_ITEM", Request["PO_ITEM"]);
        itemElement.SetAttribute("MATERIAL", Request["MATERIAL"]);
        itemElement.SetAttribute("SHORT_TEXT", Request["SHORT_TEXT"]);
        itemElement.SetAttribute("QUANTITY", Request["QUANTITY"]);
        itemElement.SetAttribute("ENTRY_QNT", qty);
        itemElement.SetAttribute("STGE_LOC", tbsavloc.Text);
        itemElement.SetAttribute("MOVE_TYPE", ddmoveType.SelectedValue);
        itemElement.SetAttribute("BATCH", tbBATCH.Text);
        Dialog.SetReturnValue(itemElement.OuterXml);
        this.CloseDialog();
    }


    protected void btnsavplace_Click(object sender, EventArgs e)
    {
        tbBATCH.Text = "";

        if (!string.IsNullOrEmpty(Dialog.GetReturnValue()))
        {
            string[] st = Dialog.GetReturnValue().Split('/');

            int i = 1;
            foreach (string s in st)
            {
                if (i == 1)
                {
                    tbsavloc.Text = s;
                }
                else
                {
                    lbsavname.Text = s;
                }
                i++;
            }

        }
        else
        {
            lbsavname.Text = "";
            tbsavloc.Text = "";
        }
        OpenDialog();

    }
    protected void btnBatch_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Dialog.GetReturnValue()))
        {
            string[] btch = Dialog.GetReturnValue().Split('/');

            foreach (string s in btch)
            {
                tbBATCH.Text = btch[0];
            }
        }
        else
        {
            tbBATCH.Text = "";
        }
    }
    protected void tbsavloc_TextChanged(object sender, EventArgs e)
    {
        string input = tbsavloc.Text.Trim();
        tbsavloc.Text = input;
        string gettxt = ZIT008(input);
        string[] st = gettxt.Split('/');
        int i = 1;
        foreach (string s in st)
        {
            if (i == 1)
            {
                tbsavloc.Text = s;
            }
            else
            {
                lbsavname.Text = s;
            }
            i++;
        }
    }

    private string ZIT008(string input)
    {
        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["connectionstring"].ToString()))
        {
            string selectSQL = "";


            selectSQL = "SELECT  distinct a.ZSLOT,b.ZSTXT from ZIT016 as a inner join ZIT008 as b on a.ZSLOT =b.ZSLOT and a.ZPLNT=b.ZPLNT where a.ZSLOT = @ZSLOT AND ZSLBIN <> '' order by a.ZSLOT;";




            SqlDataAdapter da = new SqlDataAdapter(selectSQL, con);
            da.SelectCommand.Parameters.Add("ZSLOT", SqlDbType.NVarChar).Value = input.PadLeft(4, '0');
            DataTable dt = new DataTable();
            da.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
            {
                warring.Text = "";
                return dt.Rows[0]["ZSLOT"].ToString().Trim() + "/" + dt.Rows[0]["ZSTXT"].ToString().Trim();

            }
            else
            {
                warring.Text = "此儲存位置儲格是空或無此儲存位置";
                return "/";
            }

        }
    }

}