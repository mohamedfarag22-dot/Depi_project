using FinalProject.Models;
using System.Collections.Generic;
namespace FinalProject.ViewModel
{
    public class CategoryDetailsViewModel
    {
        public Category Category { get; set; }
        public IEnumerable<Service> Services { get; set; }
    }
}