using System.Collections.Generic;
using CSharpToTypeScript.Core.Constants;
using CSharpToTypeScript.Core.Models.TypeNodes;

namespace CSharpToTypeScript.Core.Services.TypeConversionHandlers
{
    internal class DynamicConverter : BasicTypeConverterBase<Any>
    {
        protected override IEnumerable<string> ConvertibleFromPredefined => new[]
        {
            "dynamic"
        };

        protected override IEnumerable<string> ConvertibleFromIdentified => new[]
        {
            "dynamic"
        };
    }
}