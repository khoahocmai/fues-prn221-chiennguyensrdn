using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.IRepo
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategories();
        Task<Category> GetCategoryById(int id);
        Task AddCategory(Category category);
        Task UpdateCategory(Category category);
        Task RemoveCategory(int id);
    }
}
