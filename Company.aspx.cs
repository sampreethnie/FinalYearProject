﻿using System;
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
    public class CompanyEntity
    {
        public Int32 M_Company_MStateSlno { get; set; }
        public Int32 M_Company_MCountrySlno { get; set; }
        public Int32 M_Company_Slno { get; set; }
        public Int32 M_Company_Currency { get; set; }
        public String M_Company_Name { get; set; }
        public String M_Company_Addr1 { get; set; }
        public String M_Company_Addr2 { get; set; }
        public String M_Company_Landmark { get; set; }
        public String M_Company_PIN { get; set; }
        public String M_Company_TAN { get; set; }
        public String M_Company_PAN { get; set; }
        public String M_Company_URL { get; set; }
        public String M_Subscriber_ContactName { get; set; }
        public String M_Subscriber_MobileNo { get; set; }
        public String M_Subscriber_UserID { get; set; }
    }
    public partial class Company : System.Web.UI.Page
    {
        string constr = ConfigurationManager.ConnectionStrings["crudConnection"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                SqlConnection con = new SqlConnection(constr);


                string str = "select M_Company_Slno,M_Company_BuyerSellerFlag,M_Company_Name, M_Company_Addr1,M_Company_Addr2,M_Company_Landmark,M_Company_PIN,M_Company_City,M_Company_TAN,M_Company_PAN,M_Company_URL,M_Company_MCountrySlno,M_Company_MStateSlno,M_Company_Currency,M_Company_City,M_Subscriber_ContactName,M_Subscriber_MobileNo,M_Subscriber_UserID from M_Company,M_Subscriber where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber_MCompanySlno = M_Company_Slno";
                SqlCommand com = new SqlCommand(str, con);


                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(com);

                DataSet ds = new DataSet();
                da.Fill(ds);
                txtuserid.Text = ds.Tables[0].Rows[0]["M_Subscriber_UserID"].ToString();
                txtcompanynamefinal.Text = ds.Tables[0].Rows[0]["M_Company_Name"].ToString();
                txtname.Text = ds.Tables[0].Rows[0]["M_Subscriber_ContactName"].ToString();
                txtmobile.Text = ds.Tables[0].Rows[0]["M_Subscriber_MobileNo"].ToString();
                txtaddress1final.Text = ds.Tables[0].Rows[0]["M_Company_Addr1"].ToString();
                txtaddress2final.Text = ds.Tables[0].Rows[0]["M_Company_Addr2"].ToString();
                txtlandmarkfinal.Text = ds.Tables[0].Rows[0]["M_Company_Landmark"].ToString();
                txtpinfinal.Text = ds.Tables[0].Rows[0]["M_Company_PIN"].ToString();
                txttanfinal.Text = ds.Tables[0].Rows[0]["M_Company_TAN"].ToString();
                txtpanfinal.Text = ds.Tables[0].Rows[0]["M_Company_PAN"].ToString();
                txtcompanyurlfinal.Text = ds.Tables[0].Rows[0]["M_Company_URL"].ToString();
                txtcityfinal.Text = ds.Tables[0].Rows[0]["M_Company_City"].ToString();
                categorylist.SelectedValue = ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString();
                dropdowncountryfinal.SelectedValue = ds.Tables[0].Rows[0]["M_Company_MCountrySlno"].ToString();
                dropdownstatefinal.SelectedValue = ds.Tables[0].Rows[0]["M_Company_MStateSlno"].ToString();
                dropdowncurrencyfinal.SelectedValue = ds.Tables[0].Rows[0]["M_Company_Currency"].ToString();

                SqlConnection connection = new SqlConnection(constr);
                connection.Open();
                SqlCommand commandcurrency = new SqlCommand("select M_Currency_Code,M_Currency_Name from M_Currency", connection);
                SqlDataAdapter sdacurrency = new SqlDataAdapter(commandcurrency);
                DataSet dascurrency = new DataSet();
                sdacurrency.Fill(dascurrency);

                dropdowncurrencyfinal.DataTextField = dascurrency.Tables[0].Columns["M_Currency_Name"].ToString();
                dropdowncurrencyfinal.DataValueField = dascurrency.Tables[0].Columns["M_Currency_Code"].ToString();
                dropdowncurrencyfinal.DataSource = dascurrency.Tables[0];
                dropdowncurrencyfinal.DataBind();


                SqlCommand commandcountry = new SqlCommand("select M_Country_Code,M_Country_Name from M_Country", connection);
                SqlDataAdapter sdacountry = new SqlDataAdapter(commandcountry);
                DataSet dascountry = new DataSet();
                sdacountry.Fill(dascountry);

                dropdowncountryfinal.DataTextField = dascountry.Tables[0].Columns["M_Country_Name"].ToString();
                dropdowncountryfinal.DataValueField = dascountry.Tables[0].Columns["M_Country_Code"].ToString();
                dropdowncountryfinal.DataSource = dascountry.Tables[0];
                dropdowncountryfinal.DataBind();
                dropdowncountryfinal.Items.FindByText(dropdowncountryfinal.SelectedItem.Text).Selected = true;

                SqlCommand commandstate = new SqlCommand("select M_State_Slno,M_State_Name from M_State", connection);
                SqlDataAdapter sdastate = new SqlDataAdapter(commandstate);
                DataSet dasstate = new DataSet();
                sdastate.Fill(dasstate);

                dropdownstatefinal.DataTextField = dasstate.Tables[0].Columns["M_State_Name"].ToString();
                dropdownstatefinal.DataValueField = dasstate.Tables[0].Columns["M_State_Slno"].ToString();
                dropdownstatefinal.DataSource = dasstate.Tables[0];
                dropdownstatefinal.DataBind();
                dropdownstatefinal.Items.FindByText(dropdownstatefinal.SelectedItem.Text).Selected = true;

                connection.Close();
                dropdowncurrencyfinal.Items.Insert(0, new ListItem("--Select Currency--", "0"));
                dropdowncountryfinal.Items.Insert(0, new ListItem("--Select Country--", "0"));
                dropdownstatefinal.Items.Insert(0, new ListItem("--Select State--", "0"));

                Session["buyer"] = false;

                lblusername.Text = "Username:" + Session["M_Subscriber_UserID"];
                SqlConnection con1 = new SqlConnection(constr);
                con1.Open();
                string str1 = "select M_Company_Name,M_Company_BuyerSellerFlag from M_Subscriber,M_Company where M_Subscriber_UserID = '" + Session["M_Subscriber_UserID"] + "' and M_Subscriber.M_Subscriber_MCompanySlno = M_Company.M_Company_Slno";
                SqlCommand com1 = new SqlCommand(str1, con1);
                SqlDataAdapter da1 = new SqlDataAdapter(com1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1);
                lblcompanyname.Text = "CompanyName:" + ds1.Tables[0].Rows[0]["M_Company_Name"].ToString();
                //lblbuyersellerflag.Text = "Type:" + ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString();
                if (ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "b")
                {

                    ButtonSeller.Visible = false;
                   



                }
                else if (ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "s")
                {

                    ButtonBuyer.Visible = false;
                   
                }
                else if (ds.Tables[0].Rows[0]["M_Company_BuyerSellerFlag"].ToString() == "a")
                {
                    ButtonBuyer.Visible = false;
                    ButtonSeller.Visible = false;
                }

            }
        }
     protected void btnlogout_Click(object sender, EventArgs e)
        {
            Session["M_Subscriber_UserID"] = null;
            Response.Redirect("Mainpage.aspx");
        }

        

    }
    }
