using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.UI;
using System.DataType;


namespace ITHeart.BL
{
    public class GenMstBL : Common
    {

        #region " GenMst Add, Delete, save Enable-Disable Function"

        public string GenMasterRecord_Delete(string PrimaryID, string CommunityID)
        {


            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam.Add("@ID", ArrayLists.ToArrayList("@ID", PrimaryID, SqlDbType.Char));
            spParam.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", CommunityID, SqlDbType.Char));
            DataSet ds = ExecuteData(spNames.Delete_General_Master, spParam, false, false, true);

            // --- If Load has Error --------------------------------------------

            if (ds == null || _Status != 0 || _Message != "")
            {
                _Message = "Unable to delete record!";
                return _Message;
            }
            else
                _Message = "";

            return _Message;
        

        }

        public string GenMasterRecord_StatusChanged(string PrimaryID, int NewStatus, string CommunityID)
        {
            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();
            spParam.Add("@ID", ArrayLists.ToArrayList("@ID", PrimaryID, SqlDbType.Char));
            spParam.Add("@NewStatus", ArrayLists.ToArrayList("@NewStatus", NewStatus, SqlDbType.Bit));
            spParam.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", CommunityID, SqlDbType.Char));
            DataSet ds = ExecuteData(spNames.Change_Status_General_Master, spParam, false, false, true);

            // --- If Load has Error --------------------------------------------
            if (ds == null || _Status != 0 || _Message != "")
            {   _Message = "Unable to change status";
                return _Message;
            }
            else
                _Message = "";
            return _Message;

        }
      
        public DataSet Load_GenMstforMaster(string primaryId)
        {
            Dictionary<string, ArrayList> sp_Param = new Dictionary<string, ArrayList>();
            sp_Param.Add("@pGenRowID", ArrayLists.ToArrayList("@pGenRowID", primaryId, SqlDbType.VarChar));
          
            DataSet ds = base.ExecuteData(spNames.Load_GenMstforMaster, sp_Param, false, false, true);
            if (!string.IsNullOrEmpty(base.Message) || ds == null)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds;
       
        }

        public DataSet Load_GenMstDetailsBasedOnGenID(string genID,string communityID)
        {
            Dictionary<string, ArrayList> sp_Param = new Dictionary<string, ArrayList>();
            sp_Param.Add("@GenID", ArrayLists.ToArrayList("@GenID", genID, SqlDbType.VarChar));
            sp_Param.Add("@CommunityID", ArrayLists.ToArrayList("@CommunityID", communityID, SqlDbType.VarChar));

            DataSet ds = base.ExecuteData(spNames.Load_GenMstBasedOnGenID, sp_Param, false, false, true);
            if (!string.IsNullOrEmpty(base.Message) || ds == null || ds.Tables.Count < 2)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds;

        }

        public DataTable Load_sysGenDef(string _OptionID,string UserId)
        {
            Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
          
            aList.Add("@OptionID", ArrayLists.ToArrayList("@OptionID", _OptionID, SqlDbType.VarChar));
            aList.Add("@UserID", ArrayLists.ToArrayList("@UserID", UserId, SqlDbType.VarChar));
            //aList.Add("@EditionID", ArrayLists.ToArrayList("@EditionID", Community.GetLicenseValidationValue("15"), SqlDbType.Int));

            DataSet ds = base.ExecuteData(spNames.Help_sysGenDef, aList, false, false, true);
            if (!string.IsNullOrEmpty(base.Message) || ds == null)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds.Tables[0];

        }
        public DataTable Load_GmMstList(string UserId)
        {
            Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
            aList.Add("@UserID", ArrayLists.ToArrayList("@UserID", UserId, SqlDbType.VarChar));

            DataSet ds = base.ExecuteData(spNames.Load_GmMstList, aList, false, false, true);
            if (!string.IsNullOrEmpty(base.Message) || ds == null)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds.Tables[0];

        }
        public DataTable Load_GmMst(string GenId)
        {
            Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
            aList.Add("@GenID", ArrayLists.ToArrayList("@GenID", GenId, SqlDbType.VarChar));

            DataSet ds = base.ExecuteData(spNames.GenMaster_Load, aList, false, false, true);
            if (!string.IsNullOrEmpty(base.Message) || ds == null)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds.Tables[0];

        }

        public DataTable Load_LoginUser(string UserId,string LoginId,string Password)
        {
            Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
            aList.Add("@LoginID", ArrayLists.ToArrayList("@LoginID", LoginId, SqlDbType.VarChar));
            aList.Add("@Password", ArrayLists.ToArrayList("@Password", Password, SqlDbType.VarChar));
            aList.Add("@UserID", ArrayLists.ToArrayList("@UserID", UserId, SqlDbType.VarChar));

            DataSet ds = base.ExecuteData(spNames.Load_LoginUser, aList, false, false, true);
            if (!string.IsNullOrEmpty(base.Message) || ds == null)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds.Tables[1];

        }
        #endregion

        public DataTable Load_Employee()
        {
         
            DataSet ds = base.ExecuteData(spNames.Load_Employee_forms);
            if (!string.IsNullOrEmpty(base.Message) || ds == null)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds.Tables[0];

        }
        public DataTable Load_Country()
        {

            DataSet ds = base.ExecuteData(spNames.getCountry);
            if (!string.IsNullOrEmpty(base.Message) || ds == null)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds.Tables[0];

        }
        public DataTable Load_State()
        {

            DataSet ds = base.ExecuteData(spNames.getState);
            if (!string.IsNullOrEmpty(base.Message) || ds == null)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds.Tables[0];

        }


        public void Insert_Employee()
        {
            Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
            aList.Add("@Code", ArrayLists.ToArrayList("@Code", "test", SqlDbType.VarChar));
            aList.Add("@Name", ArrayLists.ToArrayList("@Name", "test", SqlDbType.VarChar));
            aList.Add("@Gender", ArrayLists.ToArrayList("@Gender", 1, SqlDbType.Int));
            aList.Add("@Doj", ArrayLists.ToArrayList("@Doj", DateTime.Now, SqlDbType.DateTime));
            aList.Add("@MritlStatus", ArrayLists.ToArrayList("@MritlStatus", 1, SqlDbType.Int));
            aList.Add("@Nationality", ArrayLists.ToArrayList("@Nationality", 1, SqlDbType.Int));
            aList.Add("@FatherName", ArrayLists.ToArrayList("@FatherName", "test", SqlDbType.VarChar));
            aList.Add("@MotherName", ArrayLists.ToArrayList("@MotherName", "Test", SqlDbType.VarChar));
            aList.Add("@dateOfRel", ArrayLists.ToArrayList("@dateOfRel", DateTime.Now, SqlDbType.DateTime));
            aList.Add("@Dob", ArrayLists.ToArrayList("@Dob", DateTime.Now, SqlDbType.DateTime));
            aList.Add("@HighQual", ArrayLists.ToArrayList("@HighQual", 1, SqlDbType.Int));
            aList.Add("@Dor", ArrayLists.ToArrayList("@Dor", DateTime.Now, SqlDbType.DateTime));
            base.ExecuteData(spNames.Insert_Employee_forms, aList, false, false, true);
        }
        public void Delete_Employee(int id)
        {
            Dictionary<string, ArrayList> aList = new Dictionary<string, ArrayList>();
            aList.Add("@EmployeeId", ArrayLists.ToArrayList("@EmployeeId", id, SqlDbType.Int));
            base.ExecuteData(spNames.Delete_Employee_forms, aList, false, false, true);
        }
    }
}
