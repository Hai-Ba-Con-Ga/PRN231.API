using BusinessObject.Common.PagedList;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Repository.Helper;

namespace Repository.Repository;

public class DeviceTypeRepository : GenericRepository<DeviceType>
{
    public DeviceTypeRepository(DbContext context) : base(context)
    {
    }

    public override Task<IPagedList<DeviceType>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        return _dbSet.AsNoTracking()
            .WhereWithExist(p => (string.IsNullOrEmpty(keySearch) ||
                                  p.TypeName.Contains(keySearch)))
            .AddOrderByString(orderBy)
            .ToPagedListAsync(pagingQuery);
    }

    public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        return _dbSet.AsNoTracking()
            .WhereWithExist(p => (string.IsNullOrEmpty(keySearch) ||
                                  p.TypeName.Contains(keySearch)))
            .Include(x => x.Devices)
            .AddOrderByString(orderBy)
            .SelectWithField<DeviceType, TResult>()
            .ToPagedListAsync(pagingQuery);
    }
}