<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeAttendance.aspx.cs"
    Inherits="Samples.transaction.TimeAttendance" MasterPageFile="~/Main.Master" %>

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
        .tmatt {
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
    <form method="post" id="TimeAttendance" enctype="multipart/form-data" runat="server">
        <section class="content card-body tmatt">
            <ul class="nav nav-tabs" id="custom-content-above-tab1" role="tablist">
                <li class="nav-item">
                    <a class="nav-link active" id="attendance-entry-tab" data-toggle="pill"
                        href="#attendance-entry" role="tab" aria-controls="attendance-entry"
                        aria-selected="true" onclick="AttendanceModule.loadAttendanceModule(1)">Attendance entry</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="monthly-attendance-process-tab" data-toggle="pill"
                        href="#monthly-attendance-process" role="tab" aria-controls="monthly-attendance-process"
                        aria-selected="false" onclick="AttendanceModule.loadMonthlyAttendanceModule()">Monthly Attendance process</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="attendance-approval-tab" data-toggle="pill"
                        href="#attendance-approval" role="tab" aria-controls="attendance-approval"
                        aria-selected="false" onclick="AttendanceModule.loadMonthlyAttendanceModule()">Attendance approval</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="payroll-process-tab" data-toggle="pill"
                        href="#payroll-process" role="tab" aria-controls="payroll-process"
                        aria-selected="false" onclick="AttendanceModule.loadMonthlyAttendanceModule()">Payroll process</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="attendance-report-tab" data-toggle="pill"
                        href="#attendance-report" role="tab" aria-controls="attendance-report"
                        aria-selected="false" onclick="AttendanceModule.loadAttendanceModule(2)">Attendance report</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" id="inout-entry-tab" data-toggle="pill"
                        href="#inout-entry" role="tab" aria-controls="inout-entry"
                        aria-selected="false" onclick="AttendanceModule.loadAttendanceModule(3)">In/Out Entry</a>
                </li>
            </ul>
            <div class="tab-content" id="custom-content-above-tab1Content">
                <div class="tab-pane fade show active" id="attendance-entry" role="tabpanel" aria-labelledby="custom-content-above-messages-tab">
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            Employee
                                    <asp:DropDownList ID="DropDownList29" runat="server" Width="100%" CssClass="ddlEmployee form-control custom-select">
                                    </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Department
                                    <asp:DropDownList ID="DropDownList30" runat="server" Width="100%" CssClass="ddlAttDepartment form-control custom-select">
                                    </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Location
                                    <asp:DropDownList ID="DropDownList31" runat="server" Width="100%" CssClass="ddlAttLocation form-control custom-select ">
                                    </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Attendance Start Date
                                    <input id="Text1" class="form-control form-control-sm dPicker tbAttStartDate" runat="server" placeholder="Start date" value="07/01/2016">
                        </div>
                        <div class="col-md-2">
                            Attendance End Date
                                    <input id="Text2" class="form-control form-control-sm dPicker tbAttEndDate" runat="server" placeholder="End date" value="07/29/2016">
                        </div>
                        <div class="col-md-2">
                            <br />
                            <button type="button" class="btn btn-block btn-outline-info btn-sm" style="float: left" onclick="AttendanceModule.onAttendanceSch(true)"><i class="fa fa-search"></i>Search</button>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-12" style="overflow-x: auto;">
                            <br />
                            <table id="tableAttendanceentry" class="table table-bordered table-hover dataTable">
                            </table>
                        </div>
                    </div>
                    <div class="row hide divAttendanceEntry">
                        <div class="col-md-10">
                            <br />

                        </div>
                        <div class="col-md-2">
                            <br />
                            <button type="button" class="btn btn-block btn-outline-info btn-sm" onclick="AttendanceModule.saveBulkAttendance()">Save Attendance </button>
                        </div>
                    </div>

                </div>
                <div class="tab-pane fade" id="monthly-attendance-process" role="tabpanel" aria-labelledby="custom-content-above-messages-tab">
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            Employee  
              <asp:DropDownList ID="DropDownList1" runat="server" Width="100%" class=" ddlEmployee form-control custom-select">
              </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Employee Type 
                <asp:DropDownList ID="DropDownList2" runat="server" Width="100%" class="ddlEmployeeType form-control custom-select">
                </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Designation
           <asp:DropDownList ID="DropDownList3" runat="server" Width="100%" class="ddlDesignation form-control custom-select">
           </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Location
                                                      <asp:DropDownList ID="DropDownList4" runat="server" Width="100%" class="ddlLocation form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Department
                                                      <asp:DropDownList ID="DropDownList5" runat="server" Width="100%" class=" ddlDepartment form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Project  
                <asp:DropDownList ID="DropDownList6" runat="server" Width="100%" class=" ddlProject form-control custom-select">
                </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Year  
              <asp:DropDownList ID="ddlYear" runat="server" Width="100%" class="ddlYear form-control custom-select">
              </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Month
                <asp:DropDownList ID="ddlMonth" runat="server" Width="100%" class="ddlMonth form-control custom-select">
                </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Shift
           <asp:DropDownList ID="DropDownList9" runat="server" Width="100%" class="ddlShift form-control custom-select">
           </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Division
                                                      <asp:DropDownList ID="DropDownList10" runat="server" Width="100%" class="ddlDivision form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <br />
                            <button type="button" class="btn btn-block btn-outline-info btn-sm" style="float: left" onclick="AttendanceModule.getMonthlyAttendance(0)"><i class="fa fa-search"></i>Search</button>
                        </div>
                    </div>
                    <br />
                    <div class="templateGrid1"></div>
                    <br />
                    <div class="dvMonthlyAttendance row">
                        <div class="col-md-11">
                        </div>
                        <div class="col-md-1 text-right">
                            <button type="button" class="btn btn-block btn-outline-info btn-sm" onclick="AttendanceModule.saveMonthlyAttendance()">Approve </button>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="attendance-approval" role="tabpanel" aria-labelledby="custom-content-above-messages-tab">
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            Employee  
              <asp:DropDownList ID="DropDownList11" runat="server" Width="100%" class=" ddlEmployee form-control custom-select">
              </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Employee Type 
                <asp:DropDownList ID="DropDownList12" runat="server" Width="100%" class="ddlEmployeeType form-control custom-select">
                </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Designation
           <asp:DropDownList ID="DropDownList13" runat="server" Width="100%" class="ddlDesignation form-control custom-select">
           </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Location
                                                      <asp:DropDownList ID="DropDownList14" runat="server" Width="100%" class="ddlLocation form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Department
                                                      <asp:DropDownList ID="DropDownList15" runat="server" Width="100%" class=" ddlDepartment form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Project  
                <asp:DropDownList ID="DropDownList16" runat="server" Width="100%" class=" ddlProject form-control form-control custom-select">
                </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Year  
              <asp:DropDownList ID="DropDownList17" runat="server" Width="100%" class="ddlYear form-control form-control custom-select">
              </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Month
                <asp:DropDownList ID="DropDownList18" runat="server" Width="100%" class="ddlMonth form-control form-control custom-select">
                </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Shift
           <asp:DropDownList ID="DropDownList19" runat="server" Width="100%" class="ddlShift form-control form-control custom-select">
           </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Division
                                                      <asp:DropDownList ID="DropDownList20" runat="server" Width="100%" class="ddlDivision form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <br />
                            <button type="button" class="btn btn-block btn-outline-info btn-sm" style="float: left" onclick="AttendanceModule.getAttndanceApproval(0)"><i class="fa fa-search"></i>Search</button>
                        </div>
                    </div>
                    <br />
                    <div class="dvAttApprovaltemplateGrid1"></div>
                    <br />
                    <div class="row dvAttApproval ">
                        <div class="col-md-10">
                        </div>
                        <div class="col-md-1">
                            <button type="button" class="btn btn-block btn-outline-info btn-sm">Approve </button>

                        </div>
                        <div class="col-md-1 text-right">
                            <button type="button" class="btn btn-block btn-outline-secondary btn-sm">Reject </button>
                        </div>
                    </div>
                    <div class=" ">
                    </div>
                </div>
                <div class="tab-pane fade" id="payroll-process" role="tabpanel" aria-labelledby="custom-content-above-messages-tab">
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            Employee  
              <asp:DropDownList ID="DropDownList7" runat="server" Width="100%" class=" ddlEmployee form-control custom-select">
              </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Employee Type 
                <asp:DropDownList ID="DropDownList8" runat="server" Width="100%" class="ddlEmployeeType form-control custom-select">
                </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Designation
           <asp:DropDownList ID="DropDownList21" runat="server" Width="100%" class="ddlDesignation form-control custom-select">
           </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Location
                                                      <asp:DropDownList ID="DropDownList22" runat="server" Width="100%" class="ddlLocation form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Department
                                                      <asp:DropDownList ID="DropDownList23" runat="server" Width="100%" class=" ddlDepartment form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Project  
                <asp:DropDownList ID="DropDownList24" runat="server" Width="100%" class=" ddlProject form-control custom-select">
                </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Year  
              <asp:DropDownList ID="DropDownList25" runat="server" Width="100%" class="ddlYear form-control custom-select">
              </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Month
                <asp:DropDownList ID="DropDownList26" runat="server" Width="100%" class="ddlMonth form-control custom-select">
                </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Shift
           <asp:DropDownList ID="DropDownList27" runat="server" Width="100%" class="ddlShift form-control custom-select">
           </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Division
                                                      <asp:DropDownList ID="DropDownList28" runat="server" Width="100%" class="ddlDivision form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            <br />
                            <button type="button" class="btn btn-block btn-outline-info btn-sm" style="float: left"><i class="fa fa-search"></i>Search</button>
                        </div>
                    </div>
                    <br />
                    <div class="row">
                        <div class="templateGrid1"></div>
                    </div>
                    <div class="row dvAttApproval">

                        <button type="button" class="btn btn-block btn-outline-info btn-sm">Genrate Payroll </button>

                        <button type="button" class="btn btn-block btn-outline-secondary btn-sm">Reject Payroll </button>


                    </div>
                </div>
                <div class="tab-pane fade" id="attendance-report" role="tabpanel" aria-labelledby="custom-content-above-messages-tab">
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            Employee
                                    <asp:DropDownList ID="ddlEmployee" runat="server" Width="100%" CssClass="ddlEmployee form-control custom-select">
                                    </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Department
                                    <asp:DropDownList ID="ddlAttDepartment" runat="server" Width="100%" CssClass="ddlAttDepartment form-control custom-select">
                                    </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Location
                                    <asp:DropDownList ID="ddlAttLocation" runat="server" Width="100%" CssClass="ddlAttLocation form-control custom-select">
                                    </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Attendance Start Date
                                    <input id="tbAttStartDate" class="form-control form-control-sm dPicker tbAttStartDate" runat="server" placeholder="Start date" value="07/01/2016">
                        </div>
                        <div class="col-md-2">
                            Attendance End Date
                                    <input id="tbAttEndDate" class="form-control form-control-sm dPicker tbAttEndDate" runat="server" placeholder="End date" value="07/29/2016">
                        </div>
                        <div class="col-md-2">
                            <br />
                            <button type="button" class="btn btn-block btn-outline-info btn-sm" style="float: left" onclick="AttendanceModule.onAttendanceSch(false)"><i class="fa fa-search"></i>Search</button>
                        </div>

                    </div>
                    <div class="row">
                        <table class="table table-bordered table-hover dataTable" cellspacing="0">
                            <tr>
                                <td style="width: 20px; height: 20px; background-color: #00a65a">
                                    <center><span style="color:white;text-align:center" ><b>Present</b></span>  </center>
                                </td>
                                <td style="width: 20px; height: 20px; background-color: red">
                                    <center><span style="color:white;text-align:center" class="text-center"><b>Absent</b></span> </center>

                                </td>
                                <td style="width: 20px; height: 20px; background-color: orange">
                                    <center> <span style="color:white;text-align:center" class="text-center"><b>Leave</b></span></center>
                                </td>
                                <td style="width: 20px; height: 20px; background-color: lightgrey">
                                    <center> <span style="color:white;text-align:center"><b>N/A</b></span> </center>
                                </td>
                            </tr>
                        </table>

                    </div>
                    <div class="row">
                        <div class="col-md-12 table-responsive" style="overflow-x: auto;">
                            <br />
                            <table id="tableAttendance" class="table table-bordered table-hover dataTable" cellspacing="0">
                            </table>
                        </div>
                    </div>
                </div>
                <div class="tab-pane fade" id="inout-entry" role="tabpanel" aria-labelledby="custom-content-above-messages-tab">
                    <br />
                    <div class="row">
                        <div class="col-md-2">
                            Employee  
              <asp:DropDownList ID="DropDownList32" runat="server" Width="100%" class=" ddlEmployee form-control custom-select">
              </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Employee Type 
                <asp:DropDownList ID="DropDownList39" runat="server" Width="100%" class="ddlEmployeeType form-control custom-select">
                </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Designation
           <asp:DropDownList ID="DropDownList40" runat="server" Width="100%" class="ddlDesignation form-control custom-select">
           </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Location
                                                      <asp:DropDownList ID="DropDownList43" runat="server" Width="100%" class="ddlLocation form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Department
                                                      <asp:DropDownList ID="DropDownList44" runat="server" Width="100%" class=" ddlDepartment form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Project  
                <asp:DropDownList ID="DropDownList45" runat="server" Width="100%" class=" ddlProject form-control custom-select">
                </asp:DropDownList>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            Year  
              <asp:DropDownList ID="DropDownList46" runat="server" Width="100%" class="ddlYear form-control custom-select">
              </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Month
                <asp:DropDownList ID="DropDownList47" runat="server" Width="100%" class="ddlMonth form-control custom-select">
                </asp:DropDownList>

                        </div>
                        <div class="col-md-2">
                            Shift
           <asp:DropDownList ID="DropDownList48" runat="server" Width="100%" class="ddlShift form-control custom-select">
           </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Division
                                                      <asp:DropDownList ID="DropDownList49" runat="server" Width="100%" class="ddlDivision form-control custom-select">
                                                      </asp:DropDownList>
                        </div>
                        <div class="col-md-2">
                            Attendance
                                    <input id="Text3" class="form-control form-control-sm dPicker tbAttndanceDate" runat="server" placeholder="Start date">
                        </div>

                        <div class="col-md-2">
                            <br />
                            <button type="button" class="btn btn-block btn-outline-info btn-sm" style="float: left" onclick="AttendanceModule.getMonthlyAttendance(2)"><i class="fa fa-search"></i>Search</button>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12">
                            <div class="tableDAtt"></div>
                        </div>
                    </div>
                    <div class="row dvMonthlyAttendance">
                        <div class="col-md-11">
                        </div>
                        <div class="col-md-1">
                            <button type="button" class="btn btn-block btn-outline-info btn-sm" onclick="AttendanceModule.saveDailyAttendance()">Approve </button>
                        </div>
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
        var AttendanceModule = (function () {

            var ddlMonth = $(".ddlMonth");
            var ddlYear = $(".ddlYear");
            var loadMonthlyAttendanceModule = function () {
                $('.templateGrid1').html('');
                $(".ddlMonth").removeClass("add-error");
                $(".ddlYear").removeClass("add-error");
                $(".dvMonthlyAttendance").hide();
                $(".dvAttApproval").hide();
                showModal();;
                CommonModule.LoadMasters();
                CommonModule.bindMasterDropDownList(ddlLocation, '010', MASTERLIST);
                CommonModule.bindMasterDropDownList(ddlDepartment, '006', MASTERLIST);
                CommonModule.bindEmployeeDropDownList(ddlEmployee, MASTERLIST.EmployeeList);
                CommonModule.bindMasterDropDownList(ddlProject, '025', MASTERLIST);
                CommonModule.bindMasterDropDownList(ddlEmployeeType, '014', MASTERLIST);
                CommonModule.bindMasterDropDownList(ddlDesignation, '007', MASTERLIST);
                CommonModule.bindMasterDropDownList(ddlDivision, '015', MASTERLIST);
                CommonModule.bindMasterDropDownList(ddlShift, '012', MASTERLIST);

                var monthList = [{ 'Value': '1', 'Text': 'January' }, { 'Value': '2', 'Text': 'February' }, { 'Value': '3', 'Text': 'March' },
                { 'Value': '4', 'Text': 'April' }, { 'Value': '5', 'Text': 'May' }, { 'Value': '6', 'Text': 'June' },
                { 'Value': '7', 'Text': 'July' }, { 'Value': '8', 'Text': 'August' }, { 'Value': '9', 'Text': 'September' },
                { 'Value': '10', 'Text': 'October' }, { 'Value': '11', 'Text': 'November' }, { 'Value': '12', 'Text': 'December' }];

                var yearList = [{ 'Value': '1', 'Text': '2018' }, { 'Value': '2', 'Text': '2019' }, { 'Value': '3', 'Text': '2020' },
                { 'Value': '4', 'Text': '2021' }, { 'Value': '5', 'Text': '2022' }, { 'Value': '6', 'Text': '2023' },
                { 'Value': '7', 'Text': '2024' }, { 'Value': '8', 'Text': '2025' }, { 'Value': '9', 'Text': '2026' },
                { 'Value': '10', 'Text': '2027' }, { 'Value': '11', 'Text': '2028' }, { 'Value': '12', 'Text': '2029' }];
                ddlMonth.html('');
                ddlMonth.append($("<option></option>").val('0').html('--Select--'));
                $.grep(monthList, function (n, i) {
                    ddlMonth.append($("<option></option>").val(n.value).html(n.Text));
                });
                ddlYear.html('');
                ddlYear.append($("<option></option>").val('0').html('--Select--'));
                $.grep(yearList, function (n, i) {
                    ddlYear.append($("<option></option>").val(n.value).html(n.Text));
                });
                hideModal();
            };

            var loadAttendanceModule = function () {
                showModal();
                $('.divAttendanceEntry').hide();
                $(".ddlMonth").removeClass("add-error");
                $(".ddlYear").removeClass("add-error");
                $(".dvMonthlyAttendance").hide();
                $(".dvAttApproval").hide();
                var ddlAttLocation = $(".ddlAttLocation");
                var ddlAttDepartment = $(".ddlAttDepartment");
                var ddlEmployee = $(".ddlEmployee");

                CommonModule.LoadMasters();
                // CommonModule.LoadEmployee();
                CommonModule.bindMasterDropDownList(ddlAttLocation, '010', MASTERLIST);
                CommonModule.bindMasterDropDownList(ddlAttDepartment, '006', MASTERLIST);
                CommonModule.bindEmployeeDropDownList(ddlEmployee, MASTERLIST.EmployeeList)
                hideModal();
            };

            var onAttendanceSch = function (isEntry) {

                isEntry ? $(".divAttendanceEntry").show() : $(".divAttendanceEntry").hide();
                showModal();
                var employeeId = parseInt($(".ddlEmployee option:selected").val());
                var startDate = $(".tbAttStartDate").val().split("/").join("-");
                var endDate = $(".tbAttEndDate").val().split("/").join("-");
                var sMonthly = false;
                var lstData = [employeeId, startDate, endDate, sMonthly];
                var url = baseUrl + 'Attendance';
                $.ajax({
                    url: url,
                    type: 'POST',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(lstData),
                    success: function (data) {
                        var table = '';
                        var htmlTable = '';
                        if (isEntry) {
                            table = $('#tableAttendanceentry');
                            htmlTable = '#tableAttendanceentry'
                        }
                        else {
                            table = $('#tableAttendance');
                            htmlTable = '#tableAttendance'
                        }
                        table.html('');
                        var cols = _headers(data.List, htmlTable);
                        // Traversing the JSON data 
                        for (var i = 0; i < data.List.length; i++) {
                            var row = $('<tr/>');
                            for (var colIndex = 0; colIndex < cols.length; colIndex++) {
                                var text = '';
                                var val = data.List[i][cols[colIndex]];
                                if (data.List[i][cols[0]] === data.List[i][cols[colIndex]]) {
                                    row.append($('<td/>').html(data.List[i][cols[colIndex]]));
                                }
                                else if (data.List[i][cols[1]] === data.List[i][cols[colIndex]]) {
                                    text = data.List[i][cols[colIndex]];
                                    row.append($('<td />').html(text));
                                }
                                else {
                                    if (isEntry) {
                                        var isDis = val === 'P' || val === 'A' || val === 'L' ? 'disabled' : '';
                                        text += '<select style="width:50px" id="ddlPresentStatus' + colIndex + '_' + i + '" att-date="' + cols[colIndex] + '" attr-emp-id="' + data.List[i][cols[1]] + '" ' + isDis + '><option value="P">P</option><option value="A">A</option><option value="L">L</option><option value="NA">N/A</option></select><br/>'
                                        row.append($('<td />').html(text));
                                        row.find('select#ddlPresentStatus' + colIndex + '_' + i + ' option[value=' + val + ']').attr('selected', 'selected');
                                    }
                                    else {
                                        if (val === 'P')
                                            text += '<div style="width:20px;height:20px;background-color:#00a65a" />'
                                        else if (val === 'L')
                                            text += '<div style="width:20px;height:20px;background-color:red" />'
                                        else if (val === 'A')
                                            text += '<div style="width:20px;height:20px;background-color:orange" />'
                                        else
                                            text += '<div style="width:20px;height:20px;background-color:lightgrey" />'
                                        row.append($('<td style="width:50px"/>').html(text));
                                    }
                                }
                            }
                            table.append(row);
                        }
                        hideModal();
                    },
                    error: function (request, message, error) {
                        hideModal();
                    }
                })
            };

            var _uuidv4 = function () {
                return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
                    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
                    return v.toString(16);
                });
            }

            var _headers = function (list, selector) {
                var columns = [];
                var header = $('<tr/>');
                for (var i = 0; i < list.length; i++) {
                    var row = list[i];
                    for (var k in row) {
                        if ($.inArray(k, columns) == -1) {
                            columns.push(k);
                            // Creating the header 
                            if (k === 'Name')
                                header.append($('<th style="width:180px"/>').html(k));
                            else if (k === 'EmployeeId')
                                header.append($('<th style="width:180px"/>').html(k));
                            else
                                header.append($('<th style="width:20px"/>').html(k.split('-')[2]));
                        }
                    }
                }

                // Appending the header to the table 
                $(selector).append(header);
                return columns;
            }

            var getMonthlyAttendance = function (entryLock) {
                $(".dvMonthlyAttendance").hide();
                $(".dvAttApproval").hide();
                $(".ddlMonth").removeClass("add-error");
                $(".ddlYear").removeClass("add-error");
                var employeeId = parseInt($(".ddlEmployee option:selected").val()) === 0 ? null : parseInt($(".ddlEmployee option:selected").val());
                var employeeTypeId = $(".ddlEmployeeType option:selected").val() === '0' ? null : $(".ddlEmployeeType option:selected").val();
                var designationId = $(".ddlDesignation option:selected").val() === '0' ? null : $(".ddlDesignation option:selected").val();
                var locationId = $(".ddlLocation option:selected").val() === '0' ? null : $(".ddlLocation option:selected").val();
                var departmentId = $(".ddlDepartment option:selected").val() === '0' ? null : $(".ddlDepartment option:selected").val();
                var projectId = $(".ddlProject option:selected").val() === '0' ? null : $(".ddlProject option:selected").val();
                var shiftId = $(".ddlShift option:selected").val() === '0' ? null : $(".ddlShift option:selected").val();
                var divisionId = $(".ddlDivision option:selected").val() === '0' ? null : $(".ddlDivision option:selected").val();
                var month = $(".ddlMonth option:selected")[0].index;
                var year = $(".ddlYear option:selected")[0].index;
                var tbAttndanceDate = $(".tbAttndanceDate").val();

                var isReq = false;
                if (entryLock === 2) {
                    if (tbAttndanceDate === '') {
                        $(".tbAttndanceDate").addClass("add-error");
                        isReq = true;
                    }
                }
                else {
                    if (month === 0) {
                        $(".ddlMonth").addClass("add-error");
                        isReq = true;
                    }
                    else {
                        $(".ddlMonth").removeClass("add-error");
                    }
                    if (year === 0) {
                        $(".ddlYear").addClass("add-error");
                        isReq = true;
                    }
                    else {
                        $(".ddlYear").removeClass("add-error");
                    }
                }
                if (isReq) {
                    return false;
                }
                showModal();;
                var sMonthly = true;
                var tablMonthlyAttendance = entryLock === 0 ? 'tablMonthlyAttendance' : 'tableDailyAttendance';
                var lstData = [employeeId, employeeTypeId, designationId, sMonthly, locationId, departmentId,
                projectId, shiftId, divisionId, month, year, entryLock, tablMonthlyAttendance, tbAttndanceDate];
                var url = baseUrl + 'Attendance';
                $.ajax({
                    url: url,
                    type: 'POST',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(lstData),
                    success: function (data) {
                        $('.templateGrid1').html('');
                        $('.tableDAtt').html('');
                        var tablMonthlyAttendance = entryLock === 0 ? 'tablMonthlyAttendance' : 'tableDailyAttendance';
                        if (data.Count > 0) {
                            $(".templateGrid1").append(data.Html);
                            if (tablMonthlyAttendance === 'tableDailyAttendance') {
                                $(".tableDAtt").append(data.Html);
                            }
                            $("#" + tablMonthlyAttendance).DataTable();
                            $(".dvTemplateNoRecords").hide();
                            $(".dvMonthlyAttendance").show();
                            $(".dvAttApproval").show();
                        }
                        else {
                            $(".dvMonthlyAttendance").hide();
                            $(".dvAttApproval").hide();
                            $(".templateGrid1").html("");
                            $('.tableDAtt').html('');
                            $(".dvTemplateNoRecords").show();
                        }
                        //  _drawChart(data.dataList);
                        hideModal();
                    },
                    error: function (request, message, error) {
                        hideModal();
                    }
                })
            };

            var _drawChart = function (dataValues) {
                var data = new google.visualization.DataTable();
                data.addColumn('string', 'Column Name');
                data.addColumn('number', 'Column Value');
                for (var i = 0; i < dataValues.length; i++) {
                    data.arrRow(dataValues[i].Gender, dataValues[i].Total);
                }
                new google.visualization.PieChart(document.getElementById('myChartDiv'));
                draw(data, { title: 'Gender Chart' });
            };

            var saveMonthlyAttendance = function () {
                showModal();;
                var arrPropIdPos = new Array();
                // var templateHtml = $("#tablMonthlyAttendance").find("tbody tr");
                var EmpAttendanceEntry = new Object();
                var attendanceEntry = [];
                $("#tablMonthlyAttendance").find("tbody tr").each(function (i, v) {
                    if ($($(v).find('input')[0]).attr('ischecked') !== undefined && $($(v).find('input')[0]).attr('ischecked') === 'true') {
                        var empAttendanceEntry = {};
                        empAttendanceEntry.emp_attendance_entry_id = parseInt($(v).find('td:eq(1)').html());
                        empAttendanceEntry.paytype_id = 0;
                        empAttendanceEntry.employee_id = parseInt($(v).find('td:eq(2)').html());
                        empAttendanceEntry.pay_period = $(".ddlMonth option:selected")[0].index;
                        empAttendanceEntry.pay_year = $(".ddlYear option:selected")[0].index;
                        empAttendanceEntry.work_unit = '';
                        empAttendanceEntry.days_worked = parseInt($(v).find('td:eq(13)').html());
                        empAttendanceEntry.days_overtime = 0;
                        empAttendanceEntry.notes = '';
                        empAttendanceEntry.attendance_method = 0;
                        empAttendanceEntry.local_day = 0;
                        empAttendanceEntry.non_local_day = 0;
                        attendanceEntry.push(empAttendanceEntry);
                    }
                });
                EmpAttendanceEntry = attendanceEntry;
                $.ajax({
                    url: baseUrl + 'MonthlyAttendance',
                    type: 'POST',
                    contentType:
                       "application/json;charset=utf-8",
                    data: JSON.stringify(EmpAttendanceEntry),
                    success: function (response) {

                        AttendanceModule.getMonthlyAttendance(0);
                        CommonModule.showNotification("Data Saved", response.Html, 'success');
                        hideModal();
                    },
                    error: function (request, message, error) {
                        hideModal();
                    }
                });

            };

            var ckbCheckAll = function (e) {
                $("input.chkAtt").prop('checked', $(e).prop('checked'));
                $("input.chkAtt").attr('isChecked', true);
            };

            var checkBoxClass = function (e) {
                if (!$(e).prop("checked")) {
                    $("input.chkAttAll").prop("checked", false);
                    $(e).attr('isChecked', false);
                }
                else {
                    $(e).attr('isChecked', true);

                }
            };

            var getAttndanceApproval = function (entryLock) {

                $(".dvAttApproval").hide();
                var employeeId = parseInt($("#FeaturedContent_DropDownList11 option:selected").val()) === 0 ? null : parseInt($("#FeaturedContent_DropDownList11 option:selected").val());
                var employeeTypeId = $("#FeaturedContent_DropDownList12 option:selected").val() === '0' ? null : $("#FeaturedContent_DropDownList12 option:selected").val();
                var designationId = $("#FeaturedContent_DropDownList13 option:selected").val() === '0' ? null : $("#FeaturedContent_DropDownList13 option:selected").val();
                var locationId = $("#FeaturedContent_DropDownList14 option:selected").val() === '0' ? null : $("#FeaturedContent_DropDownList14 option:selected").val();
                var departmentId = $("#FeaturedContent_DropDownList15 option:selected").val() === '0' ? null : $("#FeaturedContent_DropDownList15 option:selected").val();
                var projectId = $("#FeaturedContent_DropDownList16 option:selected").val() === '0' ? null : $("#FeaturedContent_DropDownList16 option:selected").val();
                var shiftId = $("#FeaturedContent_DropDownList19 option:selected").val() === '0' ? null : $("#FeaturedContent_DropDownList19 option:selected").val();
                var divisionId = $("#FeaturedContent_DropDownList20 option:selected").val() === '0' ? null : $("#FeaturedContent_DropDownList20 option:selected").val();
                var month = $("#FeaturedContent_DropDownList18 option:selected")[0].index;
                var year = $("#FeaturedContent_DropDownList17 option:selected")[0].index;
                var isReq = false;

                if (month === 0) {
                    $("#FeaturedContent_DropDownList18").addClass("add-error");
                    isReq = true;
                }
                else {
                    $("#FeaturedContent_DropDownList18").removeClass("add-error");
                }
                if (year === 0) {
                    $("#FeaturedContent_DropDownList17").addClass("add-error");
                    isReq = true;
                }
                else {
                    $("#FeaturedContent_DropDownList17").removeClass("add-error");
                }

                if (isReq) {
                    return false;
                }
                showModal();;
                var sMonthly = true;
                var tableAttendanceApproval = 'AttendanceApproval';
                var lstData = [employeeId, employeeTypeId, designationId, sMonthly, locationId, departmentId,
                projectId, shiftId, divisionId, month, year, entryLock, tableAttendanceApproval];
                var url = baseUrl + 'Attendance';
                $.ajax({
                    url: url,
                    type: 'POST',
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify(lstData),
                    success: function (data) {
                        $('.dvAttApprovaltemplateGrid1').html('');
                        if (data.Count > 0) {
                            $(".dvAttApprovaltemplateGrid1").append(data.Html);
                            $("#AttendanceApproval").DataTable();
                            $(".dvTemplateNoRecords").hide();
                            $(".dvAttApproval").show();
                        }
                        else {
                            $(".dvAttApproval").hide();
                            $(".dvAttApprovaltemplateGrid1").html("");
                            $(".dvTemplateNoRecords").show();
                        }
                        //_drawChart(data.dataList);
                        hideModal();
                        // $('#tablMonthlyAttendance').DataTable();

                    },
                    error: function (request, message, error) {
                        hideModal();
                    }
                })
            };

            var saveBulkAttendance = function () {
                showModal();
                var Attendance = new Object();
                var attendanceEntryTable = $('#tableAttendanceentry tr');
                var attendanceEntryList = [];
                var attendanceEntryObject = {};
                $.each(attendanceEntryTable, function (i, v) {
                    if (i != 0) {
                        $.each($(v).find('td'), function (j, v1) {
                            if (j >= 2) {
                                var ddlId1 = $(v1).find('select').attr('id');
                                if ($(v1).find('select').attr('disabled') === undefined &&
                                    $("#" + ddlId1 + " option:selected").val() !== 'N/A') {
                                    attendanceEntryObject.EmployeeId = $(v1).find('select').attr('attr-emp-id');
                                    attendanceEntryObject.DATE = $(v1).find('select').attr('att-date');
                                    var ddlId = $(v1).find('select').attr('id');
                                    var val = $("#" + ddlId + " option:selected").val();
                                    attendanceEntryObject.PRESENT_STATUS = val;
                                    attendanceEntryObject.INTIME = null;
                                    attendanceEntryObject.OUTTIME = null;
                                    attendanceEntryList.push(attendanceEntryObject);
                                    attendanceEntryObject = {};
                                }
                            }
                        });
                    }
                });
                Attendance.Attendanceentry = attendanceEntryList;
                Attendance.AttendanceType = 'BulkAttendance';
                $.ajax({
                    url: baseUrl + 'Attendance',
                    type: 'Put',
                    contentType:
                       "application/json;charset=utf-8",
                    data: JSON.stringify(Attendance),
                    success: function (response) {
                        hideModal();
                        AttendanceModule.onAttendanceSch(true);
                        CommonModule.showNotification("Data Saved", response.Html, 'success');
                    },
                    error: function (request, message, error) {
                        hideModal();
                    }
                });
            }

            var saveDailyAttendance = function () {
                debugger
                showModal();
                var Attendance = new Object();
                var attendanceEntryTable = $('div.tableDAtt table#tableDailyAttendance tbody tr.data-row');
                var attendanceEntryList = [];
                var attendanceEntryObject = {};
                $.each(attendanceEntryTable, function (i, v) {
                    var isChecked = $(v).find("td:eq(0)").find('input').attr("ischecked");
                    var isDisabled = $(v).find("td:eq(9)").find('input').attr("disabled");
                    if (isChecked === 'true' && isDisabled === undefined) {
                        attendanceEntryObject.EmployeeId = $(v).find("td:eq(2)").html();
                        attendanceEntryObject.DATE = $('.tbAttndanceDate').val();
                        attendanceEntryObject.PRESENT_STATUS = 'P';
                        attendanceEntryObject.INTIME = $(v).find("td:eq(9)").find('input').val();
                        attendanceEntryObject.OUTTIME = $(v).find("td:eq(10)").find('input').val();
                        attendanceEntryList.push(attendanceEntryObject);
                        attendanceEntryObject = {};
                    }
                });
                Attendance.Attendanceentry = attendanceEntryList;
                Attendance.AttendanceType = 'DailyAttendance';
                $.ajax({
                    url: baseUrl + 'Attendance',
                    type: 'Put',
                    contentType:
                       "application/json;charset=utf-8",
                    data: JSON.stringify(Attendance),
                    success: function (response) {
                        hideModal();
                        AttendanceModule.onAttendanceSch(true);
                        CommonModule.showNotification("Data Saved", response.Html, 'success');
                        AttendanceModule.getMonthlyAttendance(2);
                    },
                    error: function (request, message, error) {
                        hideModal();
                    }
                });
            };

            return {
                onAttendanceSch: onAttendanceSch,
                loadAttendanceModule: loadAttendanceModule,
                loadMonthlyAttendanceModule: loadMonthlyAttendanceModule,
                getMonthlyAttendance: getMonthlyAttendance,
                checkBoxClass: checkBoxClass,
                ckbCheckAll: ckbCheckAll,
                saveMonthlyAttendance: saveMonthlyAttendance,
                getAttndanceApproval: getAttndanceApproval,
                saveBulkAttendance: saveBulkAttendance,
                saveDailyAttendance: saveDailyAttendance
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
            AttendanceModule.loadAttendanceModule();
        });
    </script>


</asp:Content>
