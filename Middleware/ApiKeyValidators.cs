using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Middleware
{
    public class ApiKeyValidatorsMiddleware
    {
        private readonly RequestDelegate _next;
        

        public ApiKeyValidatorsMiddleware(RequestDelegate next)
        {
            _next = next;
            
        }

        public async Task Invoke(HttpContext context)
        {
            Object response = JsonConvert.SerializeObject(new Response
            {
                success = false,
                message = "X-Organizasyon-Token bilgisi geçerli değil. İstek yaparken header içinde doğru olarak gönderdiğinizden emin olunuz."
            });
            
            if (!context.Request.Headers.Keys.Contains("X-Organizasyon-Token"))
            {
                context.Response.StatusCode = 400; //Bad Request                
                

                context.Response.ContentType = "application/json; charset=utf-8";
                await context.Response.WriteAsync(response.ToString());
                return;
            }
            else
            {
                if (!ApiKeyValidatorsExtension.CheckValidApiKey(context.Request.Headers["X-Organizasyon-Token"]))
                {
                    context.Response.StatusCode = 401; //UnAuthorized
                    await context.Response.WriteAsync(response.ToString());
                    return;
                }
            }

            await _next.Invoke(context);
        }
    }

    #region ExtensionMethod
    public static class ApiKeyValidatorsExtension
    {
        public static IApplicationBuilder ApplyApiKeyValidation(this IApplicationBuilder app)
        {
            app.UseMiddleware<ApiKeyValidatorsMiddleware>();
            return app;
        }

        public static bool CheckValidApiKey(string reqkey)
        {
            var apiKeyList = new List<string>
            {
                "28236d8ec201df516d0f6472d516d72d",
                "38236d8ec201df516d0f6472d516d72c",
                "48236d8ec201df516d0f6472d516d72b"
            };

            if (apiKeyList.Contains(reqkey))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    #endregion
}
