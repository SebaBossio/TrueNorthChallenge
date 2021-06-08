using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TrueNorthChallenge.Contracts;
using TrueNorthChallenge.Common.DTO.Generic;
using TrueNorthChallenge.Common.Exceptions;

namespace TrueNorthChallenge.Middlewares
{
    public class TrueNorthMiddleware
    {
        private readonly RequestDelegate _next;

        public TrueNorthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var response = new ResponseDTO<Object>();
            Stream originBody = context.Response.Body;
            context.Response.Body = new MemoryStream();

            try
            {
                await _next(context);

                context.Response.Body.Position = 0;
                string responseBody = new StreamReader(context.Response.Body).ReadToEnd();
                context.Response.Body.Position = 0;

                response.Success = true;
                response.Data = JsonConvert.DeserializeObject(responseBody);
            }
            catch (TrueNorthSecureException ex)
            {
                response.Success = false;
                response.Message = ex.ToString();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "An unhandled exception was thrown.";
                // ToDo : Log this
                // We can recieve some other inyections from the ServiceProvider, for example the instance of the logger.
            }

            var json = JsonConvert.SerializeObject(response);
            var requestContent = new StringContent(json, Encoding.UTF8, "application/json");
            context.Response.Body = await requestContent.ReadAsStreamAsync();

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            context.Response.Body.CopyTo(originBody);
            context.Response.Body = originBody;
        }
    }
}
