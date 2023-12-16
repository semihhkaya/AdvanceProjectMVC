using AdvanceProjectMVC.Dto.Project;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.ConnectService
{
	public class ProjectConnectService
	{
        HttpClient _client;
        public ProjectConnectService(HttpClient client)
        {
            _client = client;
        }
        public async Task<List<ProjectSelectDTO>> GetProject()
        {
            var response = await _client.GetAsync("getproject");

            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<List<ProjectSelectDTO>>(await response.Content.ReadAsStringAsync());
            }

            return null;
        }
    }
}
