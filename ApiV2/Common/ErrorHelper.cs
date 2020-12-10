using ApiV2.Model;
using ModelContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiV2.Common
{
    public class ErrorHelper
    {
        private static ErrorHelper _current = new ErrorHelper();

        public static ErrorHelper Current
        {
            get
            {
                return _current != null ? _current : new ErrorHelper();
            }
        }

        public JsonResultData ErrorValidateData()
        {
            return new JsonResultData
            {
                IsOk = false,
                Msg = "Data is not valid"
            };
        }

        public JsonResultData ErrorValidateDataString(string errString)
        {
            return new JsonResultData
            {
                IsOk = false,
                Msg = errString
            };
        }
    }
}
