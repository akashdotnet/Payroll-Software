using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITHeart.BL.Models
{
   public class SalaryTemplate
    {
        public string Name { get; set; }
        public bool Active { get; set; }
        public string StatementType { get; set; }
        public int SalaryTemplateId { get; set; }

        public List<SalaryTemplateItem> SalaryTemplateItemRequestModels { get; set; }
    }
}
