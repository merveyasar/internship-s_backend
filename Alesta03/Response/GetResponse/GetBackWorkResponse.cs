using Alesta03.Model;

namespace Alesta03.Response.GetResponse
{
    public class GetBackWorkResponse
    {
        public string CompanyMail { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeID { get; set; }
        public string AppLetter { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }

        public string appStatus { get; set; }
    }
}
