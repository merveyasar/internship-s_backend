namespace Alesta03.Response.UpdateResponse
{
    public class UpdateInfoEduResponse
    {
        public string SchoolName { get; set; }
        public string DepartmentName { get; set; }
        public string SchoolType { get; set; }
        public bool EduStatus { get; set; }
        public float Avg { get; set; }
        public DateTime UpdateDate { get; set; }= DateTime.Now;
    }
}
