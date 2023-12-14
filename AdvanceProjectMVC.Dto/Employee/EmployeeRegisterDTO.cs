using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.Dto.Employee
{
	public class EmployeeRegisterDTO
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
		//public byte[] PasswordHash { get; set; }
		//public byte[] PasswordSalt { get; set; }
		public string Password { get; set; }
		public int? BusinessUnitId { get; set; }
        public int? TitleId { get; set; }
        public int? UpperEmployeeId { get; set; }
        public bool IsActive { get; set; }
    }
}
