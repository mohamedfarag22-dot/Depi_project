namespace FinalProject.Models
{
    public class Feedback
    {
        public int Id { get; set; } 

        public string Content { get; set; }

        public int Rating { get; set; }
        public int ServiceId { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Service Service { get; set; }
    }
}
