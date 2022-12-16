using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search.Core.DataTransferObjects.Movies
{
    public class Search_Response
    {
        public Search_ExternalAPI_Response result { get; set; }
        public bool error{ get; set; }
        public string message { get; set; }
    }
}
