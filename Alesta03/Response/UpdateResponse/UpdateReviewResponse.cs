namespace Alesta03.Response.UpdateResponse
{
    public class UpdateReviewResponse
    {
        public string Title { get; set; }
        public DateTime UpdateDate { get; set; }= DateTime.Now;
        public string Description { get; set; }
    }
}

