using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PatientRegistrations.Domain.Helpers;


namespace StoreOfBuild.Web.Filters
{
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IModelMetadataProvider _modelMetadaProvider;

        public CustomExceptionFilter(IModelMetadataProvider modelMetadaProvider)
        {
            _modelMetadaProvider = modelMetadaProvider;
        }

        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = 500;
            var message = context.Exception is DomainException ? context.Exception.Message : "An error ocorred";
            context.Result = new JsonResult(message);
            context.ExceptionHandled = true;

            base.OnException(context);
        }

    }
}