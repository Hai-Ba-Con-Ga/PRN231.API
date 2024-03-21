using BusinessObject.Common.PagedList;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Repository.Helper;

namespace Repository.Repository;

public class DatumRepository : GenericRepository<CollectedDatum>
{
    public DatumRepository(DbContext context) : base(context)
    {
    }

    public override Task<IPagedList<CollectedDatum>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        return _dbSet.AsNoTracking()
            .WhereWithExist(p => (string.IsNullOrEmpty(keySearch) ||
                                  p.DataValue.Contains(keySearch)))
            .AddOrderByString(orderBy)
            .ToPagedListAsync(pagingQuery);
    }

    public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        return _dbSet.AsNoTracking()
            .WhereWithExist(p => (string.IsNullOrEmpty(keySearch) ||
                                  p.DataValue.Contains(keySearch)))
            .Include(x => x.Device)
            .AddOrderByString(orderBy)
            .SelectWithField<CollectedDatum, TResult>()
            .ToPagedListAsync(pagingQuery);
    }
}