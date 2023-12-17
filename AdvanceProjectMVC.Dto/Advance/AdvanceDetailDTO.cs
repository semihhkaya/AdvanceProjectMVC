using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.Dto.Advance
{
	public class AdvanceDetailDTO
	{
		public int AdvanceID { get; set; }
		public decimal? AdvanceAmount { get; set; }
		public string AdvanceDescription { get; set; }
		public int StatusID { get; set; }
		public string StatusName { get; set; }
		public string EmployeeName { get; set; }
		public string Surname { get; set; }
		public int TransactorID { get; set; }
		public decimal? ApprovedAmount { get; set; }
		public DateTime? Date { get; set; }
		public string UpperEmployeeName { get; set; }
		public int UpperEmployeeId { get; set; }
		public DateTime? DeterminedPaymentDate { get; set; }
		public string ReceiptNo { get; set; }
	}
}
