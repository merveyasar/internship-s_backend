namespace Alesta03.Request.UpdateRequest
{
    public class UpdateInfoWorkRequest
    {
        public string CompanyMail { get; set; } = string.Empty;
        public string DepartmentName { get; set; } = string.Empty;
        public string EmployeeID { get; set; }
        public string AppLetter { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
