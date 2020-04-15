<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShipmentDetails(Seller).aspx.cs" EnableEventValidation = "false" Inherits="FinalYearProject.ShipmentDetails_Seller_" %>
<%@Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
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
                    <li><a href="GeneralMasters/DummyMasterData.aspx" target="_self">Company</a></li>
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
          
    <li><a href="ShipmentDelivery(Buyer).aspx">Shipmentdelivery</a></li>
    <li><a href="#">RFQ</a></li>
   
  </ul>
                                 </div>
       <div class="btn-group" style="vertical-align:bottom;">
                
                                 
                    
                <button type="button" id="ButtonSeller" runat="server" class="btn btn-default dropdown-toggle"  data-toggle="dropdown">
    <span class="caret"></span>
    <span class="sr-only">Toggle Dropdown</span>
  </button>
  <ul class="dropdown-menu" role="menu">
    <!-- here is the asp.net link button to make post back -->
          
    <li><a href="ShipmentDetails(Seller).aspx">ShipmentDetails</a></li>
    <li><a href="#">SellerQuote</a></li>
   
  </ul>
                                 </div>
        </div> 
             
            </nav>
  

         <nav class="navbar3 navbar navbar-dark bg-primary" style="height:20px;"id="navbarthree"> 
 
    <p class="navbar-text" style="font-size:21px">Shipment Details(Seller)</p>
             </nav>
            
            <div >
            
            
            <label for="txtshipmentnumber" style="font-size:19px;margin-left:20px;margin-right:50px"> Shipment Number* <br> <asp:TextBox ID="txtshipmentnumber" Enabled="false"  CssClass="form-control" Width="145px" runat="server"></asp:TextBox> </label>
           <asp:RequiredFieldValidator ID="rfvshipmentnumber" ControlToValidate="txtshipmentnumber" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            <label style="font-size:19px;margin-left:20px;margin-right:50px;"> Creation Date * <br > <asp:TextBox ID="txtcreationdate" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="Calendarcreationdate" PopupButtonID="imgPopup" runat="server" TargetControlID="txtcreationdate" OnClientDateSelectionChanged="AppendTime" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             <asp:RequiredFieldValidator ID="rfvcreationdate" ControlToValidate="txtcreationdate" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            <label style="font-size:19px;margin-left:20px;margin-right:50px">Customer* <br> <asp:DropDownList ID="dropdowncustomer" Height="30px"  Width="160px" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList></label>
            <asp:RequiredFieldValidator ID="rfvcustomer" ControlToValidate="dropdowncustomer" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br /> <br />
          <label style="font-size:19px;margin-left:20px;margin-right:35px"> Number of packages* <br> <asp:TextBox ID="txtnoofpackages" Width="145px"  runat="server"></asp:TextBox> </label>
          <asp:RequiredFieldValidator ID="rfvnoofpackages" ControlToValidate="txtnoofpackages" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                 
                 <label style="font-size:19px;margin-left:20px;margin-right:51px"> Gross Weight* <br> <asp:TextBox ID="txtgrossweight" runat="server" Width="145px"></asp:TextBox> </label>
                <asp:RequiredFieldValidator ID="rfvgrossweight" ControlToValidate="txtgrossweight" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <label style="font-size:19px;margin-left:20px;margin-right:50px"> Chargeable Weight* <br> <asp:TextBox ID="txtchargeableweight" runat="server" Width="145px"></asp:TextBox> </label>
                <asp:RequiredFieldValidator ID="rfvchargeableweight" ControlToValidate="txtchargeableweight" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br /> <br />
             <label style="font-size:19px;margin-left:20px;margin-right:66px"> HAWB* <br> <asp:TextBox ID="txthawb" Width="145px"  runat="server"></asp:TextBox> </label>
            <asp:RequiredFieldValidator ID="rfvhawb" ControlToValidate="txthawb" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            <label style="font-size:19px;margin-left:20px;margin-right:71px"> HAWB Date * <br > <asp:TextBox ID="txthawbdate" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="Calendarhawbdate" PopupButtonID="imgPopup" runat="server" TargetControlID="txthawbdate" OnClientDateSelectionChanged="AppendTime" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             <asp:RequiredFieldValidator ID="rfvhawbdate" ControlToValidate="txthawbdate" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            <label style="font-size:19px;margin-right:50px"> MAWB* <br> <asp:TextBox ID="txtmawb" Width="145px" runat="server"></asp:TextBox> </label>
           <asp:RequiredFieldValidator ID="rfvmawb" ControlToValidate="txtmawb" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
               <label style="font-size:19px;margin-left:20px;margin-right:66px"> MAWB Date * <br > <asp:TextBox ID="txtmawbdate" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>

                <ajaxToolkit:CalendarExtender ID="CalendarExtendermawbdate" PopupButtonID="imgPopup" runat="server" TargetControlID="txtmawbdate" OnClientDateSelectionChanged="AppendTime" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             <asp:RequiredFieldValidator ID="rfvmawbdate" ControlToValidate="txtmawbdate" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                <br /> <br />
            <label style="font-size:19px;margin-left:20px;margin-right:66px"> Airline * <br> <asp:TextBox ID="txtairline" Width="145px" runat="server"></asp:TextBox> </label>
            <asp:RequiredFieldValidator ID="rfvairline" ControlToValidate="txtairline" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            <label style="font-size:19px;margin-left:20px;margin-right:50px"> FlightNumber * <br> <asp:TextBox ID="txtflightnumber" Width="145px" runat="server"></asp:TextBox> </label>
           <asp:RequiredFieldValidator ID="rfvflightnumber" ControlToValidate="txtflightnumber" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
            <label style="font-size:19px;margin-left:20px;margin-right:50px"> ETD * <br > <asp:TextBox ID="txtetd" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderetd" PopupButtonID="imgPopup" runat="server" TargetControlID="txtetd" OnClientDateSelectionChanged="AppendTime" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             <asp:RequiredFieldValidator ID="rfvetd" ControlToValidate="txtetd" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
             <label style="font-size:19px;margin-left:20px;margin-right:50px"> ETA * <br > <asp:TextBox ID="txteta" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtendereta" PopupButtonID="imgPopup" OnClientDateSelectionChanged="AppendTime" runat="server" TargetControlID="txteta" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
          <asp:RequiredFieldValidator ID="rfveta" ControlToValidate="txteta" ValidationGroup="addshipmentdetailsseller" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                 <br /> <br />  
             <label style="font-size:19px;margin-left:20px;margin-right:86px"> ATD  <br > <asp:TextBox ID="txtatd" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderatd" PopupButtonID="imgPopup" OnClientDateSelectionChanged="AppendTime" runat="server" TargetControlID="txtatd" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             
             <label style="font-size:19px;margin-right:40px"> ATA  <br > <asp:TextBox ID="txtata" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderata" PopupButtonID="imgPopup" OnClientDateSelectionChanged="AppendTime" runat="server" TargetControlID="txtata" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             
            
           
            
            <asp:CheckBox ID ="delivery"  runat="server" style="margin-left:40px" />  <label style="font-size:19px;margin-left:13px;margin-right:69px">Delivered?</label>
            
            <label style="font-size:19px;margin-left:41px;margin-right:70px"> Delivery date  <br > <asp:TextBox ID="txtdeliverydate" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderdeliverydate" OnClientDateSelectionChanged="AppendTime" PopupButtonID="imgPopup" runat="server" TargetControlID="txtdeliverydate" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
            <br /><br /> <br /> <br />
             <label style="font-size:19px;margin-left:19px;margin-right:58px"> Receiver Name <br> <asp:TextBox ID="txtreceivedby" Width="145px" runat="server"></asp:TextBox> </label>
                <label style="font-size:19px;margin-left:30px;margin-right:119px"> Receiver Mobile Number <br> <asp:TextBox ID="txtreceivermobilenumber" Width="145px" runat="server"></asp:TextBox> </label>
                <label style="font-size:19px;margin-left:129px"> Receiver Mail ID <br> <asp:TextBox ID="txtreceivermailid" Width="145px" runat="server"></asp:TextBox> </label>
            
             <br /> <br /> <br /> <br />  
           
            <asp:Button ID="Addbutton" CausesValidation="true" ValidationGroup="addshipmentdetailsseller" Style="margin-left:20px;margin-right:20px; display:inline-block;  border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Add" runat="server" font-size="Medium" BackColor="lightblue" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Addbutton_Click" />
            <asp:Button ID="Updatebutton" CausesValidation="true" ValidationGroup="addshipmentdetailsseller" Style="margin-left:20px;margin-right:20px; display:inline-block; border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Update" runat="server" font-size="Medium" BackColor="lightgreen" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Updatebutton_Click" />
                 <asp:Button ID="Submitbutton" CausesValidation="true" ValidationGroup="addshipmentdetailsseller" Style="margin-left:20px;margin-right:20px; display:inline-block; border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Submit" runat="server" font-size="Medium" BackColor="YellowGreen" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Submitbutton_Click" />
             
             <asp:Button ID="Cancelbutton" Style="margin-left:20px;margin-right:20px; display:inline-block; border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Cancel" runat="server" font-size="Medium" BackColor="#ccff33" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Cancelbutton_Click" />
           <asp:Button ID="btnmail" Style="margin-left:20px;margin-right:20px; display:inline-block; border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="SendMail" runat="server" font-size="Medium" BackColor="#ccff33" class="btn- btn-primary" Width="94px" Height="40px" OnClick="mailbutton_Click" />
                <br />
                <br />
                <br />

                <label style="font-size:17px">Total Count:</label>
                <asp:Label ID="lbltotalcount" runat="server" Font-Bold="true" Font-Size="17px"></asp:Label>
                
                 <asp:TextBox ID="txtSearch" CssClass="pull-right" Width="200px" placeholder="Search" runat="server" />
                <asp:ImageButton ID="btnSearch" runat="server" CssClass="pull-right" style="margin-left:5px" width="20" Height="20" ImageUrl="/images/search.png" OnClick="Search" /> 

                 <asp:ImageButton ID="btnRefresh" runat="server" CssClass="pull-right" width="20" Height="20" ImageUrl="/images/refresh.png" OnClick="Refresh" />
                 
                
                <asp:GridView ID="GridViewSeller" OnPageIndexChanging="OnPageIndexChanging" runat="server" AutoGenerateColumns="false" OnRowDataBound="GridViewSeller_RowDataBound" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center"  DataKeyNames="shipmentnumber" OnSelectedIndexChanged="GridViewSeller_SelectedIndexChanged" OnRowDeleting ="GridViewSeller_RowDeleting" CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Vertical" Height="100px" Width="100%" AllowCustomPaging="True" AllowSorting="True" BorderWidth="2px" Font-Bold="True" Font-Names="Times New Roman" >
                    
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    
                    <Columns>
                        <asp:CommandField HeaderText="Select"  ShowSelectButton="true" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" />
                        <asp:BoundField DataField="shipmentnumber" HeaderText="ShipmentNumber" />
                        <asp:BoundField DataField="creationdate" HeaderText="CreationDate" />
                        <asp:BoundField DataField="customer_M_Company_Name" HeaderText="CustomerName" />
                        <asp:BoundField DataField="numberofpackages" HeaderText="NumberOfPackages" />
                         <asp:BoundField DataField="grossweight" HeaderText="GrossWeight" />
                        <asp:BoundField DataField="chargeableweight" HeaderText="ChargeableWeight" />
                        <asp:BoundField DataField="hawb" HeaderText="HAWB" />
                        <asp:BoundField DataField="hawbdate" HeaderText="HAWB Date" />
                        <asp:BoundField DataField="mawb" HeaderText="MAWB Date" />
                        <asp:BoundField DataField="mawbdate" HeaderText="MAWB Date" />
                        <asp:BoundField DataField="airline" HeaderText="Airline" />
                        <asp:BoundField DataField="flightnumber" HeaderText="FlightNumber" />
                        <asp:BoundField DataField="etd" HeaderText="ETD" />
                        <asp:BoundField DataField="eta" HeaderText="ETA" />
                        <asp:BoundField DataField="atd" HeaderText="ATD" />
                        <asp:BoundField DataField="ata" HeaderText="ATA" />
                        <asp:BoundField DataField="delivered" HeaderText="Delivered" />
                        <asp:BoundField DataField="delivery" HeaderText="DeliveryDate" />
                        <asp:BoundField DataField="receivedbyname" HeaderText="ReceivedBy" />
                        <asp:BoundField DataField="receivermobileno" HeaderText="ReceiverMobileNo" />
                        <asp:BoundField DataField="receiveremailid" HeaderText="ReceiverEmail" />

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
    </form>
</body>
</html>
