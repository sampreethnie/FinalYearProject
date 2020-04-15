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
namespace FinalYearProject
{
    public class ChargeEntity
    {
        public Int32 M_Charge_Code { get; set; }
        public String M_Charge_Name { get; set; }
        public String M_Charge_Sname { get; set; }
    }
    public partial class Charge : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                LoadData();
            }
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
                ButtonSeller.Visible = false;
                

            }
            else
            {
                ButtonBuyer.Visible = false;
               
            }
        }
        public void LoadData()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from M_Charge order by M_Charge_Code";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dTable = new DataTable();
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTable.Load(dReader);
                gvCharge.DataSource = dTable;
                gvCharge.DataBind();

            }
        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClearPopupControls();
            mpeChargeAddpopup.Show();

        }
        private void ClearPopupControls()
        {
            txtchargenameAddpopup.Text = String.Empty;
            txtchargeshortnameAddpopup.Text = String.Empty;
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        public bool AddNewCharge(ChargeEntity charge)
        {
            String insertQuery = @"INSERT INTO [M_Charge] ([M_Charge_Name],[M_Charge_Sname]) VALUES(@M_Charge_Name, @M_Charge_Sname)";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = insertQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Charge_Name", charge.M_Charge_Name);
                cmd.Parameters.AddWithValue("@M_Charge_Sname", charge.M_Charge_Sname);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
        }
        #region[Update Charge record into database]
        public bool UpdateCharge(ChargeEntity charge)
        {
            String updateQuery = @"UPDATE [M_Charge] SET [M_Charge_Name] = @M_Charge_Name ,[M_Charge_Sname] = @M_Charge_Sname Where[M_Charge_Code] = @M_Charge_Code";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Charge_Code", charge.M_Charge_Code);
                cmd.Parameters.AddWithValue("@M_Charge_Name", charge.M_Charge_Name);
                cmd.Parameters.AddWithValue("@M_Charge_Sname", charge.M_Charge_Sname);


                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();

                return true;

            }

        }
        #endregion
        #region[Delete Charge record from database]
        public bool DeleteCharge(Int32 M_Charge_Code)
        {
            String updateQuery = @"Delete from [M_Charge] Where [M_Charge_Code] = @M_Charge_Code";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Charge_Code", M_Charge_Code);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        #endregion
        #region[Get Charge to show in popup for edit]
        public ChargeEntity GetCharge(Int32 M_Charge_Code)
        {
            String updateQuery = @"Select * From  [M_Charge]
                            Where [M_Charge_Code] = @M_Charge_Code";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Charge_Code", M_Charge_Code);


                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dataReader =
                cmd.ExecuteReader(CommandBehavior.CloseConnection);

                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                ChargeEntity existingcharge = new ChargeEntity();
                foreach (DataRow row in dataTable.Rows)
                {
                    existingcharge.M_Charge_Code = Convert.ToInt32(row["M_Charge_Code"]);
                    existingcharge.M_Charge_Name = Convert.ToString(row["M_Charge_Name"]);
                    existingcharge.M_Charge_Sname = Convert.ToString(row["M_Charge_Sname"]);


                }

                return existingcharge;
            }
        }
        #endregion
        #region[Save charge]
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ChargeEntity charge = new ChargeEntity();
                charge.M_Charge_Code = Convert.ToInt32(chargeidAddpopup.Value);
                charge.M_Charge_Name = txtchargenameAddpopup.Text.Trim();
                charge.M_Charge_Sname = txtchargeshortnameAddpopup.Text.Trim();

                if (charge.M_Charge_Code == 0)
                    AddNewCharge(charge);
                else
                    UpdateCharge(charge);

                LoadData();
            }
        }
        protected void btnSave_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                ChargeEntity charge = new ChargeEntity();
                charge.M_Charge_Code = Convert.ToInt32(chargeidAddpopup.Value);
                charge.M_Charge_Name = txtchargenameAddpopup.Text.Trim();
                charge.M_Charge_Sname = txtchargeshortnameAddpopup.Text.Trim();

                if (charge.M_Charge_Code != 0)

                    UpdateCharge(charge);

                LoadData();
            }
        }
        #endregion
        #region[Edit button click event]
        protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Charge_Code = Convert.ToInt32(gvCharge.DataKeys[gvRow.RowIndex].Value);
            ChargeEntity charge = GetCharge(M_Charge_Code);
            // Now set value to modal popup
            chargeidEditpopup.Value = charge.M_Charge_Code.ToString();
            txtchargenameEditpopup.Text = charge.M_Charge_Name;
            txtchargeshortnameEditpopup.Text = charge.M_Charge_Sname;
            mpeChargeEditpopup.Show();

        }
        protected void ibtnView_Click(object sender, ImageClickEventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully')", true);

            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Charge_Code = Convert.ToInt32(gvCharge.DataKeys[gvRow.RowIndex].Value);
            ChargeEntity charge = GetCharge(M_Charge_Code);
            // Now set value to modal popup
            chargeidEditpopup.Value = charge.M_Charge_Code.ToString();
            txtchargenameEditpopup.Text = charge.M_Charge_Name;
            txtchargeshortnameEditpopup.Text = charge.M_Charge_Sname;

            mpeChargeViewpopup.Show();


        }
        #endregion
        #region[Delete button event]
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Charge_Code = Convert.ToInt32(gvCharge.DataKeys[gvRow.RowIndex].Value);
            if (DeleteCharge(M_Charge_Code))
                gvRow.Visible = false;
        }
        #endregion

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
                    cmd.CommandText = "SELECT M_Charge_Code,M_Charge_Name,M_Charge_Sname FROM M_Charge WHERE M_Charge_Name LIKE '%'+@M_Charge_Name+'%'";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@M_Charge_Name", txtSearch.Text.Trim());
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        gvCharge.DataSource = dt;
                        gvCharge.DataBind();

                    }
                }
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCharge.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void Refresh(object sender, ImageClickEventArgs e)
        {

            LoadData();
        }
    }
}
