using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tutorial.Infrastructure.Data;
using Tutorial.Domain.Entities;
using Tutorial.Infrastructure.Extensions;
using Tutorial.Infrastructure.Helper;
using System.Linq;
using Tutorial.Domain.Dtos;

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

        public async Task<Pagination<Course>> GetCoursesPagination(int pageSize, int pageNumber, string category, string primaryTehnology, string companyName, string sort, string search)
        {

            var courses = await _context
                            .Courses
                            .Find(p => true)
                            .ToListAsync();

            IEnumerable<Course> filteredCourses = courses;

            //add filters
            if (!String.IsNullOrWhiteSpace(category) && category != "All")
                filteredCourses = filteredCourses.Where(course => course.Category == category);

            if (!String.IsNullOrWhiteSpace(primaryTehnology) && primaryTehnology != "All")
                filteredCourses = filteredCourses.Where(course => course.PrimaryTehnology == primaryTehnology);

            if (!String.IsNullOrWhiteSpace(companyName) && companyName != "All")
                filteredCourses = filteredCourses.Where(course => course.Company == companyName);

            if (!String.IsNullOrWhiteSpace(search))
                filteredCourses = filteredCourses.Where(course => course.Name.Contains(search) ||  course.InstructorName.Contains(search));


            IEnumerable<Course> resultFilteredCoures = new List<Course>();

            //add sort
            if (String.IsNullOrWhiteSpace(sort) || sort == "name")
            {
                resultFilteredCoures = filteredCourses.ToList().OrderBy(course => course.Name);
            }
            else if(sort == "priceAsc")
            {
                resultFilteredCoures = filteredCourses.ToList().OrderBy(course => course.Price);
            }
            else if (sort == "priceDesc")
            {
                resultFilteredCoures = filteredCourses.ToList().OrderByDescending(course => course.Price);
            }



            return PagingExtensions.PaginationCourse(resultFilteredCoures, pageNumber, pageSize);
        }

        public async Task<List<GetCategoriesDto>> GetCategories()
        {
            var courses = await GetCourses();
            var categories = courses.Select(c => c.Category).Distinct().ToList();

            var categoriesDto = new List<GetCategoriesDto>();

            int i = 0;
            foreach (var category in categories)
            {
                categoriesDto.Add(new GetCategoriesDto() { Id = ++i, Name = category });
            }

            return categoriesDto;
        }

        public async Task<List<GetTehnologiesDto>> GetTehnologies()
        {
            var courses = await GetCourses();
            var tehnologies = courses.Select(c => c.PrimaryTehnology).Distinct().ToList();

            var tehnologiesDto = new List<GetTehnologiesDto>();

            int i = 0;
            foreach (var tehnology in tehnologies)
            {
                tehnologiesDto.Add(new GetTehnologiesDto() { Id = ++i, Name = tehnology });
            }

            return tehnologiesDto;
        }

        public async Task<List<GetCompaniesDto>> GetCompanies()
        {
            var courses = await GetCourses();
            var companies = courses.Select(c => c.Company).Distinct().ToList();

            var companiesDto = new List<GetCompaniesDto>();

            int i = 0;
            foreach (var company in companies)
            {
                companiesDto.Add(new GetCompaniesDto() { Id = ++i, Name = company });
            }

            return companiesDto;
        }

    }
}
