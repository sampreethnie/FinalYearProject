<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OrderReport(Seller).aspx.cs" Inherits="FinalYearProject.OrderReport_Seller_" %>

<%@Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
 <link type="text/css" rel="stylesheet" href="../css/bootstrap.min.css" />
<link type="text/css" rel="stylesheet" href="../css/fd-common.css" />

<script type="text/javascript" src="../js/jquery-1.11.3.min.js"></script>
<script type="text/javascript" src="../js/bootstrap.min.js"></script>
   
    
    <style type="text/css">
        .modalBackground
        {
            background-color:black;
            filter:alpha(opacity=60);
            opacity:0.6;
        }

    </style>
    </head>
     
<body>
    <form id="form1" runat="server">
        <nav class="navbar1 navbar navbar-default" id="navbarone">
  <div class="container-fluid">
    <div class="navbar-header">
      <a class="navbar-brand" href="#">FreightDeals</a>
    </div>
    <ul class="nav navbar-right pull-right top-nav">
              <li class="dropdown">
    <a href="#" class="dropdown-toggle" class="top-menu pull-right" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><span class="glyphicon glyphicon-user"></span><span class="caret"></span></a>
    <ul class="dropdown-menu" style="width:270px">
    <li>
        <div style="text-align:left">
        <a ><asp:Label ID="lblusername" style="width:30px;" runat="server"/></a>
            </div>
        <br />
        <div style="text-align:left">
        <a><asp:Label ID="lblcompanyname" style="width:30px" runat="server"></asp:Label></a>
            </div>
        <br />
        
      </li>
      <li class="divider"></li>
      <li>
        <a ><asp:Button ID="btnlogout" runat="server" style="width:70px;height:40px;color:black;background-color:orange;border-color:chartreuse;border-radius:inherit" Text="Logout" OnClick="btnlogout_Click"  /></a>
      </li>
                 
			    </ul>
				</li>
				</ul>
  </div>
</nav>

        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <asp:UpdatePanel ID="upn1Users" runat="server">
            <ContentTemplate>

            </ContentTemplate>
        </asp:UpdatePanel>
     

       <nav class="navbar2 navbar navbar-primary" id="navbartwo"> 
        
  <div class="col-xs-12" style="padding: 10px 70px 10px 50px; background-color:SteelBlue ">
            <a href="#" class="btn btn-default" role="button">Home</a>
            <div class="btn-group" style="vertical-align:bottom;">
                <button type="button" class="btn btn-default dropdown-toggle" role="button" data-toggle="dropdown">
                    <span data-bind="label">Master</span>&nbsp;<span class="caret"></span>
                </button>
                <ul class="dropdown-menu" role="menu" style="padding:0px; margin:0px; border-radius:0px;">
                    <li><a href="Company.aspx">Company</a></li>
                  <%--  <li><a href="GeneralMasters/AttributesList.aspx" target="fd_iframe">Attributes</a></li>--%>
                    <li><a href="Airport.aspx">Airports</a></li>
                    <li><a href="Commodity.aspx" >Commodity</a></li>
                    <li><a href="Carrier.aspx">Carrier</a></li>

                    <li><a href="Package.aspx">Package Types</a></li>
                    <li><a href="State.aspx">State</a></li>
                    <li><a href="Country.aspx">Country</a></li>
                    <li><a href="Currency.aspx">Currency</a></li>
                    <li><a href="Charge.aspx">Charge Details</a></li>
                </ul>
            </div>
                              <div class="btn-group" style="vertical-align:bottom;">

    
                    
                                 
                                 <button type="button" id="ButtonBuyer" runat="server" class="btn btn-default dropdown-toggle"  data-toggle="dropdown">
                                     Buyer
    <span class="caret"></span>
    <span class="sr-only">Toggle Dropdown</span>
  </button>
  <ul class="dropdown-menu" role="menu">
    <!-- here is the asp.net link button to make post back -->
          
     <li><a href="RFQ.aspx">RFQ</a></li>
    
    <li><a href="QuoteNegotiation(Buyer).aspx">Quote Negotiation(Buyer)</a></li>
     <li><a href="Orders(Buyer).aspx">Orders(Buyer)</a></li>
      <li><a href="ShipmentDelivery(Buyer).aspx">ShipmentDelivery</a></li>
      <li><a href="InvoiceAudit.aspx">Invoice Verification </a></li>
      <li><a href="OrderReport(Buyer).aspx">Order Report(Buyer)</a></li>
      <li><a href="InvoiceReport(Buyer).aspx"> Invoice Report(Buyer)</a></li>
      <li><a href="RateComparisonReport.aspx"> Rate Comparison Report(Buyer) </a></li>
  </ul>
                                 </div>
       <div class="btn-group" style="vertical-align:bottom;">
                
                                 
                    
                
           <button type="button" id="ButtonSeller" runat="server" class="btn btn-default dropdown-toggle"  data-toggle="dropdown">
      Seller
               <span class="caret"></span>
    <span class="sr-only">Toggle Dropdown</span>
  </button>
  <ul class="dropdown-menu" role="menu">
    <!-- here is the asp.net link button to make post back -->
          
    
    <li><a href="SQ.aspx">SellerQuote</a></li>
      <li><a href="QuoteNegotiation(Seller).aspx">Quote Negotiation(Seller)</a></li>
      <li><a href="Orders(Seller).aspx">Orders(Seller)</a></li>

      <li><a href="ShipmentDetails(Seller).aspx">ShipmentDetails</a></li>
      <li><a href="InvoiceDetails.aspx">Invoice</a></li>
      <li><a href="InvoiceReport.aspx">Invoice Report(Seller)</a></li>
      <li><a href="OrderReport(Seller).aspx"> Order Report(Seller)</a></li>
  </ul>
                                 </div>
        </div> 
             
            </nav>
 

         <nav class="navbar3 navbar navbar-dark bg-primary" style="height:20px;"id="navbarthree"> 
 
    <p class="navbar-text" style="font-size:21px">Order Report(Buyer)</p>

     <div class="input-group topspace5 bottomspace5">
             

        
         <span class="input-group-addon nbrd" style="padding:0px 4px">
 
         
             </span>
                </div>  
           
             
            
           
            
            
        

  
  
</nav>
    <div>
     <label style="font-size:19px;margin-left:20px;margin-right:50px;"> From Date * <br > <asp:TextBox ID = "txtfromorderdate" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="Calendarfromdate" PopupButtonID="imgPopup" runat="server" TargetControlID="txtfromorderdate" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             <asp:RequiredFieldValidator ID="rfvfromdate" ControlToValidate="txtfromorderdate" ValidationGroup="invoicereporting" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
    
    <label style="font-size:19px;margin-left:20px;margin-right:50px;"> To Date * <br > <asp:TextBox ID = "txttoorderdate" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="Calendartodate" PopupButtonID="imgPopup" runat="server" TargetControlID="txttoorderdate"  Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
<%--             <asp:RequiredFieldValidator ID="rfvtodate" ControlToValidate="txttoinvoicedate" ValidationGroup="invoicereporting" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>


        

         <label style="font-size:19px;margin-left:20px;margin-right:50px">Customer* <br> <asp:DropDownList ID="dropdowncustomer"  Height="30px"  Width="160px" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList></label>
            <asp:RequiredFieldValidator ID="rfvcustomer" ControlToValidate="dropdowncustomer" ValidationGroup="invoicereporting" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
    
        
        <br /><br />
        <asp:Button Text="Display Data" OnClick="ExportExcel" runat="server"  style="width:110px;height:40px;margin-left:50px;color:white;background-color:deepskyblue;    border:solid;border-top-left-radius:8px;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px;display:inline" />
         <asp:Button Text="Export" ID="btnexport" OnClick="ExportToExcel" runat="server" style= "width:70px;height:40px;margin-left:50px;color:white; background-color:green; border:solid;border-top-left-radius:8px;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px;display:inline" />
                <asp:Button Text="Cancel" ID="btncancel" OnClick="btnClick_Cancel" runat="server" style= "width:70px;height:40px;margin-left:50px;color:white; background-color:lightcoral; border:solid;border-top-left-radius:8px;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px;display:inline" />

         <br />
        <br />
        <br />
         <asp:GridView ID="OrderGridView" runat="server"   Width="90%" Height="50px"  AllowPaging="true" DataKeyNames="ORD_Number" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                  <AlternatingRowStyle BackColor="White" />
                                                  <RowStyle BackColor="#EFF3FB" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Medium" />
<AlternatingRowStyle BackColor="White" />
<Columns>
  

  


<asp:BoundField DataField="ORD_Number" HeaderText="Order Number"    ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />

         
<asp:BoundField DataField="ORD_Date" HeaderText="Order Date"   ItemStyle-Width="50px" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
   
    <asp:BoundField DataField="ORD_SQ_RFQ_Number" HeaderText="RFQ Number"   ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
<asp:BoundField DataField="ORD_SQ_RFQ_Company" HeaderText="Customer"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
<asp:BoundField DataField="ORD_SQ_RFQ_OriginCountry" HeaderText="Origin Country"   ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />

<asp:BoundField DataField="ORD_SQ_RFQ_DestinationCountry" HeaderText="Destination Country"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left"  />
   <asp:BoundField DataField="ORD_SQ_RFQ_OriginAirport" HeaderText="Origin Airport"   ItemStyle-Width="50px" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
    <asp:BoundField DataField="ORD_SQ_RFQ_DestinationAirport" HeaderText="Destination Airport"   ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
    <asp:BoundField DataField="ORD_SQ_RFQ_NumberofPackages" HeaderText="Number of Packages"   ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="ORD_SQ_RFQ_TotalChwt" HeaderText="Total Ch.Wt"   ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="ORD_SQ_RFQ_Commodity" HeaderText="Commodity"   ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="ORD_SQ_BuyerCurrency" HeaderText="Buyer Currency"   ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
        <asp:BoundField DataField="ORD_SellerCurrency" HeaderText="Seller Currency"   ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />

  
    
</Columns>
                                              </asp:GridView> 




        <br />
        <br />
        
               







         </div>
    </form>
</body>
</html>