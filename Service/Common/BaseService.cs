using System.Net;
using BusinessObject.Common;
using BusinessObject.Common.PagedList;
using Repository.Common;

namespace Service.Common;

public class BaseService : IDisposable
{
    protected readonly IUnitOfWork _unitOfWork;

    public BaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    protected ApiResponse<T> Success<T>(T data = default, string? message = null)
    {
        return new ApiResponse<T>
        {
            Data = data,
            StatusCode = HttpStatusCode.OK,
            Message = message
        };
    }

    protected PagingApiResponse<T> Success<T>(IPagedList<T> data = default, string message = null)
    {
        return new PagingApiResponse<T>
        {
            StatusCode = HttpStatusCode.OK,
            Message = message,
            PagingData = new PagingResponse<T>
            {
                Data = data,
                CurrentPage = data.CurrentPage,
                PageSize = data.PageSize,
                TotalCount = data.TotalCount,
                TotalPages = data.TotalPages
            }
        };
    }

    protected ApiResponse<T> Success<T>(string? message = null)
    {
        return new ApiResponse<T>
        {
            StatusCode = HttpStatusCode.OK,
            Message = message
        };
    }

    protected ApiResponse<T> Failed<T>(string message, HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        return new ApiResponse<T>
        {
            StatusCode = statusCode,
            Message = message
        };
    }

    protected PagingApiResponse<T> PagingFailed<T>(string message,
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError)
    {
        return new PagingApiResponse<T>
        {
            StatusCode = statusCode,
            Message = message
        };
    }

    #region Destructor

    private bool _disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
        }

        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~BaseService()
    {
        Dispose(false);
    }

    #endregion Destructor
}