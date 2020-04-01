
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
        <asp:ScriptManager ID="ScriptManager" runat="server"></asp:ScriptManager>
         <nav class="navbar1 navbar navbar-default" id="navbarone">
  <div class="container-fluid">
    <div class="navbar-header">
      <a class="navbar-brand" href="#">FreightDeals</a>
    </div>
    <ul class="nav navbar-right pull-right top-nav">
              <li class="dropdown">
    <a href="#" class="dropdown-toggle" class="top-menu pull-right" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false"><span class="glyphicon glyphicon-user"></span><span class="caret"></span></a>
    <ul class="dropdown-menu">
    <li>
        <a href=""><i class="fa fa-fw fa-gear"></i> Settings</a>
      </li>
      <li class="divider"></li>
      <li>
        <a href=""><i class="fa fa-fw fa-power-off"></i> Log Out</a>
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
                </ul>
            </div>
                             <div class="btn-group" style="vertical-align:bottom;">

    <button type="button" class="btn btn-default">
                    <span data-bind="label">Buyer</span>
                </button>
                                 <button type="button" class="btn btn-default">
                    <span data-bind="label">Seller</span>
                </button>
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
                                               <div>
    ShipmentNumber : <asp:Label ID="Label1" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
       CustomerName :  <asp:Label ID="Label2" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
        Date :  <asp:Label ID="Label3" runat="server" Text="Label" style="font-size:large"></asp:Label> <br />
        

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
                                              <asp:Button ID="Addbutton" runat="server" Text="Add3" style="width:80px" OnClick="Addbutton_Click" />
                                              <asp:GridView ID="InvoiceGridView" runat="server" GridLines="None" AllowPaging="true" DataKeyNames="TM_INVOICE_Slno" OnRowDataBound="InvoiceDisplay_RowDataBound" AutoGenerateColumns="false">
                                                  <RowStyle BackColor="#EFF3FB" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="White" />
<Columns>

  


<asp:BoundField DataField="TM_INVOICE_Slno" HeaderText="InvoiceSerialNo" Visible="false" />
         
<asp:BoundField DataField="TM_INVOICE_No" HeaderText="Invoice Number" />

<asp:BoundField DataField="TM_INVOICE_ShipmentRefNo" HeaderText="ShipmentReferenceNumber" />
<asp:BoundField DataField="M_Company_Name" HeaderText="Customer" />
<asp:BoundField DataField="TM_INVOICE_Description" HeaderText="Description" />
<asp:BoundField DataField="TM_INVOICE_ShipmentDelivered" HeaderText="Is Delivered?" />
    <asp:TemplateField HeaderText="Edit">
<ItemTemplate>
<asp:ImageButton ID="imgbtnInvoice" ImageUrl="/images/edit1.png" runat="server" Width="25" Height="25" OnClick="ImageButtoninvoice_Click" />
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
<asp:Panel ID="pnlpopup" runat="server" BackColor="Yellow" Height="399px" Width="600px" style="display:none">
<table width="100%" style="border:Solid 3px #D55500; width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr style="background-color:orangered">
<td colspan="2" style=" height:10%; color:White; font-weight:bold; font-size:xx-large" align="left">Add Invoice</td>
    
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
Shipment Reference Number:
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
<asp:DropDownList ID="Customer" runat="server" style="width:270px"></asp:DropDownList>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Description:
</td>
<td>
<asp:TextBox ID="txtDescription" runat="server" style="width:270px"/>
</td>
</tr>
    <tr>
        <td align="left" style="font-size:19px">
            Is delivered?
</td>
<td>
    <asp:CheckBox ID="Isdelivered" runat="server" />
</td>
        <tr>
            <td align="left" style="font-size:19px">Currency: </td>
            <td>
                <asp:TextBox ID="txtdisplaycurrency" runat="server" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td></td>
            <tr>
                <td>
                    <asp:Button ID="btnAddInvoice" runat="server" CommandName="Add" style="width:70px; margin-right:50px;margin-left:50px;height:40px" Text="Add" OnClick="AddInvoiceButton_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCancel" runat="server" style="width:70px;height:40px" Text="Cancel" />
                </td>
            </tr>
        </tr>
    </tr>

</table>
</asp:Panel>
                  <br />
         <asp:Button ID="btnEditPopup" runat="server" style="display:none" />
<ajaxToolkit:ModalPopupExtender ID="mpeInvoiceEdit" runat="server" TargetControlID="btnEditPopup" PopupControlID="pnleditpopup"
CancelControlID="btnCancel" BackgroundCssClass="modalBackground">
</ajaxToolkit:ModalPopupExtender>
<asp:Panel ID="pnleditpopup" runat="server" BackColor="Yellow" Height="399px" Width="600px" style="display:none">
<table width="100%" style="border:Solid 3px #D55500; width:100%; height:100%" cellpadding="0" cellspacing="0">
<tr style="background-color:orangered">
<td colspan="2" style=" height:10%; color:White; font-weight:bold; font-size:xx-large" align="left">Edit Invoice</td>
    
</tr>
    <tr>
        <td> <asp:Label ID="lblchargeinvoice" runat="server" Visible="false"></asp:Label></td>
    </tr>
<tr>
<td align="left" style="font-size:19px">
InvoiceNumber:
</td>
<td>
<asp:TextBox ID="txtinvoicenumbereditpopup" runat="server" style="width:270px"/>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Shipment Reference Number:
</td>
<td>
<asp:TextBox ID="txtshipmentrefnoeditpopup" runat="server" style="width:270px"/>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Customer:
</td>
<td>
<asp:DropDownList ID="Customereditpopup" runat="server" style="width:270px"></asp:DropDownList>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Description:
</td>
<td>
<asp:TextBox ID="txtdescriptioneditpopup" runat="server" style="width:270px"/>
</td>
</tr>
    <tr>
        <td align="left" style="font-size:19px">
            Is delivered?
</td>
<td>
    <asp:CheckBox ID="Isdeliverededitpopup"  runat="server" />
</td>
        <tr>
            <td align="left" style="font-size:19px">Currency: </td>
            <td>
                <asp:TextBox ID="txtcurrencyeditpopup" runat="server" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td></td>
            <tr>
                <td>
                    <asp:Button ID="UpdateInvoice" runat="server" style="width:70px;height:40px" Text="Update" OnClick="UpdateInvoice_Click" />
                </td>
                <td>
                    <asp:Button ID="btnCanceledit" runat="server" style="width:70px;height:40px" Text="Cancel" />
                </td>
            </tr>
        </tr>
    </tr>

</table>
</asp:Panel>
                  <br />
        <div class="panel-group" id="accordion2" role="tablist" aria-multiselectable="true">
            <div class="panel panel-default">
                <div class="panel-heading" role="tab" id="headingThree">
                    <h4 class="panel-title">
                        <a role="button" data-toggle="collapse" data-parent="#accordion" href="collapseThree" aria-expanded="false" aria-controls="collapseThree">
                            <i class="more-less glyphicon glyphicon-plus"></i>
                            Enter Charge Details
                        </a>
                    </h4>
                </div>
                <div id="collapseThree" class="panel panel-collapse" role="tabpanel" aria-labelledby="headingThree">
                    <div class="panel-body">
                        <div>
                            <asp:Button ID="AddbuttonCharge" runat="server" Text="Add" style="width:80px" OnClick="AddbuttonCharge_Click" />
                             <asp:GridView ID="gvChargeInvoice" runat="server" OnRowDataBound="gvChargeInvoice_RowDataBound" GridLines="None" AllowPaging="true" DataKeyNames="TD_INVOICE_Slno" AutoGenerateColumns="false">
                                                  <RowStyle BackColor="#EFF3FB" />
<FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
<HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
<AlternatingRowStyle BackColor="White" />
<Columns>

<asp:BoundField DataField="TD_INVOICE_SLNO" HeaderText="InvoiceSlno" Visible="false" />    
<asp:BoundField DataField="TD_INVOICE_TMSLNO" HeaderText="Invoice Number" />
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
    
    <asp:TemplateField HeaderText="Edit">
<ItemTemplate>
<asp:ImageButton ID="imgbtnCharge" ImageUrl="/images/edit1.png" runat="server" Width="25" Height="25" OnClick="ImagebuttonCharge_Click" />
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
            <asp:Panel ID="pn2popup" runat="server" BackColor="Yellow" Height="599px" Width="750px" style="display:none">
                <table width="100%" style="border:Solid 3px #D55500; width:100%; height:100%">
                    <tr style="background-color:orangered">
                        <td colspan="2" style="height:10%; color:White; font-weight:bold; font-size:xx-large" align="left">Add Charge Details</td>
    
</tr>
                    <tr>
<td align="left" style="font-size:19px">
InvoiceNumber:
</td>
<td>
<asp:DropDownList ID="chargeinvoicedropdown" runat="server" style="width:270px"/>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
ChargeBasis :
</td>
<td>
<asp:DropDownList ID="chargebasisdropdown" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chargebasisdropdown_SelectedIndexChanged" style="width:270px" >
    <asp:ListItem Text="GrWt" Value="G"></asp:ListItem>
    <asp:ListItem Text="ChWt" Value="C"></asp:ListItem>
    <asp:ListItem Text="Shipment" Value="S"></asp:ListItem>
  
    </asp:DropDownList>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Description:
</td>
<td>
<asp:TextBox ID="txtdescriptioncharge" runat="server" style="width:270px"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Quantity:
</td>
<td>
<asp:TextBox ID="txtquantity" runat="server" onkeyup="ForeignCurrencyCalculation();"  style="width:270px"/>
</td>
</tr>
    <tr>
        <td align="left" style="font-size:19px">
            
</td>

        <tr>
            <td align="left" style="font-size:19px">Currency: </td>
            <td>
                <asp:DropDownList ID="dropdowncurrency" runat="server" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Rate </td>
            <td>
                <asp:TextBox ID="txtrate" runat="server" onkeyup="ForeignCurrencyCalculation();"  style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Amount in Foreign Currency </td>
            <td>
                
                 <asp:TextBox ID="txtamountfc" runat="server" onkeyup="BaseCurrencyCalculation();ForeignCurrencyCalculation();"  style="width:270px" />
                
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Exchange Rate </td>
            <td>
                <asp:TextBox ID="txtexchangerate" runat="server" onkeyup="BaseCurrencyCalculation();" style="width:270px" />
                
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Amount in Base Currency  </td>
            <td>
                
                <asp:TextBox ID="txtamountbc" runat="server" onkeyup="BaseCurrencyCalculation();TaxandTotalamountCalculation();"  style="width:270px" />
            </td>
        </tr>
        
        <tr>
            <td align="left" style="font-size:19px">Taxable </td>
            <td>
                <asp:RadioButtonList ID="radiotaxable" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radiotaxable_SelectedIndexChanged"  style="width:270px">
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px"><asp:Label ID="lbltaxname" runat="server" Text="TaxName"></asp:Label> </td>
            <td>
                <asp:TextBox ID="txttaxname" runat="server" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px"><asp:Label ID="lblpercentage" runat="server" Text="Tax Percentage"></asp:Label> </td>
            <td>
                <asp:TextBox ID="txtpercentage" runat="server" onkeyup="TaxandTotalamountCalculation();" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px"><asp:Label ID="lbltaxamount" runat="server" Text="TaxAmount"></asp:Label> </td>
            <td>
                <asp:TextBox ID="txttaxamount" runat="server" onkeyup="TaxandTotalamountCalculation();" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px"><asp:Label ID="lbltotalamount" runat="server" Text="Total Amount"></asp:Label> </td>
            <td>
                <asp:TextBox ID="txttotalamount" runat="server" onkeyup="TaxandTotalamountCalculation();" style="width:270px" />
               
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
            <asp:Panel ID="pn2editpopup" runat="server" BackColor="Yellow" Height="599px" Width="750px" style="display:none">
                <table width="100%" style="border:Solid 3px #D55500; width:100%; height:100%">
                    <tr style="background-color:orangered">
                        <td colspan="2" style="height:10%; color:White; font-weight:bold; font-size:xx-large" align="left">Edit Charge Details</td>
    
</tr>
                    <tr>
<td align="left" style="font-size:19px">
InvoiceNumber:
</td>
<td>
<asp:DropDownList ID="dropdowneditinvoice" runat="server" style="width:270px"/>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
ChargeBasis :
</td>
<td>
<asp:DropDownList ID="dropdowneditcharge" runat="server" AutoPostBack="true" OnSelectedIndexChanged="chargebasisdropdown_SelectedIndexChanged" style="width:270px" >
    <asp:ListItem Text="GrWt" Value="G"></asp:ListItem>
    <asp:ListItem Text="ChWt" Value="C"></asp:ListItem>
    <asp:ListItem Text="Shipment" Value="S"></asp:ListItem>
  
    </asp:DropDownList>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Description:
</td>
<td>
<asp:TextBox ID="txteditdescription" runat="server" style="width:270px"></asp:TextBox>
</td>
</tr>
<tr>
<td align="left" style="font-size:19px">
Quantity:
</td>
<td>
<asp:TextBox ID="TextBox2" runat="server" onkeyup="ForeignCurrencyCalculation();"  style="width:270px"/>
</td>
</tr>
    <tr>
        <td align="left" style="font-size:19px">
            
</td>

        <tr>
            <td align="left" style="font-size:19px">Currency: </td>
            <td>
                <asp:DropDownList ID="DropDownList3" runat="server" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Rate </td>
            <td>
                <asp:TextBox ID="TextBox3" runat="server" onkeyup="ForeignCurrencyCalculation();"  style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Amount in Foreign Currency </td>
            <td>
                
                 <asp:TextBox ID="TextBox4" runat="server" onkeyup="BaseCurrencyCalculation();ForeignCurrencyCalculation();"  style="width:270px" />
                
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Exchange Rate </td>
            <td>
                <asp:TextBox ID="TextBox5" runat="server" onkeyup="BaseCurrencyCalculation();" style="width:270px" />
                
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px">Amount in Base Currency  </td>
            <td>
                
                <asp:TextBox ID="TextBox6" runat="server" onkeyup="BaseCurrencyCalculation();TaxandTotalamountCalculation();"  style="width:270px" />
            </td>
        </tr>
        
        <tr>
            <td align="left" style="font-size:19px">Taxable </td>
            <td>
                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="radiotaxable_SelectedIndexChanged"  style="width:270px">
                    <asp:ListItem Text="Yes" Value="Y"></asp:ListItem>
                    <asp:ListItem Text="No" Value="N"></asp:ListItem>
                    </asp:RadioButtonList>
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px"><asp:Label ID="Label4" runat="server" Text="TaxName"></asp:Label> </td>
            <td>
                <asp:TextBox ID="TextBox7" runat="server" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px"><asp:Label ID="Label5" runat="server" Text="Tax Percentage"></asp:Label> </td>
            <td>
                <asp:TextBox ID="TextBox8" runat="server" onkeyup="TaxandTotalamountCalculation();" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px"><asp:Label ID="Label6" runat="server" Text="TaxAmount"></asp:Label> </td>
            <td>
                <asp:TextBox ID="TextBox9" runat="server" onkeyup="TaxandTotalamountCalculation();" style="width:270px" />
            </td>
        </tr>
        <tr>
            <td align="left" style="font-size:19px"><asp:Label ID="Label7" runat="server" Text="Total Amount"></asp:Label> </td>
            <td>
                <asp:TextBox ID="TextBox10" runat="server" onkeyup="TaxandTotalamountCalculation();" style="width:270px" />
               
            </td>
        </tr>
        
            <tr>
                <td>
                    <asp:Button ID="Button24" runat="server" CommandName="Add" style="width:70px; margin-right:50px;margin-left:50px;height:40px" Text="Add" OnClick="btnChargeAdd_Click" />
                </td>
                <td>
                    <asp:Button ID="Button3" runat="server" style="width:70px;height:40px" Text="Cancel" />
                </td>
            </tr>
                   
                </table>
                
               
            </asp:Panel>

            
          </div>
        
    </form>
</body>
</html>
