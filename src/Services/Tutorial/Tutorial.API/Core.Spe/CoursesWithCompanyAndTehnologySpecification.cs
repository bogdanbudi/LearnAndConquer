using System;
using System.Linq.Expressions;
using Tutorial.API.Entities;

namespace Tutorial.API.Core.Spe
{
    public class CoursesWithCompanyAndTehnologySpecification: BaseSpecification<Course>
    {
        public CoursesWithCompanyAndTehnologySpecification(string sort)
        {
            AddInclude(x => x.Company);
            AddInclude(x => x.PrimaryTehnology);
            AddOrderBy(x => x.Name); //default orderBy

            if(!string.IsNullOrEmpty(sort))
            {
                switch (sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.Price);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public CoursesWithCompanyAndTehnologySpecification(string id, string sort) : base(x => x.Id == id)
        {
            AddInclude(x => x.Company);
            AddInclude(x => x.PrimaryTehnology);
            AddOrderBy(x => x.Name); //default orderBy
        }
    }
}
