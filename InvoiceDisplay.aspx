
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoiceDisplay.aspx.cs" Inherits="FinalYearProject.InvoiceDisplay" %>

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
 
    <p class="navbar-text" style="font-size:21px">DisplayInvoice</p>

     <div class="input-group topspace5 bottomspace5">
             

        
         <span class="input-group-addon nbrd" style="padding:0px 4px">
 
         
             </span>
                </div>  
           
             
            
           
            
            
        

  
  
</nav>
     <a href="InvoiceDetails.aspx" runat="server">Go Back</a>
        <div class="panel-group" id="accordion" role="tablist" aria-multiselectable="true">
                            <div class="panel panel-default">
                                <div class="panel-heading" role="tab" id="headingOne">
                                    <h4 class="panel-title">
                                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseOne" aria-expanded="false" aria-controls="CollapseOne">
                                            <i class="more-less glyphicon glyphicon-plus"></i>
                                             
                                            Details
                                        </a>
                                    </h4>
                                    </div>
                                    <div id="collapseOne" class="panel panel-collapse" role="tabpanel" aria-labelledby="headingOne">
                                           <div class="panel-body">
                                               <div style="font-family:'Times New Roman', Times, serif;font-size:medium">
    RFQ Number : <asp:Label ID="Label1" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
    ShipmentNumber : <asp:Label ID="Label2" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
    CustomerName :  <asp:Label ID="Label3" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
    Origin Country : <asp:Label ID="Label5" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
     Origin Airport  : <asp:Label ID="Label6" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
     Destination Country : <asp:Label ID="Label7" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
     Destination Airport : <asp:Label ID="Label8" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
     Gross Weight : <asp:Label ID="Label9" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
     Chargeable Weight : <asp:Label ID="Label10" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
     Number of Packages : <asp:Label ID="Label11" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
      Delivered? : <asp:Label ID="Label12" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
     Received? : <asp:Label ID="Label13" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
      Buyer Currency: <asp:Label ID="Label14" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
        <asp:Label ID="lblcompanyslno" runat="server" Text="Label" Visible="false" style="font-size:large"></asp:Label> <br />
       </div>
                                               </div>
                                        </div>

  </div>
   </div>
            <br />
            <br />
              <div class="panel-group" id="accordion1" role="tablist" aria-multiselectable="true">
                              <div class="panel panel-default">
                                  <div class="panel-heading" role="tab" id="headingTwo">
                                      <h4 class="panel-title">
                                          <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseTwo" aria-expanded="false" aria-controls="CollapseTwo">
                                              <i class="more-less glyphicon glyphicon-plus"></i>
                                              Enter Invoice
                                          </a> 
                                      </h4>
                                      </div>
                                  <div id="collapseTwo" class="panel panel-collapse" role="tabpanel" aria-labelledby="headingTwo">
                                      <div class="panel-body">
                                          <div>
                                              <asp:Button ID="Addbutton" runat="server" Text="Add" style="width:80px" OnClick="Addbutton_Click" />
                                              <asp:GridView ID="InvoiceGridView" runat="server"   Width="85%" Height="50px"  AllowPaging="true" DataKeyNames="TM_INVOICE_Slno" OnRowDataBound="InvoiceDisplay_RowDataBound" AutoGenerateColumns="false" CellPadding="4" ForeColor="#333333" GridLines="None">
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
           <asp:Label ID="lbltaxamt" runat="server"></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
    <asp:TemplateField HeaderText="Total Amount">
        <ItemTemplate>
           <asp:Label ID="lbltotalamt" runat="server"></asp:Label>
        </ItemTemplate>
        </asp:TemplateField>
    <asp:BoundField DataField="TM_INVOICE_RFQNumber" HeaderText="RFQ Number" Visible="false"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left"  />
    <asp:BoundField DataField="TM_INVOICE_Status" HeaderText="Status"  ItemStyle-Width="50px"  HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
    <asp:TemplateField HeaderText="Edit" >
<ItemTemplate>
<asp:ImageButton ID="imgbtnInvoice" ImageUrl="/images/edit1.png" runat="server" Width="25" Height="25" style="margin-right:45px" OnClick="ImageButtoninvoice_Click" />
    </ItemTemplate>
    </asp:TemplateField>
    
    <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        
                        
                       <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="/images/delete.png" OnClientClick="javascript: return confirm('Do you want to delete it?');" OnClick="ibtnDelete_Click"/>
                       
              </ItemTemplate>
                </asp:TemplateField>

</Columns>
                                              </asp:GridView> 
                                              </div>
                                          </div>
                                      </div>
                                      </div>
                                  </div>
                                  <asp:Button ID="btnShowPopup" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mpeInvoiceDisplay" runat="server" TargetControlID="btnShowPopup" PopupControlID="pnlpopup"
CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnlpopup" runat="server" BackColor="White" Height="399px" Width="600px" style="display:none" >
<table width="100%" style="border:Solid 3px #D55500; width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr style="background-color:blue">
<td colspan="2" style=" height:10%; color:white; font-weight:bold; font-size:xx-large" align="left">Add Invoice</td>
    
</tr>
    <tr>
        <td>
            <asp:Label ID="lblslno" runat="server" Visible="false" />
        </td>
    </tr>
<tr>
<td align="left" style="font-size:19px">
InvoiceNumber:
</td>
<td>
<asp:TextBox ID="txtinvoicenumber" runat="server" style="width:270px"/>
</td>
</tr>
    <tr>
<td align="left" style="font-size:19px">
Invoice Date:
</td>
<td>
<asp:TextBox ID="txtinvoicedate" runat="server" style="width:270px"/>
                    <ajaxToolkit:CalendarExtender ID="Calendarcreationdate" PopupButtonID="imgPopup" runat="server" TargetControlID="txtinvoicedate" Format="dd/MM/yyyy" > </ajaxToolkit:CalendarExtender>

</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Additional References:
</td>
<td>
<asp:TextBox ID="txtshipmentrefno" runat="server" style="width:270px"/>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Customer:
</td>
<td>
<asp:TextBox ID="txtcustomer" runat="server" ReadOnly="true"  style="width:270px" ></asp:TextBox>
</td>
</tr>

        <tr>
            <td align="left" style="font-size:19px">Currency: </td>
            <td>
                <asp:TextBox ID="txtdisplaycurrency" ReadOnly="true" runat="server" style="width:270px" />
            </td>
        </tr>
    <tr>
            <td align="left" style="font-size:19px">RFQ Number: </td>
            <td>
                <asp:TextBox ID="txtrfqnumber" ReadOnly="true" runat="server" style="width:270px" />
            </td>
        </tr>
    
        <tr>
            
            
                <td>
                    <asp:Button ID="btnAddInvoice" runat="server" CommandName="Add" CssClass="btn btn-primary" style="width:70px;height:50px;  background-color:orange;color:white;border:solid;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px;border-top-left-radius:8px; margin-right:50px;margin-left:30px;height:40px" Text="Add" OnClick="AddInvoiceButton_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" style="width:70px;height:40px;  background-color:red;color:white;border:solid;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px;border-top-left-radius:8px; margin-right:50px;margin-left:30px" Text="Cancel" />
                </td>
            </tr>
        <tr>
        <td>
            <asp:Label ID="lblvalidatecurr" runat="server"  />
        </td>
    </tr>
   

</table>
</asp:Panel>
                  <br />
         <asp:Button ID="btnEditPopup" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mpeInvoiceEdit" runat="server" TargetControlID="btnEditPopup" PopupControlID="pnleditpopup"
CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnleditpopup" runat="server" BackColor="White" Height="399px" Width="600px" style="display:none">
<table width="100%" style="border:Solid 3px #D55500; width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr style="background-color:blue">
<td colspan="2" style=" height:10%; color:White; font-weight:bold; font-size:xx-large" align="left">Edit Invoice</td>
    
</tr>
   <tr>
        <td>
            <asp:Label ID="lblchargeinvoice" runat="server"  />
        </td>
    </tr>
<tr>
<td align="left" style="font-size:19px">
InvoiceNumber:
</td>
<td>
<asp:TextBox ID="txteditinvoice" runat="server" style="width:270px"/>
</td>
</tr>
    <tr>
<td align="left" style="font-size:19px">
Invoice Date:
</td>
<td>
<asp:TextBox ID="txteditinvoicedate" runat="server" style="width:270px"/>
                        <ajaxToolkit:CalendarExtender ID="CalendarExtendereditinvoicedate" PopupButtonID="imgPopup" runat="server" TargetControlID="txteditinvoicedate"  Format="dd/MM/yyyy" > </ajaxToolkit:CalendarExtender>

   
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Additional References:
</td>
<td>
<asp:TextBox ID="txteditshipmentrefno" runat="server" style="width:270px"/>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Customer:
</td>
<td>
<asp:TextBox ID="txteditcustomer" runat="server" ReadOnly="true"  style="width:270px" ></asp:TextBox>
</td>
</tr>

        <tr>
            <td align="left" style="font-size:19px">Currency: </td>
            <td>
                <asp:TextBox ID="txteditcurrency" ReadOnly="true" runat="server" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">RFQ Number: </td>
            <td>
                <asp:TextBox ID="txteditrfqnumber" ReadOnly="true" runat="server" style="width:270px" />
            </td>
        </tr>
    <tr>
            <td align="left" style="font-size:19px">Tax Amount: </td>
            <td>
                <asp:TextBox ID="txtedittotaltaxamount" ReadOnly="true" runat="server" style="width:270px" />
            </td>
        </tr>
     <tr>
            <td align="left" style="font-size:19px">Total Amount: </td>
            <td>
                <asp:TextBox ID="txteditfinaltotalamount" ReadOnly="true" runat="server" style="width:270px" />
            </td>
        </tr>
            <tr>
                <td>
                    <asp:Button ID="UpdateInvoice" runat="server" style="width:70px;height:40px;  background-color:goldenrod;color:white;border:solid;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px;border-top-left-radius:8px; margin-right:50px;margin-left:30px" Text="Update" OnClick="UpdateInvoice_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCanceledit" runat="server" style="width:70px;height:40px;  background-color:red;color:white;border:solid;border-top-right-radius:8px;border-bottom-left-radius:8px;border-bottom-right-radius:8px;border-top-left-radius:8px; margin-right:50px;margin-left:30px" Text="Cancel" />
                </td>
            </tr>
        </tr>
    

</table>
</asp:Panel>
                  <br />
        <div class="panel-group" id="accordion2" role="tablist" aria-multiselectable="true">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingThree">
                    <h4 class="panel-title">

                        



                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                            <i class="more-less glyphicon glyphicon-plus"></i>
                            Enter Charge Details
                        </a>
                    </h4>
                </div>
                <div id="collapseThree" class="panel panel-collapse" role="tabpanel" aria-labelledby="headingThree">
                    <div class="panel-body">
                        <div>
                            <asp:Button ID="AddbuttonCharge" runat="server" Text="Add" style="width:80px" OnClick="AddbuttonCharge_Click" />
                             <asp:GridView ID="gvChargeInvoice" runat="server" OnRowDataBound="gvChargeInvoice_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="Vertical" Width="98%" Height="50px"  AllowPaging="true" DataKeyNames="TD_INVOICE_Slno" AutoGenerateColumns="false">
                                                  <AlternatingRowStyle BackColor="White" />
                                                  <RowStyle BackColor="#EFF3FB" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Font-Size="Medium" />
<AlternatingRowStyle BackColor="White" />
<Columns>

<asp:BoundField DataField="TD_INVOICE_SLNO" HeaderText="InvoiceSlno" Visible="false" />    
<asp:BoundField DataField="TD_INVOICE_TMSLNO" HeaderText="Invoice Number" />
    <asp:BoundField DataField="M_Charge_Name" HeaderText="ChargeName" />
<asp:BoundField DataField="TD_INVOICE_DESCRIPTION" HeaderText="Description" />

<asp:BoundField DataField="TD_INVOICE_BASIS" HeaderText="Charge Basis" />
<asp:BoundField DataField="TD_INVOICE_QTY" HeaderText="Quantity" />
<asp:BoundField DataField="TD_INVOICE_RATE" HeaderText="Unit Rate" />
<asp:BoundField DataField="M_Currency_Name" HeaderText="Currency" />
<asp:BoundField DataField="TD_INVOICE_AMOUNTFC" HeaderText="Amount in Foreign Currency" />
<asp:BoundField DataField="TD_INVOICE_EXCHRATE" HeaderText="ExchangeRate" />
<asp:BoundField DataField="TD_INVOICE_AMOUNTBC" HeaderText="Amount in Base Currency" />
    <asp:BoundField DataField="TD_INVOICE_TAXABLE" HeaderText="Taxable?" />
    <asp:BoundField DataField="TD_INVOICE_TAXNAME" HeaderText="TaxName" />
    <asp:BoundField DataField="TD_INVOICE_TAXPERCENTAGE" HeaderText="TaxPercentage" />
    <asp:BoundField DataField="TD_INVOICE_TAXAMOUNT" HeaderText="Taxamount" />
    <asp:BoundField DataField="TD_INVOICE_TOTALAMOUNT" HeaderText="TotalAmount" />
    <asp:BoundField DataField="TD_INVOICE_Status" HeaderText="Status"  ItemStyle-Width="50px" HeaderStyle-Width="400px" HeaderStyle-HorizontalAlign="Left" />
    
    <asp:TemplateField HeaderText="Edit">
<ItemTemplate>
<asp:ImageButton ID="imgbtnCharge" ImageUrl="/images/edit1.png" runat="server" Width="25" Height="25" style="margin-right:30px" OnClick="ImagebuttonCharge_Click" />
    </ItemTemplate>
    </asp:TemplateField>

    <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        
                        
                       <asp:ImageButton ID="ibtnchargeDelete" runat="server" ImageUrl="/images/delete.png" style="margin-left:2px" OnClientClick="javascript: return confirm('Do you want to delete it?');" OnClick="ibtnchargeDelete_Click"/>
                       
              </ItemTemplate>
                </asp:TemplateField>
        

</Columns>
 </asp:GridView> 
                        </div>
                    </div>
                </div>
            </div>
            <asp:Button ID="btnChargePopup" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mpechargedisplay" runat="server" TargetControlID="btnChargePopup" PopupControlID="pn2popup" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pn2popup" runat="server" BackColor="White" Height="400px" Width="780px" style="display:none">
                <table width="60%" style="border:Solid 3px #D55500; width:100%; height:100%">
                    <tr style="background-color:blue">
                        <td colspan="3" style="height:10%; color:White; font-weight:bold; font-size:xx-large" align="left">Add Charge Details</td>
    
</tr>
    <tr>
        <td>
            <asp:Label ID="lblchargeslno" runat="server" visible="false" />
        </td>
    </tr>                
                    <tr>
<td align="left" style="font-size:19px">
InvoiceNumber:

<asp:DropDownList ID="chargeinvoicedropdown" runat="server" style="width:133px;margin-left:6px"/>
</td>


<td align="left" style="font-size:19px">
ChargeBasis :

<asp:DropDownList ID="chargebasisdropdown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chargebasisdropdown_SelectedIndexChanged" style="width:170px;margin-left:6px" >
    <asp:ListItem Text="GrWt" Value="G"></asp:ListItem>
    <asp:ListItem Text="ChWt" Value="C"></asp:ListItem>
    <asp:ListItem Text="Shipment" Value="S"></asp:ListItem>
  
    </asp:DropDownList>
</td>

<td align="left" style="font-size:19px">
Description:

<asp:TextBox ID="txtdescriptioncharge" runat="server" style="width:147px;margin-left:2px"></asp:TextBox>
</td>
</tr>
<tr>
<td align:"left" style="font-size:19px">
    Charge:
<asp:DropdownList ID="dropdowncharge" runat="server"  style="width:140px;margin-left:5px"></asp:DropdownList>
</td>


<td align="left" style="font-size:19px">
Quantity:


<asp:TextBox ID="txtquantity" runat="server" onkeyup="ForeignCurrencyCalculation();"  style="width:175px;margin-left:4px"/>
</td>

    

        
            <td align="left" style="font-size:19px">Currency: 
            
                <asp:DropDownList ID="dropdowncurrency" runat="server" style="width:147px;margin-left:2px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Rate 
            
                <asp:TextBox ID="txtrate" runat="server" onkeyup="ForeignCurrencyCalculation();"  style="width:141px;margin-left:5px" />
            </td>
        
        
            <td align="left" style="font-size:19px">Amount in Foreign Currency 
            
                
                 <asp:TextBox ID="txtamountfc" runat="server" ReadOnly="true" onkeyup="BaseCurrencyCalculation();ForeignCurrencyCalculation();"  style="width:180px;margin-left:2px" />
                
            </td>
        
        
            <td align="left" style="font-size:19px">Exchange Rate 
            
                <asp:TextBox ID="txtexchangerate" runat="server" onkeyup="BaseCurrencyCalculation();" style="width:147px" />
                
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Amount in Base Currency  
            
                
                <asp:TextBox ID="txtamountbc" runat="server"  onkeyup="BaseCurrencyCalculation();TaxandTotalamountCalculation();"  style="width:141px;margin-left:6px" />
            </td>
        
        
        
            <td align="left" style="font-size:19px">Taxable 
            
                <asp:RadioButtonList ID="radiotaxable" RepeatDirection="Horizontal" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radiotaxable_SelectedIndexChanged"  style="width:270px">
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
            </td>
        
            <td align="left" style="font-size:19px"><asp:Label ID="lbltaxname" runat="server" Text="TaxName"></asp:Label> 
            
                <asp:TextBox ID="txttaxname" runat="server" style="width:147px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px"><asp:Label ID="lblpercentage" runat="server" Text="Tax Percentage"></asp:Label> 
            
                <asp:TextBox ID="txtpercentage" runat="server" onkeyup="TaxandTotalamountCalculation();" style="width:141px;margin-left:6px" />
            </td>
        
        
            <td align="left" style="font-size:19px"><asp:Label ID="lbltaxamount" runat="server" Text="TaxAmount"></asp:Label> 
            
                <asp:TextBox ID="txttaxamount" runat="server"  onkeyup="TaxandTotalamountCalculation();" style="width:141px" />
            </td>
        
        
            <td align="left" style="font-size:19px"><asp:Label ID="lbltotalamount" runat="server" Text="Total Amount"></asp:Label> 
            
                <asp:TextBox ID="txttotalamount" runat="server"  onkeyup="TaxandTotalamountCalculation();" style="width:147px" />
               
            </td>
        </tr>
        
            <tr>
                <td>
                    <asp:Button ID="btnChargeAdd" runat="server" CommandName="Add" style="width:70px; margin-right:50px;margin-left:50px;height:40px" Text="Add" OnClick="btnChargeAdd_Click" />
                </td>
                <td>
                    <asp:Button ID="btnChargeCancel" runat="server" style="width:70px;height:40px" Text="Cancel" />
                </td>
            </tr>
                   
                </table>
                
               
            </asp:Panel>
             <asp:Button ID="btnChargeeditpopup" runat="server" style="display:none" />
            <ajaxToolkit:ModalPopupExtender ID="mpechargeeditinvoicedisplay" runat="server" TargetControlID="btnChargeeditPopup" PopupControlID="pn2editpopup" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
            <asp:Panel ID="pn2editpopup" runat="server" BackColor="White" Height="430px" Width="790px" style="display:none">
                <table width="100%" style="border:Solid 3px #D55500; width:100%; height:100%">
                    <tr style="background-color:blue">
                        <td colspan="3" style="height:10%; color:White; font-weight:bold; font-size:xx-large" align="left">Edit Charge Details</td>
    
</tr>
                   <%-- <tr>
                        <td>
                            <asp:Label ID="lblchargeedit" runat="server" Text="editcharge" ></asp:Label>
                        </td>
                        </tr>--%>
                        <tr>
<td align="left" style="font-size:19px;margin-left:2px">
InvoiceNumber:

<asp:DropDownList ID="dropdowneditinvoice" runat="server" style="width:100px;margin-left:5px"/>
</td>

<td align="left" style="font-size:19px">
ChargeBasis :

<asp:DropDownList ID="dropdowneditcharge" runat="server"  OnSelectedIndexChanged="chargebasiseditdropdown_SelectedIndexChanged" style="width:147px" >
    <asp:ListItem Text="GrWt" Value="G"></asp:ListItem>
    <asp:ListItem Text="ChWt" Value="C"></asp:ListItem>
    <asp:ListItem Text="Shipment" Value="S"></asp:ListItem>
  
    </asp:DropDownList>
</td>

<td align="left" style="font-size:19px">
Description:

<asp:TextBox ID="txteditdescription" runat="server" style="width:147px"></asp:TextBox>
</td>
</tr>
                    <tr>
<td align="left" style="font-size:19px">
Charge:

<asp:DropdownList ID="dropdownchargeeditinvoice" runat="server" style="width:141px;margin-left:5px"></asp:DropdownList>
</td>


<td align="left" style="font-size:19px">
Quantity:

<asp:TextBox ID="txteditquantity" runat="server" onkeyup="ForeignCurrencyCalculation();"  style="width:141px"/>
</td>

    
            


        
            <td align="left" style="font-size:19px">Currency: 
            
                <asp:DropDownList ID="dropdowneditcurrency" runat="server" style="width:147px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Rate 
            
                <asp:TextBox ID="txteditrate" runat="server" onkeyup="ForeignCurrencyCalculation();"  style="width:141px;margin-left:5px" />
            </td>
        
        
            <td align="left" style="font-size:19px">Amount in Foreign Currency 
            
                
                 <asp:TextBox ID="txteditamountfc" runat="server"  onkeyup="BaseCurrencyCalculation();ForeignCurrencyCalculation();"  style="width:141px" />
                
            </td>
        
        
            <td align="left" style="font-size:19px">Exchange Rate 
            
                <asp:TextBox ID="txteditexchangerate"  runat="server" onkeyup="BaseCurrencyCalculation();" style="width:147px" />
                
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Amount in Base Currency  
            
                
                <asp:TextBox ID="txteditamountbc" runat="server" onkeyup="BaseCurrencyCalculation();TaxandTotalamountCalculation();"  style="width:141px;margin-left:5px" />
            </td>
        
        
        
            <td align="left" style="font-size:19px">Taxable ?
            
                <asp:RadioButtonList ID="radioedittaxable" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radiotaxableedit_SelectedIndexChanged"  style="width:270px">
                    <asp:ListItem Text="Yes" Value="Y" ></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
                
            </td>
        
        
            <td align="left" style="font-size:19px"><asp:Label ID="lbledittaxname" runat="server" Text="TaxName"></asp:Label>
            
                <asp:TextBox ID="txtedittaxname" runat="server" style="width:147px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px"><asp:Label ID="lbltaxpercentage" runat="server" Text="Tax Percentage"></asp:Label> 
            
                <asp:TextBox ID="txtedittaxpercentage" runat="server" onkeyup="EditTaxandTotalamountCalculation();" style="width:147px;margin-left:5px" />
            </td>
        
        
            <td align="left" style="font-size:19px"><asp:Label ID="lbledittaxamount" runat="server" Text="TaxAmount"></asp:Label> 
            
                <asp:TextBox ID="txtedittaxamount"  runat="server" onkeyup="EditTaxandTotalamountCalculation();" style="width:147px" />
            </td>
        
            <td align="left" style="font-size:19px"><asp:Label ID="lbledittotalamount" runat="server" Text="Total Amount"></asp:Label> 
            
                <asp:TextBox ID="txtedittotalamount" runat="server" onkeyup="EditTaxandTotalamountCalculation();" style="width:147px" />
               
            </td>
        </tr>
        
            <tr>
                <td>
                    <asp:Button ID="btnEdit" runat="server" CommandName="Save" style="width:70px; margin-right:50px;margin-left:50px;height:40px" Text="Update" OnClick="btnUpdateCharge_Click" />
                </td>
                <td>
                    <asp:Button ID="btnEditCancel" runat="server" style="width:70px;height:40px" Text="Cancel" />
                </td>
            </tr>
                   
                </table>
                
               
            </asp:Panel>

            <br />
            <br />
            <asp:Label ID="lbluploadfile" runat="server" Text="Upload File" Visible="false" style="font-size:10px"></asp:Label>
            <br />
            
            <br />
            
            <asp:Label ID="lblMessage" runat="server" Font-Bold="true" Visible="false" style="font-size:30px"></asp:Label>
          </div>
        



       
    </form>


</body>

     <script type="text/javascript">
   
    function BaseCurrencyCalculation()
    {
        var _txt1 = document.getElementById('<%= txtamountfc.ClientID %>');
        var _txt2 = document.getElementById('<%= txtexchangerate.ClientID %>');
        var _txt3 = document.getElementById('<%= txtamountbc.ClientID %>');
        var t1=0, t2=0;
        
        if(_txt1.value != "") t1=_txt1.value;
        if(_txt2.value != "") t2=_txt2.value;
        
        _txt3.value = parseInt(t1) * parseInt(t2);

         var _txt4 = document.getElementById('<%= txteditamountfc.ClientID %>');
        var _txt5 = document.getElementById('<%= txteditexchangerate.ClientID %>');
        var _txt6 = document.getElementById('<%= txteditamountbc.ClientID %>');
        var t3=0, t4=0;
        
        if(_txt4.value != "") t3=_txt4.value;
        if(_txt5.value != "") t4=_txt5.value;
        
        _txt6.value = parseInt(t3) * parseInt(t4);



    }
        function ForeignCurrencyCalculation()
    {
        var _txt1 = document.getElementById('<%= txtrate.ClientID %>');
        var _txt2 = document.getElementById('<%= txtquantity.ClientID %>');
        var _txt3 = document.getElementById('<%= txtamountfc.ClientID %>');
        var t1=0, t2=0;
        
        if(_txt1.value != "") t1=_txt1.value;
        if(_txt2.value != "") t2=_txt2.value;
        
        _txt3.value = parseInt(t1) * parseInt(t2);


             var _txt4 = document.getElementById('<%= txteditrate.ClientID %>');
        var _txt5 = document.getElementById('<%= txteditquantity.ClientID %>');
        var _txt6 = document.getElementById('<%= txteditamountfc.ClientID %>');
        var t3=0, t4=0;
        
        if(_txt4.value != "") t3=_txt4.value;
        if(_txt5.value != "") t4=_txt4.value;
        
        _txt6.value = parseInt(t3) * parseInt(t4);


        }
        function TaxandTotalamountCalculation()
    {
        var _txt1 = document.getElementById('<%= txtamountbc.ClientID %>');
        var _txt2 = document.getElementById('<%= txtpercentage.ClientID %>');
        var _txt3 = document.getElementById('<%= txttaxamount.ClientID %>');
        var _txt4 = document.getElementById('<%= txttotalamount.ClientID %>');
        var t1=0, t2=0, t3=0;
        
        if(_txt1.value != "") t1=_txt1.value;
        if (_txt2.value != "") t2 = _txt2.value;
        
        
        
        _txt3.value = parseInt(t1) * parseInt(t2);
        if (_txt3.value != "") t3 = _txt3.value;
        _txt4.value = parseInt(t1) + parseInt(t3);


        

        }
         function EditTaxandTotalamountCalculation()
         {
             
    
        var _txt1 = document.getElementById('<%= txteditamountbc.ClientID %>');
        var _txt2 = document.getElementById('<%= txtedittaxpercentage.ClientID %>');
        var _txt3 = document.getElementById('<%= txtedittaxamount.ClientID %>');
        var _txt4 = document.getElementById('<%= txtedittotalamount.ClientID %>');
        var t1=0, t2=0, t3=0;
        
        if(_txt1.value != "") t1=_txt1.value;
        if (_txt2.value != "") t2 = _txt2.value;
        
        
        
        _txt3.value = parseInt(t1) * parseInt(t2);
        if (_txt3.value != "") t3 = _txt3.value;
        _txt4.value = parseInt(t1) + parseInt(t3);
         }

    </script>   
    

</html>
