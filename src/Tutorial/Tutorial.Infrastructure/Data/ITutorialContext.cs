using MongoDB.Driver;
using Tutorial.Domain.Entities;

namespace Tutorial.Infrastructure.Data
{
    public interface ITutorialContext
    {
        IMongoCollection<Course> Courses { get; }
    }
}
