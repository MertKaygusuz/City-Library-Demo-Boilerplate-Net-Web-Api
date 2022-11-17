using CityLibraryInfrastructure.BaseInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CityLibraryInfrastructure.Resources;
using Microsoft.Extensions.Localization;

namespace CityLibrary.ActionFilters.Base
{
    public class GenericNotFoundFilter<T> : IAsyncActionFilter where T : IBaseCheckService
    {
        private readonly T _service;
        private readonly IStringLocalizer<DisplayNameResource> _localizer;
        public GenericNotFoundFilter(T service, IStringLocalizer<DisplayNameResource> localizer)
        {
            _service = service;
            _localizer = localizer;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionArgument = context.ActionArguments["dto"];
            Type modelType = actionArgument!.GetType();

            string modelIdPropName = modelType.GetProperties()
                                              .First(x => Attribute.IsDefined(x, typeof(KeyAttribute)))
                                              .Name;

            var id = (IConvertible)modelType.GetProperty(modelIdPropName)!
                                            .GetValue(actionArgument);

            bool doesExist = await _service.DoesEntityExistAsync(id);

            if (!doesExist)
            {
                string fieldName = modelType.GetProperty(modelIdPropName)!
                                            .GetCustomAttributes(false)
                                            .OfType<DisplayNameAttribute>()
                                            .First()
                                            .DisplayName;

                string localizedFieldName = _localizer[fieldName];

                context.Result = new NotFoundObjectResult(string.Format(_localizer["Display_Name_Not_Found"], localizedFieldName));
                return;
            }
            else
            {
                await next();
            }
        }
    }
}
