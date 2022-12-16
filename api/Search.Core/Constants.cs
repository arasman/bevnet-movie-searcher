using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search.Core
{
    public static class HttpClient_Name
    {
        public static string Default = "Search";
    }
    public static class Search_Result
    {
        public static string Success = "Success";
        public static string NoData = "NoData";
        public static string DataException = "DataException";
        public static string GeneralException = "GeneralException";
        public static string InternalServerError = "InternalServerError";
    }
}
