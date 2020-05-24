using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;

namespace FinalYearProject
{
    public partial class QuoteNegotiation_Seller_ : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(_ConnStr);
                con.Open();


                SqlCommand comrfqnumber = new SqlCommand("select RFQ_Number from RFQ", con);


                SqlDataAdapter darfqnumber = new SqlDataAdapter(comrfqnumber);
                DataSet dsrfqnumber = new DataSet();
                darfqnumber.Fill(dsrfqnumber);

                dropdownrfq.DataValueField = dsrfqnumber.Tables[0].Columns["RFQ_Number"].ToString();
                //dropdownrfq.DataTextField = dsrfqnumber.Tables[0].Columns["RFQ_Number"].ToString();
                dropdownrfq.DataSource = dsrfqnumber.Tables[0];
                dropdownrfq.DataBind();
                con.Close();
                dropdownrfq.Items.Insert(0, new ListItem("--Select RFQ No--", "0"));
                lblusername.Text = "Username:" + Session["M_Subscriber_UserID"];
                SqlConnection con1 = new SqlConnection(_ConnStr);
                con1.Open();
                string str = "select M_Company_Slno,M_Company_Name,M_Company_BuyerSellerFlag from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
                SqlCommand com1 = new SqlCommand(str, con1);
                SqlDataAdapter da1 = new SqlDataAdapter(com1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                lblcompanyname.Text = "CompanyName:" + ds1.Tables[0].Rows[0]["M_Company_Name"].ToString();
                Session["CompanySlnosq"] = ds1.Tables[0].Rows[0]["M_Company_Slno"];
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
            SqlConnection con1 = new SqlConnection(_ConnStr);

            DataTable dt = new DataTable();
            con1.Open();
            SqlCommand cmd = new SqlCommand("select SQ_Slno,SQ_RFQ_Number,SQ_RFQ_CreationDate, SQ_RFQ_OriginCountry, SQ_RFQ_DestinationCountry, SQ_RFQ_OriginAirport, SQ_RFQ_DestinationAirport, SQ_RFQ_NumberofPackages, SQ_RFQ_TotalGrwt, SQ_RFQ_TotalVolwt, SQ_RFQ_TotalChwt, SQ_RFQ_PickupAddress, SQ_RFQ_DeliveryAddress, SQ_RFQ_PickupDate, SQ_RFQ_ReqTT, SQ_RFQ_QuoteDueBy, SQ_RFQ_Commodity, SQ_RFQ_HandlingInfo,SQ_UserID,SQ_OfferPrice,SQ_RFQ_ExpectedPrice,SQ_Timestamp,SQ_RFQ_Company,SQ_BuyerCurrency,SQ_Submit,SQ_OrderStatus from SQ  where SQ_RFQ_Number =  '" + dropdownrfq.SelectedItem.Value + "'", con1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            GridViewQnb.DataSource = dt;
            GridViewQnb.DataBind();
            lbltotalcount.Text = GridViewQnb.Rows.Count.ToString();

            con1.Close();
        }
        protected void dropdownrfq_SelectedIndexChanged(object sender, EventArgs e)
        {

            BindGridView();

        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        protected void ImageButtonqnb_Click(object sender, EventArgs e)
        {
            ImageButton imgbtnqnb = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)imgbtnqnb.NamingContainer;
            lblslno.Text = GridViewQnb.DataKeys[gvRow.RowIndex].Value.ToString();
            txtofferprice.Text = gvRow.Cells[20].Text;
            txtexpectedprice.Text = gvRow.Cells[21].Text;
            mpeqnb.Show();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();

            SqlCommand cmd = new SqlCommand("update SQ set SQ_OfferPrice=@SQ_OfferPrice where SQ_Slno=@SQ_Slno", con);
            cmd.Parameters.AddWithValue("@SQ_Slno", lblslno.Text);


            cmd.Parameters.AddWithValue("@SQ_OfferPrice", txtofferprice.Text);




            cmd.ExecuteNonQuery();
            GridViewQnb.EditIndex = -1;

            BindGridView();


            con.Close();
        }

        protected void GridViewQnb_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[26].Text == "Y")
                {
                    e.Row.Cells[26].Text = "Order Placed";
                    e.Row.Enabled = false;



                }
                if (e.Row.Cells[26].Text == "N")
                {
                    e.Row.Cells[26].Text = "Order Pending";
                    e.Row.Enabled = true;








                }
            }
        }
    }
}