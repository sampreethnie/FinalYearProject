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
    public class InvoiceDetailsEntity
    {
        public Int32 shipmentnumber { get; set; }
        public DateTime creationdate { get; set; }
        public String customer_M_Company_Name { get; set; }
        public Char delivered { get; set; }
        public Char buyerreceived { get; set; }
    }
    public partial class InvoiceDetails : System.Web.UI.Page
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
            else if (ds1.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "s")
            {

                ButtonBuyer.Visible = false;
               
            }
            
        }
        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        public void LoadData()
        {
            using (SqlConnection con = new SqlConnection())
            {
                con.ConnectionString = _ConnStr;
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select ShipmentDetailsSeller.shipmentnumber,ShipmentDetailsSeller.creationdate,ShipmentDetailsSeller.customer_M_Company_Name,ShipmentDetailsSeller.delivered,ShipmentDeliveryBuyer.buyerreceived from ShipmentDetailsSeller INNER JOIN  ShipmentDeliveryBuyer ON ShipmentDetailsSeller.shipmentnumber = ShipmentDeliveryBuyer.sellershipmentnumber where ShipmentDetailsSeller.delivered='Y' and ShipmentDeliveryBuyer.buyerreceived='Y'";
                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dtable = new DataTable();
                if (con.State == ConnectionState.Closed) con.Open();
                SqlDataReader dreader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dtable.Load(dreader);
                gvInvoiceDetails.DataSource = dtable;
                gvInvoiceDetails.DataBind();


            }
        }

      
            



        }
    }
