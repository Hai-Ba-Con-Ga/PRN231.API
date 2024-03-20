using BusinessObject.Common.PagedList;
using BusinessObject.Model;
using Repository.Common;

namespace Repository.Interface
{
    public interface IDeviceRepository : IGenericRepository<Device>
    {
        Task<IPagedList<TResult>> SearchAsync<TResult>(string keySearch, PagingQuery pagingQuery, 
            string orderBy, int ownerID) where TResult : class;
    }
}
