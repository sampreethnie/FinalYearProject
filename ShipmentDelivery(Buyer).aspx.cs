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
    public partial class ShipmentDelivery_Buyer_ : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(_ConnStr);
                con.Open();


                SqlCommand comcompany = new SqlCommand("select M_Company_Slno,M_Company_Name from M_Company", con);


                SqlDataAdapter dacompany = new SqlDataAdapter(comcompany);
                DataSet dscompany = new DataSet();
                dacompany.Fill(dscompany);
                dropdowncustomer.DataTextField = dscompany.Tables[0].Columns["M_Company_Name"].ToString();
                dropdowncustomer.DataValueField = dscompany.Tables[0].Columns["M_Company_Slno"].ToString();
                dropdowncustomer.DataSource = dscompany.Tables[0];
                dropdowncustomer.DataBind();
                con.Close();
                dropdowncustomer.Items.Insert(0, new ListItem("--Select customer--", "0"));
                SqlConnection con1 = new SqlConnection(_ConnStr);
                con1.Open();


                SqlCommand comshipmentnumber = new SqlCommand("select shipmentnumber from ShipmentDetailsSeller", con1);


                SqlDataAdapter dashipmentnumber = new SqlDataAdapter(comshipmentnumber);
                DataSet dsshipmentnumber = new DataSet();
                dashipmentnumber.Fill(dsshipmentnumber);

                dropdownshipmentnumber.DataValueField = dsshipmentnumber.Tables[0].Columns["shipmentnumber"].ToString();
                dropdownshipmentnumber.DataSource = dsshipmentnumber.Tables[0];
                dropdownshipmentnumber.DataBind();
                con1.Close();
                dropdownshipmentnumber.Items.Insert(0, new ListItem("--Select--", "0"));
                BindGridView();
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
        protected void shipmentnumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select shipmentnumber,creationdate,customer_M_Company_Name,numberofpackages,hawb,hawbdate,mawb,mawbdate,airline,flightnumber,etd,eta,atd,ata,delivery,receivedbyname,delivered,userid from ShipmentDetailsSeller where " + "shipmentnumber = @shipmentnumber";

            SqlConnection con = new SqlConnection(_ConnStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@shipmentnumber", dropdownshipmentnumber.SelectedItem.Value);
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = con;
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                txtshipmentnumber.Text = sdr[0].ToString();
                DateTime cdate = (DateTime)sdr[1];
                txtcreationdate.Text = cdate.ToString("dd-MM-yyyy hh:mm:ss");
                dropdowncustomer.SelectedItem.Text = sdr[2].ToString();
                txtnoofpackages.Text = sdr[3].ToString();
                txthawb.Text = sdr[4].ToString();
                DateTime hdate = (DateTime)sdr[5];
                txthawbdate.Text = hdate.ToString("dd-MM-yyyy hh:mm:ss");
                txtmawb.Text = sdr[6].ToString();
                DateTime mdate = (DateTime)sdr[7];
                txtmawbdate.Text = mdate.ToString("dd-MM-yyyy hh:mm:ss");
                txtairline.Text = sdr[8].ToString();
                txtflightnumber.Text = sdr[9].ToString();
                DateTime etddate = (DateTime)sdr[10];
                txtetd.Text = etddate.ToString("dd-MM-yyyy hh:mm:ss");
                DateTime etadate = (DateTime)sdr[11];
                txteta.Text = etadate.ToString("dd-MM-yyyy hh:mm:ss");
                DateTime atddate = (DateTime)sdr[12];
                txtatd.Text = atddate.ToString("dd-MM-yyyy hh:mm:ss");
                DateTime atadate = (DateTime)sdr[13];
                txtata.Text = atadate.ToString("dd-MM-yyyy hh:mm:ss");
                DateTime deliverydate = (DateTime)sdr[14];
                txtdeliverydate.Text = deliverydate.ToString("dd-MM-yyyy hh:mm:ss");
                txtsellerreceivedby.Text = sdr[15].ToString();

                bool result = false;
                if (sdr[16].ToString() == "Y")
                {
                    result = true;
                    delivery.Checked = true;
                }
                else
                {
                    result = false;
                    delivery.Checked = false;

                }

                //txtsellermail.Text = sdr[17].ToString();




            }
            con.Close();
            BindGridView();
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


                string query = "select shipmentnumber from ShipmentDetailsSeller where shipmentnumber = '" + txtshipmentnumber.Text + "' ";
                SqlCommand cmd2 = new SqlCommand(query, con);
                SqlDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblvalidateshipment.Text = "Shipment Number already exists.. Choose Other Number";
                    dr.Close();
                }
                else
                {
                    dr.Close();

                    string adddetails = @"INSERT INTO [ShipmentDeliveryBuyer] ([sellershipmentnumber],[sellercreationdate],[sellercustomer],[sellernumberofpackages],[sellerhawb],[sellerhawbdate],[sellermawb],[sellermawbdate],[sellerairline],[sellerflightnumber],[selleretd],[sellereta],[selleratd],[sellerata],[sellerdelivered],[sellerdeliverydate],[sellerreceivedbyname],[buyerreceived],[buyerreceivedbyname],[buyerdeliverydate],[BuyerUserID],[BuyerTimestamp],[ShipmentDeliveryBuyer_Submit])
            VALUES(@sellershipmentnumber,@sellercreationdate,@sellercustomer,@sellernumberofpackages,@sellerhawb,@sellerhawbdate,@sellermawb,@sellermawbdate,@sellerairline,@sellerflightnumber,@selleretd,@sellereta,@selleratd,@sellerata,@sellerdelivered,@sellerdeliverydate,@sellerreceivedbyname,@buyerreceived,@buyerreceivedbyname,@buyerdeliverydate,@BuyerUserID,@BuyerTimestamp,'N')";
                    string receivedchecking = received.Checked ? "Y" : "N";
                    string deliverychecking = delivery.Checked ? "Y" : "N";


                    con.ConnectionString = _ConnStr;

                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = adddetails;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@buyerid", txtbuyerid.Text);
                    cmd.Parameters.AddWithValue("@sellershipmentnumber", txtshipmentnumber.Text);
                    cmd.Parameters.AddWithValue("@sellercreationdate", Convert.ToDateTime(txtcreationdate.Text));
                    cmd.Parameters.AddWithValue("@sellercustomer", dropdowncustomer.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@sellernumberofpackages", txtnoofpackages.Text);
                    cmd.Parameters.AddWithValue("@sellerhawb", txthawb.Text);
                    cmd.Parameters.AddWithValue("@sellerhawbdate", Convert.ToDateTime(txthawbdate.Text));
                    cmd.Parameters.AddWithValue("@sellermawb", txtmawb.Text);
                    cmd.Parameters.AddWithValue("@sellermawbdate", Convert.ToDateTime(txtmawbdate.Text));
                    cmd.Parameters.AddWithValue("@sellerairline", txtairline.Text);
                    cmd.Parameters.AddWithValue("@sellerflightnumber", txtflightnumber.Text);
                    cmd.Parameters.AddWithValue("@selleretd", Convert.ToDateTime(txtetd.Text));
                    cmd.Parameters.AddWithValue("@sellereta", Convert.ToDateTime(txteta.Text));
                    cmd.Parameters.AddWithValue("@selleratd", Convert.ToDateTime(txtatd.Text));
                    cmd.Parameters.AddWithValue("@sellerata", Convert.ToDateTime(txtata.Text));
                    cmd.Parameters.AddWithValue("@sellerdelivered", deliverychecking);
                    cmd.Parameters.AddWithValue("@sellerdeliverydate", Convert.ToDateTime(txtdeliverydate.Text));
                    cmd.Parameters.AddWithValue("@sellerreceivedbyname", txtsellerreceivedby.Text);
                    cmd.Parameters.AddWithValue("@buyerreceived", receivedchecking);
                    cmd.Parameters.AddWithValue("@buyerreceivedbyname", txtbuyerreceivedbyname.Text);
                    cmd.Parameters.AddWithValue("@buyerdeliverydate", Convert.ToDateTime(txtbuyerdeliverydatetime.Text));
                    cmd.Parameters.AddWithValue("@BuyerUserID", Session["M_Subscriber_UserID"]);
                    cmd.Parameters.AddWithValue("@BuyerTimestamp", DateTime.Now);
                    
                    cmd.ExecuteNonQuery();
                    BindGridView();
                    clear();
                    con.Close();



                }
            }
        }

        protected void UpdatebuttonBuyer_Click(object sender, EventArgs e)
        {
            
            
            string updatebuyer = @"update [ShipmentDeliveryBuyer] set [sellerdeliverydate]=@sellerdeliverydate,[sellerreceivedbyname]=@sellerreceivedbyname,[buyerreceived]=@buyerreceived,[buyerreceivedbyname]=@buyerreceivedbyname,[buyerdeliverydate]=@buyerdeliverydate where [buyerid] = @buyerid";
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.ConnectionString = _ConnStr;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = updatebuyer;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@buyerid", txtbuyerid.Text);
                cmd.Parameters.AddWithValue("@sellerdeliverydate", Convert.ToDateTime(txtdeliverydate.Text));
                cmd.Parameters.AddWithValue("@sellerreceivedbyname", txtsellerreceivedby.Text);
                cmd.Parameters.AddWithValue("@buyerreceived", received.Text);
                cmd.Parameters.AddWithValue("@buyerreceivedbyname", txtbuyerreceivedbyname.Text);
                cmd.Parameters.AddWithValue("@buyerdeliverydate", Convert.ToDateTime(txtbuyerdeliverydatetime.Text));



                con.Open();


                cmd.ExecuteNonQuery();
                GridViewBuyer.EditIndex = -1;
                BindGridView();

                Updatebutton.Visible = false;
                clear();


            }
        }

        protected void Mailbutton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            string buyercompanyname = dropdowncustomer.SelectedItem.Text;
            SqlCommand cmd = new SqlCommand("select userid from ShipmentDetailsSeller inner join ShipmentDeliveryBuyer on ShipmentDetailsSeller.shipmentnumber = ShipmentDeliveryBuyer.sellershipmentnumber where BuyerUserID = @buyerid", con);
            cmd.Parameters.AddWithValue("@buyerid",Session["M_Subscriber_UserID"]);
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

                msg.Body = "Please find herewith the details of Shipment" + "<br/>" +
                    "Seller UserName:" + Session["M_Subscriber_UserID"].ToString() + "<br/>" +
                    "Seller :" + lblcompanyname.Text + "<br/>" +
                    "BuyerCompanyName:" + dropdowncustomer.SelectedItem.Text + "<br/>" +

                           "Creation Date:" + txtcreationdate.Text + "<br/>" +

                             "Number of Packages:" + txtnoofpackages.Text + "<br/>" +

                             "HAWB:" + txthawb.Text + "<br/>" +
                             "HAWB Date:" + txthawb.Text + "<br/>" +
                             "MAWB:" + txtmawb.Text + "<br/>" +
                             "MAWB Date:" + txtmawbdate.Text + "<br/>" +
                             "Airline:" + txtairline.Text + "<br/>" +
                             "FlightNumber:" + txtflightnumber.Text + "<br/>" +
                             "ETD:" + txtetd.Text + "<br/>" +
                             "ETA:" + txteta.Text + "<br/>" +
                             "ATD:" + txtata.Text + "<br/>" +
                             "ETA:" + txtata.Text + "<br/>" +
                "Delivered:" + delivery.Text + "<br/>" +
                "Deliverydate:" + txtdeliverydate.Text + " < br /> ";
                   
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("freightlogisticsnie@gmail.com", "niemysuru");
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }







            SqlConnection con1 = new SqlConnection(_ConnStr);
            con1.Open();
            SqlCommand cmd1 = new SqlCommand("update ShipmentDeliveryBuyer set ShipmentDeliveryBuyer_Submit = 'Y' where buyerid=@buyerid", con1);

            cmd1.Parameters.AddWithValue("@buyerid", txtbuyerid.Text);




            cmd1.ExecuteNonQuery();

            BindGridView();
            con1.Close();




            Updatebutton.Visible = false;

        }
        void clear()
        {
            txtshipmentnumber.Text = string.Empty;
            txtcreationdate.Text = string.Empty;
            dropdowncustomer.SelectedItem.Value = "0";
            txtnoofpackages.Text = string.Empty;
            txthawb.Text = string.Empty;
            txthawbdate.Text = string.Empty;
            txtmawb.Text = string.Empty;
            txtmawbdate.Text = string.Empty;
            txtairline.Text = string.Empty;
            txtflightnumber.Text = string.Empty;
            txtetd.Text = string.Empty;
            txteta.Text = string.Empty;
            txtatd.Text = string.Empty;
            txtata.Text = string.Empty;
            txtdeliverydate.Text = string.Empty;
            delivery.Text = string.Empty;
            txtsellerreceivedby.Text = string.Empty;
            txtbuyerreceivedbyname.Text = string.Empty;
            txtbuyerdeliverydatetime.Text = string.Empty;

        }
        private void BindGridView()
        {
            SqlConnection con1 = new SqlConnection(_ConnStr);


            con1.Open();

            SqlCommand cmd = new SqlCommand("select *from ShipmentDeliveryBuyer where BuyerUserID = '" + Session["M_Subscriber_UserID"].ToString() + "'", con1);
            SqlDataAdapter dabuyer= new SqlDataAdapter(cmd);
            DataSet dsbuyer = new DataSet();
            dabuyer.Fill(dsbuyer);
            GridViewBuyer.DataSource = dsbuyer;
            GridViewBuyer.DataBind();
            lbltotalcount.Text = GridViewBuyer.Rows.Count.ToString();

            con1.Close();

        }
        protected void GridViewBuyer_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                if (e.Row.Cells[17].Text == "Y")
                {
                    e.Row.Cells[17].Text = "Yes";
                }
                if (e.Row.Cells[17].Text == "N")
                {
                    e.Row.Cells[17].Text = "No";
                }
                if(e.Row.Cells[20].Text == "Y")
                {
                    e.Row.Cells[20].Text = "Yes";
                }
                if(e.Row.Cells[20].Text == "N")
                {
                    e.Row.Cells[20].Text = "No";
                }
                if (e.Row.Cells[25].Text == "Y")
                {
                    e.Row.Cells[25].Text = "Yes";
                    e.Row.Enabled = false;
                }
                if (e.Row.Cells[25].Text == "N")
                {
                    e.Row.Cells[25].Text = "No";
                    e.Row.Enabled = true;
                }
            }

        }
        protected void GridViewBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridViewBuyer.SelectedRow;
            txtbuyerid.Text = row.Cells[2].Text;
            txtshipmentnumber.Text = row.Cells[3].Text;
            txtcreationdate.Text = row.Cells[4].Text;
            dropdowncustomer.SelectedItem.Text = row.Cells[5].Text;
            txtnoofpackages.Text = row.Cells[6].Text;
            txthawb.Text = row.Cells[7].Text;
            txthawbdate.Text = row.Cells[8].Text;
            txtmawb.Text = row.Cells[9].Text;
            txtmawbdate.Text = row.Cells[10].Text;
            txtairline.Text = row.Cells[11].Text;
            txtflightnumber.Text = row.Cells[12].Text;
            txtetd.Text = row.Cells[13].Text;
            txteta.Text = row.Cells[14].Text;
            txtatd.Text = row.Cells[15].Text;
            txtata.Text = row.Cells[16].Text;
            bool result = false;
            if (row.Cells[17].Text == "Yes")
            {
                result = true;
                delivery.Checked = true;

            }
            else
            {
                result = false;
                delivery.Checked = false;
            }

            txtdeliverydate.Text = row.Cells[18].Text;
            txtsellerreceivedby.Text = row.Cells[19].Text;
            bool result1 = false; ;
            if(row.Cells[20].Text == "Yes")
            {
                result1 = true;
                received.Checked = true;
            }
            if(row.Cells[20].Text=="No")
            {
                result1 = false;
                received.Checked = false;
            }
            txtbuyerreceivedbyname.Text = row.Cells[21].Text;
            txtbuyerdeliverydatetime.Text = row.Cells[22].Text;
            

            BindGridView();


        }
        protected void GridViewBuyer_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            int buyerid = Convert.ToInt32(GridViewBuyer.DataKeys[e.RowIndex].Value);
            SqlCommand delcommand = new SqlCommand("delete from ShipmentDeliveryBuyer where buyerid= '" + buyerid + "'", con);
            delcommand.ExecuteNonQuery();
            GridViewBuyer.EditIndex = -1;
            BindGridView();
            con.Close();




        }
        
        protected void Cancelbutton_Click(object sender, EventArgs e)
        {
            clear();

            Addbutton.Visible = true;
            Updatebutton.Visible = true;

        }
    }
}