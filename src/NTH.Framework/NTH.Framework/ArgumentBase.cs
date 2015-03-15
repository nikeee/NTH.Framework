using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NTH.Diagnostics;

namespace NTH.Framework
{
    internal abstract class ArgumentBase
    {
        protected ArgumentBase()
        {
            // TODO: Parse field/property attributes
            // TODO: Parse command line parameters and values
        }

        public ArgumentList ToArgumentList()
        {
            throw new NotImplementedException();
        }
        public CommandLine ToCommandLine()
        {
            throw new NotImplementedException();
        }

        public static T Parse<T>(string commandLine) where T : ArgumentBase
        {
            throw new NotImplementedException();
        }
    }

    // Action parameters?
    /*
     *
     * --action <name> -param1=10
     * ->
     * <name>(param1: 10);
     *
     */

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    internal class ParameterAttribute : Attribute
    {
        public bool IsRequired { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public Type Type { get; set; }

        protected ParameterAttribute()
        {
            IsRequired = true;
            Alias = null;
            Type = typeof(int);
        }

        public ParameterAttribute(string name, bool isRequred)
            : this()
        {
            Name = name;
            IsRequired = isRequred;
        }

        public ParameterAttribute(string name, bool isRequred, string alias)
            : this(name, isRequred)
        {
            Alias = alias;
        }
    }


    internal class CommandLineOptions : ArgumentBase
    {
        [Parameter("filename", false)]
        public string FileName { get; set; }
    }


    /**
     * CommandLineOptions.Parse<CommandLineOptions>("lol.exe --filename schwanz");
     */
}
