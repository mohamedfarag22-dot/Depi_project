

namespace FinalProject.Models
{
    public class Chat
    {
        public int Id { get; set; }
        public string Message { get; set; }
                                                     
        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public DateTime Timestamp { get; set; }
        public ApplicationUser Receiver { get; set; }
        public ApplicationUser Sender { get; set; }
    }
}
