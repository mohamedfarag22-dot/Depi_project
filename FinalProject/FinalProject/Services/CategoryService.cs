using FinalProject.Models;
using System.Collections.Generic;
using System.Linq;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Category> GetAllCategories()
    {
        return _context.categories.ToList();
    }

    public Category GetCategoryById(int id)
    {
        return _context.categories.Find(id);
    }

    public void AddCategory(Category category)
    {
        _context.categories.Add(category);
        _context.SaveChanges();
    }

    public void UpdateCategory(Category category)
    {
        _context.categories.Update(category);
        _context.SaveChanges();
    }

    public void DeleteCategory(int id)
    {
        var category = GetCategoryById(id);
        if (category != null)
        {
            _context.categories.Remove(category);
            _context.SaveChanges();
        }
    }

    public IEnumerable<Service> GetServicesByCategoryId(int categoryId)
    {
        return _context.services.Where(s => s.CategoryId == categoryId).ToList();
    }
}
