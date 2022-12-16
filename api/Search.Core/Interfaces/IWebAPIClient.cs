using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Search.Core.Interfaces
{
    public interface IWebAPIClient
    {
        public Task<HttpResponseMessage> Get(string httpClientName, string url);
        public Task<Tuple<bool,Tout>> Response_Read<Tout>(HttpResponseMessage responseMessage);
    }
}
