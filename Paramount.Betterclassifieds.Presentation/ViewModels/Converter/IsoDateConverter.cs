using System;
using AutoMapper;

namespace Paramount.Betterclassifieds.Presentation.ViewModels
{
    public class IsoDateConverter : IValueResolver
    {
        public ResolutionResult Resolve(ResolutionResult source)
        {
            if (source.Type == typeof(DateTime))
            {
                var destintationValue = (DateTime)source.Value;
                source.Context.SetResolvedDestinationValue(destintationValue.ToIsoDateString());
                return new ResolutionResult(source.Context);
            }

            throw new ArgumentException("source type must be of type DateTime");
        }
    }
}