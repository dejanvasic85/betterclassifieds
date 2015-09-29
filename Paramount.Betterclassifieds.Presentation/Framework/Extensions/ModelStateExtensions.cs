using System.Collections;
using System.Web.Mvc;
using System.Linq;

namespace Paramount.Betterclassifieds.Presentation
{
    public static class ModelStateExtensions
    {
        public static IEnumerable ToErrors(this ModelStateDictionary modelState)
        {
            return modelState.ToDictionary(kvp => kvp.Key,
                kvp => kvp.Value.Errors
                                .Select(e => e.ErrorMessage).ToArray())
                                .Where(m => m.Value.Any());
        }
    }
}