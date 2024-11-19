using System.ComponentModel.DataAnnotations;

namespace FinalProject.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int ServiceId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Address { get; set; }

        [Required]
        public DateTime AvailableFrom { get; set; }

        [Required]
        public DateTime AvailableTo { get; set; }

        public int CategoryId { get; set; }
    }
}
