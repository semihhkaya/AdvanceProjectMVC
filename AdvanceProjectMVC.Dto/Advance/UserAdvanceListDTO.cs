using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.Dto.Advance
{
	public class UserAdvanceListDTO
	{
		public int AdvanceID { get; set; }
		public string Name { get; set; }
		public string Surname { get; set; }
		public string StatusName { get; set; }	
		public string TitleName { get; set; }
		public int AdvanceStatusID { get; set; }
		public DateTime? RequestDate { get; set; }
		public DateTime? DesiredDate { get; set; }

		public decimal AdvanceAmount { get; set; }
	}
}
