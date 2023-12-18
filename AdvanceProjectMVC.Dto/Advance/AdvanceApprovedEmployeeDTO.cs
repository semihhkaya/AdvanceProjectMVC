using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.Dto.Advance
{
	public class AdvanceApprovedEmployeeDTO
	{
		public int StatusId { get; set; }
		public string StatusName { get; set; }
		public string ApprovedEmployeeName { get; set; }
		public int TitleId { get; set; }
		public string BusinessUnitID { get; set; }
		public int AdvanceID { get; set; }
		public int TransactorID { get; set; } //islemi yapan employee
	}
}
