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
    public class CommodityEntity
    {
        public Int32 M_Commodity_Slno { get; set; }
        public String M_Commodity_Name { get; set; }
        public String M_Commodity_Sname { get; set; }
    }
    public partial class Commodity : System.Web.UI.Page
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
            lblusername.Text = "Username:" + Session["M_Subscriber_UserID"];
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            string str = "select M_Company_Slno,M_Company_Name,M_Company_BuyerSellerFlag from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
            SqlCommand com = new SqlCommand(str, con);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds);
            lblcompanyname.Text = "CompanyName:" + ds.Tables[0].Rows[0]["M_Company_Name"].ToString();
            //lblbuyersellerflag.Text = "Type:" + ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString();
            if (ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "b")
            {

                ButtonSeller.Visible = false;
                btnAdd.Visible = false;
                gvCommodity.Columns[4].Visible = false;
                gvCommodity.Columns[5].Visible = false;



            }
            else if (ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "s")
            {

                ButtonBuyer.Visible = false;
                btnAdd.Visible = false;
                gvCommodity.Columns[4].Visible = false;
                gvCommodity.Columns[5].Visible = false;
            }
            else if (ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "a")
            {
                ButtonBuyer.Visible = false;
                ButtonSeller.Visible = false;
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
                cmd.CommandText = "select * from M_Commodity order by M_Commodity_Slno";
                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dTable = new DataTable();
                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dTable.Load(dReader);
                gvCommodity.DataSource = dTable;
                gvCommodity.DataBind();
            }
        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ClearPopupControls();
            mpeCommodity.Show();
        }
        private void ClearPopupControls()
        {

            txtcommodityname.Text = String.Empty;
            txtcommodityshortname.Text = String.Empty;
        }
        public bool AddNewCommodity(CommodityEntity user)
        {
            String insertQuery = @"INSERT INTO [M_Commodity] ([M_Commodity_Name],[M_Commodity_Sname]) VALUES(@M_Commodity_Name, @M_Commodity_Sname)";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = insertQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Commodity_Name", user.M_Commodity_Name);
                cmd.Parameters.AddWithValue("@M_Commodity_Sname", user.M_Commodity_Sname);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();

                return true;
            }

        }
        #region[Update User record into database]
        public bool UpdateCommodity(CommodityEntity user)
        {
            String updateQuery = @"UPDATE [M_Commodity] SET [M_Commodity_Name] = @M_Commodity_Name ,[M_Commodity_Sname] = @M_Commodity_Sname Where [M_Commodity_Slno] = @M_Commodity_Slno";
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Commodity_Slno", user.M_Commodity_Slno);
                cmd.Parameters.AddWithValue("@M_Commodity_Name", user.M_Commodity_Name);
                cmd.Parameters.AddWithValue("@M_Commodity_Sname", user.M_Commodity_Sname);


                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();

                return true;

            }

        }
        #endregion
        #region[Delete User record from database]
        public bool DeleteCommodity(Int32 M_Commodity_Slno)
        {
            String updateQuery = @"Delete from [M_Commodity] Where [M_Commodity_Slno] = @M_Commodity_Slno";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Commodity_Slno", M_Commodity_Slno);
                if (conn.State == ConnectionState.Closed) conn.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
        }
        #endregion

        #region[Get User to show in popup for edit]
        public CommodityEntity GetUserByCommodity(Int32 M_Commodity_Slno)
        {
            String updateQuery = @"Select * From  [M_Commodity]
                            Where [M_Commodity_Slno] = @M_Commodity_Slno";

            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = _ConnStr;
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = updateQuery;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Parameters.AddWithValue("@M_Commodity_Slno", M_Commodity_Slno);


                if (conn.State == ConnectionState.Closed) conn.Open();
                SqlDataReader dataReader =
                cmd.ExecuteReader(CommandBehavior.CloseConnection);

                DataTable dataTable = new DataTable();
                dataTable.Load(dataReader);
                CommodityEntity existingcommodity = new CommodityEntity();
                foreach (DataRow row in dataTable.Rows)
                {
                    existingcommodity.M_Commodity_Slno = Convert.ToInt32(row["M_Commodity_Slno"]);
                    existingcommodity.M_Commodity_Name = Convert.ToString(row["M_Commodity_Name"]);
                    existingcommodity.M_Commodity_Sname = Convert.ToString(row["M_Commodity_Sname"]);


                }

                return existingcommodity;
            }
        }
        #endregion
        #region[Save User]
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CommodityEntity commodity = new CommodityEntity();
                commodity.M_Commodity_Slno = Convert.ToInt32(commodityid.Value);
                commodity.M_Commodity_Name = txtcommodityname.Text.Trim();
                commodity.M_Commodity_Sname = txtcommodityshortname.Text.Trim();

                if (commodity.M_Commodity_Slno == 0)
                    AddNewCommodity(commodity);
                else
                    UpdateCommodity(commodity);

                LoadData();
            }
        }
        protected void btnSave_Click1(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                CommodityEntity commodity = new CommodityEntity();
                commodity.M_Commodity_Slno = Convert.ToInt32(commodityid1.Value);
                commodity.M_Commodity_Name = txtcommodityname1.Text.Trim();
                commodity.M_Commodity_Sname = txtcommodityshortname1.Text.Trim();


                if (commodity.M_Commodity_Slno != 0)

                    UpdateCommodity(commodity);

                LoadData();

            }
        }

        #endregion
        #region[Edit button click event]
        protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
        {

            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Commodity_Slno = Convert.ToInt32(gvCommodity.DataKeys[gvRow.RowIndex].Value);
            CommodityEntity commodity = GetUserByCommodity(M_Commodity_Slno);
            // Now set value to modal popup
            commodityid1.Value = commodity.M_Commodity_Slno.ToString();
            txtcommodityname1.Text = commodity.M_Commodity_Name;
            txtcommodityshortname1.Text = commodity.M_Commodity_Sname;
            mpeCommodity1.Show();

        }
        protected void ibtnView_Click(object sender, ImageClickEventArgs e)
        {
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully')", true);

            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Commodity_Slno = Convert.ToInt32(gvCommodity.DataKeys[gvRow.RowIndex].Value);
            CommodityEntity commodity = GetUserByCommodity(M_Commodity_Slno);
            // Now set value to modal popup
            commodityid2.Value = commodity.M_Commodity_Slno.ToString();
            viewcommodityname.Text = commodity.M_Commodity_Name;
            viewcommodityshortname.Text = commodity.M_Commodity_Sname;

            mpeCommodity2.Show();


        }
        #endregion
        #region[Delete button event]
        protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            Int32 M_Commodity_Slno = Convert.ToInt32(gvCommodity.DataKeys[gvRow.RowIndex].Value);
            if (DeleteCommodity(M_Commodity_Slno))
                gvRow.Visible = false;
        }
            //GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
            //Int32 M_Commodity_Slno = Convert.ToInt32(gvCommodity.DataKeys[gvRow.RowIndex].Value);
            //string check = "select M_Commodity_Slno from M_Commodity where M_Commodity_Slno = @M_Commodity_Slno";
            //// delete and hide the row from grid view


            //using (SqlConnection conn = new SqlConnection())
            //{

            //    conn.ConnectionString = _ConnStr;
            //    SqlCommand cmd = conn.CreateCommand();
            //    cmd.CommandText = check;
            //    cmd.CommandType = System.Data.CommandType.Text;
            //    conn.Open();
            //    SqlDataReader dataReader = cmd.ExecuteReader();

            //     if (DeleteCommodity(M_Commodity_Slno))
            //    {
            //        gvRow.Visible = false;

            //    }


        
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
                    cmd.CommandText = "SELECT M_Commodity_Slno,M_Commodity_Name,M_Commodity_Sname FROM M_Commodity WHERE M_Commodity_Name LIKE '%'+@M_Commodity_Name+'%'";
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@M_Commodity_Name", txtSearch.Text.Trim());
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                        gvCommodity.DataSource = dt;
                        gvCommodity.DataBind();

                    }
                }
            }
        }
        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvCommodity.PageIndex = e.NewPageIndex;
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