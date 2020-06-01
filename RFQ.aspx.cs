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

    public partial class RFQ : System.Web.UI.Page
    {

        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(_ConnStr);
                con.Open();


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
                con.Close();
                dropdownorigincountry.Items.Insert(0, new ListItem("--Select country--", "0"));
                dropdowndestinationcountry.Items.Insert(0, new ListItem("--Select country--", "0"));

                BindGridView();
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
                BindGridView();
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
                string str = "select M_Company_Slno,M_Company_Name,M_Company_BuyerSellerFlag from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
                SqlCommand com1 = new SqlCommand(str, con1);
                SqlDataAdapter da1 = new SqlDataAdapter(com1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                lblcompanyname.Text = "CompanyName:" + ds1.Tables[0].Rows[0]["M_Company_Name"].ToString();
                Session["CompanySlno"] = ds1.Tables[0].Rows[0]["M_Company_Slno"];
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
            txtexpectedprice.Text = string.Empty;


        }

        private void BindGridView()
        {
            SqlConnection con1 = new SqlConnection(_ConnStr);

            //where RFQ_UserID = '" + Session["M_Subscriber_UserID"].ToString() + "'
            con1.Open();

            SqlCommand cmd = new SqlCommand("select RFQ_Slno,RFQ_Number,RFQ_Company,RFQ_CreationDate,RFQ_OriginCountry,RFQ_DestinationCountry,RFQ_OriginAirport,RFQ_DestinationAirport,RFQ_NumberofPackages,RFQ_TotalGrwt,RFQ_TotalVolwt,RFQ_TotalChwt,RFQ_PickupAddress,RFQ_DeliveryAddress,RFQ_PickupDate,RFQ_ReqTT,RFQ_QuoteDueBy,RFQ_Commodity,RFQ_HandlingInfo,RFQ_UserID,RFQ_Timestamp,RFQ_Submit,RFQ_ExpectedPrice from RFQ where RFQ_UserID = '" + Session["M_Subscriber_UserID"].ToString() + "' ", con1);
            SqlDataAdapter darfq = new SqlDataAdapter(cmd);
            DataSet dsrfq = new DataSet();
            darfq.Fill(dsrfq);
            GridViewRfq.DataSource = dsrfq;
            GridViewRfq.DataBind();
            lbltotalcount.Text = GridViewRfq.Rows.Count.ToString();

            con1.Close();

        }

        protected void GridViewRfq_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[24].Text == "Y")
                {
                    e.Row.Cells[24].Text = "Yes";
                    e.Row.Enabled = false;



                }
                if (e.Row.Cells[24].Text == "N")
                {
                    e.Row.Cells[24].Text = "No";
                    e.Row.Enabled = true;







                }
            }
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


                string query = "select RFQ_Number from RFQ where RFQ_Number = '" + txtrfqnumber.Text + "' ";
                SqlCommand cmd2 = new SqlCommand(query, con);
                SqlDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblvalidaterfq.Text = "RFQ Number already exists.. Enter Other Number";
                    dr.Close();
                }

                else
                {


                    string adddetails = @"INSERT INTO [RFQ] ([RFQ_Number],[RFQ_Company],[RFQ_CreationDate],[RFQ_OriginCountry],[RFQ_DestinationCountry],[RFQ_OriginAirport],[RFQ_DestinationAirport],[RFQ_NumberofPackages],[RFQ_TotalGrwt],[RFQ_TotalVolwt],[RFQ_TotalChwt],[RFQ_PickupAddress],[RFQ_DeliveryAddress],[RFQ_PickupDate],[RFQ_ReqTT],[RFQ_QuoteDueBy],[RFQ_Commodity],[RFQ_HandlingInfo],[RFQ_UserID],[RFQ_Timestamp],[RFQ_Submit],[RFQ_ExpectedPrice])
            VALUES(@RFQ_Number,@RFQ_Company,@RFQ_CreationDate,@RFQ_OriginCountry,@RFQ_DestinationCountry,@RFQ_OriginAirport,@RFQ_DestinationAirport,@RFQ_NumberofPackages,@RFQ_TotalGrwt,@RFQ_TotalVolwt,@RFQ_TotalChwt,@RFQ_PickupAddress,@RFQ_DeliveryAddress,@RFQ_PickupDate,@RFQ_ReqTT,@RFQ_QuoteDueBy,@RFQ_Commodity,@RFQ_HandlingInfo,@RFQ_UserID,@RFQ_Timestamp,'N',@RFQ_ExpectedPrice)";

                   

                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandText = adddetails;
                        cmd.CommandType = System.Data.CommandType.Text;
                        cmd.Parameters.AddWithValue("@RFQ_Slno", lblrfqslno.Text);
                        cmd.Parameters.AddWithValue("@RFQ_Number", txtrfqnumber.Text);
                        cmd.Parameters.AddWithValue("@RFQ_Company", Session["CompanySlno"]);
                        cmd.Parameters.AddWithValue("@RFQ_CreationDate", Convert.ToDateTime(txtcreationdate.Text));
                        cmd.Parameters.AddWithValue("@RFQ_OriginCountry", dropdownorigincountry.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@RFQ_DestinationCountry", dropdowndestinationcountry.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@RFQ_OriginAirport", dropdownoriginairport.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@RFQ_DestinationAirport", dropdowndestinationairport.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@RFQ_NumberofPackages", txtnoofpackages.Text);
                        cmd.Parameters.AddWithValue("@RFQ_TotalGrwt", txtgrossweight.Text);
                        cmd.Parameters.AddWithValue("@RFQ_TotalVolwt", txtvolumetricweight.Text);
                        cmd.Parameters.AddWithValue("@RFQ_TotalChwt", txtchargeableweight.Text);
                        cmd.Parameters.AddWithValue("@RFQ_PickupAddress", txtpickupaddress.Text);
                        cmd.Parameters.AddWithValue("@RFQ_DeliveryAddress", txtdeliveryaddress.Text);
                        cmd.Parameters.AddWithValue("@RFQ_PickupDate", Convert.ToDateTime(txtpickupdate.Text));
                        cmd.Parameters.AddWithValue("@RFQ_ReqTT", txttransittime.Text);

                        cmd.Parameters.AddWithValue("@RFQ_QuoteDueBy", Convert.ToDateTime(txtquotedueby.Text));
                        cmd.Parameters.AddWithValue("@RFQ_Commodity", dropdowncommodity.SelectedItem.Text);
                        cmd.Parameters.AddWithValue("@RFQ_HandlingInfo", txthandlinginfo.Text);
                        cmd.Parameters.AddWithValue("@RFQ_UserID", Session["M_Subscriber_UserID"]);
                        cmd.Parameters.AddWithValue("@RFQ_Timestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@RFQ_ExpectedPrice", txtexpectedprice.Text);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        BindGridView();
                        clear();
                        con.Close();




                    }
                }
            }
        




        protected void GridViewRfq_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridViewRfq.SelectedRow;
            lblrfqslno.Text = row.Cells[2].Text;
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
            txtexpectedprice.Text = row.Cells[21].Text;

            Addbutton.Visible = false;
            //Updatebutton.Visible = true;
            BindGridView();


        }
        protected void GridViewRfq_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            int RFQ_Slno = Convert.ToInt32(GridViewRfq.DataKeys[e.RowIndex].Value);
            SqlCommand delcommand = new SqlCommand("delete from Rfq where RFQ_Slno= '" + RFQ_Slno + "'", con);
            delcommand.ExecuteNonQuery();
            GridViewRfq.EditIndex = -1;
            BindGridView();
            con.Close();




        }
        protected void Updatebuttonrfq_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.Open();


                string query = "select RFQ_Number from RFQ where RFQ_Number = '" + txtrfqnumber.Text + "' ";
                SqlCommand cmd2 = new SqlCommand(query, con);
                SqlDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblvalidaterfq.Text = "RFQ Number already exists.. Enter Other Number";
                    dr.Close();
                }

                string updaterfq = @"update [RFQ] set [RFQ_Number]=@RFQ_Number,[RFQ_Company]=@RFQ_Company,[RFQ_CreationDate]=@RFQ_CreationDate,[RFQ_OriginCountry]=@RFQ_OriginCountry,[RFQ_DestinationCountry]=@RFQ_DestinationCountry,[RFQ_OriginAirport]=@RFQ_OriginAirport,[RFQ_DestinationAirport]=@RFQ_DestinationAirport,[RFQ_NumberofPackages]=@RFQ_NumberofPackages,[RFQ_TotalGrwt]=@RFQ_TotalGrwt,[RFQ_TotalVolwt]=@RFQ_TotalVolwt,[RFQ_TotalChwt]=@RFQ_TotalChwt,[RFQ_PickupAddress]=@RFQ_PickupAddress,[RFQ_DeliveryAddress]=@RFQ_DeliveryAddress,[RFQ_PickupDate]=@RFQ_PickupDate,[RFQ_ReqTT]=@RFQ_ReqTT,[RFQ_QuoteDueBy]=@RFQ_QuoteDueBy,[RFQ_Commodity]=@RFQ_Commodity,[RFQ_HandlingInfo]=@RFQ_HandlingInfo,[RFQ_ExpectedPrice]=@RFQ_ExpectedPrice,[RFQ_Timestamp]=@RFQ_Timestamp where [RFQ_Slno] = @RFQ_Slno";
           
                
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = updaterfq;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@RFQ_Slno", lblrfqslno.Text);
                cmd.Parameters.AddWithValue("@RFQ_Number", txtrfqnumber.Text);
                cmd.Parameters.AddWithValue("@RFQ_Company", Session["CompanySlno"]);
                cmd.Parameters.AddWithValue("@RFQ_CreationDate", Convert.ToDateTime(txtcreationdate.Text));
                cmd.Parameters.AddWithValue("@RFQ_OriginCountry", dropdownorigincountry.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@RFQ_DestinationCountry", dropdowndestinationcountry.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@RFQ_OriginAirport", dropdownoriginairport.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@RFQ_DestinationAirport", dropdowndestinationairport.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@RFQ_NumberofPackages", txtnoofpackages.Text);
                cmd.Parameters.AddWithValue("@RFQ_TotalGrwt", txtgrossweight.Text);
                cmd.Parameters.AddWithValue("@RFQ_TotalVolwt", txtvolumetricweight.Text);
                cmd.Parameters.AddWithValue("@RFQ_TotalChwt", txtchargeableweight.Text);
                cmd.Parameters.AddWithValue("@RFQ_PickupAddress", txtpickupaddress.Text);
                cmd.Parameters.AddWithValue("@RFQ_DeliveryAddress", txtdeliveryaddress.Text);
                cmd.Parameters.AddWithValue("@RFQ_PickupDate", Convert.ToDateTime(txtpickupdate.Text));
                cmd.Parameters.AddWithValue("@RFQ_ReqTT", txttransittime.Text);

                cmd.Parameters.AddWithValue("@RFQ_QuoteDueBy", Convert.ToDateTime(txtquotedueby.Text));
                cmd.Parameters.AddWithValue("@RFQ_Commodity", dropdowncommodity.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@RFQ_HandlingInfo", txthandlinginfo.Text);
                cmd.Parameters.AddWithValue("@RFQ_ExpectedPrice", txtexpectedprice.Text);

                cmd.Parameters.AddWithValue("@RFQ_Timestamp", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();
                GridViewRfq.EditIndex = -1;

                BindGridView();


                //cmd.ExecuteNonQuery();
                //GridViewRfq.EditIndex = -1;
                //BindGridView();
                Updatebutton.Visible = false;


            }
        }
        
        protected void Cancelbutton_Click(object sender, EventArgs e)
        {
            clear();

            Addbutton.Visible = true;
            Updatebutton.Visible = true;



        }

        protected void Mailbutton_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            SqlCommand cmd = new SqlCommand("select M_Subscriber_UserID from M_Subscriber inner join M_Company on M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno where M_Company_BuyerSellerFlag = 's' ", con);
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
                    "Buyer UserName:" + Session["M_Subscriber_UserID"].ToString() + "<br/>" +
                    "Buyer:" + lblcompanyname.Text + "<br/>" +
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
                             "Handling Info:" + txthandlinginfo.Text + "<br/>" +
                             "ExpectedPrice:" + txtexpectedprice.Text + "<br/>";
                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Port = 587;
                smtp.Credentials = new System.Net.NetworkCredential("freightlogisticsnie@gmail.com", "niemysuru");
                smtp.EnableSsl = true;
                smtp.Send(msg);
            }



            con.Close();
            //GridViewRfq.SelectedRow.Cells[0].Enabled = false;
            //GridViewRfq.SelectedRow.Cells[1].Enabled = false;
            SqlConnection con1 = new SqlConnection(_ConnStr);
            con1.Open();
            SqlCommand cmd1 = new SqlCommand("update RFQ set RFQ_Submit = 'Y' where RFQ_Slno=@RFQ_Slno", con1);

            cmd1.Parameters.AddWithValue("@RFQ_Slno", lblrfqslno.Text);



            
                cmd1.ExecuteNonQuery();
            
            BindGridView();
            con1.Close();
                






                Updatebutton.Visible = false;


        } 
            
        

        protected void Search(object sender, ImageClickEventArgs e)
        {
            this.BindGrid();
        }
        private void BindGrid()
        {
            String constr = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "select RFQ_Slno,RFQ_Number,RFQ_Company,RFQ_CreationDate,RFQ_OriginCountry,RFQ_DestinationCountry,RFQ_OriginAirport,RFQ_DestinationAirport,RFQ_NumberofPackages,RFQ_TotalGrwt,RFQ_TotalVolwt,RFQ_TotalChwt,RFQ_PickupAddress,RFQ_DeliveryAddress,RFQ_PickupDate,RFQ_ReqTT,RFQ_QuoteDueBy,RFQ_Commodity,RFQ_HandlingInfo,RFQ_ExpectedPrice,RFQ_UserID,RFQ_Timestamp,RFQ_ExpectedPrice,RFQ_Submit from RFQ WHERE RFQ_Number LIKE '%'+@RFQ_Number+'%' AND RFQ_UserID = '" + Session["M_Subscriber_UserID"].ToString() + "' ";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@RFQ_Number", txtSearch.Text.Trim());


                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        GridViewRfq.DataSource = dt;
                        GridViewRfq.DataBind();

                    }
                }
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewRfq.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void Refresh(object sender, ImageClickEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from RFQ where RFQ_UserID = '" + Session["M_Subscriber_UserID"].ToString() + "' order by RFQ_Number ";
                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dTable = new DataTable();
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTable.Load(dReader);
                GridViewRfq.DataSource = dTable;
                GridViewRfq.DataBind();
            }

        }

        
    }
}