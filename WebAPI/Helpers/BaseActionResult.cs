using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebAPI.Helpers
{
    public class BaseActionResult : ActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        protected int _statusCode;
        protected string _contentType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="statusCode"></param>
        public BaseActionResult(int statusCode, string contentType)
        {
            _statusCode = statusCode;
            _contentType = contentType;
        }

        private HttpResponse PrepareResponse(HttpResponse response)
        {
            response.StatusCode = _statusCode;
            response.ContentType = _contentType;

            return response;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <param name="result"></param>
        protected void DoResponse<T>(HttpResponse response, T result)
        {
            PrepareResponse(response).WriteAsync(JsonConvert.SerializeObject(result));
        }
    }
}
