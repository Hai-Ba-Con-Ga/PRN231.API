using System.Net;
using BusinessObject.Common;
using Newtonsoft.Json;
using Service.Exceptions;

namespace WebAPI.Middleware;

public class ExceptionMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            ApiResponse<string> response = new ApiResponse<string>();
                
            switch (ex)
            {
                case CustomException e:
                    response.StatusCode = e.StatusCode;
                    response.Message = e.Message;
                    break;
                default:
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = ex.Message;
                    break;
            }
            context.Response.StatusCode = (int)response.StatusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
        
        
}