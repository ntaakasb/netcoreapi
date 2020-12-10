using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV2.Model
{
    public class SearchViewModel
    {
        public string PageIndex { get; set; }
        public string PageSize { get; set; }
        public string OrderBy { get; set; }
        public string FilterQuery { get; set; }
    }
}
