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

        public async Task<List<int>> GetTitleID(decimal advanceAmount)
		{
            var response = await _client.GetAsync($"gettitleid?advanceAmount={advanceAmount}");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<int>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }


        public async Task<List<AdvanceOrderConfirmDTO>> GetAdvanceOrderConfirm(int businessUnitId, List<int> titles)
        {
            var response = await _client.GetAsync("getadvanceorderconfirm");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<AdvanceOrderConfirmDTO>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }
        public async Task<List<AdvanceApprovedEmployeeDTO>>  GetAdvanceApproveEmployee(int advanceId, List<int> titles)
        {
            var response = await _client.GetAsync("getadvanceapproveemployee");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<AdvanceApprovedEmployeeDTO>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }

        public async Task<bool> AddAdvanceHistoryApprove(AdvanceHistoryApproveDTO dto)
        {
            StringContent stringContent = new StringContent(JsonConvert.SerializeObject(dto));

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await _client.PostAsync("addadvancehistoryapprove", stringContent);
            //çağrıdığımzı method httpresponsemessage döner aldığımız yolladığımız değer contentin içinde

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            return false;
        }





    }
}
