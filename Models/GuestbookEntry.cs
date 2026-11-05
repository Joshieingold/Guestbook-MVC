using System;
using System.ComponentModel.DataAnnotations;

namespace A3_Guestbook.Models
{
    public class GuestbookEntry
    {
        public int Id { get; set; }

        [Required, StringLength(50)]
        public string? Author { get; set; }

        [Required, StringLength(200)]
        public string? Message { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
