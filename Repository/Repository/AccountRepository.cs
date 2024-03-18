using BusinessObject.Common.Enums;
using BusinessObject.Common.PagedList;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Repository.Helper;

namespace Repository.Repository;

public class AccountRepository : GenericRepository<Account>
{
    public AccountRepository(DbContext context) : base(context)
    {
    }


    public override Task<IPagedList<Account>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
    {
        return _dbSet.AsNoTracking()
            .WhereWithExist(p => (string.IsNullOrEmpty(keySearch) ||
                                  p.Username.Contains(keySearch)))
            .AddOrderByString(orderBy)
            .ToPagedListAsync(pagingQuery);
    }

    public override Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery,
        string orderBy)
    {
        return _dbSet.AsNoTracking()
            .WhereWithExist(p => (string.IsNullOrEmpty(keySearch) ||
                                  p.Username.Contains(keySearch)))
            .AddOrderByString(orderBy)
            .SelectWithField<Account, TResult>()
            .ToPagedListAsync(pagingQuery);
    }
}