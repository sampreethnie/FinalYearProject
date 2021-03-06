﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuoteNegotiation(Buyer).aspx.cs" Inherits="FinalYearProject.QuoteNegotiation_Buyer_" %>

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
      <a class="navbar-brand" href="#" style="font-family:'Times New Roman', Times, serif;font-size:medium;color:blue;font-style:italic">AirFreightPro</a>
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
 
    <p class="navbar-text" style="font-size:21px">Quote Negotiation(Buyer)</p>
             </nav>
            
            <div>
            
            
                <label style="font-size:19px;margin-left:20px;margin-right:50px">Select your RFQ Number <br> <asp:DropDownList ID="dropdownrfq" OnSelectedIndexChanged="dropdownrfq_SelectedIndexChanged" Height="29px"  Width="160px" runat="server" CssClass="form-control"  AutoPostBack="true" ></asp:DropDownList></label>
           <br /><br />     
               <asp:TextBox ID="txtqnblslno"  CssClass="form-control" Width="145px" runat="server" Visible="false"></asp:TextBox>
    <br /><br /><br />

                 <label style="font-size:17px">Total Count:</label>
                <asp:Label ID="lbltotalcount" runat="server" Font-Bold="true" Font-Size="17px"></asp:Label>
                
                 
                 
                
                <asp:GridView ID="GridViewQnb" OnSelectedIndexChanged="GridViewQnb_SelectedIndexChanged"  runat="server" OnRowDataBound="GridViewQnb_RowDataBound" AutoGenerateColumns="false" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center" DataKeyNames="SQ_Slno"  CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Vertical" Height="100px" Width="100%" AllowCustomPaging="True" AllowSorting="True" BorderWidth="2px" Font-Bold="True" Font-Names="Times New Roman" >
                    
                    <AlternatingRowStyle BackColor="White" />
                                                  <RowStyle BackColor="#EFF3FB" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Medium" />
<AlternatingRowStyle BackColor="White" />
                    
                    <Columns>

                        <asp:CommandField HeaderText="Select"  ShowSelectButton="true" />
                        

   
                        <asp:TemplateField HeaderText="Edit">
<ItemTemplate>
<asp:ImageButton ID="imgbtnqnb" ImageUrl="/images/edit1.png" runat="server" Width="25" Height="25" OnClick="ImageButtonqnb_Click" />
    </ItemTemplate>
    </asp:TemplateField>
                        
                        <asp:BoundField DataField="SQ_Slno" HeaderText="Slno" />
                        <asp:BoundField DataField="SQ_RFQ_Number" HeaderText="RFQ Number" />
                       
                       <asp:BoundField DataField="SQ_RFQ_CreationDate" HeaderText="CreationDate" />
                         <asp:BoundField DataField="SQ_RFQ_OriginCountry" HeaderText="Origin Country" />
                         <asp:BoundField DataField="SQ_RFQ_DestinationCountry" HeaderText="Destination Country" />
                        <asp:BoundField DataField="SQ_RFQ_OriginAirport" HeaderText="Origin Airport" />
                        <asp:BoundField DataField="SQ_RFQ_DestinationAirport" HeaderText="Destination Airport" />
                        <asp:BoundField DataField="SQ_RFQ_NumberofPackages" HeaderText="Number of Packages" />
                        <asp:BoundField DataField="SQ_RFQ_TotalGrwt" HeaderText="Gr.Wt(kg)" />
                        <asp:BoundField DataField="SQ_RFQ_TotalVolwt" HeaderText="Vol.Wt(Kg)" />
                        <asp:BoundField DataField="SQ_RFQ_TotalChwt" HeaderText="Ch.Wt(kg)" />
                        <asp:BoundField DataField="SQ_RFQ_PickupAddress" HeaderText="Pickup Address" />
                        <asp:BoundField DataField="SQ_RFQ_DeliveryAddress" HeaderText="Delivery Address" />
                        <asp:BoundField DataField="SQ_RFQ_PickupDate" HeaderText="Pickup Date" />
                        <asp:BoundField DataField="SQ_RFQ_ReqTT" HeaderText="Required T/T" />
                        <asp:BoundField DataField="SQ_RFQ_QuoteDueBy" HeaderText="Quote DueBy" />
                        <asp:BoundField DataField="SQ_RFQ_Commodity" HeaderText="Commodity" />
                        <asp:BoundField DataField="SQ_RFQ_HandlingInfo" HeaderText="Handling Info" />
                         <asp:BoundField DataField="M_Company_Name" HeaderText="SellerCompanyName" />
                         <asp:BoundField DataField="SQ_OfferPrice" HeaderText="OfferPrice" />
                        <asp:BoundField DataField="SQ_RFQ_ExpectedPrice" HeaderText="ExpectedPrice" />
                        <asp:BoundField DataField="M_Currency_Name" HeaderText="Seller Currency" />
                        <asp:BoundField DataField="SQ_UserID" HeaderText="UserID"  />
                       
                        <asp:BoundField DataField="SQ_Timestamp" HeaderText="Timestamp" />
                       
                       
                        <asp:BoundField DataField="SQ_Submit" HeaderText="Submitted?"  />
                        <asp:BoundField DataField="SQ_BuyerCurrency" HeaderText="Buyer Currency" />
                        <asp:BoundField DataField="SQ_RFQ_Company" HeaderText="Buyer Company" />
                        <asp:BoundField DataField="SQ_OrderStatus" HeaderText="Order Status"  />
                        <asp:BoundField DataField="ORD_BuyerUserID" HeaderText="BuyerID" Visible="false" />
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
<asp:TextBox ID="txtofferprice" runat="server" ReadOnly="true" style="width:180px"/>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Expected Price
</td>
<td>
    <asp:TextBox ID="txtexpectedprice" runat="server" style="width:180px"/>
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
        <br /> <br />
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> SQ Slno <br> <asp:TextBox ID="txtsqslno" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="rfvsqslno" ControlToValidate="txtsqslno" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
           
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> Creation Date <br> <asp:TextBox ID="txtcreationdate" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtcreationdate" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> RFQ Number <br> <asp:TextBox ID="txtrfqnumber" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtrfqnumber" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> Origin Country <br> <asp:TextBox ID="txtorigincountry" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtorigincountry" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> Destination Country <br> <asp:TextBox ID="txtdestinationcountry" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtdestinationcountry" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        <br /><br />
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> Origin Airport <br> <asp:TextBox ID="txtoriginairport" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtoriginairport" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        <label style="font-size:19px;margin-left:20px;margin-right:50px"> Destination Airport <br> <asp:TextBox ID="txtdestinationairport" Width="150px" runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtdestinationairport" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        
          <label style="font-size:19px;margin-left:20px;margin-right:37px"> No of Packages <br> <asp:TextBox ID="txtnumberofpackages" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtnumberofpackages" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> Total GrWt (Kg) <br> <asp:TextBox ID="txtgrossweight" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtgrossweight" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
        <br />
        <br />
          <label style="font-size:19px;margin-left:20px;margin-right:59px"> Total VolWt (Kg) <br> <asp:TextBox ID="txtvolumetricweight" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtvolumetricweight" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Total ChWt (Kg)<br> <asp:TextBox ID="txtchargeableweight" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtchargeableweight" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Pickup Addr <br> <asp:TextBox ID="txtpickupaddress" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtpickupaddress" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> DeliveryAddr <br> <asp:TextBox ID="txtdeliveryaddress" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtdeliveryaddress" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
       <br /> <br />
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Pickup Date <br> <asp:TextBox ID="txtpickupdate" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtpickupdate" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Req T/T <br> <asp:TextBox ID="txttransittime" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txttransittime" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Quote Due By <br> <asp:TextBox ID="txtquotedueby" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtquotedueby" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Commodity <br> <asp:TextBox ID="txtcommodity" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtcommodity" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

        <br /> <br />
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Handling Info <br> <asp:TextBox ID="txthandlinginfo" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="txthandlinginfo" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Seller Company <br> <asp:TextBox ID="txtsellercompany" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="txtsellercompany" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Offer Price <br> <asp:TextBox ID="txtofferprice1" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txtofferprice1" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Expected Price <br> <asp:TextBox ID="txtexpectedprice1" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" ControlToValidate="txtexpectedprice1" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>

        <br /><br />
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Seller Currency <br> <asp:TextBox ID="txtsellercurrency" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="txtsellercurrency" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Seller Mail <br> <asp:TextBox ID="txtsellermail" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" ControlToValidate="txtsellermail" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Buyer Currency <br> <asp:TextBox ID="txtbuyercurrency" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="txtbuyercurrency" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Buyer Company <br> <asp:TextBox ID="txtbuyercompany" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" ControlToValidate="txtbuyercompany" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
         <br /><br /> <br />
        <asp:Button ID="btnorder" Text="Order" CausesValidation="true" ValidationGroup="addorderbuyer"  runat="server"  OnClick="btnorder_Click1"  OnClientClick="return confirm('Are you sure to place your Order?')"   style="background-color:salmon;display:inline-block;color:white;border-bottom-left-radius:7px; margin-left:40px;margin-right:70px;width:100px;height:40px;border-bottom-right-radius:7px;border-top-left-radius:7px;border-top-right-radius:7px" />
        <asp:Button ID="btncancel1" Text="Cancel"  runat="server" OnClick="btncancel1_Click" style="background-color:yellow;color:black;border-bottom-left-radius:7px; margin-left:40px ;width:100px;height:40px; display:inline-block;border-bottom-right-radius:7px;border-top-left-radius:7px;border-top-right-radius:7px" />
    </form>
</body>
</html>
