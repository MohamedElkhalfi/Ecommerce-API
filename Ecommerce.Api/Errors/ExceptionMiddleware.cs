using System;
using System.Net;
using System.Threading.Tasks;
using Ecommerce.Core.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization; 

namespace Ecommerce.Api.Errors
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _nextRequestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private const string TECHNICAL_ERROR = "TECHNICAL_ERROR";
        private const string CONTENT_TYPE = "application/json";

        public ExceptionMiddleware(RequestDelegate nextRequestDelegate, ILogger<ExceptionMiddleware> logger)
        {
            _logger = logger;
            _nextRequestDelegate = nextRequestDelegate ?? throw new ArgumentNullException(nameof(nextRequestDelegate));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline
                await _nextRequestDelegate(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var anomalyCode = TECHNICAL_ERROR;

            if (exception is FunctionalException functionalException)
            {
                _logger.LogInformation(functionalException.Message);
                context.Response.StatusCode = (int)functionalException.HttpStatusCode;
                anomalyCode = functionalException.ErrorCode;
            }
            else
            {
                _logger.LogError(new TechnicalException(exception), TECHNICAL_ERROR);
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var responseMessage = BuildResponseMessage(anomalyCode, exception.Message);

            context.Response.ContentType = CONTENT_TYPE;

            return context.Response.WriteAsync(responseMessage);
        }

        private string BuildResponseMessage(string anomalyCode, string anomalyMessage)
        {
            var response = new
            {
                anomaly = new Anomaly
                {
                    Code = anomalyCode,
                    Label = anomalyMessage
                }
            };

            var responseMessage = JsonConvert.SerializeObject(response, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            return responseMessage;
        }
    }

    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
