using MongoDB.Driver;
using Tutorial.API.Entities;

namespace Tutorial.API.Data
{
    public interface ITutorialContext
    {
        IMongoCollection<Course> Courses { get; }
    }
}
