using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.Dto.Advance
{
	class AdvanceInsertDTO
	{
		public decimal? AdvanceAmount { get; set; }
		public string AdvanceDescription { get; set; }
		public int? ProjectId { get; set; }
		public DateTime? DesiredDate { get; set; }
		public int? EmployeeId { get; set; } 
		public int? StatusID { get; set; } 
		public DateTime? RequestDate { get; set; }


	}
}
