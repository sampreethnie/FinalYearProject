using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalYearProject
{
    public partial class OrderReport_Buyer_ : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection concompany = new SqlConnection(_ConnStr);
                concompany.Open();
                SqlCommand comcompany = new SqlCommand("select distinct ORD_SQ_Company from Orders ", concompany);
                SqlDataAdapter dacompany = new SqlDataAdapter(comcompany);
                DataSet dscompany = new DataSet();
                dacompany.Fill(dscompany);
                dropdownseller.DataTextField = dscompany.Tables[0].Columns["ORD_SQ_Company"].ToString();
                dropdownseller.DataSource = dscompany.Tables[0];
                dropdownseller.DataBind();
                concompany.Close();
                dropdownseller.Items.Insert(0, new ListItem("--Select Company--"));
                btnexport.Visible = false;
            }

        }
        private void BindGridView()
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            
            
                SqlCommand cmd = new SqlCommand("select ORD_Number,ORD_Date as ORD_Date,ORD_SQ_RFQ_Number,ORD_SQ_Company,ORD_SQ_RFQ_OriginCountry,ORD_SQ_RFQ_DestinationCountry,ORD_SQ_RFQ_OriginAirport,ORD_SQ_RFQ_DestinationAirport,ORD_SQ_RFQ_NumberofPackages,ORD_SQ_RFQ_TotalChwt,ORD_SQ_RFQ_Commodity,ORD_SQ_BuyerCurrency,ORD_SellerCurrency from Orders where ORD_Date between @fromdate and @todate and ORD_SQ_Company = @company", con);
                cmd.Parameters.AddWithValue("@company", dropdownseller.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(txtfromorderdate.Text));
                cmd.Parameters.AddWithValue("@todate", Convert.ToDateTime(txttoorderdate.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                OrderGridView.DataSource = ds;
                OrderGridView.DataBind();
                con.Close();
        



            


            



            
          
        }
        protected void ExportExcel(object sender, EventArgs e)
        {

            BindGridView();

            btnexport.Visible = true;


        }
        protected void InvoiceDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            
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
                OrderGridView.AllowPaging = false;
                this.BindGridView();

                OrderGridView.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in OrderGridView.HeaderRow.Cells)
                {
                    cell.BackColor = OrderGridView.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in OrderGridView.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = OrderGridView.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = OrderGridView.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                OrderGridView.RenderControl(hw);

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
            txtfromorderdate.Text = string.Empty;
            txttoorderdate.Text = string.Empty;
            dropdownseller.SelectedIndex = 0;
        }

    }
}