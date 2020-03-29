<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Package.aspx.cs" Inherits="FinalYearProject.Package" %>
<%@Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">  <meta name="viewport" content="width=device-width, initial-scale=1"> 
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
                   <li><a href="Company.aspx" target="_self">Company</a></li>
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
 
    <p class="navbar-text" style="font-size:21px">Package</p>

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
        <div class="table-responsive col-xs-12 nopadding table-shadow">
        <asp:GridView ID="gvPackage" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table-ui table-hover" DataKeyNames="M_Pack_Slno" EmptyDataText="No record found!"
            EmptyDataRowStyle-CssClass="gvEmpty" AllowPaging="True" OnPageIndexChanging="OnPageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:BoundField HeaderText="ID" DataField="M_Pack_Slno" Visible="false" />
                 <asp:BoundField HeaderText="Name" DataField="M_Pack_Name" />
                 <asp:BoundField HeaderText="ShortName" DataField="M_Pack_Sname" />
                <asp:TemplateField HeaderText="View">
                    <ItemTemplate>
                        
                        <asp:ImageButton ID="ibtnView" runat="server" ImageUrl="/images/view.png" OnClick="ibtnView_Click" />
              </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Edit">
                    <ItemTemplate>
                        <asp:ImageButton ID="ibtnEdit" runat="server" ImageUrl="/images/edit1.png" OnClick="ibtnEdit_Click"/>
                        
                      
              </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        
                        
                       <asp:ImageButton ID="ibtnDelete" runat="server" ImageUrl="/images/delete.png" OnClientClick="javascript: return confirm('Do you want to delete it?');" OnClick="ibtnDelete_Click"/>
                       
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
                 <asp:Label ID="lblHeader" runat="server" Text="Add Package" style="font-size:26px;padding:10px;font-style:inherit;font-family:Tahoma" />
                 <span style="float:right">
                    <img id="imgClose" src="/images/close3.png" alt="close1" title="Close1" />
                 </span>
             </div>
        <div>
            <asp:HiddenField ID="packageidAddpopup" runat="server" Value="0" />
            <table border="0" class="table-border" style="min-height:50px;line-height:30px;text-align:left;font-weight:normal;font-size:17px">
                <tr>
                   
                    <td>Name</td>
                    <td><asp:TextBox ID="txtpackagenameAddpopup" runat="server"  /> 
                        </td>
                    <td>
                      <asp:RequiredFieldValidator ID="packnameAddpopup" ControlToValidate="txtpackagenameAddpopup"  ValidationGroup="addpopup"  SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>     
                    </td>
                    

                </tr>
                <tr>
                    <td>ShortName</td>
                    <td><asp:TextBox ID="txtpackageshortnameAddpopup" runat="server" />
                        </td>
                    <td>
                         <asp:RequiredFieldValidator ID="packsnameAddpopup" ControlToValidate="txtpackageshortnameAddpopup" ValidationGroup="addpopup"  SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                       </td>
  
                    
                    

                </tr>
                <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                     <asp:Button ID="btnSaveAddpopup" runat="server" Text=" Save " CausesValidation="true" ValidationGroup="addpopup" style="background-color:#2FBDF1;border:1px solid #0DA9D0"
                          OnClick="btnSave_Click" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancelAddpopup" runat="server" Text="Cancel" style="background-color:#9F9F9F;border:1px solid #5C5C5C" OnClientClick="javascript:$find('mpePackageBehaviourAddpopup').hide();return false;" />
                    

                </td>
                    </tr>
            </table>
        </div>
            </div>
            <div id="pn1EditPopup" runat="server" style="width:500px; height:220px; background-color:#FFFFFF; border:3px solid#0DA9D0;border-radius:12px;padding:0">
             <div id="popupheader1" class="popupHeader1" style="background-color:orange;height:30px;color:white;line-height:30px;text-align:left;font-weight:bold;border-top-left-radius:6px;border-top-right-radius:6px">
                 <asp:Label ID="Label2" runat="server" Text="Edit Package" style="font-size:26px;padding:10px;font-style:inherit;font-family:Tahoma" />
                 <span style="float:right">
                    <img id="imgClose1" src="/images/close3.png" alt="close" title="Close" />
                 </span>
             </div>
        <div>
            <asp:HiddenField ID="packageidEditpopup" runat="server" Value="0"  />
            <table border="0" class="table-border" style="min-height:50px;line-height:30px;text-align:left;font-weight:normal;font-size:17px">
                <tr>
                    <td>Name</td>
                    <td><asp:TextBox ID="txtpackagenameEditpopup" runat="server" /></td>
                    <td> 
                        <asp:RequiredFieldValidator ID="packnameEditpopup" ControlToValidate="txtpackagenameEditpopup" ValidationGroup="editpopup" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>ShortName</td>
                    <td><asp:TextBox ID="txtpackageshortnameEditpopup" runat="server" /></td>
                     <td> 
                        <asp:RequiredFieldValidator ID="packsnameEditpopup" ControlToValidate="txtpackageshortnameEditpopup" ValidationGroup="editpopup" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                     <asp:Button ID="btnSaveEditpopup" runat="server" Text="Save" CausesValidation="true" ValidationGroup="editpopup" style="background-color:#2FBDF1;border:1px solid #0DA9D0"
                          OnClick="btnSave_Click1" />
                    &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="btnCancelEditpopup" runat="server" Text="Cancel" style="background-color:#9F9F9F;border:1px solid #5C5C5C" OnClientClick="javascript:$find('mpePackageBehaviourEditpopup').hide();return false;" />
                    

                </td>
                    </tr>
            </table>
        </div>
               
        </div>
                
        <div id="pn1ViewPopup" runat="server" style="width:550px;height:200px; background-color:#FFFFFF;border:3px solid#0DA9D0;border-radius:12px;padding:0">
            <div id="popupheader2" class="popupHeader2" style="background-color:orange;height:30px;color:white;line-height:30px;text-align:left;font-weight:bold;border-top-left-radius:6px;border-top-right-radius:6px" >
                <asp:Label ID="Label1" runat="server" Text="View Package" style="font-size:26px;padding:10px;font-style:inherit;font-family:Tahoma" />
                <span style="float:right">
                    <asp:ImageButton ID="imgClose2" runat="server" ImageUrl="/images/close3.png" />

                </span>
            </div>
            <div>
                <asp:HiddenField ID="packageidViewpopup" runat="server" Value="0"  />
                <table border="0" class="table-border" style="min-height:50px;line-height:30px;text-align:left;font-weight:normal;font-size:17px">
                    <tr>
                        <td>Name</td>
                        
                        <td><asp:TextBox ID="viewpackagename" runat="server" ReadOnly="true" /></td>

                    </tr>
                     <tr>
                        <td>ShortName</td>
                         &nbsp;
                         
                        <td><asp:TextBox ID="viewpackageshortname" runat="server" ReadOnly="true"/></td>

                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnCancel2" runat="server" Text="Cancel" style="background-color:#2FBDF1;border:1px solid #5C5C5C" OnClientClick="javascript:$find('mpePackageBehaviourViewpopup').hide();return false;" />
                        </td>
                    </tr>

                </table>
            </div>
        </div>
                    
        <ajaxToolkit:ModalPopupExtender ID="mpePackageAddpopup" runat="server" TargetControlID="packageidAddpopup"
            PopupControlID="pn1AddPopup" BehaviorID="mpePackageBehaviourAddpopup" DropShadow="true"
            CancelControlID="imgClose" PopupDragHandleControlID="popupheader" BackgroundCssClass="modalBackground"  />
             <ajaxToolkit:ModalPopupExtender ID="mpePackageEditpopup" runat="server" TargetControlID="packageidAddpopup"
            PopupControlID="pn1EditPopup" BehaviorID="mpePackageBehaviourEditpopup" DropShadow="true"
            CancelControlID="imgClose1" PopupDragHandleControlID="popupheader1" BackgroundCssClass="modalBackground" />
        <ajaxToolkit:ModalPopupExtender ID="mpePackageViewpopup" runat="server" TargetControlID="packageidAddpopup"
            PopupControlID="pn1ViewPopup" BehaviorID="mpePackageBehaviourViewpopup" DropShadow="true"
            CancelControlID="imgClose2" PopupDragHandleControlID="popupheader2" BackgroundCssClass="modalBackground" />
    
    </form>
</body>
</html>
