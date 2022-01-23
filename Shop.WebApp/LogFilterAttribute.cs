using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Shop.WebApp
{
    public class LogFilterAttribute : ExceptionFilterAttribute
    {
        ILog logger;
        public LogFilterAttribute()
        {
            logger = LogManager.GetLogger(typeof(LogFilterAttribute));
        }
        public override void OnException(ExceptionContext Context)
        {
            logger.Error(Context.Exception.Message + "\n" + Context.Exception.StackTrace);
        }
    }
}
