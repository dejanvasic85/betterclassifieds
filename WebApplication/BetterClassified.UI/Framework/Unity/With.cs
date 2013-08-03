using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace BetterClassified
{
    public static class With
    {
        public static ParameterOverride Parameter<TParamValue>(string parameterName, TParamValue value)
        {
            return new ParameterOverride(parameterName, value);
        }
    }
}
