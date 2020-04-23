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
    public class CarrierEntity
    {
        public Int32 M_Carrier_Slno { get; set; }
        public String M_Carrier_Name { get; set; }
        public String M_Carrier_Sname { get; set; }
    }
        public partial class Carrier : System.Web.UI.Page
        {
            String _ConnStr = ConfigurationManager.ConnectionStrings["CrudConnection"].ConnectionString;
            protected void Page_Load(object sender, EventArgs e)
            {
                if (!IsPostBack)
                {
                    LoadData();
                }
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
                gvCarrier.Columns[4].Visible = false;
                gvCarrier.Columns[5].Visible = false;



            }
            else if (ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "s")
            {

                ButtonBuyer.Visible = false;
                btnAdd.Visible = false;
                gvCarrier.Columns[4].Visible = false;
                gvCarrier.Columns[5].Visible = false;
            }
            else if (ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "a")
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
                    cmd.CommandText = "select * from M_Carrier order by M_Carrier_Slno";
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandType = System.Data.CommandType.Text;
                    DataTable dTable = new DataTable();
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    SqlDataReader dReader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    dTable.Load(dReader);
                    gvCarrier.DataSource = dTable;
                    gvCarrier.DataBind();
                    
                }
            }
            protected void btnAdd_Click(object sender, ImageClickEventArgs e)
            {
                ClearPopupControls();
                mpeCarrierAddpopup.Show();

            }
            private void ClearPopupControls()
            {
                txtcarriernameAddpopup.Text = String.Empty;
                txtcarriershortnameAddpopup.Text = String.Empty;
            }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        public bool AddNewCarrier(CarrierEntity carrier)
            {
                String insertQuery = @"INSERT INTO [M_Carrier] ([M_Carrier_Name],[M_Carrier_Sname]) VALUES(@M_Carrier_Name, @M_Carrier_Sname)";
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = _ConnStr;
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = insertQuery;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@M_Carrier_Name", carrier.M_Carrier_Name);
                    cmd.Parameters.AddWithValue("@M_Carrier_Sname", carrier.M_Carrier_Sname);
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    cmd.ExecuteNonQuery();

                    return true;
                }
            }
            #region[Update Carrier record into database]
            public bool UpdateCarrier(CarrierEntity carrier)
            {
                String updateQuery = @"UPDATE [M_Carrier] SET [M_Carrier_Name] = @M_Carrier_Name ,[M_Carrier_Sname] = @M_Carrier_Sname Where[M_Carrier_Slno] = @M_Carrier_Slno";
                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = _ConnStr;
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = updateQuery;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@M_Carrier_Slno", carrier.M_Carrier_Slno);
                    cmd.Parameters.AddWithValue("@M_Carrier_Name", carrier.M_Carrier_Name);
                    cmd.Parameters.AddWithValue("@M_Carrier_Sname", carrier.M_Carrier_Sname);


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    cmd.ExecuteNonQuery();

                    return true;

                }

            }
            #endregion
            #region[Delete Carrier record from database]
            public bool DeleteCarrier(Int32 M_Carrier_Slno)
            {
                String updateQuery = @"Delete from [M_Carrier] Where [M_Carrier_Slno] = @M_Carrier_Slno";

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = _ConnStr;
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = updateQuery;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@M_Carrier_Slno", M_Carrier_Slno);
                    if (conn.State == ConnectionState.Closed) conn.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
            }
            #endregion
            #region[Get Carrier to show in popup for edit]
            public CarrierEntity GetCarrier(Int32 M_Carrier_Slno)
            {
                String updateQuery = @"Select * From  [M_Carrier]
                            Where [M_Carrier_Slno] = @M_Carrier_Slno";

                using (SqlConnection conn = new SqlConnection())
                {
                    conn.ConnectionString = _ConnStr;
                    SqlCommand cmd = conn.CreateCommand();
                    cmd.CommandText = updateQuery;
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.Parameters.AddWithValue("@M_Carrier_Slno", M_Carrier_Slno);


                    if (conn.State == ConnectionState.Closed) conn.Open();
                    SqlDataReader dataReader =
                    cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    DataTable dataTable = new DataTable();
                    dataTable.Load(dataReader);
                    CarrierEntity existingcarrier = new CarrierEntity();
                    foreach (DataRow row in dataTable.Rows)
                    {
                        existingcarrier.M_Carrier_Slno = Convert.ToInt32(row["M_Carrier_Slno"]);
                        existingcarrier.M_Carrier_Name = Convert.ToString(row["M_Carrier_Name"]);
                        existingcarrier.M_Carrier_Sname = Convert.ToString(row["M_Carrier_Sname"]);


                    }

                    return existingcarrier;
                }
            }
            #endregion
            #region[Save User]
            protected void btnSave_Click(object sender, EventArgs e)
            {
                if (Page.IsValid)
                {
                    CarrierEntity carrier = new CarrierEntity();
                    carrier.M_Carrier_Slno = Convert.ToInt32(carrieridAddpopup.Value);
                    carrier.M_Carrier_Name = txtcarriernameAddpopup.Text.Trim();
                    carrier.M_Carrier_Sname = txtcarriershortnameAddpopup.Text.Trim();

                    if (carrier.M_Carrier_Slno == 0)
                        AddNewCarrier(carrier);
                    else
                        UpdateCarrier(carrier);

                    LoadData();
                }
            }
            protected void btnSave_Click1(object sender, EventArgs e)
            {
                if (Page.IsValid)
                {
                    CarrierEntity carrier = new CarrierEntity();
                    carrier.M_Carrier_Slno = Convert.ToInt32(carrieridEditpopup.Value);
                    carrier.M_Carrier_Name = txtcarriernameEditpopup.Text.Trim();
                    carrier.M_Carrier_Sname = txtcarriershortnameEditpopup.Text.Trim();

                    if (carrier.M_Carrier_Slno != 0)
                          
                        UpdateCarrier(carrier);

                    LoadData();
                }
            }
            #endregion
            #region[Edit button click event]
            protected void ibtnEdit_Click(object sender, ImageClickEventArgs e)
            {

                GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
                Int32 M_Carrier_Slno = Convert.ToInt32(gvCarrier.DataKeys[gvRow.RowIndex].Value);
                CarrierEntity carrier = GetCarrier(M_Carrier_Slno);
                // Now set value to modal popup
                carrieridEditpopup.Value = carrier.M_Carrier_Slno.ToString();
                txtcarriernameEditpopup.Text = carrier.M_Carrier_Name;
                txtcarriershortnameEditpopup.Text = carrier.M_Carrier_Sname;
                mpeCarrierEditpopup.Show();

            }
            protected void ibtnView_Click(object sender, ImageClickEventArgs e)
            {
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Successfully')", true);

                GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
                Int32 M_Carrier_Slno = Convert.ToInt32(gvCarrier.DataKeys[gvRow.RowIndex].Value);
                CarrierEntity carrier = GetCarrier(M_Carrier_Slno);
                // Now set value to modal popup
                carrieridViewpopup.Value = carrier.M_Carrier_Slno.ToString();
                viewcarriername.Text = carrier.M_Carrier_Name;
                viewcarriershortname.Text = carrier.M_Carrier_Sname;

                mpeCarrierViewpopup.Show();


            }
            #endregion
            #region[Delete button event]
            protected void ibtnDelete_Click(object sender, ImageClickEventArgs e)
            {
                GridViewRow gvRow = (GridViewRow)((ImageButton)sender).NamingContainer;
                Int32 M_Carrier_Slno = Convert.ToInt32(gvCarrier.DataKeys[gvRow.RowIndex].Value);
                if (DeleteCarrier(M_Carrier_Slno))
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
                        cmd.CommandText = "SELECT M_Carrier_Slno,M_Carrier_Name,M_Carrier_Sname FROM M_Carrier WHERE M_Carrier_Name LIKE '%'+@M_Carrier_Name+'%'";
                        cmd.Connection = con;
                        cmd.Parameters.AddWithValue("@M_Carrier_Name", txtSearch.Text.Trim());
                        DataTable dt = new DataTable();
                        using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                        {
                            sda.Fill(dt);
                            gvCarrier.DataSource = dt;
                            gvCarrier.DataBind();

                        }
                    }
                }
            }
            protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
            {
                gvCarrier.PageIndex = e.NewPageIndex;
                this.BindGrid();
            }
            protected void Refresh(object sender, ImageClickEventArgs e)
            {

                LoadData();
            }
        }
    }
