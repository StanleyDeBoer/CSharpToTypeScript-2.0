using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using CSharpToTypeScript.CLITool.Utilities;

namespace CSharpToTypeScript.CLITool.Validation
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationFileDoesNotExist : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            => File.Exists("cs2tsconfig.json")
            ? new ValidationResult("Configuration file already exists.")
            : ValidationResult.Success;
    }
}