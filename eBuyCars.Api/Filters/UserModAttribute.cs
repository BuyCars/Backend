using eBuyCars.BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace eBuyCars.Api.Filters
{
    public class UserModAttribute : ActionFilterAttribute
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

            context.HttpContext.Items["CurrentUser"] = user;

            base.OnActionExecuting(context);
        }
    }
}
