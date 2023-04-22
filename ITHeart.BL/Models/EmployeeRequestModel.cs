using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHeart.BL.Models
{
    public class Employee
    {
        public bool isSalary { get; set; }
        public int EmployeeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime? Doj { get; set; }
        public int MaritalStatus { get; set; }
        public int Nationality { get; set; }
        public string FatherName { get; set; }
        public string CompanyId { get; set; }
        public DateTime? DateOfRelieving { get; set; }
        public string MotherName { get; set; }
        public DateTime? Dor { get; set; }
        public DateTime? Dob { get; set; }
        public string HighQual { get; set; }
        public string EPFNo { get; set; }
        public string ESINo { get; set; }
        public string PANNo { get; set; }
        public string AdhaarNo { get; set; }
        public string UANNo { get; set; }
        public string StatementType { get; set; }
        public string LicenceNo { get; set; }
        public DateTime? LicenceDateOfIssue { get; set; }
        public DateTime? LicenceValidity { get; set; }
        public string LicenceIssueAuthority { get; set; }
        public string PassportNo { get; set; }
        public DateTime? PassportDateOfIssue { get; set; }
        public DateTime? PassportValidity { get; set; }
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

        public List<EmployeeFamilyDetail> EmployeeFamilyDetails { get; set; }

        public List<EmployeeEmployementDetail> EmployeeEmployementDetails { get; set; }
        public List<EmployeeAcademicDetail> EmployeeAcademicDetails { get; set; }

        public List<Salary> Salarys { get; set; }
        public List<SalaryItem> SalaryItems { get; set; }
    }

    public class EmployeeFamilyDetail
    {

        public int FamilyRelationId { get; set; }
        public string FamilyMemberName { get; set; }
        public int FamilyMemberAge { get; set; }
        public DateTime? FamilyMemberDob { get; set; }
        public string FamilyMembeAdhaarNo { get; set; }
        
    }
    public class EmployeeEmployementDetail
    {

        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public DateTime? Doj { get; set; }
        public DateTime? Dol { get; set; }
        public string ReasonOfLeaving { get; set; }

    }
    public class EmployeeAcademicDetail
    {
        public string ExaminationPassed { get; set; }

        public string NameOfSchool { get; set; }
        public string Subjects { get; set; }
        public int MonthofPassing { get; set; }
        public int YearofPassing { get; set; }
        public string Grade { get; set; }

    }

    public class SalaryItem
    {
     
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public int SalaryItemId { get; set; }
        public string PayrollItemId { get; set; }
        public string PayrollItemName { get; set; }
        public string PayrollItemType { get; set; }
        public string PayrollItemTypeName { get; set; }
        public decimal Amount { get; set; }
        public DateTime recdate { get; set; }
        public DateTime recupdateddate { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime? Enddate { get; set; }
    }

    public class Salary
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public decimal CTC { get; set; }
        public decimal Gross { get; set; }
        public decimal NetPay { get; set; }
        public DateTime Startdate { get; set; }
        public DateTime? Enddate { get; set; }
        public DateTime recdate { get; set; }
        public DateTime recupdatedate { get; set; }
        public int templateid { get; set; }
        public decimal AdditionalBenefit { get; set; }
        public decimal Deduction { get; set; }
        public decimal Earning { get; set; }
    }
}
