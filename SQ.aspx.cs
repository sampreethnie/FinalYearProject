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
    public partial class SQ : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(_ConnStr);
                con.Open();


                SqlCommand comrfqnumber = new SqlCommand("select RFQ_Number from RFQ where RFQ_Submit = 'Y' ", con);


                SqlDataAdapter darfqnumber = new SqlDataAdapter(comrfqnumber);
                DataSet dsrfqnumber = new DataSet();
                darfqnumber.Fill(dsrfqnumber);

                dropdownrfq.DataValueField = dsrfqnumber.Tables[0].Columns["RFQ_Number"].ToString();
                //dropdownrfq.DataTextField = dsrfqnumber.Tables[0].Columns["RFQ_Number"].ToString();
                dropdownrfq.DataSource = dsrfqnumber.Tables[0];
                dropdownrfq.DataBind();
                con.Close();
                dropdownrfq.Items.Insert(0, new ListItem("--Select RFQ No--", "0"));
                SqlConnection concountry = new SqlConnection(_ConnStr);
                concountry.Open();


                SqlCommand comcountry = new SqlCommand("select M_Country_Code,M_Country_Name from M_Country", con);


                SqlDataAdapter dacountry = new SqlDataAdapter(comcountry);
                DataSet dscountry = new DataSet();
                dacountry.Fill(dscountry);
                dropdownorigincountry.DataTextField = dscountry.Tables[0].Columns["M_Country_Name"].ToString();
                dropdownorigincountry.DataValueField = dscountry.Tables[0].Columns["M_Country_Code"].ToString();
                dropdownorigincountry.DataSource = dscountry.Tables[0];
                dropdownorigincountry.DataBind();
                dropdowndestinationcountry.DataTextField = dscountry.Tables[0].Columns["M_Country_Name"].ToString();
                dropdowndestinationcountry.DataValueField = dscountry.Tables[0].Columns["M_Country_Code"].ToString();
                dropdowndestinationcountry.DataSource = dscountry.Tables[0];
                dropdowndestinationcountry.DataBind();
                concountry.Close();
                dropdownorigincountry.Items.Insert(0, new ListItem("--Select country--", "0"));
                dropdowndestinationcountry.Items.Insert(0, new ListItem("--Select country--", "0"));

                //BindGridView();
                SqlConnection conairport = new SqlConnection(_ConnStr);
                conairport.Open();
                SqlCommand comairport = new SqlCommand("select M_Airport_Slno,M_Airport_Name from M_Airport", con);


                SqlDataAdapter daairport = new SqlDataAdapter(comairport);
                DataSet dsairport = new DataSet();
                daairport.Fill(dsairport);
                dropdownoriginairport.DataTextField = dsairport.Tables[0].Columns["M_Airport_Name"].ToString();
                dropdownoriginairport.DataValueField = dsairport.Tables[0].Columns["M_Airport_Slno"].ToString();
                dropdownoriginairport.DataSource = dsairport.Tables[0];
                dropdownoriginairport.DataBind();
                dropdowndestinationairport.DataTextField = dsairport.Tables[0].Columns["M_Airport_Name"].ToString();
                dropdowndestinationairport.DataValueField = dsairport.Tables[0].Columns["M_Airport_Slno"].ToString();
                dropdowndestinationairport.DataSource = dsairport.Tables[0];
                dropdowndestinationairport.DataBind();
                conairport.Close();
                dropdownoriginairport.Items.Insert(0, new ListItem("--Select Airport--", "0"));
                dropdowndestinationairport.Items.Insert(0, new ListItem("--Select Airport--", "0"));
                //BindGridView();
                SqlConnection concommodity = new SqlConnection(_ConnStr);
                concommodity.Open();


                SqlCommand comcommodity = new SqlCommand("select M_Commodity_Slno,M_Commodity_Name from M_Commodity", con);


                SqlDataAdapter dacommodity = new SqlDataAdapter(comcommodity);
                DataSet dscommodity = new DataSet();
                dacommodity.Fill(dscommodity);
                dropdowncommodity.DataTextField = dscommodity.Tables[0].Columns["M_Commodity_Name"].ToString();
                dropdowncommodity.DataValueField = dscommodity.Tables[0].Columns["M_Commodity_Slno"].ToString();
                dropdowncommodity.DataSource = dscommodity.Tables[0];
                dropdowncommodity.DataBind();

                concommodity.Close();
                dropdowncommodity.Items.Insert(0, new ListItem("--Select commodity--", "0"));
                BindGridView();

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

        protected void dropdownrfq_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select RFQ_Number,RFQ_CreationDate,RFQ_OriginCountry,RFQ_DestinationCountry,RFQ_OriginAirport,RFQ_DestinationAirport,RFQ_NumberofPackages,RFQ_TotalGrwt,RFQ_TotalVolwt,RFQ_TotalChwt,RFQ_PickupAddress,RFQ_DeliveryAddress,RFQ_PickupDate,RFQ_ReqTT,RFQ_QuoteDueBy,RFQ_Commodity,RFQ_HandlingInfo,M_Company_Name,M_Currency_Name,RFQ_ExpectedPrice from RFQ inner join M_Company on RFQ.RFQ_Company = M_Company.M_Company_Slno inner join M_Currency on M_Company.M_Company_Currency = M_Currency.M_Currency_Code where " + "RFQ_Number= @RFQ_Number";
            SqlConnection con = new SqlConnection(_ConnStr);
            SqlCommand cmd = new SqlCommand();
            
            cmd.Parameters.AddWithValue("@RFQ_Number",dropdownrfq.SelectedItem.Value);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = con;
            con.Open();

            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {

                txtrfqnumber.Text = sdr[0].ToString();
                DateTime cdate = (DateTime)sdr[1];
                txtcreationdate.Text = cdate.ToString("dd-MM-yyyy hh:mm:ss");

                dropdownorigincountry.SelectedItem.Text = sdr[2].ToString();
                dropdowndestinationcountry.SelectedItem.Text = sdr[3].ToString();
                dropdownoriginairport.SelectedItem.Text = sdr[4].ToString();
                dropdowndestinationairport.SelectedItem.Text = sdr[5].ToString();
                txtnoofpackages.Text = sdr[6].ToString();
                txtgrossweight.Text = sdr[7].ToString();
                txtvolumetricweight.Text = sdr[8].ToString();
                txtchargeableweight.Text = sdr[9].ToString();
                txtpickupaddress.Text = sdr[10].ToString();
                txtdeliveryaddress.Text = sdr[11].ToString();
                txtpickupdate.Text = sdr[12].ToString();
                txttransittime.Text = sdr[13].ToString();
                DateTime qdate = (DateTime)sdr[14];
                txtquotedueby.Text = qdate.ToString("dd-MM-yyyy hh:mm:ss");
                dropdowncommodity.SelectedItem.Text = sdr[15].ToString();
                txthandlinginfo.Text = sdr[16].ToString();
                txtbuyername.Text = sdr[17].ToString();
                txtbuyercurrency.Text = sdr[18].ToString();
                txtexpectedprice.Text = sdr[19].ToString();

               
            }
            con.Close();
            BindGridView();
        }
        void clear()
        {
            txtrfqnumber.Text = string.Empty;
            txtcreationdate.Text = string.Empty;
            dropdownorigincountry.SelectedItem.Text = string.Empty;
            dropdowndestinationcountry.SelectedItem.Text = string.Empty;
            dropdownoriginairport.SelectedItem.Text = string.Empty;
            dropdowndestinationairport.SelectedItem.Text = string.Empty;
            txtnoofpackages.Text = string.Empty;
            txtgrossweight.Text = string.Empty;
            txtvolumetricweight.Text = string.Empty;
            txtchargeableweight.Text = string.Empty;
            txtpickupaddress.Text = string.Empty;
            txtdeliveryaddress.Text = string.Empty;
            txtpickupdate.Text = string.Empty;
            txttransittime.Text = string.Empty;
            txtquotedueby.Text = string.Empty;
            dropdowncommodity.SelectedItem.Value = "0";
            txthandlinginfo.Text = string.Empty;
            txtbuyercurrency.Text = string.Empty;
            txtbuyername.Text = string.Empty;
            txtofferprice.Text = string.Empty;
            txtexpectedprice.Text = string.Empty;
            dropdownrfq.SelectedItem.Text = string.Empty;
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }


        protected void Addbutton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.Open();


                string query = "select SQ_RFQ_Number from SQ where SQ_RFQ_Number = '" + txtrfqnumber.Text + "' ";
                SqlCommand cmd2 = new SqlCommand(query, con);
                SqlDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblvalidatesq.Text = "Quote already exists.. Enter Other Quote";
                    dr.Close();
                }




                else
                {
                    dr.Close();
                    string addsq = @"insert into [SQ] ([SQ_RFQ_Number],[SQ_Company],[SQ_RFQ_CreationDate],[SQ_RFQ_OriginCountry],[SQ_RFQ_DestinationCountry],[SQ_RFQ_OriginAirport],[SQ_RFQ_DestinationAirport],[SQ_RFQ_NumberofPackages],[SQ_RFQ_TotalGrwt],[SQ_RFQ_TotalVolwt],[SQ_RFQ_TotalChwt],[SQ_RFQ_PickupAddress],[SQ_RFQ_DeliveryAddress],[SQ_RFQ_PickupDate],[SQ_RFQ_ReqTT],[SQ_RFQ_QuoteDueBy],[SQ_RFQ_Commodity],[SQ_RFQ_HandlingInfo],[SQ_UserID],[SQ_OfferPrice],[SQ_Timestamp],[SQ_RFQ_Company],[SQ_BuyerCurrency],[SQ_Submit],[SQ_RFQ_ExpectedPrice],[SQ_OrderStatus]) values(@SQ_RFQ_Number,@SQ_Company,@SQ_RFQ_CreationDate,@SQ_RFQ_OriginCountry,@SQ_RFQ_DestinationCountry,@SQ_RFQ_OriginAirport,@SQ_RFQ_DestinationAirport,@SQ_RFQ_NumberofPackages,@SQ_RFQ_TotalGrwt,@SQ_RFQ_TotalVolwt,@SQ_RFQ_TotalChwt,@SQ_RFQ_PickupAddress,@SQ_RFQ_DeliveryAddress,@SQ_RFQ_PickupDate,@SQ_RFQ_ReqTT,@SQ_RFQ_QuoteDueBy,@SQ_RFQ_Commodity,@SQ_RFQ_HandlingInfo,@SQ_UserID,@SQ_OfferPrice,@SQ_Timestamp,@SQ_RFQ_Company,@SQ_BuyerCurrency,'N',@SQ_RFQ_ExpectedPrice,'N')";


                    //con.ConnectionString = _ConnStr;
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = addsq;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.Parameters.AddWithValue("@SQ_Slno", txtsqlslno.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_Number", txtrfqnumber.Text);

                    cmd.Parameters.AddWithValue("@SQ_Company", Session["CompanySlnosq"]);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_CreationDate", Convert.ToDateTime(txtcreationdate.Text));
                    cmd.Parameters.AddWithValue("@SQ_RFQ_OriginCountry", dropdownorigincountry.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_DestinationCountry", dropdowndestinationcountry.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_OriginAirport", dropdownoriginairport.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_DestinationAirport", dropdowndestinationairport.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_NumberofPackages", txtnoofpackages.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_TotalGrwt", txtgrossweight.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_TotalVolwt", txtvolumetricweight.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_TotalChwt", txtchargeableweight.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_PickupAddress", txtpickupaddress.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_DeliveryAddress", txtdeliveryaddress.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_PickupDate", Convert.ToDateTime(txtpickupdate.Text));
                    cmd.Parameters.AddWithValue("@SQ_RFQ_ReqTT", txttransittime.Text);

                    cmd.Parameters.AddWithValue("@SQ_RFQ_QuoteDueBy", Convert.ToDateTime(txtquotedueby.Text));
                    cmd.Parameters.AddWithValue("@SQ_RFQ_Commodity", dropdowncommodity.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_HandlingInfo", txthandlinginfo.Text);
                    cmd.Parameters.AddWithValue("@SQ_UserID", Session["M_Subscriber_UserID"]);
                    cmd.Parameters.AddWithValue("@SQ_OfferPrice", txtofferprice.Text);

                    cmd.Parameters.AddWithValue("@SQ_Timestamp", DateTime.Now);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_Company", txtbuyername.Text);
                    cmd.Parameters.AddWithValue("@SQ_BuyerCurrency", txtbuyercurrency.Text);
                    cmd.Parameters.AddWithValue("@SQ_RFQ_ExpectedPrice", txtexpectedprice.Text);
                    
                    cmd.ExecuteNonQuery();
                    BindGridView();
                    clear();
                    
                    con.Close();
                }

            }
        }
        private void BindGridView()
        {
            SqlConnection con1 = new SqlConnection(_ConnStr);


            con1.Open();

            SqlCommand cmd = new SqlCommand("select SQ_Slno,SQ_RFQ_Number,SQ_Company,SQ_RFQ_CreationDate,SQ_RFQ_OriginCountry,SQ_RFQ_DestinationCountry,SQ_RFQ_OriginAirport,SQ_RFQ_DestinationAirport,SQ_RFQ_NumberofPackages,SQ_RFQ_TotalGrwt,SQ_RFQ_TotalVolwt,SQ_RFQ_TotalChwt,SQ_RFQ_PickupAddress,SQ_RFQ_DeliveryAddress,SQ_RFQ_PickupDate,SQ_RFQ_ReqTT,SQ_RFQ_QuoteDueBy,SQ_RFQ_Commodity,SQ_RFQ_HandlingInfo,SQ_UserID,SQ_OfferPrice,SQ_Timestamp,SQ_RFQ_Company,SQ_BuyerCurrency,SQ_Submit,SQ_RFQ_ExpectedPrice,SQ_OrderStatus,M_Company_Name,M_Currency_Name from SQ inner join M_Company on SQ.SQ_Company = M_Company.M_Company_Slno inner join M_Currency on M_Company.M_Company_Currency = M_Currency.M_Currency_Code where SQ_UserID = '" + Session["M_Subscriber_UserID"].ToString() + "' ", con1);
            SqlDataAdapter dasq = new SqlDataAdapter(cmd);
            DataSet dssq = new DataSet();
            dasq.Fill(dssq);
            GridViewSq.DataSource = dssq;
            GridViewSq.DataBind();
            lbltotalcount.Text = GridViewSq.Rows.Count.ToString();

            con1.Close();

        }

        protected void GridViewSq_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridViewSq.SelectedRow;
            txtsqlslno.Text = row.Cells[2].Text;
            txtrfqnumber.Text = row.Cells[3].Text;
            txtcreationdate.Text = row.Cells[5].Text;
            dropdownorigincountry.SelectedItem.Text = row.Cells[6].Text;
            dropdowndestinationcountry.SelectedItem.Text = row.Cells[7].Text;
            dropdownoriginairport.SelectedItem.Text = row.Cells[8].Text;
            dropdowndestinationairport.SelectedItem.Text = row.Cells[9].Text;
            txtnoofpackages.Text = row.Cells[10].Text;
            txtgrossweight.Text = row.Cells[11].Text;
            txtvolumetricweight.Text = row.Cells[12].Text;
            txtchargeableweight.Text = row.Cells[13].Text;
            txtpickupaddress.Text = row.Cells[14].Text;
            txtdeliveryaddress.Text = row.Cells[15].Text;
            txtpickupdate.Text = row.Cells[16].Text;
            txttransittime.Text = row.Cells[17].Text;
            txtquotedueby.Text = row.Cells[18].Text;
            dropdowncommodity.SelectedItem.Text = row.Cells[19].Text;
            txthandlinginfo.Text = row.Cells[20].Text;
            txtbuyername.Text = row.Cells[21].Text;
            txtbuyercurrency.Text = row.Cells[22].Text;
            
            txtofferprice.Text = row.Cells[24].Text;
            txtexpectedprice.Text = row.Cells[25].Text;
            


            


            Addbutton.Visible = false;
            //Updatebutton.Visible = true;
            BindGridView();
        }
        protected void GridViewSq_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[27].Text == "Y")
                {
                    e.Row.Cells[27].Text = "Yes";
                    e.Row.Enabled = false;



                }
                if (e.Row.Cells[27].Text == "N")
                {
                    e.Row.Cells[27].Text = "No";
                    e.Row.Enabled = true;
                    







                }
                if (e.Row.Cells[28].Text == "Y")
                {
                    e.Row.Cells[28].Text = "Order Placed";
                    e.Row.Enabled = false;



                }
                if (e.Row.Cells[28].Text == "N")
                {
                    e.Row.Cells[28].Text = "Order Pending";
                    e.Row.Enabled = true;








                }
            }
        }
        protected void Cancelbutton_Click(object sender, EventArgs e)
        {
            clear();

            Addbutton.Visible = true;
            Updatebutton.Visible = true;
            Mailbutton.Visible = true;
            


        }
        protected void Updatebutton_Clicksq(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
           
            SqlCommand cmd = new SqlCommand("update SQ set SQ_OfferPrice=@SQ_OfferPrice where SQ_Slno=@SQ_Slno",con);
            cmd.Parameters.AddWithValue("@SQ_Slno",txtsqlslno.Text);

             cmd.Parameters.AddWithValue("@SQ_OfferPrice",txtofferprice.Text);

              

           
            cmd.ExecuteNonQuery();
                GridViewSq.EditIndex = -1;
            
                BindGridView();
            Updatebutton.Visible = false;

            con.Close();

           


        }

        protected void GridViewSq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            int SQ_Slno = Convert.ToInt32(GridViewSq.DataKeys[e.RowIndex].Value);
            SqlCommand delcommand = new SqlCommand("delete from SQ where SQ_Slno = '" + SQ_Slno + "'", con);
            delcommand.ExecuteNonQuery();
            GridViewSq.EditIndex = -1;
            
            BindGridView();
            con.Close();




        }
        protected void Mailbutton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            string buyercompanyname = txtbuyername.Text;
            SqlCommand cmd = new SqlCommand("select M_Subscriber_UserID from M_Subscriber inner join M_Company on M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno where M_Company_Name = '"+ buyercompanyname + "'" , con);
            string email;


            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                email = dr[0].ToString();
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress(Session["M_Subscriber_UserID"].ToString());
                msg.To.Add(email);
                msg.Subject = "Buyer RFQ details";
                msg.IsBodyHtml = true;

                msg.Body = "Please find herewith the details of RFQ" + "<br/>" +
                    "Seller UserName:" + Session["M_Subscriber_UserID"].ToString() + "<br/>" +
                    "Seller :" + lblcompanyname.Text + "<br/>" +
                    "BuyerCompanyName:"+txtbuyername.Text + "<br/>"+
                    "BuyerCurrency:"+txtbuyercurrency.Text+"<br/>"+
                    "RFQ Number:" + txtrfqnumber.Text + "<br/>" +
                           "Creation Date:" + txtcreationdate.Text + "<br/>" +
                            "Origin Country:" + dropdownorigincountry.SelectedItem.Text + "<br/>" +
                            "Destination Country:" + dropdowndestinationcountry.SelectedItem.Text + "<br/>" +
                             "Origin Airport:" + dropdownoriginairport.SelectedItem.Text + "<br/>" +
                             "Destination Airport:" + dropdowndestinationairport.SelectedItem.Text + "<br/>" +
                             "Number of Packages:" + txtnoofpackages.Text + "<br/>" +
                             "Total Gross Weight:" + txtgrossweight.Text + "<br/>" +
                             "Total Volwt:" + txtvolumetricweight.Text + "<br/>" +
                             "Total Chwt:" + txtchargeableweight.Text + "<br/>" +
                             "Pickup Address:" + txtpickupaddress.Text + "<br/>" +
                             "Delivery Address:" + txtdeliveryaddress.Text + "<br/>" +
                             "PickupDate:" + txtpickupdate.Text + "<br/>" +
                             "RequiredT/T:" + txttransittime.Text + "<br/>" +
                             "Quote Due by:" + txtquotedueby.Text + "<br/>" +
                             "Commodity:" + dropdowncommodity.SelectedItem.Text + "<br/>" +
                             "Handling Info:" + txthandlinginfo.Text + "<br/>"+
                             "Expected Price:"+txtexpectedprice.Text +  "<br/>"+
                "Offer Price:" + txtofferprice.Text + "<br/>";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("freightlogisticsnie@gmail.com", "niemysuru");
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }
            dr.Close();

            SqlConnection con1 = new SqlConnection(_ConnStr);
            con1.Open();
            SqlCommand cmd1 = new SqlCommand("update SQ set SQ_Submit = 'Y' where SQ_Slno=@SQ_Slno", con1);

            cmd1.Parameters.AddWithValue("@SQ_Slno", txtsqlslno.Text);




            cmd1.ExecuteNonQuery();

            BindGridView();
            con1.Close();





            Updatebutton.Visible = false;
            Mailbutton.Visible = false;


        }
    }
}