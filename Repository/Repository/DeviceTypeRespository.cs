using BusinessObject.Common.PagedList;
using BusinessObject.Model;
using Microsoft.EntityFrameworkCore;
using Repository.Common;
using Repository.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repository
{
    internal class DeviceTypeRespository : GenericRepository<DeviceType>
    {
        public DeviceTypeRespository(DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<DeviceType>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override async Task<IPagedList<TResult>> SearchAsync<TResult>(string? keySearch, PagingQuery pagingQuery, string? orderBy)
        {
            var result = await _dbSet.AsNoTracking()
                .WhereWithExist(i => string.IsNullOrEmpty(keySearch))
                .AddOrderByString(orderBy)
                .SelectWithField<DeviceType, TResult>()
                .ToPagedListAsync(pagingQuery);
            return result;
        }
    }
}
