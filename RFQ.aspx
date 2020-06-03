<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RFQ.aspx.cs" Inherits="FinalYearProject.RFQ" %>

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
    <script type="text/javascript" src="../js/jquery-1.11.3.min.js">
       <script>
        $(document).ready(function() {
        var maxlen = 8;
        $('#txtrfqnumber').keypress(function(k) {
        if($(this).val().length>=maxlen)
        {
        k.preventDefault();
        alert('character count' +maxlen);
        }
        });

        });
  
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
 
    <p class="navbar-text" style="font-size:21px">Request for Quote(RFQ)</p>
             </nav>
            
            <div >
            
            <asp:label ID="lblrfqslno" runat="server" Visible="false">Slno</asp:label>
            <label for="txtrfq" style="font-size:19px;margin-left:20px;margin-right:50px"> RFQ Number* <br> <asp:TextBox ID="txtrfqnumber" TextMode="Number" MaxLength="2"  CssClass="form-control" Width="145px" runat="server"></asp:TextBox> </label>
           <asp:RequiredFieldValidator ID="rfvrfqnumber" ControlToValidate="txtrfqnumber" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            <label style="font-size:19px;margin-left:20px;margin-right:50px;"> Creation Date * <br > <asp:TextBox ID="txtcreationdate"  CssClass="form-control" TextMode="DateTime" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="Calendarcreationdate" PopupButtonID="imgPopup" runat="server" TargetControlID="txtcreationdate" OnClientDateSelectionChanged="AppendTime" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             <asp:RequiredFieldValidator ID="rfvcreationdate" ControlToValidate="txtcreationdate" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            <label style="font-size:19px;margin-left:20px;margin-right:50px">Origin Country* <br> <asp:DropDownList ID="dropdownorigincountry" Height="29px"  Width="160px" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList></label>
            <asp:RequiredFieldValidator ID="rfvorigincountry" ControlToValidate="dropdownorigincountry" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
               <label style="font-size:19px;margin-left:20px;margin-right:50px">Destination Country* <br> <asp:DropDownList ID="dropdowndestinationcountry" Height="29px"  Width="160px" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList></label>
            <asp:RequiredFieldValidator ID="rfvdestinationcountry" ControlToValidate="dropdowndestinationcountry" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator> 
                
                <br /> <br />
                 <label style="font-size:19px;margin-left:20px;margin-right:50px">Origin Airport* <br> <asp:DropDownList ID="dropdownoriginairport" Height="29px"  Width="147px" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList></label>
            <asp:RequiredFieldValidator ID="rfvoriginairport" ControlToValidate="dropdownoriginairport" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
               <label style="font-size:19px;margin-left:20px;margin-right:32px">Destination Airport* <br> <asp:DropDownList ID="dropdowndestinationairport" Height="29px"  Width="145px" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList></label>
            <asp:RequiredFieldValidator ID="rfvdestinationairport" ControlToValidate="dropdowndestinationairport" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator> 
                
           <label style="font-size:19px;margin-left:15px;margin-right:38px"> Number of Packages* <br> <asp:TextBox ID="txtnoofpackages" TextMode="Number" MaxLength="6" runat="server" Width="157px"></asp:TextBox> </label>
                <asp:RequiredFieldValidator ID="rfvnoofpackages" ControlToValidate="txtnoofpackages" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
               
                 
                 
                 <label style="font-size:19px;margin-left:14px;margin-right:38px"> Gross Weight(Kg)* <br> <asp:TextBox ID="txtgrossweight" TextMode="Number" MaxLength="8"  runat="server" onkeyup="WeightCalculation()"  Width="157px"></asp:TextBox> </label>
                <asp:RequiredFieldValidator ID="rfvgrossweight" ControlToValidate="txtgrossweight" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                
                <br /> <br />
                <label style="font-size:19px;margin-left:20px;margin-right:36px"> Vol.Weight(Kg)* <br> <asp:TextBox ID="txtvolumetricweight"   runat="server" TextMode="Number" onkeyup="WeightCalculation()"  Width="145px"></asp:TextBox> </label>
                <asp:RequiredFieldValidator ID="rfvvolumetricweight" ControlToValidate="txtvolumetricweight" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                
                <label style="font-size:19px;margin-left:37px;margin-right:52px"> Ch.Weight(Kg)* <br> <asp:TextBox ID="txtchargeableweight" runat="server" onkeyup="WeightCalculation()"  Width="142px"></asp:TextBox> </label>
                <asp:RequiredFieldValidator ID="rfvchargeableweight" ControlToValidate="txtchargeableweight" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                
             <label style="font-size:19px;margin-left:20px;margin-right:51px"> PickupAddress* <br> <asp:TextBox ID="txtpickupaddress" Width="158px"  runat="server"></asp:TextBox> </label>
            <asp:RequiredFieldValidator ID="rfvpickupaddress" ControlToValidate="txtpickupaddress" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
             
                <label style="font-size:19px;margin-left:20px;margin-right:55px"> DeliveryAddress* <br> <asp:TextBox ID="txtdeliveryaddress" Width="159px"  runat="server"></asp:TextBox> </label>
            <asp:RequiredFieldValidator ID="rfvdeliveryaddress" ControlToValidate="txtdeliveryaddress" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                
                
            
                <br /> <br />
                <label style="font-size:19px;margin-left:20px;margin-right:71px"> Pickup Date * <br > <asp:TextBox ID="txtpickupdate" TextMode="DateTime" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="Calendarpickupdate" PopupButtonID="imgPopup" runat="server" TargetControlID="txtpickupdate" OnClientDateSelectionChanged="AppendTime" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             <asp:RequiredFieldValidator ID="rfvpickupdate" ControlToValidate="txtpickupdate" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <label style="font-size:19px;margin-right:28px"> Required T/T(Days)* <br> <asp:TextBox ID="txttransittime" Width="142px" TextMode="Number" MaxLength="4" runat="server"></asp:TextBox> </label>
           <asp:RequiredFieldValidator ID="rfvtransittime" ControlToValidate="txttransittime" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
              
                 <label style="font-size:19px;margin-left:15px;margin-right:52px"> Quote Due By * <br > <asp:TextBox ID="txtquotedueby" TextMode="DateTime"  CssClass="form-control" Width="159px" runat="server" ></asp:TextBox> </label>

                <ajaxToolkit:CalendarExtender ID="CalendarExtenderquoutedueby" PopupButtonID="imgPopup" runat="server" TargetControlID="txtquotedueby" OnClientDateSelectionChanged="AppendTime" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
                <asp:RequiredFieldValidator ID="rfvquotedueby" ControlToValidate="txtquotedueby" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <label style="font-size:19px;margin-left:20px;margin-right:50px">Commodity* <br> <asp:DropDownList ID="dropdowncommodity" Height="29px"  Width="157px" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList></label>
            <asp:RequiredFieldValidator ID="rfvcommodity" ControlToValidate="dropdowncommodity" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br /> <br />
                 <label style="font-size:19px;margin-left:20px;margin-right:59px"> Handling Info <br> <asp:TextBox ID="txthandlinginfo" Width="145px"  runat="server"></asp:TextBox> </label>
                 <label style="font-size:19px;margin-left:20px;margin-right:59px"> ExpectedPrice <br> <asp:TextBox ID="txtexpectedprice" Width="145px" TextMode="Number"  runat="server"></asp:TextBox> </label>
                  <asp:RequiredFieldValidator ID="rfvexpectedprice" ControlToValidate="txtexpectedprice" ValidationGroup="addrfq" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                
                <br />
                <br />
                <asp:Label ID="lblvalidaterfq" runat="server"></asp:Label>
           
            
             <br /> <br /> <br /> <br />  
           
            <asp:Button ID="Addbutton" CausesValidation="true" ValidationGroup="addrfq" Style="margin-left:20px;margin-right:20px; display:inline-block;  border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Add" runat="server" font-size="Medium" BackColor="lightblue" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Addbutton_Click"  />
            <asp:Button ID="Updatebutton" CausesValidation="true" ValidationGroup="addrfq" Style="margin-left:20px;margin-right:20px; display:inline-block; border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Update" runat="server" font-size="Medium" BackColor="lightgreen" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Updatebuttonrfq_Click"  />
             <asp:Button ID="Mailbutton" runat="server" CausesValidation="true" ValidationGroup="addrfq" Style="margin-left:20px;margin-right:20px; display:inline-block; border:inherit;border-radius:25px; width:100px;  font-family:'Linux Libertine G'" Text="Release" font-size="Medium" BackColor="YellowGreen" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Mailbutton_Click" />
             <asp:Button ID="Cancelbutton" Style="margin-left:20px;margin-right:20px; display:inline-block; border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Cancel" runat="server" font-size="Medium" BackColor="#ccff33" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Cancelbutton_Click"  />
           
                <br />
                <br />
                <br />

                <label style="font-size:17px">Total Count:</label>
                <asp:Label ID="lbltotalcount" runat="server" Font-Bold="true" Font-Size="17px"></asp:Label>
                
                 <asp:TextBox ID="txtSearch" CssClass="pull-right" Width="200px" placeholder="Search" runat="server" />
                <asp:ImageButton ID="btnSearch" runat="server" CssClass="pull-right" style="margin-left:5px" width="20" Height="20" ImageUrl="/images/search.png" OnClick="Search"  /> 

                 <asp:ImageButton ID="btnRefresh" runat="server" CssClass="pull-right" width="20" Height="20" ImageUrl="/images/refresh.png" OnClick="Refresh"  />
                 
                
                <asp:GridView ID="GridViewRfq"  runat="server" AutoGenerateColumns="false"  RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center"  DataKeyNames="RFQ_Slno"  OnRowDataBound="GridViewRfq_RowDataBound" OnSelectedIndexChanged="GridViewRfq_SelectedIndexChanged" OnRowDeleting ="GridViewRfq_RowDeleting" CellPadding="5" CellSpacing="5" ForeColor="#333333" GridLines="Vertical" Height="10px" Width="90%" AllowCustomPaging="True" AllowSorting="True" BorderWidth="2px" Font-Bold="True" Font-Names="Times New Roman" >
                    
                    <AlternatingRowStyle BackColor="White" />
                                                  <RowStyle BackColor="#EFF3FB" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Medium" />
<AlternatingRowStyle BackColor="White" />
                    
                    <Columns>
                        <asp:CommandField HeaderText="Select"  ShowSelectButton="true" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" />
                        <asp:BoundField DataField="RFQ_Slno" HeaderText="Slno"  />
                        <asp:BoundField DataField="RFQ_Number" HeaderText="RFQ Number" />
                        <asp:BoundField DataField="RFQ_Company" HeaderText="BuyerCompanyname" Visible="false" />
                       <asp:BoundField DataField="RFQ_CreationDate" HeaderText="CreationDate" />
                         <asp:BoundField DataField="RFQ_OriginCountry" HeaderText="Origin Country" />
                         <asp:BoundField DataField="RFQ_DestinationCountry" HeaderText="Destination Country" />
                        <asp:BoundField DataField="RFQ_OriginAirport" HeaderText="Origin Airport" />
                        <asp:BoundField DataField="RFQ_DestinationAirport" HeaderText="DestinationAirport" />
                        <asp:BoundField DataField="RFQ_NumberofPackages" HeaderText="No of Packages" />
                        <asp:BoundField DataField="RFQ_TotalGrwt" HeaderText="Gr.Wt(Kg)" />
                        <asp:BoundField DataField="RFQ_TotalVolwt" HeaderText="Vol.Wt(Kg)" />
                        <asp:BoundField DataField="RFQ_TotalChwt" HeaderText="Ch.Wt(Kg)" />
                        <asp:BoundField DataField="RFQ_PickupAddress" HeaderText="Pickup Addr" />
                        <asp:BoundField DataField="RFQ_DeliveryAddress" HeaderText="Delivery Addr" />
                        <asp:BoundField DataField="RFQ_PickupDate" HeaderText="Pickup Date" />
                        <asp:BoundField DataField="RFQ_ReqTT" HeaderText="Required T/T" />
                        <asp:BoundField DataField="RFQ_QuoteDueBy" HeaderText="Quote DueBy" />
                        <asp:BoundField DataField="RFQ_Commodity" HeaderText="Commodity" />
                        <asp:BoundField DataField="RFQ_HandlingInfo" HeaderText="Handling Info" />
                        <asp:BoundField DataField="RFQ_ExpectedPrice" HeaderText="ExpectedPrice" />
                        <asp:BoundField DataField="RFQ_UserID" HeaderText="UserID" Visible="false" />
                        <asp:BoundField DataField="RFQ_Timestamp" HeaderText="Timestamp" Visible="false" />
                        <asp:BoundField DataField="RFQ_Submit" HeaderText="Submitted?" />
                        
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
        <script type="text/javascript">
   
    function WeightCalculation()
    {
        var _txt1 = document.getElementById('<%= txtgrossweight.ClientID %>');
        var _txt2 = document.getElementById('<%= txtvolumetricweight.ClientID %>');
        var _txt3 = document.getElementById('<%= txtchargeableweight.ClientID %>');
        var t1=0, t2=0;
        
        if(_txt1.value != "") t1=_txt1.value;
        if (_txt2.value != "") t2 = _txt2.value;
        if(_txt1.value >= _txt2.value)
            _txt3.value = parseInt(t1);
        if (_txt2.value >= _txt1.value)
            _txt3.value = parseInt(t2);
    }
        </script>
    </form>
</body>
</html>
