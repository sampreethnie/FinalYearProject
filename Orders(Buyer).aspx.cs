using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.IO;
using System.Windows;

namespace FinalYearProject
{
    public partial class Orders_Buyer_ : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblusername.Text = "Username:" + Session["M_Subscriber_UserID"];
                SqlConnection con1 = new SqlConnection(_ConnStr);
                con1.Open();
                string str = "select M_Company_Slno,M_Company_Name,M_Company_BuyerSellerFlag from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
                SqlCommand com1 = new SqlCommand(str, con1);
                SqlDataAdapter da1 = new SqlDataAdapter(com1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                lblcompanyname.Text = "CompanyName:" + ds1.Tables[0].Rows[0]["M_Company_Name"].ToString();
                Session["CompanySlnosq"] = ds1.Tables[0].Rows[0]["M_Company_Slno"];
                //lblbuyersellerflag.Text = "Type:" + ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString();
                if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "b")
                {
                    ButtonSeller.Visible = false;

                }
                else
                {
                    ButtonBuyer.Visible = false;
                }
                BindGridView();
            }
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        private void BindGridView()
        {
            SqlConnection con1 = new SqlConnection(_ConnStr);

            DataTable dt = new DataTable();
            con1.Open();

            SqlCommand cmd = new SqlCommand("select *from Orders where ORD_BuyerUserID = '" + Session["M_Subscriber_UserID"] + "' ", con1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(dt);
            GridViewOrdersBuyer.DataSource = dt;
            GridViewOrdersBuyer.DataBind();
           

            con1.Close();
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            
            using (SqlConnection con = new SqlConnection(_ConnStr))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Orders WHERE ORD_Date BETWEEN @From AND @To", con))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        cmd.Parameters.AddWithValue("@From", Convert.ToDateTime(this.txtfromdate.Text));
                        cmd.Parameters.AddWithValue("@To", Convert.ToDateTime(this.txttodate.Text));
                        DataSet ds = new DataSet();
                        da.Fill(ds);
                        GridViewOrdersBuyer.DataSource = ds;
                        GridViewOrdersBuyer.DataBind();
                    }
                }
            }
        }
    }
}