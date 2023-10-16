using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace Alesta03.Response.PostResponse
{
    public class AddPostResponse
    {
        public int Id { get; set; }
        public int ?UserId { get; set; }
        public string Content { get; set; }
        public DateTime PostDate { get; set; } = DateTime.Now;
    }
}
