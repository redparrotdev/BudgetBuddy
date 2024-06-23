
using BB.Finances.Core.System;
using BB.Finances.Data.Exceptions;
using Serilog;
using System.Text;

namespace BB.Finances.WebAPI.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder($"!----- {ex.Message} -----!");
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);
                sb.Append(ex.StackTrace);
                sb.Append(Environment.NewLine);
                sb.Append(Environment.NewLine);

                Log.Error(sb.ToString());

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = DetermineStatusCode(ex);

                await context.Response.WriteAsync(new ErrorModel()
                {
                    StatusCode = context.Response.StatusCode,
                    Message = ex.Message
                }.ToString());
            }
        }

        private static int DetermineStatusCode(Exception ex) => ex switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            RaceException => StatusCodes.Status400BadRequest,
            GeneralException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };
    }
}
