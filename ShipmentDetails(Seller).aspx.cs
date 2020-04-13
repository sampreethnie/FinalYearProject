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
            txtgrossweight.Text = string.Empty;
            txtchargeableweight.Text = string.Empty;
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
            txtreceivermobilenumber.Text = string.Empty;
            txtreceivermailid.Text = string.Empty;


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
               
                if(e.Row.RowType == DataControlRowType.DataRow)
                {
                    
                       
                      if(e.Row.Cells[18].Text == "Y")
                      {
                          e.Row.Cells[18].Text="Yes";
                      }
                      if(e.Row.Cells[18].Text == "N")
                      {
                          e.Row.Cells[18].Text = "No";
                      }


                   

                      
                      
                    
                }
            }
        

        protected void Addbutton_Click(object sender, EventArgs e)
        {
            string adddetails = @"INSERT INTO [ShipmentDetailsSeller] ([creationdate],[customer_M_Company_Name],[numberofpackages],[grossweight],[chargeableweight],[hawb],[hawbdate],[mawb],[mawbdate],[airline],[flightnumber],[etd],[eta],[atd],[ata],[delivered],[delivery],[receivedbyname],[receivermobileno],[receiveremailid])
            VALUES(@creationdate,@customer_M_Company_Name,@numberofpackages,@grossweight,@chargeableweight,@hawb,@hawbdate,@mawb,@mawbdate,@airline,@flightnumber,@etd,@eta,@atd,@ata,@delivered,@delivery,@receivedbyname,@receivermobileno,@receiveremailid)";
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
                cmd.Parameters.AddWithValue("@grossweight", txtgrossweight.Text);
                cmd.Parameters.AddWithValue("@chargeableweight", txtchargeableweight.Text);
                cmd.Parameters.AddWithValue("@receivermobileno", txtreceivermobilenumber.Text);
                cmd.Parameters.AddWithValue("@receiveremailid", txtreceivermailid.Text);
                con.Open();
                cmd.ExecuteNonQuery();
                BindGridView();
                clear();
                con.Close();
               

                
                
            }
        }
        protected void mailbutton_Click(object sender,EventArgs e)
        {
            StringWriter sw = new StringWriter();
            HtmlTextWriter ht = new HtmlTextWriter(sw);
            GridViewSeller.RenderControl(ht);
            MailMessage mm = new MailMessage("sampreeth1998@gmail.com", txtreceivermailid.Text);
            mm.Body = "<h1> Gridview Details </h1> <hr/>" + sw.ToString();
            mm.IsBodyHtml = true;
            mm.Subject = "gridviewdata";
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            System.Net.NetworkCredential nc = new System.Net.NetworkCredential("sampreeth1998@gmail.com", "sampreet");
            smtp.Credentials = nc;
            smtp.Send(mm);
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
           
        }



        protected void GridViewSeller_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GridViewSeller.SelectedRow;
            txtshipmentnumber.Text = row.Cells[2].Text;
            txtcreationdate.Text = row.Cells[3].Text;
            dropdowncustomer.SelectedItem.Text = row.Cells[4].Text;
            txtnoofpackages.Text = row.Cells[5].Text;
            txtgrossweight.Text = row.Cells[6].Text;
            txtchargeableweight.Text = row.Cells[7].Text;
            txthawb.Text = row.Cells[8].Text;
            txthawbdate.Text = row.Cells[9].Text;
            txtmawb.Text = row.Cells[10].Text;
            txtmawbdate.Text = row.Cells[11].Text;
            txtairline.Text = row.Cells[12].Text;
            txtflightnumber.Text = row.Cells[13].Text;
            txtetd.Text = row.Cells[14].Text;
            txteta.Text = row.Cells[15].Text;
            txtatd.Text = row.Cells[16].Text;
            txtata.Text = row.Cells[17].Text;

            bool result = false;
            if(row.Cells[18].Text=="Yes")
            {
                result = true;
                delivery.Checked = true;

            }
            else
            {
                result = false;
                delivery.Checked = false;
            }
           
            txtdeliverydate.Text = row.Cells[19].Text;
            txtreceivedby.Text = row.Cells[20].Text;
            txtreceivermobilenumber.Text = row.Cells[21].Text;
            txtreceivermailid.Text = row.Cells[22].Text;
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
            SqlCommand cmd = new SqlCommand("update ShipmentDetailsSeller set creationdate=@creationdate,customer_M_Company_Name=@customer_M_Company_Name,numberofpackages=@numberofpackages,hawb=@hawb,hawbdate=@hawbdate,mawb=@mawb,mawbdate=@mawbdate,airline=@airline,flightnumber=@flightnumber,etd=@etd,eta=@eta,atd=@atd,ata=@ata,delivered=@delivered,delivery=@delivery,receivedbyname=@receivedbyname,receivermobileno=@receivermobileno,receiveremailid=@receiveremailid where shipmentnumber=@shipmentnumber", con);
            cmd.Parameters.AddWithValue("@shipmentnumber", txtshipmentnumber.Text);
            cmd.Parameters.AddWithValue("@creationdate", Convert.ToDateTime(txtcreationdate.Text));
            cmd.Parameters.AddWithValue("@customer_M_Company_Name", dropdowncustomer.SelectedItem.Text);
            cmd.Parameters.AddWithValue("@numberofpackages", txtnoofpackages.Text);
            cmd.Parameters.AddWithValue("@grossweight", txtgrossweight.Text);
            cmd.Parameters.AddWithValue("@chargeableweight", txtchargeableweight.Text);
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
            cmd.Parameters.AddWithValue("@receivermobileno", txtreceivermobilenumber.Text);
            cmd.Parameters.AddWithValue("@receiveremailid", txtreceivermailid.Text);
            cmd.ExecuteNonQuery();
            GridViewSeller.EditIndex = -1;
            BindGridView();
            Updatebutton.Visible = false;
            
            
        }
        protected void Submitbutton_Click(object sender,EventArgs e)
       {
           SqlConnection con = new SqlConnection(_ConnStr);
           con.Open();
           SqlCommand cmd = new SqlCommand("update ShipmentDetailsSeller set creationdate=@creationdate,customer_M_Company_Name=@customer_M_Company_Name,numberofpackages=@numberofpackages,grossweight=@grossweight,chargeableweight=@chargeableweight,hawb=@hawb,hawbdate=@hawbdate,mawb=@mawb,mawbdate=@mawbdate,airline=@airline,flightnumber=@flightnumber,etd=@etd,eta=@eta,atd=@atd,ata=@ata,delivered=@delivered,delivery=@delivery,receivedbyname=@receivedbyname,receivermobileno=@receivermobileno,receiveremailid=@receiveremailid where shipmentnumber=@shipmentnumber", con);
           cmd.Parameters.AddWithValue("@shipmentnumber", txtshipmentnumber.Text);
           cmd.Parameters.AddWithValue("@creationdate", Convert.ToDateTime(txtcreationdate.Text));
           cmd.Parameters.AddWithValue("@customer_M_Company_Name", dropdowncustomer.SelectedItem.Text);
           cmd.Parameters.AddWithValue("@numberofpackages", txtnoofpackages.Text);
            cmd.Parameters.AddWithValue("@grossweight", txtgrossweight.Text);
            cmd.Parameters.AddWithValue("@chargeableweight", txtchargeableweight.Text);
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
            cmd.Parameters.AddWithValue("@receivermobileno", txtreceivermobilenumber.Text);
            cmd.Parameters.AddWithValue("@receiveremailid", txtreceivermailid.Text);
        
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
                    cmd.CommandText = "SELECT shipmentnumber,creationdate,customer_M_Company_Name,numberofpackages,grossweight,chargeableweight,hawb,hawbdate,mawb,mawbdate,airline,flightnumber,etd,eta,atd,ata,delivered,delivery,receivedbyname,receivermobileno,receiveremailid FROM ShipmentDetailsSeller WHERE shipmentnumber LIKE '%'+@shipmentnumber+'%' OR customer_M_Company_Name LIKE '%'+@customer_M_Company_Name+'%' OR numberofpackages LIKE '%'+@numberofpackages+'%' OR hawb LIKE '%'+@hawb+'%' OR mawb LIKE '%'+@mawb+'%' OR airline LIKE '%'+@airline+'%' OR flightnumber LIKE '%'+@flightnumber+'%' OR delivered LIKE '%'+@delivered+'%' OR receivedbyname LIKE '%'+@receivedbyname+'%' OR receivermobileno LIKE '%'+@receivermobileno+'%' OR receiveremailid LIKE '%'+@receiveremailid+'%' ";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@shipmentnumber", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@customer_M_Company_Name", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@numberofpackages", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@grossweight", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@chargeableweight", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@hawb", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@mawb", txtSearch.Text.Trim());
                      
                    cmd.Parameters.AddWithValue("@airline", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@flightnumber", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@delivered", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@receivedbyname", txtSearch.Text.Trim());
                    cmd.Parameters.AddWithValue("@receivermobileno", txtSearch.Text.Trim());
                     cmd.Parameters.AddWithValue("@receiveremailid", txtSearch.Text.Trim());

                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        GridViewSeller.DataSource = dt;
                        GridViewSeller.DataBind();

                    }
                }
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewSeller.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void Refresh(object sender, ImageClickEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from ShipmentDetailsSeller order by shipmentnumber";
                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dTable = new DataTable();
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTable.Load(dReader);
                GridViewSeller.DataSource = dTable;
                GridViewSeller.DataBind();
            }

        }
    }
}