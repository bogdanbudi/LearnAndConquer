using System.Collections.Generic;
using System.Threading.Tasks;
using Tutorial.Domain.Dtos;
using Tutorial.Domain.Entities;
using Tutorial.Infrastructure.Helper;

namespace Tutorial.Infrastructure.Repository
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCourses();
        Task<Course> GetCourse(string id);
        Task<IEnumerable<Course>> GetCourseByName(string name);
        Task<IEnumerable<Course>> GetCourseByCategory(string categoryName);

        Task CreateCourse(Course course);
        Task<bool> UpdateCourse(Course course);
        Task<bool> DeleteCourse(string id);
        Task<Pagination<Course>> GetCoursesPagination(int pageSize, int pageNumber, string category, string primaryTehnology, string companyName, string sort);

        Task<List<GetCategoriesDto>> GetCategories();

        Task<List<GetTehnologiesDto>> GetTehnologies();

        Task<List<GetCompaniesDto>> GetCompanies();

    }
}
