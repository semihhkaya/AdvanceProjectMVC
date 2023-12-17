using AdvanceProjectMVC.Dto.Advance;
using AdvanceProjectMVC.Dto.Employee;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.ConnectService
{
	public class EmployeeConnectService
	{
		HttpClient _client;
		public EmployeeConnectService(HttpClient client)
		{
			_client = client;
		}

		public async Task<bool> Register(EmployeeRegisterDTO dto)
		{
			StringContent stringContent = new StringContent(JsonConvert.SerializeObject(dto));

			stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
			
			var donendeger = await _client.PostAsync("register", stringContent);
			//çağrıdığımzı method httpresponsemessage döner aldığımız yolladığımız değer contentin içinde

			if (donendeger.IsSuccessStatusCode)
			{
				return true;
			}
			return false;
		}

		public async Task<List<EmployeeSelectDTO>> GetEmployee()
		{
			var donendeger = await _client.GetAsync("getemployee");

			if (donendeger.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<List<EmployeeSelectDTO>>(await donendeger.Content.ReadAsStringAsync());
			}

			return null;
		}
		public async Task<EmployeeSelectDTO> Login(EmployeeLoginDTO employeeLoginDTO)
		{
			StringContent stringContent = new StringContent(JsonConvert.SerializeObject(employeeLoginDTO));
			stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

	//		_client.DefaultRequestHeaders.Authorization =
	//new AuthenticationHeaderValue("Bearer", "Your Oauth token");

			var response = await _client.PostAsync("login", stringContent);

			if (response.IsSuccessStatusCode)
			{
				var responseData = JsonConvert.DeserializeObject<EmployeeSelectDTO>(await response.Content.ReadAsStringAsync());
				return responseData;
			}
			return null;
		}

		//localhost:13275/Employee/GetAdvanceByEmployeeId?employeeId=12
		public async Task<List<EmployeeAdvanceResponseDto>> GetAdvanceByEmployeeId(int employeeId)
		{
			var donendeger = await _client.GetAsync($"getadvancebyemployeeid?employeeId={employeeId}");

			if (donendeger.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<List<EmployeeAdvanceResponseDto>>(await donendeger.Content.ReadAsStringAsync());
			}

			return null;

		}
		public async Task<List<AdvanceDetailDTO>> GetAdvanceDetails(int advanceId)
		{

			var donendeger = await _client.GetAsync($"getadvancedetails?advanceId={advanceId}");

			if (donendeger.IsSuccessStatusCode)
			{
				return JsonConvert.DeserializeObject<List<AdvanceDetailDTO>>(await donendeger.Content.ReadAsStringAsync());
			}

			return null;

		}
	}
}
