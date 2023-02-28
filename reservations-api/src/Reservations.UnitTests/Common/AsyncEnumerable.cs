using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Reservations.Application.UnitTests.Common
{
    public class AsyncEnumerable<TEntity> : EnumerableQuery<TEntity>, IAsyncEnumerable<TEntity>, IQueryable<TEntity>
    {
    public AsyncEnumerable(IEnumerable<TEntity> enumerable)
        : base(enumerable)
    {
    }

    public AsyncEnumerable(Expression expression)
        : base(expression)
    {
    }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator()
    {
        return new AsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());
    }
    public IAsyncEnumerator<TEntity> GetEnumerator()
    {
        return GetAsyncEnumerator();
    }

    public IAsyncEnumerator<TEntity> GetAsyncEnumerator(CancellationToken cancellationToken = default)
    {
        return new AsyncEnumerator<TEntity>(this.AsEnumerable().GetEnumerator());
    }

    IQueryProvider IQueryable.Provider
    {
        get { return new AsyncQueryProvider<TEntity>(this); }
    }
    }
}
