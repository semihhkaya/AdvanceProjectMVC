using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdvanceProjectMVC.Dto.Advance
{
	public class AdvanceConfirmDTO
	{
		public int AdvanceID { get; set; }
		public string EmployeeName { get; set; }
		public string EmployeeSurname { get; set; }
		public string EmployeeBusinessUnit { get; set; }
		public string EmployeeTitle { get; set; }
		public string AdvanceDescription { get; set; }
		public int StatusID { get; set; } //Ekleme
		public int TransactorID { get; set; }

		public decimal? AdvanceAmount { get; set; }
		public DateTime? RequestDate { get; set; }
		public DateTime? DesiredDate { get; set; }
		public string TransactorName { get; set; } //Islem yapan
		public string TransactorSurname { get; set; }
		public string TransactorTitleName { get; set; }
		public string LastStatusName { get; set; } 
		public decimal? ApprovedAmount { get; set; }
		public string NextTransactorEmployeeName { get; set; }
		public string NextTransactorEmployeeSurname { get; set; }
		public string NextTransactorEmployeeTitle { get; set; }
		public int NextTransactorEmployeeTitleID { get; set; }
	}
}
