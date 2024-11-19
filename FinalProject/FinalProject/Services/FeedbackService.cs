using FinalProject.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ApplicationDbContext _context;

        public FeedbackService(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Feedback> GetAllFeedback()
        {
            return _context.Feedbacks.ToList();
        }

        public Feedback GetFeedbackById(int id)
        {
            return _context.Feedbacks.FirstOrDefault(f => f.Id == id);
        }

        public void AddFeedback(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            _context.SaveChanges();
        }

        public void DeleteFeedback(int id)
        {
            var feedback = _context.Feedbacks.Find(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                _context.SaveChanges();
            }
        }

        public void EditFeedback(Feedback feedback)
        {
            var editedfeedback = _context.Feedbacks.Find(feedback.Id);
            if (editedfeedback != null)
            {
                _context.Feedbacks.Update(editedfeedback);
                _context.SaveChanges();
            }
        }

        public ICollection<Feedback> GetFeedbacksByServiceId(int id)
        {
            return _context.Feedbacks.Include(f=>f.User).Where(e=>e.ServiceId == id).ToList();
        }
    }
}
