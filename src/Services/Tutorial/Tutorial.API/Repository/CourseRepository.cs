using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tutorial.API.Core.Spe;
using Tutorial.API.Data;
using Tutorial.API.Entities;
using Tutorial.API.Infra.Data.SpecEva;

namespace Tutorial.API.Repository
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

        //With Specifications
        public async Task<Course> GetEntityWithSpec(ISpecification<Course> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<Course>> GetEntitiesWithSpec(ISpecification<Course> spec)
        {


            var returnA = await ApplySpecification(spec).ToListAsync();
            return returnA;
        }

        private IQueryable<Course> ApplySpecification(ISpecification<Course> spec)
        {
            //_context.Set<T>.AsQueryable....
           // return SpecificationEvaluator<Course>.GetQuery(_context.Courses.AsQueryable(), spec);
            return SpecificationEvaluator<Course>.GetQuery(_context.CoursesDbSet.AsQueryable(), spec);
        }
    }
}
