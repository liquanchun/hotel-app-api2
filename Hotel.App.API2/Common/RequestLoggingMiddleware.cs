using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.App.API2
{
    public class RequestLoggingMiddleware
    {   
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            _logger.LogInformation("request.Path:" + request.Path);
            //_logger.LogInformation("request.Headers:" + JsonConvert.SerializeObject(request.Headers, Formatting.Indented));
            _logger.LogInformation("request.QueryString:" + JsonConvert.SerializeObject(request.QueryString, Formatting.Indented));

            var injectedRequestStream = new MemoryStream();            

            try
            {
                var requestLog = $"request HttpMethod: {context.Request.Method}, Path: {context.Request.Path}";

                using (var bodyReader = new StreamReader(context.Request.Body))
                {
                    var bodyAsText = bodyReader.ReadToEnd();
                    if (string.IsNullOrWhiteSpace(bodyAsText) == false)
                    {
                        requestLog += $", Body : {bodyAsText}";
                    }

                    var bytesToWrite = Encoding.UTF8.GetBytes(bodyAsText);
                    injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                    injectedRequestStream.Seek(0, SeekOrigin.Begin);
                    context.Request.Body = injectedRequestStream;
                }

                _logger.LogInformation(requestLog);

                await _next.Invoke(context);                                
            }
            finally
            {
                injectedRequestStream.Dispose();
            }
        }       
    }
}
