using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Data;
using System.Data.SqlClient;


namespace FinalYearProject
{
    //Used to pass data
    public class MainpageEntity
    {
        public Int32 M_Company_Slno { get; set; }
        public String M_Company_Name { get; set; }
        public String M_Company_Addr1 { get; set; }
        public String M_Company_Addr2 { get; set; }
        public String M_Company_Landmark { get; set; }
        public String M_Company_City { get; set; }
        public Int32 M_Company_MStateSlno { get; set; }
        public Int32 M_Company_MCountrySlno { get; set; }
        public String M_Company_PIN { get; set; }
        public String M_Company_TAN { get; set; }
        public DateTime M_Company_Timestamp { get; set; }
        public String M_Company_PAN { get; set; }
        public Int32 M_Company_Currency { get; set; }
        public String M_Company_URL { get; set; }
        public Int32 M_Subscriber_Slno { get; set; }
        public Int32 M_Subscriber_MCompanySlno { get; set; }
        public String M_Subscriber_ContactName { get; set; }
        public String M_Subscriber_MobileNo { get; set; }
        public String M_Subscriber_Password { get; set; }
        public String M_Subscriber_UserID { get; set; }

        public DateTime M_Subscriber_TimeStamp { get; set; }

    }

    public partial class Mainpage : System.Web.UI.Page
    {

        string constr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(constr);
                con.Open();
               
                
                SqlCommand comcountry = new SqlCommand("select M_Country_Code,M_Country_Name from M_Country", con);
                
               
                SqlDataAdapter dacountry = new SqlDataAdapter(comcountry);
                DataSet dscountry = new DataSet();
                dacountry.Fill(dscountry);
                dropdowncountry.DataTextField = dscountry.Tables[0].Columns["M_Country_Name"].ToString();
                dropdowncountry.DataValueField = dscountry.Tables[0].Columns["M_Country_Code"].ToString();
                dropdowncountry.DataSource = dscountry.Tables[0];
                dropdowncountry.DataBind();
                con.Close();
                con.Open();
                SqlCommand comstate = new SqlCommand("select M_State_Slno,M_State_Name from M_State", con);
                SqlDataAdapter dastate = new SqlDataAdapter(comstate);
                DataSet dsstate = new DataSet();
                dastate.Fill(dsstate);
                dropdownstate.DataTextField = dsstate.Tables[0].Columns["M_State_Name"].ToString();
                dropdownstate.DataValueField = dsstate.Tables[0].Columns["M_State_Slno"].ToString();
                dropdownstate.DataSource = dsstate.Tables[0];
                dropdownstate.DataBind();
                con.Close();
                con.Open();
                SqlCommand comcurrency = new SqlCommand("select M_Currency_Code,M_Currency_Name from M_Currency", con);
                SqlDataAdapter dacurrency = new SqlDataAdapter(comcurrency);
                DataSet dscurrency = new DataSet();
                dacurrency.Fill(dscurrency);
                dropdowncurrency.DataTextField = dscurrency.Tables[0].Columns["M_Currency_Name"].ToString();
                dropdowncurrency.DataValueField = dscurrency.Tables[0].Columns["M_Currency_Code"].ToString();
                dropdowncurrency.DataSource = dscurrency.Tables[0];
                dropdowncurrency.DataBind();
                con.Close();

                dropdowncountry.Items.Insert(0,     new ListItem("--Select country--", "0"));
                dropdownstate.Items.Insert(0, new ListItem("--Select state--", "0"));
                dropdowncurrency.Items.Insert(0, new ListItem("--Select currency--", "0"));


            }
            
        }
        protected void btnSubmit(object sender, EventArgs e)
        {


            String query = @"DECLARE @companyid int INSERT INTO [M_Company]([M_Company_TimeStamp],[M_Company_BuyerSellerFlag],[M_Company_Name],[M_Company_Addr1],[M_Company_Addr2],[M_Company_Landmark],[M_Company_PIN],[M_Company_TAN],[M_Company_PAN],[M_Company_URL],[M_Company_MCountrySlno],[M_Company_MStateSlno],[M_Company_Currency],[M_Company_City]) VALUES(@M_Company_TimeStamp,@M_Company_BuyerSellerFlag,@M_Company_Name,@M_Company_Addr1,@M_Company_Addr2,@M_Company_Landmark,@M_Company_PIN,@M_Company_TAN,@M_Company_PAN,@M_Company_URL,@M_Company_MCountrySlno,@M_Company_MStateSlno,@M_Company_Currency,@M_Company_City) SET @companyid = SCOPE_IDENTITY();
            INSERT INTO [M_Subscriber] ([M_Subscriber_MCompanySlno],[M_Subscriber_TimeStamp],[M_Subscriber_ContactName],[M_Subscriber_MobileNo],[M_Subscriber_UserID],[M_Subscriber_Password]) VALUES(@companyid,@M_Subscriber_TimeStamp,@M_Subscriber_ContactName,@M_Subscriber_MobileNo,@M_Subscriber_UserID,@M_Subscriber_Password)";
           
            using(SqlConnection con = new SqlConnection())
            {
                //string strpass = encryptpass(txtpassword1.Text);
                con.ConnectionString = constr;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
               
                cmd.CommandText = query;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Company_TimeStamp", DateTime.Now);
                cmd.Parameters.AddWithValue("@M_Company_BuyerSellerFlag", categorylist.SelectedItem.Value);
                 cmd.Parameters.AddWithValue("@M_Company_Name", txtcompanyname.Text);
                 cmd.Parameters.AddWithValue("@M_Company_Addr1", txtaddress1.Text);
                 cmd.Parameters.AddWithValue("@M_Company_Addr2", txtaddress2.Text);
                 cmd.Parameters.AddWithValue("@M_Company_Landmark", txtlandmark.Text);
                 cmd.Parameters.AddWithValue("@M_Company_City",txtcity.Text);
                 cmd.Parameters.AddWithValue("@M_Company_PIN", txtpin.Text);
                 cmd.Parameters.AddWithValue("@M_Company_MCountrySlno",dropdowncountry.SelectedItem.Value);
                 cmd.Parameters.AddWithValue("@M_Company_MStateSlno",dropdownstate.SelectedItem.Value);
                 cmd.Parameters.AddWithValue("@M_Company_Currency", dropdowncurrency.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@M_Company_PAN", txtpan.Text);
                cmd.Parameters.AddWithValue("@M_Company_TIN", txttin.Text);
                cmd.Parameters.AddWithValue("@M_Company_TAN", txttan.Text);
                 cmd.Parameters.AddWithValue("@M_Company_URL", txtcompanyurl.Text);
                 cmd.Parameters.AddWithValue("@M_Subscriber_ContactName", txtname.Text);
                 cmd.Parameters.AddWithValue("@M_Subscriber_MobileNo", txtmobile.Text);
                 cmd.Parameters.AddWithValue("@M_Subscriber_UserID", txtuserid.Text);
                 cmd.Parameters.AddWithValue("@M_Subscriber_Password", txtpassword1.Text);
                 cmd.Parameters.AddWithValue("@M_Subscriber_TimeStamp", DateTime.Now);
                
                 
                 cmd.ExecuteNonQuery();
                
                 con.Close();

            }


           
           // con.Open();
            
           //string companyquery=String.Format(@"insert into M_Company(M_Company_BuyerSellerFlag,M_Company_Name,M_Company_Addr1,M_Company_Addr2,M_Company_Landmark,M_Company_PIN,M_Company_TAN,M_Company_PAN,M_Company_URL,M_Company_MCountrySlno,M_Company_MStateSlno,M_Company_Currency) " + "VALUES(@M_Company_BuyerSellerFlag,@M_Company_Name,@M_Company_Addr1,@M_Company_Addr2,@M_Company_Landmark,@M_Company_PIN,@M_Company_TAN,@M_Company_PAN,@M_Company_URL,@M_Company_MCountrySlno,@M_Company_MStateSlno,@M_Company_Currency)") ;
           //cmd.Parameters.AddWithValue("M_Company_BuyerSellerFlag", categorylist.SelectedValue);
           // cmd.Parameters.AddWithValue("M_Company_Name", txtcompanyname.Text);
           // cmd.Parameters.AddWithValue("M_Company_Addr1", txtaddress1.Text);
           // cmd.Parameters.AddWithValue("M_Company_Addr2", txtaddress2.Text);
           // cmd.Parameters.AddWithValue("M_Company_Landmark", txtlandmark.Text);
           // cmd.Parameters.AddWithValue("M_Company_Name", txtcompanyname.Text);
           // cmd.Parameters.AddWithValue("M_Company_City",txtcity.Text);
           // cmd.Parameters.AddWithValue("M_Company_PIN", txtpin.Text);
           // cmd.Parameters.AddWithValue("M_Company_MCountrySlno",dropdowncountry.SelectedItem.Value);
           // cmd.Parameters.AddWithValue("M_Company_MStateSlno",dropdownstate.SelectedItem.Value);
           // cmd.Parameters.AddWithValue("M_Company_PAN", txtpan.Text);
           // cmd.Parameters.AddWithValue("M_Company_TIN", txttin.Text);
           // cmd.Parameters.AddWithValue("M_Company_URL", txtcompanyurl.Text);
           // cmd = new SqlCommand(companyquery, con);
           // cmd.ExecuteNonQuery();
           
           // string subscriberquery = String.Format("@insert into M_Subscriber(M_Subscriber_ContactName,M_Subscriber_MobileNo,M_Subscriber_UserID,M_Subscriber_Password) " + "VALUES(@M_Subscriber_ContactName,@M_Subscriber_MobileNo,@M_Subscriber_UserID,@M_Subscriber_Password)");
           // cmd.Parameters.AddWithValue("M_Subscriber_ContactName", txtname.Text);
           // cmd.Parameters.AddWithValue("M_Subscriber_MobileNo", txtmobile.Text);
           // cmd.Parameters.AddWithValue("M_Subscriber_UserID", txtuserid.Text);
           // cmd.Parameters.AddWithValue("M_Subscriber_Password", txtpassword1.Text);
           // cmd = new SqlCommand(subscriberquery, con);
           // cmd.ExecuteNonQuery();
           // con.Close();



        }

        //public string encryptpass(string password)
        //{
        //    string msg = "";
        //    byte[] encode = new byte[password.Length];
        //    encode = Encoding.UTF8.GetBytes(password);
        //    msg = Convert.ToBase64String(encode);
        //    return msg;

        //}
       
        
        protected void btnlogin(object sender,EventArgs e)
        {
            //String M_Subscriber_UserID = txtEmail.Text.Trim();
            //String M_Subscriber_Password = txtPassword.Text.Trim();

            SqlConnection con = new SqlConnection(constr);
            con.Open();
            //string str = "select * from M_Subscriber";
            //SqlCommand com = new SqlCommand(str);
            //SqlDataAdapter sda = new SqlDataAdapter(com.CommandText, con);
            //DataTable dt = new DataTable();
            //sda.Fill(dt);
            //int RowCount = dt.Rows.Count;
            //for(int i=0;i<RowCount;i++)
            //{
            //    M_Subscriber_UserID = dt.Rows[i]["M_Subscriber_UserID"].ToString();
            //    M_Subscriber_Password = dt.Rows[i]["M_Subscriber_Password"].ToString();
            //    if(M_Subscriber_UserID == txtEmail.Text && M_Subscriber_Password == txtPassword.Text)
            //    {
            //        Session["M_Subscriber_UserID"] = M_Subscriber_UserID;
            //        Response.Redirect("Company.aspx");
            //    }
            //    else
            //    {
            //        lblmessage.Text = "UserName or password is incorrect";
            //    }
            //}
            SqlCommand cmd = new SqlCommand("select M_Company_Name, M_Company_Addr1,M_Company_Addr2,M_Company_Landmark,M_Company_PIN,M_Company_TAN,M_Company_PAN,M_Company_URL,M_Company_MCountrySlno,M_Company_MStateSlno,M_Company_Currency,M_Company_City,M_Subscriber_ContactName,M_Subscriber_MobileNo,M_Subscriber_UserID,M_Subscriber_Password from M_Company,M_Subscriber where M_Subscriber_UserID=@M_Subscriber_UserID and M_Subscriber_Password = @M_Subscriber_Password", con);
            cmd.Parameters.AddWithValue("@M_Subscriber_UserID", txtEmail.Text);
            cmd.Parameters.AddWithValue("@M_Subscriber_Password", txtPassword.Text);
            SqlDataReader sdr = cmd.ExecuteReader();
            if (sdr.Read())
            {
                Session["M_Subscriber_UserID"] = txtEmail.Text;
                
                
                Response.Redirect("Company.aspx");

            }
            else
            {
                lblmessage.Text = "Username or password is incorrect";


            }
            if(categorylist.SelectedValue == "b")
            {
                Session["buyer"] = true;
            }
            else
            {
                Session["buyer"] = false;
            }
            con.Close();
        }
       protected void OnSelectedIndexChangedCountry(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#RegistrationModal').modal('show')", true);

        }
       protected void OnSelectedIndexChangedState(object sender, EventArgs e)
       {
           ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#RegistrationModal').modal('show')", true);

       }
       protected void OnSelectedIndexChangedCurrency(object sender, EventArgs e)
       {
           ClientScript.RegisterStartupScript(this.GetType(), "Popup", "$('#RegistrationModal').modal('show')", true);

       }
    }
}