using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Onboard.Web.UI
{
    public class OnboardController : Controller
    {
        private readonly ICompositeViewEngine _viewEngine;

        public OnboardController(ICompositeViewEngine viewEngine)
        {
            this._viewEngine = viewEngine;
        }

        /// <summary>Returns a <see cref="JsonResult"/> with the specified message.</summary>
        /// <param name="isSuccess"><c>true</c> is result indicates success, otherwise <c>false</c>.</param>
        /// <param name="message">The string message to include in the result.</param>
        /// <param name="errors">List of errors when <paramref name="isSuccess"/> is <c>false</c>.</param>
        /// <returns>A <see cref="JsonResult"/> instance.</returns>
        public JsonResult GetJsonResult(bool isSuccess, string message, IList<string> errors)
        {
            return this.Json(
                new
                {
                    Success = isSuccess,
                    Message = message,
                    Errors = errors
                });
        }

        /// <summary>Return the list of error messages contained in <see cref="ModelState"/>.</summary>
        /// <returns>The list of model errors.</returns>
        protected List<string> ModelErrors()
        {
            var errors = ModelState.Values
                                   .Where(r => r.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid)
                                   .Select(r => r.Errors)
                                   .First()
                                   .Select(r => r.ErrorMessage); 

            return errors.ToList();
        }

        /// <summary>Renders a (partial view) List to string.</summary>
        /// <param name="viewName">(Partial view) List to render</param>
        /// <param name="model">(Partial view) model to render</param>
        /// <returns>Rendered (partial view) List as string</returns>
        protected string RenderPartialViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.DisplayName;

            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = _viewEngine.FindView(ControllerContext, viewName, false);

                ViewContext viewContext = new ViewContext(
                    ControllerContext,
                    viewResult.View,
                    ViewData,
                    TempData,
                    sw,
                    new HtmlHelperOptions() //Added this parameter in
                );

                //Everything is async now!
                var t = viewResult.View.RenderAsync(viewContext);
                t.Wait();

                return sw.GetStringBuilder().ToString();
            }
        }
    }
}
