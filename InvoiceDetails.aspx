<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceDetails.aspx.cs" Inherits="FinalYearProject.InvoiceDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

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
        <a ><asp:Button ID="btnlogout" runat="server" style="width:70px;height:40px;color:black;background-color:orange;border-color:chartreuse;border-radius:inherit" Text="Logout" OnClick="btnlogout_Click" /></a>
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
 
    <p class="navbar-text" style="font-size:21px">InvoiceDetails</p>

     <div class="input-group topspace5 bottomspace5">
             <asp:TextBox ID="txtSearch" CssClass="pull-right" Width="200px" placeholder="Search" runat="server" />

         

         <span class="input-group-addon nbrd" style="padding:0px 4px">
 
          <asp:ImageButton ID="btnSearch" runat="server" CssClass="pull-right" width="20" Height="20" ImageUrl="/images/search.png"  />
             </span>
                </div>  
           </nav>
     
             
        <!- GridView Code to show invoice details -->
       <%-- <div class="table-responsive col-xs-12 nopadding table-shadow">--%>
        <asp:GridView ID="gvInvoiceDetails" runat="server" AutoGenerateColumns="false" OnRowDataBound="gvInvoiceDetails_RowDataBound"   Width="100%"  EmptyDataText="No records found!!" Height="70px"   AllowPaging="true" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="RFQ Number"  DataField="ShipmentRFQ_Number" />
                <asp:BoundField HeaderText="ShipmentNumber" DataField="shipmentnumber" InsertVisible="false" ReadOnly="true" SortExpression="shipmentnumber"  />
                <asp:BoundField HeaderText="CreationDate" DataField="creationdate" />
                <asp:BoundField HeaderText="Customer" DataField="customer_M_Company_Name" SortExpression="customer_M_Company_Name" />
                <asp:BoundField HeaderText="Customer Currency" DataField="M_Currency_Name"  />
                <asp:BoundField DataField="RFQ_OriginCountry" HeaderText="Origin Country" />
                <asp:BoundField DataField="RFQ_OriginAirport" HeaderText="Origin Airport" />
                <asp:BoundField DataField="RFQ_DestinationCountry" HeaderText="Destination Country" />
                <asp:BoundField DataField="RFQ_DestinationAirport" HeaderText="Destination Airport" />
                <asp:BoundField HeaderText="Delivered" DataField="sellerdelivered"  />
                <asp:BoundField HeaderText="Received" DataField="buyerreceived" />
                <asp:HyperLinkField Text="View" DataNavigateUrlFields="shipmentnumber,ShipmentRFQ_Number,customer_M_Company_Name,RFQ_OriginCountry,RFQ_OriginAirport,RFQ_DestinationCountry,RFQ_DestinationAirport,RFQ_TotalGrwt,RFQ_TotalChwt,RFQ_NumberofPackages,sellerdelivered,buyerreceived,M_Currency_Name,creationdate" DataNavigateUrlFormatString="InvoiceDisplay.aspx?shipmentnumber={0}&ShipmentRFQ_Number={1}&customer_M_Company_Name={2}&RFQ_OriginCountry={3}&RFQ_OriginAirport={4}&RFQ_DestinationCountry={5}&RFQ_DestinationAirport={6}&RFQ_TotalGrwt={7}&RFQ_TotalChwt={8}&RFQ_NumberofPackages={9}&sellerdelivered={10}&buyerreceived={11}&M_Currency_Name={12}&creationdate={13:d}" />
             
              
            </Columns>
            <EmptyDataRowStyle CssClass="gvEmpty"></EmptyDataRowStyle>
            <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
            <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
            <SortedAscendingCellStyle BackColor="#FDF5AC" />
            <SortedAscendingHeaderStyle BackColor="#4D0000" />
            <SortedDescendingCellStyle BackColor="#FCF6C0" />
            <SortedDescendingHeaderStyle BackColor="#820000" />
        </asp:GridView> 
            <%--</div>  
            --%>
      
    </form>
</body>
</html>
