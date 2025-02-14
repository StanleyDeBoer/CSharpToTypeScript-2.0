using CSharpToTypeScript.CLITool.Utilities;
using CSharpToTypeScript.CLITool.Validation;
using CSharpToTypeScript.Core.Options;
using McMaster.Extensions.CommandLineUtils;
using System.Collections.Generic;

namespace CSharpToTypeScript.CLITool.Commands
{
    [InputExists]
    [OutputMatchesInput]
    [ValidIndentation]
    public abstract class CommandBase
    {
        protected CommandBase() => OnBeforeArgumentsSet();

        [Argument(0, Description = "Input file or directory path")]
        public string Input { get; set; } = ".";

        [Option(ShortName = "o", Description = "Output file or directory path")]
        public string Output { get; set; }

        [Option(ShortName = "t", Description = "Use tabs for indentation")]
        public bool UseTabs { get; set; }

        [Option(ShortName = "ts", Description = "Number of spaces per tab")]
        public int TabSize { get; set; } = 4;

        [Option(ShortName = "se", Description = "Skip 'export' keyword")]
        public bool SkipExport { get; set; }

        [Option(ShortName = "k", Description = "Use kebab case for output file names")]
        public bool UseKebabCase { get; set; }

        [Option(ShortName = "m", Description = "Append '.model' suffix to output file names")]
        public bool AppendModelSuffix { get; set; }

        [Option(ShortName = "c", Description = "Clear output directory")]
        public bool ClearOutputDirectory { get; set; }

        [Option(ShortName = "a", Description = "Use Angular style conventions")]
        public bool AngularMode { get; set; }

        [Option(ShortName = "p", Description = "Override only part of output file between marker comments")]
        public bool PartialOverride { get; set; }

        [Option(ShortName = "pc", Description = "Don't convert field names to camel case")]
        public bool PreserveCasing { get; set; }

        [Option(ShortName = "pip", Description = "Don't remove interface prefixes")]
        public bool PreserveInterfacePrefix { get; set; }

        [Option(ShortName = "d", Description = "Set output type for dates")]
        public DateOutputType ConvertDatesTo { get; set; }

        [Option(ShortName = "n", Description = "Set output type for nullables")]
        public NullableOutputType ConvertNullablesTo { get; set; }

        [Option(ShortName = "i", Description = "Enable import generation")]
        public ImportGenerationMode ImportGeneration { get; set; }

        [Option(ShortName = "q", Description = "Set quotation marks for import statements & identifiers")]
        public QuotationMark QuotationMark { get; set; }

        [Option(ShortName = "anl", Description = "Append new line to end of file (removes TSLint warning)")]
        public bool AppendNewLine { get; set; }

        [Option(ShortName = "ot", Description = "Output type")]
        public OutputType OutputType { get; set; }

        [Option(ShortName = "pm", Description = "Add public modifier keywords")]
        public bool PublicModifier { get; set; }
        public Dictionary<string, string> Imports { get; set; }

        [Option(ShortName = "ao", Description = "Make all fields optional")]
        public bool AllOptional { get; set; }

        [Option(ShortName = "cf", Description = "Config file")]
        public string ConfigFile { get; set; } = "cs2tsconfig.json";

        [Option(ShortName ="ty", Description = "The types to export")]
        public string[] Types { get; set; } = null;

        private void OnBeforeArgumentsSet()
        {
            if (ConfigurationFile.Load(ConfigFile) is Configuration configuration)
            {
                configuration.Override(this);
            }
        }
    }
}