namespace Alesta03.Request.UpdateRequest
{
    public class UpdateCProfileRequest
    {
        public string Category { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string FDate { get; set; } = string.Empty;
        public string TotalStaff { get; set; }     
        public string Location { get; set; } = string.Empty;
        public string Prof { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Website { get; set; } = string.Empty;
    }
}
