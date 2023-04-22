using System;
using System.Collections.Generic;
using System.Text;

namespace ITHeart.BL
{
    public static class spNames
    {
        #region " Generic "
        /// Execute Any Query ////
        public static string ExecuteQry = "ExecuteQry";

        /// Fetch Any Data ////
        public static string SP_GetData = "SP_GetData";

        /// Retrive Or Update Info ////
        public static string sp_RetriveInfo = "RetriveInfo";
        public static string sp_UpdateInfo = "UpdateInfo";


        /// Generates Primary Key
        public static string Generate_A_New_Primary_Key = "Generate_NewPrimaryKey";

        /// Fetch Image in Binary format ////
        public static string Load_BinaryImage = "Load_BinaryImage";

        /// Config Date and Time
        public static string Update_UserDateTimeConfig = "Update_UserDateTimeConfig";
        public static string Load_AddtionalFields = "Load_AddtionalFields";
        public static string SaveAdditionalMasterValues = "AdditionalMasterValues_Save";

        /// UDPConfig
        public static string UDPConfig_Load = "UDPConfig_Load";
        public static string UDPConfig_Insert_Update_Delete = "UDPConfig_Insert_Update_Delete";

        /// UDM Master Config
        public static string Load_UDMMasterDataforMaster = "Load_UDMMasterDataforMaster";
        public static string Load_UDMMaster_Objects = "Load_UDMMasterObjs";

        public static string UDMMaster_Load = "UDMMaster_Load";
        public static string UDMMaster_Insert_Update_Delete = "UDMMaster_Insert_Update_Delete";

        public static string UDMMasterConfig_Load = "UDMMasterConfig_Load";
        public static string UDMMasterConfig_Insert_Update_Delete = "UDMMasterConfig_Insert_Update_Delete";
        public static string UDMMasterData_Insert_Update = "UDMMasterData_Insert_Update";

        #endregion

        #region " Selection Criteria & Helps "
        /// Selection Criteria
        public static string Load_SelectionCriteriaDef_Objects = "Load_SysSelectionCriteriaObjs";

        /// Help Details
        public static string Load_HelpConfig = "Load_HelpConfig";
        public static string Load_Text_For_Help_User_Control = "Load_HelpUserControlText";

        /// Save User's Selection Criteria
        public static string Save_UserReportSettings = "UserReportSettings_Save";
        public static string Delete_UserReportSettings = "UserReportSettings_Delete";
        #endregion


        #region " General Definition "
        /// Load Help fo General Definition
        public static string Help_sysGenDef = "Help_sysGenDef";

        /// Manage General Master
        public static string Load_GenMstforMaster = "Load_GenMstforMaster";

        public static string Load_GenDef_Objects = "Load_SysGenObjs";
        public static string Load_GenMstBasedOnGenID = "GenMst_Load";
        public static string Load_UDMMasterDataBasedOnGenID = "UDMMasterData_Load";

        public static string Add_General_Master = "GenMst_Insert";
        public static string Update_General_Master = "GenMst_Update";
        public static string Upload_Data_General_Master = "GenMst_UploadData";
        public static string Delete_General_Master = "GenMst_Delete";
        public static string Delete_UDM_Master_Data = "UDMMasterData_Delete";
        public static string Change_Status_General_Master = "GenMst_Status";
        public static string Load_GmMstList = "Load_GmMstList";
        public static string GenMaster_Load = "GenMaster_Load";
        public static string GenMasterAll_Load = "GenMasterAll_Load";
        public static string Load_LoginUser = "Load_LoginUser";
        #endregion

        #region " Company Master "
        /// Manage  Company Master
        /// 


        public static string Load_Company_MasterNav = "CompanyMaster_LoadAll";
        public static string Load_Company_Master = "Load_CompanyMaster";
        public static string GetCustomCaption = "GetCustomCaption";

        public static string Add_Company_Master = "CompanyMaster_Insert";
        public static string Update_Company_Master = "CompanyMaster_Update";
        public static string Upload_Data_Company_Master = "CompanyMaster_UploadData";
        public static string Check_Company_Name = "Check_CompanyMasterName";
        public static string Delete_Company_Master = "CompanyMaster_Delete";
        public static string Change_Status_Company_Master = "CompanyMaster_Status";

        #endregion

        #region 
        ///Employee
        /// 
        public static string Insert_Employee_forms = "Employee_Insert";
        public static string Load_Employee_forms = "Employee_Load";
        public static string Load_Employee_forms_by_id = "Employee_Load_By_Id";
        public static string Delete_Employee_forms = "Delete_Employee";

        public static string get_EmployeeFamilyDetail = "get_EmployeeFamilyDetail";
        public static string get_EmployeeEmployementDetail = "get_EmployeeEmployementDetail";
        public static string get_EmployeeAcademicDetail = "get_EmployeeAcademicDetail";
        public static string Insert_Update_Employee_FamilyDetail = "Insert_Update_Employee_FamilyDetail";
        public static string Insert_Update_Employee_Employment = "Insert_Update_Employee_Employment";
        public static string Insert_Update_Employee_AcademicDetail = "Insert_Update_Employee_AcademicDetail";   
        #endregion

        #region
        public static string Insert_Update_Salary_Template = "Insert_Update_Salary_Template";
        public static string Insert_Update_Salary_Template_Itm = "Insert_Update_Salary_Template_Itm";
        public static string salary_template_Load = "salary_template_Load";
        public static string Delete_Salary_Template_forms = "Delete_Salary_Template";
        public static string Load_Salary_Template_forms_by_id = "Salary_Template_Load_By_Id";
        public static string Employee_Salary_Insert = "Employee_Salary_Insert";
        public static string Employee_SalaryItem_Insert = "Employee_SalaryItem_Insert";
        public static string get_EmployeeSalaryDetail = "get_EmployeeSalaryDetail";

        //Attndance
        //public static string Insert_Employee_forms = "Employee_Insert";
        //public static string Load_Employee_forms = "Employee_Load";
        public static string Load_att_by_date = "GET_ATTENDANCEREPORT";
        public static string Monthly_Attendance_Entry_Get = "MonthlyAttendanceEntryGet";
        public static string MaleFemaleGrah = "MaleFemaleGrah";
        public static string emp_daily_attendance_get = "emp_daily_attendance_get1";
        
        //public static string Delete_Employee_forms = "Delete_Employee";


        //Leave        
        public static string Get_Leave_Request = "LEAVEAPPLYREQUEST";

        //Company        
        public static string Get_Company = "Get_Company";
        public static string Get_Company_by_id = "Get_Company_by_id";
        public static string Save_Company_Details = "Save_Company_Details";
        public static string Update_Company_Details = "Update_Company_Details";
        public static string Delete_Company_Details = "Delete_Company_Details";

        //cvountry state
        public static string getCountry = "getCountry";
        public static string getState = "getState";
        
            
        #endregion
    }
}
