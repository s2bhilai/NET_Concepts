using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace App_UrlChecker.Infrastructure
{
    public static class ControllerExtensions
    {
        public static IActionResult RedirectTo<TController>(
            this Controller controller,
            Expression<Action<TController>> redirectExpression)
        {
            if(redirectExpression.Body.NodeType != ExpressionType.Call)
            {
                throw new InvalidOperationException
                    ($"The provided expression is not a valid method call: {redirectExpression.Body}");
            }

            //Extract Action Name
            var methodCallExpression = (MethodCallExpression)redirectExpression.Body;
            var actionName = GetActionName(methodCallExpression);
            var controllerName = 
                typeof(TController).Name.Replace(nameof(Controller), string.Empty);

            var routeValues = ExtractRouteValues(methodCallExpression);

            return controller.RedirectToAction(actionName, controllerName, routeValues);
        }

        private static string GetActionName(MethodCallExpression expression)
        {
            var methodName = expression.Method.Name;

            var actionName = expression
                .Method
                .GetCustomAttributes(true)
                .OfType<ActionNameAttribute>()
                .FirstOrDefault()
                ?.Name;

            return actionName ?? methodName;
        }

        private static RouteValueDictionary ExtractRouteValues(MethodCallExpression expression)
        {
            var names = expression.Method
                .GetParameters()
                .Select(p => p.Name)
                .ToArray();

            var values = expression.Arguments
                .Select(arg =>
                {
                    if (arg.NodeType == ExpressionType.Constant)
                    {
                        var constantExpression = (ConstantExpression)arg;
                        return constantExpression.Value;
                    }

                    //() => (object)arg
                    var convertExpression = Expression.Convert(arg, typeof(object));
                    var funcExpression = Expression.Lambda<Func<object>>(convertExpression);
                    return funcExpression.Compile().Invoke();

                }).ToArray();

            var routeValueDictionary = new RouteValueDictionary();

            for (int i = 0; i < names.Length; i++)
            {
                routeValueDictionary.Add(names[i], values[i]);
            }

            return routeValueDictionary;
        }
    }
}
