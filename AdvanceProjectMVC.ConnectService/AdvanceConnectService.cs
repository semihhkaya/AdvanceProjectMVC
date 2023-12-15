using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.ConnectService
{
	public class AdvanceConnectService
    {
        HttpClient _client;
        public AdvanceConnectService(HttpClient client)
        {
            _client = client;
        }
    }
}
