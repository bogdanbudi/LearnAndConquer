using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Tutorial.API.Entities;

namespace Tutorial.API.Data
{
    public class TutorialContextSeed
    {
        public static void SeedData(IMongoCollection<Course> courseCollection)
        {
            bool existCourse = courseCollection.Find(p => true).Any();
            if (!existCourse)
            {
                courseCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Course> GetPreconfiguredProducts()
        {
            return new List<Course>()
            {
                new Course()
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "Curs ASP.NET Core",
                    PrimaryTehnology = "ASP.NET",
                    Company = "Endava",
                    InstructorName = "Budi Bogdan", 
                    Summary = "ASP.NET Core is a free and open-source web framework and successor to ASP.NET.",
                    Description = "The framework is a complete rewrite that unites the previously separate ASP.NET MVC and ASP.NET Web API into a single programming model." +
                    "Despite being a new framework, built on a new web stack, it does have a high degree of concept compatibility with ASP.NET. The ASP.NET Core framework supports side-by-side versioning so that different applications being developed on a single machine can target different versions of ASP.NET Core. This is not possible with previous versions of ASP.NET.",
                    ImageFile = "course-1.png",
                    VideoFile = "video-1.png",
                    Price = 950,
                    Category = "Development IT"
                },
                new Course()
                {
                    Id = "602d2149e773f2a3990b47g2",
                    Name = "Curs Spring Boot",
                    PrimaryTehnology = "Spring Boot",
                    Company = "Endava",
                    InstructorName = "Mihai Andreea",
                    Summary = "Spring Boot makes it easy to create stand-alone, production-grade Spring based Applications that you can just run.",
                    Description = "We take an opinionated view of the Spring platform and third-party libraries so you can get started with minimum fuss. Most Spring Boot applications need minimal Spring configurationIf you’re looking for information about a specific version, or instructions about how to upgrade from an earlier release, check out the project release notes section on our wiki.",
                    ImageFile = "course-2.png",
                    Price = 500,
                    Category = "Development IT"
                },
                new Course()
                {
                    Id = "602d2149e773f2a3990b47M2",
                    Name = "Curs Spring Boot Begin",
                    PrimaryTehnology = "Spring Boot",
                    Company = "Endava",
                    InstructorName = "Mihai Andreea",
                    Summary = "Spring Boot makes it easy to create stand-alone, production-grade Spring based Applications that you can just run.",
                    Description = "We take an opinionated view of the Spring platform and third-party libraries so you can get started with minimum fuss. Most Spring Boot applications need minimal Spring configurationIf you’re looking for information about a specific version, or instructions about how to upgrade from an earlier release, check out the project release notes section on our wiki.",
                    ImageFile = "course-3.png",
                    Price = 0,
                    Category = "Development IT"
                },
                new Course()
                {
                    Id = "602d2149e773f2a399aaa7M2",
                    Name = "Machine Learning",
                    PrimaryTehnology = "Python",
                    Company = "Endava",
                    InstructorName = "George Popescu",
                    Summary = "Learn to create Machine Learning Algorithms in Python and R from two Data Science experts. Code templates included.",
                    Description = "Interested in the field of Machine Learning? Then this course is for you! " +
                                  "This course has been designed by two professional Data Scientists so that we can share our knowledge and help you learn complex theory, algorithms, and coding libraries in a simple way." +
                                  "We will walk you step-by-step into the World of Machine Learning. With every tutorial, you will develop new skills and improve your understanding of this challenging yet lucrative sub-field of Data Science." +
                                  "This course is fun and exciting, but at the same time, we dive deep into Machine Learning.",
                    ImageFile = "course-4.png",
                    Price = 0,
                    Category = "Data Science"
                }
            };
        }
    }
}
