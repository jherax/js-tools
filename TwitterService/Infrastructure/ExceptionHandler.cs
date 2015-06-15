using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace TwitterService.Infrastructure {
    public sealed class ExceptionHandler : ExceptionFilterAttribute {

        /// <summary>
        /// Handles the captured exceptions
        /// </summary>
        /// <see cref="http://www.asp.net/web-api/overview/testing-and-debugging/exception-handling"/>
        /// <see cref="http://weblogs.asp.net/fredriknormen/asp-net-web-api-exception-handling"/>
        /// <param name="actionContext">The context where the action throws an exception</param>
        /// <!-- Created by: David Rivera -->
        public override void OnException(HttpActionExecutedContext actionContext) {
            //base.OnException(actionContext);

            Logger.GetInstance.LogException(actionContext.Exception);
            /*
             * http://goo.gl/R68N89 (stackoverflow)
            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError) {
                Content = new StringContent("Ha ocurrido un error. Revise el log de excepciones."),
                ReasonPhrase = "Error interno del servidor"
            });
            */
            throw new HttpResponseException(actionContext.Request.CreateResponse(
                HttpStatusCode.InternalServerError,
                new { Message = "Ha ocurrido un error. Revise el log de excepciones." }
            ));
        }

    }//end class
}//end namespace