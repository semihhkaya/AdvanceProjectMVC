using AdvanceProjectMVC.Dto.Title;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.ConnectService
{
	public class TitleConnectService
	{
        HttpClient _client;
        public TitleConnectService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<TitleSelectDTO>> GetTitle()
		{
            var donendeger = await _client.GetAsync("gettitle");

            if (donendeger.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<TitleSelectDTO>>(await donendeger.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
