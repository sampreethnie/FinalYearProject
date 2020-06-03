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
    public partial class RateComparisonReport : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection concompany = new SqlConnection(_ConnStr);
                concompany.Open();
                SqlCommand comcompany = new SqlCommand("select distinct RFQ_Number from RFQ where RFQ_UserID = @userid  ", concompany);
                comcompany.Parameters.AddWithValue("@userid", Session["M_Subscriber_UserID"]);
                SqlDataAdapter dacompany = new SqlDataAdapter(comcompany);
                DataSet dscompany = new DataSet();
                dacompany.Fill(dscompany);
                dropdownseller.DataValueField = dscompany.Tables[0].Columns["RFQ_Number"].ToString();
                dropdownseller.DataSource = dscompany.Tables[0];
                dropdownseller.DataBind();
                concompany.Close();
                dropdownseller.Items.Insert(0, new ListItem("--Select RFQ Number--"));
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


            SqlCommand cmd = new SqlCommand("select SQ_Slno,SQ_RFQ_Number,M_Company.M_Company_Name,SQ_OfferPrice,SQ_RFQ_ExpectedPrice,SQ_RFQ_OriginCountry,SQ_RFQ_DestinationCountry,SQ_RFQ_OriginAirport,SQ_RFQ_DestinationAirport from SQ inner join M_Company on SQ.SQ_Company = M_Company.M_Company_Slno where SQ_RFQ_Number = @rfqnumber order by SQ_OfferPrice", con);
            cmd.Parameters.AddWithValue("@rfqnumber", dropdownseller.SelectedItem.Value);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            RateComparisonGridView.DataSource = ds;
            RateComparisonGridView.DataBind();
            con.Close();
        }
        protected void ExportExcel(object sender, EventArgs e)
        {

            BindGridView();

            btnexport.Visible = true;


        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }



        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=ratecomparisonreports.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                RateComparisonGridView.AllowPaging = false;
                this.BindGridView();

                RateComparisonGridView.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in RateComparisonGridView.HeaderRow.Cells)
                {
                    cell.BackColor = RateComparisonGridView.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in RateComparisonGridView.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = RateComparisonGridView.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = RateComparisonGridView.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                RateComparisonGridView.RenderControl(hw);

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
            
            dropdownseller.SelectedIndex = 0;
        }

    }
}