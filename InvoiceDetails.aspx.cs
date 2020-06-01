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
                cmd.CommandText = "select ShipmentRFQ_Number,shipmentnumber,creationdate,customer_M_Company_Name,M_Currency_Name,RFQ_OriginCountry,RFQ_DestinationCountry,RFQ_OriginAirport,RFQ_DestinationAirport,RFQ_TotalGrwt,RFQ_TotalChwt,RFQ_NumberofPackages,sellerdelivered,buyerreceived from ShipmentDeliveryBuyer inner join ShipmentDetailsSeller on ShipmentDeliveryBuyer.sellershipmentnumber = ShipmentDetailsSeller.shipmentnumber inner join RFQ on ShipmentDetailsSeller.ShipmentRFQ_Number = RFQ.RFQ_Number inner join M_Company on RFQ.RFQ_Company = M_Company.M_Company_Slno inner join M_Currency on M_Company.M_Company_Currency = M_Currency.M_Currency_Code   where ShipmentDeliveryBuyer.sellerdelivered='Y' and ShipmentDeliveryBuyer.buyerreceived='Y' and userid = @userid";
                cmd.Parameters.AddWithValue("@userid", Session["M_Subscriber_UserID"]);
                cmd.CommandType = System.Data.CommandType.Text;
                DataTable dtable = new DataTable();
                if (con.State == ConnectionState.Closed) con.Open();
                SqlDataReader dreader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dtable.Load(dreader);
                gvInvoiceDetails.DataSource = dtable;
                gvInvoiceDetails.DataBind();


            }
        }

        protected void gvInvoiceDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {


            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                if (e.Row.Cells[9].Text == "Y")
                {
                    e.Row.Cells[9].Text = "Yes";
                }
                if (e.Row.Cells[9].Text == "N")
                {
                    e.Row.Cells[9].Text = "No";
                }
                if (e.Row.Cells[10].Text == "Y")
                {
                    e.Row.Cells[10].Text = "Yes";

                }
                if (e.Row.Cells[10].Text == "N")
                {
                    e.Row.Cells[10].Text = "No";

                }







            }
        }
    }
}
