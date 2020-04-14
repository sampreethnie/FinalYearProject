<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShipmentDelivery(Buyer).aspx.cs" Inherits="FinalYearProject.ShipmentDelivery_Buyer_" %>
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

    
                    <asp:Button ID="btnbuyer" class="btn btn-default" runat="server" Text="Buyer" style="width:100px;height:25px" />
                
                                 
                    <asp:Button ID="btnseller" class="btn btn-default" runat="server" Text="Seller" style="width:100px;height:25px" />
               
                                 </div>
        </div> 
             
            </nav>
  

         <nav class="navbar3 navbar navbar-dark bg-primary" style="height:20px;"id="navbarthree"> 
 
    <p class="navbar-text" style="font-size:21px">Shipment Delivery(Buyer)</p>
             </nav>
        <asp:TextBox ID="txtbuyerid" runat="server" Visible="false"></asp:TextBox>

        <div>
            <label style="font-size:19px;margin-left:20px;margin-right:50px">
            Select your shipment number <br>
            <asp:DropDownList ID="dropdownshipmentnumber" Width="100px" runat="server" AutoPostBack="true" OnSelectedIndexChanged="shipmentnumber_SelectedIndexChanged" >

            </asp:DropDownList> </label>
            <br />
                             <label for="txtshipmentnumber" style="font-size:19px;margin-left:20px;margin-right:50px"> Shipment number <br> <asp:TextBox ID="txtshipmentnumber"  CssClass="form-control" Width="145px" runat="server"></asp:TextBox> </label>
           
            <label style="font-size:19px;margin-left:20px;margin-right:50px;"> Creationdate * <br > <asp:TextBox ID="txtcreationdate" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="Calendarcreationdate" OnClientDateSelectionChanged="AppendTime" PopupButtonID="imgPopup" runat="server" TargetControlID="txtcreationdate" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             
            <label style="font-size:19px;margin-left:20px;margin-right:50px">Customer <br> <asp:DropDownList ID="dropdowncustomer" Height="27px"  Width="160px" runat="server" CssClass="form-control" AutoPostBack="true" ></asp:DropDownList></label>
            
          <label style="font-size:19px;margin-left:20px;margin-right:50px"> Number of packages <br> <asp:TextBox ID="txtnoofpackages" Width="145px"  runat="server"></asp:TextBox> </label>
            
             <label style="font-size:19px;margin-left:20px;margin-right:50px"> HAWB <br> <asp:TextBox ID="txthawb" Width="145px"  runat="server"></asp:TextBox> </label>
             <br /> <br />
            <label style="font-size:19px;margin-left:20px;margin-right:75px"> HAWB Date * <br > <asp:TextBox ID="txthawbdate" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="Calendarhawbdate" OnClientDateSelectionChanged="AppendTime" PopupButtonID="imgPopup" runat="server" TargetControlID="txthawbdate" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             
            <label style="font-size:19px;margin-right:50px"> MAWB <br> <asp:TextBox ID="txtmawb" Width="145px" runat="server"></asp:TextBox> </label>
           

            
            

            
             
            
            <label style="font-size:19px;margin-left:20px;margin-right:49px"> MAWB Date * <br > <asp:TextBox ID="txtmawbdate" CssClass="form-control" Width="165px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtendermawbdate" OnClientDateSelectionChanged="AppendTime" PopupButtonID="imgPopup" runat="server" TargetControlID="txtmawbdate" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             
            <label style="font-size:19px;margin-left:20px;margin-right:73px"> Airline <br> <asp:TextBox ID="txtairline" Width="145px" runat="server"></asp:TextBox> </label>
            
            <label style="font-size:19px;margin-left:20px;margin-right:50px"> FlightNumber <br> <asp:TextBox ID="txtflightnumber" Width="145px" runat="server"></asp:TextBox> </label>
            
            <br /><br />
            <label style="font-size:19px;margin-left:20px;margin-right:55px"> ETD * <br > <asp:TextBox ID="txtetd" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderetd" OnClientDateSelectionChanged="AppendTime" PopupButtonID="imgPopup" runat="server" TargetControlID="txtetd" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             
             <label style="font-size:19px;margin-left:20px;margin-right:50px"> ETA * <br > <asp:TextBox ID="txteta" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtendereta" OnClientDateSelectionChanged="AppendTime" PopupButtonID="imgPopup" runat="server" TargetControlID="txteta" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             
             <label style="font-size:19px;margin-left:20px;margin-right:70px"> ATD * <br > <asp:TextBox ID="txtatd" CssClass="form-control" Width="165px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderatd" OnClientDateSelectionChanged="AppendTime" PopupButtonID="imgPopup" runat="server" TargetControlID="txtatd" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             
             <label style="font-size:19px"> ATA * <br > <asp:TextBox ID="txtata" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderata" OnClientDateSelectionChanged="AppendTime" PopupButtonID="imgPopup" runat="server" TargetControlID="txtata" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             
            <br /> <br /> <br /> <br />
           
            
            <asp:CheckBox ID ="delivery" style="margin-left:20px"  runat="server" />  <label style="font-size:19px;margin-left:10px;margin-right:50px">Delivered?</label>
            
            <label style="font-size:19px;margin-left:56px;margin-right:39px"> Delivery date Seller * <br > <asp:TextBox ID="txtdeliverydate" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderdeliverydate" OnClientDateSelectionChanged="AppendTime" PopupButtonID="imgPopup" runat="server" TargetControlID="txtdeliverydate" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
             
             <label style="font-size:19px;margin-left:2px"> ReceivedBy Seller <br> <asp:TextBox ID="txtsellerreceivedby" Width="145px" runat="server"></asp:TextBox> </label>
            <br />
            <br />
             <asp:CheckBox ID ="received" style="margin-left:20px"  runat="server" />  <label style="font-size:19px;margin-left:10px;margin-right:50px">Received?</label>
            <label style="font-size:19px;margin-left:62px"> ReceivedBy Buyer <br> <asp:TextBox ID="txtbuyerreceivedbyname" Width="145px" runat="server"></asp:TextBox> </label>
            <label style="font-size:19px;margin-left:65px;margin-right:70px"> Delivery date Buyer * <br > <asp:TextBox ID="txtbuyerdeliverydatetime" CssClass="form-control" Width="145px" runat="server" ></asp:TextBox> </label>
                <ajaxToolkit:CalendarExtender ID="CalendarExtenderdeliverydatetime" OnClientDateSelectionChanged="AppendTime" PopupButtonID="imgPopup" runat="server" TargetControlID="txtbuyerdeliverydatetime" Format="dd/MM/yyyy"> </ajaxToolkit:CalendarExtender>
            <br />
            <br />
             <asp:Button ID="Addbutton" Style="margin-left:20px;margin-right:80px; display:inline-block;  border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Add" runat="server" font-size="Medium" BackColor="lightblue" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Addbutton_Click" />
            <asp:Button ID="Updatebutton" Style="margin-left:20px;margin-right:20px; display:inline-block; border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Update" runat="server" font-size="Medium" BackColor="lightgreen" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Updatebutton1_Click" />
            <asp:Button ID="Submitbutton" Style="margin-left:20px;margin-right:20px; display:inline-block; border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Submit" runat="server" font-size="Medium" BackColor="YellowGreen" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Submitbutton_Click" />
             
             <asp:Button ID="Cancelbutton" Style="margin-left:20px;margin-right:20px; display:inline-block; border:inherit;border-radius:25px;font-family:'Linux Libertine G'" Text="Cancel" runat="server" font-size="Medium" BackColor="#ccff33" class="btn- btn-primary" Width="94px" Height="40px" OnClick="Cancelbutton_Click" />
             <br />
            <br />
            <label style="font-size:17px">Total Count:</label>
                <asp:Label ID="lbltotalcount" runat="server" Font-Bold="true" Font-Size="17px"></asp:Label>
                <asp:GridView ID="GridViewBuyer" AutoGenerateColumns="false" runat="server" OnRowDataBound="GridViewBuyer_RowDataBound" RowStyle-HorizontalAlign="Center" RowStyle-VerticalAlign="Middle" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center"  DataKeyNames="buyerid" OnSelectedIndexChanged="GridViewBuyer_SelectedIndexChanged" OnRowDeleting ="GridViewBuyer_RowDeleting" CellPadding="1" CellSpacing="1" ForeColor="#333333" GridLines="Vertical" Height="100px" Width="100%" AllowCustomPaging="True" AllowSorting="True" BorderWidth="2px" Font-Bold="True" Font-Names="Times New Roman" >
                    
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    
                    <Columns>
                        <asp:CommandField HeaderText="Select" ShowSelectButton="true" />
                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="true" />
                        <asp:BoundField DataField="buyerid" HeaderText="BuyerID" />
                        <asp:BoundField DataField="sellershipmentnumber" HeaderText="ShipmentNumber" />
                        <asp:BoundField DataField="sellercreationdate" HeaderText="CreationDate" />
                        <asp:BoundField DataField="sellercustomer" HeaderText="CustomerName" />
                        <asp:BoundField DataField="sellernumberofpackages" HeaderText="NumberOfPackages" />
                        <asp:BoundField DataField="sellerhawb" HeaderText="HAWB" />
                        <asp:BoundField DataField="sellerhawbdate" HeaderText="HAWB Date" />
                        <asp:BoundField DataField="sellermawb" HeaderText="MAWB Date" />
                        <asp:BoundField DataField="sellermawbdate" HeaderText="MAWB Date" />
                        <asp:BoundField DataField="sellerairline" HeaderText="Airline" />
                        <asp:BoundField DataField="sellerflightnumber" HeaderText="FlightNumber" />
                        <asp:BoundField DataField="selleretd" HeaderText="ETD" />
                        <asp:BoundField DataField="sellereta" HeaderText="ETA" />
                        <asp:BoundField DataField="selleratd" HeaderText="ATD" />
                        <asp:BoundField DataField="sellerata" HeaderText="ATA" />
                        <asp:BoundField DataField="sellerdelivered" HeaderText="Delivered" />
                        <asp:BoundField DataField="sellerdeliverydate" HeaderText="DeliveryDate" />
                        <asp:BoundField DataField="sellerreceivedbyname" HeaderText="ReceivedBy" />
                        <asp:BoundField DataField="buyerreceived" HeaderText="BuyerReceived" />
                        <asp:BoundField DataField="buyerreceivedbyname" HeaderText="BuyerReceivedName" />
                        <asp:BoundField DataField="buyerdeliverydate" HeaderText="BuyerDeliveryDate" />
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
