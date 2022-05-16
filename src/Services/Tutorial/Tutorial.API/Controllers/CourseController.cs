
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Tutorial.API.Entities;
using Tutorial.API.Helper;
using Tutorial.API.Repository;

namespace Tutorial.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    [EnableCors("CoursesPolicy")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseRepository _repository;
        private readonly ILogger<CourseController> _logger;

        public CourseController(ICourseRepository repository, ILogger<CourseController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Course>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            var courses = await _repository.GetCourses();
            return Ok(courses);
        }

        [HttpGet("{id:length(24)}", Name = "GetCourse")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Course>> GetCourseById(string id)
        {
            var course = await _repository.GetCourse(id);
            if (course == null)
            {
                _logger.LogError($"Course with id: {id}, not found.");
                return NotFound();
            }
            return Ok(course);
        }

        [Route("[action]/{category}", Name = "GetCourseByCategory")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Course>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourseByCategory(string category)
        {
            var courses = await _repository.GetCourseByCategory(category);
            return Ok(courses);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Course>> CreateProduct([FromBody] Course course)
        {
            await _repository.CreateCourse(course);

            return CreatedAtRoute("GetCourse", new { id = course.Id }, course);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Course course)
        {
            return Ok(await _repository.UpdateCourse(course));
        }

        [HttpDelete("{id:length(24)}", Name = "DeleteCourse")]
        [ProducesResponseType(typeof(Course), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteCourseById(string id)
        {
            return Ok(await _repository.DeleteCourse(id));
        }


        [HttpGet("GetCoursesPagination")]
        [ProducesResponseType(typeof(Pagination<Course>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Pagination<Course>>> GetCoursesPagination(int pageSize = 2, int pageNumber = 1)
        {
            var courses = await _repository.GetCoursesPagination(pageSize, pageNumber);
            return Ok(courses);
        }

        

    }
}
