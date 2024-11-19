namespace FinalProject.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public string Price { get; set; }

        public int CategoryId { get; set; }
        public string ServiceProviderId { get; set; }


        public ICollection<Order> Orders { get; set; } = new List<Order>();
        public ICollection<Feedback> Feedbacks { get; set; } = new List<Feedback>();    

        public Category Category { get; set; }

        public ApplicationUser ServiceProvider { get; set; }

    }
}
