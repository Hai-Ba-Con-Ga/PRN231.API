using BusinessObject.Common.PagedList;
using BusinessObject.Model;
using Repository.Common;
using Repository.Helper;
using Repository.Interface;
using System.Data.Entity;

namespace Repository.Repository
{
    public class DeviceRepository : GenericRepository<Device>, IDeviceRepository
    {
        public DeviceRepository(Microsoft.EntityFrameworkCore.DbContext context) : base(context)
        {
        }

        public override Task<IPagedList<Device>> SearchAsync(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            throw new NotImplementedException();
        }

        public override async Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, string orderBy)
        {
            var result = await _dbSet.AsNoTracking()
                 .WhereWithExist(x =>  string.IsNullOrEmpty(keySearch) 
                    || x.DeviceName.Equals(keySearch, StringComparison.OrdinalIgnoreCase)
                 )
                 .Include(x => x.DeviceType)
                 .Include(x => x.Owner)
                 .AddOrderByString(orderBy)
                 .SelectWithField<Device, TResult>()
                 .ToPagedListAsync(pagingQuery);

            return result;
        }

        public async Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch,
            PagingQuery pagingQuery, string orderBy, int ownerID) where TResult : class
        {
            var result = await _dbSet.AsNoTracking()
                 .WhereWithExist(x => (string.IsNullOrEmpty(keySearch)
                        || x.DeviceName.Equals(keySearch, StringComparison.OrdinalIgnoreCase))
                    && x.OwnerId == ownerID
                 )
                 .Include(x => x.DeviceType)
                 .Include(x => x.Owner)
                 .AddOrderByString(orderBy)
                 .SelectWithField<Device, TResult>()
                 .ToPagedListAsync(pagingQuery);

            return result;
        }
    }
}
