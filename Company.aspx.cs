using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace FinalYearProject
{
    public class CompanyEntity
    {
        public Int32 M_Company_MStateSlno { get; set; }
        public Int32 M_Company_MCountrySlno { get; set; }
        public Int32 M_Company_Slno { get; set; }
        public Int32 M_Company_Currency { get; set; }
        public String M_Company_Name { get; set; }
        public String M_Company_Addr1 { get; set; }
        public String M_Company_Addr2 { get; set; }
        public String M_Company_Landmark { get; set; }
        public String M_Company_PIN { get; set; }
        public String M_Company_TAN { get; set; }
        public String M_Company_PAN { get; set; }
        public String M_Company_URL { get; set; }
        public String M_Subscriber_ContactName { get; set; }
        public String M_Subscriber_MobileNo { get; set; }
        public String M_Subscriber_UserID { get; set; }
    }
    public partial class Company : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
       
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
               
            SqlConnection con = new SqlConnection(constr);


            string str = "select M_Company_BuyerSellerFlag,M_Company_Name, M_Company_Addr1,M_Company_Addr2,M_Company_Landmark,M_Company_PIN,M_Company_City,M_Company_TAN,M_Company_PAN,M_Company_URL,M_Company_MCountrySlno,M_Company_MStateSlno,M_Company_Currency,M_Company_City,M_Subscriber_ContactName,M_Subscriber_MobileNo,M_Subscriber_UserID from M_Company,M_Subscriber where M_Subscriber_UserID = '"+Session["M_Subscriber_UserID"]+"' and M_Subscriber_MCompanySlno = M_Company_Slno";
            SqlCommand com = new SqlCommand(str, con);
            

            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(com);
            
            DataSet ds = new DataSet();
            da.Fill(ds);
            txtuserid.Text = ds.Tables[0].Rows[0]["M_Subscriber_UserID"].ToString();
            txtcompanynamefinal.Text = ds.Tables[0].Rows[0]["M_Company_Name"].ToString();
            txtname.Text = ds.Tables[0].Rows[0]["M_Subscriber_ContactName"].ToString();
            txtmobile.Text = ds.Tables[0].Rows[0]["M_Subscriber_MobileNo"].ToString();
            txtaddress1final.Text = ds.Tables[0].Rows[0]["M_Company_Addr1"].ToString();
            txtaddress2final.Text = ds.Tables[0].Rows[0]["M_Company_Addr2"].ToString();
            txtlandmarkfinal.Text = ds.Tables[0].Rows[0]["M_Company_Landmark"].ToString();
            txtpinfinal.Text = ds.Tables[0].Rows[0]["M_Company_PIN"].ToString();
            txttanfinal.Text = ds.Tables[0].Rows[0]["M_Company_TAN"].ToString();
            txtpanfinal.Text = ds.Tables[0].Rows[0]["M_Company_PAN"].ToString();
            txtcompanyurlfinal.Text = ds.Tables[0].Rows[0]["M_Company_URL"].ToString();
            txtcityfinal.Text = ds.Tables[0].Rows[0]["M_Company_City"].ToString();
            categorylist.SelectedValue = ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString();
            dropdowncountryfinal.SelectedValue = ds.Tables[0].Rows[0]["M_Company_MCountrySlno"].ToString();
            dropdownstatefinal.SelectedValue = ds.Tables[0].Rows[0]["M_Company_MStateSlno"].ToString();
            dropdowncurrencyfinal.SelectedValue = ds.Tables[0].Rows[0]["M_Company_Currency"].ToString();
            
               SqlConnection connection = new SqlConnection(constr);
               connection.Open();
               SqlCommand commandcurrency = new SqlCommand("select M_Currency_Code,M_Currency_Name from M_Currency", connection);
               SqlDataAdapter sdacurrency = new SqlDataAdapter(commandcurrency);
               DataSet dascurrency = new DataSet();
               sdacurrency.Fill(dascurrency);

               dropdowncurrencyfinal.DataTextField = dascurrency.Tables[0].Columns["M_Currency_Name"].ToString();
               dropdowncurrencyfinal.DataValueField = dascurrency.Tables[0].Columns["M_Currency_Code"].ToString();
               dropdowncurrencyfinal.DataSource = dascurrency.Tables[0];
               dropdowncurrencyfinal.DataBind();
               

               SqlCommand commandcountry = new SqlCommand("select M_Country_Code,M_Country_Name from M_Country", connection);
               SqlDataAdapter sdacountry = new SqlDataAdapter(commandcountry);
               DataSet dascountry = new DataSet();
               sdacountry.Fill(dascountry);

              dropdowncountryfinal.DataTextField = dascountry.Tables[0].Columns["M_Country_Name"].ToString();
               dropdowncountryfinal.DataValueField = dascountry.Tables[0].Columns["M_Country_Code"].ToString();
               dropdowncountryfinal.DataSource = dascountry.Tables[0];
               dropdowncountryfinal.DataBind();
               dropdowncountryfinal.Items.FindByText(dropdowncountryfinal.SelectedItem.Text).Selected = true;

               SqlCommand commandstate = new SqlCommand("select M_State_Slno,M_State_Name from M_State", connection);
               SqlDataAdapter sdastate = new SqlDataAdapter(commandstate);
               DataSet dasstate = new DataSet();
               sdastate.Fill(dasstate);

               dropdownstatefinal.DataTextField = dasstate.Tables[0].Columns["M_State_Name"].ToString();
               dropdownstatefinal.DataValueField = dasstate.Tables[0].Columns["M_State_Slno"].ToString();
               dropdownstatefinal.DataSource = dasstate.Tables[0];
               dropdownstatefinal.DataBind();
               dropdownstatefinal.Items.FindByText(dropdownstatefinal.SelectedItem.Text).Selected = true;
               
               connection.Close();
               dropdowncurrencyfinal.Items.Insert(0, new ListItem("--Select Currency--", "0"));
               dropdowncountryfinal.Items.Insert(0, new ListItem("--Select Country--", "0"));
               dropdownstatefinal.Items.Insert(0, new ListItem("--Select State--", "0"));

               Session["buyer"] = false;
               
                   
               }

           }






            

        

        protected void btnEnable(object sender,EventArgs e)
        {
            categorylist.Enabled = false;
            txtcompanynamefinal.ReadOnly = false;
            txtaddress1final.ReadOnly = false;
            txtaddress2final.ReadOnly = false;
            txtlandmarkfinal.ReadOnly = false;
            txtcityfinal.ReadOnly = false;
            txtpinfinal.ReadOnly = false;
            txtpanfinal.ReadOnly = false;
            txttanfinal.ReadOnly = false;
            dropdowncountryfinal.Enabled = false;
            dropdowncurrencyfinal.Enabled = false;
            dropdownstatefinal.Enabled = true;
            txtcompanyurlfinal.ReadOnly = false;
            txtname.ReadOnly = false;
            txtmobile.ReadOnly = false;
            txtuserid.ReadOnly = false;
        }

        //protected void btnEdit(object sender,EventArgs e)
        //{
        //   // Response.Write(Session["M_Company_Slno"]);
           
            
                


            


        //    string updatequery = @"UPDATE [M_Company] set  [M_Company_Name] = @M_Company_Name , [M_Company_Addr1] = @M_Company_Addr1 where [M_Company_Slno] = M_Company_Slno" ;
        //    using (SqlConnection conedit = new SqlConnection())
        //    {
        //        conedit.ConnectionString = constr;

        //        SqlCommand cmdedit = conedit.CreateCommand();
        //        cmdedit.CommandText = updatequery;
        //        cmdedit.CommandType = System.Data.CommandType.Text;
        //        CompanyEntity company = new CompanyEntity();
        //        string companyname = txtcompanynamefinal.Text;
        //        string companyaddress = txtaddress1final.Text;

        //        cmdedit.Parameters.AddWithValue("@M_Company_Slno", SqlDbType.Int);

        //        cmdedit.Parameters.AddWithValue("@M_Company_Name", SqlDbType.) = companyname;



        //        cmdedit.Parameters.AddWithValue("@M_Company_Addr1", SqlDbType.Text).Value = companyaddress;




        //        conedit.Open();
        //        cmdedit.ExecuteNonQuery();
        //        conedit.Close();
        //    }    
        //    }
            
            //SqlConnection conedit = new SqlConnection(constr);
            //conedit.Open();
            //SqlCommand cmdedit = new SqlCommand("Update M_Company set M_Company_Name='" + txtcompanynamefinal.Text + "',M_Company_Addr1='" + txtaddress1final.Text + "',M_Company_Addr2='" + txtaddress2final.Text + "',M_Company_Landmark='" + txtlandmarkfinal.Text + "',M_Company_PAN='" + txtpanfinal.Text + "',M_Company_PIN='" + txtpinfinal.Text + "',M_Company_TAN='" + txttanfinal.Text + "',M_Company_URL='" + txtcompanyurlfinal.Text + "' where M_Company_Slno='" + "M_Company_Slno" + "'", conedit);
            //cmdedit.CommandType = CommandType.Text;
            //cmdedit.ExecuteNonQuery();
            //conedit.Close();

            
            //string UpdateQuery = "Update M_Company set M_Company_Name='" + txtcompanynamefinal.Text + "',M_Company_Addr1='" + txtaddress1final.Text + "',M_Company_Addr2='" + txtaddress2final.Text + "',M_Company_Landmark='" + txtlandmarkfinal.Text + "',M_Company_PAN='" + txtpanfinal.Text + "',M_Company_PIN='" + txtpinfinal.Text + "',M_Company_TAN='" + txttanfinal.Text + "',M_Company_URL='" + txtcompanyurlfinal.Text + "' where M_Company_Slno='" + "M_Company_Slno" + "'";
             
           }
    }
