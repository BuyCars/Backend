using BuyCars.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BuyCars.Api.Filters
{
    public class AdminModAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var token = context.HttpContext.Request.Headers["X-KEY"].FirstOrDefault();

            if (string.IsNullOrEmpty(token))
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    message = "Требуется авторизация (X-KEY header)"
                });
                return;
            }

            var bl = new BussinesLogic();
            var session = bl.GetSessionBL();
            var user = session.GetUserByCookie(token);

            if (user == null)
            {
                context.Result = new UnauthorizedObjectResult(new
                {
                    message = "Недействительный или истёкший токен"
                });
                return;
            }

            if (user.Role != "admin")
            {
                context.Result = new ObjectResult(new
                {
                    message = "Доступ запрещён: требуются права администратора"
                })
                { StatusCode = 403 };
                return;
            }

            context.HttpContext.Items["CurrentUser"] = user;

            base.OnActionExecuting(context);
        }
    }
}
