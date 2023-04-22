using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class Employee
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int Gender { get; set; }
        public DateTime Doj { get; set; }
        public int MritlStatus { get; set; }
        public int Nationality { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public DateTime DateOfRel { get; set; }
        public DateTime Dob { get; set; }
        public string HighQual { get; set; }
        public DateTime dor { get; set; }
    }
}