using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
namespace FinalYearProject
{
    public partial class InvoiceDisplay : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //string dateTime = Request.QueryString["creationdate"];

            //DateTime dt = Convert.ToDateTime(dateTime);

            //string finaltime = dt.ToString();
           



            Label1.Text = Request.QueryString["ShipmentRFQ_Number"];
            Label2.Text = Request.QueryString["shipmentnumber"];
            Label3.Text = Request.QueryString["customer_M_Company_Name"];
            Label5.Text = Request.QueryString["RFQ_OriginCountry"];
            Label6.Text = Request.QueryString["RFQ_OriginAirport"];
            Label7.Text = Request.QueryString["RFQ_DestinationCountry"];
            Label8.Text = Request.QueryString["RFQ_DestinationAirport"];
            Label9.Text = Request.QueryString["RFQ_TotalGrwt"];
            Label10.Text = Request.QueryString["RFQ_TotalChwt"];
            Label11.Text = Request.QueryString["RFQ_NumberofPackages"];
            Label12.Text = Request.QueryString["sellerdelivered"];
            Label13.Text = Request.QueryString["buyerreceived"];
            Label14.Text= Request.QueryString["M_Currency_Name"];
            txtcustomer.Text = Request.QueryString["customer_M_Company_Name"];
            txtdisplaycurrency.Text = Request.QueryString["M_Currency_Name"];
            txteditcustomer.Text= Request.QueryString["customer_M_Company_Name"];
            txteditcurrency.Text= Request.QueryString["M_Currency_Name"];
            //Label3.Text = finaltime;
            txtrfqnumber.Text = Label1.Text;
            txteditrfqnumber.Text = Label1.Text;


            txttaxname.Visible = false;
            lbltaxname.Visible = false;
            txtpercentage.Visible = false;
            lblpercentage.Visible = false;

            txttaxamount.Visible = false;
            lbltaxamount.Visible = false;
            txttotalamount.Visible = false;
            lbltotalamount.Visible = false;

            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(_ConnStr);
                con.Open();


                //SqlCommand comcompany = new SqlCommand("select M_Company_Slno,M_Company_Name from M_Company", con);


                //SqlDataAdapter dacompany = new SqlDataAdapter(comcompany);
                //DataSet dscompany = new DataSet();
                //dacompany.Fill(dscompany);
                //Customer.DataTextField = dscompany.Tables[0].Columns["M_Company_Name"].ToString();
                //Customer.DataValueField = dscompany.Tables[0].Columns["M_Company_Slno"].ToString();
                //Customer.DataSource = dscompany.Tables[0];
                //Customer.DataBind();
                //Customereditdropdown.DataTextField = dscompany.Tables[0].Columns["M_Company_Name"].ToString();
                //Customereditdropdown.DataValueField = dscompany.Tables[0].Columns["M_Company_Slno"].ToString();
                //Customereditdropdown.DataSource = dscompany.Tables[0];
                //Customereditdropdown.DataBind();

                //con.Close();
                //BindGridView();
                //Customer.Items.Insert(0, new ListItem("--Select Customer--", "0"));
                //Customereditdropdown.Items.Insert(0, new ListItem("--Select Customer--", "0"));
                SqlConnection conchargeinvoice = new SqlConnection(_ConnStr);

                conchargeinvoice.Open();
                SqlCommand comchargeinvoice = new SqlCommand("select TM_INVOICE_No from TM_INVOICE where TM_INVOICE_Status = 'N' or TM_INVOICE_Status='P' ", conchargeinvoice);
                SqlDataAdapter dachargeinvoice = new SqlDataAdapter(comchargeinvoice);
                DataSet dschargeinvoice = new DataSet();
                dachargeinvoice.Fill(dschargeinvoice);
                chargeinvoicedropdown.DataValueField = dschargeinvoice.Tables[0].Columns["TM_INVOICE_No"].ToString();
                chargeinvoicedropdown.DataSource = dschargeinvoice.Tables[0];
                chargeinvoicedropdown.DataBind();
                dropdowneditinvoice.DataValueField = dschargeinvoice.Tables[0].Columns["TM_INVOICE_No"].ToString();
                dropdowneditinvoice.DataSource = dschargeinvoice.Tables[0];
                dropdowneditinvoice.DataBind();


                conchargeinvoice.Close();
                chargeinvoicedropdown.Items.Insert(0, new ListItem("--Select Invoice--", "0"));
                SqlConnection concurrencycharge = new SqlConnection(_ConnStr);
                concurrencycharge.Open();
                SqlCommand comcurrencycharge = new SqlCommand("select M_Currency_Code,M_Currency_Name from M_Currency", concurrencycharge);
                SqlDataAdapter dacurrencycharge = new SqlDataAdapter(comcurrencycharge);
                DataSet dscurrencycharge = new DataSet();
                dacurrencycharge.Fill(dscurrencycharge);
                dropdowncurrency.DataValueField = dscurrencycharge.Tables[0].Columns["M_Currency_Code"].ToString();
                dropdowncurrency.DataTextField = dscurrencycharge.Tables[0].Columns["M_Currency_Name"].ToString();
                dropdowncurrency.DataSource = dscurrencycharge.Tables[0];
                dropdowncurrency.DataBind();

                dropdowneditcurrency.DataValueField = dscurrencycharge.Tables[0].Columns["M_Currency_Code"].ToString();
                dropdowneditcurrency.DataTextField = dscurrencycharge.Tables[0].Columns["M_Currency_Name"].ToString();
                dropdowneditcurrency.DataSource = dscurrencycharge.Tables[0];
                dropdowneditcurrency.DataBind();
                concurrencycharge.Close();
                dropdowncurrency.Items.Insert(0, new ListItem("--Select Currency--"));
                chargebasisdropdown.Items.Insert(0, new ListItem("--Select Charge Basis--"));
                SqlConnection concharge = new SqlConnection(_ConnStr);
                concharge.Open();
                SqlCommand comcharge = new SqlCommand("select M_Charge_Code,M_Charge_Name from M_Charge", concurrencycharge);
                SqlDataAdapter dacharge = new SqlDataAdapter(comcharge);
                DataSet dscharge = new DataSet();
                dacharge.Fill(dscharge);
                dropdowncharge.DataValueField = dscharge.Tables[0].Columns["M_Charge_Code"].ToString();
                dropdowncharge.DataTextField = dscharge.Tables[0].Columns["M_Charge_Name"].ToString();
                dropdowncharge.DataSource = dscharge.Tables[0];
                dropdowncharge.DataBind();
                dropdownchargeeditinvoice.DataValueField = dscharge.Tables[0].Columns["M_Charge_Code"].ToString();
                dropdownchargeeditinvoice.DataTextField = dscharge.Tables[0].Columns["M_Charge_Name"].ToString();
                dropdownchargeeditinvoice.DataSource = dscharge.Tables[0];
                dropdownchargeeditinvoice.DataBind();

                concharge.Close();
                dropdowncharge.Items.Insert(0, new ListItem("--Select Charge--"));
                


                lblusername.Text = "Username:" + Session["M_Subscriber_UserID"];
                SqlConnection con1 = new SqlConnection(_ConnStr);
                con1.Open();
                string str = "select M_Company_Slno,M_Company_Name,M_Company_BuyerSellerFlag from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
                SqlCommand com1 = new SqlCommand(str, con1);
                SqlDataAdapter da1 = new SqlDataAdapter(com1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                lblcompanyname.Text = "CompanyName:" + ds1.Tables[0].Rows[0]["M_Company_Name"].ToString();
                //lblbuyersellerflag.Text = "Type:" + ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString();
                lblcompanyslno.Text = ds1.Tables[0].Rows[0]["M_Company_Slno"].ToString();
                if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "b")
                {
                    ButtonSeller.Visible = false;
                    

                }
                else
                {
                    ButtonBuyer.Visible = false;
                    
                }

                BindGridView();
                BindChargeGridView();
               
            }
        }
        private void BindGridView()
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select TM_INVOICE_Slno,TM_INVOICE_No,TM_INVOICE_ShipmentRefNo,TM_INVOICE_Customer,TM_INVOICE_Currency,TM_INVOICE_Date,TM_INVOICE_Status,TM_INVOICE_RFQNumber from TM_INVOICE; select SUM(TD_INVOICE_TAXAMOUNT) from TD_INVOICE inner join  TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No group by TM_INVOICE.TM_INVOICE_No;", con);
            SqlDataAdapter dainvoice = new SqlDataAdapter(cmd);
            DataSet dsinvoice = new DataSet();
            dainvoice.Fill(dsinvoice);
            InvoiceGridView.DataSource = dsinvoice;
            InvoiceGridView.DataBind();
            con.Close();
        }
        private void BindChargeGridView()
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select TD_INVOICE_SLNO,TD_INVOICE_TMSLNO,M_Charge_Name,TD_INVOICE_DESCRIPTION,TD_INVOICE_BASIS,TD_INVOICE_QTY,TD_INVOICE_Rate,M_Currency_Name,TD_INVOICE_AMOUNTFC,TD_INVOICE_EXCHRATE,TD_INVOICE_AMOUNTBC,TD_INVOICE_TAXABLE,TD_INVOICE_TAXPERCENTAGE,TD_INVOICE_TAXNAME,TD_INVOICE_TAXAMOUNT,TD_INVOICE_TOTALAMOUNT,TD_INVOICE_Status from TD_INVOICE inner join M_Currency on TD_INVOICE.TD_INVOICE_CURRENCY = M_Currency.M_Currency_Code inner join  M_Charge on TD_INVOICE.TD_INVOICE_CHARGESLNO = M_Charge.M_Charge_Code", con);
            SqlDataAdapter dacharge = new SqlDataAdapter(cmd);
            DataSet dscharge = new DataSet();
            dacharge.Fill(dscharge);
            gvChargeInvoice.DataSource = dscharge;
            gvChargeInvoice.DataBind();
            con.Close();

            //          TD_INVOICE_TMSLNO
            //          [TD_INVOICE_DESCRIPTION]
            //,[TD_INVOICE_BASIS]
            //,[TD_INVOICE_QTY]
            //,[TD_INVOICE_Rate]
            //,[TD_INVOICE_CURRENCY]
            //,[TD_INVOICE_AMOUNTFC]
            //,[TD_INVOICE_EXCHRATE]
            //,[TD_INVOICE_AMOUNTBC]
            //,[TD_INVOICE_TAXABLE]
            //,[TD_INVOICE_TAXPERCENTAGE]
            //,[TD_INVOICE_TAXNAME]
            //,[TD_INVOICE_TAXAMOUNT]
            //,[TD_INVOICE_TOTALAMOUNT]
        }

        protected void Addbutton_Click(object sender, EventArgs e)
        {
            mpeInvoiceDisplay.Show();
        }


        protected void AddbuttonCharge_Click(object sender, EventArgs e)
        {
            mpechargedisplay.Show();
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        protected void AddInvoiceButton_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.Open();


                string query = "select TM_INVOICE_No from TM_INVOICE where TM_INVOICE_No = '" + txtinvoicenumber.Text + "' ";
                SqlCommand cmd2 = new SqlCommand(query, con);
                SqlDataReader dr = cmd2.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    lblvalidatecurr.Text = "Invoice Number already exists.. Enter Other Number";
                    dr.Close();
                }
                
                else
                {
                    dr.Close();
                    string addinvoice = @"INSERT INTO [TM_INVOICE] ([TM_INVOICE_No],[TM_INVOICE_ShipmentRefNo],[TM_INVOICE_Customer],[TM_INVOICE_Date],[TM_INVOICE_Currency],[TM_INVOICE_RFQNumber],[TM_INVOICE_COMPANYSLNO],[TM_INVOICE_USERID],[TM_INVOICE_Status]) VALUES(@TM_INVOICE_No,@TM_INVOICE_ShipmentRefNo,@TM_INVOICE_Customer,@TM_INVOICE_Date,@TM_INVOICE_Currency,@TM_INVOICE_RFQNumber,@TM_INVOICE_COMPANYSLNO,@TM_INVOICE_USERID,'P')";


                    //con.ConnectionString = _ConnStr;
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandText = addinvoice;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@TM_INVOICE_No", txtinvoicenumber.Text);
                    cmd.Parameters.AddWithValue("@TM_INVOICE_ShipmentRefNo", txtshipmentrefno.Text);
                    cmd.Parameters.AddWithValue("@TM_INVOICE_Customer", txtcustomer.Text);
                    cmd.Parameters.AddWithValue("@TM_INVOICE_Date", Convert.ToDateTime(txtinvoicedate.Text));
                    cmd.Parameters.AddWithValue("@TM_INVOICE_Currency", txtdisplaycurrency.Text);
                    cmd.Parameters.AddWithValue("@TM_INVOICE_RFQNumber", txtrfqnumber.Text);
                    cmd.Parameters.AddWithValue("@TM_INVOICE_COMPANYSLNO", lblcompanyslno.Text);
                    cmd.Parameters.AddWithValue("@TM_INVOICE_USERID", Session["M_Subscriber_UserID"]);
                    cmd.ExecuteNonQuery();

                    BindGridView();



                    con.Close();




                    clear();

                }
            }

        }

        protected void ImageButtoninvoice_Click(object sender, EventArgs e)
        {
            ImageButton imgbtnInvoice = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)imgbtnInvoice.NamingContainer;

            lblslno.Text = InvoiceGridView.DataKeys[gvRow.RowIndex].Value.ToString();
            txteditinvoice.Text = gvRow.Cells[1].Text;
            txteditinvoicedate.Text = gvRow.Cells[2].Text;
            txteditshipmentrefno.Text = gvRow.Cells[3].Text;
            txteditcustomer.Text = gvRow.Cells[4].Text;
            txteditcurrency.Text = gvRow.Cells[5].Text;
            
            
            
            mpeInvoiceEdit.Show();
        }
        protected void ImagebuttonCharge_Click(object sender, EventArgs e)
        {
            ImageButton imgbtnCharge = sender as ImageButton;
            GridViewRow gvrow = (GridViewRow)imgbtnCharge.NamingContainer;
            lblchargeslno.Text = gvChargeInvoice.DataKeys[gvrow.RowIndex].Value.ToString();
        

            dropdowneditinvoice.SelectedItem.Text = gvrow.Cells[1].Text;

            dropdownchargeeditinvoice.SelectedItem.Text = gvrow.Cells[2].Text;
            txteditdescription.Text = gvrow.Cells[3].Text;
            dropdowneditcharge.SelectedItem.Text = gvrow.Cells[4].Text;

            txteditquantity.Text = gvrow.Cells[5].Text;
            txteditrate.Text = gvrow.Cells[6].Text;
            dropdowneditcurrency.SelectedItem.Text = gvrow.Cells[7].Text;

            txteditamountfc.Text = gvrow.Cells[8].Text;
            txteditexchangerate.Text = gvrow.Cells[9].Text;
            txteditamountbc.Text = gvrow.Cells[10].Text;
            bool resultcharge = false;
            if (gvrow.Cells[11].Text == "Yes")
            {
                ListItem yes = radioedittaxable.Items.FindByValue("Y");
                yes.Selected = true;

                txtedittaxname.Visible = true;
                txtedittaxpercentage.Visible = true;
                txtedittaxamount.Visible = true;
                txtedittotalamount.Visible = true;


            }
            if (gvrow.Cells[11].Text == "No")
            {
                ListItem no = radioedittaxable.Items.FindByValue("N");
                no.Selected = true;


                txtedittaxname.Visible = false;
                txtedittaxpercentage.Visible = false;
                txtedittaxamount.Visible = false;
                txtedittotalamount.Visible = false;

            }
            txtedittaxname.Text = gvrow.Cells[12].Text;
            txtedittaxpercentage.Text = gvrow.Cells[13].Text;
            txtedittaxamount.Text = gvrow.Cells[14].Text;
            txtedittotalamount.Text = gvrow.Cells[15].Text;

            mpechargeeditinvoicedisplay.Show();



        }
        protected void InvoiceDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
        { 
            
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                string invoiceno = e.Row.Cells[1].Text;
                
                SqlConnection conn = new SqlConnection(_ConnStr);
                conn.Open();
                SqlCommand cmd = new SqlCommand("select SUM(TD_INVOICE_TAXAMOUNT) TM_INVOICE_Taxamount from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No where TM_INVOICE_No = @TM_INVOICE_No group by TM_INVOICE.TM_INVOICE_No", conn);
                cmd.Parameters.Add(new SqlParameter("@TM_INVOICE_No", invoiceno));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Label Tax = (Label)e.Row.FindControl("lbltaxamt");
                    object value = dr.GetValue(0);
                    Tax.Text = value.ToString();
                }
                dr.Close();
               
              
                SqlCommand cmd1 = new SqlCommand("select SUM(TD_INVOICE_TOTALAMOUNT) from TD_INVOICE inner join TM_INVOICE on TD_INVOICE.TD_INVOICE_TMSLNO = TM_INVOICE.TM_INVOICE_No where TM_INVOICE_No = @TM_INVOICE_No group by TM_INVOICE.TM_INVOICE_No", conn);
                cmd1.Parameters.Add(new SqlParameter("@TM_INVOICE_No", invoiceno));
                SqlDataReader dr1 = cmd1.ExecuteReader();
                if (dr1.Read())
                {
                    Label Total = (Label)e.Row.FindControl("lbltotalamt");
                    object value = dr1.GetValue(0);
                    Total.Text = value.ToString();
                }
               if(e.Row.Cells[9].Text == "Y")
                {
                    e.Row.Cells[9].Text = "Accepted";
                    e.Row.Enabled = false;
                }
               
            }
        }


        protected void UpdateInvoice_Click(object sender, EventArgs e)
        {
            string updateinvoice = @"UPDATE [TM_INVOICE] SET [TM_INVOICE_No] = @TM_INVOICE_No,[TM_INVOICE_ShipmentRefNo] = @TM_INVOICE_ShipmentRefNo,[TM_INVOICE_Date] = @TM_INVOICE_Date,[TM_INVOICE_RFQNumber] = @TM_INVOICE_RFQNumber,[TM_INVOICE_COMPANYSLNO] = @TM_INVOICE_COMPANYSLNO,[TM_INVOICE_USERID] = @TM_INVOICE_USERID where [TM_INVOICE_Slno] = @TM_INVOICE_Slno";
            
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.ConnectionString = _ConnStr;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = updateinvoice;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@TM_INVOICE_Slno", lblslno.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_No", txteditinvoice.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_ShipmentRefNo",txteditshipmentrefno.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_Date", Convert.ToDateTime(txteditinvoicedate.Text));
                cmd.Parameters.AddWithValue("@TM_INVOICE_RFQNumber", txteditrfqnumber.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_COMPANYSLNO", lblcompanyslno.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_USERID", Session["M_Subscriber_UserID"]);
                con.Open();
                cmd.ExecuteNonQuery();
                BindGridView();

                con.Close();
                clear();

            }
        }
        protected void btnUpdateCharge_Click(object sender, EventArgs e)
        {
            string taxable = string.Empty;
            ListItem yes = radioedittaxable.Items.FindByValue("Y");
            ListItem no = radioedittaxable.Items.FindByValue("N");
            if(yes.Selected)
            {
                taxable = "Y";
            }
            else if(no.Selected)
            {
                taxable = "N";
            }
            string updatechargeinvoice = @"UPDATE [TD_INVOICE] SET [TD_INVOICE_TMSLNO] = @TD_INVOICE_TMSLNO, [TD_INVOICE_CHARGESLNO] = @TD_INVOICE_CHARGESLNO,
      [TD_INVOICE_DESCRIPTION] = @TD_INVOICE_DESCRIPTION,
           [TD_INVOICE_BASIS] = @TD_INVOICE_BASIS,
          
            [TD_INVOICE_QTY] = @TD_INVOICE_QTY,
             [TD_INVOICE_Rate] = @TD_INVOICE_Rate,
              [TD_INVOICE_CURRENCY] = @TD_INVOICE_CURRENCY,
               [TD_INVOICE_AMOUNTFC] = @TD_INVOICE_AMOUNTFC,
                [TD_INVOICE_EXCHRATE] = @TD_INVOICE_EXCHRATE,
                 [TD_INVOICE_AMOUNTBC] = @TD_INVOICE_AMOUNTBC,
                  [TD_INVOICE_TAXABLE] = @TD_INVOICE_TAXABLE,
                   [TD_INVOICE_TAXPERCENTAGE] = @TD_INVOICE_TAXPERCENTAGE,
              [TD_INVOICE_TAXNAME] = @TD_INVOICE_TAXNAME,
             [TD_INVOICE_TAXAMOUNT] = @TD_INVOICE_TAXAMOUNT,
              [TD_INVOICE_TOTALAMOUNT] = @TD_INVOICE_TOTALAMOUNT,[TD_INVOICE_USERID] = @TD_INVOICE_USERID,[TD_INVOICE_TIMESTAMP] = @TD_INVOICE_TIMESTAMP where [TD_INVOICE_SLNO] = @TD_INVOICE_SLNO";
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.ConnectionString = _ConnStr;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = updatechargeinvoice;
                cmd.CommandType = System.Data.CommandType.Text;
                
                cmd.Parameters.AddWithValue("@TD_INVOICE_SLNO", lblchargeslno.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TMSLNO", dropdowneditinvoice.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TD_INVOICE_CHARGESLNO", dropdownchargeeditinvoice.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TD_INVOICE_DESCRIPTION", txteditdescription.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_BASIS", dropdowneditcharge.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TD_INVOICE_QTY", txteditquantity.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_Rate", txteditrate.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_CURRENCY", dropdowneditcurrency.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TD_INVOICE_AMOUNTFC", txteditamountfc.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_EXCHRATE", txteditexchangerate.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_AMOUNTBC", txteditamountbc.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TAXABLE", taxable);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TAXPERCENTAGE", txtedittaxpercentage.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TAXNAME", txtedittaxname.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TAXAMOUNT", txtedittaxamount.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TOTALAMOUNT", txtedittotalamount.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_USERID", Session["M_Subscriber_UserID"]);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TIMESTAMP", DateTime.Now);
                con.Open();
                cmd.ExecuteNonQuery();

                BindChargeGridView();
                con.Close();
            }
        }

        protected void radiotaxableedit_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (radioedittaxable.SelectedValue.ToString() == "Y")
            {
                
                lbledittaxname.Visible = true;
                txtedittaxname.Visible = true;
                lbltaxpercentage.Visible = true;
                txtedittaxpercentage.Visible = true;
                lbledittaxamount.Visible = true;
                txtedittaxamount.Visible = true;
                lbledittotalamount.Visible = true;
                txtedittotalamount.Visible = true;


            }
            else if (radioedittaxable.SelectedValue.ToString() == "N")
            {
                
                lbledittaxname.Visible = false;
                txtedittaxname.Visible = false;
                lbltaxpercentage.Visible = false;
                txtedittaxpercentage.Visible = false;
                lbledittaxamount.Visible = false;
                txtedittaxamount.Visible = false;
                lbledittotalamount.Visible = false;
                txtedittotalamount.Visible = false;
                txtedittaxname.Text = string.Empty;
                txtedittaxpercentage.Text = string.Empty;
                txtedittaxamount.Text = string.Empty;
                txtedittotalamount.Text = string.Empty;

            }
            mpechargeeditinvoicedisplay.Show();
        }


        protected void radiotaxable_SelectedIndexChanged(object sender,EventArgs e)
        {
            if (radiotaxable.SelectedValue.ToString() == "Y")
            {
                lbltaxname.Visible = true;
                txttaxname.Visible = true;
                lblpercentage.Visible = true;
                txtpercentage.Visible = true;
                lbltaxamount.Visible = true;
                txttaxamount.Visible = true;
                lbltotalamount.Visible = true;
                txttotalamount.Visible = true;
            }
            else if (radiotaxable.SelectedValue.ToString() == "N")
            {
                lbltaxname.Visible = false;
                txttaxname.Visible = false;
                lblpercentage.Visible = false;
                txtpercentage.Visible = false;
                lbltaxamount.Visible = false;
                txttaxamount.Visible = false;
                lbltotalamount.Visible = false;
                txttotalamount.Visible = false;
            }
            mpechargedisplay.Show();
        }
    
        protected void chargebasisdropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (chargebasisdropdown.SelectedItem.Value == "S")
            {
                txtquantity.Text = "1";
                txtquantity.Enabled = false;
            }
            else if (chargebasisdropdown.SelectedItem.Value == "G")
            {
                txtquantity.Enabled = true;
                txtquantity.Text = "";
            }
            else if (chargebasisdropdown.SelectedItem.Value == "C")
            {
                txtquantity.Enabled = true;
                txtquantity.Text = "";
            }


            mpechargedisplay.Show();
        }
         protected void chargebasiseditdropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dropdowneditcharge.SelectedItem.Value == "S")
            {
                txteditquantity.Text = "1";
                txteditquantity.Enabled = false;
            }
            else if (dropdowneditcharge.SelectedItem.Value == "G")
            {
                txteditquantity.Enabled = true;
                txteditquantity.Text = "";
            }
            else if (dropdowneditcharge.SelectedItem.Value == "C")
            {
                txteditquantity.Enabled = true;
                txteditquantity.Text = "";
            }

            mpechargeeditinvoicedisplay.Show();

        }
        protected void btnChargeAdd_Click(object sender, EventArgs e)
        {
            string addinvoice =
@"INSERT INTO [TD_INVOICE] ([TD_INVOICE_TMSLNO],[TD_INVOICE_DESCRIPTION],[TD_INVOICE_CHARGESLNO],[TD_INVOICE_BASIS],
[TD_INVOICE_QTY],[TD_INVOICE_Rate],[TD_INVOICE_CURRENCY],[TD_INVOICE_AMOUNTFC],[TD_INVOICE_EXCHRATE],
[TD_INVOICE_AMOUNTBC],[TD_INVOICE_TAXABLE],[TD_INVOICE_TAXPERCENTAGE],[TD_INVOICE_TAXNAME],
[TD_INVOICE_TAXAMOUNT],[TD_INVOICE_TOTALAMOUNT],[TD_INVOICE_Status],[TD_INVOICE_USERID],[TD_INVOICE_TIMESTAMP]) 

VALUES(@TD_INVOICE_TMSLNO,@TD_INVOICE_DESCRIPTION,@TD_INVOICE_CHARGESLNO,@TD_INVOICE_BASIS,@TD_INVOICE_QTY,
@TD_INVOICE_Rate,@TD_INVOICE_CURRENCY,@TD_INVOICE_AMOUNTFC,@TD_INVOICE_EXCHRATE,@TD_INVOICE_AMOUNTBC,
@TD_INVOICE_TAXABLE,@TD_INVOICE_TAXPERCENTAGE,@TD_INVOICE_TAXNAME,@TD_INVOICE_TAXAMOUNT,@TD_INVOICE_TOTALAMOUNT,'P',@TD_INVOICE_USERID,@TD_INVOICE_TIMESTAMP)";

            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.ConnectionString = _ConnStr;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = addinvoice;
                cmd.CommandType = System.Data.CommandType.Text;
                
                cmd.Parameters.AddWithValue("@TD_INVOICE_TMSLNO", chargeinvoicedropdown.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TD_INVOICE_DESCRIPTION", txtdescriptioncharge.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_BASIS", chargebasisdropdown.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TD_INVOICE_CHARGESLNO", dropdowncharge.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TD_INVOICE_QTY", txtquantity.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_Rate", txtrate.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_CURRENCY", dropdowncurrency.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TD_INVOICE_AMOUNTFC", txtamountfc.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_EXCHRATE", txtexchangerate.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_AMOUNTBC", txtamountbc.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TAXABLE", radiotaxable.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TAXPERCENTAGE", txtpercentage.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TAXNAME", txttaxname.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TAXAMOUNT", txttaxamount.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TOTALAMOUNT", txttotalamount.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_USERID", Session["M_Subscriber_UserID"]);
                cmd.Parameters.AddWithValue("@TD_INVOICE_TIMESTAMP", DateTime.Now);

                con.Open();
                cmd.ExecuteNonQuery();

                BindChargeGridView();

                
            }
        }

        protected void gvChargeInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "G")
                {
                    e.Row.Cells[4].Text = "GrWt";
                }
                else if (e.Row.Cells[4].Text == "C")
                {
                    e.Row.Cells[4].Text = "Chwt";
                }
                else if (e.Row.Cells[4].Text == "S")
                {
                    e.Row.Cells[4].Text = "Shipment";
                }
                if(e.Row.Cells[11].Text == "Y")
                {
                    e.Row.Cells[11].Text = "Yes";
                }
                else if(e.Row.Cells[11].Text == "N")
                {
                    e.Row.Cells[11].Text = "No";
                }
                if(e.Row.Cells[16].Text == "Y")
                {
                    e.Row.Cells[16].Text = "Accepted";
                    e.Row.Enabled = false;
                }
                if (e.Row.Cells[16].Text == "N")
                {
                    e.Row.Cells[16].Text = "Rejected";
                    
                }
                if (e.Row.Cells[16].Text == "P")
                {
                    e.Row.Cells[16].Text = "Pending";

                }
            }
        }

        //protected void Customer_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    mpeInvoiceDisplay.Show();
        //    SqlConnection con = new SqlConnection(_ConnStr);
        //    SqlCommand cmd = new SqlCommand("select M_Currency_Name from M_Currency,M_Company where M_Currency.M_Currency_Code = M_Company.M_Company_Currency and M_Company_Slno = @M_Company_Slno ");
        //    cmd.Parameters.AddWithValue("@M_Company_Slno", Customer.SelectedItem.Value);
        //    cmd.Connection = con;
        //    con.Open();
        //    SqlDataReader sdr = cmd.ExecuteReader();
        //    while(sdr.Read())
        //    {
        //        txtdisplaycurrency.Text = sdr[0].ToString();
        //    }
        //    con.Close();
        //}

        protected void Customereditdropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeInvoiceEdit.Show();
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {

            //invoiceupload.SaveAs(Server.MapPath("~/Uploads/" + invoiceupload.FileName));
            //lblMessage.Text = "File Uploaded";
            //lblMessage.ForeColor = System.Drawing.Color.Green;
            if(invoiceupload.HasFile)
            {
                invoiceupload.PostedFile.SaveAs(Server.MapPath("~/Uploads/" + invoiceupload.FileName));
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("File", typeof(string));
            dt.Columns.Add("Size", typeof(string));
            dt.Columns.Add("Type", typeof(string));
            foreach (string strFile in Directory.GetFiles(Server.MapPath("~/Uploads/")))
            {
                FileInfo fi = new FileInfo(strFile);
                dt.Rows.Add(fi.Name, fi.Length, GetFileTypeByExtension(fi.Extension));



            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        
        private string GetFileTypeByExtension(string extension)
        {
            switch(extension.ToLower())
            {
                case ".doc":
                case ".docx":
                    return "Microsoft Word Document";
                case ".jpg":
                case ".png":
                    return "Image";
                case ".pdf":
                    return "PDF Document";
                case ".txt":
                    return "Text Document";
                case ".xlsx":
                case ".xls":
                    return "Microsoft Excel Document";
                default:
                    return "Unknown";



            }
        }
        private void clear()
        {
            txtinvoicenumber.Text = string.Empty;
            txtinvoicedate.Text = string.Empty;
            txtshipmentrefno.Text = string.Empty;
        }

       

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if(e.CommandName == "Download")
            {
                Response.Clear();
                Response.ContentType = "application/octet-stream";
                Response.AppendHeader("content-disposition", "filename = " + e.CommandArgument);
                Response.TransmitFile(Server.MapPath("~/Uploads/") + e.CommandArgument);
                Response.End();
            }
        }
    }
}
