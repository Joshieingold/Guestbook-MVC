using System.ComponentModel.DataAnnotations;

namespace A3_Guestbook.Models
{
    public class GuestbookEntry
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter your name")]
        [StringLength(50, ErrorMessage = "Your name cannot exceed 50 characters")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Please write a message")]
        [StringLength(200, ErrorMessage = "Message cannot exceed 200 characters")]
        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
