using System.ComponentModel.DataAnnotations;

namespace FrontToBack_2.Models
{
    public class Blog
    {
        public int Id { get; set; }
        [MaxLength(25, ErrorMessage = "Uzunlugu 25 xarakterden cox olmamalidir.")]
        public string Title { get; set; }
        public string ImageUrl { get; set; }
        [MaxLength(250, ErrorMessage = "Uzunlugu 250 xarakterden cox olmamalidir.")]
        public string Description { get; set; }
        public int ReplyCount { get; set; } 
        public DateTime dateTime { get; set; }
    }
}
