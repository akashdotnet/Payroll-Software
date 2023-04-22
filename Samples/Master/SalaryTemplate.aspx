<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryTemplate.aspx.cs" 
    Inherits="Samples.Master.SalaryTemplate"  MasterPageFile="~/Main.Master" %>

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
    <%--Css--%>
    <style>
        #SalaryTemplateInfo {
            background-color: white !important;
        }

        .add-error {
            box-sizing: border-box;
            border: 2px solid red;
            border-radius: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
    <form method="post" id="SalaryTemplateInfo" enctype="multipart/form-data" runat="server">
        <div class="tab-pane " id="custom-content-above-profile" role="tabpanel" aria-labelledby="custom-content-above-profile-tab">
                    <div id="pBarSalary" style="display: none; text-align: center">
                        <img src="../Images/gss.gif" />
                    </div>
                    <div class="card-body" id="dvSalaryTemplate">
                    </div>
                    <br />
                    <div class="card">
                        <div class="card-header">
                            <h3 class="card-title">All Template</h3>
                        </div>
                        <div class="card-body">
                            <div id="templateGrid"></div>
                        </div>
                    </div>

                    <div class="alert alert-danger alert-dismissible fade in" style="display: none" id="dvTemplateNoRecords">
                        <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
                        <strong>No Records</strong>
                    </div>
                </div>
        </form>
    </asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptsSection" runat="server">
    <link href="../css/bootstrap-datepicker.css" rel="stylesheet" />
    <script src="../js/bootstrap-datepicker.js"></script>
    <script src="../AdminLTE-3.0.1/plugins/sweetalert2/sweetalert2.min.js"></script>
    <script src="../AdminLTE-3.0.1/plugins/toastr/toastr.min.js"></script>
    <script src="../AdminLTE-3.0.1/plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="../AdminLTE-3.0.1/plugins/datatables/jquery.dataTables.js"></script>
    <script src="../AdminLTE-3.0.1/datatables-bs4/js/dataTables.bootstrap4.js"></script>
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

        //Salary template module
        var InitEmpSalaryTemplate = (function () {

            'use strict';
            var isTemplateReq = false;

            var initSalaryTemplate = function () {
                debugger
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
                    + 'Template Name </div><div class="col-md-6"><input id="tbSalaryTemplateName"                                 '
                    + 'placeholder="Template Name" class="form-control form-control-sm" id="tbSalaryTemplate"/> '
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
                    $(document).scrollTop($("#SalaryTemplateInfo").offset().top);

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
                            $("#myModal").modal('hide');
                        },
                        error: function (request, message, error) {
                            $("#myModal").modal('hide');
                            InitEmpSalaryTemplate.initSalaryTemplate();
                            EmployeeModule.clearControl();
                            CommonModule.showNotification(message, error, 'danger');

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
            InitEmpSalaryTemplate.initSalaryTemplate();
            Toast = Swal.mixin({
                toast: true,
                position: 'top-end',
                showConfirmButton: false,
                timer: 3000
            });
        });
    </script>
</asp:Content>
