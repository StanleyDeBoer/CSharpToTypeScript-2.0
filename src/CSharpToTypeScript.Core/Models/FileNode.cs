using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CSharpToTypeScript.Core.Options;
using CSharpToTypeScript.Core.Transformations;
using CSharpToTypeScript.Core.Utilities;
using static CSharpToTypeScript.Core.Utilities.StringUtilities;

namespace CSharpToTypeScript.Core.Models
{
    internal class FileNode
    {
        public FileNode(IEnumerable<RootNode> rootNodes)
        {
            RootNodes = rootNodes;
        }

        public IEnumerable<RootNode> RootNodes { get; }

        public IEnumerable<string> Requires => RootNodes.SelectMany(r => r.Requires).Distinct();

        public IEnumerable<string> Imports => Requires.Except(RootNodes.Select(r => r.Name));

        public string WriteTypeScript(CodeConversionOptions options)
        {
            var context = new Context();

            return // imports
                (Imports.Select(i =>
                        // type
                        "import { " + i.TransformIf(options.RemoveInterfacePrefix, StringUtilities.RemoveInterfacePrefix) + " }"
                        // module
                        + " from " + ("./" + ModuleNameTransformation.Transform(i, options)).InQuotes(options.QuotationMark) + ";")
                    .Distinct().LineByLine()
                + EmptyLine).If(Imports.Any() && options.ImportGenerationMode == ImportGenerationMode.Simple)
                + (Imports.Select(i => i.TransformIf(options.RemoveInterfacePrefix, StringUtilities.RemoveInterfacePrefix)).OrderBy(i => i)
                          //  .Where(i => options.Imports?.ContainsKey(i) == true)
                          .Select(i => {
                                if (options.ImportGenerationMode == ImportGenerationMode.Config && options.Imports?.ContainsKey(i) != true)
                              {
                                  throw new Exception($"No import config found for {i}");
                              }
                              string importPath = Path.ChangeExtension(Path.GetRelativePath(Path.GetDirectoryName(options.OutputLocation), options.Imports[i]), null);
                              // type
                              return "import { " + i + " }"
                              // module
                              + " from " + ("./".If(!importPath.StartsWith(".")) + importPath.Replace("\\","/")).InQuotes(options.QuotationMark) + ";";
                        })

                    .Distinct().LineByLine()
                + EmptyLine).If(Imports.Any() && options.ImportGenerationMode == ImportGenerationMode.Config)
                // types
                + RootNodes.WriteTypeScript(options, context).ToEmptyLineSeparatedList()
                // empty line at the end
                + NewLine.If(options.AppendNewLine);
        }
    }
}