using System.Collections.Generic;
using System.Threading.Tasks;
using Tutorial.API.Core.Spe;
using Tutorial.API.Entities;

namespace Tutorial.API.Repository
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

        //using Specs

        Task<Course> GetEntityWithSpec(ISpecification<Course> spec);

        Task<IReadOnlyList<Course>> GetEntitiesWithSpec(ISpecification<Course> spec);
    }
}
