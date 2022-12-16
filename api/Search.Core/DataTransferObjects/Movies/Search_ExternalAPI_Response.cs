using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search.Core.DataTransferObjects.Movies
{
    public class Search_ExternalAPI_Response
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
        public int total_pages { get; set; }
        public IEnumerable<Search_ListItem> data { get; set; }
    }
}
