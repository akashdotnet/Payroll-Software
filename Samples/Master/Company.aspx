<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Company.aspx.cs" Inherits="Samples.Master.Company"
    MasterPageFile="~/Main.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <link href="../css/jquery-ui.css" rel="stylesheet" />
    <script src="../js/jquery-1.7.2.min.js"></script>
    <script src="../js/jquery-ui.js"></script>
    <link href="../css/fixedHeader.bootstrap4.min.css" rel="stylesheet" />
    <link href="../AdminLTE-3.0.1/plugins/toastr/toastr.min.css" rel="stylesheet" />
    <!-- SweetAlert2 -->
    <link rel="stylesheet" href="../AdminLTE-3.0.1/plugins/sweetalert2-theme-bootstrap-4/bootstrap-4.min.css">
    <script src="../AdminLTE-3.0.1/plugins/datatables/jquery.dataTables.min.js"></script>
    <script src="../AdminLTE-3.0.1/plugins/datatables/dataTables.bootstrap.min.js"></script>
    <link rel="stylesheet" href="../AdminLTE-3.0.1/plugins/datatables-bs4/css/dataTables.bootstrap4.css">
    <link href="../AdminLTE-3.0.1/plugins/icheck-bootstrap/icheck-bootstrap.min.css" rel="stylesheet" />
    <script src="../js/jquery.maskedinput-1.3.1.min_.js"></script>
    <style>
        .add-error {
            box-sizing: border-box;
            border: 2px solid red;
            border-radius: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <form method="post" id="CompanyInfo" enctype="multipart/form-data" runat="server">
        <section class="content">
            <div class="tab-pane card-body" id="custom-content-above-company" role="tabpanel" aria-labelledby="custom-content-above-profile-tab">
                <h2 class="lead mb-0">General Information</h2>

                <div class="tab-custom-content">
                </div>
                <input type="hidden" id="hfCmId" />
                <div class="row">
                    <input type="hidden" id="CompanyId" name="CompanyId" />
                    <div class="col-md-3">
                        Name
                        <asp:TextBox runat="server" ID="tbName" CssClass="form-control form-control-sm tbName" placeholder="Name"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        Short Name
                        <asp:TextBox runat="server" ID="tbShortName" CssClass="form-control form-control-sm " placeholder="Short Name"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        Fax #
                        <asp:TextBox runat="server" ID="tbFax" CssClass="form-control form-control-sm " placeholder="Fax #"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        Address
                        <asp:TextBox ID="tbAddress" runat="server" CssClass="form-control form-control-sm " placeholder="Address"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        Email
                        <asp:TextBox runat="server" ID="tbEmail" CssClass="form-control form-control-sm " placeholder="Email"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        PhoneNo
                        <asp:TextBox runat="server" ID="tbPhoneNo" CssClass="form-control form-control-sm " placeholder="PhoneNo"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        Website
                        <asp:TextBox runat="server" ID="tbWebsite" CssClass="form-control form-control-sm " placeholder="Website"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        PinCode
                        <asp:TextBox ID="tbPinCode" runat="server" CssClass="form-control form-control-sm " placeholder="PinCode"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        Location
                         <asp:DropDownList ID="ddlMasterLocation" runat="server" Width="100%" CssClass="form-control custom-select">
                         </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        Country
                        <asp:DropDownList ID="ddlMasterCountry" runat="server" Width="100%" CssClass="form-control custom-select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        State
                        <asp:DropDownList ID="ddlMasterState" runat="server" Width="100%" CssClass="form-control custom-select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        City
                        <asp:TextBox ID="tbCity" runat="server" CssClass="form-control form-control-sm " placeholder="City"></asp:TextBox>
                    </div>
                </div>
                <br />
                <h2 class="lead mb-0">Legal Information</h2>

                <div class="tab-custom-content">
                </div>
                <div class="row">
                    <div class="col-md-3">
                        Legal Name
                        <asp:TextBox runat="server" ID="tbLegalName" CssClass="form-control form-control-sm " placeholder="Name"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        Legal Short Name
                        <asp:TextBox runat="server" ID="tbLegalShortName" CssClass="form-control form-control-sm " placeholder="Legal Short Name"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        Legal Fax #
                        <asp:TextBox runat="server" ID="tbLegalFax" CssClass="form-control form-control-sm " placeholder="Legal Fax #"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        Legal Address
                        <asp:TextBox ID="tbLegalAddress" runat="server" CssClass="form-control form-control-sm " placeholder="Legal Address"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        Email
                        <asp:TextBox runat="server" ID="tbLegalEmail" CssClass="form-control form-control-sm " placeholder="Email"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        PhoneNo
                        <asp:TextBox runat="server" ID="tbLegalPhoneNo" CssClass="form-control form-control-sm " placeholder="PhoneNo1"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        Website
                        <asp:TextBox runat="server" ID="tbLegalWebsite" CssClass="form-control form-control-sm " placeholder="Website"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        PinCode
                        <asp:TextBox ID="tbLegalPinCode" runat="server" CssClass="form-control form-control-sm " placeholder="PinCode"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        Location
                        <asp:DropDownList ID="ddlLegalLocation" runat="server" Width="100%" CssClass="form-control custom-select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        Country
                        <asp:DropDownList ID="ddlLegalCountry" runat="server" Width="100%" CssClass="form-control custom-select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        State
                        <asp:DropDownList ID="ddlLegalState" runat="server" Width="100%" CssClass="form-control custom-select">
                        </asp:DropDownList>
                    </div>
                    <div class="col-md-3">
                        City
                        <asp:TextBox ID="tbLegalCity" runat="server" CssClass="form-control form-control-sm " placeholder="City"></asp:TextBox>
                    </div>
                </div>

                <br />
                <h2 class="lead mb-0">Company Setting</h2>

                <div class="tab-custom-content">
                </div>
                <div class="row">
                    <div class="col-md-3">
                        Fiscal Year's First Month 
                        <asp:TextBox runat="server" ID="tbFiscalYear" CssClass="form-control form-control-sm " placeholder="Fiscal Year's First Month"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        Tax Month
                        <asp:TextBox runat="server" ID="tbTaxMonth" CssClass="form-control form-control-sm " placeholder="Tax Month"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        Year
                        <asp:TextBox runat="server" ID="tbYear" CssClass="form-control form-control-sm " placeholder="Year"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        Default Currency
                        <asp:TextBox ID="tbDefaultCurrency" runat="server" CssClass="form-control form-control-sm " placeholder="Default Currency"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        FEIN
                        <asp:TextBox runat="server" ID="tbFEIN" CssClass="form-control form-control-sm " placeholder="FEIN"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        No of Year Required
                        <asp:TextBox runat="server" ID="tbNoofYear" CssClass="form-control form-control-sm " placeholder="No of Year Required"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        SSN
                        <asp:TextBox runat="server" ID="tbSSN" CssClass="form-control form-control-sm " placeholder="SSN"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        Payroll Monthly Start Date
                        <asp:TextBox ID="tbPayrollMonthlyStartDate" runat="server" CssClass="form-control form-control-sm dPicker" placeholder="Payroll Monthly Start Date"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        Start Date
                        <asp:TextBox runat="server" ID="tbStartDate" CssClass="form-control form-control-sm dPicker" placeholder="Start Date"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        End Date
                        <asp:TextBox runat="server" ID="tbEndDate" CssClass="form-control form-control-sm dPicker" placeholder="End Date"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>

                <br />

                <h2 class="lead mb-0">Tax Form Information</h2>

                <div class="tab-custom-content">
                </div>
                <div class="row">
                    <div class="col-md-3">
                        EPF #
                        <asp:TextBox runat="server" ID="tbEPF" CssClass="form-control form-control-sm " placeholder="EPF #"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        ESIC #
                        <asp:TextBox runat="server" ID="tbESIC" CssClass="form-control form-control-sm " placeholder="ESIC #"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        PAN #
                        <asp:TextBox runat="server" ID="tbPAN" CssClass="form-control form-control-sm " placeholder="PAN #"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                        TAN #
                        <asp:TextBox ID="tbTAN" runat="server" CssClass="form-control form-control-sm " placeholder="TAN #"></asp:TextBox>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-3">
                        Premises Code # 
                        <asp:TextBox runat="server" ID="tbPremisesCode" CssClass="form-control form-control-sm " placeholder="Premises Code #"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        GST #
                        <asp:TextBox runat="server" ID="tbGST" CssClass="form-control form-control-sm " placeholder="GSTN #"></asp:TextBox>

                    </div>
                    <div class="col-md-3">
                        Service Tax #

                        <asp:TextBox runat="server" ID="tbServiceTax" CssClass="form-control form-control-sm " placeholder="Service Tax #"></asp:TextBox>
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
                <div class="row" style="display: none">
                    <div class="col-md-3">
                        Upload Company Logo

                        <input type="file" id="Path" class="form-control" name="file" onchange="onFileSelected(event)" placeholder="Select Photo">
                        <input type="hidden" class="form-control" id="imagePath" name="imagePath">
                    </div>
                    <div class="col-md-3">
                        <div style="height: 200px;">
                            <div style="width: 150px; height: 150px;" class="col-lg-offset-4" id="logoimage">
                                <img id="limage" class="img-thumbnail" onload="LoadImage(this);" />
                            </div>
                            <div style="width: 150px; height: 150px;" class="col-lg-offset-4" id="logoid">
                            </div>
                        </div>

                    </div>
                    <div class="col-md-3">
                    </div>
                    <div class="col-md-3">
                    </div>
                </div>
                <div class="row margin-bottom-none">
                    <div class="col-sm-10">
                        &nbsp;
                    </div>
                    <div class="col-sm-1 pull-right">
                        <button type="button" class="btn btn-block btn-outline-secondary btn-sm" onclick="CompanyModule.clearControl();">Cancel</button>
                    </div>
                    <div class="col-sm-1 pull-left">
                        <button type="button" class="btn btn-block btn-outline-info btn-sm" id="btnSave" onclick="CompanyModule.saveCompany();">
                            Save<i class="fa fa-spinner fa-spin spinner" style="font-size: 10px; float: right; display: none"></i>
                        </button>
                    </div>
                </div>
                <br />
                <div class="card">
               
                <div class="card-body">
                    <div id="data"></div>
                </div>
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
        var _url = baseUrl + 'Master/GetMaster';
        var MASTERLIST = [];
        var PLEASESELECT = '--Select--';
        $(function () {
            showModal();
            $(".dPicker").datepicker({
                minDate: 0
            });
            Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000
            });
            LoadMasters();
            var masterLocation = $("#FeaturedContent_ddlMasterLocation,#FeaturedContent_ddlLegalLocation");  //010  
           // var masterCountry = $("#FeaturedContent_ddlMasterCountry,#FeaturedContent_ddlLegalCountry");  //010  
           // var masterState = $("#FeaturedContent_ddlMasterState,#FeaturedContent_ddlLegalState");  //010  
            bindMasterDropDownList(masterLocation, '010', MASTERLIST);
           // bindMasterDropDownList(masterCountry, '005', MASTERLIST);
            // bindMasterDropDownList(masterState, '010', MASTERLIST);
            hideModal();
            CompanyModule.loadCompany();

        });


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
                },
                error: function (request, message, error) {
                    hideModal();
                }
            });
        };

        var bindMasterDropDownList = function (controlId, GenId, MASTERLIST) {
            controlId.html('');
            controlId.append($("<option></option>").val('0').html(PLEASESELECT));
            $.grep(MASTERLIST, function (n, i) {
                if (n.GenID === GenId) {
                    controlId.append($("<option></option>").val(n.ID).html(n.MAINDESCR));
                }
            });
        };

        var CompanyModule = (function () {
            var loadCompany = function () {
                $.ajax({
                    url: baseUrl + 'Company',
                    type: 'GET'
                }).done(function (data, textStatus, xhr) {
                    debugger
                     var masterCountry = $("#FeaturedContent_ddlMasterCountry,#FeaturedContent_ddlLegalCountry");  //010  
                     var masterState = $("#FeaturedContent_ddlMasterState,#FeaturedContent_ddlLegalState");  //010  
                     masterCountry.html('');
                     masterState.html('');
                     masterCountry.append($("<option></option>").val('0').html(PLEASESELECT));
                     masterState.append($("<option></option>").val('0').html(PLEASESELECT));
                     $.grep(data.Load_Country, function (n, i) {
                        //if (n.GenID === GenId) {
                         masterCountry.append($("<option></option>").val(n.Country_Id).html(n.Country_Name));
                        //}
                    });

                    $.grep(data.Load_State, function (n, i) {
                        //if (n.GenID === GenId) {
                        masterState.append($("<option></option>").val(n.StateID).html(n.StateName));
                        //}
                    });

                    $("#data").html("");
                    if (data.Count > 0) {
                        $("#data").append(data.Html);
                        $("#CompanyDataTable").DataTable();
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
            }

            var saveCompany = function () {
                var d = '#FeaturedContent_';
                var tbName = $('.tbName');
                if (tbName.val().trim() === '') {
                    tbName.addClass('add-error');
                    $(document).scrollTop($("#CompanyInfo").offset().top);
                    return false;
                }
                showModal();
                tbName.removeClass('add-error');
                var tbShortName = $(d + 'tbShortName').val();
                var tbFax = $(d + 'tbFax').val();
                var tbAddress = $(d + 'tbAddress').val();
                var tbEmail = $(d + 'tbEmail').val();
                var tbPhoneNo = $(d + 'tbPhoneNo').val();
                var tbWebsite = $(d + 'tbWebsite').val();
                var tbPinCode = $(d + 'tbPinCode').val();

                var ddlMasterLocation = $(d + 'ddlMasterLocation option:selected').val() == PLEASESELECT
                         ? 0 : $(d + 'ddlMasterLocation option:selected').val();
                var ddlMasterCountry = $(d + 'ddlMasterCountry option:selected').val() == PLEASESELECT
                         ? 0 : $(d + 'ddlMasterCountry option:selected').val();
                var ddlMasterState = $(d + 'ddlMasterState option:selected').val() == PLEASESELECT
                         ? 0 : $(d + 'ddlMasterState option:selected').val();
                var tbCity = $(d + 'tbCity').val();
                var tbLegalName = $(d + 'tbLegalName').val();
                var tbLegalShortName = $(d + 'tbLegalShortName').val();
                var tbLegalFax = $(d + 'tbLegalFax').val();
                var tbLegalEmail = $(d + 'tbLegalEmail').val();
                var tbLegalPhoneNo = $(d + 'tbLegalPhoneNo').val();
                var tbLegalWebsite = $(d + 'tbLegalWebsite').val();
                var tbLegalPinCode = $(d + 'tbLegalPinCode').val();
                var ddLegallLocation = $(d + 'ddlLegalLocation option:selected').val() == PLEASESELECT
                         ? 0 : $(d + 'ddlLegalLocation option:selected').val();
                var ddLegallCountry = $(d + 'ddlLegalCountry option:selected').val() == PLEASESELECT
                         ? 0 : $(d + 'ddlLegalCountry option:selected').val();
                var ddLegallState = $(d + 'ddlLegalState option:selected').val() == PLEASESELECT
                        ? 0 : $(d + 'ddlLegalState option:selected').val();
                var tbLegalCity = $(d + 'tbLegalCity').val();
                var tbFiscalYear = $(d + 'tbFiscalYear').val();
                var tbTaxMonth = $(d + 'tbTaxMonth').val();
                var tbYear = $(d + 'tbYear').val();
                var tbDefaultCurrency = $(d + 'tbDefaultCurrency').val();
                var tbFEIN = $(d + 'tbFEIN').val();
                var tbNoofYear = $(d + 'tbNoofYear').val();
                var tbSSN = $(d + 'tbSSN').val();
                var tbPayrollMonthlyStartDate = $(d + 'tbPayrollMonthlyStartDate').val();
                var tbStartDate = $(d + 'tbStartDate').val();
                var tbEndDate = $(d + 'tbEndDate').val();
                var tbEPF = $(d + 'tbEPF').val();
                var tbESIC = $(d + 'tbESIC').val();
                var tbPAN = $(d + 'tbPAN').val();
                var tbTAN = $(d + 'tbTAN').val();
                var tbPremisesCode = $(d + 'tbPremisesCode').val();
                var tbGST = $(d + 'tbGST').val();
                var tbServiceTax = $(d + 'tbServiceTax').val();
                var CompanyMaster = new Object();
                CompanyMaster.LocID = ddlMasterLocation;
                CompanyMaster.AssoID = null;
                CompanyMaster.Name = tbName.val().trim();
                CompanyMaster.DisplayName = $(d + 'tbShortName').val();
                CompanyMaster.Abbrv = $(d + 'tbShortName').val();
                CompanyMaster.Address = $(d + 'tbAddress').val();
                CompanyMaster.City = $(d + 'tbCity').val();
                CompanyMaster.Zip = $(d + 'tbPinCode').val();
                CompanyMaster.State = ddlMasterState;
                CompanyMaster.CountryID = ddlMasterCountry;
                CompanyMaster.PhoneNo1 = tbPhoneNo;
                CompanyMaster.PhoneNo2 = tbPhoneNo;
                CompanyMaster.PhoneNo3 = tbPhoneNo;
                CompanyMaster.Fax = $(d + 'tbFax').val();
                CompanyMaster.Email = $(d + 'tbEmail').val();
                CompanyMaster.WebURL = tbWebsite;
                CompanyMaster.LegalName = $(d + 'tbLegalName').val();
                CompanyMaster.LegalShortName = $(d + 'tbLegalShortName').val();
                CompanyMaster.LegalFax = $(d + 'tbLegalFax').val();
                CompanyMaster.LegalAddress = $(d + 'tbLegalAddress').val();
                CompanyMaster.LegalEmail = $(d + 'tbLegalEmail').val();
                CompanyMaster.LegalPhone = tbLegalPhoneNo;
                CompanyMaster.LegalWebsite = $(d + 'tbLegalWebsite').val();
                CompanyMaster.LegalPincode = $(d + 'tbLegalPinCode').val();
                CompanyMaster.LegalLocationId = ddLegallLocation;
                CompanyMaster.LegalCountryId = ddLegallCountry;
                CompanyMaster.LegalStateId = ddLegallState;
                CompanyMaster.LegalCityId = $(d + 'tbLegalCity').val();
                CompanyMaster.CityId = $(d + 'tbCityId').val();
                CompanyMaster.StateId = $(d + 'tbStateId').val();
                CompanyMaster.FiscalYearMonth = $(d + 'tbFiscalYear').val();
                CompanyMaster.Year = $(d + 'tbYear').val();
                CompanyMaster.TaxMonth = $(d + 'tbTaxMonth').val();
                CompanyMaster.NoofYear = $(d + 'tbNoofYear').val();
                CompanyMaster.Fein = tbFEIN;
                CompanyMaster.Ssn = tbSSN;
                CompanyMaster.PayrollStartDate = tbPayrollMonthlyStartDate == "" ? null : tbPayrollMonthlyStartDate;
                CompanyMaster.StartDate = tbStartDate == "" ? null : tbStartDate;
                CompanyMaster.Enddate = tbEndDate == "" ? null : tbEndDate;
                CompanyMaster.EPF = tbEPF;
                CompanyMaster.ESIC = tbESIC;
                CompanyMaster.PAN = tbPAN;
                CompanyMaster.TAN = tbTAN;
                CompanyMaster.PREMISES = tbPremisesCode;
                CompanyMaster.GST = tbGST;
                CompanyMaster.ServiceTax = tbServiceTax;
                CompanyMaster.DefaultCurrency = tbDefaultCurrency;
                if ($("#btnSave").text() == 'Update') {
                    CompanyMaster.StatementType = "Update";
                    CompanyMaster.pRowID = $("#hfCmId").val();
                    _addOrUpdate('Update', CompanyMaster);
                }
                else {
                    CompanyMaster.pRowID = Math.floor(1000000000 + Math.random() * 9000000000);
                    CompanyMaster.StatementType = "Insert";
                    _addOrUpdate('Save', CompanyMaster);
                }
            };

            var clearControl = function () {
                $("#hfCmId").val("");
                $("input,select").removeClass("add-error");
                $("input").val("");
                $("select").val("0");
                $("#btnSave").text("Save");
            };

            var _addOrUpdate = function (operation, CompanyMaster) {
                var type = operation == 'Save' ? 'POST' : 'PUT';
                $.ajax({
                    url: baseUrl + 'Company',
                    type: type,
                    contentType:
                       "application/json;charset=utf-8",
                    data: JSON.stringify(CompanyMaster),
                    success: function (response) {
                        CompanyModule.loadCompany();
                        if (operation == 'Save') {
                            CompanyModule.showNotification("Company Master Saved", response.Html, 'success');
                        }
                        else {
                            CompanyModule.showNotification("Company Master updated", response.Html, 'success');
                        }
                        CompanyModule.clearControl();
                        hideModal();
                        // window.location.href = window.location.href;

                    },
                    error: function (request, message, error) {
                        // handleException(request, message, error);
                        showNotification(message, error, 'danger');
                    }
                });
            }

            var showNotification = function (title, message, type) {
                Toast.fire({
                    type: type,
                    title: message
                })
            }

            var RowDelete = function (e) {
                var id = $(e).attr("data-id")
                try {
                    if (!confirm("Do You Want To Delete")) {
                        return false;
                    }
                    showModal();;
                    $.ajax({
                        url: baseUrl + 'Company/' + id,
                        type: 'DELETE',
                        success: function (info) {
                            CompanyModule.loadCompany();
                            $("#btnSave").text("Save");
                            CompanyModule.showNotification("Company deleted", info.Html, 'success');
                            hideModal();
                        },
                        error: function (request, message, error) {
                            hideModal();
                        }
                    });
                }
                catch (Ex) {

                }
            }

            var getRowVal = function (e) {
                $("#btnSave").text("Update");
                var id = $(e).attr("data-id")
                $("#hfCmId").val(id);
                showModal();
                $.ajax({
                    url: baseUrl + 'Company/' + id,
                    type: 'GET',
                    success: function (data) {
                        var obj = data.dtCompanyMasterGet[0];
                        $("#hfCmId").val(obj.pRowID)
                        var d = '#FeaturedContent_';
                        $('.tbName').val(obj.Name)
                        $(d + 'tbShortName').val(obj.DisplayName);
                        $(d + 'tbFax').val(obj.Fax);
                        $(d + 'tbAddress').val(obj.Address);
                        $(d + 'tbEmail').val(obj.Email);
                        $(d + 'tbPhoneNo').val(obj.PhoneNo1); 
                        $(d + 'tbWebsite').val(obj.WebURL);
                        $(d + 'tbPinCode').val(obj.Zip);

                        $(d + 'ddlMasterLocation').val(obj.LocID)
                        $(d + 'ddlMasterCountry').val(obj.CountryID ===null ? obj.CountryID: obj.CountryID.trim());
                        $(d + 'ddlMasterState').val( obj.State ===null ? obj.State: obj.State.trim());
                        $(d + 'tbCity').val(obj.City);
                        $(d + 'tbLegalName').val(obj.LegalName);
                        $(d + 'tbLegalShortName').val(obj.LegalShortName);
                        $(d + 'tbLegalFax').val(obj.LegalFax);
                        $(d + 'tbLegalEmail').val(obj.LegalEmail);
                        $(d + 'tbLegalPhoneNo').val(obj.LegalPhone);
                        $(d + 'tbLegalWebsite').val(obj.LegalWebsite);
                        $(d + 'tbLegalPinCode').val(obj.LegalPincode);
                        $(d + 'ddLegallLocation').val(obj.LegalLocationId);
                        $(d + 'ddlLegalCountry').val(obj.LegalCountryId ===null ? obj.LegalCountryId: obj.LegalCountryId.trim());
                        $(d + 'ddlLegalState').val(obj.LegalStateId ===null ? obj.LegalStateId: obj.LegalStateId.trim());
                        $(d + 'tbLegalCity').val(obj.LegalCityId);
                        $(d + 'tbFiscalYear').val(obj.FiscalYearMonth);
                        $(d + 'tbTaxMonth').val(obj.TaxMonth);
                        $(d + 'tbYear').val(obj.Year);
                        $(d + 'tbDefaultCurrency').val(obj.DefaultCurrency);
                        $(d + 'tbFEIN').val(obj.Fein);
                        $(d + 'tbNoofYear').val(obj.NoofYear);
                        $(d + 'tbSSN').val(obj.Ssn); 
                        $(d + 'tbPayrollMonthlyStartDate').datepicker('setDate', CompanyModule.setDate(obj.PayrollStartDate));
                        $(d + 'tbStartDate').datepicker('setDate', CompanyModule.setDate(obj.StartDate));
                        $(d + 'tbEndDate').datepicker('setDate', CompanyModule.setDate(obj.Enddate));
                        $(d + 'tbEPF').val(obj.EPF);
                        $(d + 'tbESIC').val(obj.ESIC);
                        $(d + 'tbPAN').val(obj.PAN);
                        $(d + 'tbTAN').val(obj.TAN);
                        $(d + 'tbPremisesCode').val(obj.PREMISES);
                        $(d + 'tbGST').val(obj.GST);
                        $(d + 'tbServiceTax').val(obj.SERVICETAX);

                        hideModal();
                    },
                    error: function (request, message, error) {
                        hideModal();
                    }
                });
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

            return {
                saveCompany: saveCompany,
                getRowVal: getRowVal,
                RowDelete: RowDelete,
                loadCompany: loadCompany,
                clearControl: clearControl,
                showNotification: showNotification,
                setDate: setDate
            };
        })();

    </script>

</asp:Content>
