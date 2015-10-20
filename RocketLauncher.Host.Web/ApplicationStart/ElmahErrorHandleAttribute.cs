using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;

using Elmah;

namespace RocketLauncher.Host.Web.ApplicationStart
{
    /// <summary>
    /// Attribute for handling elmah errors and putting them into the log.
    /// </summary>
    public class ElmahHandleErrorAttribute : ExceptionFilterAttribute
    {
        /// <summary>
        /// Raises the exception event.
        /// </summary>
        /// <param name="actionExecutedContext">The context for the action.</param>
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            base.OnException(actionExecutedContext);

            ErrorSignal.FromCurrentContext().Raise(actionExecutedContext.Exception);
        }
    }
}