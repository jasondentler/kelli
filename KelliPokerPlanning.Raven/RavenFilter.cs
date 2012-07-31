using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Raven.Client;

namespace KelliPokerPlanning
{
    public class RavenFilter : IActionFilter
    {
        private readonly IDocumentSession _session;

        public RavenFilter(IDocumentSession session)
        {
            _session = session;
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            if (filterContext.Exception != null && !filterContext.ExceptionHandled)
                return; // Don't save if an unhandled error occurred

            _session.SaveChanges();
        }
    }
}
