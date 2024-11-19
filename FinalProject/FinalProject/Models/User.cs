using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace FinalProject.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string? Job { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();
        public ICollection<Service> Services { get; set; } = new List<Service>();

        public virtual ICollection<Chat> SentMessages { get; set; } = new List<Chat>();
        public virtual ICollection<Chat> ReceivedMessages { get; set; } = new List<Chat>();
    }
}