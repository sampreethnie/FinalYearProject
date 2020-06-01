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
    public partial class InvoiceAudit : System.Web.UI.Page
    {
        
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(_ConnStr);
                con.Open();


                SqlCommand comrfqnumber = new SqlCommand("select distinct ORD_SQ_RFQ_Number from Orders where ORD_BuyerUserID = @ORD_BuyerUserID  ", con);
                comrfqnumber.Parameters.AddWithValue("@ORD_BuyerUserID", Session["M_Subscriber_UserID"]);

                SqlDataAdapter darfqnumber = new SqlDataAdapter(comrfqnumber);
                DataSet dsrfqnumber = new DataSet();
                darfqnumber.Fill(dsrfqnumber);

                dropdownrfqaudit.DataTextField = dsrfqnumber.Tables[0].Columns["ORD_SQ_RFQ_Number"].ToString();

                dropdownrfqaudit.DataSource = dsrfqnumber.Tables[0];
                dropdownrfqaudit.DataBind();
                con.Close();
                dropdownrfqaudit.Items.Insert(0, new ListItem("--Select RFQ No--", "0"));

                lblusername.Text = "Username:" + Session["M_Subscriber_UserID"];
                SqlConnection con1 = new SqlConnection(_ConnStr);
                con1.Open();
                string str = "select M_Company_Slno,M_Company_Name,M_Company_BuyerSellerFlag,M_Company_Currency from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
                SqlCommand com1 = new SqlCommand(str, con1);
                SqlDataAdapter da1 = new SqlDataAdapter(com1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                lblcompanyname.Text = "CompanyName:" + ds1.Tables[0].Rows[0]["M_Company_Name"].ToString();
                Session["CompanySlnosq"] = ds1.Tables[0].Rows[0]["M_Company_Slno"];
                Session["SellerCurrencysq"] = ds1.Tables[0].Rows[0]["M_Company_Currency"];
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
            SqlCommand cmd = new SqlCommand("select TM_INVOICE_Slno,TM_INVOICE_No,TM_INVOICE_ShipmentRefNo,TM_INVOICE_Customer,TM_INVOICE_Currency,TM_INVOICE_Date,TM_INVOICE_RFQNumber,TM_INVOICE_Status,TM_INVOICE_GLCode from TM_INVOICE where TM_INVOICE_RFQNumber = '" + dropdownrfqaudit.SelectedItem.Text + "' ; select SUM(TD_INVOICE_TAXAMOUNT) from TD_INVOICE inner join  TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No where TM_INVOICE.TM_INVOICE_RFQNumber = '" + dropdownrfqaudit.SelectedItem.Text + "'  group by TM_INVOICE.TM_INVOICE_No;", con);
            SqlDataAdapter dainvoice = new SqlDataAdapter(cmd);
            DataSet dsinvoice = new DataSet();
            dainvoice.Fill(dsinvoice);
            InvoiceGridView.DataSource = dsinvoice;
            InvoiceGridView.DataBind();
            con.Close();
        }
        protected void InvoiceDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
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


                SqlCommand cmd2 = new SqlCommand("select SUM(TD_INVOICE_TAXAMOUNT) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No where TM_INVOICE.TM_INVOICE_RFQNumber = @TM_INVOICE_RFQNumber;", conn);
                cmd2.Parameters.Add(new SqlParameter("@TM_INVOICE_RFQNumber", txtrfqnumber.Text));
                SqlDataReader dr2 = cmd2.ExecuteReader();
                if (dr2.Read())
                {
                    
                    object value = dr2.GetValue(0);
                    txtfinaltaxamount.Text = value.ToString();
                }
                dr2.Close();

                SqlCommand cmd3 = new SqlCommand("select SUM(TD_INVOICE_TOTALAMOUNT),SUM(TD_INVOICE_AMOUNTBC) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No where TM_INVOICE.TM_INVOICE_RFQNumber = @TM_INVOICE_RFQNumber", conn);
                cmd3.Parameters.Add(new SqlParameter("@TM_INVOICE_RFQNumber", txtrfqnumber.Text));
                SqlDataReader dr3 = cmd3.ExecuteReader();
                if (dr3.Read())
                {

                    object value = dr3.GetValue(0);
                    object valuebc = dr3.GetValue(1);
                    txtfinaltotalamount.Text = value.ToString();
                    txtfinalbilledamount.Text = valuebc.ToString();
                }
                dr3.Close();

                //Label taxamountotal = (Label)e.Row.FindControl("lbltaxamt");
                //taxtotal = taxtotal + Int32.Parse(taxamountotal.Text);
                //Label totalamountfinal = (Label)e.Row.FindControl("lbltotalamt");
                //finaltotal = finaltotal + Int32.Parse(totalamountfinal.Text);
                if(e.Row.Cells[10].Text == "P")
                {
                    e.Row.Cells[10].Text = "Pending";
                }
                if (e.Row.Cells[10].Text == "Y")
                {
                    e.Row.Cells[10].Text = "Accepted";
                    e.Row.Enabled = false;
                }
                if(e.Row.Cells[10].Text == "N")
                {
                    e.Row.Cells[10].Text = "Rejected";
                }

            }

          
        }

        protected void dropdownrfqaudit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select ORD_SQ_RFQ_Number,ORD_SQ_RFQ_CreationDate,ORD_SQ_RFQ_OriginCountry,ORD_SQ_RFQ_DestinationCountry,ORD_SQ_RFQ_OriginAirport,ORD_SQ_RFQ_DestinationAirport,ORD_SQ_RFQ_NumberofPackages,ORD_SQ_RFQ_TotalGrwt,ORD_SQ_RFQ_TotalVolwt,ORD_SQ_RFQ_TotalChwt,ORD_SQ_RFQ_PickupAddress,ORD_SQ_RFQ_DeliveryAddress,ORD_SQ_RFQ_PickupDate,ORD_SQ_RFQ_ReqTT,ORD_SQ_RFQ_Commodity,ORD_SQ_RFQ_HandlingInfo,ORD_SQ_Company,ORD_SQ_OfferPrice,ORD_SellerCurrency,ORD_SQ_BuyerCurrency from Orders where " + " ORD_SQ_RFQ_Number= @ORD_SQ_RFQ_Number";
            SqlConnection con = new SqlConnection(_ConnStr);
            SqlCommand cmd = new SqlCommand();

            cmd.Parameters.AddWithValue("@ORD_SQ_RFQ_Number", dropdownrfqaudit.SelectedItem.Text);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = con;
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                txtrfqnumber.Text = sdr[0].ToString();

                txtcreationdate.Text = sdr[1].ToString();

                txtorigincountry.Text = sdr[2].ToString();

                txtdestinationcountry.Text = sdr[3].ToString();
                txtoriginairport.Text = sdr[4].ToString();
                txtdestinationairport.Text = sdr[5].ToString();
                txtnumberofpackages.Text = sdr[6].ToString();

                txtgrossweight.Text = sdr[7].ToString();
                txtvolumetricweight.Text = sdr[8].ToString();
                txtchargeableweight.Text = sdr[9].ToString();
                txtpickupaddress.Text = sdr[10].ToString();
                txtdeliveryaddress.Text = sdr[11].ToString();

                txtpickupdate.Text = sdr[12].ToString();
                txttransittime.Text = sdr[13].ToString();
                txtcommodity.Text = sdr[14].ToString();
                txthandlinginfo.Text = sdr[15].ToString();
                txtsellercompany.Text = sdr[16].ToString();
                txtofferprice1.Text = sdr[17].ToString();
                txtsellercurrency.Text = sdr[18].ToString();
                txtbuyercurrency.Text = sdr[19].ToString();

            }

            con.Close();
            BindGridView();
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        protected void imgbtninvoiceaudit_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton imgbtnaudit = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)imgbtnaudit.NamingContainer;
            lblslno.Text = InvoiceGridView.DataKeys[gvRow.RowIndex].Value.ToString();
            txtinvoicenumber.Text = gvRow.Cells[1].Text;
            txtinvoicedate.Text = gvRow.Cells[2].Text;
            txtshipmentreferences.Text = gvRow.Cells[3].Text;
            txtcustomer.Text = gvRow.Cells[4].Text;
            txtcurrency.Text = gvRow.Cells[5].Text;
            Label _Labeltaxamount = gvRow.FindControl("lbltaxamt") as Label;
            Label _Labeltotalamount = gvRow.FindControl("lbltotalamt") as Label;
            Label _Labelbilledamount = gvRow.FindControl("lblbilledamt") as Label;
            txttaxamount.Text = _Labeltaxamount.Text;
            txttotalamount.Text = _Labeltotalamount.Text;
            txtbilledamount.Text = _Labelbilledamount.Text;
            mpeqnb.Show();
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();

            SqlCommand cmd = new SqlCommand("update TM_INVOICE set TM_INVOICE_Status=@TM_INVOICE_Status,TM_INVOICE_Taxamount =@TM_INVOICE_Taxamount,TM_INVOICE_Totalamount = @TM_INVOICE_Totalamount,TM_INVOICE_GLCode = @TM_INVOICE_GLCode,TM_INVOICE_BilledAmount = @TM_INVOICE_BilledAmount where TM_INVOICE_Slno=@TM_INVOICE_Slno", con);
            cmd.Parameters.AddWithValue("@TM_INVOICE_Slno", lblslno.Text);


            cmd.Parameters.AddWithValue("@TM_INVOICE_Status", "Y");
            cmd.Parameters.AddWithValue("@TM_INVOICE_Taxamount", txttaxamount.Text);
            cmd.Parameters.AddWithValue("@TM_INVOICE_Totalamount", txttotalamount.Text);
            cmd.Parameters.AddWithValue("@TM_INVOICE_GLCode", txtglcode.Text);
            cmd.Parameters.AddWithValue("@TM_INVOICE_BilledAmount", txtbilledamount.Text);



            cmd.ExecuteNonQuery();

            BindGridView();
            SqlCommand cmd1 = new SqlCommand("update TD_INVOICE set TD_INVOICE_Status=@TD_INVOICE_Status where TD_INVOICE_TMSLNO=@TD_INVOICE_TMSLNO", con);
            cmd1.Parameters.AddWithValue("@TD_INVOICE_TMSLNO", txtinvoicenumber.Text);


            cmd1.Parameters.AddWithValue("@TD_INVOICE_Status", "Y");




            cmd1.ExecuteNonQuery();

            BindGridView();

          


                con.Close();
        }
    }
    }
