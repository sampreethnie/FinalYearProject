<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuoteNegotiation(Seller).aspx.cs" Inherits="FinalYearProject.QuoteNegotiation_Seller_" %>

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
     <script type="text/javascript">
        //this script will get the date selected from the given calendarextender (ie: "sender") and append the
        //current time to it.
        function AppendTime(sender, args) {
            var selectedDate = new Date();
            selectedDate = sender.get_selectedDate();
            var now = new Date();
            sender.get_element().value = selectedDate.format("dd/MM/yyyy") + " " + now.format("hh:mm tt");
        }
    </script>
   
   
    
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
        <a ><asp:Button ID="btnlogout" runat="server" style="width:70px;height:40px;color:black;background-color:orange;border-color:chartreuse;border-radius:inherit" Text="Logout" OnClick="btnlogout_Click"   /></a>
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
   
  </ul>
                                 </div>
        </div> 
             
            </nav>
  

         <nav class="navbar3 navbar navbar-dark bg-primary" style="height:20px;"id="navbarthree"> 
 
    <p class="navbar-text" style="font-size:21px">Quote Negotiation(Seller)</p>
             </nav>
            
            <div>
            
            
                <label style="font-size:19px;margin-left:20px;margin-right:50px">Select your RFQ Number <br> <asp:DropDownList ID="dropdownrfq" OnSelectedIndexChanged="dropdownrfq_SelectedIndexChanged" Height="29px"  Width="160px" runat="server" CssClass="form-control"  AutoPostBack="true" ></asp:DropDownList></label>
           <br /><br />     
               <asp:TextBox ID="txtqnblslno"  CssClass="form-control" Width="145px" runat="server" Visible="false"></asp:TextBox>
    <br /><br /><br />

                 <label style="font-size:17px">Total Count:</label>
                <asp:Label ID="lbltotalcount" runat="server" Font-Bold="true" Font-Size="17px"></asp:Label>
                
                 <asp:TextBox ID="txtSearch" CssClass="pull-right" Width="200px" placeholder="Search" runat="server" />
                <asp:ImageButton ID="btnSearch" runat="server" CssClass="pull-right" style="margin-left:5px" width="20" Height="20" ImageUrl="/images/search.png"   /> 

                 <asp:ImageButton ID="btnRefresh" runat="server" CssClass="pull-right" width="20" Height="20" ImageUrl="/images/refresh.png"   />
                 
                
                <asp:GridView ID="GridViewQnb"  runat="server" AutoGenerateColumns="false" OnRowDataBound="GridViewQnb_RowDataBound" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" DataKeyNames="SQ_Slno"  CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Vertical" Height="100px" Width="100%" AllowCustomPaging="True" AllowSorting="True" BorderWidth="2px" Font-Bold="True" Font-Names="Times New Roman" >
                    
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    
                    <Columns>
                        
                        <asp:TemplateField HeaderText="Edit">
<ItemTemplate>
<asp:ImageButton ID="imgbtnqnb" ImageUrl="/images/edit1.png" runat="server" Width="25" Height="25" OnClick="ImageButtonqnb_Click" />
    </ItemTemplate>
    </asp:TemplateField>
                        
                        <asp:BoundField DataField="SQ_Slno" HeaderText="SQ_Slno"    />
                        <asp:BoundField DataField="SQ_RFQ_Number" HeaderText="RFQ Number" />
                       
                       <asp:BoundField DataField="SQ_RFQ_CreationDate" HeaderText="CreationDate" />
                         <asp:BoundField DataField="SQ_RFQ_OriginCountry" HeaderText="Origin Country" />
                         <asp:BoundField DataField="SQ_RFQ_DestinationCountry" HeaderText="Destination Country" />
                        <asp:BoundField DataField="SQ_RFQ_OriginAirport" HeaderText="Origin Airport" />
                        <asp:BoundField DataField="SQ_RFQ_DestinationAirport" HeaderText="Destination Airport" />
                        <asp:BoundField DataField="SQ_RFQ_NumberofPackages" HeaderText="Number of Packages" />
                        <asp:BoundField DataField="SQ_RFQ_TotalGrwt" HeaderText="Gross Weight" />
                        <asp:BoundField DataField="SQ_RFQ_TotalVolwt" HeaderText="Volumetric Weight" />
                        <asp:BoundField DataField="SQ_RFQ_TotalChwt" HeaderText="Chargeable Weight" />
                        <asp:BoundField DataField="SQ_RFQ_PickupAddress" HeaderText="Pickup Address" />
                        <asp:BoundField DataField="SQ_RFQ_DeliveryAddress" HeaderText="Delivery Address" />
                        <asp:BoundField DataField="SQ_RFQ_PickupDate" HeaderText="Pickup Date" />
                        <asp:BoundField DataField="SQ_RFQ_ReqTT" HeaderText="Required T/T" />
                        <asp:BoundField DataField="SQ_RFQ_QuoteDueBy" HeaderText="Quote DueBy" />
                        <asp:BoundField DataField="SQ_RFQ_Commodity" HeaderText="Commodity" />
                        <asp:BoundField DataField="SQ_RFQ_HandlingInfo" HeaderText="Handling Info" />
                         <asp:BoundField DataField="SQ_RFQ_Company" HeaderText="Buyer CompanyName" />
                         <asp:BoundField DataField="SQ_OfferPrice" HeaderText="OfferPrice" />
                        <asp:BoundField DataField="SQ_RFQ_ExpectedPrice" HeaderText="ExpectedPrice" />
                        <asp:BoundField DataField="SQ_BuyerCurrency" HeaderText="Buyer Currency" />
                        <asp:BoundField DataField="SQ_UserID" HeaderText="UserID" Visible="false" />
                       
                        <asp:BoundField DataField="SQ_Timestamp" HeaderText="Timestamp" Visible="false" />
                       
                       
                        <asp:BoundField DataField="SQ_Submit" HeaderText="Submitted?" Visible="false" />
                       <asp:BoundField DataField="SQ_OrderStatus" HeaderText="Order Status" />
                    </Columns>
                    
                    <EditRowStyle BackColor="#999999" />
                    
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    
                </asp:GridView>  
                 

        </div>
        <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mpeqnb" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
 BackgroundCssClass="modalBackground" CancelControlID="btnCancel">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="200px" Width="503px" style="display:none">
<table width="100%" style="border:Solid 1.5px #00ff21;border-bottom:solid 2px #00ff21; border-bottom-color:#00ff21;  width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr style="background-color:blue">
<td colspan="2" style=" height:10%; color:White; font-weight:bold; font-size:xx-large" align="left">Edit Price</td>
    
</tr>
    <tr>
        <td>
            <asp:Label ID="lblslno" runat="server" Visible="false" />
        </td>
    </tr>
<tr>
<td align="left" style="font-size:19px">
Offer Price:
</td>
<td>
<asp:TextBox ID="txtofferprice" runat="server"  style="width:180px"/>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Expected Price
</td>
<td>
    <asp:TextBox ID="txtexpectedprice" runat="server" ReadOnly="true" style="width:180px"/>
</td>
</tr>
    <tr>
        <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary"  style="width:70px;background-color:goldenrod;height:40px;margin-left:50px;margin-right:50px;color:white;border:solid;border-top-left-radius:8px;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px" Text="Submit" OnClick="btnSubmit_Click" />
                </td>
    <td>
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger"  style="width:70px;height:40px;margin-left:50px;color:white;border:solid;border-top-left-radius:8px;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px" Text="Cancel" />
                </td>
        
            </tr>
</table>
</asp:Panel>
                </form>
</body>
</html>