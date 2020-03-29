<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Mainpage.aspx.cs" Inherits="FinalYearProject.Mainpage" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" >
        function Validate()
        {
            var EmailID = document.getElementById('txtEmail');
            var Password = document.getElementById('txtPassword');
            if((EmailID.value=='') || (Password.value==''))
            {
                alert("EmailID and password should not be blank");
                return false;
            }
            else {
                return true;
            }
        }

    </script>
    <meta charset="utf-8">
  <meta name="viewport" content="width=device-width, initial-scale=1">
 <link type="text/css" rel="stylesheet" href="../css/bootstrap.min.css" />
<%--<link type="text/css" rel="stylesheet" href="../css/fd-common.css" />--%>
<%--<link type="text/css" rel="stylesheet" href="../css/custom.css" />--%>

    
   
    <style type="text/css">
        
   .panel-group .panel {
        border-radius: 0;
        box-shadow: none;
        border-color: #EEEEEE;
    }

    .panel-default > .panel-heading {
        padding: 0;
        border-radius: 0;
        color: #212121;
        background-color: #FAFAFA;
        border-color: #EEEEEE;
    }

    .panel-title {
        font-size: 14px;
    }

    .panel-title > a {
        display: block;
        padding: 15px;
        text-decoration: none;
    }

    .more-less {
        float: right;
        color: #212121;
    }

    .panel-default > .panel-heading + .panel-collapse > .panel-body {
        border-top-color: #EEEEEE;
    }


    </style>
    
    
</head>
<body>
    <form id="form1" runat="server">
         <nav class="navbar navbar-inverse">
  <div class="container demo">
    <div class="navbar-header">
      <a class="navbar-brand" href="#">FreightDeals</a>
    </div>
    <ul class="nav navbar-nav navbar-right">
       <%-- <li> <button type="button" class="glyphicon glyphicon-log-in" data-toggle="modal" data-target="#loginModal">Login</button></li>--%>
     <li><a href="#RegistrationModal" data-toggle="modal" data-target="#RegistrationModal"><span class="glyphicon glyphicon-user"></span> Signup </a></li>
     <li><a href="#loginModal" data-toggle="modal" data-target="#loginModal"><span class="glyphicon glyphicon-log-in"></span> Login </a></li>
     </ul>
   </div>
 </nav>
        <div class="modal fade" role="dialog" id="loginModal">
            <div class="modal-dialog">
                <div class="modal-content">
                <div class="modal-header">
                   
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                     <h4 class="modal-title">Login </h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                       <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Email:"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="emailid" ControlToValidate="txtEmail" ValidationGroup="login" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="Please enter username"></asp:RequiredFieldValidator>
                    </div>
                    <div class="form-group">
                         <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Password:"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="password" ControlToValidate="txtPassword" ValidationGroup="login" SetFocusOnError="true" EnableClientScript="true" runat="server" ErrorMessage="Please enter password"></asp:RequiredFieldValidator>
                    </div>
                </div>
                <div class="modal-footer">
                   <asp:Button ID="btnLogin" Text="Login" runat="server" class="btn btn-primary" OnClick="btnlogin" OnClientClick="Validate()" CausesValidation="true" ValidationGroup="login"  />
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" data-dismiss="modal" class="btn btn-default"  />
                   <asp:Label ID="lblmessage" runat="server" />
                </div>
            </div>
        </div>
            </div>
        <div class="modal fade " role="dialog"  id="RegistrationModal">
            <div class="modal-dialog" style="width:840px">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Sign up</h4>

                    </div>
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
                                                       <br> <asp:RadioButtonList ID="categorylist" runat="server"  >
                                                           <asp:ListItem Value="b">Buyer</asp:ListItem>
                                                           <asp:ListItem Value="s">Seller</asp:ListItem>
                                                            </asp:RadioButtonList>
                                                   </label>
                                               </div>
                                               
                                               <div>
                                                   
                                                   <label>Company Name *<br> <asp:TextBox ID="txtcompanyname" runat="server" CssClass="form-control"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                   <asp:RequiredFieldValidator ID="companyname" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtcompanyname" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                                    <label>Address1 *<br> <asp:TextBox ID="txtaddress1" Width="400px" CssClass="form-control" Height="34px"  runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="addressone" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtaddress1" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                                   <label>Address2<br> <asp:TextBox ID="txtaddress2" Width="400px" CssClass="form-control" Height="34px"  runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                   <label>Landmark<br> <asp:TextBox ID="txtlandmark" Width="400px" CssClass="form-control" Height="34px" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                    <label>City *<br> <asp:TextBox ID="txtcity" runat="server" CssClass="form-control"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="city" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtcity" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                                      </div>
                                               <div>
                                               <label>ZipCode *<br> <asp:TextBox ID="txtpin" runat="server" CssClass="form-control"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="pin" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtpin" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                               <label>Country *<br> <asp:DropDownList ID="dropdowncountry" OnSelectedIndexChanged="OnSelectedIndexChangedCountry" CssClass="form-control" AutoPostBack="true" Width="140px" runat="server"></asp:DropDownList> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="countrylist" runat="server" InitialValue="0" Text="" ValidationGroup="registrationform" ControlToValidate="dropdowncountry"  ErrorMessage=""></asp:RequiredFieldValidator>
                                                <label>State<br> <asp:DropDownList ID="dropdownstate" OnSelectedIndexChanged="OnSelectedIndexChangedState" Width="140px" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="statelist" runat="server" InitialValue="0" Text="" ValidationGroup="registrationform" ControlToValidate="dropdownstate"  ErrorMessage=""></asp:RequiredFieldValidator>
                                                <label>Currency *<br> <asp:DropDownList ID="dropdowncurrency" Width="140px" OnSelectedIndexChanged="OnSelectedIndexChangedCurrency" CssClass="form-control" AutoPostBack="true" runat="server"></asp:DropDownList> </label>
                                                   &nbsp;&nbsp;
                                                    <asp:RequiredFieldValidator ID="currencylist" runat="server" InitialValue="0" Text="" ValidationGroup="registrationform" ControlToValidate="dropdowncurrency"  ErrorMessage=""></asp:RequiredFieldValidator>
                                                   </div>
                                               <div>
                                                    <label>PAN<br> <asp:TextBox ID="txtpan" CssClass="form-control" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                    <label>TIN<br> <asp:TextBox ID="txttin" CssClass="form-control" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                    <label>TAN<br> <asp:TextBox ID="txttan" CssClass="form-control" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                   <label>CompanyURL *<br> <asp:TextBox ID="txtcompanyurl" CssClass="form-control" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                                   <asp:RequiredFieldValidator ID="companyurl" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtcompanyurl" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>

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
                                              <label>Name *<br> <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;&nbsp;
                                               <asp:RequiredFieldValidator ID="name" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtname" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                                    <label>Mobile *<br> <asp:TextBox ID="txtmobile" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;&nbsp;
                                               <asp:RequiredFieldValidator ID="mobileno" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtmobile" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                               <label>UserId *<br> <asp:TextBox ID="txtuserid" CssClass="form-control" runat="server"></asp:TextBox> </label>
                                                   &nbsp;&nbsp;
                                               <asp:RequiredFieldValidator ID="userid" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtuserid" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                               
                                                       
                                              </div>
                                              <div>
                                                   
                                                  <label>Password *<br> <asp:TextBox ID="txtpassword1" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox> </label>
                                              <asp:RequiredFieldValidator ID="passwordfield" runat="server" ValidationGroup="registrationform" SetFocusOnError="true" ControlToValidate="txtpassword1" EnableClientScript="true" ErrorMessage=""></asp:RequiredFieldValidator>
                                                   </div>
                                          </div>
                                      </div>
                                  </div>
                             <br />
                        <asp:Button ID="submitbutton1" runat="server" CssClass="btn btn-primary" Text="Submit" OnClick="btnSubmit" CausesValidation="true" ValidationGroup="registrationform" />
                        <asp:Button ID="resetbutton" runat="server" CssClass="btn btn-danger" Text="Reset" />
                        <asp:Button ID="cancelbutton" runat="server" data-dismiss="modal" CssClass="btn btn-default" Text="Cancel" />

                                  </div>
                              </div>
                    </div> 
        </div>
        </div>
        <script type="text/javascript" src="../js/jquery-1.11.3.min.js"></script>
<script type="text/javascript" src="../js/bootstrap.min.js"></script>
        <script type="text/javascript">
            function toggleIcon(e) {
                $(e.target)
                    .prev('.panel-heading')
                    .find(".more-less")
                    .toggleClass('glyphicon-plus glyphicon-minus');

            }

            $('.panel-group').on('hidden.bs.collapse', toggleIcon);
            $('.panel-group').on('shown.bs.collapse', toggleIcon);
    </script>
    </form>
</body>
</html>
