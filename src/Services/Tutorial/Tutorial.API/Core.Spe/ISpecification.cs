using System;
using System.Linq.Expressions;

namespace Tutorial.API.Core.Spe
{
    public interface ISpecification<T>
    {
        Expression<Func<T, object>> OrderBy { get; }
        Expression<Func<T, object>> OrderByDescending { get; }
    }
}
