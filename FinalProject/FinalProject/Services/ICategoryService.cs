using FinalProject.Models;
using System.Collections.Generic;

public interface ICategoryService
{
    IEnumerable<Category> GetAllCategories();
    Category GetCategoryById(int id);
    void AddCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(int id);
    IEnumerable<Service> GetServicesByCategoryId(int categoryId); 
}
