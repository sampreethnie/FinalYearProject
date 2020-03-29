﻿using System;
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
    public partial class ShipmentDetails_Seller_ : System.Web.UI.Page
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
               
                BindGridView();
                
                {

                }
            }}
           
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
            txtreceivedby.Text = string.Empty;

        }
            
            private void BindGridView()
            { 
                SqlConnection con1 = new SqlConnection(_ConnStr);

                
                    con1.Open();
                
                SqlCommand cmd = new SqlCommand("select *from ShipmentDetailsSeller",con1);
                SqlDataAdapter daseller = new SqlDataAdapter(cmd);
                DataSet dsseller = new DataSet();
                daseller.Fill(dsseller);
                GridViewSeller.DataSource = dsseller;
                GridViewSeller.DataBind();
                lbltotalcount.Text = GridViewSeller.Rows.Count.ToString();
                
                con1.Close();

            }

            protected void GridViewSeller_RowDataBound(object sender, GridViewRowEventArgs e)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    e.Row.Cells[2].Text = "ShipmentNumber";
                    e.Row.Cells[3].Text = "Creation Date";
                    e.Row.Cells[4].Text = "Customer Name";
                    e.Row.Cells[5].Text = "Number of Packages";
                    e.Row.Cells[6].Text = "HAWB";
                    e.Row.Cells[7].Text = "HAWB Date";
                    e.Row.Cells[8].Text = "MAWB";
                    e.Row.Cells[9].Text = "MAWB Date";
                    e.Row.Cells[10].Text = "Airline";
                    e.Row.Cells[11].Text = "Flight Number";
                    e.Row.Cells[12].Text = "ETD";
                    e.Row.Cells[13].Text = "ETA";
                    e.Row.Cells[14].Text = "ATD";
                    e.Row.Cells[15].Text = "ATA";
                    e.Row.Cells[16].Text = "Delivered";
                    e.Row.Cells[17].Text = "DeliveryDate";
                    e.Row.Cells[18].Text = "Received By";

               
                }
                if(e.Row.RowType == DataControlRowType.DataRow)
                {
                    
                       
                      if(e.Row.Cells[16].Text == "Yes")
                      {
                          e.Row.Cells[16].Text="Yes";
                      }
                      if(e.Row.Cells[16].Text == "No")
                      {
                          e.Row.Cells[16].Text = "No";
                      }


                   

                      
                      
                    
                }
            }
        

        protected void Addbutton_Click(object sender, EventArgs e)
        {
            string adddetails = @"INSERT INTO [ShipmentDetailsSeller] ([creationdate],[customer_M_Company_Name],[numberofpackages],[hawb],[hawbdate],[mawb],[mawbdate],[airline],[flightnumber],[etd],[eta],[atd],[ata],[delivered],[delivery],[receivedbyname])
            VALUES(@creationdate,@customer_M_Company_Name,@numberofpackages,@hawb,@hawbdate,@mawb,@mawbdate,@airline,@flightnumber,@etd,@eta,@atd,@ata,@delivered,@delivery,@receivedbyname)";
            string deliverchecking = delivery.Checked ? "Y" : "N";
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                con.ConnectionString = _ConnStr;
                
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = adddetails;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@shipmentnumber",txtshipmentnumber.Text);
                cmd.Parameters.AddWithValue("@creationdate",Convert.ToDateTime(txtcreationdate.Text));
                cmd.Parameters.AddWithValue("@customer_M_Company_Name", dropdowncustomer.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@numberofpackages", txtnoofpackages.Text);
                cmd.Parameters.AddWithValue("@hawb", txthawb.Text);
                cmd.Parameters.AddWithValue("@hawbdate",Convert.ToDateTime(txthawbdate.Text));
                cmd.Parameters.AddWithValue("@mawb", txtmawb.Text);
                cmd.Parameters.AddWithValue("@mawbdate", Convert.ToDateTime(txtmawbdate.Text));
                cmd.Parameters.AddWithValue("@airline", txtairline.Text);
                cmd.Parameters.AddWithValue("@flightnumber", txtflightnumber.Text);
                cmd.Parameters.AddWithValue("@etd",Convert.ToDateTime (txtetd.Text));
                cmd.Parameters.AddWithValue("@eta",Convert.ToDateTime(txteta.Text));
                cmd.Parameters.AddWithValue("@atd",Convert.ToDateTime(txtatd.Text));
                cmd.Parameters.AddWithValue("@ata",Convert.ToDateTime(txtata.Text));
                cmd.Parameters.AddWithValue("@delivered", deliverchecking);
                cmd.Parameters.AddWithValue("@delivery",Convert.ToDateTime(txtdeliverydate.Text));
                cmd.Parameters.AddWithValue("@receivedbyname", txtreceivedby.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                BindGridView();
                clear();
                con.Close();
            }
        }
        protected void GridViewSeller_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridViewSeller.SelectedRow;
            txtshipmentnumber.Text = row.Cells[2].Text;
            txtcreationdate.Text = row.Cells[3].Text;
            dropdowncustomer.SelectedItem.Text = row.Cells[4].Text;
            txtnoofpackages.Text = row.Cells[5].Text;
            txthawb.Text = row.Cells[6].Text;
            txthawbdate.Text = row.Cells[7].Text;
            txtmawb.Text = row.Cells[8].Text;
            txtmawbdate.Text = row.Cells[9].Text;
            txtairline.Text = row.Cells[10].Text;
            txtflightnumber.Text = row.Cells[11].Text;
            txtetd.Text = row.Cells[12].Text;
            txteta.Text = row.Cells[13].Text;
            txtatd.Text = row.Cells[14].Text;
            txtata.Text = row.Cells[15].Text;
            bool result = false;
            if(row.Cells[16].Text=="Yes")
            {
                result = true;
                delivery.Checked = true;

            }
            else
            {
                result = false;
                delivery.Checked = false;
            }
           
            txtdeliverydate.Text = row.Cells[17].Text;
            txtreceivedby.Text = row.Cells[18].Text;
            Addbutton.Visible = false;
            Updatebutton.Visible = true;
            BindGridView();
            

        }
        protected void GridViewSeller_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            int shipmentnumber = Convert.ToInt32(GridViewSeller.DataKeys[e.RowIndex].Value);
            SqlCommand delcommand = new SqlCommand("delete from ShipmentDetailsSeller where shipmentnumber= '"+shipmentnumber+"'",con);
            delcommand.ExecuteNonQuery();
            GridViewSeller.EditIndex = -1;
            BindGridView();
            con.Close();


            

        }
       protected void Updatebutton_Click(object sender,EventArgs e)
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            SqlCommand cmd = new SqlCommand("update ShipmentDetailsSeller set creationdate=@creationdate,customer_M_Company_Name=@customer_M_Company_Name,numberofpackages=@numberofpackages,hawb=@hawb,hawbdate=@hawbdate,mawb=@mawb,mawbdate=@mawbdate,airline=@airline,flightnumber=@flightnumber,etd=@etd,eta=@eta,atd=@atd,ata=@ata,delivered=@delivered,delivery=@delivery,receivedbyname=@receivedbyname where shipmentnumber=@shipmentnumber", con);
            cmd.Parameters.AddWithValue("@shipmentnumber", txtshipmentnumber.Text);
            cmd.Parameters.AddWithValue("@creationdate", Convert.ToDateTime(txtcreationdate.Text));
            cmd.Parameters.AddWithValue("@customer_M_Company_Name", dropdowncustomer.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@numberofpackages", txtnoofpackages.Text);
            cmd.Parameters.AddWithValue("@hawb", txthawb.Text);
            cmd.Parameters.AddWithValue("@hawbdate", Convert.ToDateTime(txthawbdate.Text));
            cmd.Parameters.AddWithValue("@mawb", txtmawb.Text);
            cmd.Parameters.AddWithValue("@mawbdate", Convert.ToDateTime(txtmawbdate.Text));
            cmd.Parameters.AddWithValue("@airline", txtairline.Text);
            cmd.Parameters.AddWithValue("@flightnumber", txtflightnumber.Text);
            cmd.Parameters.AddWithValue("@etd", Convert.ToDateTime(txtetd.Text));
            cmd.Parameters.AddWithValue("@eta", Convert.ToDateTime(txteta.Text));
            cmd.Parameters.AddWithValue("@atd", Convert.ToDateTime(txtatd.Text));
            cmd.Parameters.AddWithValue("@ata", Convert.ToDateTime(txtata.Text));
            cmd.Parameters.AddWithValue("@delivered", delivery.Text);
            cmd.Parameters.AddWithValue("@delivery", Convert.ToDateTime(txtdeliverydate.Text));
            cmd.Parameters.AddWithValue("@receivedbyname", txtreceivedby.Text);
            cmd.ExecuteNonQuery();
            GridViewSeller.EditIndex = -1;
            BindGridView();
            Updatebutton.Visible = false;
            
            
        }
        protected void Submitbutton_Click(object sender,EventArgs e)
       {
           SqlConnection con = new SqlConnection(_ConnStr);
           con.Open();
           SqlCommand cmd = new SqlCommand("update ShipmentDetailsSeller set creationdate=@creationdate,customer_M_Company_Name=@customer_M_Company_Name,numberofpackages=@numberofpackages,hawb=@hawb,hawbdate=@hawbdate,mawb=@mawb,mawbdate=@mawbdate,airline=@airline,flightnumber=@flightnumber,etd=@etd,eta=@eta,atd=@atd,ata=@ata,delivered=@delivered,delivery=@delivery,receivedbyname=@receivedbyname where shipmentnumber=@shipmentnumber", con);
           cmd.Parameters.AddWithValue("@shipmentnumber", txtshipmentnumber.Text);
           cmd.Parameters.AddWithValue("@creationdate", Convert.ToDateTime(txtcreationdate.Text));
           cmd.Parameters.AddWithValue("@customer_M_Company_Name", dropdowncustomer.SelectedItem.Text);
           cmd.Parameters.AddWithValue("@numberofpackages", txtnoofpackages.Text);
           cmd.Parameters.AddWithValue("@hawb", txthawb.Text);
           cmd.Parameters.AddWithValue("@hawbdate", Convert.ToDateTime(txthawbdate.Text));
           cmd.Parameters.AddWithValue("@mawb", txtmawb.Text);
           cmd.Parameters.AddWithValue("@mawbdate", Convert.ToDateTime(txtmawbdate.Text));
           cmd.Parameters.AddWithValue("@airline", txtairline.Text);
           cmd.Parameters.AddWithValue("@flightnumber", txtflightnumber.Text);
           cmd.Parameters.AddWithValue("@etd", Convert.ToDateTime(txtetd.Text));
           cmd.Parameters.AddWithValue("@eta", Convert.ToDateTime(txteta.Text));
           cmd.Parameters.AddWithValue("@atd", Convert.ToDateTime(txtatd.Text));
           cmd.Parameters.AddWithValue("@ata", Convert.ToDateTime(txtata.Text));
           cmd.Parameters.AddWithValue("@delivered", delivery.Text);
           cmd.Parameters.AddWithValue("@delivery", Convert.ToDateTime(txtdeliverydate.Text));
           cmd.Parameters.AddWithValue("@receivedbyname", txtreceivedby.Text);
           cmd.ExecuteNonQuery();
           BindGridView();


         

               GridViewSeller.SelectedRow.Cells[0].Enabled = false;
               GridViewSeller.SelectedRow.Cells[1].Enabled = false;
           
               Updatebutton.Visible = false;
          
           
          



       }
        protected void Cancelbutton_Click(object sender,EventArgs e)
       {
           clear();
           
           Addbutton.Visible = true;


       }
    }
}