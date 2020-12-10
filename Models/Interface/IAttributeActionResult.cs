using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Interface
{
    public interface IAttributeActionResult
    {
        bool Successful { get; set; }
        string Id { get; set; }
        string ErrorMessage { get; set; }
    }
}
