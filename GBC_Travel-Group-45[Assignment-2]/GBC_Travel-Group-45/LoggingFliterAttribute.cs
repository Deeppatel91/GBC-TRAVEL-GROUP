using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.IO;

namespace GBC_Travel_Group_45.Filters
{
    public class LoggingFilterAttribute : ActionFilterAttribute
    {
        private readonly string _actionName;

        public LoggingFilterAttribute(string actionName)
        {
            _actionName = actionName;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = filterContext.HttpContext.User.Identity.Name;
            var requestPath = filterContext.HttpContext.Request.Path;
            var queryString = filterContext.HttpContext.Request.QueryString;

            // Log the start of the action
            Log($"Action Started: {_actionName}, User: {user}, Path: {requestPath}, Query: {queryString}");

            base.OnActionExecuting(filterContext);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var user = filterContext.HttpContext.User.Identity.Name;
            var requestPath = filterContext.HttpContext.Request.Path;
            var queryString = filterContext.HttpContext.Request.QueryString;

            // Check if the booking ID was present before the action execution
            var bookingIdBefore = filterContext.HttpContext.Request.Query["bookingId"];

            base.OnActionExecuted(filterContext);

            // Check if the booking ID is present after the action execution
            var bookingIdAfter = filterContext.HttpContext.Request.Query["bookingId"];

            if (!string.IsNullOrEmpty(bookingIdBefore) && !string.IsNullOrEmpty(bookingIdAfter))
            {
                // If a booking ID was present before and after the action execution, consider it as a successful booking
                Log($"Action Completed: {_actionName}, User: {user}, Path: {requestPath}, Query: {queryString}, BookingAttempt: Successful");
            }
        }


        private void Log(string message)
        {
            string logPath = "Logs/logs.txt";
            string logMessage = $@"
================================================================================
{DateTime.Now}: 
{message}
================================================================================
";

            try
            {
                // Log to console for debugging
                Console.WriteLine($"Logging: {logMessage}");

                // Write log message to the log file
                using (StreamWriter writer = new StreamWriter(logPath, true))
                {
                    writer.WriteLine(logMessage);
                }
            }
            catch (Exception ex)
            {
                // Handle logging error
                Console.WriteLine($"Error logging message: {ex.Message}");
            }
        }
    }
}