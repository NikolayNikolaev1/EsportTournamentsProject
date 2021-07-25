namespace EsportsTournaments.Core.Filters
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System.Linq;

    /// <summary>
    /// This action filter validate the model state, if the action parameter name contains "model" and the action class inherits "Controller" class.
    /// </summary>
    public class ValidateModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var controller = context.Controller as Controller;

                var model = context
                    .ActionArguments
                    .FirstOrDefault(a => a.Key.ToLower().Contains("model"))
                    .Value;

                if (controller == null || model == null)
                {
                    return;
                }

                context.Result = controller.View();
            }
        }
    }
}
