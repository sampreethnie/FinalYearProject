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
    public class AirportEntity
    {
        public Int32 M_Airport_Slno { get; set; }
        public String M_Airport_Name { get; set; }
        public String M_Airport_Sname { get; set; }
        public Int32 M_Airport_Country { get; set; }

    }
    public partial class Airport : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection con = new SqlConnection(_ConnStr);
                con.Open();
                SqlCommand com = new SqlCommand("select M_Country_Code,M_Country_Name from M_Country", con);
                SqlDataAdapter da = new SqlDataAdapter(com);
                DataSet ds = new DataSet();
                da.Fill(ds);

                countrydropdownlistAddpopup.DataTextField = ds.Tables[0].Columns["M_Country_Name"].ToString();
                countrydropdownlistAddpopup.DataValueField = ds.Tables[0].Columns["M_Country_Code"].ToString();
                countrydropdownlistAddpopup.DataSource = ds.Tables[0];
                countrydropdownlistAddpopup.DataBind();
                countrydropdownlistEditpopup.DataTextField = ds.Tables[0].Columns["M_Country_Name"].ToString();
                countrydropdownlistEditpopup.DataValueField = ds.Tables[0].Columns["M_Country_Code"].ToString();
                countrydropdownlistEditpopup.DataSource = ds.Tables[0];
                countrydropdownlistEditpopup.DataBind();
                countrydropdownlistViewpopup.DataTextField = ds.Tables[0].Columns["M_Country_Name"].ToString();
                countrydropdownlistViewpopup.DataValueField = ds.Tables[0].Columns["M_Country_Code"].ToString();
                countrydropdownlistViewpopup.DataSource = ds.Tables[0];
                countrydropdownlistViewpopup.DataBind();

                con.Close();
                countrydropdownlistAddpopup.Items.Insert(0, new ListItem("--Select country--", "0"));
                countrydropdownlistEditpopup.Items.Insert(0, new ListItem("--Select country--", "0"));
                LoadData();

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
                if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "b")
                {

                    ButtonSeller.Visible = false;
                    btnAdd.Visible = false;
                    gvAirport.Columns[5].Visible = false;
                    gvAirport.Columns[6].Visible = false;



                }
                else if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "s")
                {

                    ButtonBuyer.Visible = false;
                    btnAdd.Visible = false;
                    gvAirport.Columns[5].Visible = false;
                    gvAirport.Columns[6].Visible = false;
                }
                else if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "a")
                {
                    ButtonBuyer.Visible = false;
                    ButtonSeller.Visible = false;
                }


            }
        }
        //Load airport data from database
        public void LoadData()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select *from M_Airport";

                //"select M_Country.M_Country_Code,M_Country.M_Country_Name,M_Country.M_Country_Sname,M_Currency.M_Currency_Name,M_Country.M_Country_Isd from M_Country INNER JOIN M_Currency ON M_Currency.M_Currency_Code = M_Country.M_Country_Currency order by M_Country_Code";

                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dTable = new DataTable();
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTable.Load(dReader);
                gvAirport.DataSource = dTable;
                gvAirport.DataBind();
                conn.Close();
            }
        }
        //Add button click event
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClearPopupControls();
            mpeAirportAdd.Show();

        }
        //Clear Modalpopup data
        private void ClearPopupControls()
        {
            txtairportnameAddpopup.Text = String.Empty;
            txtairportshortnameAddpopup.Text = String.Empty;
        }
        //Adding airport data to database
        public bool AddNewAirport(AirportEntity airport)
        {
            String insertQuery = @"INSERT INTO [M_Airport]([M_Airport_Name],[M_Airport_Sname],[M_Airport_Country]) VALUES(@M_Airport_Name,@M_Airport_Sname,@M_Airport_Country)";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = insertQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Airport_Name", airport.M_Airport_Name);
                cmd.Parameters.AddWithValue("@M_Airport_Sname", airport.M_Airport_Sname);
                cmd.Parameters.AddWithValue("@M_Airport_Country", countrydropdownlistAddpopup.SelectedItem.Value);


                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }
        // Update airport data in database
        public bool UpdateAirport(AirportEntity airport)
        {
            String updateQuery = @"UPDATE [M_Airport] SET [M_Airport_Name]=@M_Airport_Name , [M_Airport_Sname] = @M_Airport_Sname, [M_Airport_Country] = @M_Airport_Country  Where [M_Airport_Slno]=@M_Airport_Slno";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Airport_Slno", airport.M_Airport_Slno);
                cmd.Parameters.AddWithValue("@M_Airport_Name", airport.M_Airport_Name);
                cmd.Parameters.AddWithValue("@M_Airport_Sname", airport.M_Airport_Sname);
                cmd.Parameters.AddWithValue("@M_Airport_Country", countrydropdownlistEditpopup.SelectedItem.Value);


                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }
        //Delete airport data from database
        public bool DeleteAirport(Int32 M_Airport_Slno)
        {
            String deleteQuery = @"Delete from [M_Airport] where [M_Airport_Slno]=@M_Airport_Slno";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = deleteQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Airport_Slno", M_Airport_Slno);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }
        //To show data in popup for editing
        public AirportEntity GetAirport(Int32 M_Airport_Slno)
        {
            String selectQuery = @"select *from [M_Airport] where [M_Airport_Slno]=@M_Airport_Slno";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = selectQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Airport_Slno", M_Airport_Slno);

                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                AirportEntity existingairport = new AirportEntity();
                foreach (DataRow row in dataTable.Rows)
                {
                    existingairport.M_Airport_Slno = Convert.ToInt32(row["M_Airport_Slno"]);
                    existingairport.M_Airport_Name = Convert.ToString(row["M_Airport_Name"]);
                    existingairport.M_Airport_Sname = Convert.ToString(row["M_Airport_Sname"]);
                    existingairport.M_Airport_Country = Convert.ToInt32(row["M_Airport_Country"]);

                }
                conn.Close();

                return existingairport;
            }
        }

        protected void AddpopupSaveButton_Click(object sender, EventArgs e)
        {
            //  ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Sorry there are no attachments');", true);

            if (Page.IsValid)
            {


                AirportEntity airport = new AirportEntity();
                airport.M_Airport_Slno = Convert.ToInt32(airportidAddpopup.Value);
                airport.M_Airport_Name = txtairportnameAddpopup.Text.Trim();
                airport.M_Airport_Sname = txtairportshortnameAddpopup.Text.Trim();
                airport.M_Airport_Country = Convert.ToInt32(countrydropdownlistAddpopup.SelectedItem.Value);




                if (airport.M_Airport_Slno == 0)

                    AddNewAirport(airport);


                LoadData();
            }


        }
        protected void EditpopupSaveButton_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {

                AirportEntity airport = new AirportEntity();
                airport.M_Airport_Slno = Convert.ToInt32(airportidEditpopup.Value);
                airport.M_Airport_Name = txtairportnameEditpopup.Text.Trim();
                airport.M_Airport_Sname = txtairportshortnameEditpopup.Text.Trim();
                airport.M_Airport_Country = Convert.ToInt32(countrydropdownlistEditpopup.SelectedItem.Value);




                if (airport.M_Airport_Slno != 0)

                    UpdateAirport(airport);


                LoadData();
            }


        }
        //Edit button click event
        protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Airport_Slno = Convert.ToInt32(gvAirport.DataKeys[gvRow.RowIndex].Value);
            AirportEntity airport = GetAirport(M_Airport_Slno);
            airportidEditpopup.Value = airport.M_Airport_Slno.ToString();
            txtairportnameEditpopup.Text = airport.M_Airport_Name;
            txtairportshortnameEditpopup.Text = airport.M_Airport_Sname;

            mpeAirportEdit.Show();
        }


        //View button click event
        protected void ibtnView_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Airport_Slno = Convert.ToInt32(gvAirport.DataKeys[gvRow.RowIndex].Value);
            AirportEntity airport = GetAirport(M_Airport_Slno);
            airportidAddpopup.Value = airport.M_Airport_Slno.ToString();
            txtairportnameViewpopup.Text = airport.M_Airport_Name;
            txtairportshortnameViewpopup.Text = airport.M_Airport_Sname;

            mpeAirportView.Show();

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

                    cmd.CommandText = "select M_Airport.M_Airport_Slno,M_Airport.M_Airport_Name,M_Airport.M_Airport_Sname,M_Country.M_Country_Name from M_Airport INNER JOIN M_Country ON M_Airport.M_Airport_Country = M_Country.M_Country_Code where M_Airport.M_Airport_Name LIKE '%'+@M_Airport_Name+'%'";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@M_Airport_Name", txtSearch.Text.Trim());
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        gvAirport.DataSource = dt;
                        gvAirport.DataBind();
                    }
                }

            }
        }
        //Delete button click event
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Airport_Slno = Convert.ToInt32(gvAirport.DataKeys[gvRow.RowIndex].Value);
            if (DeleteAirport(M_Airport_Slno))
                gvRow.Visible = false;
        }
        protected void countrydropdownlistAddpopup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeAirportAdd.Show();

        }
        protected void countrydropdownlistEditpopup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeAirportEdit.Show();
        }
        protected void Refresh(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvAirport.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }

        protected void gvAirport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string countrytest = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "M_Airport_Slno"));

                if (countrytest != "")
                {
                    SqlConnection con = new SqlConnection(_ConnStr);
                    con.Open();
                    string sqlQuery = "select A.M_Airport_Slno,B.M_Country_Name from M_Airport A,M_Country B where A.M_Airport_Slno='" + countrytest + "' and B.M_Country_Code=A.M_Airport_Country";
                    SqlCommand cmd = new SqlCommand(sqlQuery, con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        (e.Row.FindControl("CountryData") as Label).Text = rdr["M_Country_Name"].ToString();
                    }
                    con.Close();

                }


            }
        }
    }
}