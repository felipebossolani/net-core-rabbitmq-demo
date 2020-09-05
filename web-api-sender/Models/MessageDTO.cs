using System.ComponentModel.DataAnnotations;

namespace web_api_sender.Models
{
    public class MessageDTO
    {
        [Required]
        public string Content { get; set; }
    }
}
