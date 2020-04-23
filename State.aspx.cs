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
    public class StateEntity
    {
        public Int32 M_State_Slno { get; set; }
        public String M_State_Name { get; set; }
        public String M_State_Sname { get; set; }
        public Int32 M_State_M_Countryslno { get; set; }
        
    }
    public partial class State : System.Web.UI.Page
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
                    gvState.Columns[5].Visible = false;
                    gvState.Columns[6].Visible = false;



                }
                else if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "s")
                {

                    ButtonBuyer.Visible = false;
                    btnAdd.Visible = false;
                    gvState.Columns[5].Visible = false;
                    gvState.Columns[6].Visible = false;
                }
                else if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "a")
                {
                    ButtonBuyer.Visible = false;
                    ButtonSeller.Visible = false;
                }

            }
        }
            //Load state data from database
            public void LoadData()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select *from M_State";
                    
                   //"select M_Country.M_Country_Code,M_Country.M_Country_Name,M_Country.M_Country_Sname,M_Currency.M_Currency_Name,M_Country.M_Country_Isd from M_Country INNER JOIN M_Currency ON M_Currency.M_Currency_Code = M_Country.M_Country_Currency order by M_Country_Code";
               
                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dTable = new DataTable();
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTable.Load(dReader);
                gvState.DataSource = dTable;
                gvState.DataBind();
                conn.Close();
            }
        }
        //Add button click event
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClearPopupControls();
            mpeStateAdd.Show();

        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        //Clear Modalpopup data
        private void ClearPopupControls()
        {
            txtstatenameAddpopup.Text = String.Empty;
            txtstateshortnameAddpopup.Text = String.Empty;
        }
        //Adding state data to database
        public bool AddNewState(StateEntity state)
        {
            String insertQuery = @"INSERT INTO [M_State]([M_State_Name],[M_State_Sname],[M_State_M_Countryslno]) VALUES(@M_State_Name,@M_State_Sname,@M_State_M_Countryslno)";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = insertQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_State_Name", state.M_State_Name);
                cmd.Parameters.AddWithValue("@M_State_Sname", state.M_State_Sname);
                cmd.Parameters.AddWithValue("@M_State_M_Countryslno", countrydropdownlistAddpopup.SelectedItem.Value);

               
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }
         // Update state data in database
        public bool UpdateState(StateEntity state)
        {
            String updateQuery = @"UPDATE [M_State] SET [M_State_Name]=@M_State_Name , [M_State_Sname] = @M_State_Sname, [M_State_M_Countryslno] = @M_State_M_Countryslno  Where [M_State_Slno]=@M_State_Slno";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_State_Slno", state.M_State_Slno);
                cmd.Parameters.AddWithValue("@M_State_Name", state.M_State_Name);
                cmd.Parameters.AddWithValue("@M_State_Sname", state.M_State_Sname);
                cmd.Parameters.AddWithValue("@M_State_M_Countryslno", countrydropdownlistEditpopup.SelectedItem.Value);
               

                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }
         //Delete state data from database
        public bool DeleteState(Int32 M_State_Slno)
        {
            String deleteQuery = @"Delete from [M_State] where [M_State_Slno]=@M_State_Slno";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = deleteQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_State_Slno", M_State_Slno);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();

                return true;
            }
        }
        //To show data in popup for editing
        public StateEntity GetState(Int32 M_State_Slno)
        {
            String selectQuery = @"select *from [M_State] where [M_State_Slno]=@M_State_Slno";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = selectQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_State_Slno", M_State_Slno);

                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dataReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                StateEntity existingstate = new StateEntity();
                foreach (DataRow row in dataTable.Rows)
                {
                    existingstate.M_State_Slno = Convert.ToInt32(row["M_State_Slno"]);
                    existingstate.M_State_Name = Convert.ToString(row["M_State_Name"]);
                    existingstate.M_State_Sname = Convert.ToString(row["M_State_Sname"]);
                    existingstate.M_State_M_Countryslno = Convert.ToInt32(row["M_State_M_Countryslno"]);
                  
                }
                conn.Close();

                return existingstate;
            }
        }
         protected void AddpopupSaveButton_Click(object sender, EventArgs e)
        {
            //  ScriptManager.RegisterStartupScript(this, GetType(), "showalert", "alert('Sorry there are no attachments');", true);

            if (Page.IsValid)
            {


                StateEntity state = new StateEntity();
                state.M_State_Slno = Convert.ToInt32(stateidAddpopup.Value);
                state.M_State_Name = txtstatenameAddpopup.Text.Trim();
                state.M_State_Sname = txtstateshortnameAddpopup.Text.Trim();
                state.M_State_M_Countryslno = Convert.ToInt32(countrydropdownlistAddpopup.SelectedItem.Value);
              



                if (state.M_State_Slno == 0)

                    AddNewState(state);


                LoadData();
            }


        }

        protected void EditpopupSaveButton_Click(object sender, EventArgs e)
        {




            if (Page.IsValid)
            {

               StateEntity state =  new StateEntity();
                state.M_State_Slno = Convert.ToInt32(stateidEditpopup.Value);
                state.M_State_Name = txtstatenameEditpopup.Text.Trim();
                state.M_State_Sname = txtstateshortnameEditpopup.Text.Trim();
                state.M_State_M_Countryslno = Convert.ToInt32(countrydropdownlistEditpopup.SelectedItem.Value);
               


                
                if (state.M_State_Slno != 0)

                    UpdateState(state);


                LoadData();
            }


        }
          //Edit button click event
        protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_State_Slno = Convert.ToInt32(gvState.DataKeys[gvRow.RowIndex].Value);
            StateEntity state = GetState(M_State_Slno);
            stateidEditpopup.Value = state.M_State_Slno.ToString();
            txtstatenameEditpopup.Text = state.M_State_Name;
            txtstateshortnameEditpopup.Text = state.M_State_Sname;
           
            mpeStateEdit.Show();
        }
         //View button click event
        protected void ibtnView_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_State_Slno = Convert.ToInt32(gvState.DataKeys[gvRow.RowIndex].Value);
            StateEntity state = GetState(M_State_Slno);
            stateidAddpopup.Value = state.M_State_Slno.ToString();
            txtstatenameViewpopup.Text = state.M_State_Name;
            txtstateshortnameViewpopup.Text = state.M_State_Sname;
           
            mpeStateView.Show();

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

                    cmd.CommandText = "select M_State.M_State_Slno,M_State.M_State_Name,M_State.M_State_Sname,M_Country.M_Country_Name from M_State INNER JOIN M_Country ON M_State.M_State_M_Countryslno = M_Country.M_Country_Code where M_State.M_State_Name LIKE '%'+@M_State_Name+'%'";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@M_State_Name", txtSearch.Text.Trim());
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        gvState.DataSource = dt;
                        gvState.DataBind();
                    }
                }

            }
        }
        //Delete button click event
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_State_Slno = Convert.ToInt32(gvState.DataKeys[gvRow.RowIndex].Value);
            if (DeleteState(M_State_Slno))
                gvRow.Visible = false;
        }
        protected void countrydropdownlistAddpopup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeStateAdd.Show();

        }
        protected void countrydropdownlistEditpopup_SelectedIndexChanged(object sender, EventArgs e)
        {
            mpeStateEdit.Show();
        }
        protected void Refresh(object sender, ImageClickEventArgs e)
        {
            LoadData();
        }
         protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvState.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        
     protected void gvState_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if(e.Row.RowType == DataControlRowType.DataRow)
            {
                string countrytest = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "M_State_Slno"));

                if(countrytest !="")
                {
                    SqlConnection con = new SqlConnection(_ConnStr);
                    con.Open();
                    string sqlQuery = "select A.M_State_Slno,B.M_Country_Name from M_State A,M_Country B where A.M_State_Slno='" + countrytest + "' and B.M_Country_Code=A.M_State_M_Countryslno";
                    SqlCommand cmd = new SqlCommand(sqlQuery,con);
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if(rdr.Read())
                    {
                        (e.Row.FindControl("CountryData") as Label).Text = rdr["M_Country_Name"].ToString();
                    }
                    con.Close();
                   
                }

             
            }
        }
    }
}