using System.IO;
using System.Linq;
using System.Threading.Tasks;
using asp_mvc.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ValidationFilterAttribute : IActionFilter
{


    public ValidationFilterAttribute()
    {
        System.Console.WriteLine("constr");

    }
    public void OnActionExecuting(ActionExecutingContext context)
    {
        System.Console.WriteLine("---------------------------------------");
        System.Console.WriteLine("BEFORE:");
        //https://code-maze.com/action-filters-aspnetcore/
        var param = context.ActionArguments.SingleOrDefault(p => p.Value is MyClass);
        if (param.Value == null)
        {
            System.Console.WriteLine("Can not be null!");
            context.Result = new BadRequestObjectResult("Object is null");
            return;
        }

        if (!context.ModelState.IsValid)
        {
            System.Console.WriteLine("Model is invalid!");
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
    }
}