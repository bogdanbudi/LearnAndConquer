using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Tutorial.API.Entities;

namespace Tutorial.API.Data
{
    public interface ITutorialContext
    {
        IMongoCollection<Course> Courses { get; }

        public DbSet<Course> CoursesDbSet { get; set; }
    }
}
