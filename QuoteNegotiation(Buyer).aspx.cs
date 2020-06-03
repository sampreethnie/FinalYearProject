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
    public partial class QuoteNegotiation_Buyer_ : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(_ConnStr);
                con.Open();


                SqlCommand comrfqnumber = new SqlCommand("select distinct RFQ_Number from RFQ inner join SQ on RFQ.RFQ_Number = SQ.SQ_RFQ_Number where SQ_Submit = 'Y' and RFQ.RFQ_UserID = @buyerrfquserid", con);
                comrfqnumber.Parameters.AddWithValue("@buyerrfquserid", Session["M_Subscriber_UserID"]);

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
            SqlCommand cmd = new SqlCommand("select SQ_Slno,SQ_RFQ_Number,SQ_RFQ_CreationDate, SQ_RFQ_OriginCountry, SQ_RFQ_DestinationCountry, SQ_RFQ_OriginAirport, SQ_RFQ_DestinationAirport, SQ_RFQ_NumberofPackages, SQ_RFQ_TotalGrwt, SQ_RFQ_TotalVolwt, SQ_RFQ_TotalChwt, SQ_RFQ_PickupAddress, SQ_RFQ_DeliveryAddress, SQ_RFQ_PickupDate, SQ_RFQ_ReqTT, SQ_RFQ_QuoteDueBy, SQ_RFQ_Commodity, SQ_RFQ_HandlingInfo,SQ_UserID,SQ_OfferPrice,SQ_RFQ_ExpectedPrice,SQ_Timestamp,SQ_Submit, M_Company_Name, M_Currency_Name,SQ_OrderStatus,SQ_BuyerCurrency,SQ_RFQ_Company from SQ inner join M_Company on SQ.SQ_Company = M_Company.M_Company_Slno inner join M_Currency on M_Company.M_Company_Currency = M_Currency.M_Currency_Code where SQ_RFQ_Number =  '" + dropdownrfq.SelectedItem.Value + "'", con1);
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
            txtofferprice.Text = gvRow.Cells[21].Text;
            txtexpectedprice.Text = gvRow.Cells[22].Text;
            mpeqnb.Show();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();

            SqlCommand cmd = new SqlCommand("update SQ set SQ_RFQ_ExpectedPrice=@SQ_RFQ_ExpectedPrice where SQ_Slno=@SQ_Slno", con);
            cmd.Parameters.AddWithValue("@SQ_Slno", lblslno.Text);


            cmd.Parameters.AddWithValue("@SQ_RFQ_ExpectedPrice", txtexpectedprice.Text);




            cmd.ExecuteNonQuery();
            GridViewQnb.EditIndex = -1;

            BindGridView();


            con.Close();

            // Mail to be sent to seller







            




        }

       
       
        protected void GridViewQnb_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[26].Text == "Y")
                {
                    e.Row.Cells[26].Text = "Yes";
                    e.Row.Enabled = false;



                }
                if (e.Row.Cells[26].Text == "N")
                {
                    e.Row.Cells[26].Text = "No";
                    e.Row.Enabled = true;








                }
                if (e.Row.Cells[29].Text == "Y")
                {
                    e.Row.Cells[29].Text = "Order Placed";
                    e.Row.Enabled = false;



                }
                if (e.Row.Cells[29].Text == "N")
                {
                    e.Row.Cells[29].Text = "Order Pending";
                    e.Row.Enabled = true;








                }
            }

        }
        protected void btnorder_Click1(object sender, EventArgs e)
        {



            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();

            SqlCommand cmd = new SqlCommand("update SQ set SQ_OrderStatus= 'Y' where SQ_Slno=@SQ_Slno;insert into Orders(ORD_SQ_Slno,ORD_SQ_RFQ_Number,ORD_SQ_RFQ_CreationDate,ORD_SQ_RFQ_OriginCountry,ORD_SQ_RFQ_DestinationCountry,ORD_SQ_RFQ_OriginAirport,ORD_SQ_RFQ_DestinationAirport,ORD_SQ_RFQ_NumberofPackages,ORD_SQ_RFQ_TotalGrwt,ORD_SQ_RFQ_TotalVolwt,ORD_SQ_RFQ_TotalChwt,ORD_SQ_RFQ_PickupAddress,ORD_SQ_RFQ_DeliveryAddress,ORD_SQ_RFQ_PickupDate,ORD_SQ_RFQ_ReqTT,ORD_SQ_RFQ_QuoteDueBy,ORD_SQ_RFQ_Commodity,ORD_SQ_RFQ_HandlingInfo,ORD_SQ_Company,ORD_SQ_OfferPrice,ORD_SQ_RFQ_ExpectedPrice,ORD_SellerCurrency,ORD_UserID,ORD_SQ_BuyerCurrency,ORD_SQ_RFQ_Company,ORD_Date,ORD_BuyerUserID) values (@SQ_Slno,@ORD_SQ_RFQ_Number,@ORD_SQ_RFQ_CreationDate,@ORD_SQ_RFQ_OriginCountry,@ORD_SQ_RFQ_DestinationCountry,@ORD_SQ_RFQ_OriginAirport,@ORD_SQ_RFQ_DestinationAirport,@ORD_SQ_RFQ_NumberofPackages,@ORD_SQ_RFQ_TotalGrwt,@ORD_SQ_RFQ_TotalVolwt,@ORD_SQ_RFQ_TotalChwt,@ORD_SQ_RFQ_PickupAddress,@ORD_SQ_RFQ_DeliveryAddress,@ORD_SQ_RFQ_PickupDate,@ORD_SQ_RFQ_ReqTT,@ORD_SQ_RFQ_QuoteDueBy,@ORD_SQ_RFQ_Commodity,@ORD_SQ_RFQ_HandlingInfo,@ORD_SQ_Company,@ORD_SQ_OfferPrice,@ORD_SQ_RFQ_ExpectedPrice,@ORD_SellerCurrency,@ORD_UserID,@ORD_SQ_BuyerCurrency,@ORD_SQ_RFQ_Company,'" + DateTime.Now + "','" + Session["M_Subscriber_UserID"] + "' )", con);

            cmd.Parameters.AddWithValue("@SQ_Slno", txtsqslno.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_Number", txtrfqnumber.Text);



            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_CreationDate", txtcreationdate.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_OriginCountry", txtorigincountry.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_DestinationCountry", txtdestinationcountry.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_OriginAirport", txtoriginairport.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_DestinationAirport", txtdestinationairport.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_NumberofPackages", txtnumberofpackages.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_TotalGrwt", txtgrossweight.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_TotalVolwt", txtvolumetricweight.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_TotalChwt", txtchargeableweight.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_PickupAddress", txtpickupaddress.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_DeliveryAddress", txtdeliveryaddress.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_PickupDate", txtpickupdate.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_ReqTT", txttransittime.Text);

            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_QuoteDueBy", txtquotedueby.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_Commodity", txtcommodity.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_HandlingInfo", txthandlinginfo.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_Company", txtsellercompany.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_OfferPrice", txtofferprice1.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_ExpectedPrice", txtexpectedprice1.Text);
            cmd.Parameters.AddWithValue("@ORD_SellerCurrency", txtsellercurrency.Text);
            cmd.Parameters.AddWithValue("@ORD_UserID", txtsellermail.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_BuyerCurrency", txtbuyercurrency.Text);
            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_Company", txtbuyercompany.Text);







            cmd.ExecuteNonQuery();
            GridViewQnb.EditIndex = -1;

            BindGridView();


            
            ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('The order was placed successfully!!!');", true);

            //Mail Implementation 
            SqlConnection con1 = new SqlConnection(_ConnStr);
            string sellercompanyname = txtsellercompany.Text;
            SqlCommand cmd1 = new SqlCommand("select ORD_Number,ORD_Date from Orders where ORD_SQ_Company = '" + sellercompanyname + "'", con);
            string email = txtsellermail.Text;
            string ordnumber, orddate;



            con1.Open();
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                ordnumber = dr[0].ToString();
                orddate = dr[1].ToString();
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(Session["M_Subscriber_UserID"].ToString());
                msg.To.Add(email);
                msg.Subject = "Order Confirmation for Air Freight against Transaction Number '" + txtrfqnumber.Text + "'";
                msg.IsBodyHtml = true;

                msg.Body = "Hi you have received a new order '" + ordnumber + "' " + "<br/>" +
                    "Dated '" + orddate + "' from '" + txtbuyercompany.Text + "' for Air Freight " + "<br/>" +
                    "From '" + txtoriginairport.Text + "' to '" + txtdestinationairport.Text + "' against" + "<br/>" +
                    "Transaction Number '" + txtrfqnumber.Text + "'";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("freightlogisticsnie@gmail.com", "niemysuru");
                smtp.EnableSsl = true;
                smtp.Send(msg);

                
            }

            clear();
        }

        void clear()
        {
            txtsqslno.Text = string.Empty;
            txtrfqnumber.Text = string.Empty;
            txtcreationdate.Text = string.Empty;
            txtorigincountry.Text = string.Empty;
            txtdestinationcountry.Text = string.Empty;
            txtoriginairport.Text = string.Empty;
            txtdestinationairport.Text = string.Empty;
            txtnumberofpackages.Text = string.Empty;
            txtgrossweight.Text = string.Empty;
            txtvolumetricweight.Text = string.Empty;
            txtchargeableweight.Text = string.Empty;
            txtpickupaddress.Text = string.Empty;
            txtdeliveryaddress.Text = string.Empty;
            txtpickupdate.Text = string.Empty;
            txttransittime.Text = string.Empty;
            txtquotedueby.Text = string.Empty;
            txtcommodity.Text = string.Empty;
            txthandlinginfo.Text = string.Empty;
            txtsellercompany.Text = string.Empty;
            txtofferprice1.Text = string.Empty;
            txtexpectedprice1.Text = string.Empty;
            txtsellercurrency.Text = string.Empty;
            txtsellermail.Text = string.Empty;
            txtbuyercurrency.Text = string.Empty;
            txtbuyercompany.Text = string.Empty;
        }
        protected void GridViewQnb_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridViewQnb.SelectedRow;
            txtsqslno.Text = row.Cells[2].Text;
           
            txtrfqnumber.Text = row.Cells[3].Text;
            txtcreationdate.Text = row.Cells[4].Text; 
            txtorigincountry.Text = row.Cells[5].Text;
            txtdestinationcountry.Text = row.Cells[6].Text;
            txtoriginairport.Text = row.Cells[7].Text;
            txtdestinationairport.Text = row.Cells[8].Text;
            txtnumberofpackages.Text = row.Cells[9].Text;
            txtgrossweight.Text = row.Cells[10].Text;
            txtvolumetricweight.Text = row.Cells[11].Text;
            txtchargeableweight.Text = row.Cells[12].Text;
            txtpickupaddress.Text = row.Cells[13].Text;
            txtdeliveryaddress.Text = row.Cells[14].Text;
            txtpickupdate.Text = row.Cells[15].Text;
            txttransittime.Text = row.Cells[16].Text;
            txtquotedueby.Text = row.Cells[17].Text;
            txtcommodity.Text = row.Cells[18].Text;
            txthandlinginfo.Text = row.Cells[19].Text;
            txtsellercompany.Text = row.Cells[20].Text;
            txtofferprice1.Text = row.Cells[21].Text;
            txtexpectedprice1.Text = row.Cells[22].Text;
            txtsellercurrency.Text = row.Cells[23].Text;
            txtsellermail.Text = row.Cells[24].Text;
            txtbuyercurrency.Text = row.Cells[27].Text;
            txtbuyercompany.Text = row.Cells[28].Text;
            
           

        }

        protected void btncancel1_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}