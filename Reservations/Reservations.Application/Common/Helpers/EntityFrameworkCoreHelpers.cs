using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Reservations.Application.Common.Helpers
{
    public static class EntityFrameworkCoreHelpers
    {
        public static IQueryable<TEntity> IncludeMany<TEntity, TProperty>(
            [NotNull] this IQueryable<TEntity> source,
            [NotNull] Expression<Func<TEntity, TProperty>> navigationPropertyPath,
            [NotNull] params Expression<Func<TProperty, object>>[] nextProperties
            )
            where TEntity : class
        {
            foreach (var nextProperty in nextProperties)
            {
                source = source.Include(navigationPropertyPath)
                    .ThenInclude(nextProperty);
            }

            return source;
        }

        public static IQueryable<TEntity> IncludeMany<TEntity, TProperty>(
            [NotNull] this IQueryable<TEntity> source,
            [NotNull] Expression<Func<TEntity, IEnumerable<TProperty>>> navigationPropertyPath,
            [NotNull] params Expression<Func<TProperty, object>>[] nextProperties)
            where TEntity : class
        {
            foreach (var nextProperty in nextProperties)
            {
                source = source.Include(navigationPropertyPath)
                    .ThenInclude(nextProperty);
            }

            return source;
        }
    }
}
