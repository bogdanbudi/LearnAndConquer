using System;
using System.Linq.Expressions;
using Tutorial.API.Entities;

namespace Tutorial.API.Core.Spe
{
    public class CoursesWithCompanyAndTehnologySpecification: BaseSpecification<Course>
    {
        public CoursesWithCompanyAndTehnologySpecification()
        {
            AddInclude(x => x.Company);
            AddInclude(x => x.PrimaryTehnology);
        }

        public CoursesWithCompanyAndTehnologySpecification(string id) : base(x => x.Id == id)
        {
            AddInclude(x => x.Company);
            AddInclude(x => x.PrimaryTehnology);
        }
    }
}
