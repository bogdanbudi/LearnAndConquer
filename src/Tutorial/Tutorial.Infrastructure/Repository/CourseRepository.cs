using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tutorial.Infrastructure.Data;
using Tutorial.Domain.Entities;
using Tutorial.Infrastructure.Extensions;
using Tutorial.Infrastructure.Helper;
using System.Linq;

namespace Tutorial.Infrastructure.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ITutorialContext _context;

        public CourseRepository(ITutorialContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _context
                            .Courses
                            .Find(p => true)
                            .ToListAsync();
        }
        public async Task<Course> GetCourse(string id)
        {
            return await _context
                           .Courses
                           .Find(p => p.Id == id)
                           .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Course>> GetCourseByName(string name)
        {
            FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(p => p.Name, name);

            return await _context
                            .Courses
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetCourseByCategory(string categoryName)
        {
            FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(p => p.Category, categoryName);

            return await _context
                            .Courses
                            .Find(filter)
                            .ToListAsync();
        }

        public async Task CreateCourse(Course product)
        {
            await _context.Courses.InsertOneAsync(product);
        }

        public async Task<bool> UpdateCourse(Course product)
        {
            var updateResult = await _context
                                        .Courses
                                        .ReplaceOneAsync(filter: g => g.Id == product.Id, replacement: product);

            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }

        public async Task<bool> DeleteCourse(string id)
        {
            FilterDefinition<Course> filter = Builders<Course>.Filter.Eq(p => p.Id, id);

            DeleteResult deleteResult = await _context
                                                .Courses
                                                .DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

        public async Task<Pagination<Course>> GetCoursesPagination(int pageSize, int pageNumber)
        {
            var courses = await _context
                            .Courses
                            .Find(p => true)
                            .ToListAsync();

            return PagingExtensions.PaginationCourse(courses, pageNumber, pageSize);
        }

        public async Task<List<string>> GetCategories()
        {
            var courses = await GetCourses();

            return  courses.Select(c => c.Category).Distinct().ToList();
        }

        public async Task<List<string>> GetTehnologies()
        {
            var courses = await GetCourses();

            return courses.Select(c => c.PrimaryTehnology).Distinct().ToList();
        }

    }
}
