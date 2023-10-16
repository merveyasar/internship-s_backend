namespace Alesta03.Request.UpdateRequest
{
    public class UpdatePProfileRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public DateTime Birthday { get; set; } = DateTime.Now;
        public string Phone { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
    }
}
