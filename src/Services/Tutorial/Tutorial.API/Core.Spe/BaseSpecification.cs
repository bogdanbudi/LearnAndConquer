﻿using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Tutorial.API.Core.Spe
{
    public class BaseSpecification<T> : ISpecification<T>
    {

        public BaseSpecification()
        {

        }

        public BaseSpecification(Expression<Func<T,bool>> criteria)
        {
                Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria { get; }

        public List<Expression<Func<T, object>>> Includes { get; } = new List<Expression<Func<T, object>>>();

        protected void AddInclude(Expression<Func<T, object>> includeExpression)
         {
            Includes.Add(includeExpression);
         }


        //ordering

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
        {
            OrderByDescending = orderByDescExpression;
        }
    }
}
