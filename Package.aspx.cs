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
    public class PackageEntity
    {
        public Int32 M_Pack_Slno { get; set; }
        public String M_Pack_Name { get; set; }
        public String M_Pack_Sname { get; set; }
    }
    public partial class Package : System.Web.UI.Page
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
                gvPackage.Columns[4].Visible = false;
                gvPackage.Columns[5].Visible = false;



            }
            else if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "s")
            {

                ButtonBuyer.Visible = false;
                btnAdd.Visible = false;
                gvPackage.Columns[4].Visible = false;
                gvPackage.Columns[5].Visible = false;
            }
            else if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "a")
            {
                ButtonBuyer.Visible = false;
                ButtonSeller.Visible = false;
            }

        }
        public void LoadData()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = "select * from M_Pack order by M_Pack_Slno";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dTable = new DataTable();
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTable.Load(dReader);
                gvPackage.DataSource = dTable;
                gvPackage.DataBind();
            }
        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClearPopupControls();
            mpePackageAddpopup.Show();
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        private void ClearPopupControls()
        {
            txtpackagenameAddpopup.Text = String.Empty;
            txtpackageshortnameAddpopup.Text = String.Empty;
        }
        public bool AddNewPackage(PackageEntity package)
        {
            String insertQuery = @"INSERT INTO [M_Pack] ([M_Pack_Name],[M_Pack_Sname]) VALUES(@M_Pack_Name, @M_Pack_Sname)";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = insertQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Pack_Name", package.M_Pack_Name);
                cmd.Parameters.AddWithValue("@M_Pack_Sname", package.M_Pack_Sname);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();

                return true;
            }
        }
        #region[Update Package record into database]
        public bool UpdatePackage(PackageEntity package)
        {
            String updateQuery = @"UPDATE [M_Pack] SET [M_Pack_Name] = @M_Pack_Name ,[M_Pack_Sname] = @M_Pack_Sname Where[M_Pack_Slno] = @M_Pack_Slno";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Pack_Slno", package.M_Pack_Slno);
                cmd.Parameters.AddWithValue("@M_Pack_Name", package.M_Pack_Name);
                cmd.Parameters.AddWithValue("@M_Pack_Sname", package.M_Pack_Sname);


                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();

                return true;

            }

        }
        #endregion
        #region[Delete Package record from database]
        public bool DeletePackage(Int32 M_Pack_Slno)
        {
            String updateQuery = @"Delete from [M_Pack] Where [M_Pack_Slno] = @M_Pack_Slno";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Pack_Slno", M_Pack_Slno);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        #endregion

        #region[Get Package to show in popup for edit]
        public PackageEntity GetPackage(Int32 M_Pack_Slno)
        {
            String updateQuery = @"Select * From  [M_Pack]
                            Where [M_Pack_Slno] = @M_Pack_Slno";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Pack_Slno", M_Pack_Slno);


                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dataReader =
                cmd.ExecuteReader(CommandBehavior.CloseConnection);

                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                PackageEntity existingpackage = new PackageEntity();
                foreach (DataRow row in dataTable.Rows)
                {
                    existingpackage.M_Pack_Slno = Convert.ToInt32(row["M_Pack_Slno"]);
                    existingpackage.M_Pack_Name = Convert.ToString(row["M_Pack_Name"]);
                    existingpackage.M_Pack_Sname = Convert.ToString(row["M_Pack_Sname"]);


                }

                return existingpackage;
            }
        }
        #endregion
        #region[Save User]
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                PackageEntity package = new PackageEntity();
                package.M_Pack_Slno = Convert.ToInt32(packageidAddpopup.Value);
                package.M_Pack_Name = txtpackagenameAddpopup.Text.Trim();
                package.M_Pack_Sname = txtpackageshortnameAddpopup.Text.Trim();

                if (package.M_Pack_Slno == 0)
                    AddNewPackage(package);
                else
                    UpdatePackage(package);

                LoadData();
            }
        }

        protected void btnSave_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                PackageEntity package = new PackageEntity();
                package.M_Pack_Slno = Convert.ToInt32(packageidEditpopup.Value);
                package.M_Pack_Name = txtpackagenameEditpopup.Text.Trim();
                package.M_Pack_Sname = txtpackageshortnameEditpopup.Text.Trim();


                if (package.M_Pack_Slno != 0)

                    UpdatePackage(package);

                LoadData();

            }
        }

        #endregion
        #region[Edit button click event]
        protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Pack_Slno = Convert.ToInt32(gvPackage.DataKeys[gvRow.RowIndex].Value);
            PackageEntity package = GetPackage(M_Pack_Slno);
            // Now set value to modal popup
            packageidEditpopup.Value = package.M_Pack_Slno.ToString();
            txtpackagenameEditpopup.Text = package.M_Pack_Name;
            txtpackageshortnameEditpopup.Text = package.M_Pack_Sname;
            mpePackageEditpopup.Show();

        }
        protected void ibtnView_Click(object sender, ImageClickEventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully')", true);

            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Pack_Slno = Convert.ToInt32(gvPackage.DataKeys[gvRow.RowIndex].Value);
            PackageEntity package = GetPackage(M_Pack_Slno);
            // Now set value to modal popup
            packageidViewpopup.Value = package.M_Pack_Slno.ToString();
            viewpackagename.Text = package.M_Pack_Name;
            viewpackageshortname.Text = package.M_Pack_Sname;

            mpePackageViewpopup.Show();


        }
        #endregion
        #region[Delete button event]
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Pack_Slno = Convert.ToInt32(gvPackage.DataKeys[gvRow.RowIndex].Value);
            if (DeletePackage(M_Pack_Slno))
                gvRow.Visible = false;
        }
        #endregion

        protected void Search(object sender,ImageClickEventArgs e)
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
                    cmd.CommandText = "SELECT M_Pack_Slno,M_Pack_Name,M_Pack_Sname FROM M_Pack WHERE M_Pack_Name LIKE '%'+@M_Pack_Name+'%'";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@M_Pack_Name", txtSearch.Text.Trim());
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        gvPackage.DataSource = dt;
                        gvPackage.DataBind();

                    }
                }
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvPackage.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }
        protected void Refresh(object sender, ImageClickEventArgs e)
        {

            LoadData();
        }
    }
}