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
    //Used to pass data
    public class CountryEntity
    {
        public Int32 M_Country_Code { get; set; }
        public String M_Country_Name { get; set; }
        public String M_Country_Sname { get; set; }
        public Int32 M_Country_Currency { get; set; }
        public Int32 M_Country_ISD { get; set; }
    }
    
    public partial class Country : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(_ConnStr);
                con.Open();
                SqlCommand com = new SqlCommand("select M_Currency_Code,M_Currency_Name from M_Currency", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);
               
                currencydropdownlistAddpopup.DataTextField = ds.Tables[0].Columns["M_Currency_Name"].ToString();
                currencydropdownlistAddpopup.DataValueField = ds.Tables[0].Columns["M_Currency_Code"].ToString();
                currencydropdownlistAddpopup.DataSource = ds.Tables[0];
                currencydropdownlistAddpopup.DataBind();
                currencydropdownlistEditpopup.DataTextField = ds.Tables[0].Columns["M_Currency_Name"].ToString();
                currencydropdownlistEditpopup.DataValueField = ds.Tables[0].Columns["M_Currency_Code"].ToString();
                currencydropdownlistEditpopup.DataSource = ds.Tables[0];
                currencydropdownlistEditpopup.DataBind();
                currencydropdownlistViewpopup.DataTextField = ds.Tables[0].Columns["M_Currency_Name"].ToString();
                currencydropdownlistViewpopup.DataValueField = ds.Tables[0].Columns["M_Currency_Code"].ToString();
                currencydropdownlistViewpopup.DataSource = ds.Tables[0];
                currencydropdownlistViewpopup.DataBind();

                con.Close();
                currencydropdownlistAddpopup.Items.Insert(0, new ListItem("--Select currency--", "0"));
                currencydropdownlistEditpopup.Items.Insert(0, new ListItem("--Select currency--", "0"));
                LoadData();
                lblusername.Text = "Username:" + Session["M_Subscriber_UserID"];
                SqlConnection con1 = new SqlConnection(_ConnStr);
                con1.Open();
                string str = "select M_Company_Name,M_Company_BuyerSellerFlag from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
                SqlCommand com1 = new SqlCommand(str, con1);
                SqlDataAdapter da1 = new SqlDataAdapter(com1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                lblcompanyname.Text = "CompanyName:" + ds1.Tables[0].Rows[0]["M_Company_Name"].ToString();
                //lblbuyersellerflag.Text = "Type:" + ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString();
                if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "b")
                {
                    btnseller.Visible = false;

                }
                else
                {
                    btnbuyer.Visible = false;
                }



            }
           
         
          

        }

        //Load country data from database
        public void LoadData()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select *from M_Country";
                    
                   //"select M_Country.M_Country_Code,M_Country.M_Country_Name,M_Country.M_Country_Sname,M_Currency.M_Currency_Name,M_Country.M_Country_Isd from M_Country INNER JOIN M_Currency ON M_Currency.M_Currency_Code = M_Country.M_Country_Currency order by M_Country_Code";
               
                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dTable = new DataTable();
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTable.Load(dReader);
                gvCountry.DataSource = dTable;
                gvCountry.DataBind();
                conn.Close();
            }
        }

        //Add button click event
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClearPopupControls();
            mpeCountryAdd.Show();

        }

        //Clear Modalpopup data
        private void ClearPopupControls()
        {
            txtcountrynameAddpopup.Text = String.Empty;
            txtcountryshortnameAddpopup.Text = String.Empty;
            txtcountryisdAddpopup.Text = String.Empty;

        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }

        //Adding country data to database
        public bool AddNewCountry(CountryEntity country)
        {
            String insertQuery = @"INSERT INTO [M_Country]([M_Country_Name],[M_Country_Sname],[M_Country_Currency],[M_Country_Isd]) VALUES(@M_Country_Name,@M_Country_Sname,@M_Country_Currency,@M_Country_Isd)";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = insertQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Country_Name", country.M_Country_Name);
                cmd.Parameters.AddWithValue("@M_Country_Sname", country.M_Country_Sname);
                cmd.Parameters.AddWithValue("@M_Country_Currency", currencydropdownlistAddpopup.SelectedItem.Value);

                cmd.Parameters.AddWithValue("@M_Country_Isd", @country.M_Country_ISD);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }

        // Update country data in database
        public bool UpdateCountry(CountryEntity country)
        {
            String updateQuery = @"UPDATE [M_Country] SET [M_Country_Name]=@M_Country_Name , [M_Country_Sname] = @M_Country_Sname, [M_Country_Currency] = @M_Country_Currency ,[M_Country_Isd] = @M_Country_Isd Where [M_Country_Code]=@M_Country_Code";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Country_Code", country.M_Country_Code);
                cmd.Parameters.AddWithValue("@M_Country_Name", country.M_Country_Name);
                cmd.Parameters.AddWithValue("@M_Country_Sname", country.M_Country_Sname);
                cmd.Parameters.AddWithValue("@M_Country_Currency", currencydropdownlistEditpopup.SelectedItem.Value);
                cmd.Parameters.AddWithValue("@M_Country_Isd", country.M_Country_ISD);

                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }

        //Delete country data from database
        public bool DeleteCountry(Int32 M_Country_Code)
        {
            String deleteQuery = @"Delete from [M_Country] where [M_Country_Code]=@M_Country_Code";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = deleteQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Country_Code", M_Country_Code);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }

        //To show data in popup for editing
        public CountryEntity GetCountry(Int32 M_Country_Code)
        {
            String selectQuery = @"select *from [M_Country] where [M_Country_Code]=@M_Country_Code";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = selectQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Country_Code", M_Country_Code);

                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                CountryEntity existingcountry = new CountryEntity();
                foreach (DataRow row in dataTable.Rows)
                {
                    existingcountry.M_Country_Code = Convert.ToInt32(row["M_Country_Code"]);
                    existingcountry.M_Country_Name = Convert.ToString(row["M_Country_Name"]);
                    existingcountry.M_Country_Sname = Convert.ToString(row["M_Country_Sname"]);
                    existingcountry.M_Country_Currency = Convert.ToInt32(row["M_Country_Currency"]);
                    existingcountry.M_Country_ISD = Convert.ToInt32(row["M_Country_Isd"]);
                }
                conn.Close();

                return existingcountry;
            }
        }
       
        protected void AddpopupSaveButton_Click(object sender, EventArgs e)
        {
            //  ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Sorry there are no attachments');", true);

            if (Page.IsValid)
            {


                CountryEntity country = new CountryEntity();
                country.M_Country_Code = Convert.ToInt32(countryidAddpopup.Value);
                country.M_Country_Name = txtcountrynameAddpopup.Text.Trim();
                country.M_Country_Sname = txtcountryshortnameAddpopup.Text.Trim();
                country.M_Country_Currency = Convert.ToInt32(currencydropdownlistAddpopup.SelectedItem.Value);
                country.M_Country_ISD = Convert.ToInt32(txtcountryisdAddpopup.Text.Trim());



                if (country.M_Country_Code == 0)

                    AddNewCountry(country);


                LoadData();
            }


        }
        protected void EditpopupSaveButton_Click(object sender, EventArgs e)
        {




            if (Page.IsValid)
            {

                CountryEntity country = new CountryEntity();
                country.M_Country_Code = Convert.ToInt32(countryidEditpopup.Value);
                country.M_Country_Name = txtcountrynameEditpopup.Text.Trim();
                country.M_Country_Sname = txtcountryshortnameEditpopup.Text.Trim();
                country.M_Country_Currency = Convert.ToInt32(currencydropdownlistEditpopup.SelectedItem.Value);
                country.M_Country_ISD = Convert.ToInt32(txtcountryisdEditpopup.Text.Trim());


                
                if (country.M_Country_Code != 0)

                    UpdateCountry(country);


                LoadData();
            }


        }

        

        //Edit button click event
        protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Country_Code = Convert.ToInt32(gvCountry.DataKeys[gvRow.RowIndex].Value);
            CountryEntity country = GetCountry(M_Country_Code);
            countryidEditpopup.Value = country.M_Country_Code.ToString();
            txtcountrynameEditpopup.Text = country.M_Country_Name;
            txtcountryshortnameEditpopup.Text = country.M_Country_Sname;
            if (country.M_Country_ISD != null)
            {
                txtcountryisdEditpopup.Text = country.M_Country_ISD.ToString();
            }
            mpeCountryEdit.Show();
        }


        
        //View button click event
        protected void ibtnView_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Country_Code = Convert.ToInt32(gvCountry.DataKeys[gvRow.RowIndex].Value);
            CountryEntity country = GetCountry(M_Country_Code);
            countryidAddpopup.Value = country.M_Country_Code.ToString();
            txtcountrynameViewpopup.Text = country.M_Country_Name;
            txtcountryshortnameViewpopup.Text = country.M_Country_Sname;
            if (country.M_Country_ISD != null)
            {
                txtcountryisdViewpopup.Text = country.M_Country_ISD.ToString();
            }
            mpeCountryView.Show();

        }

        // Search button Click event
        protected void Search(object sender, ImageClickEventArgs e)
        {
            this.BindGrid();




        }
        private void BindGrid()
        {
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {

                    cmd.CommandText = "select M_Country.M_Country_Code,M_Country.M_Country_Name,M_Country.M_Country_Sname,M_Currency.M_Currency_Name,M_Country_Isd from M_Country INNER JOIN M_Currency ON M_Country.M_Country_Currency = M_Currency.M_Currency_Code where M_Country.M_Country_Name LIKE '%'+@M_Country_Name+'%'";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@M_Country_Name", txtSearch.Text.Trim());
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        gvCountry.DataSource = dt;
                        gvCountry.DataBind();
                    }
                }

            }
        }

        //Delete button click event
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Country_Code = Convert.ToInt32(gvCountry.DataKeys[gvRow.RowIndex].Value);
            string statecheck = "select M_Country_Code from M_Country where exists (select M_State_M_Countryslno from M_State where M_State_M_Countryslno = '" + M_Country_Code + "' )";
            string airportcheck = "select M_Country_Code from M_Country where exists(select M_Airport_Country from M_Airport where M_Airport_Country = '" + M_Country_Code + "')";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmdstate = conn.CreateCommand();
                
                cmdstate.CommandText = statecheck;
                
                cmdstate.CommandType = System.Data.CommandType.Text;
                
                conn.Open();
                SqlDataReader dataReaderstate = cmdstate.ExecuteReader();
                
                if (dataReaderstate.HasRows)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cannot delete the data')", true);
                }


                else if (DeleteCountry(M_Country_Code))
                {
                    gvRow.Visible = false;
                }
                conn.Close();
                SqlCommand cmdairport = conn.CreateCommand();
                cmdairport.CommandText = airportcheck;
                cmdairport.CommandType = System.Data.CommandType.Text;


                conn.Open();
                SqlDataReader dataReaderairport = cmdairport.ExecuteReader();
                if(dataReaderairport.HasRows)
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cannot delete the data')", true);
                }
                else if (DeleteCountry(M_Country_Code))
                {
                    gvRow.Visible = false;
                }
                conn.Close();
            }
        }

        protected void currencydropdownlistAddpopup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeCountryAdd.Show();

        }

        protected void currencydropdownlistEditpopup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeCountryEdit.Show();
        }

        protected void Refresh(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCountry.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void gvCountry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                string currencytest = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "M_Country_Code"));

                if(currencytest !="")
                {
                    SqlConnection con = new SqlConnection(_ConnStr);
                    con.Open();
                    string sqlQuery = "select A.M_Country_Code,B.M_Currency_Name from M_Country A,M_Currency B where A.M_Country_Code='" + currencytest + "' and B.M_Currency_Code=A.M_Country_Currency";
                    SqlCommand cmd = new SqlCommand(sqlQuery,con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.Read())
                    {
                        (e.Row.FindControl("CurrencyData") as Label).Text = rdr["M_Currency_Name"].ToString();
                    }
                    con.Close();
                   
                }

             
            }
        }





    }
}
        
    

