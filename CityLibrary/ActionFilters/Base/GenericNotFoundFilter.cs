using CityLibraryInfrastructure.BaseInterfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CityLibrary.ActionFilters.Base
{
    public class GenericNotFoundFilter<T> : IAsyncActionFilter where T : IBaseCheckService
    {
        private readonly T _service;
        public GenericNotFoundFilter(T service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var actionArgument = context.ActionArguments["dto"];
            Type modelType = actionArgument!.GetType();

            string modelIdPropName = modelType.GetProperties()
                                              .First(x => Attribute.IsDefined(x, typeof(KeyAttribute)))
                                              .Name;

            var Id = (IConvertible)modelType.GetProperty(modelIdPropName)!
                                            .GetValue(actionArgument);

            bool doesExist = await _service.DoesEntityExistAsync(Id);

            if (!doesExist)
            {
                string fieldName = modelType.GetProperty(modelIdPropName)!
                                            .GetCustomAttributes(false)
                                            .OfType<DisplayNameAttribute>()
                                            .First()
                                            .DisplayName;

                context.Result = new NotFoundObjectResult($"{fieldName} was not found.");
                return;
            }
            else
            {
                await next();
            }
        }
    }
}
