using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.Dto.Advance
{
	public class EmployeeAdvanceResponseDto
	{
		public string Surname { get; set; }
		public string EmployeeName { get; set; }
		public int EmployeeID { get; set; }
		public string StatusName { get; set; }
		public int StatusID { get; set; }
		public string ProjectName { get; set; }
		public int ProjectID { get; set; }
		public string AdvanceDescription { get; set; }
		public decimal? AdvanceAmount { get; set; }
		public int AdvanceID { get; set; }
		public DateTime? RequestDate { get; set; }
		public DateTime? DesiredDate { get; set; }
	}
}
