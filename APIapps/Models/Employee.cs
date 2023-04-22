using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIapps.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }        
        public string Code { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime Doj { get; set; }
        public int MaritalStatus { get; set; }
        public int Nationality { get; set; }
        public string FatherName { get; set; }
        public string CompanyId { get; set; }
        public DateTime DateOfRelieving { get; set; }
        public string MotherName { get; set; }
        public DateTime Dor { get; set; }
        public DateTime Dob { get; set; }
        public string HighQual { get; set; }
        public string EPFNo { get; set; }
        public string ESINo { get; set; }
        public string PANNo { get; set; }
        public string AdhaarNo { get; set; }
        public string UANNo { get; set; }
        public string StatementType { get; set; }
        public string LicenceNo { get; set; }
        public DateTime LicenceDateOfIssue { get; set; }
        public DateTime LicenceValidity { get; set; }
        public string LicenceIssueAuthority { get; set; }
        public string PassportNo { get; set; }
        public string PassportDateOfIssue { get; set; }
        public string PassportValidity { get; set; }
        public string PassportPlaceIssue { get; set; }
        public string ShiftId { get; set; }
        public string EmployeeTypeId { get; set; }
        public string EmployeeCategoryId { get; set; }
        public string ProjectId { get; set; }
        public string DepartmentId { get; set; }
        public string DesignationId { get; set; }
        public string LocationId { get; set; }
        public string DivisionId { get; set; }
        public string BankId { get; set; }
        public string BankAccountNo { get; set; }
        public string BankIfscCode { get; set; }
        public string PAdd1 { get; set; }
        public string PAdd2 { get; set; }
        public string PCity { get; set; }
        public string PState { get; set; }
        public string PDistrict { get; set; }
        public string PPinCode { get; set; }
        public string PMobileNo { get; set; }
        public string PEmergencyContact { get; set; }
        public string RAdd1 { get; set; }
        public string RAdd2 { get; set; }
        public string RCity { get; set; }
        public string RState { get; set; }
        public string RDistrict { get; set; }
        public string RPinCode { get; set; }
        public string RMobileNo { get; set; }
        public string REmergencyContact { get; set; }
        public bool? IsPresentSame { get; set; }

    }
}