namespace Alesta03.Response.GetResponse
{
    public class GetReviewResponse
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }      
        public int personId { get; set; }
        public int companyId { get; set; }
    }
}
