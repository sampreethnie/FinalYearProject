<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceAudit.aspx.cs" Inherits="FinalYearProject.InvoiceAudit" %>

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
 
    <p class="navbar-text" style="font-size:21px">Invoice Audit and Gl Coding</p>
             </nav>
    <div>

                <label style="font-size:19px;margin-left:20px;margin-right:50px">Select RFQ Number*<asp:DropDownList ID="dropdownrfqaudit" Height="30px"  Width="160px" runat="server" OnSelectedIndexChanged="dropdownrfqaudit_SelectedIndexChanged" AutoPostBack="true" EnableViewState="true"></asp:DropDownList></label>

           <br /> <br />
        
           <label style="font-size:19px;margin-left:20px;margin-right:59px"> RFQ Number <br> <asp:TextBox ID="txtrfqnumber" Width="150px" ReadOnly="true" runat="server"></asp:TextBox> </label>
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> Creation Date <br> <asp:TextBox ID="txtcreationdate" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
        
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> Origin Country <br> <asp:TextBox ID="txtorigincountry" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtorigincountry" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> Destination Country <br> <asp:TextBox ID="txtdestinationcountry" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtdestinationcountry" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
        <br /><br />
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> Origin Airport <br> <asp:TextBox ID="txtoriginairport" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtoriginairport" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
        <label style="font-size:19px;margin-left:20px;margin-right:50px"> Destination Airport <br> <asp:TextBox ID="txtdestinationairport" Width="150px" runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="txtdestinationairport" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
        
          <label style="font-size:19px;margin-left:20px;margin-right:37px"> Number Of Packages <br> <asp:TextBox ID="txtnumberofpackages" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--      <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtnumberofpackages" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
        <label style="font-size:19px;margin-left:20px;margin-right:59px"> Total GrWt <br> <asp:TextBox ID="txtgrossweight" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtgrossweight" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
        <br />
        <br />
          <label style="font-size:19px;margin-left:20px;margin-right:59px"> Total VolWt <br> <asp:TextBox ID="txtvolumetricweight" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtvolumetricweight" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Total ChWt <br> <asp:TextBox ID="txtchargeableweight" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtchargeableweight" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Pickup Address <br> <asp:TextBox ID="txtpickupaddress" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" ControlToValidate="txtpickupaddress" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> DeliveryAddress <br> <asp:TextBox ID="txtdeliveryaddress" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtdeliveryaddress" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
       <br /> <br />
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Pickup Date <br> <asp:TextBox ID="txtpickupdate" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtpickupdate" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Req T/T <br> <asp:TextBox ID="txttransittime" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txttransittime" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
         
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Commodity <br> <asp:TextBox ID="txtcommodity" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtcommodity" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>

        <br /> <br />
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Handling Info <br> <asp:TextBox ID="txthandlinginfo" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" ControlToValidate="txthandlinginfo" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Seller Company <br> <asp:TextBox ID="txtsellercompany" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" ControlToValidate="txtsellercompany" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Offer Price <br> <asp:TextBox ID="txtofferprice1" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" ControlToValidate="txtofferprice1" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
        

        <br /><br />
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Seller Currency <br> <asp:TextBox ID="txtsellercurrency" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator21" ControlToValidate="txtsellercurrency" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
        
         <label style="font-size:19px;margin-left:20px;margin-right:59px"> Buyer Currency <br> <asp:TextBox ID="txtbuyercurrency" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
<%--        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" ControlToValidate="txtbuyercurrency" ValidationGroup="addorderbuyer" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
         
         <br /><br /> <br />


         <asp:GridView ID="InvoiceGridView" runat="server"   Width="90%" Height="50px"  AllowPaging="true" DataKeyNames="TM_INVOICE_Slno" OnRowDataBound="InvoiceDisplay_RowDataBound" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None">
                                                  <AlternatingRowStyle BackColor="White" />
                                                  <RowStyle BackColor="#EFF3FB" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Medium" />
<AlternatingRowStyle BackColor="White" />
<Columns>
  

  


<asp:BoundField DataField="TM_INVOICE_Slno" HeaderText="InvoiceSerialNo" Visible="false" />
         
<asp:BoundField DataField="TM_INVOICE_No" HeaderText="Invoice Number"   ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
   
    <asp:BoundField DataField="TM_INVOICE_Date" HeaderText="Invoice Date" DataFormatString="{0:dd/MM/yyyy}"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
<asp:BoundField DataField="TM_INVOICE_ShipmentRefNo" HeaderText="ShipmentReferenceNumber"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left"  />
<asp:BoundField DataField="TM_INVOICE_Customer" HeaderText="Customer"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
<asp:BoundField DataField="TM_INVOICE_Currency" HeaderText="Currency"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left"  />
    <asp:TemplateField HeaderText="Billed Amount">
        <ItemTemplate>
           <asp:Label ID="lblbilledamt" runat="server"  ></asp:Label>
        </ItemTemplate>
        
        </asp:TemplateField>
    
    
    
    
    <asp:TemplateField HeaderText="Tax Amount">
        <ItemTemplate>
           <asp:Label ID="lbltaxamt" runat="server"  ></asp:Label>
        </ItemTemplate>
        <FooterTemplate>
            <asp:Label ID="lbltaxtotal" runat="server" Text="Total Tax Amount"></asp:Label>
        </FooterTemplate>
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Total Amount">
        <ItemTemplate>
           <asp:Label ID="lbltotalamt" runat="server" ></asp:Label>
        </ItemTemplate>
        <FooterTemplate>
            <asp:Label ID="lblfinaltotal" runat="server" Text="Total"></asp:Label>
        </FooterTemplate>
        </asp:TemplateField>
    <asp:BoundField DataField="TM_INVOICE_RFQNumber" HeaderText="RFQ Number"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left"  />
        <asp:BoundField DataField="TM_INVOICE_Status" HeaderText="Status"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
            <asp:BoundField DataField="TM_INVOICE_GLCode" HeaderText="Gl Code"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />

       <asp:TemplateField HeaderText="Edit">
<ItemTemplate>
<asp:ImageButton ID="imgbtnqnb" ImageUrl="/images/edit1.png" runat="server" Width="25" Height="25" OnClick="imgbtninvoiceaudit_Click" />
    </ItemTemplate>
    </asp:TemplateField>

</Columns>
                                              </asp:GridView> 

        <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mpeqnb" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
 BackgroundCssClass="modalBackground" CancelControlID="btnCancel">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="440px" Width="503px" style="display:none">
<table width="100%" style="border:Solid 1.5px #00ff21;border-bottom:solid 2px #00ff21; border-bottom-color:#00ff21;  width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr style="background-color:blue">
<td colspan="3" style=" height:10%; color:White; font-weight:bold; font-size:xx-large" align="left">Invoice Approval</td>
    
</tr>
    <tr>
        <td>
            <asp:Label ID="lblslno" runat="server" Visible="false" />
        </td>
    </tr>
<tr>
<td align="left" style="font-size:19px">
Invoice Number
</td>
<td>
<asp:TextBox ID="txtinvoicenumber" runat="server" ReadOnly="true" style="width:180px"/>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Invoice Date
</td>
<td>
    <asp:TextBox ID="txtinvoicedate" runat="server" ReadOnly="true" style="width:180px"/>
</td>
</tr>
    <tr>
    <td align="left" style="font-size:19px">
Additional References
</td>
<td>
    <asp:TextBox ID="txtshipmentreferences" runat="server" ReadOnly="true" style="width:180px"/>
</td>
</tr>
    <tr>
    <td align="left" style="font-size:19px">
Customer
</td>
<td>
    <asp:TextBox ID="txtcustomer" runat="server" ReadOnly="true" style="width:180px"/>
</td>
</tr>
    <tr>
    <td align="left" style="font-size:19px">
Currency
</td>
<td>
    <asp:TextBox ID="txtcurrency" runat="server" ReadOnly="true" style="width:180px"/>
</td>
</tr>
    <tr>
    <td align="left" style="font-size:19px">
Billed Amount
</td>
<td>
    <asp:TextBox ID="txtbilledamount" runat="server" ReadOnly="true" style="width:180px"/>
</td>
</tr>
    <tr>
    <td align="left" style="font-size:19px">
Tax Amount
</td>
<td>
    <asp:TextBox ID="txttaxamount" runat="server" ReadOnly="true" style="width:180px"/>
</td>
</tr>
    <tr>
    <td align="left" style="font-size:19px">
Total Amount
</td>
<td>
    <asp:TextBox ID="txttotalamount" runat="server" ReadOnly="true" style="width:180px"/>
</td>
</tr>
     <tr>
    <td align="left" style="font-size:19px">
GL Code
</td>
<td>
    <asp:TextBox ID="txtglcode" runat="server" style="width:180px"/>
</td>
</tr>
    <tr>
        <td>
                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-primary"  style="width:70px;background-color:goldenrod;height:40px;margin-left:20px;margin-right:50px;color:white;border:solid;border-top-left-radius:8px;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px" Text="Accept" OnClick="btnSubmit_Click"  />
                </td>
         <td>
                    <asp:Button ID="btnReject" runat="server" CssClass="btn btn-primary"  style="width:70px;background-color:lightcoral;height:40px;margin-left:30px;margin-right:50px;color:white;border:solid;border-top-left-radius:8px;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px" Text="Reject"  />
                </td>
    <td>
                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger"  style="width:70px;height:40px;margin-left:30px;color:white;border:solid;border-top-left-radius:8px;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px" Text="Cancel" />
                </td>
        
            </tr>
</table>
</asp:Panel>
        <br /> <br />
        
                  <label style="font-size:19px;margin-left:20px;margin-right:59px"> Total Billed Amount <br> <asp:TextBox ID="txtfinalbilledamount" Width="150px" ReadOnly="true" runat="server"></asp:TextBox> </label>

          <label style="font-size:19px;margin-left:40px;margin-right:59px"> TotalTaxAmount <br> <asp:TextBox ID="txtfinaltaxamount" Width="150px" ReadOnly="true" runat="server"></asp:TextBox> </label>
        <label style="font-size:19px;margin-left:40px;margin-right:59px"> GrandTotalAmount <br> <asp:TextBox ID="txtfinaltotalamount" Width="150px"   runat="server" ReadOnly="true"></asp:TextBox> </label>
    <br />
        <br />
        <br />
        <br />
    </div>
    </form>
</body>
</html>
