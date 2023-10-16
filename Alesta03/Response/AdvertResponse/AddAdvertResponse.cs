namespace Alesta03.Response.AdvertResponse
{
    public class AddAdvertResponse
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? CompanyName { get; set; }
        public string? AdvertName { get; set; }
        public DateTime? AdvertDate { get; set; }
        public string? Description { get; set; }
        public string? AdvertType { get; set; }
        public string? Department { get; set; }
        public string? WorkType { get; set; }
        public string? WorkPreference { get; set; }
    }
}
