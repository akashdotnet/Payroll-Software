using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHeart.BL.Models
{
   public class SalaryTemplateItem
    {
        public bool ISAmount { get; set; }
        public string PayrollItemId { get; set; }
        public string ApplicableOn { get; set; }
        public decimal DefaultValue { get; set; }
        public int SalaryTemplateId { get; set; }
        public int SalaryTemplateItemId { get; set; }
        public string PayrollItemText { get; set; }
        public string ApplicableItemText { get; set; }    
        public bool Active { get; set; }

        public string StatementType { get; set; }
        public int ValueType { get; set; }

    }
}
