using FinalProject.Models;

namespace FinalProject.Services
{
    public interface IFeedbackService
    {
        IEnumerable<Feedback> GetAllFeedback();
        Feedback GetFeedbackById(int id);
        void AddFeedback(Feedback feedback);
        ICollection<Feedback> GetFeedbacksByServiceId(int id);
        void EditFeedback(Feedback feedback);

        void DeleteFeedback(int id);
    }
}
