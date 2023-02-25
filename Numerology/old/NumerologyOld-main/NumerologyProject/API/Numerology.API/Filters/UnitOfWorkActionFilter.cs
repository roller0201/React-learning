using Common.UOW;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Numerology.API.Filters
{
    public class UnitOfWorkActionFilter : ActionFilterAttribute
    {
        private readonly IUnitOfWork uow;

        public UnitOfWorkActionFilter(IUnitOfWork _uow)
        {
            uow = _uow;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            uow.BeginTransaction();
        }

        public override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Exception == null)
            {
                // commit if no exceptions
                uow.Commit();
            }
            else
            {
                // rollback if exception
                uow.Rollback();
            }
        }
    }
}
