using AdvanceProjectMVC.Dto.BusinessUnit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.ConnectService
{
	public class BusinessUnitConnectService
	{
        HttpClient _client;
        public BusinessUnitConnectService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<BusinessUnitSelectDTO>> GetBusinessUnit()
        {
            var donendeger = await _client.GetAsync("getbusinessunit");

            if (donendeger.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<BusinessUnitSelectDTO>>(await donendeger.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
