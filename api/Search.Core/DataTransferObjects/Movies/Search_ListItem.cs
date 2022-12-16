using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search.Core.DataTransferObjects.Movies
{
    public class Search_ListItem
    {
        public string Title { get; set; }
        public int Year { get; set; }
        public string imdbID { get; set; }
    }
}
