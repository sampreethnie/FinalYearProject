<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="FinalYearProject.Company" %>
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
        <a ><asp:Button ID="btnlogout" runat="server" style="width:70px;height:40px;color:black;background-color:orange;border-color:chartreuse;border-radius:inherit" Text="Logout" OnClick="btnlogout_Click" /></a>
      </li>
                 
			    </ul>
				</li>
				</ul>
  </div>
</nav><nav class="navbar2 navbar navbar-primary" id="navbartwo"> 
        
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
          
    <li><a href="ShipmentDelivery(Buyer).aspx">Shipmentdelivery</a></li>
    <li><a href="#">RFQ</a></li>
   
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
          
    <li><a href="ShipmentDetails(Seller).aspx">ShipmentDetails</a></li>
    <li><a href="#">SellerQuote</a></li>
   
  </ul>
                                 </div>
        </div> 
             
            </nav>
  

         <nav class="navbar3 navbar navbar-dark bg-primary" style="height:20px;"id="navbarthree"> 
 
    <p class="navbar-text" style="font-size:21px">Company</p>
             </nav>
         <div class="modal-body">
                        
                        
                        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingOne">
                                    <h4 class="panel-title">
                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="CollapseOne">
                                            <i class="more-less glyphicon glyphicon-plus"></i>
                                             
                                            Company Registration Details
                                        </a>
                                    </h4>
                                    </div>
                                    <div id="collapseOne" class="panel panel-collapse" role="tabpanel" aria-labelledby="headingOne">
                                           <div class="panel-body">
                                               <div>
                                                   <label>Category
                                                       <br> <asp:RadioButtonList ID="categorylist" Enabled="false" runat="server" >
                                                           <asp:ListItem Value="b">Buyer</asp:ListItem>
                                                           <asp:ListItem Value="s">Seller</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                   </label>
                                               </div>
                                               
                                               <div>
                                                   
                                                   <label>Company Name *<br> <asp:TextBox ID="txtcompanynamefinal" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                   <asp:RequiredFieldValidator ID="companyname" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtcompanynamefinal" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                                    <label>Address1 *<br> <asp:TextBox ID="txtaddress1final" Width="400px" CssClass="form-control" ReadOnly="true" Height="34px"  runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="addressone" runat="server" ValidationGroup="registrationform" SetFocusOnError="true"  ControlToValidate="txtaddress1final" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                                   <label>Address2<br> <asp:TextBox ID="txtaddress2final" ReadOnly="true" Width="400px" CssClass="form-control" Height="34px"  runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                   <label>Landmark<br> <asp:TextBox ID="txtlandmarkfinal" ReadOnly="true" Width="400px" CssClass="form-control" Height="34px" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                    <label>City *<br> <asp:TextBox ID="txtcityfinal" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="city" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtcityfinal" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                                      </div>
                                               <div>
                                               <label>ZipCode *<br> <asp:TextBox ID="txtpinfinal" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="pin" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtpinfinal" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                               <label>Country *<br> <asp:DropDownList ID="dropdowncountryfinal" Enabled="false" CssClass="form-control" AutoPostBack="true" Width="140px" runat="server"></asp:DropDownList> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="countrylist" runat="server" InitialValue="0" Text="" ValidationGroup="registrationform" ControlToValidate="dropdowncountryfinal"  ErrorMessage=""></asp:RequiredFieldValidator>
                                                <label>State<br> <asp:DropDownList ID="dropdownstatefinal" Enabled="false"  Width="140px" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="statelist" runat="server" InitialValue="0" Text="" ValidationGroup="registrationform" ControlToValidate="dropdownstatefinal"  ErrorMessage=""></asp:RequiredFieldValidator>
                                                <label>Currency *<br> <asp:DropDownList ID="dropdowncurrencyfinal" Enabled="false" Width="140px"  CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="currencylist" runat="server" InitialValue="0" Text="" ValidationGroup="registrationform" ControlToValidate="dropdowncurrencyfinal"  ErrorMessage=""></asp:RequiredFieldValidator>
                                                   </div>
                                               <div>
                                                    <label>PAN<br> <asp:TextBox ID="txtpanfinal" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                   
                                                    <label>TAN<br> <asp:TextBox ID="txttanfinal" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                   <label>CompanyURL *<br> <asp:TextBox ID="txtcompanyurlfinal" ReadOnly="true" CssClass="form-control" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                   <asp:RequiredFieldValidator ID="companyurl" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtcompanyurlfinal" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                                   <br />
                                                   <br />
                                                   <asp:Button ID="enablebutton" runat="server" CssClass="btn btn-primary" Text="Enable" OnClick="btnEnable" Width="90px" Height="30px" BackColor="YellowGreen"/>
                                                   <%--<asp:Button ID="editbutton" runat="server" CssClass="btn btn-primary" Text="Edit" OnClick="btnEdit" Width="90px" Height="30px" BackColor="CornflowerBlue"/>--%>
                        
                                               </div></div></div> </div> </div> 
             <div class="panel-group" id="accordion1" role="tablist" aria-multiselectable="true">
                              <div class="panel panel-default">
                                  <div class="panel-heading" role="tab" id="headingTwo">
                                      <h4 class="panel-title">
                                          <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="CollapseTwo">
                                              <i class="more-less glyphicon glyphicon-plus"></i>
                                              Contact Details
                                          </a> 
                                      </h4>
                                      </div>
                                  <div id="collapseTwo" class="panel panel-collapse" role="tabpanel" aria-labelledby="headingTwo">
                                      <div class="panel-body">
                                          <div>
                                              <label>Name *<br> <asp:TextBox ID="txtname" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;&nbsp;
                                               <asp:RequiredFieldValidator ID="name" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtname" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                                    <label>Mobile *<br> <asp:TextBox ID="txtmobile" TextMode="Number" ReadOnly="true" runat="server" CssClass="form-control"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;&nbsp;
                                               <asp:RequiredFieldValidator ID="mobileno" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtmobile" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                               <label>UserId *<br> <asp:TextBox ID="txtuserid" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                               <asp:RequiredFieldValidator ID="userid" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtuserid" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                               
                                                       
                                              </div>
                                              </div></div>
                                  </div> </div>
    </form>
</body>
</html>
