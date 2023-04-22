using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace APIapps.Models
{
    public class CommonUtil
    {




        //

        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };
        private static string EncryptionKey = "!#853g`de";


        public static StringBuilder htmlTableEditMode(DataTable datatable)
        {
            try
            {

                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                html.Append("<thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    html.Append("<th>" + col.Caption + "</th>");

                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        html.Append("<tr class='data-row'  onclick='getRowVal(" + rowId + ")' >");
                        foreach (var cell in row.ItemArray)
                        {
                            html.Append("<td>" + cell.ToString() + "</td>");
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody>");

                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }



        public static StringBuilder htmlTableSelectMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'  style='margin-left:5px;margin-right:5px;'   width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row' onclick='getRowVal(" + i + "," + rowId + ")'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }
        public static StringBuilder htmlTableBirthday(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0' ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {

                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    if (!(string.IsNullOrEmpty(rowData)))
                                    {
                                        txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                        html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                        txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                        html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                    }
                                    else
                                    {

                                        txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                        html.Append("<td>  </td>");
                                        txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                        html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                    }
                                }
                                else
                                {
                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='button'  value='Send Mail'  onclick='SendMail()' class='btn btn-success' style='float:left'><span class='glyphicon glyphicon-envelope'></span> Send Wishes</button></td></tr>");
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        public static StringBuilder htmlTable(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;



                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive' style='margin-left:5px;margin-right:5px;'   width='100%' cellspacing='0'  >");

                html.Append("<thead><tr>");

                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th class='exclude' hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td class='exclude' hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }



        public static StringBuilder htmlTablePFECR(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;



                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive' style='margin-left:5px;margin-right:5px;'   width='100%' cellspacing='0'  >");

                html.Append("<thead><tr>");

                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th class='exclude' hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            if (!(columnHide.Contains(i)))
                            {

                                if (i == 11)
                                {
                                    html.Append("<td style='color:red;'>" + Convert.ToString(cell) + "</td>");
                                }
                                else
                                {
                                    html.Append("<td>" + Convert.ToString(cell) + "</td>");
                                }
                            }
                            else
                            {
                                html.Append("<td class='exclude' hidden='hidden'>" + Convert.ToString(cell) + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlTableImageGet(DataTable datatable)
        {
            try
            {
                byte[] byteImage = null;
                string strbyteImage = null;
                StringBuilder html = new StringBuilder();
                if (datatable.Rows.Count > 0)
                {
                    string strImage = Convert.ToString(datatable.Rows[0][1]);
                    if (!string.IsNullOrEmpty(strImage))
                    {
                        byteImage = (byte[])datatable.Rows[0][1];
                        strbyteImage = Convert.ToBase64String(byteImage);
                    }
                    html.Append("<div><button type='button' id='btnDownloadAttachment' onclick='SubmitAttendanceForm()' title='Download Attachment' class='btn btn-xs btn-success'><span class='glyphicon glyphicon-download-alt'></span>Download</button></div>");
                    html.Append("<div><br /></div>");
                    html.Append("<div><img src='" + "data:image/png;base64," + strbyteImage + "' class = 'img-rounded img-thumbnail'></div>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static StringBuilder htmlTableNotesGet(DataTable datatable, int[] columnHide)
        {
            try
            {
                //byte[] byteImage = null;
                //string strbyteImage = null;
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                html.Append("<table id='data_Table_Notes' class='display table table-striped table-bordered table-hover table-responsive' style='margin-left:5px;margin-right:5px;'   width='100%' cellspacing='0'  >");
                //html.Append("<thead><tr>");
                //foreach (System.Data.DataColumn col in datatable.Columns)
                //{
                //    if (!(columnHide.Contains(i)))
                //    {
                //        html.Append("<th>" + col.Caption + "</th>");
                //    }
                //    else
                //    {
                //        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                //    }
                //    i = i + 1;
                //}
                //html.Append("</tr></thead><tbody>");
                html.Append("<tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            //if (i == 7)
                            //{
                            //    Id = cell.ToString();
                            //    string strImage = Convert.ToString(datatable.Rows[rowId]["AttachedFile"]);
                            //    if (!string.IsNullOrEmpty(strImage))
                            //    {
                            //        byteImage = (byte[])datatable.Rows[rowId]["AttachedFile"];
                            //        strbyteImage = Convert.ToBase64String(byteImage);
                            //    }
                            //    html.Append("<tr class='data-row'><td><img src='" + "data:image/png;base64," + strbyteImage + "' class = 'img-rounded img-thumbnail' style='width:50px; height:50px;'></td>");
                            //}
                            if (i == 7)
                            {
                                Id = cell.ToString();
                                html.Append("<td><button type='button' title='Download Attachment' onclick='SubmitForm(" + Id + ")' name='command' value='Download'  class='btn-outline-secondary'><span class='glyphicon glyphicon-download'></span></button></td>");
                            }
                            if (!(columnHide.Contains(i)) && (i != 7))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public static StringBuilder htmlTableMenuModule(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive' cellspacing='0'  >");

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<tr class='data-row'><td><button type='button' onclick='RowWorkFlowGet(" + Id + ")' class='btn-outline-secondary'><span class='glyphicon glyphicon-sort'></span></button></td>");
                                //html.Append("<tr class='data-row'><td style='width:60px;'><span class='pull-right clickable'><i class='glyphicon glyphicon-chevron-up'></i></span></td>");
                                //<span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                                //<span class="pull-right clickable"><i class="glyphicon glyphicon-chevron-up"></i></span>
                                //html.Append("<tr class='data-row'> <td style='width:60px;' ><button type='button' style='margin-left:1px;margin-right:1px;'  onclick='RowGet(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-ok'></span></button></td>");

                            }
                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //-------------Work flow dyanamic menu generate with Processs hoide/show
        public static StringBuilder htmlTableMenuText(DataTable datatable, int[] columnHide, string MenuText, string RowId)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                string divMenuText = "divMenuText" + RowId;
                string TableId = "TableId" + RowId;
                string pageUrl = "";
                html.Append("<div class='panel panel-default'>");
                html.Append("<div class='panel-heading'>");

                html.Append("<a class='accordion-toggle' data-toggle='collapse' data-parent='#accordion' href='#" + divMenuText + "'>" + MenuText + "</a>");

                html.Append("</div>");

                html.Append("<div class='panel-collapse collapse in' id='" + divMenuText + "'>");

                html.Append("<div class='panel-body' >");


                html.Append("<table id='" + TableId + "' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  >");

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {

                            if (i == 0)
                            {
                                Id = cell.ToString();
                            }
                            if (!(columnHide.Contains(i)))
                            {
                                pageUrl = Convert.ToString(row.ItemArray[11]);
                                html.Append("<td><a href='#'><span onclick='" + pageUrl.Trim() + "'>" + cell.ToString() + "</span></a></td>");
                                //html.Append("<td><a href=" + pageUrl.Trim() + ">" + cell.ToString() + "</a></td>");
                                //html.Append("<td onclick='PutHereMethodName(" + Id + ")'>" + cell.ToString() + "</td>");
                                //<a style="background-color: #34495e; color: #E6F1F3;" href="<%=  Url.Content(pageUrl.Trim()) %>"><%=menuItemName %></a>
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    html.Append("</div></div></div>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringBuilder htmlTableLeftMenu(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                string pageUrl = string.Empty;





                html.Append("<table id='table_menu' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  >");

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {

                            if (i == 0)
                            {
                                Id = cell.ToString();
                            }
                            if (!(columnHide.Contains(i)))
                            {
                                pageUrl = Convert.ToString(row.ItemArray[11]);
                                html.Append("<td><a href='#'><span onclick='" + pageUrl.Trim() + "'>" + cell.ToString() + "</span></a></td>");
                                //html.Append("<td><a href=" + pageUrl.Trim() + ">" + cell.ToString() + "</a></td>");
                                //html.Append("<td onclick='PutHereMethodName(" + Id + ")'>" + cell.ToString() + "</td>");
                                //<a style="background-color: #34495e; color: #E6F1F3;" href="<%=  Url.Content(pageUrl.Trim()) %>"><%=menuItemName %></a>
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");

                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringBuilder htmlTableECEmployeeDetail(DataTable datatable)
        {
            try
            {
                string Id = null;
                string EmployeeName = null;
                StringBuilder html = new StringBuilder();
                int i = 0;
                int j = 0;
                int RowId = 0;




                html.Append("<table id='wfDataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'><thead><tr>");
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                EmployeeName = Convert.ToString(datatable.Rows[j][1]);
                                html.Append("<tr class='data-row'> <td style='width:60px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getEmployeeVal(" + Id + "," + RowId + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");
                            }
                            if (i == 1)
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;

                        }
                        RowId = RowId + 1;
                        j++;
                        html.Append("</tr>");
                    }
                    html.Append("<input type='hidden' id='hfEmployee' name='hfEmployee' value='" + Convert.ToString(datatable.Rows[0][0]) + "'");
                    html.Append("</tbody></table>");

                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }






        public static StringBuilder htmlTableLoanDetails(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive'   width='100%' cellspacing='0'  ><thead><tr><th>Details</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<tr class='data-row'> <td style='width:60px;' ><button type='button' style='margin-left:1px;margin-right:1px;'  onclick='RowGet(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-ok'></span></button></td>");


                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlTableEditModeloanApproval(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive'   width='100%' cellspacing='0'  ><thead><tr><th>Approval</th><th>DisApproval</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<tr class='data-row'> <td style='width:60px;' ><button type='button' title='Approve Record' style='margin-left:1px;margin-right:1px;'  onclick='RowApprove(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-ok'></span></button></td>");
                                html.Append("<td style='width:60px;' ><button type='button' title='DisApprove Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDisApprove(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-ok'></span></button></td>");


                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlNestedTableChildPanel(int[] columnShow, DataTable datatable)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Site Address</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnShow.Contains(i)))
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> ");

                            }
                            if (!(columnShow.Contains(i)))
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        //  childPanelDivId = "childPanelDiv" + rowId;
                        //    html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' ></div></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    //html.Append("<button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnPdf'  class='btn  btn-success enabling cancel' name='command' value='Pdf'>Pdf</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnExcel' class='btn  btn-success enabling cancel' name='command' value='Excel'>Excel</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnWord' class='btn  btn-success enabling cancel' name='command' value='Word'>Word</button> ");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static StringBuilder htmlTableEditMode1(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='col-sm-6 display table table-striped table-bordered table-hover table-responsive table-condensed' width='100%' cellspacing='0'><thead><tr><th  class='exclude' >Site Address</th><th style='white-space:nowrap' class='exclude' >Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> <td style='width:70px; white-space:nowrap' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                                //html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> ");



                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }
        public static StringBuilder htmlTableAttendance(DataTable datatable, int[] columnHide, string pageModule, string jsDataTableId)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<div class='table-responsive'><table id='" + jsDataTableId + "' class='table table-bordered table-hover dataTable'><thead><tr><th style='white-space:nowrap' class='exclude' ><input type='checkbox' id='chkAttAll'  onclick='" + pageModule + ".ckbCheckAll(this)'/></th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr  id='DataRow" + rowId + "' class='data-row dailyAttendanceTableRow'><td style='width:70px; white-space:nowrap' ><input class='chkAtt' onclick='" + pageModule + ".checkBoxClass(this)' type='checkbox' name='check[]' id='" + Id + "'    />");
                            }
                            if (!(columnHide.Contains(i)))
                            {
                                if (i == 8)
                                {
                                    html.Append("<td>");
                                    var disabled = cell.ToString() == "" ? "" : "disabled";
                                    html.Append("<input type = 'text' class='form-control form-control-sm timeclass' value = '" + cell.ToString() + "' " + disabled + ">");
                                    html.Append("</td>");
                                }
                                else if (i == 9)
                                {
                                    html.Append("<td>");
                                    var disabled = cell.ToString() == "" ? "" : "disabled";
                                    html.Append("<input type = 'text' class='form-control form-control-sm timeclass' value = '" + cell.ToString() + "' " + disabled + "> ");
                                    html.Append("</td>");
                                }
                                else if (i == 11)
                                {
                                    if (cell.ToString() != "")
                                    { html.Append("<td> <i class='fa fa-check' style='color:green'></i></td>"); }
                                    else { html.Append("<td> <i class='fa fa-window-close' style='color:red'></i></td>"); }

                                }

                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }
                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table></<div>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlTableAttendance1(DataTable datatable, int[] columnHide, string pageModule, string jsDataTableId)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<div class='table-responsive'><table id='" + jsDataTableId + "' class='table table-bordered table-hover dataTable'><thead><tr><th style='white-space:nowrap' class='exclude' ><input type='checkbox' id='chkAttAll'  onclick='" + pageModule + ".ckbCheckAll(this)'/></th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:70px; white-space:nowrap' ><input class='chkAtt' onclick='" + pageModule + ".checkBoxClass(this)' type='checkbox' name='check[]' id='" + Id + "'  />");
                            }
                            if (!(columnHide.Contains(i)))
                            {
                                if (i == 11)
                                {
                                    html.Append("<td>");
                                    html.Append(" <input type = 'text' class='form-control' value='" + cell.ToString() + "'>");
                                    html.Append("</td>");
                                }
                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }
                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table></<div>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlTableEditMode(DataTable datatable, int[] columnHide, string pageModule, string jsDataTableId)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='" + jsDataTableId + "' class='table table-bordered table-hover dataTable'><thead><tr><th style='white-space:nowrap' class='exclude' >Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:70px; white-space:nowrap' >");
                                if (pageModule == "CompanyModule")
                                {
                                    html.Append("<button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='" + pageModule + ".getRowVal(this)' class='btn-outline-secondary'  data-id=" + Id + "><span class='fa fa-edit'></span></button>");
                                    html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='" + pageModule + ".RowDelete(this)' class='btn-outline-secondary' data-id=" + Id + "><span class='fa fa-trash'></span></button></td>");

                                }
                                else
                                {
                                    html.Append("<button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='" + pageModule + ".getRowVal(" + Id + ")' class='btn-outline-secondary'  data-id=" + Id + "><span class='fa fa-edit'></span></button>");
                                    html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='" + pageModule + ".RowDelete(" + Id + ")' class='btn-outline-secondary' data-id=" + Id + "><span class='fa fa-trash'></span></button></td>");
                                }
                            }

                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }
        public static StringBuilder htmlTableEditModeunAssigned(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='col-sm-6 display table table-striped table-bordered table-hover table-responsive table-condensed' width='100%' cellspacing='0'><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                // html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:70px; white-space:nowrap' ><button title='Edit Record' disabled='disabled' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");
                                //  html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  disabled='disabled' onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                                //html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> ");



                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlTableEditModePIVOT(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='col-sm-6 display table table-striped table-bordered table-hover table-responsive table-condensed' width='100%' cellspacing='0'><thead><tr><th style='white-space:nowrap' class='exclude' >Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:70px; white-space:nowrap' ><button title='Edit Record' disabled='disabled' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  disabled='disabled' onclick='RowDelete(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-trash'></span></button></td>");
                                //html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> ");



                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlTableDisplayMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='col-sm-6 display table table-striped table-bordered table-hover table-responsive table-condensed' width='100%' cellspacing='0'><thead><tr><th  class='exclude' ></th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<tr class='data-row'> <td  class='exclude'  style='width:60px;' >");


                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }


        public static StringBuilder htmlTableEditaccountMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='col-sm-6 display table table-striped table-bordered table-hover table-responsive table-condensed' width='100%' cellspacing='0'><thead><tr><th  class='exclude' >Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("<th> Active </th>");
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();

                                html.Append("<tr class='data-row'> <td  class='exclude'  style='width:60px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");

                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-trash'></span></button></td>");
                            }


                            if (!(columnHide.Contains(i)))
                            {

                                html.Append("<td>" + cell.ToString() + "</td>");


                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }

                        var value = Convert.ToString(datatable.Rows[rowId]["Active"]);
                        if (value == "True")
                        {
                            html.Append("<td><input type='checkbox' disabled='disabled' checked/></td>");

                        }
                        else
                        {
                            html.Append("<td><input type='checkbox' disabled='disabled'/></td>");
                        }

                        html.Append("</tr>");

                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }


        public static StringBuilder htmlTableWithoutEditMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='datahtmlTable' class='display table table-striped table-bordered table-hover table-responsive'   width='100%' cellspacing='0'  ><thead><tr><th></th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'><td></td>");
                        foreach (var cell in row.ItemArray)
                        {
                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }




        public static StringBuilder htmlTableEditModeHideDelete(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<tr class='data-row'> <td style='width:60px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button></td>");

                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }


        public static StringBuilder htmlTableEmployeeCenter(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<tr class='data-row'> <td style='width:60px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowValData(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button></td>");

                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }






        public static StringBuilder htmlTableEditModeLeaveUpdate(DataTable datatable, int[] columnHide)
        {
            try
            {
                string leavestatus = null;
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive'   width='100%' cellspacing='0'  ><thead><tr><th  class='exclude' >Edit/Cancel</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {

                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        leavestatus = Convert.ToString(datatable.Rows[rowId][12]);

                        foreach (var cell in row.ItemArray)
                        {

                            if (i == 0)
                            {
                                Id = cell.ToString();
                                if (!(leavestatus == "Appoved" || leavestatus == "Rejected" || leavestatus == "Canceled"))
                                {
                                    html.Append("<tr class='data-row'> <td  class='exclude'  style='width:60px;' ><button title='Edit Leave' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");

                                    html.Append("<button type='button' title='Cancel Leave' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-warning btn-xs'><span class='glyphicon glyphicon-ban-circle'></span></button></td>");
                                }
                                else
                                {
                                    html.Append("<tr class='data-row'> <td>  </td>");

                                }

                            }




                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }











        public static StringBuilder htmlTableExport(int[] columnShow, DataTable datatable)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTableExport' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnShow.Contains(i)))
                    {
                        // html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }

                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<tr class='data-row'>");
                            }


                            if (!(columnShow.Contains(i)))
                            {
                                // html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public static StringBuilder htmlTableExport(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTableExport' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        //   html.Append("<th  class='exclude' hidden='hidden'>" + col.Caption + "</th>");

                    }

                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<tr class='data-row'>");
                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                //  html.Append("<td  class='exclude' hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }





        public static StringBuilder htmlTableEditMode(int[] columnShow, DataTable datatable)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr><th  class='exclude'>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnShow.Contains(i)))
                    {
                        html.Append("<th class='exclude' hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }

                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<tr class='data-row'> <td  class='exclude' style='width:60px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");

                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-trash'></span></button></td>");
                            }


                            if (!(columnShow.Contains(i)))
                            {
                                html.Append("<td  class='exclude' hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static StringBuilder htmlChildTableEditMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='childDataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='childdata-row'> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowValChild(" + rowId + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDeleteChild(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        //---------------------child table without delete

        public static StringBuilder htmlChildTableEditModeWithoutDelete(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='childDataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='childdata-row'> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowValChild(" + rowId + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");

                        foreach (var cell in row.ItemArray)
                        {



                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }


        //-------------Nested Html Table with hide column--------
        public static StringBuilder htmlNestedTable(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th></th><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr  class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'   </button></td> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");

                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-trash'></span></button></td>");
                            }
                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        childPanelDivId = "childPanelDiv" + rowId;
                        html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' > </div></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }



        //-------------Nested Html Table with hide column--------
        public static StringBuilder htmlNestedTableEditMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th></th><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr  class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'   </button></td> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");

                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        childPanelDivId = "childPanelDiv" + rowId;
                        html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' > </div></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        //-------------Nested Html Table with show column--------

        public static StringBuilder htmlNestedTableEditMode(int[] columnShow, DataTable datatable)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th></th><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnShow.Contains(i)))
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (!(columnShow.Contains(i)))
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        childPanelDivId = "childPanelDiv" + rowId;
                        html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' ></div></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    //html.Append("<button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnPdf'  class='btn  btn-success enabling cancel' name='command' value='Pdf'>Pdf</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnExcel' class='btn  btn-success enabling cancel' name='command' value='Excel'>Excel</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnWord' class='btn  btn-success enabling cancel' name='command' value='Word'>Word</button> ");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //-----------Nested grid without load panel- Load panel need to add at run time on loadChildPanal

        public static StringBuilder htmlNestedTableEditModeWithoutChildPanel(int[] columnShow, DataTable datatable)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th></th><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnShow.Contains(i)))
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");
                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (!(columnShow.Contains(i)))
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        //  childPanelDivId = "childPanelDiv" + rowId;
                        //    html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' ></div></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    //html.Append("<button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnPdf'  class='btn  btn-success enabling cancel' name='command' value='Pdf'>Pdf</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnExcel' class='btn  btn-success enabling cancel' name='command' value='Excel'>Excel</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnWord' class='btn  btn-success enabling cancel' name='command' value='Word'>Word</button> ");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static StringBuilder htmlNestedTableEmpBankdetailHistoryGet(int[] columnShow, DataTable datatable)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnShow.Contains(i)))
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                //  html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");
                                // html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (!(columnShow.Contains(i)))
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        //  childPanelDivId = "childPanelDiv" + rowId;
                        //    html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' ></div></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    //html.Append("<button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnPdf'  class='btn  btn-success enabling cancel' name='command' value='Pdf'>Pdf</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnExcel' class='btn  btn-success enabling cancel' name='command' value='Excel'>Excel</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnWord' class='btn  btn-success enabling cancel' name='command' value='Word'>Word</button> ");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static StringBuilder htmlNestedTableEditModeWithoutChildPanelHistory(int[] columHide, DataTable datatable)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if ((columHide.Contains(i)))
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                //  html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");
                                //  html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if ((columHide.Contains(i)))
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        //  childPanelDivId = "childPanelDiv" + rowId;
                        //    html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' ></div></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    //html.Append("<button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnPdf'  class='btn  btn-success enabling cancel' name='command' value='Pdf'>Pdf</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnExcel' class='btn  btn-success enabling cancel' name='command' value='Excel'>Excel</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnWord' class='btn  btn-success enabling cancel' name='command' value='Word'>Word</button> ");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static StringBuilder htmlNestedTableEmpDepartmentGetHistory(int[] columnShow, DataTable datatable)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnShow.Contains(i)))
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                //  html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");
                                //   html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (!(columnShow.Contains(i)))
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        //  childPanelDivId = "childPanelDiv" + rowId;
                        //    html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' ></div></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    //html.Append("<button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnPdf'  class='btn  btn-success enabling cancel' name='command' value='Pdf'>Pdf</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnExcel' class='btn  btn-success enabling cancel' name='command' value='Excel'>Excel</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnWord' class='btn  btn-success enabling cancel' name='command' value='Word'>Word</button> ");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static StringBuilder htmlNestedTableEditModeEmpshifthistory(int[] columnShow, DataTable datatable)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnShow.Contains(i)))
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                //  html.Append("<tr  id='DataRow" + rowId + "' class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'  > </button></td> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn btn-success btn-xs'><span class='glyphicon glyphicon-edit'></span></button>");
                                //   html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (!(columnShow.Contains(i)))
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        //  childPanelDivId = "childPanelDiv" + rowId;
                        //    html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' ></div></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    //html.Append("<button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnPdf'  class='btn  btn-success enabling cancel' name='command' value='Pdf'>Pdf</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnExcel' class='btn  btn-success enabling cancel' name='command' value='Excel'>Excel</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnWord' class='btn  btn-success enabling cancel' name='command' value='Word'>Word</button> ");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        //-------------Nested Html Table with show column without delete button--------

        public static StringBuilder htmlNestedTableEditModeWithoutDelete(int[] columnShow, DataTable datatable)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th></th><th>Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnShow.Contains(i)))
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'   </button></td> <td style='width:70px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowVal(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button></td>");
                                // html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                            }
                            if (!(columnShow.Contains(i)))
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        childPanelDivId = "childPanelDiv" + rowId;
                        html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' ></div></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        //-------------Html Table with hide column and Download Option --------
        public static StringBuilder htmlViewDocumentNestedTableEditMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th></th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                DataKeyId = Id;
                                html.Append("<tr class='data-row'><td style='width:50px;'> <button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'   </button></td>");
                            }

                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }

                            i = i + 1;
                        }
                        html.Append("</tr>");
                        childPanelDivId = "childPanelDiv" + rowId;
                        html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' > </div></tr>");



                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //---------- Child Grid With Download Options ---------------------------------------------------
        public static StringBuilder htmlChildTableDownloadEditMode(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='childDataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr><th>Download</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        html.Append("<tr class='data-row'> <td style='width:40px;' >");
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = Convert.ToString(cell);
                                if (!(string.IsNullOrEmpty(Id)))
                                {

                                    html.Append("<button type='button' title='Download Document' style='margin-left:1px;margin-right:1px;' onclick='SubmitForm(" + Id + ")' name='command' value='Download'  class='btn-outline-secondary'><span class='glyphicon glyphicon-download'></span></button></td>");
                                }
                                else
                                {
                                    html.Append(" No Document</td>");
                                }

                            }
                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                    //<%: Html.ActionLink(img,"GetFile/"+data++) %>
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        //-------------------Daily Attendance Entry-------------------------

        public static StringBuilder htmlTableDailyAttendanceEntry(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string txtId = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        if (i == 10)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxPresentChange()' id='IsCheckedPresentAll' name='IsCheckedPresentAll' /> Present</th>");
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxAbsentChange()' id='IsCheckedAbsentAll' name='IsCheckedAbsentAll' /> Absent</th>");
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxLeaveChange()' id='IsCheckedLeaveAll' name='IsCheckedLeaveAll' /> Leave</th>");

                        }
                        else
                        {
                            html.Append("<th>" + col.Caption + "</th>");
                        }


                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }

                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                string rName = string.Empty;

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();

                                if (i == 1)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td ><input id='" + txtName + "' name='" + txtName + "' readonly='readonly' class='form-control' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 8)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].InTime";
                                    html.Append("<td ><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' type='TEXT' class='form-control' /> </td>");
                                }
                                else if (i == 9)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].OutTime";
                                    html.Append("<td ><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' type='TEXT' class='form-control' /> </td>");
                                }
                                else if (i == 10)
                                {




                                    rName = "command[" + rowId + "]";
                                    txtName = "EmployeeDataList[" + rowId + "].AttendanceStatus";
                                    // if (command == "Edit")

                                    rowData = cell.ToString().Trim();
                                    if (rowData == "P")
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "' checked='checked' value='P' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                    }
                                    else if (rowData == "A")
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "'  value='P' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline' name='" + rName + "' checked='checked' value='A' onchange='makeRadioButtons(" + rowId + ",this)'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                    }
                                    else if (rowData == "L")
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline' name='" + rName + "'  value='P' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline' name='" + rName + "' checked='checked' value='L' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                    }
                                    else if ((string.IsNullOrEmpty(rowData)) || (rowData == ""))
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "' checked='checked'  value='P' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' onchange='makeRadioButtons(" + rowId + ",this)' />  </td>");
                                    }



                                    else
                                    {
                                        //----------in case of new record----------------
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "'  value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline' name='" + rName + "'  value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline' name='" + rName + "' value='L' />  </td>");
                                    }



                                }

                                else if (i == 11)
                                {
                                    rowData = cell.ToString().Trim();
                                    txtName = "EmployeeDataList[" + rowId + "].WorkedUnits";
                                    txtId = "WorkedUnits" + rowId;
                                    html.Append("<td><input id='" + txtId + "' name='" + txtName + "' class='form-control '  onchange='CheckWorkedDay(" + rowId + ")'  maxlength='5' value='" + rowData + "' />  </td>");
                                }
                                else if (i == 12)
                                {
                                    rowData = cell.ToString().Trim();
                                    txtName = "EmployeeDataList[" + rowId + "].OvertimeHours";
                                    txtId = "OvertimeHours" + rowId;
                                    html.Append("<td><input id='" + txtId + "' name='" + txtName + "' class='form-control ' onchange='CheckOverTime(" + rowId + ")'  maxlength='5' value='" + rowData + "' />  </td>");
                                }
                                else if (i == 13)
                                {
                                    rowData = cell.ToString().Trim();
                                    txtName = "EmployeeDataList[" + rowId + "].Remarks";
                                    html.Append("<td><input id='" + txtName + "' name='" + txtName + "'  maxlength='100' class='form-control' value='" + rowData + "' />  </td>");
                                }
                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmployeeDataList[" + rowId + "].EmpDailyAttendanceId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                }

                                else if (i == 1)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }

                                else if (i == 8)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].InTime";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 9)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].OutTime";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    //html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' id='insert' value='Insert'  name='commandName' onclick='return  CheckValidation()' class='btn btn-success '>Insert</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }











        //-------------------Biometric Daily Attendance Entry-------------------------

        public static StringBuilder htmlTableBMDailyAttendanceEntry(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string txtId = "";
                string rowData = "";
                html.Append("<table id='data_Table'  width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        if (i == 10)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxPresentChange()' id='IsCheckedPresentAll' name='IsCheckedPresentAll' /> Present</th>");
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxAbsentChange()' id='IsCheckedAbsentAll' name='IsCheckedAbsentAll' /> Absent</th>");
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxLeaveChange()' id='IsCheckedLeaveAll' name='IsCheckedLeaveAll' /> Leave</th>");

                        }
                        else
                        {
                            html.Append("<th>" + col.Caption + "</th>");
                        }



                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }

                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                string rName = string.Empty;

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = Convert.ToString(cell).Trim();
                                if (i == 6)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].InTime";
                                    txtId = "InTime" + rowId;
                                    html.Append("<td ><input placeholder='00:00' maxlength=5 class='time_picker form-control form-control-small'  onchange='CheckInTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 7)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].OutTime";
                                    txtId = "OutTime" + rowId;
                                    html.Append("<td ><input placeholder='00:00' maxlength=5 class='time_picker form-control form-control-small' onchange='CheckOutTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                //else if (i == 9)
                                //{

                                //    txtName = "EmployeeDataList[" + rowId + "].Remarks";
                                //    txtId = "Remarks" + rowId;
                                //    html.Append("<td ><input  class='form-control form-control-small' onchange='CheckOutTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                //}
                                else if (i == 11)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].Overtime";
                                    txtId = "Overtime" + rowId;
                                    html.Append("<td ><input placeholder='0.00'  class='form-control form-control-small' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                //else if (i == 10)
                                //{

                                //    rName = "command[" + rowId + "]";
                                //    txtName = "EmployeeDataList[" + rowId + "].Status";
                                //    // if (command == "Edit")

                                //    rowData = cell.ToString().Trim();
                                //    if (rowData == "P")
                                //    {
                                //        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "' checked='checked' value='P' />  </td>");
                                //        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A'  />  </td>");
                                //        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' />  </td>");
                                //    }
                                //    else if (rowData == "A")
                                //    {
                                //        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "'  value='P' />  </td>");
                                //        html.Append("<td><input  type='radio' class='radio_Absent radio-inline' name='" + rName + "' checked='checked' value='A'  />  </td>");
                                //        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' />  </td>");
                                //    }
                                //    else if (rowData == "L")
                                //    {
                                //        html.Append("<td><input type='radio' class='radio_Present radio-inline' name='" + rName + "'  value='P' />  </td>");
                                //        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A'  />  </td>");
                                //        html.Append("<td><input  type='radio' class='radio_Leave radio-inline' name='" + rName + "' checked='checked' value='L' />  </td>");
                                //    }
                                //    else if ((string.IsNullOrEmpty(rowData)) || (rowData == ""))
                                //    {
                                //        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "' checked='checked'  value='P' />  </td>");
                                //        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A'  />  </td>");
                                //        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' />  </td>");
                                //    }
                                //    else
                                //    {
                                //        //----------in case of new record----------------
                                //        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "'  value='P' />  </td>");
                                //        html.Append("<td><input  type='radio' class='radio_Absent radio-inline' name='" + rName + "'  value='A'  />  </td>");
                                //        html.Append("<td><input  type='radio' class='radio_Leave radio-inline' name='" + rName + "' value='L' />  </td>");
                                //    }


                                //}

                                else
                                {
                                    html.Append("<td>" + Convert.ToString(cell).Trim() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = Convert.ToString(cell).Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmployeeDataList[" + rowId + "].BMUserId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                }
                                //if (i == 8)
                                //{

                                //    txtName = "EmployeeDataList[" + rowId + "].Remarks";
                                //    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                //}

                                else
                                {

                                    html.Append("<td hidden='hidden'>" + Convert.ToString(cell).Trim() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' id='insert' value='Insert'  name='command' onclick='return  CheckValidation()' class='btn btn-success '><span class='glyphicon glyphicon-picture'></span> Save</ button> &nbsp;&nbsp; <button type='submit' id='delete' value='Delete'  name='command' onclick='return  CheckValidation()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Delete</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }
        public static string getshortmonthname(int month)
        {
            switch (month)
            {
                case 1: return "Jan";
                case 2: return "Feb";
                case 3: return "Mar";
                case 4: return "Apr";
                case 5: return "May";
                case 6: return "Jun";
                case 7: return "Jul";
                case 8: return "Aug";
                case 9: return "Sep";
                case 10: return "Oct";
                case 11: return "Nov";
                case 12: return "Dec";
                default:
                    return null;

            }






        }




        //-------------------Biometric Bulk Attendance Entry-------------------------

        public static StringBuilder htmlTableBMBulkAttendanceEntry(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string txtId = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");



                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }

                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                string rName = string.Empty;

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = Convert.ToString(cell).Trim();
                                if (i == 6)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].AttendanceDate";
                                    txtId = "AttendanceDate" + rowId;
                                    html.Append("<td ><input readonly='readonly' class='form-control '   id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 11)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].InTime";
                                    txtId = "InTime" + rowId;
                                    html.Append("<td ><input placeholder='00:00' maxlength=5 class='time_picker form-control form-control-small'  onchange='CheckInTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 12)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].OutTime";
                                    txtId = "OutTime" + rowId;
                                    html.Append("<td ><input placeholder='00:00' maxlength=5 class='time_picker form-control form-control-small' onchange='CheckOutTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 15)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].Overtime";
                                    txtId = "OutTime" + rowId;
                                    html.Append("<td ><input placeholder='0.00'  class='form-control form-control-small' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }

                                else
                                {
                                    html.Append("<td>" + Convert.ToString(cell).Trim() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = Convert.ToString(cell).Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmployeeDataList[" + rowId + "].BMUserId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                }
                                if (i == 13)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].Remarks";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }

                                else
                                {

                                    html.Append("<td hidden='hidden'>" + Convert.ToString(cell).Trim() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' id='insert' value='Insert'  name='command' onclick='return  CheckValidation()' class='btn btn-success '><span class='glyphicon glyphicon-picture'></span> Save</ button> &nbsp;&nbsp; <button type='submit' id='delete' value='Delete'  name='command' onclick='return  CheckValidation()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Delete</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }





        //-------------------------Time Sheet Entry------------------------

        public static StringBuilder htmlTableTSDailyAttendanceEntry(DataTable datatable, int[] columnHide)
        {
            int Check = 0;
            try
            {

                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string txtId = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead>");
                html.Append("<tr style='background-color:#34495e;color:#fff;'><th colspan='6'></th><th colspan='3'>Working Time</th><th colspan='3'></th><th colspan='5'>Break Down</th><th colspan='4'></th><th colspan='9'></th></tr><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        if (i == 24)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxPresentChange()' id='IsCheckedPresentAll' name='IsCheckedPresentAll' /> Present</th>");
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxAbsentChange()' id='IsCheckedAbsentAll' name='IsCheckedAbsentAll' /> Absent</th>");
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxLeaveChange()' id='IsCheckedLeaveAll' name='IsCheckedLeaveAll' /> Leave</th>");

                        }
                        else
                        {
                            html.Append("<th>" + col.Caption + "</th>");
                        }


                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }

                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                string rName = string.Empty;

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            if (!(columnHide.Contains(i)))
                            {
                                var txtId3 = Convert.ToDateTime(row.ItemArray[11]).Hour + (Convert.ToDateTime(row.ItemArray[11]).Minute) / 60;
                                var txtId1 = Convert.ToDateTime(row.ItemArray[17]).Hour + (Convert.ToDateTime(row.ItemArray[17]).Minute) / 60;
                                //var txtId2 = row.ItemArray[22].ToString() != ""? Convert.ToDecimal(row.ItemArray[22]):0;
                                var txtId2 = row.ItemArray[21].ToString() != "" ? Convert.ToDecimal(row.ItemArray[21]) : 0;
                                decimal d = txtId3;
                                decimal empot;
                                if (row.ItemArray[22].ToString() != "0")
                                {
                                    empot = Convert.ToDecimal(row.ItemArray[22]);
                                }
                                else
                                {
                                    empot = 0;
                                }

                                var total = Convert.ToString(txtId3 - txtId2 - txtId1).Contains("-") || Convert.ToString(txtId3 - txtId2 - txtId1).Contains("000") ? 0 : txtId3 - txtId2 - txtId1;

                                var EmployeetotalOT = Convert.ToString(txtId3 - empot).Contains("-") || Convert.ToString(txtId3 - empot).Contains("0.00") ? 0 : txtId3 - empot;

                                rowData = Convert.ToString(cell).Trim();
                                if (i == 7)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].AttendanceDate";
                                    txtId = "AttendanceDate" + rowId;
                                    html.Append("<td ><input style='width:100px' readonly='readonly' class='form-control time'   id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 12)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].WorkInTime";
                                    txtId = "WorkInTime" + rowId;
                                    html.Append("<td ><input style='width:100px'  placeholder='00:00' maxlength=5 class='time_picker form-control form-control-small'  onchange='WorkInTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 13)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].WorkOutTime";
                                    txtId = "WorkOutTime" + rowId;
                                    html.Append("<td ><input style='width:100px'  placeholder='00:00' maxlength=5 class='time_picker form-control form-control-small' onchange='WorkOutTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 14)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].LunchBreak";
                                    txtId = "LunchBreak" + rowId;
                                    html.Append("<td ><input  style='width:100px'  style='width:100px'  placeholder='00:00' maxlength=5 class='time_picker form-control form-control-small' onchange='LunchBreak(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 18)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].BreakInTime";
                                    txtId = "BreakInTime" + rowId;
                                    html.Append("<td ><input style='width:100px'  placeholder='00:00' maxlength=5 class='time_picker form-control form-control-small' onchange='BreakInTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                }
                                else if (i == 19)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].BreakOutTime";
                                    txtId = "BreakOutTime" + rowId;
                                    html.Append("<td ><input style='width:100px'placeholder='HH:MM'  placeholder='00:00' maxlength=5 class='time_picker form-control form-control-small' onchange='BreakOutTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 21)
                                {

                                    html.Append("<td>" + Convert.ToString(total).Trim() + "</td>");

                                }
                                else if (i == 22)
                                {

                                    html.Append("<td>" + Convert.ToString(EmployeetotalOT).Trim() + "</td>");

                                }
                                else if (i == 23)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].MeterReading";
                                    txtId = "MeterReading" + rowId;
                                    html.Append("<td ><input style='width:100px'  placeholder='' maxlength=5 class='form-control' onchange='CheckMR(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }

                                else if (i == 24)
                                {

                                    rName = "command[" + rowId + "]";
                                    txtName = "EmployeeDataList[" + rowId + "].AttendanceStatus";
                                    // if (command == "Edit")

                                    rowData = cell.ToString().Trim();
                                    if (rowData == "P")
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "' checked='checked' value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' />  </td>");
                                    }
                                    else if (rowData == "A")
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "'  value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline' name='" + rName + "' checked='checked' value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' />  </td>");
                                    }
                                    else if (rowData == "L")
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline' name='" + rName + "'  value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline' name='" + rName + "' checked='checked' value='L' />  </td>");
                                    }
                                    else if ((string.IsNullOrEmpty(rowData)) || (rowData == ""))
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "' checked='checked'  value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' />  </td>");
                                    }




                                    else
                                    {
                                        //----------in case of new record----------------
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "'  value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline' name='" + rName + "'  value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline' name='" + rName + "' value='L' />  </td>");
                                    }
                                }
                                else if (i == 26)
                                {
                                    if (rowData != "")
                                    {
                                        // html.Append("<div id=""></div>"); 
                                        txtName = "EmployeeDataList[" + rowId + "].UploadAttachment";
                                        html.Append("<td><button style='width:32px;height:32px' type='button' id='btnInsert' name='btnInsert' class='btn btn-warning'  onclick='SubmitForm(" + rowId + "," + Check + ")'><span class='glyphicon glyphicon-eye-open'></span></td>");

                                    }
                                    else
                                    {
                                        html.Append("<td> No Document</td>");
                                    }

                                }
                                else if (i == 28)
                                {
                                    txtName = "EmployeeDataList[" + rowId + "].Id";
                                    txtId = "Id" + rowId;
                                    html.Append("<td ><input style='width:100px'readonly='readonly' class='form-control'id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else
                                {
                                    html.Append("<td>" + Convert.ToString(cell).Trim() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = Convert.ToString(cell).Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmployeeDataList[" + rowId + "].BMUserId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                }
                                if (i == 12)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].Remarks";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }

                                else
                                {

                                    html.Append("<td hidden='hidden'>" + Convert.ToString(cell).Trim() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' id='insert' value='Insert'  name='command' onclick='return  CheckValidation()' class='btn btn-success '><span class='glyphicon glyphicon-picture'></span> Save</ button>  &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; <button type='submit' id='delete' value='Delete'  name='command' onclick='return  CheckValidation()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Delete</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }


        }

        //--------------------------Time Sheet Bulk Entry-----------------------------

        public static StringBuilder htmlTableTimeSheetBulkEntry(DataTable datatable, int[] columnHide)
        {
            int Check = 1;
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string txtId = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead>");
                html.Append("<tr style='background-color:#34495e;color:#fff;'><th colspan='6'></th><th colspan='3'>Working Time</th><th colspan='3'></th><th colspan='5'>Break Down</th><th colspan='4'></th><th colspan='7'>Check Attendance</th></tr><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        if (i == 24)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxPresentChange()' id='IsCheckedPresentAll' name='IsCheckedPresentAll' /> Present</th>");
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxAbsentChange()' id='IsCheckedAbsentAll' name='IsCheckedAbsentAll' /> Absent</th>");
                            html.Append("<th><input type='checkbox' class='checkbox-inline' onchange='checkBoxLeaveChange()' id='IsCheckedLeaveAll' name='IsCheckedLeaveAll' /> Leave</th>");

                        }
                        else
                        {
                            html.Append("<th>" + col.Caption + "</th>");
                        }


                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }

                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                string rName = string.Empty;

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {

                        DateTime thisDay = DateTime.Today;
                        // Display the date in the default (general) format.
                        //   Console.WriteLine(thisDay.ToString());

                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {

                            if (!(columnHide.Contains(i)))
                            {



                                var txtId3 = Convert.ToDateTime(row.ItemArray[11]).Hour + (Convert.ToDateTime(row.ItemArray[11]).Minute) / 60;
                                var txtId1 = Convert.ToDateTime(row.ItemArray[17]).Hour + (Convert.ToDateTime(row.ItemArray[17]).Minute) / 60;
                                //var txtId2 = row.ItemArray[22].ToString() != ""? Convert.ToDecimal(row.ItemArray[22]):0;
                                var txtId2 = row.ItemArray[21].ToString() != "" ? Convert.ToDecimal(row.ItemArray[21]) : 0;
                                decimal d = txtId3;
                                decimal empot;
                                if (row.ItemArray[22].ToString() != "0:0")
                                {
                                    empot = Convert.ToDecimal(row.ItemArray[22]);
                                }
                                else
                                {
                                    empot = 0;
                                }

                                var total = Convert.ToString(txtId3 - txtId2 - txtId1).Contains("-") || Convert.ToString(txtId3 - txtId2 - txtId1).Contains("000") ? 0 : txtId3 - txtId2 - txtId1;

                                var EmployeetotalOT = Convert.ToString(txtId3 - empot).Contains("-") || Convert.ToString(txtId3 - empot).Contains("0.00") ? 0 : txtId3 - empot;

                                rowData = Convert.ToString(cell).Trim();
                                if (i == 7)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].AttendanceDate";
                                    txtId = "AttendanceDate" + rowId;
                                    html.Append("<td ><input style='width:100px' readonly='readonly' class='time_picker form-control form-control-small'   id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 12)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].WorkInTime";
                                    txtId = "WorkInTime" + rowId;
                                    html.Append("<td ><input style='width:100px' class='time_picker form-control form-control-small' type='text' size='10' placeholder='HH:MM' onchange='WorkInTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 13)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].WorkOutTime";
                                    txtId = "WorkOutTime" + rowId;
                                    html.Append("<td ><input style='width:100px'  maxlength=5 class='time_picker form-control form-control-small'placeholder='HH:MM' onchange='WorkOutTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 14)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].LunchBreak";
                                    txtId = "LunchBreak" + rowId;
                                    html.Append("<td ><input  style='width:100px'  style='width:100px'   maxlength=5 class='time_picker form-control form-control-small'placeholder='HH:MM' onchange='LunchBreak(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 18)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].BreakInTime";
                                    txtId = "BreakInTime" + rowId;
                                    html.Append("<td ><input style='width:100px'   maxlength=5 class='time_picker form-control form-control-small'placeholder='HH:MM' onchange='BreakInTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                }
                                else if (i == 19)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].BreakOutTime";
                                    txtId = "BreakOutTime" + rowId;
                                    html.Append("<td ><input style='width:100px' placeholder='HH:MM' maxlength=5 class='time_picker form-control form-control-small' onchange='BreakOutTime(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 21)
                                {

                                    html.Append("<td>" + Convert.ToString(total).Trim() + "</td>");

                                }
                                else if (i == 22)
                                {

                                    html.Append("<td>" + Convert.ToDecimal(row.ItemArray[22].ToString()) + "</td>");

                                }
                                else if (i == 23)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].MeterReading";
                                    txtId = "MeterReading" + rowId;
                                    html.Append("<td ><input style='width:100px'  placeholder='' maxlength=5 class='form-control' onchange='CheckMR(" + rowId + ")' id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 24)
                                {

                                    rName = "command[" + rowId + "]";
                                    txtName = "EmployeeDataList[" + rowId + "].AttendanceStatus";
                                    // if (command == "Edit")

                                    rowData = cell.ToString().Trim();
                                    if (rowData == "P")
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "' checked='checked' value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' />  </td>");
                                    }
                                    else if (rowData == "A")
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "'  value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline' name='" + rName + "' checked='checked' value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' />  </td>");
                                    }
                                    else if (rowData == "L")
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline' name='" + rName + "'  value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline' name='" + rName + "' checked='checked' value='L' />  </td>");
                                    }
                                    else if ((string.IsNullOrEmpty(rowData)) || (rowData == ""))
                                    {
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "' checked='checked'  value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline'  name='" + rName + "'  value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline'  name='" + rName + "' value='L' />  </td>");
                                    }



                                    else
                                    {
                                        //----------in case of new record----------------
                                        html.Append("<td><input type='radio' class='radio_Present radio-inline'  name='" + rName + "'  value='P' />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Absent radio-inline' name='" + rName + "'  value='A'  />  </td>");
                                        html.Append("<td><input  type='radio' class='radio_Leave radio-inline' name='" + rName + "' value='L' />  </td>");
                                    }


                                }
                                else if (i == 26)
                                {
                                    if (rowData != "")
                                    {
                                        // html.Append("<div id=""></div>"); 
                                        txtName = "EmployeeDataList[" + rowId + "].UploadAttachment";
                                        html.Append("<td><button style='width:32px;height:32px' type='button' id='btnInsert' name='btnInsert' class='btn btn-warning'  onclick='SubmitForm(" + rowId + "," + Check + ")'><span class='glyphicon glyphicon-eye-open'></span></td>");

                                    }
                                    else
                                    {
                                        html.Append("<td> No Document</td>");
                                    }

                                }
                                else if (i == 28)
                                {
                                    txtName = "EmployeeDataList[" + rowId + "].Id";
                                    txtId = "Id" + rowId;
                                    html.Append("<td ><input style='width:100px'readonly='readonly' class='form-control'id='" + txtId + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else
                                {
                                    html.Append("<td>" + Convert.ToString(cell).Trim() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = Convert.ToString(cell).Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmployeeDataList[" + rowId + "].BMUserId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                }
                                if (i == 12)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].Remarks";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                if (i == 28)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].Id";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else
                                {

                                    html.Append("<td hidden='hidden'>" + Convert.ToString(cell).Trim() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' id='insert' value='Insert'  name='command' onclick='return  CheckValidation()' class='btn btn-success '><span class='glyphicon glyphicon-picture'></span> Save</ button>  &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; <button type='submit' id='delete' value='Delete'  name='command' onclick='return  CheckValidation()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Delete</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }


        //-------------------Attendance Entry-------------------------
        //-- without disabled checkbox ----------------
        public static StringBuilder htmlTableAttendanceEntryOLD(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string txtId = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' />    </th><th>SNo.</th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");

                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();

                                if (i == 1)
                                {
                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td ><input id='" + txtName + "' name='" + txtName + "' readonly='readonly' class='form-control' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 13)
                                {
                                    txtName = "EmployeeDataList[" + rowId + "].WorkqtyActual";
                                    txtId = "WorkqtyActual" + rowId;
                                    html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' onchange='CheckWorkedDay(" + rowId + ")' class='form-control workedday' maxlength='5' value='" + rowData + "' />  </td>");
                                }
                                else if (i == 14)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].OvertimeActual";
                                    txtId = "OvertimeActual" + rowId;
                                    html.Append("<td><input id='" + txtId + "' name='" + txtName + "' class='form-control overtime' onchange='CheckOvertime(" + rowId + ")' maxlength='6' value='" + rowData + "' />  </td>");
                                }
                                else if (i == 15)
                                {



                                    txtName = "EmployeeDataList[" + rowId + "].LocalDay";
                                    txtId = "LocalDay" + rowId;
                                    html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' class='form-control LocalDay' onchange='CheckLocalDay(" + rowId + ")' maxlength='6' value='" + rowData + "' />  </td>");
                                    //local day              //  html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' class='form-control LocalDay' onchange='CheckLocalDay(" + rowId + ")' maxlength='6' value='" + rowData + "' />  </td>");


                                }
                                else if (i == 16)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].NonLocalDay";
                                    txtId = "NonLocalDay" + rowId;
                                    html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' class='form-control NonLocalDay' onchange='CheckNonLocalDay(" + rowId + ")' maxlength='6' value='" + rowData + "' />  </td>");
                                    // non local day //     html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' class='form-control NonLocalDay' onchange='CheckNonLocalDay(" + rowId + ")' maxlength='6' value='" + rowData + "' />  </td>");
                                }
                                else if (i == 17)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].Notes";
                                    html.Append("<td><input  id='" + txtName + "' name='" + txtName + "' maxlength='200' class='form-control' value='" + rowData + "' />  </td>");
                                }
                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }
                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmployeeDataList[" + rowId + "].EmpAttendanceEntryId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td><td>" + (rowId + 1) + "</td>");
                                }

                                else if (i == 1)
                                {
                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 7)
                                {
                                    txtName = "EmployeeDataList[" + rowId + "].Workunit";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else
                                {
                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' id='insert' value='Insert'  name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'><span class='glyphicon glyphicon-picture'></span> Save</ button></td></tr>");
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public static StringBuilder htmlTableAttendanceEntryOld(DataTable datatable, int[] columnHide, int? AttendanceSourceId)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string txtId = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {


                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' />    </th><th>SNo.</th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");




                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        string flag = Convert.ToString(datatable.Rows[rowId]["flag"]);

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();

                                if (i == 1)
                                {
                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td ><input id='" + txtName + "' name='" + txtName + "' readonly='readonly' class='form-control' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 13)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].WorkqtyActual";
                                    txtId = "WorkqtyActual" + rowId;
                                    if (AttendanceSourceId != 0)
                                    {
                                        html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' readonly='readonly' onchange='CheckWorkedDay(" + rowId + ")' class='form-control workedday' maxlength='5' value='" + rowData + "' />  </td>");
                                    }
                                    else
                                    {
                                        html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' onchange='CheckWorkedDay(" + rowId + ")' class='form-control workedday' maxlength='5' value='" + rowData + "' />  </td>");
                                    }

                                }
                                else if (i == 14)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].OvertimeActual";
                                    txtId = "OvertimeActual" + rowId;
                                    html.Append("<td><input id='" + txtId + "' name='" + txtName + "' class='form-control overtime' onchange='CheckOvertime(" + rowId + ")' maxlength='6' value='" + rowData + "' />  </td>");
                                }

                                else if (i == 15)
                                {



                                    txtName = "EmployeeDataList[" + rowId + "].LocalDay";
                                    txtId = "LocalDay" + rowId;
                                    html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' class='form-control LocalDay' onchange='CheckLocalDay(" + rowId + ")' maxlength='6' value='" + rowData + "' />  </td>");
                                    //local day              //  html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' class='form-control LocalDay' onchange='CheckLocalDay(" + rowId + ")' maxlength='6' value='" + rowData + "' />  </td>");


                                }
                                else if (i == 16)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].NonLocalDay";
                                    txtId = "NonLocalDay" + rowId;
                                    html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' class='form-control NonLocalDay' onchange='CheckNonLocalDay(" + rowId + ")' maxlength='6' value='" + rowData + "' />  </td>");
                                    // non local day //     html.Append("<td><input  id='" + txtId + "' name='" + txtName + "' class='form-control NonLocalDay' onchange='CheckNonLocalDay(" + rowId + ")' maxlength='6' value='" + rowData + "' />  </td>");


                                }
                                else if (i == 17)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].Notes";
                                    html.Append("<td><input  id='" + txtName + "' name='" + txtName + "' maxlength='200' class='form-control' value='" + rowData + "' />  </td>");
                                }

                                else if (i == 18)
                                {
                                    if (flag == "1")
                                    {
                                        html.Append("<td style='color:red;'>" + cell.ToString() + "</td>");
                                    }
                                    else
                                    {
                                        html.Append("<td>" + cell.ToString() + "</td>");
                                    }
                                }
                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";

                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    if (flag == "1")
                                    {
                                        html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline' disabled=true  name='" + txtName + "'  />  </td>");
                                    }
                                    else
                                    {
                                        html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    }
                                    txtName = "EmployeeDataList[" + rowId + "].EmpAttendanceEntryId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td><td>" + (rowId + 1) + "</td>");

                                }

                                else if (i == 1)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 7)
                                {
                                    txtName = "EmployeeDataList[" + rowId + "].Workunit";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                }
                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' id='insert' value='Insert'  name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'><span class='glyphicon glyphicon-picture'></span> Save</ button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }
        //Daily Wages Atendance Entry





        //----------------------Precess Leave Cycle---------------

        public static StringBuilder htmlTableProcessLeaveCycle(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<div>");
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {

                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "LeaveCyclingList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "LeaveCyclingList[" + rowId + "].LeaveTypeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                    //html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");

                                }

                                //else if (i == 1)
                                //{

                                //    //txtName = "LeaveCyclingList[" + rowId + "].LeaveTypeId";
                                //    //html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                //}

                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }

                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    //html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' id='Begin' value='BeginProcess' name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'>Begin Process</button>&nbsp;&nbsp;<button type='submit' id='End'  value='EndProcess' name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'>End Process</button></td></tr>");
                    html.Append("<br/><tr class='data-row' ><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' value='BeginProcess' name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'>Begin Process</button>&nbsp;&nbsp;<button type='submit' value='EndProcess' name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'>End Process</button></td></tr>");
                    //html.Append("<br/><tr class='data-row' ><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit'  id='btnbegin' onclick='return  CheckValidation()' class='btn btn-success enabling'>Begin Process</button>&nbsp;&nbsp;<button type='submit' id='btnEnd' onclick='return  CheckValidation()' class='btn btn-success enabling'>End Process</button></td></tr>");
                    html.Append("</tbody></table>");
                    html.Append("</div>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;

            }
        }

        public static StringBuilder htmlTableLeaveCycle(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='data_Table1' class='display table table-striped table-bordered table-hover table-responsive' style='margin-left:5px;margin-right:5px;'   width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        //-------------------Attendance Approval-------------------------

        public static StringBuilder htmlTableAttendanceApproval(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th style='width: 173px; text-align: center;'>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th style='width: 30px; text-align: center;'><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th><th style='width: 30px; text-align: center;'>SNo.</th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 1)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td ><input id='" + txtName + "' name='" + txtName + "'  readonly='readonly' class='form-control' value='" + rowData + "' /> </td>");
                                }
                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }


                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td><td>" + (rowId + 1) + "</td>");
                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");

                                }

                                else if (i == 1)
                                {

                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }

                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    //html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' id='insert' value='Insert' name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'>Approve Attendance</button>&nbsp;&nbsp;<button type='submit'  value='Unlock' name='command' onclick='return  CheckValidationUnlock()' class='btn btn-success'>Unlock Attendance</button></td></tr>");
                    html.Append("</tbody></table>");


                    html.Append("<br/><div style='text-align:center;' ><button type='submit' id='insert' value='Insert' name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'>Approve Attendance</button>&nbsp;&nbsp;<button type='submit'  value='Unlock' name='command' onclick='return  CheckValidationUnlock()' class='btn btn-success'>Unlock Attendance</button></div><br/>");



                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        //-------------------Payroll Process-------------------------

        public static StringBuilder htmlTablePayrollProcess(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {

                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                            html.Append("<th>SNo.</th>");

                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        string flag = Convert.ToString(datatable.Rows[rowId]["IsUsed"]);

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {

                                rowData = cell.ToString().Trim();
                                if (flag == "1")
                                {
                                    if (i == 12)
                                    {


                                        html.Append("<td style='color:red;'>" + cell.ToString() + "</td>");
                                    }
                                    else
                                    {
                                        if (i == 3)
                                        {
                                            txtName = "EmployeeDataList[" + rowId + "].EmpCode";
                                            html.Append("<td><input  readonly=readonly id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");
                                        }
                                        else
                                        {
                                            html.Append("<td>" + cell.ToString() + "</td>");
                                        }
                                    }
                                }
                                else
                                {
                                    if (i == 3)
                                    {
                                        txtName = "EmployeeDataList[" + rowId + "].EmpCode";
                                        html.Append("<td><input  readonly=readonly id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");
                                    }
                                    else
                                    {

                                        html.Append("<td>" + cell.ToString() + "</td>");
                                    }
                                }


                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";


                                    if (flag == "1")
                                    {
                                        txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                        html.Append("<td '><input type='checkbox' disabled='disabled' class='checkbox-inline'  name='" + txtName + "'  />  </td>");
                                        txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                        html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td><td>" + (rowId + 1) + "</td>");
                                    }
                                    else
                                    {
                                        txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                        html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                        txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                        html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td><td>" + (rowId + 1) + "</td>");
                                    }




                                }

                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }



                    //  html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' id='insert' value='Insert' name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'>Generate Payroll</button>&nbsp;&nbsp;<button type='submit'  value='DisApprove' name='command' onclick='return  CheckValidationDisapprove()' class='btn btn-success'>DisApprove Attendance</button></td></tr>");
                    html.Append("</tbody></table>");
                    html.Append("<br/><div style='text-align:center;' ><button type='submit' id='insert' value='Insert' name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'>Generate Payroll</button>&nbsp;&nbsp;<button type='submit'  value='DisApprove' name='command' onclick='return  CheckValidationDisapprove()' class='btn btn-success'>DisApprove Attendance</button></div><br/>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        //-------------------Payroll Details-------------------------

        public static StringBuilder htmlTablePayrollDetailsOld(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null, DataKeyId = null;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='table table-striped table-bordered table-hover table-responsive'><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th><th></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();

                                html.Append("<td>" + cell.ToString() + "</td>");


                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {

                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }
                                else if (i == 1)
                                {
                                    DataKeyId = rowData;

                                    html.Append("<td><button type='button' id='ParentRow" + rowId + "' class='btn btn-default btn-xs glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'>   </button></td>");
                                }

                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        childPanelDivId = "childPanelDiv" + rowId;
                        html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' > </div></td></tr>");

                        rowId = rowId + 1;
                    }


                    //   html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='button'  value='Delete' name='command' onclick='return  CheckValidationDeletePayroll()' class='btn btn-success enabling'>Delete Payroll</button></td></tr>");
                    html.Append("</tbody></table>");

                    html.Append("<br/><div style='text-align:center;' ><button type='button'  value='Delete' name='command' onclick='return  CheckValidationDeletePayroll()' class='btn btn-success'>Delete Payroll</button></div><br/>");



                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }

        public static StringBuilder htmlTablePayrollDetails(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {

                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");


                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        //string flag = Convert.ToString(datatable.Rows[rowId]["IsUsed"]);

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {


                                html.Append("<td>" + cell.ToString() + "</td>");




                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";



                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                }

                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }



                    html.Append("</tbody></table>");


                    html.Append("<br/><div style='text-align:center;' ><button  type='button' ' class='btn  btn-success  cancel' name='command'  value='Delete' onclick='return  CheckValidationDeletePayroll()'><span class='glyphicon glyphicon-remove'></span> Delete Payroll</button></div><br/>");


                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }




        //-------------------Work Flow Payroll  Details -------------------------

        public static StringBuilder htmlTablePayrollDetailsWorkFlow(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null, DataKeyId = null;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                string childPanelId = "childEditPanel";
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {

                            html.Append("<th></th><th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();

                                html.Append("<td>" + cell.ToString() + "</td>");


                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {

                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }
                                else if (i == 1)
                                {
                                    DataKeyId = rowData;

                                    html.Append("<td><button type='button' id='ParentRow" + rowId + "' class='btn-outline-secondary glyphicon glyphicon-plus tree' onclick='loadChildPanal(" + rowId + "," + DataKeyId + ")'   </button></td>");
                                }

                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        childPanelDivId = "childPanelDiv" + rowId;
                        html.Append("<tr id='" + childPanelId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' > </div></tr>");

                        rowId = rowId + 1;
                    }



                    // html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='button' id='btnDelete' name='btnDelete' onclick='return  CheckValidationDeletePayroll()' class='btn btn-success enabling'>Delete Payroll</button></td></tr>");
                    //html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDelete(" + Id + ")' class='btn btn-danger btn-xs'><span class='glyphicon glyphicon-remove'></span></button></td>");
                    html.Append("</tbody></table>");
                    //  html.Append("<button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnPdf'  class='btn  btn-success enabling cancel' name='command' value='Pdf'>Pdf</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnExcel' class='btn  btn-success enabling cancel' name='command' value='Excel'>Excel</button> <button  style='margin-bottom:2px;margin-right:1px;'  type='submit' id='btnWord' class='btn  btn-success enabling cancel' name='command' value='Word'>Word</button> ");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }




        //-------------------Payroll Salary Transfer-------------------------

        public static StringBuilder htmlTablePayrollSalayTransferDetails(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();

                                html.Append("<td>" + cell.ToString() + "</td>");


                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");




                                }
                                if (i == 1)
                                {
                                    //  txtName = "isCheck";

                                    txtName = "EmployeeDataList[" + rowId + "].PayrollId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }

                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit'  value='Transfer' name='command' onclick='return  CheckValidationSalaryTransfer()' class='btn btn-success enabling'>Salary Transfer</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }




        //-------------------Leave Process-------------------------

        public static StringBuilder htmlTableLeaveProcess(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {



                                if (i == 8)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmpDataList[" + rowId + "].LeaveCredit";

                                    html.Append("<td><input id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");


                                }

                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmpDataList[" + rowId + "].isRowCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmpDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }




                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i) + "'><button type='submit'  value='Update' name='command' onclick='return  CheckValidationUpdatePayroll()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Update</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }



        public static StringBuilder htmlTableLeaveCredit(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            if (!(columnHide.Contains(i)))
                            {
                                if (i == 6)
                                {

                                    rowData = cell.ToString().Trim();
                                    txtName = "EmpDataList[" + rowId + "].LeaveCredit";
                                    string idtxtName = "LeaveCredit" + rowId;
                                    html.Append("<td><input id='" + idtxtName + "' name='" + txtName + "' onchange='CheckLeaveCredit(" + rowId + ")' class='form-control' value='" + rowData + "' />  </td>");
                                }
                                else if (i == 8)
                                {
                                    rowData = cell.ToString().Trim();
                                    txtName = "EmpDataList[" + rowId + "].Notes";
                                    html.Append("<td><input id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");
                                }
                                else if (i == 9)
                                {
                                    rowData = cell.ToString().Trim();
                                    txtName = "EmpDataList[" + rowId + "].LeaveStatus";
                                    html.Append("<td><input readonly='readonly' id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");
                                }
                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }
                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmpDataList[" + rowId + "].isRowCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmpDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else
                                {
                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i) + "'><button type='submit' value='Update' name='command' onclick='return  CheckValidationUpdateLeave()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Update</button>");
                    html.Append("&nbsp;&nbsp;<button type='submit' value='Delete' name='command' onclick='return  CheckValidationDeleteLeave()' class='btn btn-success'>Delete</button></td></tr>");

                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }




        //-------------------Leave Debit Process-------------------------

        public static StringBuilder htmlTableLeaveDebitProcess(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {


                                // LeaveReason
                                if (i == 6)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmpDataList[" + rowId + "].LeaveDebit";
                                    string idtxtName = "LeaveDebit" + rowId;
                                    html.Append("<td><input id='" + idtxtName + "' name='" + txtName + "' onchange='CheckLeaveDebit(" + rowId + ")' class='form-control' value='" + rowData + "' />  </td>");


                                }
                                else if (i == 7)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmpDataList[" + rowId + "].LeaveReason";
                                    string idtxtName = "LeaveReason" + rowId;
                                    html.Append("<td><input id='" + idtxtName + "' name='" + txtName + "'  class='form-control' value='" + rowData + "' />  </td>");


                                }
                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmpDataList[" + rowId + "].isRowCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmpDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }




                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i) + "'><button type='submit'  value='Update' name='command' onclick='return  CheckValidationUpdatePayroll()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Update</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }






        //-------------------Employee Performance-------------------------

        public static StringBuilder htmlTableEmployeePerformance(DataTable datatable, int[] columnHide, string PerformanceType)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        if ((i == 7))
                        {
                            if (PerformanceType == "Yearly")
                            {
                                html.Append("<th hidden='hidden'>" + col.Caption + "</th>");

                            }
                            else
                            {
                                html.Append("<th>" + col.Caption + "</th>");
                            }

                        }

                        else if ((i == 8) || (i == 9))
                        {
                            if (PerformanceType == "Yearly")
                            {
                                html.Append("<th>" + col.Caption + "</th>");
                            }
                            else
                            {
                                html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                            }

                        }
                        else
                        {
                            html.Append("<th>" + col.Caption + "</th>");

                        }


                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }

                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {



                                if (i == 7)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmpDataList[" + rowId + "].IndividualRate";

                                    if (PerformanceType == "Yearly")
                                    {
                                        html.Append("<td hidden='hidden' ><input id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");

                                    }
                                    else
                                    {
                                        html.Append("<td><input id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");
                                    }


                                }
                                else if (i == 8)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmpDataList[" + rowId + "].UnitRate";
                                    if (PerformanceType == "Yearly")
                                    {
                                        html.Append("<td><input id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");
                                    }
                                    else
                                    {
                                        html.Append("<td hidden='hidden' ><input id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");
                                    }

                                }
                                else if (i == 9)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmpDataList[" + rowId + "].OrganizationRate";

                                    if (PerformanceType == "Yearly")
                                    {
                                        html.Append("<td><input id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");
                                    }
                                    else
                                    {
                                        html.Append("<td hidden='hidden' ><input id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");
                                    }


                                }

                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmpDataList[" + rowId + "].isRowCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmpDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }




                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i) + "'><button type='submit'  value='Update' name='command' onclick='return  CheckValidationUpdatePayroll()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Update</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }





        //-------------------Variable salary-------------------------

        public static StringBuilder htmlTablePayrollVariableSalary(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {



                                if (i == 5)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmpDataList[" + rowId + "].ItemAmount";

                                    html.Append("<td><input id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");


                                }

                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmpDataList[" + rowId + "].isRowCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmpDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }




                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i) + "'><button type='submit'  value='Update' name='command' onclick='return  CheckValidationUpdatePayroll()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Update</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }


        //Dailywages overtime

        public static StringBuilder htmlTablePayrollDailywagesOvertime(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {



                                if (i == 5)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmpDataList[" + rowId + "].ItemAmount";

                                    html.Append("<td><input id='" + txtName + "' name='" + txtName + "' class='form-control' value='" + rowData + "' />  </td>");


                                }

                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmpDataList[" + rowId + "].isRowCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmpDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }




                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i) + "'><button type='submit'  value='Update' name='command' onclick='return  CheckValidationUpdatePayroll()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Update</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }




        //------------------Loan process-------------------------

        public static StringBuilder htmlTablePayrollLoanProcess(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {



                                if (i == 5)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmpDataList[" + rowId + "].ItemAmount";

                                    html.Append("<td><input id='" + txtName + "' name='" + txtName + "' readonly='readonly' class='form-control' value='" + rowData + "' />  </td>");


                                }

                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmpDataList[" + rowId + "].isRowCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmpDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }




                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i) + "'><button type='submit'  value='Update' name='command' onclick='return  CheckValidationUpdatePayroll()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Update</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }





        //-------------------Arrear Process-------------------------

        public static StringBuilder htmlTablePayrollArrearProcess(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {

                                html.Append("<td>" + cell.ToString() + "</td>");


                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmpDataList[" + rowId + "].isRowCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmpDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }




                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }


                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i) + "'><button type='submit'  value='Update' name='command' onclick='return  CheckValidationUpdatePayroll()' class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Update</button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }






        // ------------------ Pay Slip Grid -----------------------------------

        public static StringBuilder htmlTablePaySlip(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0' ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {

                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    if (!(string.IsNullOrEmpty(rowData)))
                                    {
                                        txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                        html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                        txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                        html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                    }
                                    else
                                    {

                                        txtName = "EmployeeDataList[" + rowId + "].isCheck";
                                        html.Append("<td>  </td>");
                                        txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                        html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");

                                    }
                                }
                                else
                                {
                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='button'  value='Print' name='command' onclick='CheckValidation1()' class='btn btn-success'><span class='glyphicon glyphicon-print'></span> Print Pay Slip</button>&nbsp;&nbsp;<button type='button'  value='Send Mail'  onclick='SendMail()' class='btn btn-success'><span class='glyphicon glyphicon-envelope'></span> Send Mail</button></td></tr>");
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //------------------  CheckBox in DropDownList -------------------------
        public static StringBuilder checkBoxInDropDownList(DataTable datatable)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int i = 0;
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<label for='one' id='lbl" + Id + "'>&nbsp;&nbsp;<input type='hidden' id='hfValue" + Id + "' value='" + Id + "' />");
                                i += 1;
                            }
                            else
                            {
                                html.Append("<input type='checkbox' class='Check_Box' id='cbText" + Id + "' />&nbsp;&nbsp;" + cell.ToString() + "</label>");
                            }
                        }

                    }
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //-------------Void Leave Cycle Html Table with show column--------

        public static StringBuilder LeaveCycleVoidHtmlNestedTableEditMode(int[] columnHide, DataTable datatable)
        {
            try
            {
                string txtName = "";
                string rowData = "";
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                html.Append("<table id='dataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    Id = cell.ToString();
                                    DataKeyId = Id;
                                    txtName = "LeaveCyclingList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  id='ParentRow" + rowId + "' onchange='loadChildPanal(" + rowId + "," + DataKeyId + ")'   />  </td>");
                                    //txtName = "LeaveCyclingList[" + Id + "].isCheck";
                                    //html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  id='ParentRow" + Id + "' onchange='loadChildPanal(" + Id + "," + DataKeyId + ")'   />  </td>");
                                    txtName = "LeaveCyclingList[" + rowId + "].LeaveCycleId";
                                    //txtName = "LeaveCyclingList[" + Id + "].LeaveTypeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }

                                else
                                {
                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }

                            i = i + 1;
                        }
                        html.Append("</tr>");
                        //childPanelDivId = "childPanelDiv" + Id;
                        //html.Append("<tr id='" + childPanelDivId     + Id + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' ></div></tr>");
                        childPanelDivId = "childPanelDiv" + rowId;
                        html.Append("<tr id='" + childPanelDivId + rowId + "' style='display:none;' class='panel childrow'> <td  colspan='" + colspan + "'>  <div id='" + childPanelDivId + "' class='childRecord' ></div></tr>");
                        //html.Append("<br/><tr class='data-row' ><td style='text-align:center;' colspan='" + (i - 1) + "'><button type='submit' value='Void' name='command' onclick='return  CheckValidation()' class='btn btn-success enabling'>Void Selected Cycles</button></td></tr>");
                        html.Append("<br/><tr class='data-row' ><td style='text-align:center;' colspan='" + (i - 1) + "'>  <button type='button' id='btnVoid' name = 'btnVoid' class='btn btn-success enabling' onclick = 'return btnclick()'>Void Selected Cycles</button></td></tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //-----------void leave cycle child table ------------------------
        public static StringBuilder htmlChildTable(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null, DataKeyId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                int colspan = datatable.Columns.Count;
                string childPanelDivId = "";
                html.Append("<table id='childDataTable' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='Check_Boxes checkbox-inline'  onchange='checkBoxChangeinner()'  id='CheckedAllinnner' name='CheckedAllinnner' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            if (!(columnHide.Contains(i)))
                            {
                                rowData = cell.ToString().Trim();
                                html.Append("<td>" + cell.ToString() + "</td>");
                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    Id = cell.ToString();
                                    DataKeyId = Id;
                                    txtName = "empleavetxnList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Boxes checkbox-inline'  name='" + txtName + "'  id='ParentRowI" + rowId + "'  />  </td>");
                                    //txtName = "empleavetxnList[" + Id + "].isCheck";
                                    //html.Append("<td><input type='checkbox' class='Check_Boxes checkbox-inline'  name='" + txtName + "'  id='ParentRowI" + Id + "'  />  </td>");
                                    txtName = "empleavetxnList[" + rowId + "].EmpLeaveTxnId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else
                                {
                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }

                            i = i + 1;
                        }
                        html.Append("</tr>");
                        html.Append("<tr class='data-row'> <td style='width:70px;' >");
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static StringBuilder htmlTaxComputation(DataSet ds)
        {
            StringBuilder html = new StringBuilder();

            html.Append("<table cellpadding='0' cellspacing='0' border='1'   style='text-align: left; margin-left:10px;'>");
            html.Append("<tr>");
            html.Append("<td style='vertical-align:top;'>");
            html.Append("<table cellpadding='0' cellspacing='0' border='1'   style='text-align: left'>");
            html.Append("<th>Tax Computation</th><th>Amount</th>");
            html.Append("<tr><td>Gross Salary </td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["GrossSalary"] + "</td></tr>");
            html.Append("<tr><td>Profession Tax</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["ProfessionTax"] + "</td></tr>");

            html.Append("<tr><td>Exemptions under section 10 & 17</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["ExceptionAfterSec10_17Exp"] + "</td></tr>");

            html.Append("<tr><td><STRONG>Gross Salary after Section 10 & 17 exemptions </STRONG></td><td style='text-align:right'><STRONG>" + ds.Tables[1].Rows[0]["GrsSalAfterSec10_17Exp"] + "</STRONG></td></tr>");
            html.Append("<tr><td>Accommodation & Car Perquisites</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["AcmCarPerquisites"] + "</td></tr>");
            html.Append("<tr><td><STRONG>Income chargeable under head 'Salaries'</STRONG></td><td style='text-align:right'><STRONG>" + ds.Tables[1].Rows[0]["IncomeChargeAbleHeadSalary"] + "</STRONG></td></tr>");
            html.Append("<tr><td>Income chargeable under head 'House/Property'</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["IncomeChargeHousePropertyMax"] + "</td></tr>");
            html.Append("<tr><td>Income chargeable under head 'Capital Gains' at nominal rate</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["IncomeChargeUnderCapitalGain"] + "</td></tr>");
            html.Append("<tr><td>Income chargeable under head 'Other Sources'</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["IncomeChargeOtherSourceMax"] + "</td></tr>");
            html.Append("<tr><td><STRONG>Gross Total Income</STRONG></td><td style='text-align:right'><STRONG>" + ds.Tables[1].Rows[0]["GrossTotalIncome"] + "</STRONG></td></tr>");
            html.Append("<tr><td>Deductions under chapter VI-A</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["DeductionUnderVIMax"] + "</td></tr>");
            html.Append("<tr><td>Deductions under sec 80C</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["DeductionUnderSec80Max"] + "</td></tr>");
            html.Append("<tr><td><STRONG>Net taxable income</STRONG></td><td style='text-align:right'><STRONG>" + ds.Tables[1].Rows[0]["NetTaxableIncome"] + "</STRONG></td></tr>");

            html.Append("<tr>");
            html.Append("<td colspan='2'><table style='width:100%' cellpadding='0' cellspacing='0' border='1'><th>Tax Slabs</th><th>Tax rate</th><th>Appl Amt</th><th>Balance</th><th>Tax</th>");
            html.Append("<tr><td style='text-align:left'>" + ds.Tables[1].Rows[0]["TaxSlab1Name"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxSlab1Rate"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["AppAmountTaxSlab1"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["BalanceTaxSlab1"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxTaxSlab1"] + "</td></tr>");
            html.Append("<tr><td style='text-align:left'>" + ds.Tables[1].Rows[0]["TaxSlab2Name"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxSlab2Rate"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["AppAmountTaxSlab2"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["BalanceTaxSlab2"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxTaxSlab2"] + "</td></tr>");
            html.Append("<tr><td style='text-align:left'>" + ds.Tables[1].Rows[0]["TaxSlab3Name"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxSlab3Rate"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["AppAmountTaxSlab3"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["BalanceTaxSlab3"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxTaxSlab3"] + "</td></tr>");
            html.Append("<tr><td style='text-align:left'>" + ds.Tables[1].Rows[0]["TaxSlab4Name"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxSlab4Rate"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["AppAmountTaxSlab4"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["BalanceTaxSlab4"] + "</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxTaxSlab4"] + "</td></tr>");
            html.Append("</table></td>");
            html.Append("</tr>");

            html.Append("<tr><td>Tax Credit (Sec 87A)</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxCreditSec87A"] + "</td></tr>");
            html.Append("<tr><td>Tax on Income</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxOnIncome"] + "</td></tr>");
            html.Append("<tr><td>Capital Gains Tax (from Stocks)</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["CapitalGainsTaxFromStocks"] + "</td></tr>");
            html.Append("<tr><td>Capital Gains Tax (from Property)</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["CapitalGainsTaxFromProperty"] + "</td></tr>");
            html.Append("<tr><td>Surcharge on Income Tax</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["SurchargeOnIncomeTax"] + "</td></tr>");
            html.Append("<tr><td>Education Cess</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["EducationCess"] + "</td></tr>");
            html.Append("<tr><td><STRONG>Total Tax Liability</STRONG></td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TotalTaxLiability"] + "</td></tr>");
            html.Append("<tr><td>Total Income Tax paid from salary</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TotalIncomeTaxPaidFromSalary"] + "</td></tr>");
            html.Append("<tr><td>Tax paid outside of salary</td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["TaxPaidOutsideOfsalary"] + "</td></tr>");
            html.Append("<tr><td><STRONG>Income tax due </STRONG></td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["IncomeTaxDue"] + "</td></tr>");
            html.Append("<tr><td>Remaining months in year </td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["RemainingMonthsinYear"] + "</td></tr>");
            html.Append("<tr><td><STRONG>" + ds.Tables[4].Rows[0]["MonthTaxText"] + "</STRONG></td><td style='text-align:right'>" + ds.Tables[1].Rows[0]["BalanceTaxPayable"] + "</td></tr>");
            html.Append("</table>");
            html.Append("</td>");

            html.Append("<td>");
            html.Append("<table cellpadding='0' cellspacing='0' border='1' style='text-align: left'>");
            html.Append("<th>Exemptions under section 10&17 </th><th>Produced </th><th>Limited</th>");

            // "+ (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["trans_exemption"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["trans_exemption"]) : "0.00") + "

            html.Append("<tr><td>HRA Exemption (Sec 10 (13A))</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Hra_Exemption"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["Hra_Exemption"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["Hra_Exemption"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["Hra_Exemption"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Transport Exemption (Sec 10(14))</td><td style='text-align:right'></td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["trans_exemption"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["trans_exemption"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Other exemptions under Sec 10 (10) (gratuity, etc.) </td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["other_exemption"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["other_exemption"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["other_exemption"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["other_exemption"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Medical Bills Exemption (Sec 17(2))</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["med_bill_exemption"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["med_bill_exemption"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["med_bill_exemption"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["med_bill_exemption"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Children's Education Allowance Exemption (sec 10 (14)) </td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["child_edu_exemption"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["child_edu_exemption"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["child_edu_exemption"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["child_edu_exemption"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>LTA exemption (Sec 10(5)) </td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["lta_exemption"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["lta_exemption"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["lta_exemption"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["lta_exemption"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Uniform expenses (Sec 10(14))</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["uniform_exemption"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["uniform_exemption"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["uniform_exemption"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["uniform_exemption"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td><STRONG>Total Exempted Allowances</STRONG></td><td> </td><td style='text-align:right'><STRONG>" + (ds.Tables[1].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[1].Rows[0]["TotalExceptions"].ToString()) ? "0.00" : ds.Tables[1].Rows[0]["TotalExceptions"]) : "0.00") + "</STRONG></td></tr>");
            html.Append("<tr><td><STRONG>Other income </STRONG></td><td><STRONG>Produced </STRONG></td><td><STRONG>Limited</STRONG></td></tr>");
            html.Append("<tr><td>House/property income or loss (enter loss as negative)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["House_Property_Income"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["House_Property_Income"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["House_Property_Income"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["House_Property_Income"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Interest on housing loan (for tax exemption)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["InterestHousingLoan"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["InterestHousingLoan"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["InterestHousingLoan"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["InterestHousingLoan"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Savings Bank interest</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SavingBank_Interest"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["SavingBank_Interest"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["SavingBank_Interest"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["SavingBank_Interest"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Other income (interest, etc.)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Other_Income"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["Other_Income"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["Other_Income"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["Other_Income"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td><STRONG>Deductions under Chapter VI-A </STRONG></td><td><STRONG>Produced</STRONG></td><td><STRONG>Limited</STRONG></td></tr>");
            html.Append("<tr><td>Medical Insurance Premium (sec 80D)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Med_Insur_Premium"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["Med_Insur_Premium"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["Med_Insur_Premium"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["Med_Insur_Premium"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Medical Insurance Premium for Parents (sec 80D)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["med_insur_premium_par"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["med_insur_premium_par"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["med_insur_premium_par"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["med_insur_premium_par"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Medical for handicapped dependents (Sec 80DD) </td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Med_Handicap_Depend"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["Med_Handicap_Depend"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["Med_Handicap_Depend"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["Med_Handicap_Depend"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Medical for specified diseases (Sec 80DDB)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Med_Spec_Disease"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["Med_Spec_Disease"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["Med_Spec_Disease"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["Med_Spec_Disease"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Higher Education Loan Interest Repayment (Sec 80E) </td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["High_Edu_Loan_Repayment"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["High_Edu_Loan_Repayment"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["High_Edu_Loan_Repayment"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["High_Edu_Loan_Repayment"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Donation to approved fund and charities (sec 80G) </td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Donate_Fund_Charity"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["Donate_Fund_Charity"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["Donate_Fund_Charity"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["Donate_Fund_Charity"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Rent deduction (sec 80GG) only if HRA not received</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["rent_deduction"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["rent_deduction"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["rent_deduction"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["rent_deduction"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Savings Bank interest exemption (sec 80TTA)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["SavingBank_Interest_Exception"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["SavingBank_Interest_Exception"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["SavingBank_Interest_Exception"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["SavingBank_Interest_Exception"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Deduction for permanent disability (80U)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Permanent_Disable_Deduction"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["Permanent_Disable_Deduction"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["Permanent_Disable_Deduction"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["Permanent_Disable_Deduction"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Any other deductions</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["other_deduction"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["other_deduction"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["other_deduction"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["other_deduction"]) : "0.00") + "</td></tr>");

            html.Append("<tr><td><STRONG>Total Deductibles</STRONG></td><td> </td><td style='text-align:right'><STRONG>" + (ds.Tables[1].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[1].Rows[0]["Total_Deductions"].ToString()) ? "0.00" : ds.Tables[1].Rows[0]["Total_Deductions"]) : "0.00") + "</STRONG></td></tr>");

            html.Append("<tr><td><STRONG>Deductions under Chapter VI (sec 80C) </STRONG></td><td><STRONG>Produced </STRONG></td><td><STRONG>Limited</STRONG></td></tr>");
            html.Append("<tr><td>Pension scheme (sec 80C)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Pension_Scheme"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["Pension_Scheme"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["Pension_Scheme"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["Pension_Scheme"]) : "0.00") + "</td></tr>");
            //html.Append("<tr><td>NSC (sec 80C)</td><td></td><td></td></tr>");
            html.Append("<tr><td>NSC (sec 80C)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["NSC"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["NSC"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["NSC"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["NSC"]) : "0.00") + "</td></tr>");
            // html.Append("<tr><td>Public Provident Fund (sec 80C)</td><td>" + ds.Tables[0].Rows[0]["PPF"] + "</td><td>" + ds.Tables[0].Rows[0]["PPF"] + "</td></tr>");

            html.Append("<tr><td>Public Provident Fund (sec 80C)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PPF"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["PPF"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["PPF"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["PPF"]) : "0.00") + "</td></tr>");

            html.Append("<tr><td>Employees Provident Fund & Voluntary PF (sec 80C) </td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["PF"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["PF"]) : "0.00") + "</td><td style='text-align:right'> " + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["PF"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["PF"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Children's Education Tuition Fees (sec 80C)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["child_edu_Fund"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["child_edu_Fund"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["child_edu_fees_exemption"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["child_edu_fees_exemption"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td>Housing loan principal repayment (sec 80C)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["house_loan_principal_repay"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["house_loan_principal_repay"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["house_loan_principal_repay"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["house_loan_principal_repay"]) : "0.00") + "</td></tr>");

            html.Append("<tr><td>Insurance premium & others (MF, ULIP, FD, etc.) (sec 80C) </td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["insurance_premium"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["insurance_premium"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["insurance_premium"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["insurance_premium"]) : "0.00") + "</td></tr>");
            //html.Append("<tr><td>Long-term Infrastructure Bonds (sec 80CCF)</td><td>" + ds.Tables[0].Rows[0]["House_Property_Income"] + "</td><td>" + ds.Tables[0].Rows[0]["House_Property_Income"] + "</td></tr>");
            html.Append("<tr><td>Rajiv Gandhi Equity Savings Scheme (sec 80CCG)</td><td style='text-align:right'>" + (ds.Tables[0].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[0].Rows[0]["RajivGandhiSavingsScheme"].ToString()) ? "0.00" : ds.Tables[0].Rows[0]["RajivGandhiSavingsScheme"]) : "0.00") + "</td><td style='text-align:right'>" + (ds.Tables[4].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[4].Rows[0]["RajivGandhiSavingsScheme"].ToString()) ? "0.00" : ds.Tables[4].Rows[0]["RajivGandhiSavingsScheme"]) : "0.00") + "</td></tr>");
            html.Append("<tr><td><STRONG>Total Investments</STRONG></td><td> </td> <td style='text-align:right'><STRONG>" + (ds.Tables[1].Rows.Count > 0 ? (string.IsNullOrEmpty(ds.Tables[1].Rows[0]["Total_Investments"].ToString()) ? "0.00" : ds.Tables[1].Rows[0]["Total_Investments"]) : "0.00") + "</STRONG></td></tr>");

            //<td style='text-align:right'>" + ds.Tables[4].Rows[0]["Max_Limit_80C"] + "</td>
            //html.Append("<tr><td>Employees Provident Fund & Voluntary PF (sec 80C) </td><td>" + ds.Tables[4].Rows[0]["PF"] + "</td><td>" + ds.Tables[4].Rows[0]["PF"] + "</td></tr>");
            //html.Append("<tr><td>Children's Education Tuition Fees (sec 80C)</td><td>" + ds.Tables[4].Rows[0]["child_edu_exemption"] + "</td><td>" + ds.Tables[4].Rows[0]["child_edu_exemption"] + "</td></tr>");
            //html.Append("<tr><td>Housing loan principal repayment (sec 80C)</td><td>" + ds.Tables[4].Rows[0]["house_loan_principal_repay"] + "</td><td>" + ds.Tables[4].Rows[0]["house_loan_principal_repay"] + "</td></tr>");

            //html.Append("<tr><td>Insurance premium & others (MF, ULIP, FD, etc.) (sec 80C) </td><td>" + ds.Tables[4].Rows[0]["House_Property_Income"] + "</td><td>" + ds.Tables[4].Rows[0]["House_Property_Income"] + "</td></tr>");
            //html.Append("<tr><td>Long-term Infrastructure Bonds (sec 80CCF)</td><td>" + ds.Tables[4].Rows[0]["House_Property_Income"] + "</td><td>" + ds.Tables[4].Rows[0]["House_Property_Income"] + "</td></tr>");
            //html.Append("<tr><td>Rajiv Gandhi Equity Savings Scheme (sec 80CCG)</td><td>" + ds.Tables[4].Rows[0]["RajivGandhiSavingsScheme"] + "</td><td>" + ds.Tables[4].Rows[0]["RajivGandhiSavingsScheme"] + "</td></tr>");
            //html.Append("<tr><td><STRONG>Total Investments</STRONG></td><td>" + ds.Tables[1].Rows[0]["Total_Investments"] + "</td><td>" + ds.Tables[1].Rows[0]["Total_Investments"] + "</td></tr>");
            html.Append("</table>");
            html.Append("</td>");
            html.Append("</tr>");
            html.Append("</table");

            return html;

        }

        public static StringBuilder htmlTable(DataTable datatable, DataTable dt)
        {
            try
            {
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive' style='margin-left:5px;margin-right:5px;'   width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    html.Append("<th>" + col.Caption + "</th>");
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            html.Append("<td>" + cell.ToString() + "</td>");
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in dt.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            html.Append("<td>" + cell.ToString() + "</td>");
                            i = i + 1;
                        }
                        html.Append("</tr>");
                    }
                    html.Append("</tbody></table>");
                }
                return html;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static StringBuilder htmlTableAll(DataTable datatable)
        {
            try
            {
                StringBuilder html = new StringBuilder();
                int i = 0;

                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive' style='margin-left:5px;margin-right:5px;'   width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    html.Append("<th>" + col.Caption + "</th>");
                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");
                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            html.Append("<td>" + cell.ToString() + "</td>");
                            i = i + 1;
                        }
                        html.Append("</tr>");
                    }
                }
                html.Append("</tbody></table>");
                return html;
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static bool IsInteger(string str)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsStringValue(string str)
        {
            Regex regex = new Regex(@"^\w+$");
            try
            {
                if (String.IsNullOrEmpty(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsStringWithSpace(string str)
        {
            Regex regex = new Regex("^[a-zA-Z ]*$");
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsAlphaNumeric(string str)
        {
            Regex regex = new Regex("^[a-zA-Z0-9]*$");
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsCharValue(string str)
        {
            Regex regex = new Regex(@"^[mMfF]$");
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool IsDate(string str)
        {
            Regex ddmmyyyy = new Regex(@"^(0[1-9]|1[0-9]|2[0-9]|3[0-1])/(0[1-9]|1[0-2])/([1-4][0-9][0-9][0-9])$");
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!ddmmyyyy.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static DataTable DataTableColumnRemove(DataTable dt, int[] HideColumn)
        {
            DataTable dtClone = dt.Copy();
            foreach (DataColumn dc in dtClone.Columns)
            {
                string cname = dc.ColumnName;
                int columnIndex = dtClone.Columns[cname].Ordinal;
                if ((HideColumn.Contains(columnIndex)))
                {
                    dt.Columns.Remove(cname);
                }
            }
            return dt;


        }

        public static DataTable DataTableColumnRemoveButNothidecolumn(DataTable dt)
        {
            DataTable dtClone = dt.Copy();
            foreach (DataColumn dc in dtClone.Columns)
            {
                string cname = dc.ColumnName;
                int columnIndex = dtClone.Columns[cname].Ordinal;
                //if ((HideColumn.Contains(columnIndex)))
                //{
                //    dt.Columns.Remove(cname);
                //}
            }
            return dt;
        }

        //------------------DB objects(Column /Sp /Fucntion) Description --------------


        public static StringBuilder htmlTableDbObjectDescription(DataTable datatable)
        {
            try
            {
                StringBuilder html = new StringBuilder();
                int i = 0;
                int x = 0;
                string txtName = "";
                string rowData = "";

                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive' width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {
                    html.Append("<th>" + col.Caption + "</th>");
                }
                html.Append("</tr></thead><tbody>");
                for (i = 0; i < datatable.Rows.Count; i++)
                {
                    html.Append("<tr class='data-row'>");

                    for (x = 0; x < datatable.Columns.Count; x++)
                    {
                        rowData = Convert.ToString(datatable.Rows[i][x]);
                        if (x == 0)
                        {
                            txtName = "TableCoulmnList[" + i + "].SchemaName";
                            html.Append("<td ><input id='" + txtName + "' name='" + txtName + "' readonly='readonly' class='form-control' value='" + rowData + "' /> </td>");

                        }
                        else if (x == 1)
                        {
                            txtName = "TableCoulmnList[" + i + "].TableName";
                            html.Append("<td ><input id='" + txtName + "' name='" + txtName + "' readonly='readonly' class='form-control' value='" + rowData + "' /> </td>");

                        }
                        else if (x == 2)
                        {
                            txtName = "TableCoulmnList[" + i + "].ColumnName";
                            html.Append("<td ><input id='" + txtName + "' name='" + txtName + "' readonly='readonly' class='form-control' value='" + rowData + "' /> </td>");
                        }
                        else if (x == 3)
                        {
                            txtName = "TableCoulmnList[" + i + "].ColumnDesc";
                            html.Append("<td ><input id='" + txtName + "' name='" + txtName + "' maxlengh='500' class='form-control' value='" + rowData + "' /> </td>");
                        }
                    }
                    html.Append("</tr>");
                }
                html.Append("<br/><tr><td style='text-align:center;' colspan='" + (x) + "'><button type='submit' id='Update' value='Update'  name='command'  class='btn btn-success '><span class='glyphicon glyphicon-pencil'></span> Update</button></td></tr>");
                html.Append("</tbody></table>");
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        //-------------------Work Flow Process-------------------------

        public static StringBuilder htmlTableWorkFlowProcess(DataTable datatable, int[] columnHide)
        {
            try
            {
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string App_Key = null;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='dataTableSearch' class='display table table-striped table-bordered table-hover table-responsive' style='width:105%;'    cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Boxx'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            //  html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        App_Key = Convert.ToString(datatable.Rows[rowId][0]);
                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = Convert.ToString(cell);

                                if (i == 1)
                                {
                                    html.Append("<td style='text-align:center;'><a onClick=WorkFlowDocument('PayrollDetails','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-list-alt'></span></a></td>");
                                }
                                else if (i == 2)
                                {
                                    html.Append("<td style='text-align:center;'><a  onClick=WorkFlowDocument('Notes','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-list-alt'></span></a></td>");
                                }
                                else if (i == 3)
                                {
                                    html.Append("<td style='text-align:center;'><a  onClick=WorkFlowDocument('LogDetails','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-list-alt'></span></a></td>");
                                }
                                else if (i == 4)
                                {
                                    html.Append("<td style='text-align:center;'><a  onClick=WorkFlowDocument('DocDownload','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-download'></span></a></td>");
                                }
                                else if (i == 5)
                                {
                                    rowData = Convert.ToString(cell);

                                    txtName = "WorkflowDataList[" + rowId + "].EmpCode";


                                    html.Append("<td style='width:60px;'><input id='" + txtName + "' name='" + txtName + "' readonly='readonly'    class='form-control' value='" + rowData + "' />  </td>");

                                }
                                else
                                {
                                    html.Append("<td>" + rowData + "</td>");
                                }
                            }
                            else
                            {
                                rowData = Convert.ToString(cell);
                                if (i == 0)
                                {
                                    txtName = "WorkflowDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />");
                                    txtName = "WorkflowDataList[" + rowId + "].AppKey";
                                    html.Append("<input type='text' hidden='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else
                                {
                                    html.Append("<td hidden='hidden'>" + rowData + "</td>");
                                }
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //-------------------Work Flow Reimbursement Process-------------------------

        public static StringBuilder htmlTableWorkFlowReimbursementProcess(DataTable datatable, int[] columnHide)
        {
            try
            {
                string txtId = string.Empty;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string App_Key = null;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='dataTableSearch' class='display table table-striped table-bordered table-hover table-responsive' style='width:105%;'    cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Boxx'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            //  html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        App_Key = Convert.ToString(datatable.Rows[rowId][0]);
                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = Convert.ToString(cell);

                                if (i == 1)
                                {
                                    html.Append("<td style='text-align:center;'><a onClick=WorkFlowDocument('PayrollDetails','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-list-alt'></span></a></td>");
                                }
                                else if (i == 2)
                                {
                                    html.Append("<td style='text-align:center;'><a  onClick=WorkFlowDocument('Notes','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-list-alt'></span></a></td>");
                                }
                                else if (i == 3)
                                {
                                    html.Append("<td style='text-align:center;'><a  onClick=WorkFlowDocument('LogDetails','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-list-alt'></span></a></td>");
                                }
                                else if (i == 4)
                                {
                                    html.Append("<td style='text-align:center;'><a  onClick=WorkFlowDocument('DocDownload','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-download'></span></a></td>");
                                }
                                else if (i == 5)
                                {
                                    rowData = Convert.ToString(cell);

                                    txtName = "WorkflowDataList[" + rowId + "].EmpCode";


                                    html.Append("<td style='width:60px;'><input id='" + txtName + "' name='" + txtName + "' readonly='readonly'    class='form-control' value='" + rowData + "' />  </td>");

                                }
                                else if (i == 10)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "WorkflowDataList[" + rowId + "].TotalAmount";
                                    txtId = "TotalAmount" + rowId;

                                    html.Append("<td><input id='" + txtId + "' name='" + txtName + "' readonly='readonly'    class='form-control' value='" + rowData + "' />  </td>");


                                }
                                else if (i == 12)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "WorkflowDataList[" + rowId + "].Amount";
                                    txtId = "Amount" + rowId;
                                    html.Append("<td><input id='" + txtId + "' name='" + txtName + "'  onchange='CheckAmount(" + rowId + ")'  class='form-control' value='" + rowData + "' /> ");
                                    txtName = "WorkflowDataList[" + rowId + "].BalanceAmount";
                                    txtId = "BalanceAmount" + rowId;
                                    html.Append("<input type='hidden' id='" + txtId + "' name='" + txtName + "'   class='form-control' value='" + rowData + "' />  </td>");

                                }

                                else
                                {
                                    html.Append("<td>" + rowData + "</td>");
                                }
                            }
                            else
                            {
                                rowData = Convert.ToString(cell);
                                if (i == 0)
                                {
                                    txtName = "WorkflowDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />");
                                    txtName = "WorkflowDataList[" + rowId + "].AppKey";
                                    html.Append("<input type='text' hidden='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }
                                else if (i == 17)
                                {
                                    rowData = Convert.ToString(cell);

                                    txtName = "WorkflowDataList[" + rowId + "].EmpReimbursementId";
                                    txtId = "Amount" + rowId;
                                    html.Append("<td hidden='hidden'><input id='" + txtId + "' name='" + txtName + "'    class='form-control' value='" + rowData + "' />  </td>");


                                }
                                else
                                {
                                    html.Append("<td hidden='hidden'>" + rowData + "</td>");
                                }
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        //-------------------Work Flow Variable salary Process-------------------------

        public static StringBuilder htmlTableWorkFlowVariableSalaryProcess(DataTable datatable, int[] columnHide)
        {
            try
            {
                string txtId = string.Empty;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string App_Key = null;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='dataTableSearch' class='display table table-striped table-bordered table-hover table-responsive' style='width:105%;'    cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {
                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Boxx'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            //  html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        App_Key = Convert.ToString(datatable.Rows[rowId][0]);
                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {


                            if (!(columnHide.Contains(i)))
                            {
                                rowData = Convert.ToString(cell);

                                if (i == 1)
                                {
                                    html.Append("<td style='text-align:center;'><a onClick=WorkFlowDocument('PayrollDetails','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-list-alt'></span></a></td>");
                                }
                                else if (i == 2)
                                {
                                    html.Append("<td style='text-align:center;'><a  onClick=WorkFlowDocument('Notes','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-list-alt'></span></a></td>");
                                }
                                else if (i == 3)
                                {
                                    html.Append("<td style='text-align:center;'><a  onClick=WorkFlowDocument('LogDetails','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-list-alt'></span></a></td>");
                                }
                                else if (i == 4)
                                {
                                    html.Append("<td style='text-align:center;'><a  onClick=WorkFlowDocument('DocDownload','" + App_Key + "') class='btn-outline-secondary'><span class='glyphicon glyphicon-download'></span></a></td>");
                                }
                                else if (i == 5)
                                {
                                    rowData = Convert.ToString(cell);

                                    txtName = "WorkflowDataList[" + rowId + "].EmpCode";


                                    html.Append("<td style='width:60px;'><input id='" + txtName + "' name='" + txtName + "' readonly='readonly'    class='form-control' value='" + rowData + "' />  </td>");

                                }

                                else if (i == 9)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "WorkflowDataList[" + rowId + "].Amount";
                                    txtId = "Amount" + rowId;
                                    html.Append("<td><input id='" + txtId + "' name='" + txtName + "'  readonly='readonly'   class='form-control' value='" + rowData + "' /> ");


                                }

                                else
                                {
                                    html.Append("<td>" + rowData + "</td>");
                                }
                            }
                            else
                            {
                                rowData = Convert.ToString(cell);
                                if (i == 0)
                                {
                                    txtName = "WorkflowDataList[" + rowId + "].isCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />");
                                    txtName = "WorkflowDataList[" + rowId + "].AppKey";
                                    html.Append("<input type='text' hidden='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");
                                }

                                else
                                {
                                    html.Append("<td hidden='hidden'>" + rowData + "</td>");
                                }
                            }
                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    html.Append("</tbody></table>");
                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }






        //-------------------Reimbursement Entry-------------------------

        public static StringBuilder htmlTableReimbursementEntry(DataTable datatable, int[] columnHide)
        {
            try
            {
                string txtId = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;
                string txtName = "";
                string rowData = "";
                html.Append("<table id='data_Table' class='display table table-striped table-bordered table-hover table-responsive'    width='100%' cellspacing='0'  ><thead><tr>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {

                        html.Append("<th>" + col.Caption + "</th>");

                    }
                    else
                    {
                        if (i == 0)
                        {

                            html.Append("<th><input type='checkbox' class='checkbox-inline Check_Box'  onchange='checkBoxChange()' id='CheckedAll' name='CheckedAll' /></th>");
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");

                        }
                        else
                        {
                            html.Append("<th hidden='hidden'>" + col.Caption + "</th>");
                        }
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;

                        html.Append("<tr class='data-row'>");
                        foreach (var cell in row.ItemArray)
                        {
                            txtId = null;
                            txtName = null;
                            if (!(columnHide.Contains(i)))
                            {



                                if (i == 7)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmployeeDataList[" + rowId + "].TotalAmount";
                                    txtId = "TotalAmount" + rowId;

                                    html.Append("<td><input id='" + txtId + "' name='" + txtName + "' readonly='readonly'    class='form-control' value='" + rowData + "' />  </td>");


                                }
                                else if (i == 9)
                                {
                                    rowData = cell.ToString().Trim();

                                    txtName = "EmployeeDataList[" + rowId + "].Amount";
                                    txtId = "Amount" + rowId;
                                    html.Append("<td><input id='" + txtId + "' name='" + txtName + "'  onchange='CheckAmount(" + rowId + ")'  class='form-control' value='" + rowData + "' />  </td>");


                                }
                                else
                                {
                                    html.Append("<td>" + cell.ToString() + "</td>");
                                }

                            }
                            else
                            {
                                rowData = cell.ToString().Trim();
                                if (i == 0)
                                {
                                    //  txtName = "isCheck";
                                    txtName = "EmployeeDataList[" + rowId + "].isRowCheck";
                                    html.Append("<td><input type='checkbox' class='Check_Box checkbox-inline'  name='" + txtName + "'  />  </td>");
                                    txtName = "EmployeeDataList[" + rowId + "].EmpReimbursementId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }

                                else if (i == 1)
                                {
                                    //  txtName = "isCheck";

                                    txtName = "EmployeeDataList[" + rowId + "].EmployeeId";
                                    html.Append("<td hidden='hidden'><input id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' /> </td>");



                                }



                                else
                                {

                                    html.Append("<td hidden='hidden'>" + cell.ToString() + "</td>");
                                }
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }
                    // onclick='return CheckValidation()'

                    html.Append("<br/><tr><td style='text-align:center;' colspan='" + (i) + "'><button type='button'  onclick='CheckValidation()'  value='Update' name='command' id='btnInsert'  class='btn btn-success '><span class='glyphicon glyphicon-picture'></span> Save</ button></td></tr>");
                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {

                return null;

            }

        }




        public static StringBuilder htmlTableEditModeSameForm(DataTable datatable, int[] columnHide)
        {
            try
            {
                string Id = null;
                StringBuilder html = new StringBuilder();
                int rowId = 0;
                int i = 0;

                html.Append("<table id='dataTableForm' class='display table table-striped table-bordered table-hover table-responsive table-condensed' ><thead><tr><th  class='exclude' >Edit</th>");
                foreach (System.Data.DataColumn col in datatable.Columns)
                {

                    if (!(columnHide.Contains(i)))
                    {
                        html.Append("<th>" + col.Caption + "</th>");
                    }
                    else
                    {
                        html.Append("<th  class='exclude'  hidden='hidden'>" + col.Caption + "</th>");
                    }



                    i = i + 1;
                }
                html.Append("</tr></thead><tbody>");


                if (datatable.Rows.Count > 0)
                {
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;


                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                Id = cell.ToString();
                                html.Append("<tr class='data-row'> <td  class='exclude'  style='width:60px;' ><button title='Edit Record' style='margin-left:1px;margin-right:1px;' type='button' onclick='getRowValForm(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-edit'></span></button>");

                                html.Append("<button type='button' title='Delete Record' style='margin-left:1px;margin-right:1px;'  onclick='RowDeleteForm(" + Id + ")' class='btn-outline-secondary'><span class='fa fa-trash'></span></button></td>");
                            }


                            if (!(columnHide.Contains(i)))
                            {
                                html.Append("<td>" + cell.ToString() + "</td>");

                            }
                            else
                            {
                                html.Append("<td  class='exclude'  hidden='hidden'>" + cell.ToString() + "</td>");
                            }



                            i = i + 1;
                        }
                        html.Append("</tr>");
                        rowId = rowId + 1;
                    }

                    html.Append("</tbody></table>");




                    return html;
                }
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }





    }

    public class DropDownList
    {

        public int? ListValue { get; set; }
        public string ListText { get; set; }
        public string IsCheck { get; set; }
        public List<DropDownList> SelectList { get; set; }

        //attndanc

        //------------------  CheckBox in DropDownList -------------------------
        public static StringBuilder DropDownListWithCheckBox(DataTable datatable)
        {
            try
            {
                int rowId = 0;
                string txtName = string.Empty;
                string rowData = null;
                StringBuilder html = new StringBuilder();
                int i = 0;
                html.Append("<ul style='max-height: 500px; overflow: auto;' class='nav nav-pills'>  <li class='dropdown'>  <a href='#' data-toggle='dropdown' class='dropdown'>--Select--<b class='caret'></b></a>  <ul style='max-height: 500px; overflow: auto;' class='dropdown-menu'>");
                if (datatable.Rows.Count > 0)
                {

                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                /* <ul class="nav nav-pills" >

                            <li class="dropdown">
                              <a href="#" data-toggle="dropdown" class="dropdown-toggle">--Select--<b class="caret"></b></a>
                               <ul class="dropdown-menu">
                                  <li style="width:200px;"><input type="checkbox"/>hello</li>
                                 <li style="width:200px;"><input type="checkbox"/><input type="text"  />hello</li>

                           </ul> </li>                   
                   </ul>*/
                                rowData = cell.ToString();

                                txtName = "SelectList[" + rowId + "].IsCheck";
                                html.Append("<li><input type='checkbox' class='Check_Box' id='" + txtName + "' name='" + txtName + "' />&nbsp;&nbsp;");

                                txtName = "SelectList[" + rowId + "].ListValue";
                                html.Append("<input type='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' />");



                                i += 1;
                            }
                            else
                            {
                                rowData = cell.ToString();
                                txtName = "SelectList[" + rowId + "].ListText";

                                html.Append("&nbsp;<input type='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' />" + rowData + "</li>");
                            }
                        }

                    }

                }

                html.Append("</ul></li></ul>");
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }



        }



        //------------------  CheckBox in DropDownList -------------------------
        public static StringBuilder EmployeeDropDownListWithCheckBox(DataTable datatable)
        {
            try
            {
                int rowId = 0;
                string txtName = string.Empty;
                string rowData = null;

                StringBuilder html = new StringBuilder();
                int i = 0;
                html.Append("<ul  class='nav nav-pills' style='width:200px;'>  <li class='dropdown' style='width:200px;'>  <a  href='#' data-toggle='dropdown' class='dropdown form-control'>--Select--<b class='caret'></b></a>  <ul style='max-height: 500px; overflow: auto;' class='dropdown-menu'>");
                if (datatable.Rows.Count > 0)
                {
                    html.Append("<li ><input type='checkbox' onchange='checkBoxChangeDropDown()' class='CheckBoxList' id='CheckedAllCheckBox' name='CheckedAllCheckBox' />&nbsp;&nbsp;");
                    html.Append("<input type='hidden' id='hfAll' name='hfAll' value='Select All' />Select All </li>");
                    foreach (System.Data.DataRow row in datatable.Rows)
                    {
                        i = 0;
                        foreach (var cell in row.ItemArray)
                        {
                            if (i == 0)
                            {
                                /* <ul class="nav nav-pills" >

                            <li class="dropdown">
                              <a href="#" data-toggle="dropdown" class="dropdown-toggle">--Select--<b class="caret"></b></a>
                               <ul class="dropdown-menu">
                                  <li style="width:200px;"><input type="checkbox"/>hello</li>
                                 <li style="width:200px;"><input type="checkbox"/><input type="text"  />hello</li>

                           </ul> </li>                   
                   </ul>*/

                                rowData = cell.ToString();
                                txtName = "SelectList[" + rowId + "].Is_Check";
                                if (rowId == 0)
                                {
                                    html.Append("<li ><input type='checkbox' class='CheckBoxList' id='" + txtName + "' name='" + txtName + "' />&nbsp;&nbsp;");
                                }
                                else
                                {
                                    html.Append("<li ><input type='checkbox'  class='CheckBoxList' id='" + txtName + "' name='" + txtName + "' />&nbsp;&nbsp;");
                                }

                                txtName = "SelectList[" + rowId + "].ListValue";
                                html.Append("<input type='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' />");

                            }
                            else
                            {
                                if (i == 1)
                                {
                                    rowData = cell.ToString();
                                    txtName = "SelectList[" + rowId + "].ListText";
                                    html.Append("&nbsp;<input type='hidden' id='" + txtName + "' name='" + txtName + "' value='" + rowData + "' />" + rowData + "</li>");

                                }
                                else
                                {

                                }
                            }
                            i += 1;
                        }
                        rowId = rowId + 1;

                    }

                }

                html.Append("</ul></li></ul>");
                return html;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        Regex regex = new Regex(@"^[0-9]+$");
        private bool IsInteger(string str)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(str))
                {
                    return false;
                }
                if (!regex.IsMatch(str))
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



















    }







}
