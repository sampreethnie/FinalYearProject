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




namespace FinalYearProject
{
    public partial class InvoiceDisplay : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            string dateTime = Request.QueryString["creationdate"];

            DateTime dt = Convert.ToDateTime(dateTime);

            string finaltime = dt.ToString();




            Label1.Text = Request.QueryString["shipmentnumber"];
            Label2.Text = Request.QueryString["customer_M_Company_Name"];
            Label3.Text = finaltime;

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


                SqlCommand comcompany = new SqlCommand("select M_Company_Slno,M_Company_Name from M_Company", con);


                SqlDataAdapter dacompany = new SqlDataAdapter(comcompany);
                DataSet dscompany = new DataSet();
                dacompany.Fill(dscompany);
                Customer.DataTextField = dscompany.Tables[0].Columns["M_Company_Name"].ToString();
                Customer.DataValueField = dscompany.Tables[0].Columns["M_Company_Slno"].ToString();
                Customer.DataSource = dscompany.Tables[0];
                Customer.DataBind();
                Customereditpopup.DataTextField = dscompany.Tables[0].Columns["M_Company_Name"].ToString();
                Customereditpopup.DataValueField = dscompany.Tables[0].Columns["M_Company_Slno"].ToString();
                Customereditpopup.DataSource = dscompany.Tables[0];
                Customereditpopup.DataBind();

                con.Close();
                BindGridView();
                Customer.Items.Insert(0, new ListItem("--Select Customer--", "0"));
                Customereditpopup.Items.Insert(0, new ListItem("--Select Customer--", "0"));
                SqlConnection conchargeinvoice = new SqlConnection(_ConnStr);

                conchargeinvoice.Open();
                SqlCommand comchargeinvoice = new SqlCommand("select TM_INVOICE_No from TM_INVOICE", conchargeinvoice);
                SqlDataAdapter dachargeinvoice = new SqlDataAdapter(comchargeinvoice);
                DataSet dschargeinvoice = new DataSet();
                dachargeinvoice.Fill(dschargeinvoice);
                chargeinvoicedropdown.DataValueField = dschargeinvoice.Tables[0].Columns["TM_INVOICE_No"].ToString();
                chargeinvoicedropdown.DataSource = dschargeinvoice.Tables[0];
                chargeinvoicedropdown.DataBind();
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
                concurrencycharge.Close();
                dropdowncurrency.Items.Insert(0, new ListItem("--Select Currency--"));
                chargebasisdropdown.Items.Insert(0, new ListItem("--Select Charge Basis--"));
                BindGridView();
                BindChargeGridView();

            }
        }
        private void BindGridView()
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("select TM_INVOICE_Slno,TM_INVOICE_No,TM_INVOICE_ShipmentRefNo,M_Company_Name,TM_INVOICE_Description,TM_INVOICE_ShipmentDelivered from TM_INVOICE,M_Company where TM_INVOICE.TM_INVOICE_Customer = M_Company.M_Company_Slno", con);
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
            SqlCommand cmd = new SqlCommand("select TD_INVOICE_SLNO,TD_INVOICE_TMSLNO,TD_INVOICE_DESCRIPTION,TD_INVOICE_BASIS,TD_INVOICE_QTY,TD_INVOICE_Rate,M_Currency_Name,TD_INVOICE_AMOUNTFC,TD_INVOICE_EXCHRATE,TD_INVOICE_AMOUNTBC,TD_INVOICE_TAXABLE,TD_INVOICE_TAXPERCENTAGE,TD_INVOICE_TAXNAME,TD_INVOICE_TAXAMOUNT,TD_INVOICE_TOTALAMOUNT from TD_INVOICE,M_Currency where TD_INVOICE.TD_INVOICE_CURRENCY = M_Currency.M_Currency_Code", con);
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
        protected void AddInvoiceButton_Click(object sender, EventArgs e)
        {
            string addinvoice = @"INSERT INTO [TM_INVOICE] ([TM_INVOICE_No],[TM_INVOICE_ShipmentRefNo],[TM_INVOICE_Customer],[TM_INVOICE_Description],[TM_INVOICE_ShipmentDelivered]) VALUES(@TM_INVOICE_No,@TM_INVOICE_ShipmentRefNo,@TM_INVOICE_Customer,@TM_INVOICE_Description,@TM_INVOICE_ShipmentDelivered)";
            string isdeliverycheck = Isdelivered.Checked ? "Y" : "N";
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.ConnectionString = _ConnStr;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = addinvoice;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@TM_INVOICE_No", txtinvoicenumber.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_ShipmentRefNo", txtshipmentrefno.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_Customer", Customer.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TM_INVOICE_Description", txtDescription.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_ShipmentDelivered", isdeliverycheck);

                con.Open();
                cmd.ExecuteNonQuery();

                BindGridView();
                con.Close();

            }

        }

        protected void ImageButtoninvoice_Click(object sender, EventArgs e)
        {
            ImageButton imgbtnInvoice = sender as ImageButton;
            GridViewRow gvRow = (GridViewRow)imgbtnInvoice.NamingContainer;

            lblslno.Text = InvoiceGridView.DataKeys[gvRow.RowIndex].Value.ToString();
            txtinvoicenumbereditpopup.Text = gvRow.Cells[1].Text;
            txtshipmentrefnoeditpopup.Text = gvRow.Cells[2].Text;
            Customereditpopup.SelectedItem.Text = gvRow.Cells[3].Text;
            txtdescriptioneditpopup.Text = gvRow.Cells[4].Text;
            bool result = false;
            if (gvRow.Cells[5].Text == "Y")
            {
                result = true;
                Isdeliverededitpopup.Checked = true;

            }
            else
            {
                result = false;
                Isdeliverededitpopup.Checked = false;
            }
            mpeInvoiceEdit.Show();
        }
        protected void ImagebuttonCharge_Click(object sender, EventArgs e)
        {
            ImageButton imgbtnCharge = sender as ImageButton;
            GridView gvRow = (GridViewRow)imgbtnCharge.NamingContainer;
            lblchargeinvoice.Text = gvChargeInvoice.DataKeys[gvRow.]


        }
        protected void InvoiceDisplay_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[5].Text == "Y")
                {
                    e.Row.Cells[5].Text = "Yes";
                }
                else
                {
                    e.Row.Cells[5].Text = "No";
                }
            }
        }


        protected void UpdateInvoice_Click(object sender, EventArgs e)
        {
            string updateinvoice = @"UPDATE [TM_INVOICE] SET [TM_INVOICE_No] = @TM_INVOICE_No,[TM_INVOICE_ShipmentRefNo] = @TM_INVOICE_ShipmentRefNo,[TM_INVOICE_Customer] = @TM_INVOICE_Customer,[TM_INVOICE_Description] = @TM_INVOICE_Description,[TM_INVOICE_ShipmentDelivered] = @TM_INVOICE_ShipmentDelivered where [TM_INVOICE_Slno] = @TM_INVOICE_Slno";
            string isdeliverycheckeditpopup = Isdeliverededitpopup.Checked ? "Y" : "N";
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.ConnectionString = _ConnStr;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = updateinvoice;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@TM_INVOICE_Slno", lblslno.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_No", txtinvoicenumbereditpopup.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_ShipmentRefNo", txtshipmentrefnoeditpopup.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_Customer", Customereditpopup.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TM_INVOICE_Description", txtdescriptioneditpopup.Text);
                cmd.Parameters.AddWithValue("@TM_INVOICE_ShipmentDelivered", isdeliverycheckeditpopup);

                con.Open();
                cmd.ExecuteNonQuery();

                BindGridView();


            }
        }

        protected void radiotaxable_SelectedIndexChanged(object sender, EventArgs e)
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
        protected void btnChargeAdd_Click(object sender, EventArgs e)
        {
            string addinvoice =
@"INSERT INTO [TD_INVOICE] ([TD_INVOICE_TMSLNO],[TD_INVOICE_DESCRIPTION],[TD_INVOICE_BASIS],
[TD_INVOICE_QTY],[TD_INVOICE_Rate],[TD_INVOICE_CURRENCY],[TD_INVOICE_AMOUNTFC],[TD_INVOICE_EXCHRATE],
[TD_INVOICE_AMOUNTBC],[TD_INVOICE_TAXABLE],[TD_INVOICE_TAXPERCENTAGE],[TD_INVOICE_TAXNAME],
[TD_INVOICE_TAXAMOUNT],[TD_INVOICE_TOTALAMOUNT]) 

VALUES(@TD_INVOICE_TMSLNO,@TD_INVOICE_DESCRIPTION,@TD_INVOICE_BASIS,@TD_INVOICE_QTY,
@TD_INVOICE_Rate,@TD_INVOICE_CURRENCY,@TD_INVOICE_AMOUNTFC,@TD_INVOICE_EXCHRATE,@TD_INVOICE_AMOUNTBC,
@TD_INVOICE_TAXABLE,@TD_INVOICE_TAXPERCENTAGE,@TD_INVOICE_TAXNAME,@TD_INVOICE_TAXAMOUNT,@TD_INVOICE_TOTALAMOUNT)";

            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.ConnectionString = _ConnStr;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = addinvoice;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@TD_INVOICE_TMSLNO", chargeinvoicedropdown.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@TD_INVOICE_DESCRIPTION", txtdescriptioncharge.Text);
                cmd.Parameters.AddWithValue("@TD_INVOICE_BASIS", chargebasisdropdown.SelectedItem.Value);
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

                con.Open();
                cmd.ExecuteNonQuery();

                BindChargeGridView();


            }
        }

        protected void gvChargeInvoice_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text == "G")
                {
                    e.Row.Cells[3].Text = "GrWt";
                }
                else if (e.Row.Cells[3].Text == "C")
                {
                    e.Row.Cells[3].Text = "Chwt";
                }
                else if (e.Row.Cells[3].Text == "S")
                {
                    e.Row.Cells[3].Text = "Shipment";
                }
                if(e.Row.Cells[10].Text == "Y")
                {
                    e.Row.Cells[10].Text = "Yes";
                }
                else if(e.Row.Cells[10].Text == "N")
                {
                    e.Row.Cells[10].Text = "No";
                }
            }
        }
    }
}
