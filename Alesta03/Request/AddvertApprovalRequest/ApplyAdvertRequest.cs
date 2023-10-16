namespace Alesta03.Request.AddvertApprovalRequest
{
    public class ApplyAdvertRequest
    {
        public int ApprovalId { get; set; }
        public int UserId { get; set; }
        public int AdvertId { get; set; }
        public string AppName { get; set; }
        public string AppSurname { get; set; }
        public float AppAvg { get; set; }
        public string Status { get; set; }
        public DateTime ApproveDate { get; set; }=DateTime.Now;
    }
}
