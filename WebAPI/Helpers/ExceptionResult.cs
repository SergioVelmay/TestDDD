using System;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Helpers
{
    public class ExceptionResult : BaseActionResult
    {
        private Exception _ex;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="statusCode"></param>
        public ExceptionResult(Exception ex, int statusCode = 500, string contentType = "application/json") : base(statusCode, contentType)
        {
            _ex = ex;
            _statusCode = statusCode;
            _contentType = contentType;
        }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get { return _ex.Message; } }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ActionContext context)
        {
            DoResponse<ExceptionResult>(context.HttpContext.Response, this);
        }
    }
}
