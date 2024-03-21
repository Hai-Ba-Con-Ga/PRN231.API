using BusinessObject.Common.PagedList;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Repository.Helper;

namespace Repository.Repository;

public class DataTypeRepository : GenericRepository<CollectedDataType>
{
    public DataTypeRepository(DbContext context) : base(context)
    {
    }

    public override Task<IPagedList<CollectedDataType>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        return _dbSet.AsNoTracking()
            .WhereWithExist(p => (string.IsNullOrEmpty(keySearch) ||
                                  p.DataTypeName.Contains(keySearch)))
            .AddOrderByString(orderBy)
            .ToPagedListAsync(pagingQuery);
    }

    public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        return _dbSet.AsNoTracking()
            .WhereWithExist(p => (string.IsNullOrEmpty(keySearch) ||
                                  p.DataTypeName.Contains(keySearch)))
            .AddOrderByString(orderBy)
            .SelectWithField<CollectedDataType, TResult>()
            .ToPagedListAsync(pagingQuery);
    }
}