﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;

namespace FinalYearProject
{
    public partial class OrderReport_Buyer_ : System.Web.UI.Page
    {
        String _ConnStr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                SqlConnection concompany = new SqlConnection(_ConnStr);
                concompany.Open();
                SqlCommand comcompany = new SqlCommand("select distinct ORD_SQ_Company from Orders where ORD_BuyerUserID = @buyeruserid ", concompany);
                comcompany.Parameters.AddWithValue("@buyeruserid", Session["M_Subscriber_UserID"]);
                SqlDataAdapter dacompany = new SqlDataAdapter(comcompany);
                DataSet dscompany = new DataSet();
                dacompany.Fill(dscompany);
                dropdownseller.DataTextField = dscompany.Tables[0].Columns["ORD_SQ_Company"].ToString();
                dropdownseller.DataSource = dscompany.Tables[0];
                dropdownseller.DataBind();
                concompany.Close();
                dropdownseller.Items.Insert(0, new ListItem("--Select Company--"));
                btnexport.Visible = false;

                lblusername.Text = "Username:" + Session["M_Subscriber_UserID"];
                SqlConnection con2 = new SqlConnection(_ConnStr);
                con2.Open();
                string str = "select M_Company_Slno,M_Company_Name,M_Company_BuyerSellerFlag from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
                SqlCommand com1 = new SqlCommand(str, con2);
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

        }

        protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }
        private void BindGridView()
        {
            SqlConnection con = new SqlConnection(_ConnStr);
            con.Open();
            
            
                SqlCommand cmd = new SqlCommand("select ORD_Number,ORD_Date,ORD_SQ_RFQ_Number,ORD_SQ_Company,ORD_SQ_RFQ_OriginCountry,ORD_SQ_RFQ_DestinationCountry,ORD_SQ_RFQ_OriginAirport,ORD_SQ_RFQ_DestinationAirport,ORD_SQ_RFQ_NumberofPackages,ORD_SQ_RFQ_TotalChwt,ORD_SQ_RFQ_Commodity,ORD_SQ_BuyerCurrency,ORD_SellerCurrency from Orders where ORD_Date between @fromdate and @todate and ORD_SQ_Company = @company", con);
                cmd.Parameters.AddWithValue("@company", dropdownseller.SelectedItem.Text);
                cmd.Parameters.AddWithValue("@fromdate", Convert.ToDateTime(txtfromorderdate.Text));
                cmd.Parameters.AddWithValue("@todate", Convert.ToDateTime(txttoorderdate.Text));
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                OrderGridView.DataSource = ds;
                OrderGridView.DataBind();
                con.Close(); 
        }
        protected void ExportExcel(object sender, EventArgs e)
        {

            BindGridView();

            btnexport.Visible = true;


        }
       
        

        protected void ExportToExcel(object sender, EventArgs e)
        {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=orderreports.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            using (StringWriter sw = new StringWriter())
            {
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                OrderGridView.AllowPaging = false;
                this.BindGridView();

                OrderGridView.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in OrderGridView.HeaderRow.Cells)
                {
                    cell.BackColor = OrderGridView.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in OrderGridView.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = OrderGridView.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = OrderGridView.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                OrderGridView.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style> .textmode { } </style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            /* Verifies that the control is rendered */
        }

        protected void btnClick_Cancel(object sender, EventArgs e)
        {
            txtfromorderdate.Text = string.Empty;
            txttoorderdate.Text = string.Empty;
            dropdownseller.SelectedIndex = 0;
        }

    }
}