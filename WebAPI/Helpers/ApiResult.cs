using System;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Helpers
{
    public class ApiResult<T> : BaseActionResult where T : class
    {
        private T _item;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="statusCode"></param>
        public ApiResult(T item, int statusCode = 200, string contentType = "application/json") : base(statusCode, contentType)
        {
            _item = item;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ActionContext context)
        {
            DoResponse(context.HttpContext.Response, _item);
        }
    }
}
