using System.Collections.Generic;
using System.Linq;
using Tutorial.API.Entities;
using Tutorial.API.Helper;

namespace Tutorial.API.Extensions
{
    public static class PagingExtensions
    {
        //used by LINQ to SQL
        public static IQueryable<TSource> Page<TSource>(this IQueryable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        //used by LINQ
        public static IEnumerable<TSource> Page<TSource>(this IEnumerable<TSource> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);
        }

        //used by LINQ
        public static Pagination<Course> PaginationCourse(this IEnumerable<Course> source, int page, int pageSize)
        {
            var count = source.Count();
            var data = source.Skip((page - 1) * pageSize).Take(pageSize);
            var pagingObject = new Pagination<Course>(page, pageSize, count, data);
            return pagingObject;
        }

    }
}
