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

using FinalYearProject;

namespace FinalYearProject
{
    public class CurrencyEntity
    {
        public Int32 M_Currency_Code { get; set; }
        public String M_Currency_Name { get; set; }
        public String M_Currency_Sname { get; set; }
    }

    public partial class Currency : System.Web.UI.Page
    {


        String _ConnStr = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;



        protected void Page_Load(object sender, EventArgs e)
        {
            Page.UnobtrusiveValidationMode = UnobtrusiveValidationMode.None;
            if (!IsPostBack)
            {
                LoadData();
            }
            //if (!this.IsPostBack)
            //{
            //    this.BindGrid();
            //}
       lblusername.Text = "Username:" + Session["M_Subscriber_UserID"]  ;
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            string str = "select M_Company_Name,M_Company_BuyerSellerFlag from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
            SqlCommand com = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblcompanyname.Text = "CompanyName:"+ ds.Tables[0].Rows[0]["M_Company_Name"].ToString();
            //lblbuyersellerflag.Text = "Type:" + ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString();
           if(ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "b")
            {
                btnseller.Visible = false;

            }
            else
            {
                btnbuyer.Visible = false;
            }

            //else
            //{
            //    btnbuyer.Visible = false;
                
            //}
            //if (Session["buyer"].Equals('b'))

            //{
            //    btnseller.Visible = false;
                  
                
            //}
            // else if(Session["buyer"].Equals('s'))
            //{
            //    btnbuyer.Visible = false;
               
            //}
          

            


        }

        public void LoadData()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from M_Currency order by M_Currency_Code";
                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dTable = new DataTable();
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTable.Load(dReader);
                gvCurrency.DataSource = dTable;
                gvCurrency.DataBind();
            }
        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClearPopupControls();
            mpeCurrency.Show();
        }
        private void ClearPopupControls()
        {
            
            txtcurrencyname.Text = String.Empty;
            txtcurrencyshortname.Text = String.Empty;
        }
        public bool AddNewCurrency(CurrencyEntity user)
        {
            String insertQuery = @"INSERT INTO [M_Currency] ([M_Currency_Name],[M_Currency_Sname]) VALUES(@M_Currency_Name, @M_Currency_Sname)";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = insertQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Currency_Name", user.M_Currency_Name);
                cmd.Parameters.AddWithValue("@M_Currency_Sname", user.M_Currency_Sname);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                
                return true;
            }

        }
        #region[Update User record into database]
        public bool UpdateCurrency(CurrencyEntity user)
        {
            String updateQuery = @"UPDATE [M_Currency] SET [M_Currency_Name] = @M_Currency_Name ,[M_Currency_Sname] = @M_Currency_Sname Where [M_Currency_Code] = @M_Currency_Code";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Currency_Code", user.M_Currency_Code);
                cmd.Parameters.AddWithValue("@M_Currency_Name", user.M_Currency_Name);
                cmd.Parameters.AddWithValue("@M_Currency_Sname", user.M_Currency_Sname);


                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                
                return true;

            }

        }
        #endregion
        #region[Delete User record from database]
        public bool DeleteCurrency(Int32 M_Currency_Code)
        {
            String updateQuery = @"Delete from [M_Currency] Where [M_Currency_Code] = @M_Currency_Code";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Currency_Code", M_Currency_Code);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        #endregion

        #region[Get User to show in popup for edit]
        public CurrencyEntity GetUserByCurrency(Int32 M_Currency_Code)
        {
            String updateQuery = @"Select * From  [M_Currency]
                            Where [M_Currency_Code] = @M_Currency_Code";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Currency_Code", M_Currency_Code);


                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dataReader =
                cmd.ExecuteReader(CommandBehavior.CloseConnection);

                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                CurrencyEntity existingcurrency = new CurrencyEntity();
                foreach (DataRow row in dataTable.Rows)
                {
                    existingcurrency.M_Currency_Code = Convert.ToInt32(row["M_Currency_Code"]);
                    existingcurrency.M_Currency_Name = Convert.ToString(row["M_Currency_Name"]);
                    existingcurrency.M_Currency_Sname = Convert.ToString(row["M_Currency_Sname"]);


                }

                return existingcurrency;
            }
        }
        #endregion
        #region[Save User]
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CurrencyEntity currency = new CurrencyEntity();
                currency.M_Currency_Code = Convert.ToInt32(currencyid.Value);
                currency.M_Currency_Name = txtcurrencyname.Text.Trim();
                currency.M_Currency_Sname = txtcurrencyshortname.Text.Trim();

                if (currency.M_Currency_Code == 0)
                    AddNewCurrency(currency);
                else
                    UpdateCurrency(currency);

                LoadData();
            }
        }
        protected void btnSave_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CurrencyEntity currency = new CurrencyEntity();
                currency.M_Currency_Code = Convert.ToInt32(currencyid1.Value);
                currency.M_Currency_Name = txtcurrencyname1.Text.Trim();
                currency.M_Currency_Sname = txtcurrencyshortname1.Text.Trim();


                if (currency.M_Currency_Code != 0)

                    UpdateCurrency(currency);

                LoadData();

            }
        }
       
        #endregion
        #region[Edit button click event]
        protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
        {
            
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Currency_Code = Convert.ToInt32(gvCurrency.DataKeys[gvRow.RowIndex].Value);
            CurrencyEntity currency = GetUserByCurrency(M_Currency_Code);
            // Now set value to modal popup
            currencyid1.Value = currency.M_Currency_Code.ToString();
            txtcurrencyname1.Text = currency.M_Currency_Name;
            txtcurrencyshortname1.Text = currency.M_Currency_Sname;
            mpeCurrency1.Show();
            
        }
        protected void ibtnView_Click(object sender, ImageClickEventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully')", true);

            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Currency_Code = Convert.ToInt32(gvCurrency.DataKeys[gvRow.RowIndex].Value);
            CurrencyEntity currency = GetUserByCurrency(M_Currency_Code);
            // Now set value to modal popup
            currencyid2.Value = currency.M_Currency_Code.ToString();
            viewcurrencyname.Text = currency.M_Currency_Name;
            viewcurrencyshortname.Text = currency.M_Currency_Sname;

            mpeCurrency2.Show();
          

        }
        #endregion
        #region[Delete button event]
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {


            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Currency_Code = Convert.ToInt32(gvCurrency.DataKeys[gvRow.RowIndex].Value);
            string check = "select M_Currency_Code from M_Currency where exists (select M_Country_Currency from M_Country where M_Country_Currency = '"+ M_Currency_Code +"')";
            // delete and hide the row from grid view

          
                using (SqlConnection conn = new SqlConnection())
                {
                  
                    conn.ConnectionString = _ConnStr;
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = check;
                    cmd.CommandType = System.Data.CommandType.Text;
                    conn.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if(dataReader.HasRows)
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Cannot delete the data')", true);
                        //conditioncheck.Text = "Cannot delete";

                       // conditioncheck.Visible = true;

                        //ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();", true);
                        
                       
                        
                    }
                    else if(DeleteCurrency(M_Currency_Code))
                    {
                        gvRow.Visible = false;
                        
                    }

                    conn.Close();
                }
            
        }
        #endregion

        protected void Search(object sender,ImageClickEventArgs e)
    {
        this.BindGrid();
    }
        private void BindGrid()
        {
            String constr = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;
            using(SqlConnection con = new SqlConnection(constr))
            {
                using(SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT M_Currency_Code,M_Currency_Name,M_Currency_Sname FROM M_Currency WHERE M_Currency_Name LIKE '%'+@M_Currency_Name+'%'";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@M_Currency_Name", txtSearch.Text.Trim());
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        gvCurrency.DataSource = dt;
                        gvCurrency.DataBind();

                    }
                }
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCurrency.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void Refresh(object sender, ImageClickEventArgs e)
        {

            LoadData();
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
    }
}