using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;

namespace FinalYearProject
{
    public partial class InvoiceReport_Buyer_ : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection concompany = new SqlConnection(_ConnStr);
                concompany.Open();
                SqlCommand comcompany = new SqlCommand("select distinct TM_INVOICE.TM_INVOICE_COMPANYSLNO,M_Company.M_Company_Name from TM_INVOICE inner join M_Company on TM_INVOICE.TM_INVOICE_COMPANYSLNO = M_Company.M_Company_Slno ", concompany);
                SqlDataAdapter dacompany = new SqlDataAdapter(comcompany);
                DataSet dscompany = new DataSet();
                dacompany.Fill(dscompany);
                dropdowncustomer.DataValueField = dscompany.Tables[0].Columns["TM_INVOICE_COMPANYSLNO"].ToString();
                dropdowncustomer.DataTextField = dscompany.Tables[0].Columns["M_Company_Name"].ToString();
                dropdowncustomer.DataSource = dscompany.Tables[0];
                dropdowncustomer.DataBind();
                concompany.Close();
                dropdowncustomer.Items.Insert(0, new ListItem("--Select Company--"));
                btnexport.Visible = false;

                lblusername.Text = "Username:" + Session["M_Subscriber_UserID"];
                SqlConnection con2 = new SqlConnection(_ConnStr);
                con2.Open();
                string str = "select M_Company_Slno,M_Company_Name,M_Company_BuyerSellerFlag from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
                SqlCommand com1 = new SqlCommand(str, con2);
                SqlDataAdapter da1 = new SqlDataAdapter(com1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                lblcompanyname.Text = "CompanyName:" + ds1.Tables[0].Rows[0]["M_Company_Name"].ToString();
                //lblbuyersellerflag.Text = "Type:" + ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString();
                if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "b")
                {
                    ButtonSeller.Visible = false;


                }
                else
                {
                    ButtonBuyer.Visible = false;

                }
            }

        }
        private void BindGridView()
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            if (chkall.Checked)
            {
                SqlCommand cmd = new SqlCommand("select TM_INVOICE_Slno,TM_INVOICE.TM_INVOICE_No,TM_INVOICE.TM_INVOICE_Date,M_Company.M_Company_Name,TM_INVOICE.TM_INVOICE_RFQNumber,ShipmentDetailsSeller.hawb,ShipmentDetailsSeller.delivery,TM_INVOICE.TM_INVOICE_Status from TM_INVOICE inner join ShipmentDetailsSeller on TM_INVOICE.TM_INVOICE_RFQNumber = ShipmentDetailsSeller.ShipmentRFQ_Number inner join M_Company on TM_INVOICE.TM_INVOICE_COMPANYSLNO = M_Company.M_Company_Slno where TM_INVOICE_Date between @fromdate and @todate and  TM_INVOICE.TM_INVOICE_Customer='freightoptics' and TM_INVOICE_Status = 'Y' and TM_INVOICE_Status = 'P' and TM_INVOICE_Status = 'N'   ;select SUM(TD_INVOICE_TAXAMOUNT), SUM(TD_INVOICE_TOTALAMOUNT) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No   group by TM_INVOICE.TM_INVOICE_No", con);
                cmd.Parameters.AddWithValue("@customerinvoice", dropdowncustomer.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(txtfrominvoicedate.Text));
                cmd.Parameters.AddWithValue("@todate", Convert.ToDateTime(txttoinvoicedate.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                InvoiceGridView.DataSource = ds;
                InvoiceGridView.DataBind();
                con.Close();
            }

            else if (chkapproved.Checked)
            {
                SqlCommand cmd = new SqlCommand("select TM_INVOICE_Slno,TM_INVOICE.TM_INVOICE_No,TM_INVOICE.TM_INVOICE_Date,M_Company.M_Company_Name,TM_INVOICE.TM_INVOICE_RFQNumber,ShipmentDetailsSeller.hawb,ShipmentDetailsSeller.delivery,TM_INVOICE.TM_INVOICE_Status from TM_INVOICE inner join ShipmentDetailsSeller on TM_INVOICE.TM_INVOICE_RFQNumber = ShipmentDetailsSeller.ShipmentRFQ_Number inner join M_Company on TM_INVOICE.TM_INVOICE_COMPANYSLNO = M_Company.M_Company_Slno where TM_INVOICE_Date between @fromdate and @todate and  TM_INVOICE.TM_INVOICE_Customer='freightoptics' and TM_INVOICE_Status = 'Y' ;select SUM(TD_INVOICE_TAXAMOUNT), SUM(TD_INVOICE_TOTALAMOUNT) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No   group by TM_INVOICE.TM_INVOICE_No; ", con);
                cmd.Parameters.AddWithValue("@customerinvoice", dropdowncustomer.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(txtfrominvoicedate.Text));
                cmd.Parameters.AddWithValue("@todate", Convert.ToDateTime(txttoinvoicedate.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                InvoiceGridView.DataSource = ds;
                InvoiceGridView.DataBind();
                con.Close();



            }

            else if (chkrejected.Checked)
            {
                SqlCommand cmd = new SqlCommand("select TM_INVOICE_Slno,TM_INVOICE.TM_INVOICE_No,TM_INVOICE.TM_INVOICE_Date,M_Company.M_Company_Name,TM_INVOICE.TM_INVOICE_RFQNumber,ShipmentDetailsSeller.hawb,ShipmentDetailsSeller.delivery,TM_INVOICE.TM_INVOICE_Status from TM_INVOICE inner join ShipmentDetailsSeller on TM_INVOICE.TM_INVOICE_RFQNumber = ShipmentDetailsSeller.ShipmentRFQ_Number inner join M_Company on TM_INVOICE.TM_INVOICE_COMPANYSLNO = M_Company.M_Company_Slno where TM_INVOICE_Date between @fromdate and @todate and  TM_INVOICE.TM_INVOICE_Customer='freightoptics' and TM_INVOICE_Status = 'N'   ;select SUM(TD_INVOICE_TAXAMOUNT), SUM(TD_INVOICE_TOTALAMOUNT) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No   group by TM_INVOICE.TM_INVOICE_No ", con);
                cmd.Parameters.AddWithValue("@customerinvoice", dropdowncustomer.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(txtfrominvoicedate.Text));
                cmd.Parameters.AddWithValue("@todate", Convert.ToDateTime(txttoinvoicedate.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                InvoiceGridView.DataSource = ds;
                InvoiceGridView.DataBind();
                con.Close();



            }


            else if (chkpending.Checked)
            {
                SqlCommand cmd = new SqlCommand("select TM_INVOICE_Slno,TM_INVOICE.TM_INVOICE_No,TM_INVOICE.TM_INVOICE_Date,M_Company.M_Company_Name,TM_INVOICE.TM_INVOICE_RFQNumber,ShipmentDetailsSeller.hawb,ShipmentDetailsSeller.delivery,TM_INVOICE.TM_INVOICE_Status from TM_INVOICE inner join ShipmentDetailsSeller on TM_INVOICE.TM_INVOICE_RFQNumber = ShipmentDetailsSeller.ShipmentRFQ_Number inner join M_Company on TM_INVOICE.TM_INVOICE_COMPANYSLNO = M_Company.M_Company_Slno where TM_INVOICE_Date between @fromdate and @todate and  TM_INVOICE.TM_INVOICE_Customer='freightoptics' and TM_INVOICE_Status = 'Y' or TM_INVOICE_Status = 'P' or TM_INVOICE_Status = 'N'   ;select SUM(TD_INVOICE_TAXAMOUNT), SUM(TD_INVOICE_TOTALAMOUNT) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No   group by TM_INVOICE.TM_INVOICE_No ", con);
                cmd.Parameters.AddWithValue("@customerinvoice", dropdowncustomer.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(txtfrominvoicedate.Text));
                cmd.Parameters.AddWithValue("@todate", Convert.ToDateTime(txttoinvoicedate.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                InvoiceGridView.DataSource = ds;
                InvoiceGridView.DataBind();
                con.Close();



            }
            con.Close();
        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        protected void ExportExcel(object sender, EventArgs e)
        {

            BindGridView();
            //SqlConnection con = new SqlConnection(_ConnStr);

            //if (chkall.Checked)
            //{
            //    SqlCommand com = new SqlCommand(" select TM_INVOICE.TM_INVOICE_No,TM_INVOICE.TM_INVOICE_Date,TM_INVOICE.TM_INVOICE_Customer,TM_INVOICE.TM_INVOICE_RFQNumber,ShipmentDetailsSeller.hawb,ShipmentDetailsSeller.delivery from TM_INVOICE inner join ShipmentDetailsSeller on TM_INVOICE.TM_INVOICE_RFQNumber = ShipmentDetailsSeller.ShipmentRFQ_Number where TM_INVOICE_Date between @fromdate and @todate and  TM_INVOICE.TM_INVOICE_Customer= @customerinvoice and TM_INVOICE_Status = 'Y' or TM_INVOICE_Status = 'P' or TM_INVOICE_Status = 'N';select SUM(TD_INVOICE_TAXAMOUNT), SUM(TD_INVOICE_TOTALAMOUNT) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No   group by TM_INVOICE.TM_INVOICE_No; ");
            //    com.Parameters.AddWithValue("@customerinvoice", dropdowncustomer.SelectedItem.Text);
            //    com.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(txtfrominvoicedate.Text));
            //    com.Parameters.AddWithValue("@todate", Convert.ToDateTime(txttoinvoicedate.Text));

            //    SqlDataAdapter sda = new SqlDataAdapter();
            //    com.Connection = con;
            //    sda.SelectCommand = com;
            //    DataTable dt = new DataTable();
            //    sda.Fill(dt);
            //XLWorkbook wb = new XLWorkbook();
            //wb.Worksheets.Add(dt,"TM_INVOICE");
            //wb.Worksheets.Add(dt, "ShipmentDetailsSeller");

            //Response.Clear();
            //Response.Buffer = true;
            //Response.Charset = "";

            //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            //Response.AddHeader("content-disposition", "attachment;filename=SqlExport.xlsx");
            //MemoryStream MyMemoryStream = new MemoryStream();

            //wb.SaveAs(MyMemoryStream);
            //MyMemoryStream.WriteTo(Response.OutputStream);
            //Response.Flush();
            //Response.End();

            btnexport.Visible = true;


        }
        protected void InvoiceDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[7].Text == "Y")
                {
                    e.Row.Cells[7].Text = "Accepted";
                }
                if (e.Row.Cells[7].Text == "N")
                {
                    e.Row.Cells[7].Text = "Rejected";
                }
                if (e.Row.Cells[7].Text == "P")
                {
                    e.Row.Cells[7].Text = "Pending";
                }
                string invoiceno = e.Row.Cells[1].Text;

                SqlConnection conn = new SqlConnection(_ConnStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("select SUM(TD_INVOICE_TAXAMOUNT) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No where TM_INVOICE_No = @TM_INVOICE_No group by TM_INVOICE.TM_INVOICE_No", conn);
                cmd.Parameters.Add(new SqlParameter("@TM_INVOICE_No", invoiceno));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Label Tax = (Label)e.Row.FindControl("lbltaxamt");
                    object value = dr.GetValue(0);
                    Tax.Text = value.ToString();
                }
                dr.Close();


                SqlCommand cmd1 = new SqlCommand("select SUM(TD_INVOICE_TOTALAMOUNT),SUM(TD_INVOICE_AMOUNTBC) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No where TM_INVOICE_No = @TM_INVOICE_No group by TM_INVOICE.TM_INVOICE_No", conn);
                cmd1.Parameters.Add(new SqlParameter("@TM_INVOICE_No", invoiceno));
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    Label Total = (Label)e.Row.FindControl("lbltotalamt");
                    Label Amtbc = (Label)e.Row.FindControl("lblbilledamt");
                    object value = dr1.GetValue(0);
                    object valuebase = dr1.GetValue(1);

                    Total.Text = value.ToString();
                    Amtbc.Text = valuebase.ToString();
                }
                dr1.Close();


                //SqlCommand cmd2 = new SqlCommand("select SUM(TD_INVOICE_TAXAMOUNT) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No where TM_INVOICE.TM_INVOICE_RFQNumber = @TM_INVOICE_RFQNumber;", conn);
                //cmd2.Parameters.Add(new SqlParameter("@TM_INVOICE_RFQNumber", txtrfqnumber.Text));
                //SqlDataReader dr2 = cmd2.ExecuteReader();
                //if (dr2.Read())
                //{

                //    object value = dr2.GetValue(0);
                //    txtfinaltaxamount.Text = value.ToString();
                //}
                //dr2.Close();

                //SqlCommand cmd3 = new SqlCommand("select SUM(TD_INVOICE_TOTALAMOUNT),SUM(TD_INVOICE_AMOUNTBC) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No where TM_INVOICE.TM_INVOICE_RFQNumber = @TM_INVOICE_RFQNumber", conn);
                //cmd3.Parameters.Add(new SqlParameter("@TM_INVOICE_RFQNumber", txtrfqnumber.Text));
                //SqlDataReader dr3 = cmd3.ExecuteReader();
                //if (dr3.Read())
                //{

                //    object value = dr3.GetValue(0);
                //    object valuebc = dr3.GetValue(1);
                //    txtfinaltotalamount.Text = value.ToString();
                //    txtfinalbilledamount.Text = valuebc.ToString();
                //}
                //dr3.Close();
            }
        }

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=invoicereports.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                InvoiceGridView.AllowPaging = false;
                this.BindGridView();

                InvoiceGridView.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in InvoiceGridView.HeaderRow.Cells)
                {
                    cell.BackColor = InvoiceGridView.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in InvoiceGridView.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = InvoiceGridView.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = InvoiceGridView.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                InvoiceGridView.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnClick_Cancel(object sender, EventArgs e)
        {
            txtfrominvoicedate.Text = string.Empty;
            txttoinvoicedate.Text = string.Empty;
            dropdowncustomer.SelectedIndex = 0;
        }

    }
}