namespace FinalProject.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ServiceId { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime AvailableFrom {  get; set; } 
        public DateTime AvailableTo {  get; set; } 
        public string Address { get; set; }
        public ApplicationUser User { get; set; }
        public Service Service { get; set; }
    }
}
