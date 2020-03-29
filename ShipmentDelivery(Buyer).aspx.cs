using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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

            }
        }
        protected void shipmentnumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            string query = "select shipmentnumber,creationdate,customer_M_Company_Name,numberofpackages,hawb,hawbdate,mawb,mawbdate,airline,flightnumber,etd,eta,atd,ata,delivery,receivedbyname,delivered from ShipmentDetailsSeller where " + "shipmentnumber = @shipmentnumber";

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






            }
            con.Close();
            BindGridView();
        }
        protected void Addbutton_Click(object sender, EventArgs e)
        {
            string adddetails = @"INSERT INTO [ShipmentDeliveryBuyer] ([sellershipmentnumber],[sellercreationdate],[sellercustomer],[sellernumberofpackages],[sellerhawb],[sellerhawbdate],[sellermawb],[sellermawbdate],[sellerairline],[sellerflightnumber],[selleretd],[sellereta],[selleratd],[sellerata],[sellerdelivered],[sellerdeliverydate],[sellerreceivedbyname],[buyerreceived],[buyerreceivedbyname],[buyerdeliverydate])
            VALUES(@sellershipmentnumber,@sellercreationdate,@sellercustomer,@sellernumberofpackages,@sellerhawb,@sellerhawbdate,@sellermawb,@sellermawbdate,@sellerairline,@sellerflightnumber,@selleretd,@sellereta,@selleratd,@sellerata,@sellerdelivered,@sellerdeliverydate,@sellerreceivedbyname,@buyerreceived,@buyerreceivedbyname,@buyerdeliverydate)";
            string receivedchecking = received.Checked ? "Y" : "N";
            string deliverychecking = delivery.Checked ? "Y" : "N";
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.ConnectionString = _ConnStr;

                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = adddetails;
                cmd.CommandType = System.Data.CommandType.Text;
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
                con.Open();
                cmd.ExecuteNonQuery();
                BindGridView();
                clear();
                con.Close();


              
            }
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

            SqlCommand cmd = new SqlCommand("select *from ShipmentDeliveryBuyer", con1);
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
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[2].Text = "BuyerID";
                e.Row.Cells[3].Text = "ShipmentNumber";
                e.Row.Cells[4].Text = "Creation Date";
                e.Row.Cells[5].Text = "Customer Name";
                e.Row.Cells[6].Text = "Number of Packages";
                e.Row.Cells[7].Text = "HAWB";
                e.Row.Cells[8].Text = "HAWB Date";
                e.Row.Cells[9].Text = "MAWB";
                e.Row.Cells[10].Text = "MAWB Date";
                e.Row.Cells[11].Text = "Airline";
                e.Row.Cells[12].Text = "Flight Number";
                e.Row.Cells[13].Text = "ETD";
                e.Row.Cells[14].Text = "ETA";
                e.Row.Cells[15].Text = "ATD";
                e.Row.Cells[16].Text = "ATA";
                e.Row.Cells[17].Text = "Delivered";
                e.Row.Cells[18].Text = "DeliveryDate";
                e.Row.Cells[19].Text = "Received By";
                e.Row.Cells[20].Text = "BuyerReceived";
                e.Row.Cells[21].Text = "BuyerReceivedByName";
                e.Row.Cells[22].Text = "BuyerDeliveryDate";

            }
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

            }

        }
        protected void GridViewBuyer_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridViewBuyer.SelectedRow;
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
            if (row.Cells[17].Text == "Y")
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
            if(row.Cells[20].Text == "Y")
            {
                result1 = true;
                received.Checked = true;
            }
            if(row.Cells[20].Text=="N")
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
    }
}