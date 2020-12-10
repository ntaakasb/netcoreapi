using ModelContext.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelContext
{
    public class AttributeActionResult: IAttributeActionResult
    {
        public bool Successful { get; set; }
        public string Id { get; set; }
        public string ErrorMessage { get; set; }
    }
}
