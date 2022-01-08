using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Tutorial.API.Entities;

namespace Tutorial.API.Data
{
    public class TutorialContext: ITutorialContext
    {
        public TutorialContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            var database = client.GetDatabase(configuration.GetValue<string>("DatabaseSettings:DatabaseName"));

            Courses = database.GetCollection<Course>(configuration.GetValue<string>("DatabaseSettings:CollectionName"));
            TutorialContextSeed.SeedData(Courses);

        }
        public IMongoCollection<Course> Courses { get; }
    }

}
