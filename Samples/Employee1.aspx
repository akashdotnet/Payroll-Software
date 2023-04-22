<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employee1.aspx.cs" Inherits="Samples.Employee1" MasterPageFile="~/Main.Master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.7.2.min.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <link href="../css/fixedHeader.bootstrap4.min.css" rel="stylesheet" />
    <link href="../AdminLTE-3.0.1/plugins/toastr/toastr.min.css" rel="stylesheet" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminLTE-3.0.1/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css">
    <script src="../AdminLTE-3.0.1/plugins/datatables/jquery.dataTables.min.js"></script>
    <%--<script src="../AdminLTE-3.0.1/plugins/datatables/dataTables.bootstrap.min.js"></script>--%>
    <link rel="stylesheet" href="../AdminLTE-3.0.1/plugins/datatables-bs4/css/dataTables.bootstrap4.css">
    <link href="../AdminLTE-3.0.1/plugins/icheck-bootstrap/icheck-bootstrap.min.css" rel="stylesheet" />
    <script src="../js/jquery.maskedinput-1.3.1.min_.js"></script>
    <style>
        .panel-body {
            background-color: ghostwhite !important;
        }

        .add-error {
            box-sizing: border-box;
            border: 2px solid red;
            border-radius: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">

    <form method="post" id="EmployeeInfo" enctype="multipart/form-data" runat="server">
        <input type="hidden" id="hfEmployeId" />
         <input type="hidden" id="hfSalaryTemplateId" />
        <section class="content">
            <div class="tab-pane fade show active card-body" id="custom-content-above-home" role="tabpanel" aria-labelledby="custom-content-above-home-tab">
                    <h2 class="lead mb-0">General Information</h2>
                    <div class="tab-custom-content">
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            Company 
                                                        <br />
                            <asp:DropDownList ID="ddlcompany" runat="server" Width="100%" CssClass="form-control custom-select">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            Code
                                                        <br />
                            <asp:TextBox runat="server" ID="tbEmpCode" CssClass="form-control form-control-sm " placeholder="Code"></asp:TextBox>

                        </div>
                        <div class="col-md-3">
                            Employee Name 
                                                        <br />
                            <asp:TextBox runat="server" ID="tbName" CssClass="form-control form-control-sm " placeholder="Name"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            Date of Birth<br />
                            <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-sm dPicker" placeholder="DOB"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            Gender<br />

                            <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control custom-select">
                                <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                <asp:ListItem Text="Male" Value="1">Male</asp:ListItem>
                                <asp:ListItem Text="Female" Value="2">Female</asp:ListItem>
                                <asp:ListItem Text="Female" Value="3">Others</asp:ListItem>

                            </asp:DropDownList>

                        </div>
                        <div class="col-md-3">
                            Marital Status 
                                                        <br />
                            <asp:DropDownList ID="ddlMaritalStatus" runat="server" CssClass="form-control custom-select">
                                <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                <asp:ListItem Text="Unmarried" Value="1">Unmarried</asp:ListItem>
                                <asp:ListItem Text="Married" Value="2">Married</asp:ListItem>
                                <asp:ListItem Text="Divorced" Value="3">Divorced</asp:ListItem>
                                <asp:ListItem Text="Widow" Value="4">Widow</asp:ListItem>
                                <asp:ListItem Text="Widower" Value="5">Widower</asp:ListItem>
                                <asp:ListItem Text="Remarried" Value="6">Remarried</asp:ListItem>
                                <asp:ListItem>Others</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            Nationality<br />
                            <asp:DropDownList ID="ddlNationality" runat="server" CssClass="form-control custom-select">
                                <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                <asp:ListItem Value="1">Indian</asp:ListItem>
                                <asp:ListItem Value="2">Others</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-3">
                            Father/Husband Name
                                                        <br />
                            <asp:TextBox runat="server" ID="tbFatherName" CssClass="form-control form-control-sm" placeholder="Father/Husband Name"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            Mother Name<br />
                            <asp:TextBox runat="server" ID="tbMotherName" CssClass="form-control form-control-sm" placeholder="MotherName"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            UAN No <a href="#" data-toggle="tooltip" title="UAN SHOULD BE 12 DIGIT NUMBER eg.564567898456"><i class="glyphicon glyphicon-info-sign"></i></a>
                            <br />
                            <asp:TextBox runat="server" ID="tbUanNo" CssClass="form-control form-control-sm" placeholder="UAN No" onfocusout="EmployeeModule.validateUANNo();">

                            </asp:TextBox>
                            <span id="spanUanErrorMsg" style="color: red"></span>
                        </div>
                        <div class="col-md-3">
                            Date of Joining   
                                                        <br />
                            <asp:TextBox runat="server" ID="tbDoj" CssClass="form-control form-control-sm dPicker" placeholder="DOJ"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            Highest Education<br />
                            <asp:DropDownList ID="ddlEduacation" runat="server" CssClass="form-control custom-select">
                                <asp:ListItem Selected="True" Value="0">--Please Select--</asp:ListItem>
                                <asp:ListItem Text="Class I" Value="1">Class I</asp:ListItem>
                                <asp:ListItem Text="ClassII" Value="2">ClassII</asp:ListItem>
                                <asp:ListItem Text="Class III" Value="3">Class III</asp:ListItem>
                                <asp:ListItem Text="Class IV" Value="4">Class IV</asp:ListItem>
                                <asp:ListItem Text="Class V" Value="5">Class V</asp:ListItem>
                                <asp:ListItem Text="Class VI - IX" Value="6">VI - IX</asp:ListItem>
                                <asp:ListItem Text="MATRIX (CLASS X)" Value="7">CLASS X</asp:ListItem>
                                <asp:ListItem Text="CLASS XI" Value="8">CLASS XI</asp:ListItem>
                                <asp:ListItem Text="intermediate (CLASS XII)" Value="9">intermediate CLASS XII</asp:ListItem>
                                <asp:ListItem Text="Diploma" Value="10">Diploma</asp:ListItem>
                                <asp:ListItem Text="ITI" Value="11">ITI</asp:ListItem>
                                <asp:ListItem Text="Non-ITI" Value="12">Non-ITI</asp:ListItem>
                                <asp:ListItem Text="Graduate" Value="13">Graduate</asp:ListItem>
                                <asp:ListItem Text="Post Graduate" Value="14">Post Graduate</asp:ListItem>
                                <asp:ListItem Text="Phd." Value="15">Phd.</asp:ListItem>
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            EPF No           
                                                        <br />
                            <asp:TextBox runat="server" ID="tbEPFNo" CssClass="form-control form-control-sm" placeholder="EPF No"></asp:TextBox>
                        </div>
                        <div class="col-md-3 ">
                            ESIC No         
                                                        <br />
                            <asp:TextBox runat="server" ID="tbESICNo" CssClass="form-control form-control-sm" placeholder="ESIC No"></asp:TextBox>
                        </div>
                        <div class="col-md-3 ">
                            PAN No.   <a href="#" data-toggle="tooltip" title="PAN should be eg.GFHFF7565H"><i class="fa fa-info-sign"></i></a>
                            <br />
                            <asp:TextBox runat="server" ID="tbPanNo" CssClass="form-control form-control-sm" placeholder="Pan No" onfocusout="EmployeeModule.validatePANNo();"></asp:TextBox>
                            <span id="spanPanErrorMsg" style="color: red"></span>
                        </div>
                        <div class="col-md-3">
                            Aadhaar Card Number <a href="#" data-toggle="tooltip" title="Adhaar SHOULD BE 12 DIGIT NUMBER eg.564567898456"><i class="glyphicon glyphicon-info-sign"></i></a>
                            <br />
                            <asp:TextBox runat="server" ID="tbAdhaarNo" CssClass="form-control form-control-sm" placeholder="Adhaar Number" onfocusout="EmployeeModule.validateAdhaar();"></asp:TextBox>
                            <span id="spanAdhaarErrorMsg" style="color: red"></span>
                        </div>
                    </div>

                    <div class="row hide divDOL">
                        <div class="col-md-3">
                            Date of Relieving
                                                        <br />
                            <asp:TextBox runat="server" ID="tbDateOfRel" CssClass="form-control form-control-sm dPicker" placeholder="Date of relieving"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            Date of Leaving<br />

                            <asp:TextBox runat="server" ID="tbDol" CssClass="form-control form-control-sm dPicker" placeholder="DOL"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            Reason for Leaving (PF)<br />

                            <asp:TextBox runat="server" ID="tbRol" CssClass="form-control form-control-sm"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            Reason for Leaving (ESI)<br />
                            <asp:TextBox runat="server" ID="tbRolEsi" CssClass="form-control form-control-sm" placeholder="Reason for leaving"></asp:TextBox>
                        </div>
                    </div>

                    <br />
                    <div style="display: none" class="pnlSalary">

                        <h2 class="lead mb-0">Salary</h2>
                        <div class="tab-custom-content">
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                Select Salary Template   
               
                            <asp:DropDownList ID="DropDownList50" runat="server" Width="100%" onchange="EmployeeModule.onSalaryTemplate()" CssClass="form-control custom-select ddlSalaryTemplate">
                            </asp:DropDownList>
                            </div>
                        </div>

                        <div class="row">
                            <div class="col-md-12 salTemplate ">
                            </div>
                        </div>
                    </div>
                    <h2 class="lead mb-0">Mapping</h2>

               <hr>
                   
                    <div class="row">
                        <div class="col-md-3">
                            Shift                                                   
       <asp:DropDownList ID="ddlShift" runat="server" Width="100%" CssClass="form-control custom-select">
       </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 ">
                            Employee Type                                                   
       <asp:DropDownList ID="ddlEmployeeType" runat="server" Width="100%" CssClass="form-control custom-select">
       </asp:DropDownList>
                        </div>

                        <div class="col-lg-3 ">
                            Employee Cateory                                             
       <asp:DropDownList ID="ddlEmployeeCategory" runat="server" Width="100%" CssClass="form-control custom-select">
       </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            Project    
     <asp:DropDownList ID="ddlProject" runat="server" Width="100%" CssClass="form-control custom-select">
     </asp:DropDownList>
                        </div>

                    </div>
                    <div class="row">

                        <div class="col-lg-3 ">
                            Department                                                   
       <asp:DropDownList ID="ddlDepartment" runat="server" Width="100%" CssClass="form-control custom-select">
       </asp:DropDownList>
                        </div>

                        <div class="col-lg-3 ">
                            Designation                                             
       <asp:DropDownList ID="ddlDesignation" runat="server" Width="100%" CssClass="form-control custom-select">
       </asp:DropDownList>
                        </div>
                        <div class="col-lg-3">
                            Location                                                
       
     <asp:DropDownList ID="ddlLocation" runat="server" Width="100%" CssClass="form-control custom-select">
     </asp:DropDownList>
                        </div>
                        <div class="col-lg-3 ">
                            Division                                                   
       <asp:DropDownList ID="ddlDivision" runat="server" Width="100%" CssClass="form-control custom-select">
       </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3">
                            Bank Name                                                        
         <asp:DropDownList ID="ddlBank" runat="server" Width="100%" CssClass="form-control custom-select">
         </asp:DropDownList>
                        </div>

                        <div class="col-lg-3">
                            Bank A/C No       
                                                        <br />
                            <asp:TextBox runat="server" ID="tbBankAccountNo" CssClass="form-control form-control-sm" placeholder="Bank Acc/No"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Bank IFSC Code    
                                                          <br />
                            <asp:TextBox runat="server" ID="tbBankIfscCode" CssClass="form-control form-control-sm" placeholder="IFSC Code"></asp:TextBox>
                        </div>
                    </div>

                    <br />
                    <h2 class="lead mb-0">Address Information</h2>
                    <div class="tab-custom-content">
                    </div>

                    <div class="row">
                        <div class="col-lg-3">
                            Address Line 1           
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPermanentAdd1" CssClass="form-control form-control-sm" placeholder="Address Line 1"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Address Line 2 
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPermanentAdd2" CssClass="form-control form-control-sm" placeholder="Address Line 2"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            District      
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPermanentAdd3" CssClass="form-control form-control-sm" placeholder="District"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            City      
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPermanentAdd4" CssClass="form-control form-control-sm" placeholder="City"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 ">
                            State<br />
                            <asp:TextBox runat="server" ID="tbPermanentAdd5" CssClass="form-control form-control-sm" placeholder="State"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Pin Code 
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPermanentAdd6" CssClass="form-control form-control-sm" placeholder="Pin Code"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Mobile Number<br />
                            <asp:TextBox runat="server" ID="tbPermanentAdd7" CssClass="form-control form-control-sm" placeholder="Mobile Number"></asp:TextBox>
                        </div>
                        <div class="col-lg-3">
                            Emergency Contact<br />
                            <asp:TextBox runat="server" ID="tbPermanentAdd9" CssClass="form-control form-control-sm" placeholder="Emergency Contact"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            Same as Permanent Address
                                                        <input type="checkbox" name="sameaspermanent" value="sameaspermanent" style="width: 20px" />

                        </div>

                    </div>
                    <div class="row">
                        <div class="col-lg-3 ">
                            Address Line 1      
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPresentAdd1" CssClass="form-control form-control-sm" placeholder="Address Line 1"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 ">
                            Address Line 2    
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPresentAdd2" CssClass="form-control form-control-sm" placeholder="Address Line 2"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 ">
                            District           
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPresentAdd3" CssClass="form-control form-control-sm" placeholder="District"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 ">
                            City<br />
                            <asp:TextBox runat="server" ID="tbPresentAdd4" CssClass="form-control form-control-sm" placeholder="City"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-3 ">
                            State<br />
                            <asp:TextBox runat="server" ID="tbPresentAdd5" CssClass="form-control form-control-sm" placeholder="State"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 ">
                            Pin Code  
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPresentAdd6" CssClass="form-control form-control-sm" placeholder="Pin Code"></asp:TextBox>
                        </div>
                        <div class="col-lg-3 ">
                            Mobile Number<br />
                            <asp:TextBox runat="server" ID="tbPresentAdd7" CssClass="form-control form-control-sm" placeholder="Mobile Number"></asp:TextBox>
                        </div>
                        <div class="col-md-3">
                            Emergency Contact<br />
                            <asp:TextBox runat="server" ID="tbPresentAdd9" CssClass="form-control form-control-sm" placeholder="Emergency Contact"></asp:TextBox>
                        </div>
                    </div>

                    <h2 class="lead mb-0">Driving Licence & Passport Details</h2>
                    <div class="tab-custom-content">
                    </div>

                    <div class="row">

                        <div class="col-lg-3 ">
                            Licence No. 
                                                        <br />
                            <asp:TextBox runat="server" ID="tbLicenceNo" CssClass="form-control form-control-sm" placeholder="Licence No."></asp:TextBox>

                        </div>

                        <div class="col-lg-3 ">
                            Date of Issue 
                                                        <br />
                            <asp:TextBox runat="server" ID="tbLicenceDateOfIssue" CssClass=" form-control form-control-sm dPicker" ReadOnly="true" placeholder="Date of Issue"></asp:TextBox>

                        </div>
                        <div class="col-md-3">
                            Validity<br />
                            <asp:TextBox runat="server" ID="tbLicenceValidity" CssClass=" form-control form-control-sm dPicker" ReadOnly="true" placeholder="Validity"></asp:TextBox>


                        </div>
                        <div class="col-lg-3 ">
                            Issue Authority   
                                                        <br />
                            <asp:TextBox runat="server" ID="tbLicenceIssueAuthority" CssClass=" form-control form-control-sm " placeholder="Issue Authority"></asp:TextBox>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-lg-3 ">
                            Passport No.      
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPassportNo" CssClass=" form-control form-control-sm" placeholder="Passport No. "></asp:TextBox>

                        </div>

                        <div class="col-lg-3 ">
                            Date of Issue 
                                                        <br />
                            <asp:TextBox runat="server" ID="tbPassportDateOfIssue" CssClass="form-control form-control-sm dPicker" ReadOnly="true" placeholder="Date of Issue"></asp:TextBox>

                        </div>
                        <div class="col-md-3">
                            Validity<br />
                            <asp:TextBox runat="server" ID="tbPassportValidity" CssClass="form-control form-control-sm dPicker" ReadOnly="true" placeholder="Validity"></asp:TextBox>

                        </div>
                        <div class="col-lg-3 ">
                            Place of Issue<br />
                            <asp:TextBox runat="server" ID="tbPassportPlaceIssue" CssClass="form-control form-control-sm" placeholder="Place of Issue"></asp:TextBox>

                        </div>
                    </div>


                    <div style="display: none">
                        <h2 class="lead mb-0">Previous Employment</h2>
                        <div class="tab-custom-content">
                        </div>

                        <div class="row">
                            <div class="col-lg-12  table-responsive">
                                <table id="myTableEmployement" class="table table-bordered table-hover dataTable order-listEmployement">
                                    <tr>
                                        <td>Organization Name</td>
                                        <td>Organization Addess</td>
                                        <td>Date Of Joining </td>
                                        <td>Last Day</td>
                                        <td>Description</td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <input type="text" name="CompanyName[]" id="CompanyName" class="form-control form-control-sm" placeholder="Organization Name"></td>
                                        <td>
                                            <input type="text" name="CompanyAddress[]" id="CompanyAddress" class="form-control form-control-sm" placeholder="Organization Addess">
                                        </td>
                                        <td>
                                            <input type="text" name="Doj[]" id="Doj" class="dPicker form-control form-control-sm" readonly placeholder="DOJ"></td>
                                        <td>
                                            <input type="text" name="Dol[]" id="Dol" class="dPicker form-control form-control-sm" readonly placeholder="DOL">
                                        </td>
                                        <td>
                                            <input type="text" name="ReasonOfLeaving[]" id="ReasonOfLeaving" class="form-control form-control-sm" placeholder="Reason">
                                        </td>
                                        <td><a class="deleteRow"></a></td>
                                    </tr>

                                    <tfoot>
                                        <tr>
                                            <td colspan="5" style="text-align: left;">
                                                <button type="button" id="addrowEmployement" class="btn btn-secondary btn-xs"><span class="fa fa-plus"></span></button>
                                            </td>
                                        </tr>

                                    </tfoot>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div style="display: none">
                        <br />
                        <h2 class="lead mb-0">Family Details</h2>
                        <div class="tab-custom-content">
                        </div>

                        <div class="row  table-responsive ">

                            <table id="myTablefamily" class="table table-bordered table-hover dataTable order-listfamily">

                                <tr>
                                    <td>Relation</td>
                                    <td>Name</td>
                                    <td>Age</td>
                                    <td>DOB</td>
                                    <td>Adhaar No</td>
                                </tr>

                                <tr>
                                    <td>
                                        <select name="familydetailsname[]" class="form-control form-control-sm">
                                            <option value="0" selected="selected">--Select--</option>
                                            <option value="5">Father</option>
                                            <option value="6">Mother</option>
                                            <option value="1">Wife</option>
                                            <option value="2">Husband</option>
                                            <option value="3">Son</option>
                                            <option value="4">Daughter</option>
                                            <option value="8">Brother</option>
                                            <option value="7">Sister</option>
                                            <option value="9">Others</option>
                                        </select></td>
                                    <td>
                                        <input type="text" name="FamilyMemberName[]" class="form-control form-control-sm" id="FamilyMemberName" placeholder="Name">
                                    </td>
                                    <td>
                                        <input type="text" name="FamilyMemberAge[]" class="form-control form-control-sm" id="FamilyMemberAge" placeholder="Age">
                                    </td>
                                    <td>
                                        <input id="FamilyMemberDob" class="dPicker form-control form-control-sm" name="FamilyMemberDob[]" readonly placeholder="DOB"></td>

                                    <td>
                                        <input type="text" name="FamilyMembeAdhaarNo[]" class="form-control form-control-sm" id="FamilyMembeAdhaarNo" placeholder="Adhaar No">
                                    </td>
                                    <td><a class="deleteRow"></a></td>
                                </tr>

                                <tfoot>
                                    <tr>
                                        <td colspan="5" style="text-align: left;">
                                            <button type="button" id="addrowfamily" class="btn btn-secondary btn-xs"><span class="fa fa-plus"></span></button>
                                        </td>
                                    </tr>

                                </tfoot>
                            </table>
                        </div>
                    </div>
                   
                    <div style="display: none">
                        <h2 class="lead mb-0">Academic Details</h2>
                        <div class="tab-custom-content">
                        </div>

                        <div class="row table-responsive">
                            <table id='data_TableInfo' class="table table-bordered table-hover dataTable">
                                <tr>
                                    <td>Examination Passed</td>
                                    <td>Name Of School</td>
                                    <td>Subjects </td>
                                    <td>Month of Passing</td>
                                    <td>Year of Passing</td>
                                    <td>Grade/%</td>
                                    <td></td>
                                </tr>
                                <tr class="appendableDIV">
                                    <td>
                                        <input type="text" placeholder="Examination Passed" class="form-control form-control-sm" />
                                    </td>
                                    <td>
                                        <input type="text" placeholder="Name Of School" class="form-control form-control-sm" />
                                    </td>
                                    <td>
                                        <input type="text" placeholder="Subjects" class="form-control form-control-sm" />
                                    </td>
                                    <td>
                                        <select id="EducationMonth2" name="EducationMonth2" class="form-control form-control-sm">
                                            <option value="0">--Please Select--</option>
                                            <option value="1">January</option>
                                            <option value="2">February</option>
                                            <option value="3">March</option>
                                            <option value="4">April</option>
                                            <option value="5">May</option>
                                            <option value="6">June</option>
                                            <option value="7">July</option>
                                            <option value="8">August</option>
                                            <option value="9">September</option>
                                            <option value="10">October</option>
                                            <option value="11">November</option>
                                            <option value="12">December</option>
                                        </select>
                                    </td>
                                    <td>
                                        <select id="EducationYearE" name="EducationYearE" class="form-control form-control-sm">
                                            <option value="0">--Please Select--</option>
                                            <option value="2027">2027</option>
                                            <option value="2026">2026</option>
                                            <option value="2025">2025</option>
                                            <option value="2024">2024</option>
                                            <option value="2023">2023</option>
                                            <option value="2022">2022</option>
                                            <option value="2021">2021</option>
                                            <option value="2020">2020</option>
                                            <option value="2019">2019</option>
                                            <option value="2018">2018</option>
                                            <option value="2017">2017</option>
                                            <option value="2016">2016</option>
                                            <option value="2015">2015</option>
                                            <option value="2014">2014</option>
                                            <option value="2013">2013</option>
                                            <option value="2012">2012</option>
                                            <option value="2011">2011</option>
                                            <option value="2010">2010</option>
                                            <option value="2009">2009</option>
                                            <option value="2008">2008</option>
                                            <option value="2007">2007</option>
                                            <option value="2006">2006</option>
                                            <option value="2005">2005</option>
                                            <option value="2004">2004</option>
                                            <option value="2003">2003</option>
                                            <option value="2002">2002</option>
                                            <option value="2001">2001</option>
                                            <option value="2000">2000</option>
                                            <option value="1999">1999</option>
                                            <option value="1998">1998</option>
                                            <option value="1997">1997</option>
                                            <option value="1996">1996</option>
                                            <option value="1995">1995</option>
                                        </select>
                                    </td>
                                    <td>
                                        <input type="text" placeholder="Garde" class="form-control form-control-sm" />
                                    </td>

                                    <td id="dButton">
                                        <button class="btn btn-primary btn-xs" type="button" onclick="CommonModule.addRow();"><i class="fa fa-plus"></i></button>
                                        <button class="btn btn-danger btn-xs" type="button" onclick="CommonModule.removeRow(this);"><i class="fa fa-minus"></i></button>

                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    <br />
                    <div class="row margin-bottom-none">
                        <div class="col-sm-10">
                            &nbsp;
                        </div>
                        <div class="col-sm-1 pull-right">
                            <button type="button" class="btn btn-block btn-outline-secondary btn-sm" onclick="EmployeeModule.clearControl();">Cancel</button>
                        </div>
                        <div class="col-sm-1 pull-left">
                            <button type="button" class="btn btn-block btn-outline-info btn-sm" id="btnSave" onclick="EmployeeModule.createEmployee();">
                                Save
                                                 <i class="fa fa-spinner fa-spin spinner" style="font-size: 10px; float: right; display: none"></i>
                            </button>
                        </div>
                    </div>
                   <br />
                    <div id="progressBar" style="display: none; text-align: center">
                        <img src="../Images/gss.gif" />
                    </div>

                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">All Employee</h3>
                        </div>

                        <div class="card-body">
                            <div id="data"></div>
                        </div>
                    </div>


                    <div class="alert alert-danger alert-dismissible fade in" style="display: none" id="dvNoRecords">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong>No Records</strong>
                    </div>

                </div>
        </section>
    </form>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsSection" runat="server">
    <link href="../css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../js/bootstrap-datepicker.js"></script>
    <script src="../AdminLTE-3.0.1/plugins/sweetalert2/sweetalert2.min.js"></script>
    <script src="../AdminLTE-3.0.1/plugins/toastr/toastr.min.js"></script>
    <script src="../AdminLTE-3.0.1/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../AdminLTE-3.0.1/plugins/datatables/jquery.dataTables.js"></script>
    <script src="../AdminLTE-3.0.1/plugins/datatables-bs4/js/dataTables.bootstrap4.js"></script>
    <script src="../alert.js"></script>
    <script type="text/javascript">
        var baseUrl = '<%= ConfigurationManager.AppSettings["baseUrl"]%>';
        var globalCol = [];
        var PLEASESELECT = '--Select--';
        var count = 0;
        var ADITIONAL = 'Aditional';
        var na = 'N/A';
        var EARNING = 'Earning';
        var DEDUCTION = 'Deduction';
        var MASTERLIST = [];
        var EMPLOYEEID = null;
        var ddlEmployee = $(".ddlEmployee");
        var ddlEmployeeType = $(".ddlEmployeeType");
        var ddlDesignation = $(".ddlDesignation");
        var ddlLocation = $(".ddlLocation");
        var ddlDepartment = $(".ddlDepartment");
        var ddlProject = $(".ddlProject");
        var ddlDivision = $(".ddlDivision");
        var ddlShift = $(".ddlShift");
        var templateItemObject = [];
        var Toast = null;

        //employee information
        var EmployeeModule = (function () {
            'use strict'
            var dvAddress = $('.dvAddress')
            var dvDrivingLicence = $('.dvDrivingLicence');
            var dvEmployment = $('.dvEmployment');
            var dvFamilyDetails = $('.dvFamilyDetails');
            var dvAcademic = $('.dvAcademic');
            var dvSalary = $('.dvSalary');
            var dvDOL = $('.divDOL');
            var ddlSalaryTemplate = $('.ddlSalaryTemplate');
            var pnlSalary = $('.pnlSalary');

            var onSalaryTemplate = function () {
                // showModal();;
                var selectedVal = $(".ddlSalaryTemplate option:selected").val();
                $.ajax({
                    url: baseUrl + 'EmployeeTemplate/' + selectedVal,
                    type: 'GET',
                    success: function (data) {
                        var deductionAmt = 0;
                        var additionalAmt = 0;
                        var earningAmt = 0;
                        var templateObject = data.dtEmployeeGet;
                        templateItemObject = [];
                        templateItemObject = data.dtEmpddress;
                        var html = '<div class="table-responsive"><table id="tableSalaryTemplate" class="table table-bordered table-hover dataTable"><thead><tr><th>Payroll Item</th><th>Payroll Type</th><th>Default Value</th><th>Value Type</th><th>Applicable On</th><th style="display:none">Active</th><th>Final Value</th></tr><thead><tbody>';
                        $('.salTemplate').html("");
                        $.each(templateItemObject, function (i, v) {
                            var actv = v.Acitve ? 'Yes' : 'No';
                            var vType = v.ISAmount ? "Rs." : "%";
                            html += '<tr>'
                            + '<td PayrollItemId="' + v.PayrollItemId + '">' + v.PayrollItemText + '</td>'
                            + '<td>' + InitEmpSalaryTemplate._bindPayrollItemTypeDDL(v.ValueType) + '</td>                                     '
                            + '<td>' + parseFloat(v.DefaultValue).toFixed(2) + '</td>'
                            + '<td>' + vType + '</td>'
                            + '<td>' + v.ApplicableItemText + '</td>                                          '
                            + '<td style="display:none">' + actv + '</td>                                          '
                            + '<td>' + InitEmpSalaryTemplate._calculateLastValue(v) + '</td>                                          '
                            + '</tr>';

                        });
                        html += '</tbody><br/>'
                            + '<tfooter><tr><th>CTC</th><th>Net Salary</th><th>Gross</th><th>Earning</th><th>Deduction</th><th>Aditional</th></tr>'
                            + '<tr><td id="tdCTC"></td><td id="tdNtSal"></td><td id="tdGross"></td><td id="tdEarning"></td><td id="tdDeduction"></td><td id="tdAdtional"></td></tr>'
                            + '</tfooter></table></div>';

                        var l = $(html).find('tbody tr');
                        $.each(l, function (i, v) {
                            if ($(v).find('td:eq(1)').html() === 'Earning') {
                                earningAmt += parseFloat($(v).find('td:eq(6)').html());
                            }
                            if ($(v).find('td:eq(1)').html() === 'Aditional') {
                                additionalAmt += parseFloat($(v).find('td:eq(6)').html());
                            }
                            if ($(v).find('td:eq(1)').html() === 'Deduction') {
                                deductionAmt += parseFloat($(v).find('td:eq(6)').html());
                            }
                        })

                        $('.salTemplate').append(html);
                        var ctc = (earningAmt + additionalAmt).toFixed(2)
                        var ntSal = (earningAmt - deductionAmt).toFixed(2);
                        $('#tdCTC').html(ctc);
                        $('#tdNtSal').html(ntSal);
                        $('#tdGross').html(earningAmt.toFixed(2));
                        $('#tdEarning').html(earningAmt.toFixed(2));
                        $('#tdDeduction').html(deductionAmt.toFixed(2));
                        $('#tdAdtional').html(additionalAmt.toFixed(2));
                        hideModal();
                    },
                    error: function (request, message, error) {
                        $("#myModal").modal('hide');
                    }
                });
            };

            var initEmployee = function () {
                pnlSalary.hide();
                showModal();;
                //CommonModule.bindEmployeeDropDownList(ddlSalaryTemplate, MASTERLIST.SalaryTemplate);
                CommonModule.LoadMasters();
                ddlSalaryTemplate.html('');
                ddlSalaryTemplate.append($("<option></option>").val('0').html(PLEASESELECT));
                $.grep(MASTERLIST.SalaryTemplate, function (n, i) {
                    // if (n.GenID === GenId) {
                    ddlSalaryTemplate.append($("<option></option>").val(n.salaryTemplateid).html(n.Name));
                    //}
                });
                EmployeeModule.hideEmployeeDivs();
                dvDOL.hide();
                $.ajax({
                    url: baseUrl + 'Employee',
                    type: 'GET'
                }).done(function (data, textStatus, xhr) {
                    $("#data").html("");
                    if (data.Count > 0) {
                        $("#data").append(data.Html);
                        $("#employeeDataTable").DataTable();
                        $("#dvNoRecords").hide();
                    }
                    else {
                        $("#data").html("");
                        $("#dvNoRecords").show();
                    }
                    hideModal();
                }).fail(function (jqXHR, textStatus) {
                    hideModal();
                }).always(function () {

                });

            };

            var clearControl = function () {
                $("input,select").removeClass("add-error");
                $("input").val("");
                $("select").val("0");
                $("#btnSave").text("Save");
                EmployeeModule.hideEmployeeDivs();
            };

            var createEmployee = function () {
                var isReq = false;

                if ($("#FeaturedContent_ddlcompany option:selected").val() == "0") { $("#FeaturedContent_ddlcompany").addClass("add-error"); $("#progressBar").hide(); isReq = true; }
                else { $("#FeaturedContent_ddlcompany").removeClass("add-error"); }

                if ($("#FeaturedContent_tbEmpCode").val() == "") { $("#FeaturedContent_tbEmpCode").addClass("add-error"); $("#progressBar").hide(); isReq = true; }
                else { $("#FeaturedContent_tbEmpCode").removeClass("add-error"); }

                if ($("#FeaturedContent_txtDate").val() == "") { $("#FeaturedContent_txtDate").addClass("add-error"); $("#progressBar").hide(); isReq = true; }
                else { $("#FeaturedContent_txtDate").removeClass("add-error"); }

                if ($("#FeaturedContent_tbName").val() == "") { $("#FeaturedContent_tbName").addClass("add-error"); $("#progressBar").hide(); isReq = true; }
                else { $("#FeaturedContent_tbName").removeClass("add-error"); }

                if ($("#FeaturedContent_tbDoj").val() == "") { $("#FeaturedContent_tbDoj").addClass("add-error"); $("#progressBar").hide(); isReq = true; }
                else { $("#FeaturedContent_tbDoj").removeClass("add-error"); }



                if (isReq) {
                    $(document).scrollTop($("#EmployeeInfo").offset().top);

                    return false;
                }

                showModal();
                var Employee = new Object();
                Employee.CompanyId = $("#FeaturedContent_ddlcompany option:selected").val();
                Employee.Code = $("#FeaturedContent_tbEmpCode").val();
                Employee.Name = $("#FeaturedContent_tbName").val();
                Employee.Gender = $("#FeaturedContent_ddlGender option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlGender option:selected").val();
                Employee.Doj = $("#FeaturedContent_tbDoj").val() == "" ? null : $("#FeaturedContent_tbDoj").val();
                Employee.MaritalStatus = $("#FeaturedContent_ddlMaritalStatus option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlMaritalStatus option:selected").val();
                Employee.Nationality = $("#FeaturedContent_ddlNationality option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlNationality option:selected").val();
                Employee.FatherName = $("#FeaturedContent_tbFatherName").val();
                Employee.MotherName = $("#FeaturedContent_tbMotherName").val();
                Employee.Dor = $("#FeaturedContent_tbDor").val() == "" ? null : $("#FeaturedContent_tbDor").val();
                Employee.DateOfRelieving = $("#FeaturedContent_tbDateOfRel").val() == "" ? null : $("#FeaturedContent_tbDateOfRel").val();
                Employee.Dob = $("#FeaturedContent_txtDate").val();
                Employee.HighQual = $("#FeaturedContent_ddlEduacation option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlEduacation option:selected").val();
                Employee.EPFNo = $("#FeaturedContent_tbEPFNo").val();
                Employee.ESINo = $("#FeaturedContent_tbESICNo").val();
                Employee.PANNo = $("#FeaturedContent_tbPanNo").val();
                Employee.AdhaarNo = $("#FeaturedContent_tbAdhaarNo").val();
                Employee.UANNo = $("#FeaturedContent_tbUanNo").val();
                Employee.LicenceNo = $("#FeaturedContent_tbLicenceNo").val();
                Employee.LicenceDateOfIssue = $("#FeaturedContent_tbLicenceDateOfIssue").val() == "" ? null : $("#FeaturedContent_tbLicenceDateOfIssue").val();
                Employee.LicenceValidity = $("#FeaturedContent_tbLicenceValidity").val() == '' ? null : $("#FeaturedContent_tbLicenceValidity").val();
                Employee.LicenceIssueAuthority = $("#FeaturedContent_tbLicenceIssueAuthority").val();
                Employee.PassportNo = $("#FeaturedContent_tbPassportNo").val();
                Employee.PassportDateOfIssue = $("#FeaturedContent_tbPassportDateOfIssue").val() == "" ? null : $("#FeaturedContent_tbPassportDateOfIssue").val();
                Employee.PassportValidity = $("#FeaturedContent_tbPassportValidity").val() == '' ? null : $("#FeaturedContent_tbPassportValidity").val();
                Employee.PassportPlaceIssue = $("#FeaturedContent_tbPassportPlaceIssue").val();
                Employee.ShiftId = $("#FeaturedContent_ddlShift option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlShift option:selected").val();
                Employee.EmployeeTypeId = $("#FeaturedContent_ddlEmployeeType option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlEmployeeType option:selected").val();
                Employee.EmployeeCategoryId = $("#FeaturedContent_ddlEmployeeCategory option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlEmployeeCategory option:selected").val();
                Employee.ProjectId = $("#FeaturedContent_ddlProject option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlProject option:selected").val();
                Employee.DepartmentId = $("#FeaturedContent_Department option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlDepartment option:selected").val();
                Employee.DesignationId = $("#FeaturedContent_ddlDesignation option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlDesignation option:selected").val();
                Employee.LocationId = $("#FeaturedContent_ddlLocation option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlLocation option:selected").val();
                Employee.DivisionId = $("#FeaturedContent_ddlDivision option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlDivision option:selected").val();
                Employee.BankId = $("#FeaturedContent_ddlBank option:selected").val() == PLEASESELECT
                    ? 0 : $("#FeaturedContent_ddlBank option:selected").val();
                Employee.BankAccountNo = $("#FeaturedContent_tbBankAccountNo").val();
                Employee.BankIfscCode = $("#FeaturedContent_tbBankIfscCode").val();


                Employee.PAdd1 = $("#FeaturedContent_tbPermanentAdd1").val();
                Employee.PAdd2 = $("#FeaturedContent_tbPermanentAdd2").val();
                Employee.PDistrict = $("#FeaturedContent_tbPermanentAdd3").val();
                Employee.PCity = $("#FeaturedContent_tbPermanentAdd4").val();
                Employee.PState = $("#FeaturedContent_tbPermanentAdd5").val();
                Employee.PPinCode = $("#FeaturedContent_tbPermanentAdd6").val();
                Employee.PMobileNo = $("#FeaturedContent_tbPermanentAdd7").val();
                Employee.PEmergencyContact = $("#FeaturedContent_tbPermanentAdd9").val();
                Employee.RAdd1 = $("#FeaturedContent_tbPresentAdd1").val();
                Employee.RAdd2 = $("#FeaturedContent_tbPresentAdd2").val();
                Employee.RDistrict = $("#FeaturedContent_tbPresentAdd3").val();
                Employee.RCity = $("#FeaturedContent_tbPresentAdd4").val();
                Employee.RState = $("#FeaturedContent_tbPresentAdd5").val();
                Employee.RPinCode = $("#FeaturedContent_tbPresentAdd6").val();
                Employee.RMobileNo = $("#FeaturedContent_tbPresentAdd7").val();
                Employee.REmergencyContact = $("#FeaturedContent_tbPresentAdd9").val();
                Employee.IsPresentSame = true;//$("input[name=sameaspermanent]:checked").is(':checked') ? true : false;


                var arrayObjects = {};
                var arrayList = [];
                //Academic Details
                $("#data_TableInfo").find("tr").each(function (i, el) {
                    if (i != 0) {
                        arrayObjects.ExaminationPassed = $(this).find('td:eq(0) input')[0] == undefined || "" ? null : $(this).find('td:eq(0) input')[0].value;
                        arrayObjects.NameOfSchool = $(this).find('td:eq(1) input')[0] == undefined || "" ? null : $(this).find('td:eq(1) input')[0].value;
                        arrayObjects.Subjects = $(this).find('td:eq(2) input')[0] == undefined || null ? "" : $(this).find('td:eq(2) input')[0].value;
                        arrayObjects.MonthofPassing = $(this).find('td:eq(3) select')[0] == undefined || "" ? 0 : parseInt($(this).find('td:eq(3) select')[0].value);
                        arrayObjects.YearofPassing = $(this).find('td:eq(4) select')[0] == undefined || "" ? 0 : parseInt($(this).find('td:eq(4) select')[0].value);
                        arrayObjects.Grade = $(this).find('td:eq(5) input')[0] == undefined || "" ? null : $(this).find('td:eq(5) input')[0].value;
                        arrayList.push(arrayObjects);
                    }
                    arrayObjects = {};
                });
                Employee.EmployeeAcademicDetails = arrayList;
                arrayObjects = {};
                arrayList = [];
                //Family Details
                $("#myTablefamily").find("tr").each(function (i, el) {
                    if (i != 0) {
                        arrayObjects.FamilyRelationId = $(this).find('td:eq(0) select')[0] == undefined ? 0 : parseInt($(this).find('td:eq(0) select')[0].value);
                        arrayObjects.FamilyMemberName = $(this).find('td:eq(1) input')[0] == undefined ? "" : $(this).find('td:eq(1) input')[0].value;
                        arrayObjects.FamilyMemberAge = $(this).find('td:eq(2) input')[0] == undefined ? 0 : parseInt($(this).find('td:eq(2) input')[0].value);
                        arrayObjects.FamilyMemberDob = $(this).find('td:eq(3) input')[0] == undefined ? null : $(this).find('td:eq(3) input')[0].value;
                        arrayObjects.FamilyMembeAdhaarNo = $(this).find('td:eq(4) input')[0] == undefined ? "" : $(this).find('td:eq(4) input')[0].value;
                        arrayList.push(arrayObjects);
                    }
                    arrayObjects = {};
                });
                Employee.EmployeeFamilyDetails = arrayList;
                arrayObjects = {};
                arrayList = [];
                //Employement Details
                $("#myTableEmployement").find("tr").each(function (i, el) {
                    if (i != 0) {
                        arrayObjects.CompanyName = $(this).find('td:eq(0) input')[0] == undefined ? "" : $(this).find('td:eq(0) input')[0].value;
                        arrayObjects.CompanyAddress = $(this).find('td:eq(1) input')[0] == undefined ? "" : $(this).find('td:eq(1) input')[0].value;
                        arrayObjects.Doj = $(this).find('td:eq(2) input')[0] == undefined ? null : $(this).find('td:eq(2) input')[0].value;
                        arrayObjects.Dol = $(this).find('td:eq(3) input')[0] == undefined ? null : $(this).find('td:eq(3) input')[0].value;
                        arrayObjects.ReasonOfLeaving = $(this).find('td:eq(4) input')[0] == undefined ? "" : $(this).find('td:eq(4) input')[0].value;
                        arrayList.push(arrayObjects);
                    }
                    arrayObjects = {};
                });
                Employee.EmployeeEmployementDetails = arrayList;
                arrayObjects = {};
                arrayList = [];

                $(".ibtnDelEmployement").trigger("click");
                $(".ibtnDelfamily").trigger("click");
                $(".appendableDIV").each(function (i, v) {
                    $($(v).find('td:eq(6)').find("button")[1]).trigger("click");
                });
                arrayObjects = {};
                arrayList = [];
                var isSal = $("#FeaturedContent_DropDownList50").attr("disabled") === 'disabled' ? true : false;
                if (!isSal) {
                    //salay tm Details
                    $('.salTemplate table tbody:eq(1) tr:eq(1)').each(function (i, el) {
                        arrayObjects.CTC = $(el).find('td:eq(0)').html();
                        arrayObjects.Gross = $(el).find('td:eq(2)').html();
                        arrayObjects.NetPay = $(el).find('td:eq(1)').html();
                        arrayObjects.AdditionalBenefit = $(el).find('td:eq(5)').html();
                        arrayObjects.Deduction = $(el).find('td:eq(4)').html();
                        arrayObjects.Earning = $(el).find('td:eq(3)').html();
                        arrayObjects.templateid = $(".ddlSalaryTemplate option:selected").val();
                        arrayObjects.EmployeeId = EMPLOYEEID;
                        arrayList.push(arrayObjects);
                        arrayObjects = {};
                    });

                    Employee.Salarys = arrayList;
                    arrayObjects = {};
                    arrayList = [];

                    $('.salTemplate  table tbody:eq(0) tr').each(function (i, el) {
                        arrayObjects.PayrollItemId = $(el).find('td:eq(0)').attr('PayrollItemId');
                        arrayObjects.PayrollItemName = $(el).find('td:eq(0)').html();
                        arrayObjects.PayrollItemType = $(el).find('td:eq(1)').html();
                        arrayObjects.PayrollItemTypeName = $(el).find('td:eq(1)').html();
                        arrayObjects.Amount = $(el).find('td:eq(6)').html();
                        arrayList.push(arrayObjects);
                        arrayObjects = {};
                    });
                    Employee.SalaryItems = arrayList;
                    Employee.isSalary = false;
                }
                else {
                    Employee.isSalary = true;
                }
                arrayObjects = {};
                arrayList = [];


                if ($("#btnSave").text() == 'Update') {
                    Employee.StatementType = "Update";
                    Employee.EmployeeId = $("#hfEmployeId").val();
                    _employeeAddOrUpdate('Update', Employee);
                }
                else {

                    Employee.StatementType = "Insert";
                    _employeeAddOrUpdate('Save', Employee);
                }
                hideModal();
                EmployeeModule.hideEmployeeDivs();

            };

            var validateAdhaar = function () {
                var adhar = $("#FeaturedContent_tbAdhaarNo").val();
                var adharcard = /^\d{12}$/;
                if (adhar != '') {
                    if (!adhar.match(adharcard)) {
                        $("#spanAdhaarErrorMsg").html("Adhaar should be 12 digit number");
                        return false;
                    }
                    else {
                        $("#spanAdhaarErrorMsg").html("");
                    }
                }
            };

            var validateUANNo = function () {
                var adhar = $("#FeaturedContent_tbUanNo").val();
                var adharcard = /^\d{12}$/;
                if (adhar != '') {
                    if (!adhar.match(adharcard)) {
                        $("#spanUanErrorMsg").html("UAN No should be 12 digit number.");
                        return false;
                    }
                    else {
                        $("#spanUanErrorMsg").html("");
                    }
                }
            };

            var validatePANNo = function () {
                var panRegularExp = /^[A-Z]{5}\d{4}[A-Z]{1}$/;
                var isPan = panRegularExp.test($("#FeaturedContent_tbPanNo").val().toUpperCase());
                if (!isPan) {
                    $("#spanPanErrorMsg").html("InValid Pan No (eg. AASDC6767V)");
                    return false;
                }
                else {
                    $("#spanPanErrorMsg").html("");
                }
            };

            var _employeeAddOrUpdate = function (operation, employee) {
                var type = operation == 'Save' ? 'POST' : 'PUT';
                $.ajax({
                    url: baseUrl + 'employee',
                    type: type,
                    contentType:
                       "application/json;charset=utf-8",
                    data: JSON.stringify(employee),
                    success: function (response) {
                        EmployeeModule.initEmployee();
                        // $("#liEmployee").trigger("click");
                        if (operation == 'Save') {
                            CommonModule.showNotification("Employee Saved", response.Html, 'success');
                        }
                        else {
                            CommonModule.showNotification("Employee updated", response.Html, 'success');

                        }
                        EmployeeModule.clearControl();
                        // window.location.href = window.location.href;

                    },
                    error: function (request, message, error) {
                        // handleException(request, message, error);
                        CommonModule.showNotification(message, error, 'danger');
                    }
                });
            }

            var RowDelete = function (id) {
                try {
                    if (!confirm("Do You Want To Delete")) {
                        return false;
                    }
                    showModal();;
                    $.ajax({
                        url: baseUrl + 'employee/' + id,
                        type: 'DELETE',
                        success: function (info) {
                            $("#liEmployee").trigger("click");
                            $("#btnSave").text("Save");
                            CommonModule.showNotification("Employee deleted", info.Html, 'success');
                            EmployeeModule.hideEmployeeDivs();
                        },
                        error: function (request, message, error) {
                            $("#liEmployee").trigger("click");
                            CommonModule.showNotification(message, error, 'danger');
                        }
                    });
                }
                catch (Ex) {

                }
            }

            var getRowVal = function (id) {
                dvDOL.show();
                pnlSalary.show();
                EMPLOYEEID = parseInt(id);
                showModal();;
                EmployeeModule.showEmployeeDivs();
                $.ajax({
                    url: baseUrl + 'employee/' + id,
                    type: 'GET',
                    success: function (data) {
                        var empObject = data.dtEmployeeGet[0];
                        var empAddress = data.dtEmpddress[0];
                        $("#hfEmployeId").val(empObject.EmployeeId);
                        $("#FeaturedContent_ddlcompany").val(CommonModule.setDefaultSelectDropdown(empObject.companyid));
                        $("#FeaturedContent_tbEmpCode").val(empObject.Code);
                        $("#FeaturedContent_tbName").val(empObject.Name);
                        $("#FeaturedContent_ddlGender").val(CommonModule.setDefaultSelectDropdown(empObject.Gender))
                        $("#FeaturedContent_tbDoj").datepicker('setDate', CommonModule.setDate(empObject.Doj));;
                        $("#FeaturedContent_ddlMaritalStatus").val(CommonModule.setDefaultSelectDropdown(empObject.MritalStatus));
                        $("#FeaturedContent_ddlNationality").val(CommonModule.setDefaultSelectDropdown(empObject.Nationality));
                        $("#FeaturedContent_tbFatherName").val(empObject.FatherName);
                        $("#FeaturedContent_tbMotherName").val(empObject.MotherName);
                        $("#FeaturedContent_tbDor").datepicker('setDate', CommonModule.setDate(empObject.Dor));;
                        $("#FeaturedContent_tbDateOfRel").datepicker('setDate', CommonModule.setDate(empObject.dateOfRel));
                        $("#FeaturedContent_txtDate").datepicker('setDate', CommonModule.setDate(empObject.Dob));
                        $("#FeaturedContent_ddlEduacation").val(CommonModule.setDefaultSelectDropdown(empObject.HighestQual));
                        $("#FeaturedContent_tbEPFNo").val(empObject.EPFNo);
                        $("#FeaturedContent_tbESICNo").val(empObject.ESINo);
                        $("#FeaturedContent_tbPanNo").val(empObject.PANNo);
                        $("#FeaturedContent_tbAdhaarNo").val(empObject.AdhaarNo);
                        $("#FeaturedContent_tbUanNo").val(empObject.UANNo);
                        $("#FeaturedContent_tbLicenceNo").val(empObject.LicenceNo);
                        $("#FeaturedContent_tbLicenceDateOfIssue").datepicker('setDate', CommonModule.setDate(empObject.LicenceDateOfIssue));
                        $("#FeaturedContent_tbLicenceValidity").datepicker('setDate', CommonModule.setDate(empObject.LicenceValidity));
                        $("#FeaturedContent_tbLicenceIssueAuthority").val(empObject.LicenceIssueAuthority);
                        $("#FeaturedContent_tbPassportNo").val(empObject.PassportNo);
                        $("#FeaturedContent_tbPassportDateOfIssue").datepicker('setDate', CommonModule.setDate(empObject.PassportDateOfIssue));
                        $("#FeaturedContent_tbPassportValidity").datepicker('setDate', CommonModule.setDate(empObject.PassportValidity));
                        $("#FeaturedContent_tbPassportPlaceIssue").val(empObject.PassportPlaceIssue);
                        $("#FeaturedContent_ddlShift").val(CommonModule.setDefaultSelectDropdown(empObject.ShiftId));
                        $("#FeaturedContent_ddlEmployeeType").val(CommonModule.setDefaultSelectDropdown(empObject.EmployeeTypeId));
                        $("#FeaturedContent_ddlEmployeeCategory").val(CommonModule.setDefaultSelectDropdown(empObject.EmployeeCategoryId));
                        $("#FeaturedContent_ddlProject").val(CommonModule.setDefaultSelectDropdown(empObject.ProjectId));
                        $("#FeaturedContent_ddlDepartment").val(CommonModule.setDefaultSelectDropdown(empObject.DepartmentId));
                        $("#FeaturedContent_ddlDesignation").val(CommonModule.setDefaultSelectDropdown(empObject.DesignationId));
                        $("#FeaturedContent_ddlLocation").val(CommonModule.setDefaultSelectDropdown(empObject.LocationId));
                        $("#FeaturedContent_ddlDivision").val(CommonModule.setDefaultSelectDropdown(empObject.DivisionId));
                        $("#FeaturedContent_ddlBank").val(CommonModule.setDefaultSelectDropdown(empObject.BankId));
                        $("#FeaturedContent_tbBankAccountNo").val(empObject.BankAccountNo);
                        $("#FeaturedContent_tbBankIfscCode").val(empObject.BankIfscCode);
                        if (empAddress != undefined) {
                            $("#FeaturedContent_tbPermanentAdd1").val(empAddress.PAdd1);
                            $("#FeaturedContent_tbPermanentAdd2").val(empAddress.PAdd2);
                            $("#FeaturedContent_tbPermanentAdd3").val(empAddress.PCity);
                            $("#FeaturedContent_tbPermanentAdd4").val(empAddress.PState);
                            $("#FeaturedContent_tbPermanentAdd5").val(empAddress.PDistrict);
                            $("#FeaturedContent_tbPermanentAdd6").val(empAddress.PPinCode);
                            $("#FeaturedContent_tbPermanentAdd7").val(empAddress.PMobileNo);
                            $("#FeaturedContent_tbPermanentAdd9").val(empAddress.PEmergencyContact);
                            $("#FeaturedContent_tbPresentAdd1").val(empAddress.RAdd1);
                            $("#FeaturedContent_tbPresentAdd2").val(empAddress.RAdd2);
                            $("#FeaturedContent_tbPresentAdd3").val(empAddress.RCity);
                            $("#FeaturedContent_tbPresentAdd4").val(empAddress.RState);
                            $("#FeaturedContent_tbPresentAdd5").val(empAddress.RDistrict);
                            $("#FeaturedContent_tbPresentAdd6").val(empAddress.RPinCode);
                            $("#FeaturedContent_tbPresentAdd7").val(empAddress.RMobileNo);
                            $("#FeaturedContent_tbPresentAdd9").val(empAddress.REmergencyContact);
                        }
                        //var get_EmployeeFamilyDetail = data.get_EmployeeFamilyDetail;
                        //var get_EmployeeEmployementDetail = data.get_EmployeeEmployementDetail;
                        //var get_EmployeeAcademicDetail = data.get_EmployeeAcademicDetail;
                        var get_EmployeeSalaryDetail = data.get_EmployeeSalaryDetail;
                        var get_EmployeeSalaryItemDetail = data.get_EmployeeSalaryItemDetail;
                        if (get_EmployeeSalaryDetail.length > 0 && get_EmployeeSalaryItemDetail.length > 0) {
                            $("#FeaturedContent_DropDownList50").attr("disabled", "disabled");
                            $("#FeaturedContent_DropDownList50").val(CommonModule.setDefaultSelectDropdown(get_EmployeeSalaryDetail[0].templateid));
                        }
                        else {
                            $("#FeaturedContent_DropDownList50").removeAttr("disabled");
                            $("#FeaturedContent_DropDownList50").val("0");
                        }
                        $("#btnSave").text("Update");
                        $("input,select").removeClass("add-error");
                        hideModal();
                    },
                    error: function (request, message, error) {
                        hideModal();
                    }
                });
            }

            var hideEmployeeDivs = function () {
                dvAddress.hide();
                dvDrivingLicence.hide();
                dvEmployment.hide();
                dvFamilyDetails.hide();
                dvAcademic.hide();
                dvSalary.hide();
            };

            var showEmployeeDivs = function () {
                dvAddress.show();
                dvDrivingLicence.show();
                dvEmployment.show();
                dvFamilyDetails.show();
                dvAcademic.show();
                dvSalary.show();
            };

            var academicDetails = function () { };
            var familyDetails = function () { };
            var previousEmployment = function () {

                var employeeId = EMPLOYEEID
                $('.spinEmploymentDetails').show();
                var _url = baseUrl + 'PIS/GetEmployment/' + employeeId;
                showModal();;
                //EmployeeModule.hideEmployeeDivs();
                $.ajax({
                    url: _url,
                    type: 'POST'
                }).done(function (data, textStatus, xhr) {
                    var getEmployment = data.GetEmployment;
                    hideModal();
                    $('.spinEmploymentDetails').hide();
                }).fail(function (jqXHR, textStatus) {
                    hideModal();
                }).always(function () {

                });
            };


            return {
                onSalaryTemplate: onSalaryTemplate,
                hideEmployeeDivs: hideEmployeeDivs,
                showEmployeeDivs: showEmployeeDivs,
                initEmployee: initEmployee,
                clearControl: clearControl,
                createEmployee: createEmployee,
                validateAdhaar: validateAdhaar,
                validateUANNo: validateUANNo,
                validatePANNo: validatePANNo,
                getRowVal: getRowVal,
                RowDelete: RowDelete,
                academicDetails: academicDetails,
                familyDetails: familyDetails,
                previousEmployment: previousEmployment,
            };
        })();

        $('input[name=sameaspermanent]').click(function () {
            var id = '#FeaturedContent';
            $("input[name=sameaspermanent]:checked").is(':checked') ? $('#FeaturedContent_tbPresentAdd1').val($('#FeaturedContent_tbPermanentAdd1').val()) : $('#FeaturedContent_tbPresentAdd1').val("");
            $("input[name=sameaspermanent]:checked").is(':checked') ? $('#FeaturedContent_tbPresentAdd2').val($('#FeaturedContent_tbPermanentAdd2').val()) : $('#FeaturedContent_tbPresentAdd2').val("");
            $("input[name=sameaspermanent]:checked").is(':checked') ? $('#FeaturedContent_tbPresentAdd3').val($('#FeaturedContent_tbPermanentAdd3').val()) : $('#FeaturedContent_tbPresentAdd3').val("");
            $("input[name=sameaspermanent]:checked").is(':checked') ? $('#FeaturedContent_tbPresentAdd4').val($('#FeaturedContent_tbPermanentAdd4').val()) : $('#FeaturedContent_tbPresentAdd4').val("");
            $("input[name=sameaspermanent]:checked").is(':checked') ? $('#FeaturedContent_tbPresentAdd5').val($('#FeaturedContent_tbPermanentAdd5').val()) : $('#FeaturedContent_tbPresentAdd5').val("");
            $("input[name=sameaspermanent]:checked").is(':checked') ? $('#FeaturedContent_tbPresentAdd6').val($('#FeaturedContent_tbPermanentAdd6').val()) : $('#FeaturedContent_tbPresentAdd6').val("");
            $("input[name=sameaspermanent]:checked").is(':checked') ? $('#FeaturedContent_tbPresentAdd7').val($('#FeaturedContent_tbPermanentAdd7').val()) : $('#FeaturedContent_tbPresentAdd7').val("");
            $("input[name=sameaspermanent]:checked").is(':checked') ? $('#FeaturedContent_tbPresentAdd8').val($('#FeaturedContent_tbPermanentAdd8').val()) : $('#FeaturedContent_tbPresentAdd8').val("");
            $("input[name=sameaspermanent]:checked").is(':checked') ? $('#FeaturedContent_tbPresentAdd9').val($('#FeaturedContent_tbPermanentAdd9').val()) : $('#FeaturedContent_tbPresentAdd9').val("");

        });

        var CommonModule = (function () {
            var _url = baseUrl + 'Master/GetMaster';
            var _urlEmployeeList = baseUrl + 'Master/GetEmployeeList';

            var progressBarShow = function () {
                $('#myModal').modal({
                    backdrop: 'static'
                });
                $("#myModal").modal('show');
            }

            var progressBarHide = function () {
                //$('#myModal').modal({
                //    backdrop: 'static'
                //}).modal('hide');
                $("#myModal").modal('hide');
            }

            var setDefaultSelectDropdown = function (object) {

                if (object == null || object == "0")
                    return "0";
                if (object == 0)
                    return PLEASESELECT;
                return object;

            }

            var setDate = function (dFormat) {
                if (dFormat != null) {
                    var dTime = dFormat.split('-');
                    var year = dTime[0];
                    var month = dTime[1];
                    var date = dTime[2].split("T")[0];
                    return month + "/" + date + "/" + year;
                }
                return "";
            }

            var showNotification = function (title, message, type) {
                Toast.fire({
                    type: type,
                    title: message
                })
                //$.alert(message, {
                //    title: title,
                //    closeTime: 5 * 1000,
                //    autoClose: true,
                //    position: ["top-right"],
                //    withTime: false,
                //    type: type, //info,secondary,danger
                //    isOnly: false
                //});
            }

            var addRow = function () {
                count++;
                var initial_row = $('tr.appendableDIV').first().clone();
                var td0 = initial_row.find('td:eq(0) input').attr('name');
                initial_row.find('td:eq(0) input').val("");
                var td1 = initial_row.find('td:eq(1) input').attr('name');
                initial_row.find('td:eq(1) input').val("");
                var td2 = initial_row.find('td:eq(2) input').attr('name');
                initial_row.find('td:eq(2) input').val("");
                var td3 = initial_row.find('td:eq(3) select').attr('name');
                initial_row.find('td:eq(3) select option')[0];
                var td4 = initial_row.find('td:eq(4) select').attr('name');
                initial_row.find('td:eq(4) select option')[0];
                var td5 = initial_row.find('td:eq(5) input').attr('name');
                initial_row.find('td:eq(5) input').val("");

                initial_row.find('td:eq(0) input').attr('name', td0 + count);
                initial_row.find('td:eq(1) input').attr('name', td1 + count);
                initial_row.find('td:eq(2) input').attr('name', td2 + count);
                initial_row.find('td:eq(3) select').attr('name', td3 + count);
                initial_row.find('td:eq(4) select').attr('name', td4 + count);
                initial_row.find('td:eq(5) input').attr('name', td5 + count);


                initial_row.find('td:eq(0) input').attr('id', td0 + count);
                initial_row.find('td:eq(1) input').attr('id', td1 + count);
                initial_row.find('td:eq(2) input').attr('id', td2 + count);
                initial_row.find('td:eq(3) select').attr('id', td3 + count);
                initial_row.find('td:eq(4) select').attr('id', td4 + count);
                initial_row.find('td:eq(5) input').attr('name', td5 + count);

                $(initial_row).appendTo($("#data_TableInfo"));
            }

            var removeRow = function (e) {
                var len = $("#data_TableInfo").find("tr").length;
                if (len > 2) {
                    $(e).closest("tr").remove();
                }
                else {
                    return false;
                }

            }

            var bindMasterDropDownList = function (controlId, GenId, MASTERLIST) {
                controlId.html('');
                controlId.append($("<option></option>").val('0').html(PLEASESELECT));
                $.grep(MASTERLIST, function (n, i) {
                    if (n.GenID === GenId) {
                        controlId.append($("<option></option>").val(n.ID).html(n.MAINDESCR));
                    }
                });
            };

            var LoadMasters = function () {
                $.ajax({
                    url: _url,
                    type: 'GET',
                    async: false,
                    success: function (data) {
                        MASTERLIST = [];
                        MASTERLIST = data.Lst;
                        MASTERLIST.EmployeeList = [];
                        MASTERLIST.EmployeeList = data.EmployeeList;
                        MASTERLIST.SalaryTemplate = data.Salarytemplate;
                    },
                    error: function (request, message, error) {
                        hideModal();
                    }
                });
            };

            var bindEmployeeDropDownList = function (controlId, LIST) {
                controlId.html('');
                controlId.append($("<option></option>").val('0').html(PLEASESELECT));
                $.grep(LIST, function (n, i) {
                    controlId.append($("<option></option>").val(n.EmployeeId).html(n.Name + '[' + n.Code + ']'));
                });
            };

            return {
                LoadMasters: LoadMasters,
                bindMasterDropDownList: bindMasterDropDownList,
                progressBarShow: progressBarShow,
                removeRow: removeRow,
                showNotification: showNotification,
                addRow: addRow,
                progressBarHide: progressBarHide,
                setDate: setDate,
                setDefaultSelectDropdown: setDefaultSelectDropdown,
                bindEmployeeDropDownList: bindEmployeeDropDownList
            };

        })();

        $(function () {
            $(".dPicker").datepicker({
                minDate: 0
            });
            Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000
            });
            EmployeeModule.initEmployee();
            $("#addrowEmployement").on("click", function () {
                counterEmpl = $('#myTableEmployement div').length - 2;
                var newRow = $("<tr>");
                var cols = "";

                cols += '<td><input class="form-control form-control-sm" type="text" name="CompanyName' + counterEmpl + '" id="CompanyName' + counterEmpl + '"class=" " placeholder="Organization Name"/></td>';
                cols += '<td><input class="form-control form-control-sm" type="text" name="CompanyAddress' + counterEmpl + '" id="CompanyAddress' + counterEmpl + '" class=" " placeholder="Organization Addess"/></td>';
                cols += '<td><input class="form-control form-control-sm" type="text" name="Doj" class="dPickerEmPDt  " id="Doj" readonly placeholder="DOJ"/></td>';
                cols += '<td><input class="form-control form-control-sm" type="text" name="Dol" class="dPickerEmPDt  " id="Dol" readonly placeholder="DOL"/></td>';
                cols += '<td><input class="form-control form-control-sm" type="text" name="ReasonOfLeaving' + counterEmpl + '" class=" " id="ReasonOfLeaving' + counterEmpl + '" placeholder="Reason"/></td>';
                cols += '<td><button type="button" id="addrow" class="ibtnDelEmployement btn btn-danger btn-xs" ><span class="fa fa-minus"></span></button></td>';
                newRow.append(cols);

                $("table.order-listEmployement").append(newRow);
                counterEmpl++;

                $('#Doj', newRow).each(function (i) {
                    var newID = 'Doj[]' + counterEmpl;
                    $(this).attr('id', newID);
                    $(".dPickerEmPDt").datepicker();
                });
                $('#Dol', newRow).each(function (i) {
                    var newID = 'Dol[]' + counterEmpl;
                    $(this).attr('id', newID);
                    $(".dPickerEmPDt").datepicker();
                });

            });
            $(".numberOnly").keypress(function (e) {
                //if the letter is not digit then display error and don't type anything
                if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
                    //display error message
                    // $("#errmsg").html("Digits Only").show().fadeOut("slow");
                    return false;
                }
            });
            $("table.order-listEmployement").on("click", ".ibtnDelEmployement", function (event) {
                $(this).closest("tr").remove();
                counterEmpl -= 1;
            });
            $("#addrowfamily").on("click", function () {

                counter2 = $('#myTablfamily tr').length - 2;
                var newRow = $("<tr>");
                var cols = "";
                cols += '<td><select class="form-control form-control-sm" name="familydetailsname' + counter2 + '"  class=" "><option value="0" selected="selected">--Select--</option>   <option value="5" >Father</option>    <option value="6">Mother</option>    <option value="1">Wife</option>    <option value="2">Husband</option>    <option value="3">Son</option>    <option value="4">Daughter</option>    <option value="8">Brother</option>    <option value="7">Sister</option>                <option value="9">Others</option></select></td>';
                cols += '<td><input class="form-control form-control-sm" type="text" name="FamilyMemberName' + counter2 + '" class=" "  id="FamilyMemberName' + counter2 + '" placeholder="Name"/></td>';
                cols += '<td><input class="form-control form-control-sm" type="text" name="FamilyMemberAge' + counter2 + '"class=" " id="FamilyMemberAge' + counter2 + '" placeholder="Age"/></td>';
                cols += '<td><input class="form-control form-control-sm" id="FamilyMemberDob' + counter2 + '" class="fMemberDob  " name="FamilyMemberDob' + counter2 + '" readonly placeholder="DOB"/></td>';
                cols += '<td><input class="form-control form-control-sm" type="text" name="FamilyMembeAdhaarNo' + counter2 + '"class=" " id="FamilyMembeAdhaarNo' + counter2 + '" placeholder="Adhaar No"/></td>';
                cols += '<td><button type="button" id="addrowPro" class="ibtnDelfamily btn btn-danger btn-xs"><span class="fa fa-minus"></span></button></td>';
                newRow.append(cols);

                $("table.order-listfamily").append(newRow);
                counter2++;
                $(".fMemberDob").datepicker();

            });
            $("table.order-listfamily").on("click", ".ibtnDelfamily", function (event) {
                $(this).closest("tr").remove();
                counter2 -= 1;
            });

        });


        //Salary template module
        var InitEmpSalaryTemplate = (function () {

            'use strict';
            var isTemplateReq = false;

            var initSalaryTemplate = function () {
                showModal();;
                $("#dvSalaryTemplate").html("");
                $.ajax({
                    url: baseUrl + 'EmployeeTemplate',
                    type: 'GET'
                }).done(function (data, textStatus, xhr) {
                    $("#templateGrid").html("");
                    if (data.Count > 0) {
                        $("#templateGrid").append(data.Html);
                        $("#SalaryTemplateDataTable").DataTable();
                        $("#dvTemplateNoRecords").hide();
                    }
                    else {
                        $("#templateGrid").html("");
                        $("#dvTemplateNoRecords").show();
                    }

                    var ddlArray = data.List;

                    var html = '<div class="row">                                         '
                      + '<div class="col-md-2">                                          '
                    + 'Define Salary Template Name </div><div class="col-md-6"><input id="tbSalaryTemplateName"                                 '
                    + 'placeholder="Define Salary Template Name" class="form-control form-control-sm" id="tbSalaryTemplate"/> '
                    + '</div>                                                           '
                    + '<div class="col-md-4">                                          '
                    + ' Active  <input id="chkIsActive" type="checkbox" checked="checked"/>                               '
                    + '</div>                                                           '
                    + '</div>                                                       '
                    + '<div class="col-lg-12 table-responsive"><br/>'
                    + '<table id="tableSalaryTemplate" class="table table-bordered table-hover dataTable">                                            '
                    + '<thead>'
                    + '<tr>                                                                                                              '
                    + '<td colspan="7">                                                                        '
                    + '<button type="button" class="btnSaveTemplate btn btn-block btn-outline-success btn-sm" style="width:150px" onclick="InitEmpSalaryTemplate.createSalaryTemplate(this)">Save</button>'
                    + '</td>                                                                                                             '
                    + '</tr>                                                                                                             '
                    + '<tr>                                                                                                              '
                    + '<td>'
                    + '<input type="checkbox" id="select_all">'
                    + '</div>'
                    + '</td>                                                                 '
                    + '<td>Pay Items</td>                                                                                                '
                    + '<td>Type</td>                                                                                                     '
                    + '<td>Value Type</td>                                                                                               '
                    + '<td>Default Value</td>                                                                                            '
                    + '<td>Applicable On</td>                                                                                            '
                    + '<td>Active</td>                                                                                            '
                    + '<td style="display:none">TemplateId</td>                                                                                            '
                    + '<td style="display:none">TemplateItemId</td>                                                                                            '
                    + '</tr></thead><tbody>                                                                                                            ';

                    $.each(data.List, function (i, v) {
                        html += '<tr>'
                        + '<td> '
                        + '<input class="checkbox" type="checkbox" name="check[]" id="' + v.ID + '">'
                        + '</td>'
                        + '<td>' + v.MAINDESCR + '</td>'
                        + '<td value-type=' + parseInt(v.NUMVAL1) + '>' + _bindPayrollItemTypeDDL(v.NUMVAL1) + '</td>                                                                                                 '
                        + '<td><input type="checkbox" onclick="InitEmpSalaryTemplate.isAmountCheck(this,' + i + ')" id="isAmount' + i + '"> Is Amount </td>                                                                       '
                        + '<td><input type="text" class="form-control form-control-sm" placeholder="Default Value" value="0.00"></td>                                     '
                        + '<td>' + _createDDL(ddlArray, i) + '</td>                                          '
                        + '<td><input type="checkbox" checked="checked"></td>'
                         + '<td style="display:none"></td>                                                                                            '
                         + '<td style="display:none"></td>                                                                                            '
                        + '</tr>                                                                                                            ';
                    });
                    html += '</tbody> <tfoot>                                                                                                     '
                    + '<tr>                                                                                                              '
                    + '<td colspan="9">                                                                        '
                    + '<button type="button" class="btnSaveTemplate btn btn-block btn-outline-success btn-sm" style="width:150px" onclick="InitEmpSalaryTemplate.createSalaryTemplate(this)">Save</button>'
                    + '</td>                                                                                                             '
                    + '</tr>                                                                                                             '
                    + '</tfoot>                                                                                                          '
                    + '</table>'
                    + '</div> ';
                    $("#dvSalaryTemplate").append(html);
                    // $("#pBarSalary").hide();
                    var select_all = document.getElementById("select_all"); //select all checkbox
                    var checkboxes = document.getElementsByClassName("checkbox"); //checkbox items

                    //select all checkboxes
                    select_all.addEventListener("change", function (e) {
                        debugger
                        for (i = 0; i < checkboxes.length; i++) {
                            checkboxes[i].checked = select_all.checked;
                            $(checkboxes[i]).attr("data-attr-checked", true);
                        }
                    });

                    for (var i = 0; i < checkboxes.length; i++) {
                        checkboxes[i].addEventListener('change', function (e) { //".checkbox" change 
                            debugger
                            //uncheck "select all", if one of the listed checkbox item is unchecked
                            if (this.checked == false) {
                                select_all.checked = false;
                                $(this).removeAttr("data-attr-checked");
                            }
                            if (this.checked) {
                                $(this).attr("data-attr-checked", true)
                            }
                            //check "select all" if all checkbox items are checked
                            if (document.querySelectorAll('.checkbox:checked').length == checkboxes.length) {
                                select_all.checked = true;
                                $(this).attr("data-attr-checked", true);
                                // select_all.attr("data-attr-checked", true)
                            }
                        });
                    }
                    hideModal();
                }).fail(function (jqXHR, textStatus) {
                    $("#pBarSalary").hide();
                    hideModal();
                }).always(function () {
                    hideModal();
                });
            };

            var isAmountCheck = function (e, i) {

                if ($(e)[0].checked) {
                    $('#isAmountDDL' + i + '').attr('disabled', 'disabled');
                } else {
                    $('#isAmountDDL' + i + '').removeAttr('disabled');
                }
            }

            var createSalaryTemplate = function (e) {
                var templateHtml = $("#tableSalaryTemplate").find("tbody tr");
                // showModal();;
                if ($("#tbSalaryTemplateName").val() == "") {
                    $("#tbSalaryTemplateName").addClass("add-error");
                    hideModal();
                    isTemplateReq = true;
                }
                else {
                    $("#tbSalaryTemplateName").removeClass("add-error");
                    hideModal();
                    isTemplateReq = false;
                }
                if (isTemplateReq) {
                    CommonModule.showNotification("Salary Template Required!", "Salary template and at lease one item required", 'danger');
                    return false;
                }

                $('#tableSalaryTemplate').find("tbody").removeClass('add-error');
                showModal();;
                var SalaryTemplate = new Object();
                SalaryTemplate.Name = $("#tbSalaryTemplateName").val();
                SalaryTemplate.Active = $("#chkIsActive").is(":checked");
                SalaryTemplate.SalaryTemplateId = $("#hfSalaryTemplateId").val();
                var tmlateItems = {};
                var tmlatelST = [];

                templateHtml.each(function (i, v) {
                    var d = $(v).find("td:eq(0) input");
                    if (d.attr("data-attr-checked") != undefined) {
                        tmlateItems.PayrollItemId = d.attr('id');
                        tmlateItems.PayrollItemText = $(v).find("td:eq(1)").html();
                        tmlateItems.ValueType = $(v).find("td:eq(2)").attr('value-type');
                        tmlateItems.ISAmount = $(v).find("td:eq(3) input").is(":checked");
                        tmlateItems.Active = $(v).find("td:eq(6) input").is(":checked");
                        tmlateItems.DefaultValue = $(v).find("td:eq(4) input").val();
                        var pItems = $(v).find("td:eq(5) select option:selected");
                        tmlateItems.ApplicableOn = '';
                        if (pItems.length > 0) {
                            $.each(pItems, function (j, w) {
                                var text = $(w).text();
                                var value = $(w).val();
                                if (tmlateItems.ApplicableOn != '') {
                                    tmlateItems.ApplicableOn += "," + value;
                                    tmlateItems.ApplicableItemText += "," + text;
                                }
                                else {
                                    tmlateItems.ApplicableOn = value;
                                    tmlateItems.ApplicableItemText = text;
                                }
                            })
                        }

                        tmlateItems.SalaryTemplateItemId = $(v).find("td:eq(8)").html();
                        tmlateItems.SalaryTemplateId = $(v).find("td:eq(7)").html();
                        tmlatelST.push(tmlateItems);
                        tmlateItems = {};
                    }
                });
                debugger
                SalaryTemplate.SalaryTemplateItemRequestModels = tmlatelST;
                if ($(e).text() == 'Update') {
                    SalaryTemplate.StatementType = "Update";
                    _salayTemplateAddOrUpdate('Update', SalaryTemplate);
                }
                else {
                    SalaryTemplate.StatementType = "Insert";
                    _salayTemplateAddOrUpdate('Save', SalaryTemplate);
                }
                //hideModal();
                //window.location.href = window.location.href;
            };

            var _salayTemplateAddOrUpdate = function (operation, salaryTemplate) {
                var type = operation == 'Save' ? 'POST' : 'PUT';
                $.ajax({
                    url: baseUrl + 'employeetemplate',
                    type: type,
                    contentType:
                       "application/json;charset=utf-8",
                    data: JSON.stringify(salaryTemplate),
                    success: function (response) {
                        // $("#liEmployee").trigger("click");
                        if (operation == 'Save') {
                            CommonModule.showNotification("Template Saved", response.Html, 'success');
                        }
                        else {
                            CommonModule.showNotification("Template updated", response.Html, 'success');

                        }
                        InitEmpSalaryTemplate.initSalaryTemplate();
                        EmployeeModule.clearControl();
                        // window.location.href = window.location.href;

                    },
                    error: function (request, message, error) {
                        // handleException(request, message, error);
                        CommonModule.showNotification(message, error, 'danger');
                    }
                });
            };

            var _createDDL = function (arr, i) {
                var t = '<select class=" " id="isAmountDDL' + i + '" multiple>                                                                            '
                         + '<option value="0">--Please Select--</option>                                                                      '
                $.each(arr, function (j, w) {
                    t += '<option value="' + w.ID + '">' + w.MAINDESCR + '</option>                                                                      '
                });
                t += '</select>  ';
                return t;
            };

            var RowDelete = function (id) {
                try {
                    if (!confirm("Do You Want To Delete")) {
                        return false;
                    }
                    showModal();;
                    $.ajax({
                        url: baseUrl + 'EmployeeTemplate/' + id,
                        type: 'DELETE',
                        success: function (info) {
                            InitEmpSalaryTemplate.initSalaryTemplate();
                            EmployeeModule.clearControl();
                            $(".btnSaveTemplate").text("Save");
                            CommonModule.showNotification("Template deleted", info.Html, 'success');
                            hideModal();
                        },
                        error: function (request, message, error) {
                            InitEmpSalaryTemplate.initSalaryTemplate();
                            EmployeeModule.clearControl();
                            CommonModule.showNotification(message, error, 'danger');
                            hideModal();
                        }
                    });
                }
                catch (Ex) {

                }
            }

            var getRowVal = function (id) {
                showModal();;
                $.ajax({
                    url: baseUrl + 'EmployeeTemplate/' + id,
                    type: 'GET',
                    success: function (data) {
                        var empObject = data.dtEmployeeGet[0];
                        var templateItems = data.dtEmpddress;
                        $("#tbSalaryTemplateName").val(empObject.Name);
                        $("#hfSalaryTemplateId").val(empObject.SalaryTemplateId);
                        $("#chkIsActive").removeAttr('checked');
                        if (empObject.Active == 1) {
                            $("#chkIsActive").attr('checked', 'checked');
                        }

                        $('#tableSalaryTemplate').find('tbody tr').each(function (i, v) {
                            $(v).find("td:eq(4) input").val('');
                            $(v).find("td:eq(0) input").removeAttr("checked", "checked");
                            $(v).find("td:eq(0) input").removeAttr("data-attr-checked");
                            $(v).find("td:eq(3) input").removeAttr("checked", "checked");
                            $(v).find("td:eq(6) input").removeAttr("checked", "checked");
                            $(v).find("td:eq(7)").html('');
                            $(v).find("td:eq(8)").html('');
                        });
                        $(templateItems).each(function (j, l) {
                            var d = l.PayrollItemId;
                            $('#tableSalaryTemplate').find('tbody tr').each(function (i, v) {
                                var dd = $(v).find("td:eq(0) input");
                                if (d === dd.attr('id')) {
                                    $(v).find("td:eq(4) input").val(l.DefaultValue);
                                    $(v).find("td:eq(0) input").attr("checked", "checked");
                                    $(v).find("td:eq(0) input").attr("data-attr-checked", true);
                                    if (l.ISAmount == 1) {
                                        $(v).find("td:eq(3) input").attr("checked", "checked");
                                        $(v).find("td:eq(5) select").attr('disabled', 'disabled');
                                    }
                                    else {
                                        $(v).find("td:eq(5) select").removeAttr('disabled');
                                    }
                                    if (l.Acitve == 1) {
                                        $(v).find("td:eq(6) input").removeAttr("checked", "checked");
                                        $(v).find("td:eq(6) input").attr("checked", "checked");
                                    }

                                    if (l.ApplicableOn !== '') {
                                        var txty = l.ApplicableOn.split(',');
                                        $.each(txty, function (j, l) {
                                            var pItems = $(v).find("td:eq(5) select option")
                                            $.each(pItems, function (k, w) {
                                                if (w.value === l) {
                                                    $(v).find("td:eq(5) select").find('option[value="' + l + '"]').attr("selected", true);
                                                }
                                            })
                                        })
                                    }
                                    $(v).find("td:eq(7)").html(l.SalaryTemplateID);
                                    $(v).find("td:eq(8)").html(l.SalaryTemplateItemID);
                                    return false;
                                }
                            });
                        });
                        $(".btnSaveTemplate").text("Update");
                        $("input,select").removeClass("add-error");
                        hideModal();
                    },
                    error: function (request, message, error) {
                        hideModal();
                    }
                });
            }



            var onSalaryTemplateChange = function () {
                showModal();;
                var selectedVal = $("#FeaturedContent_ddlSalaryTemplate option:selected").val();
                $.ajax({
                    url: baseUrl + 'EmployeeTemplate/' + selectedVal,
                    type: 'GET',
                    success: function (data) {
                        var deductionAmt = 0;
                        var additionalAmt = 0;
                        var earningAmt = 0;
                        var templateObject = data.dtEmployeeGet;
                        templateItemObject = [];
                        templateItemObject = data.dtEmpddress;
                        var html = '<div class="table-responsive"><table id="tableSalaryTemplate" class="table table-bordered table-hover dataTable"><thead><tr><th>Payroll Item</th><th>Payroll Type</th><th>Default Value</th><th>Value Type</th><th>Applicable On</th><th>Active</th><th>Final Value</th></tr><thead><tbody>';
                        $('.salaryTemplateList').html("");
                        $.each(templateItemObject, function (i, v) {
                            var actv = v.Acitve ? 'Yes' : 'No';
                            var vType = v.ISAmount ? "Rs." : "%";
                            html += '<tr>'
                            + '<td>' + v.PayrollItemText + '</td>'
                            + '<td>' + _bindPayrollItemTypeDDL(v.ValueType) + '</td>                                     '
                            + '<td>' + parseFloat(v.DefaultValue).toFixed(2) + '</td>'
                            + '<td>' + vType + '</td>'
                            + '<td>' + v.ApplicableItemText + '</td>                                          '
                            + '<td>' + actv + '</td>                                          '
                            + '<td>' + _calculateLastValue(v) + '</td>                                          '
                            + '</tr>';

                        });
                        html += '</tbody><br/>'
                            + '<tfooter><tr><th>CTC</th><th>Net Salary</th><th>Gross</th><th>Earning</th><th>Deduction</th><th>Aditional</th></tr>'
                            + '<tr><td id="tdCTC"></td><td id="tdNtSal"></td><td id="tdGross"></td><td id="tdEarning"></td><td id="tdDeduction"></td><td id="tdAdtional"></td></tr>'
                            + '</tfooter></table></div>';

                        var l = $(html).find('tbody tr');
                        $.each(l, function (i, v) {

                            if ($(v).find('td:eq(1)').html() === 'Earning') {
                                earningAmt += parseFloat($(v).find('td:eq(6)').html());
                            }
                            if ($(v).find('td:eq(1)').html() === 'Aditional') {
                                additionalAmt += parseFloat($(v).find('td:eq(6)').html());
                            }
                            if ($(v).find('td:eq(1)').html() === 'Deduction') {
                                deductionAmt += parseFloat($(v).find('td:eq(6)').html());
                            }
                        })

                        $('.salaryTemplateList').append(html);
                        var ctc = (earningAmt + additionalAmt).toFixed(2)
                        var ntSal = (earningAmt - deductionAmt).toFixed(2);
                        $('#tdCTC').html('').html(ctc);
                        $('#tdNtSal').html('').html(ntSal);
                        $('#tdGross').html(earningAmt.toFixed(2));
                        $('#tdEarning').html('').html(earningAmt.toFixed(2));
                        $('#tdDeduction').html('').html(deductionAmt.toFixed(2));
                        $('#tdAdtional').html('').html(additionalAmt.toFixed(2));

                        hideModal();
                        deductionAmt = 0;
                        additionalAmt = 0;
                        earningAmt = 0;
                    },
                    error: function (request, message, error) {
                        $("#myModal").modal('hide');
                    }
                });
            };

            var _calculateLastValue = function (v) {

                var globalColChild = {};
                if (v.ISAmount) {
                    globalColChild.PayrollItemText = v.PayrollItemText;
                    globalColChild.DefaultValue = parseFloat(v.DefaultValue);
                    globalCol.push(globalColChild);
                    // templateItemObject[i].finalAmount = parseFloat(v.DefaultValue);
                    return parseFloat(v.DefaultValue);
                }
                else {
                    var percentage = parseFloat(v.DefaultValue);
                    var amt = 0;
                    var txt = v.ApplicableItemText.split(',');
                    for (var xxx = 0; xxx < txt.length; xxx++) {
                        for (var iii = 0; iii < globalCol.length; iii++) {
                            if (txt[xxx] === globalCol[iii].PayrollItemText) {
                                //templateItemObject[iii].finalAmount=
                                amt += globalCol[iii].DefaultValue
                            }
                        }
                    }
                    var finalAmount = ((amt * percentage) / 100);
                    globalColChild.PayrollItemText = v.PayrollItemText;
                    globalColChild.DefaultValue = parseFloat(finalAmount);
                    globalCol.push(globalColChild);
                    return parseFloat(finalAmount);;
                }
            };

            var _calculateFinalAmount = function (v, i) {
                if (v.ISAmount) {
                    // templateItemObject[i].finalAmount = parseFloat(v.DefaultValue);
                    return parseFloat(v.DefaultValue);
                }
                var percentage = v.DefaultValue;
                var addayrollItems = 0;
                if (v.ApplicableItemText != '') {
                    var matched = [];
                    var txt = v.ApplicableItemText.split(',');
                    for (var xxx = 0; xxx < txt.length; xxx++) {
                        for (var iii = 0; iii < templateItemObject.length; iii++) {
                            if (txt[xxx] === templateItemObject[iii].PayrollItemText) {
                                //templateItemObject[iii].finalAmount=
                                matched.push(templateItemObject[iii]);
                            }
                        }
                    }
                    for (var tt = 0; tt < matched.length; tt++) {
                        if (matched[tt].ISAmount) {
                            return parseFloat(matched[tt].DefaultValue);
                        }
                        else {
                            var defaultValue = matched[tt].DefaultValue;
                            var payrollItemApplicableText = matched[tt].ApplicableItemText;
                            var rr = payrollItemApplicableText.split(',');
                            $.each(rr, function (i, val) {
                                $.each(templateItemObject, function (j, jval) {
                                    if (val === jval.PayrollItemText) {
                                        //if (jval.ISAmount) {
                                        //    addayrollItems += parseFloat(jval.DefaultValue);                                            
                                        //}
                                        var dVal = jval.DefaultValue;
                                        var finalAmount = ((defaultValue * dVal) / 100);
                                        addayrollItems += parseFloat(finalAmount);
                                    }
                                })
                            })

                        }
                    }
                }
                return parseFloat((percentage * addayrollItems) / 100);
            };

            var _bindPayrollItemTypeDDL = function (arr) {

                var t = '';
                if (parseInt(arr) === 1) {
                    t = ADITIONAL;
                }
                else if (arr === undefined) {
                    t = na;
                }
                else if (arr === 3) {
                    t = EARNING;
                }
                else {
                    t = DEDUCTION;
                }
                return t;
            };

            return {
                _bindPayrollItemTypeDDL: _bindPayrollItemTypeDDL,
                initSalaryTemplate: initSalaryTemplate,
                createSalaryTemplate: createSalaryTemplate,
                getRowVal: getRowVal,
                RowDelete: RowDelete,
                onSalaryTemplateChange: onSalaryTemplateChange,
                isAmountCheck: isAmountCheck,
                _calculateLastValue: _calculateLastValue
            };
        })();
    </script>
</asp:Content>