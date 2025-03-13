using System.Collections.Generic;
using CSharpToTypeScript.Core.Options;

namespace CSharpToTypeScript.Core.Models
{
    public class Context
    {
        public IEnumerable<string> GenericTypeParameters { get; set; }
        public OutputType OutputType { get; set; }

        public Context Clone() => (Context)MemberwiseClone();
    }
}