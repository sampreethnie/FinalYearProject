<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Country.aspx.cs"  Inherits="FinalYearProject.Country" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
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

    
                    
                                 
                                 <button type="button" id="ButtonBuyer" runat="server" class="btn btn-default dropdown-toggle"  data-toggle="dropdown">
                                     Buyer
    <span class="caret"></span>
    <span class="sr-only">Toggle Dropdown</span>
  </button>
  <ul class="dropdown-menu" role="menu">
    <!-- here is the asp.net link button to make post back -->
          
    <li><a href="ShipmentDelivery(Seller).aspx">Shipmentdelivery</a></li>
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
          
    <li><a href="ShipmentDetails(Buyer).aspx">ShipmentDetails</a></li>
    <li><a href="#">SellerQuote</a></li>
   
  </ul>
                                 </div>
        </div> 
             
            </nav>

         <nav class="navbar3 navbar navbar-dark bg-primary" style="height:20px;"id="navbarthree"> 
 
    <p class="navbar-text" style="font-size:21px">Country</p>

     <div class="input-group topspace5 bottomspace5">
             <asp:TextBox ID="txtSearch" CssClass="pull-right" Width="200px" placeholder="Search" runat="server" />
<span class="input-group-addon nbrd" style="padding:0px 4px">
         <asp:ImageButton ID="btnAdd" runat="server" CssClass="pull-right" width="20" Height="20" ImageUrl ="/images/add.png" OnClick="btnAdd_Click"/> 
    </span>
 <span class="input-group-addon nbrd" style="padding:0px 4px">
         <asp:ImageButton ID="btnRefresh" runat="server" CssClass="pull-right" width="20" Height="20" ImageUrl="/images/refresh.png" OnClick="Refresh" /> 
                </span>

         <span class="input-group-addon nbrd" style="padding:0px 4px">
 
          <asp:ImageButton ID="btnSearch" runat="server" CssClass="pull-right" width="20" Height="20" ImageUrl="/images/search.png" OnClick="Search" />
             </span>
                </div>  
           </nav>
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <asp:UpdatePanel ID="upn1Users" runat="server">
            <ContentTemplate>

            </ContentTemplate>
        </asp:UpdatePanel>
        <!- GridView Code HTML to show country -- >
        <div class="table-responsice col-xs-12 no padding table-shadow">
            <asp:GridView ID="gvCountry" runat="server" OnRowDataBound="gvCountry_RowDataBound" AutoGenerateColumns="false" Width="100%"
                CssClass="table-ui table-hover" DataKeyNames="M_Country_Code" EmptyDataText="No record found!!"
                EmptyDataRowStyle-CssClass="gvEmpty" 
                CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="OnPageIndexChanging" >
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:BoundField HeaderText="ID" DataField="M_Country_Code" Visible="false"/>
                     <asp:BoundField HeaderText="Name" DataField="M_Country_Name"  />
                    <asp:BoundField HeaderText="Shortname" DataField="M_Country_Sname"  />
                                      


                    
                      <asp:TemplateField HeaderText="Currency">
                        <ItemTemplate>
                            <asp:Label ID="CurrencyData" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="ISD" DataField="M_Country_Isd"  />
                    <asp:TemplateField HeaderText="View">
                        <ItemTemplate>
                            <asp:ImageButton ID="ibtnView" runat="server" ImageUrl="/images/view.png" OnClick="ibtnView_Click" />

                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="/images/edit1.png" OnClick="ibtnEdit_Click" />

                        </ItemTemplate>
                    </asp:TemplateField>
                   <asp:TemplateField HeaderText="Delete">
                        <ItemTemplate>
                            <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="/images/delete.png"  OnClientClick="javascript: return confirm('Do you want to delete it?');" OnClick="ibtnDelete_Click" />

                        </ItemTemplate>
                    </asp:TemplateField>
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
        </div>
        <div id="pn1AddPopup" runat="server" style="width:500px; height:220px; background-color:#FFFFFF; border:3px solid#0DA9D0;border-radius:12px;padding:0">
 <div id="popupheader" class="popupHeader" style="background-color:orange;height:30px;color:white;line-height:30px;text-align:left;font-weight:bold;border-top-left-radius:6px;border-top-right-radius:6px">
 <asp:Label ID="lblHeader" runat="server" Text="Add Country" style="font-size:26px;padding:10px;font-style:inherit;font-family:Tahoma" />
 <span style="float:right">
 <img id="imgClose" src="/images/close3.png" alt="close1" title="Close1"/>
 </span>
 </div>
            <div>
                <asp:HiddenField ID="countryidAddpopup" runat="server" Value="0" />
 <table border="0" class="table-border" style="min-height:50px;line-height:30px;text-align:left;font-weight:normal;font-size:17px">
            <tr>
                <td>Name</td>
                <td><asp:TextBox ID="txtcountrynameAddpopup" runat="server" /></td>
                <td><asp:RequiredFieldValidator ID="countrynameAddpopup" ControlToValidate="txtcountrynameAddpopup" ValidationGroup="addpopup1" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>

            </tr>
     <tr>
 <td>ShortName</td>
 <td><asp:TextBox ID="txtcountryshortnameAddpopup" runat="server" />
 </td>
 <td>
 <asp:RequiredFieldValidator ID="countrysnameAddpopup"
ControlToValidate="txtcountryshortnameAddpopup" ValidationGroup="addpopup1" SetFocusOnError="true"
EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
 </td>


 </tr>
     <tr>
         <td> Currency </td>
         <td>
             <asp:DropDownList ID="currencydropdownlistAddpopup" AutoPostback="true" runat="server" OnSelectedIndexChanged="currencydropdownlistAddpopup_SelectedIndexChanged" >
               
             </asp:DropDownList> 
             <asp:RequiredFieldValidator ID="currlist" Text="(Required)" InitialValue="0" ControlToValidate="currencydropdownlistAddpopup" ValidationGroup="addpopup1" runat="server" />
         </td>
      </tr>

     <tr>
         <td> ISD

         </td>
         <td>
             <asp:TextBox ID="txtcountryisdAddpopup" runat="server"  />
         </td>
         <td>
              <asp:RequiredFieldValidator ID="countryisdAddpopup"
ControlToValidate="txtcountryisdAddpopup" ValidationGroup="addpopup1" SetFocusOnError="true"
EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
             <asp:RangeValidator ID ="RangeValidator1" Type="Integer" ValidationGroup="addpopup1" MinimumValue="9" MaximumValue="1000" ControlToValidate="txtcountryisdAddpopup" runat="server" ErrorMessage="Enter Valid Isd" SetFocusOnError="true"></asp:RangeValidator>
         </td>
     </tr>
     <tr>
 <td>
 &nbsp;
 </td>
 <td>


     <asp:Button ID="AddpopupSaveButton" runat="server" Text="Save" style="background-color:#2FBDF1;border:1px solid #0DA9D0"
                        onclick="AddpopupSaveButton_Click" CausesValidation="true" ValidationGroup="addpopup1" />
 &nbsp; &nbsp; &nbsp;
 <asp:Button ID="btnCancel" runat="server" Text="Cancel"
style="background-color:#9F9F9F;border:1px solid #5C5C5C" OnClientClick="javascript:
$find('mpeCountryBehaviour').hide();return false;" />

 </td>
 </tr>
 </table>

     </div>
            </div>

        <div id="pn1EditPopup" runat="server" style="width:500px; height:220px; background-color:#FFFFFF; border:3px solid#0DA9D0;border-radius:12px;padding:0">
 <div id="popupheader1" class="popupHeader1" style="background-color:orange;height:30px;color:white;line-height:30px;text-align:left;font-weight:bold;border-top-left-radius:6px;border-top-right-radius:6px">
 <asp:Label ID="Label1" runat="server" Text="Edit Country" style="font-size:26px;padding:10px;font-style:inherit;font-family:Tahoma" />
 <span style="float:right">
 <img id="imgClose1" src="/images/close3.png" alt="close1" title="Close"/>
 </span>
 </div>
            <div>
                <asp:HiddenField ID="countryidEditpopup" runat="server" Value="0"  />
 <table border="0" class="table-border" style="min-height:50px;line-height:30px;text-align:left;font-weight:normal;font-size:17px">
            <tr>
                <td>Name</td>
                <td><asp:TextBox ID="txtcountrynameEditpopup" runat="server" /></td>
                <td><asp:RequiredFieldValidator ID="countrynameEditpopup" ControlToValidate="txtcountrynameEditpopup" ValidationGroup="editpopup1" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>

            </tr>
     <tr>
 <td>ShortName</td>
 <td><asp:TextBox ID="txtcountryshortnameEditpopup" runat="server" />
 </td>
 <td>
 <asp:RequiredFieldValidator ID="countrysnameEditpopup"
ControlToValidate="txtcountryshortnameEditpopup" ValidationGroup="editpopup1" SetFocusOnError="true"
EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
 </td>


 </tr>
     <tr>
         <td> Currency </td>
         <td>
             <asp:DropDownList ID="currencydropdownlistEditpopup" runat="server" AutoPostBack="true"  OnSelectedIndexChanged="currencydropdownlistEditpopup_SelectedIndexChanged" >
                 </asp:DropDownList>
             <asp:RequiredFieldValidator ID="editcurr" Text="(Required)" ValidationGroup="editpopup1" InitialValue="0" ControlToValidate="currencydropdownlistEditpopup" runat="server" /> 
         </td>
      </tr>

     <tr>
         <td> ISD

         </td>
         <td>
             <asp:TextBox ID="txtcountryisdEditpopup" runat="server" > </asp:TextBox>
            
            
         </td>
         <td>
             <asp:RequiredFieldValidator ID="countryisdEditpopup"
ControlToValidate="txtcountryisdEditpopup" ValidationGroup="editpopup1" SetFocusOnError="true"
EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
              <asp:RangeValidator ID ="RangeValidator2" Type="Integer" MinimumValue="9" MaximumValue="1000" ControlToValidate="txtcountryisdEditpopup" runat="server" ErrorMessage="Enter Valid Isd"></asp:RangeValidator>
         </td>
     </tr>
     <tr>
 <td>
 &nbsp;
 </td>
 <td>

     <asp:Button ID ="EditpopupSaveButton" runat="server" Text="Save" CausesValidation="true" style="background-color:#2FBDF1;border:1px solid #0DA9D0" OnClick="EditpopupSaveButton_Click" />
 &nbsp; &nbsp; &nbsp;
 <asp:Button ID="btnCancel1" runat="server" Text="Cancel"
style="background-color:#9F9F9F;border:1px solid #5C5C5C" OnClientClick="javascript:
$find('mpeCountryBehaviour').hide();return false;" />

 </td>
 </tr>
 </table>

     </div>
            </div>
        <div id="pn1ViewPopup" runat="server" style="width:500px; height:220px; background-color:#FFFFFF; border:3px solid#0DA9D0;border-radius:12px;padding:0">
 <div id="popupheader3" class="popupHeader3" style="background-color:orange;height:30px;color:white;line-height:30px;text-align:left;font-weight:bold;border-top-left-radius:6px;border-top-right-radius:6px">
 <asp:Label ID="Label2" runat="server" Text="View Country" style="font-size:26px;padding:10px;font-style:inherit;font-family:Tahoma" />
 <span style="float:right">
 <img id="imgClose2" src="/images/close3.png" alt="close1" title="Close1"/>
 </span>
 </div>
            <div>
                <asp:HiddenField ID="countryidViewpopup" runat="server"  />
 <table border="0" class="table-border" style="min-height:50px;line-height:30px;text-align:left;font-weight:normal;font-size:17px">
            <tr>
                <td>Name</td>
                <td><asp:TextBox ID="txtcountrynameViewpopup" runat="server" ReadOnly="true" /></td>
                <td><asp:RequiredFieldValidator ID="countrynameViewpopup" ControlToValidate="txtcountrynameViewpopup" ValidationGroup="viewpopup" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator></td>

            </tr>
     <tr>
 <td>ShortName</td>
 <td><asp:TextBox ID="txtcountryshortnameViewpopup" runat="server" ReadOnly="true" />
 </td>
 <td>
 <asp:RequiredFieldValidator ID="countrysnameViewpopup"
ControlToValidate="txtcountryshortnameViewpopup" ValidationGroup="viewpopup" SetFocusOnError="true"
EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
 </td>


 </tr>
     <tr>
         <td> Currency </td>
         <td>
             <asp:DropDownList ID="currencydropdownlistViewpopup" runat="server" AutoPostBack="true"  /> 
         </td>
      </tr>

     <tr>
         <td> ISD

         </td>
         <td>
             <asp:TextBox ID="txtcountryisdViewpopup" runat="server" ReadOnly="true" />
         </td>
     </tr>
     <tr>
 <td>
 &nbsp;
 </td>
 <td>
 
 &nbsp; &nbsp; &nbsp;
 <asp:Button ID="btnCancel2" runat="server" Text="Cancel"
style="background-color:#9F9F9F;border:1px solid #5C5C5C" OnClientClick="javascript:
$find('mpeCountryBehaviour').hide();return false;" />

 </td>
 </tr>
 </table>

     </div>
            </div>

        <ajaxToolkit:ModalPopupExtender ID="mpeCountryAdd" runat="server"
TargetControlID="countryidAddpopup"
 PopupControlID="pn1AddPopup" BehaviorID="mpeCountryBehaviour" DropShadow="true"
 CancelControlID="imgClose" PopupDragHandleControlID="popupheader"
BackgroundCssClass="modalBackground" />
 <ajaxToolkit:ModalPopupExtender ID="mpeCountryEdit" runat="server"
TargetControlID="countryidAddpopup"
 PopupControlID="pn1EditPopup" BehaviorID="mpeCountryBehaviour1" DropShadow="true"
 CancelControlID="imgClose1" PopupDragHandleControlID="popupheader1"
BackgroundCssClass="modalBackground" />
 <ajaxToolkit:ModalPopupExtender ID="mpeCountryView" runat="server"
TargetControlID="countryidAddpopup"
 PopupControlID="pn1ViewPopup" BehaviorID="mpeCountryBehaviour2" DropShadow="true"
 CancelControlID="imgClose2" PopupDragHandleControlID="popupheader2"
BackgroundCssClass="modalBackground" />
        
    </form>
</body>
</html>
 