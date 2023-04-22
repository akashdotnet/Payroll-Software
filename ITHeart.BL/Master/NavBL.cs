using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Data;

namespace ITHeart.BL
{
    public class NavBL:Common
    {
        public bool CreateRecords = true, UpdateRecords = true;
        //public int RecordsHavingError = 0, RecordsCreated = 0, RecordsUpdated = 0, TotalLicencesAllowed = 0;
        private Dictionary<string, ArrayList> SPParam;


        public DataSet LoadNavigationData(string OptionId, string GenID, string CommunityID, string AddOnCriteria, string SearchOn = "")
        {
            Dictionary<string, ArrayList> spParam = new Dictionary<string, ArrayList>();

            string sVal = "";
            switch (OptionId.Substring(0, 9))
            {
                #region " Masters "
             
                case "202076000":       //  General Master
                    spParam = SetDictionaryValue(spParam, "CommunityID", CommunityID);
                    spParam = SetDictionaryValue(spParam, "GenID", GenID);
                    spParam = AddDictionaryValues(spParam, AddOnCriteria);
                    if (!string.IsNullOrEmpty(SearchOn.Trim()))
                        spParam = SetDictionaryValue(spParam, "SearchOn", SearchOn);
                    sVal = spNames.Load_GenMstBasedOnGenID;
                    break;
                
                #endregion

            }

            DataSet ds = base.ExecuteData(sVal, spParam, false, false, true);
            if (base.Status != 0 && base.Message != "" || ds == null)
            {
                _Status = base.Status;
                _Message = base.Message;
                return null;
            }
            return ds;
        }

        
    }
}
