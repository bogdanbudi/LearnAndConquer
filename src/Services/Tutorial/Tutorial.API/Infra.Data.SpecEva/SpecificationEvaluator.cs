using Microsoft.EntityFrameworkCore;
using System.Linq;
using Tutorial.API.Core.Spe;

namespace Tutorial.API.Infra.Data.SpecEva
{
    public class SpecificationEvaluator<TEntity> where TEntity : class
    {
        //pentru ca este o metoda statica o putem folosi fara a instantia clasa
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpecification<TEntity> spec)
        {
           var query = inputQuery.AsQueryable();

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }

            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            return query;
        }
    }
}
