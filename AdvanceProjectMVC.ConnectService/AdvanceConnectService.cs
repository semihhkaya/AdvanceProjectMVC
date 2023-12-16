using AdvanceProjectMVC.Dto.Advance;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task<bool> AddAdvance(AdvanceInsertDTO dto)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(dto));

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("addadvance", stringContent);
            //çağrıdığımzı method httpresponsemessage döner aldığımız yolladığımız değer contentin içinde

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }

        

    }
}
