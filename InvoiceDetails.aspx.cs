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
